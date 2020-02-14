using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GoThere.Models
{
    public class LocationCityViewModel
    {
        public List<Location> Locations { get; set; }
        public SelectList Cities { get; set; }
        public string LocationCity { get; set; }
        public string SearchString { get; set; }
    }
}