using System;
using System.Collections.Generic;
using System.Linq;

namespace BookDbSharp
{
	[Serializable]
	public class BookLibrary
	{
		private readonly List<Book> books = new List<Book>();

		public List<Book> Books
		{
			get { return books; }
		}

		public ReadingQueue ReadingQueue { get; protected set; }

		public BookLibrary()
		{
			ReadingQueue = new ReadingQueue();
		}

		public LibraryDiff Refresh(Book[] foundBooksThisTime)
		{
			List<Book> newBooks = new List<Book>();
			List<Book> removedBooks = new List<Book>();

			var existingPaths = foundBooksThisTime.Select(x => x.Path);
			var orphanedBooks = new List<Book>();
			var updatedBooks = new List<Book>();

			foreach (var book in Books)
			{
				if (!existingPaths.Contains(book.Path, StringComparer.InvariantCultureIgnoreCase))
					orphanedBooks.Add(book);
			}

			foreach (var orphanedBook in orphanedBooks)
			{
				Books.Remove(orphanedBook);
			}

			removedBooks.AddRange(orphanedBooks);

			foreach (var foundBook in foundBooksThisTime)
			{
				var existingBook = Books.SingleOrDefault(x => x.Path == foundBook.Path);

				if (existingBook != null)
				{
					// if size or timestamp changed
					if (existingBook.Size != foundBook.Size ||
						foundBook.Timestamp.Subtract(existingBook.Timestamp).Milliseconds > 0)
					{
						existingBook.UpdateFileInfo(foundBook);

						updatedBooks.Add(existingBook);
					}
				}
				else
				{
					Books.Add(foundBook);
					newBooks.Add(foundBook);
				}
			}

			return new LibraryDiff(newBooks, updatedBooks, removedBooks);
		}
	}
}
