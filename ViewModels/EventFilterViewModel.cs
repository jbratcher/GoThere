using GoThere.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GoThere.ViewModels
{
    public class EventFilterViewModel
    {
        public List<Event> Events { get; set; }
        public SelectList Cities { get; set; }
        public SelectList Industries { get; set; }
        public string EventCity { get; set; }
        public string EventIndustry { get; set; }
        public string SearchString { get; set; }
    }
}