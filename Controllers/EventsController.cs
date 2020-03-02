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

namespace GoThere.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private static readonly string ApiAccessToken = "cBkkg4ZBqtEyJF1wPACn9HO2Rg0f3JOOjqwEUd3L";


        // GET: Events
        [AllowAnonymous]
        public async Task<IActionResult> Index(string eventType, string eventOccupation, string eventIndustry, string eventCity, string eventState, string searchString)
        {
            // get list of events
            var events = from l in _context.Events
                         select l;

            // Use LINQ to get list of types
            IQueryable<string> typeQuery = from t in _context.Events
                                           orderby t.Type
                                           select t.Type;

            // Use LINQ to get list of occupations
            IQueryable<string> occupationQuery = from o in _context.Events
                                               orderby o.Industry
                                               select o.Industry;

            // Use LINQ to get list of industries
            IQueryable<string> industryQuery = from i in _context.Events
                                               orderby i.Industry
                                               select i.Industry;

            // Use LINQ to get list of cities.
            IQueryable<string> cityQuery = from c in _context.Events
                                           orderby c.City
                                           select c.City;

            // Use LINQ to get list of states.
            IQueryable<string> stateQuery = from s in _context.Events
                                            orderby s.City
                                            select s.City;

            // check for name search string
            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.Name.Contains(searchString));
            }

            // check for industry selection
            if (!string.IsNullOrEmpty(eventType))
            {
                events = events.Where(x => x.Type== eventType);
            }

            // check for industry selection
            if (!string.IsNullOrEmpty(eventOccupation))
            {
                events = events.Where(x => x.Occupation== eventOccupation);
            }

            // check for industry selection
            if (!string.IsNullOrEmpty(eventIndustry))
            {
                events = events.Where(x => x.Industry == eventIndustry);
            }

            // check for city selection
            if (!string.IsNullOrEmpty(eventCity))
            {
                events = events.Where(x => x.City == eventCity);
            }

            // check for city selection
            if (!string.IsNullOrEmpty(eventState))
            {
                events = events.Where(x => x.State== eventState);
            }

            var eventFilterVM = new EventFilterViewModel
            {
                Events = await events.ToListAsync(),
                Types = new SelectList(await typeQuery.Distinct().ToListAsync()),
                Occupations = new SelectList(await occupationQuery.Distinct().ToListAsync()),
                Industries = new SelectList(await industryQuery.Distinct().ToListAsync()),
                Cities = new SelectList(await cityQuery.Distinct().ToListAsync()),
                States = new SelectList(await stateQuery.Distinct().ToListAsync())
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
        public async Task<IDisposable> SearchNewEvents()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.predicthq.com/v1");
                // client.DefaultRequestHeaders.Add("Authorization", "Bearer " + ApiAccessToken);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiAccessToken);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                var response = await client.GetAsync("/events/");
                response.EnsureSuccessStatusCode();

                return response.Content.ReadAsStringAsync();
            }

        }

    }
}
