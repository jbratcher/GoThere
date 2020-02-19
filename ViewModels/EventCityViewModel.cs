using GoThere.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GoThere.ViewModels
{
    public class EventCityViewModel
    {
        public List<Event> Events { get; set; }
        public SelectList Cities { get; set; }
        public string LocationCity { get; set; }
        public string SearchString { get; set; }
    }
}