using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoThere.Models
{
    public class Event
    {

        public double Relevance { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [NotMapped]
        public List<string> Labels { get; set; }
        public int Rank { get; set; }
        public int Local_rank { get; set; }
        [NotMapped]
        public List<object> Entities { get; set; }
        public int Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime Updated { get; set; }
        public DateTime First_seen { get; set; }
        public string Timezone { get; set; }
        [NotMapped]
        public List<double> Location { get; set; }
        public string Scope { get; set; }
        public string Country { get; set; }
        [NotMapped]
        public List<List<string>> Place_hierarchies { get; set; }
        public string State { get; set; }

    }
}