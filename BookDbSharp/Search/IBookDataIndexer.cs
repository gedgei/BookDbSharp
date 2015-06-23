using System;
using System.IO;

namespace BookDbSharp.Search
{
	public interface IBookDataIndexer
	{
		void IndexContent(Guid bookId, TextReader reader);
		void RemoveEntries(Guid[] bookIds);
		void UpdateContent(Guid id, TextReader textReader);
	}
}