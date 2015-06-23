using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookDbSharp.Search;
using Lucene.Net.Store;
using NUnit.Framework;

namespace BookDbSharp.Tests
{
    [TestFixture]
    public class SearchTests
    {
        [Test]
        public void TestSimpleSearch()
        {
            var bookGuid = Guid.Parse("11111111-1111-1111-1111-111111111111");

			const string dataToIndex = @"some test string
some other line
next line";
			
			var bookGuid2 = Guid.Parse("11111111-1111-1111-1111-111111111112");

			const string dataToIndex2 = @"some test2 string
some other line
next line";

			//TODO: abstract references to lucene
	        using (var directory = new RAMDirectory())
	        {
				/*var indexer = new BookDataIndexer(directory);
	
				indexer.IndexContent(bookGuid, new StringReader(dataToIndex));
				indexer.IndexContent(bookGuid2, new StringReader(dataToIndex2));


				var searcher = new BookSearchService(directory);

				IEnumerable<SearchResult> searchResults = searcher.Search("test");
				Assert.That(searchResults.Single().BookId, Is.EqualTo(bookGuid));

				searchResults = searcher.Search("test2");
				Assert.That(searchResults.Single().BookId, Is.EqualTo(bookGuid2));*/
			}
        }
    }
}
