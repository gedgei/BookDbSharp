using System.Collections.Generic;

namespace BookDbSharp
{
	public class LibraryDiff
	{
		public List<Book> NewBooks { get; private set; }
		public List<Book> UpdatedBooks { get; private set; }
		public List<Book> RemovedBooks { get; private set; }

		public LibraryDiff(List<Book> newBooks, List<Book> updatedBooks, List<Book> removedBooks)
		{
			NewBooks = newBooks;
			UpdatedBooks = updatedBooks;
			RemovedBooks = removedBooks;
		}
	}
}