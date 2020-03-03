using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using GoThere.Data;
using GoThere.Models;
using GoThere.ViewModels;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity;
using GoThere.Areas.Identity.Data;

namespace GoThere.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private static readonly string ApiAccessToken = "cBkkg4ZBqtEyJF1wPACn9HO2Rg0f3JOOjqwEUd3L";


        // GET: Events
        [AllowAnonymous]
        public async Task<IActionResult> Index(string eventCategory, string searchString)
        {
            // get list of events
            var events = from l in _context.Events
                         select l;

            // Use LINQ to get list of types
            IQueryable<string> categoryQuery = from t in _context.Events
                                           orderby t.Category
                                           select t.Category;

            // check for name search string
            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.Title.Contains(searchString));
            }

            // check for industry selection
            if (!string.IsNullOrEmpty(eventCategory))
            {
                events = events.Where(x => x.Category== eventCategory);
            }

            var eventFilterVM = new EventFilterViewModel
            {
                Events = await events.ToListAsync(),
                Categories = new SelectList(await categoryQuery.Distinct().ToListAsync()),
            };

            return View(eventFilterVM);
        }

        // GET: Events/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Type,StartDateTime,EndDateTime,Price,LocationName,StreetAddress,City,State,Country,PostalCode,Occupation,Industry")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Description,Type,StartDateTime,EndDateTime,Price,LocationName,StreetAddress,City,State,Country,PostalCode,Occupation,Industry")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var @event = await _context.Events.FindAsync(id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(string id)
        {
            return _context.Events.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> SearchNewEvents()
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);

            string categories = "?category=conferences,expos";
            string query = $"?q={applicationUser.Occupation}";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.predicthq.com/v1");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiAccessToken);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.GetAsync($"/events/{categories}&{query}&country=US");
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                JObject eventSearch = JObject.Parse(responseString);
                IList<JToken> results = eventSearch["results"].Children().ToList();
                IList<Event> searchResults = new List<Event>();

                foreach (JToken result in results)
                {
                    // JToken.ToObject is a helper method that uses JsonSerializer internally
                    Event searchResult = result.ToObject<Event>();
                    searchResults.Add(searchResult);

                    if (!EventExists(searchResult.Id))
                    {
                        _context.Add(searchResult);
                    }
                    else
                    {
                        _context.Update(searchResult);
                    }

                    await _context.SaveChangesAsync();
                }

                return Redirect("/Events");

            }
        }

    }
}
