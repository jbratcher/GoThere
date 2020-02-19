using GoThere.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace GoThere.ViewModels
{
    public class LocationCityViewModel
    {
        public List<Location> Locations { get; set; }
        public SelectList Cities { get; set; }
        public string LocationCity { get; set; }
        public string SearchString { get; set; }
    }
}