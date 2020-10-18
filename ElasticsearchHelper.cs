using Elasticsearch.Net;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticsearchCRUDDemo
{
    public static class ElasticsearchHelper
    {
        public static ElasticClient GetESClient()
        {
            ConnectionSettings connectionSettings;
            ElasticClient elasticClient;
            StaticConnectionPool connectionPool;

            var nodes = new Uri[] { 
                new Uri("http://2863edd07334.ngrok.io") //Provide your Elasticsearch endpoint.
            };

            connectionPool = new StaticConnectionPool(nodes);
            connectionSettings = new ConnectionSettings(connectionPool);
            elasticClient = new ElasticClient(connectionSettings);

            return elasticClient;
        }

        public static void CreateDocument(ElasticClient elasticClient, string indexName, string typeName, Product product, string documentId)
        {
            var response = elasticClient.Index(product, i => i
            .Index(indexName)
            .Type(typeName)
            .Id(documentId)
            .Refresh(Elasticsearch.Net.Refresh.True));
        }

        public static void GetDocument(ElasticClient elasticClient, string indexName, string typeName, string documentId)
        {
            var response = elasticClient.Search<Product>(s => s
              .Index(indexName)
              .Type(typeName)
              .Query(q => q.Term(t => t.Field("_id").Value(documentId))));
            foreach (var hit in response.Hits)
            {
                Console.WriteLine("Id:{0} Name:{1} Description:{2}", hit.Source.id, hit.Source.name, hit.Source.description);
            }

        }

        public static void UpdateDocument(ElasticClient elasticClient, string indexName, string typeName, Product product, string documentId)
        {
            var response = elasticClient.Index(product, i => i
                  .Index(indexName)
                  .Type(typeName)
                  .Id(documentId)
                  .Refresh(Elasticsearch.Net.Refresh.True));
        }

        public static void DeleteDocument(ElasticClient elasticClient, string indexName, string typeName, string documentId)
        {
            var response = elasticClient.Delete<Product>(documentId, d => d
           .Index(indexName)
           .Type(typeName));
        }

    }
}
