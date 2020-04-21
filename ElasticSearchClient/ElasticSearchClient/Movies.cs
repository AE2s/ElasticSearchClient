using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearchClient
{
    public class Movies
    {
        public string Id { get; set; }

        public string Type { get; set; }

        public Movie Fields { get; set; }
    }
}
