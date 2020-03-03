using GoThere.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GoThere.ViewModels
{
    public class EventFilterViewModel
    {
        public List<Event> Events { get; set; }
        public SelectList Categories { get; set; }
        public string EventCategory { get; set; }
        public string SearchString { get; set; }
    }
}