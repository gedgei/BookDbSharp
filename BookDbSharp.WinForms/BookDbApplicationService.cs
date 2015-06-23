using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BookDbSharp.Search;
using log4net;

namespace BookDbSharp.WinForms
{
	public class BookDbApplicationService
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(BookDbApplicationService));

		private readonly IBookLibraryRepository bookLibraryRepository;
		private readonly IBookDataIndexer bookIndexer;
		private readonly IBookDirectory directory;
		private readonly Func<BookSearchService> searchServiceFactory;
		private readonly BookFormatRegistrar bookFormatRegistrar;

		private BookLibrary bookLibrary;

		public BookDbApplicationService(IBookLibraryRepository bookLibraryRepository,
			IBookDataIndexer bookIndexer,
			IBookDirectory directory,
			Func<BookSearchService> searchServiceFactory,
			BookFormatRegistrar bookFormatRegistrar)
		{
			this.bookLibraryRepository = bookLibraryRepository;
			this.bookIndexer = bookIndexer;
			this.directory = directory;
			this.searchServiceFactory = searchServiceFactory;
			this.bookFormatRegistrar = bookFormatRegistrar;
		}

		public List<Book> Books
		{
			get { return bookLibrary.Books; }
		}

		public void Load()
		{
			Log.Debug("Loading data from file");

			if (bookLibraryRepository.FileExists())
			{
				Log.Debug("Loading from file");
				bookLibrary = bookLibraryRepository.Load();
			}
			else
			{
				bookLibrary = new BookLibrary();
			}

			Log.Debug("Loading data from file complete");
		}

		public void RefreshLibrary(Action<string> statusUpdate)
		{
			Log.Debug("Checking library for changes..");

			var foundBooksThisTime = directory.Iterate().ToArray();

			var diff = bookLibrary.Refresh(foundBooksThisTime);

			bookIndexer.RemoveEntries(diff.RemovedBooks.Select(x => x.Id).ToArray());

			if (diff.NewBooks.Count == 0 && diff.UpdatedBooks.Count == 0)
			{
				Log.Debug("Nothing to index");
			}
			else
			{
				Index(diff.NewBooks, diff.UpdatedBooks, statusUpdate);
			}

			statusUpdate("Library refreshed");

			//TODO: when to save the library?
			bookLibraryRepository.Save(bookLibrary);
		}

		private void Index(List<Book> toIndex, IEnumerable<Book> booksToReindex, Action<string> statusUpdate)
		{
			Log.Debug("Indexing new/changes items..");

			foreach (var bookToIndex in toIndex.Union(booksToReindex))
			{
				using (TextReader textReader = bookFormatRegistrar.CreateReader(bookToIndex))
				{
					statusUpdate("Indexing " + bookToIndex.Path);

					Log.Debug("Start indexing " + bookToIndex.Path);

					if (toIndex.Contains(bookToIndex))
						bookIndexer.IndexContent(bookToIndex.Id, textReader);
					else
						bookIndexer.UpdateContent(bookToIndex.Id, textReader);

					textReader.Close();

					Log.Debug("End indexing");
				}
			}
		}

		public IEnumerable<Book> Search(string searchString)
		{
			if (string.IsNullOrEmpty(searchString))
				return Books;

			var results = new List<Book>();

			if (!string.IsNullOrEmpty(searchString))
			{
				var bookSearchService = searchServiceFactory.Invoke();
				IEnumerable<SearchResult> searchResults = bookSearchService.Search(searchString);

				foreach (var result in searchResults)
				{
					var book = Books.SingleOrDefault(x => x.Id == result.BookId);

					if(book != null)
						results.Add(book);
					else
						Log.Error("Stale result for keyword '" + searchString + "'. Result book ID: " + result.BookId);
				}
			}

			return results;
		}

		public IEnumerable<ReadingQueueItem> GetReadingQueue()
		{
			return bookLibrary.ReadingQueue.Select(x => new ReadingQueueItem
			{
				Book = bookLibrary.Books.Single(book => book.Id == x.BookId),
				Entry = x
			});
		}

		public void QueueForReading(Guid bookId, int position)
		{
			bookLibrary.ReadingQueue.Enqueue(bookId, position);

			bookLibraryRepository.Save(bookLibrary);
		}

		public void ReorderReadingQueue(Guid bookId, int newPosition)
		{
			bookLibrary.ReadingQueue.Reorder(bookId, newPosition);

			bookLibraryRepository.Save(bookLibrary);
		}

		public void RemoveFromReadingQueue(Guid bookId)
		{
			bookLibrary.ReadingQueue.Remove(bookId);

			bookLibraryRepository.Save(bookLibrary);
		}
	}
}
