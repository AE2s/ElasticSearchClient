using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearchClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ElasticClient(new ConnectionSettings().DefaultIndex("movies2"));

            var mapping = await client.MapAsync<Movies>(x => x.Properties(props =>
                props.Object<Dictionary<string, object>>(s => s.Name(n => n.Fields))
            ));

            var searchResponse = await client.SearchAsync<Movies>(x => x
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Fields.Title
                        ).Query("Star Wars")
                    )
                )
            );
            Console.WriteLine(searchResponse.Documents.Count);

            searchResponse = await client.SearchAsync<Movies>(s => s
               //.AllIndices()
               .From(0)
               .Size(10)
               .Query(q => q
                   .Match(m => m
                       .Field(f => f.Fields.Actors)
                       .Query("Harrison Ford")
                   )
               )
           );

            Console.WriteLine(searchResponse.Documents.Count);

            searchResponse = await client.SearchAsync<Movies>(s => s
                .Query(q => q
                    .Bool(m => m
                        .Must(must => must.MatchPhrase(ma => ma.Field(f => f.Fields.Title).Query("Star Wars")))
                        .Must(must => must.MatchPhrase(ma => ma.Field(f => f.Fields.Directors).Query("George Lucas")))
                    )
                )
            );

            Console.WriteLine(searchResponse.Documents.Count);


            //aggregats
            var searchAggregationsResponse = await client.SearchAsync<Movies>(s =>
                s.Aggregations(a =>
                    a.Terms("note moyenne",
                        t => t.Field(f => f.Fields.Rating)))
            );

            var results = searchAggregationsResponse
                .Aggregations
                .Terms("note moyenne")
                .Buckets
                .Select(e => new
                {
                    e.Key,
                    count = e.DocCount
                });


            foreach (var rate in results)
            {
                Console.WriteLine($"note : {rate.Key} => nombres de films : {rate.count}");
            }

            Console.ReadKey();
        }
    }
}
