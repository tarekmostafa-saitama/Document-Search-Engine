using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.AspNetCore.Hosting;

namespace DocumentSearchEngine.Models
{
    public class QueryManager
    {
        public static List<ResultModel> Search(IWebHostEnvironment hostEnvironment, string query)
        {
            var AppLuceneVersion = LuceneVersion.LUCENE_48;

            var indexLocation = hostEnvironment.WebRootPath + "\\Index";
            var dir = FSDirectory.Open(indexLocation);

            //create an analyzer to process the text
            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            //create an index writer
            var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);
            var writer = new IndexWriter(dir, indexConfig);


            var phrase = new MultiPhraseQuery();
            phrase.Add(new Term("content", query));
          //  phrase.Add(new Term("title", query));


            // re-use the writer to get real-time updates
            var searcher = new IndexSearcher(writer.GetReader(applyAllDeletes: true));
            var hits = searcher.Search(phrase, 20 /* top 20 */).ScoreDocs;

            var resultList = new List<ResultModel>();
            foreach (var hit in hits)
            {
                var foundDoc = searcher.Doc(hit.Doc);
               // hit.Score.Dump("Score");
             
                 resultList.Add(new ResultModel()
                 {
                     Title = foundDoc.Get("title"),
                     Url = foundDoc.Get("url")
                 });
            }
            writer.Dispose();
            return resultList;
        }
    }
}
