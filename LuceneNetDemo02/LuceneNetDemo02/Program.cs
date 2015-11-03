using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneNetDemo02
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            

            // create default Lucene search index directory
            if (!Directory.Exists(LuceneSearch._luceneDir)) Directory.CreateDirectory(LuceneSearch._luceneDir);
            Console.WriteLine("Get entire index");
            sw.Start();
            LuceneSearch.AddUpdateLuceneIndex(Data.SampleDataRepository.GetAll());
            sw.Stop();

            Console.WriteLine("Indexing time = {0}ms", sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            sw.Start();
            var all = LuceneSearch.GetAllIndexRecords();
            sw.Stop();
            Console.WriteLine("Getting all data time = {0}ms", sw.Elapsed.TotalMilliseconds);
            sw.Reset();

            foreach (var sampleData in all)
            {
                Console.WriteLine("item: {0}, {1}, {2}", sampleData.Id, sampleData.Name, sampleData.Description);
            }
            sw.Reset();

            // 
            Console.WriteLine("------");
            Console.WriteLine("Search for all results has form");
            sw.Start();
            var results = LuceneSearch.Search("goformz");
            sw.Stop();
            Console.WriteLine("Search data time = {0}ms", sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            foreach (var sampleData in results)
            {
                Console.WriteLine("result: {0}, {1}, {2}", sampleData.Id, sampleData.Name, sampleData.Description);
            }

            Console.ReadLine();
        }
    }
}
