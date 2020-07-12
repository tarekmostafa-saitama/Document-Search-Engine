using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.AspNetCore.Hosting;

namespace DocumentSearchEngine.Models
{
    public class FileIndexingManager
    {
        public static void AddToIndex(IWebHostEnvironment hostEnvironment, string docTitle, string docContent,string url)
        {
            // Ensures index backwards compatibility
            var AppLuceneVersion = LuceneVersion.LUCENE_48;

            var indexLocation = hostEnvironment.WebRootPath+"\\Index";
            var dir = FSDirectory.Open(indexLocation);

            //create an analyzer to process the text
            var analyzer = new StandardAnalyzer(AppLuceneVersion);

            //create an index writer
            var indexConfig = new IndexWriterConfig(AppLuceneVersion, analyzer);
            var writer = new IndexWriter(dir, indexConfig);

          
            Document doc = new Document
            {
                new StringField("title",
                    docTitle,
                    Field.Store.YES),
                new TextField("content",
                    docContent,
                    Field.Store.NO),
                new TextField("url",
                    url,
                    Field.Store.YES)
            };
          
            writer.AddDocument(doc);
            writer.Commit();
            writer.Dispose();
          //  writer.Flush(triggerMerge: false, applyAllDeletes: false);
        }
    }
}
