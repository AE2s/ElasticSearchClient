using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace ElasticSearchClient
{
    public class Person
    {
        public int Id { get; set; }
        [Keyword]
        public string FirstName { get; set; }
        [Keyword]
        public string LastName { get; set; }
    }
}
