using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Version = Lucene.Net.Util.Version;

namespace LuceneNetDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            // A directory is a place where Lucene stores the data we add to it, the Documents. 
            Directory directory =  new RAMDirectory();
            // represents a policy for extracting index terms from text
            Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_30);
            // which will handle the actual writing of Documents to the Directory with the help of the Analyzer.
            IndexWriter indexWriter = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);



            // store some docs
            Document document = new Document();
            string blogEntryBody = "This is some example text for the first blog entry body.";
            Field bodyField = new Field("blogEntryBody", blogEntryBody, Field.Store.YES,
            Field.Index.ANALYZED);
            document.Add(bodyField);
            indexWriter.AddDocument(document);

            Document secondDocument = new Document();
            string secondBlogEntryBody = "This is some example text for the second blog entry body. The body of this blog entry is a bit longer than the first.";
            Field secondBodyField = new Field("blogEntryBody", blogEntryBody, Field.Store.YES,
            Field.Index.ANALYZED);
            document.Add(secondBodyField);
            indexWriter.AddDocument(secondDocument);

            // Searching 
            IndexSearcher indexSearcher = new IndexSearcher(directory);
            QueryParser queryParser = new QueryParser(Version.LUCENE_30, "blogEntryBody", analyzer);
            Query query = queryParser.Parse("example");
            TopDocs topDocs = indexSearcher.Search(query,  10);


            // get results
            int numberOfResults = topDocs.TotalHits;
            string numberOfResultsHeader = string.Format("The search returned {0} results.",
            numberOfResults);
            Console.WriteLine(numberOfResultsHeader);

            for (int i = 0; i < numberOfResults; i++)
            {
                float score = topDocs.ScoreDocs[i].Score;
                string hitHeader = string.Format("\nHit number {0}, with a score of {1}:", i, score);
                Console.WriteLine(hitHeader);
                //Console.WriteLine(topDocs..Get("blogEntryBody"));
            }

            Console.ReadLine();
        }
    }
}
