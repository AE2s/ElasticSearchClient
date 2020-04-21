using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchClient
{
    public class Movie
    {
        public string Title { get; set; }

        public List<string> Directors { get; set; }

        public List<string> Actors { get; set; }

        public DateTime Release_date { get; set; }

        public float Rating { get; set; }

        public float Rank { get; set; }

        public List<string> Genres { get; set; }

        public string Image_url { get; set; }

        public string Plot { get; set; }

        public int Running_time_secs { get; set; }

        public int Year { get; set; }
    }
}
