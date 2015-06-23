using System;
using System.IO;
using log4net;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Version = Lucene.Net.Util.Version;

namespace BookDbSharp.Search
{
	//TODO: implement API for single and multiple document indexing
	public class BookDataIndexer : IBookDataIndexer
	{
		private readonly string indexPath;
		private static readonly ILog Log = LogManager.GetLogger(typeof (BookDataIndexer));

	    private Lucene.Net.Store.Directory directory;

	    public BookDataIndexer(string indexPath)
	    {
		    this.indexPath = indexPath;
	    }

		private IndexWriter CreateWriter()
		{
			if(directory == null)
				directory = new SimpleFSDirectory(new DirectoryInfo(indexPath));

			Analyzer analyzer = new StandardAnalyzer(Version.LUCENE_29);

		    bool createIndex = !IndexReader.IndexExists(directory);

			return new IndexWriter(directory, analyzer, createIndex, IndexWriter.MaxFieldLength.UNLIMITED);
		}

	    public void IndexContent(Guid bookId, TextReader reader)
        {
			using (IndexWriter writer = CreateWriter())
		    {
			    var document = CreateLuceneDocument(bookId, reader);
				
			    writer.AddDocument(document);
			    writer.Optimize();
		    }
        }

	    private Document CreateLuceneDocument(Guid bookId, TextReader reader)
	    {
		    Document document = new Document();

			// we need to index this field for delete to work
		    var idField = new Field("BookId", bookId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
		    idField.OmitTermFreqAndPositions = true;
			document.Add(idField);

		    var field = new Field("Content", reader.ReadToEnd(), Field.Store.NO, Field.Index.ANALYZED_NO_NORMS, Field.TermVector.NO);
			document.Add(field);

		    return document;
	    }

		public void UpdateContent(Guid bookId, TextReader reader)
		{
			using (IndexWriter writer = CreateWriter())
			{
				var document = CreateLuceneDocument(bookId, reader);

				writer.UpdateDocument(new Term("BookId", bookId.ToString()), document);
				writer.Optimize();
			}
		}

	    public void RemoveEntries(Guid[] bookIds)
	    {
			if (bookIds.Length == 0)
				return;

		    using (IndexWriter writer = CreateWriter())
		    {
			    foreach (var bookId in bookIds)
			    {
				    writer.DeleteDocuments(new Term("BookId", bookId.ToString()));
				    writer.Commit();
			    }
		    }
	    }
    }
}
