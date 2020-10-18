using Nest;
using System;

namespace ElasticsearchCRUDDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string INDEX_NAME = "newtestdb";
            string TYPE_NAME = "product";
            string documentId = "1";
            Product product = new Product
            {
                id = documentId,
                name = "Samsung TV",
                category = "LCD TV",
                description = "Samsung LCD 100 inch TV",
                isPopularProduct = 1,
                price = 50000,
                keyWords = "Tv,LCD,SAMSUNG TV"
            };

            //1.Connect to Elastic Search.
            ElasticClient elasticClient = ElasticsearchHelper.GetESClient();

            //2.Creating Documents In Elasticsearch.
             ElasticsearchHelper.CreateDocument(elasticClient, INDEX_NAME,TYPE_NAME, product,documentId);

            //3.Getting Documents From Elasticsearch.
            ElasticsearchHelper.GetDocument(elasticClient, INDEX_NAME, TYPE_NAME,documentId);

            //4.Updating Documents In Elasticsearch.
            // ElasticsearchHelper.UpdateDocument(elasticClient, INDEX_NAME, TYPE_NAME, product, documentId);

            //5.Deleting Documents From Elasticsearch.
            ElasticsearchHelper.DeleteDocument(elasticClient, INDEX_NAME, TYPE_NAME, documentId);
            Console.ReadKey();
        }
    }

}
