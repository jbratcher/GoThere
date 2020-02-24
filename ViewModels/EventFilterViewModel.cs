using GoThere.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GoThere.ViewModels
{
    public class EventFilterViewModel
    {
        public List<Event> Events { get; set; }
        public SelectList Types { get; set; }
        public SelectList Occupations { get; set; }
        public SelectList Industries { get; set; }
        public SelectList Cities { get; set; }
        public SelectList States { get; set; }
        public string EventType { get; set; }
        public string EventOccupation {get; set;}
        public string EventIndustry { get; set; }
        public string EventCity { get; set; }
        public string EventState { get; set; }
        public string SearchString { get; set; }
    }
}