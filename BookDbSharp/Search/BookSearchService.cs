using System;
using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Directory = Lucene.Net.Store.Directory;
using Version = Lucene.Net.Util.Version;

namespace BookDbSharp.Search
{
    public class BookSearchService
    {
	    private readonly Directory directory;

	    public BookSearchService(string path)
		{
			directory = new SimpleFSDirectory(new DirectoryInfo(path));
		}

	    public SearchResult[] Search(string searchString)
        {
			Analyzer analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Version.LUCENE_29);

			QueryParser parser = new QueryParser(Version.LUCENE_29, "Content", analyzer);

			var query = parser.Parse(searchString);

			Searcher searcher = new IndexSearcher(Lucene.Net.Index.IndexReader.Open(directory, true));

			TopScoreDocCollector collector = TopScoreDocCollector.Create(100, true);

			searcher.Search(query, collector);
		    var hits = collector.TopDocs().ScoreDocs;

			List<SearchResult> results = new List<SearchResult>();

			for (int i = 0; i < hits.Length; i++)
			{
				int docId = hits[i].Doc;
				float score = hits[i].Score;

				Lucene.Net.Documents.Document doc = searcher.Doc(docId);

				results.Add(new SearchResult
				{
					BookId = Guid.Parse(doc.Get("BookId")),
					Score = score
				});
			}

		    return results.ToArray();
        }
    }
}