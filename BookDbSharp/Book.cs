using System;

namespace BookDbSharp
{
	[Serializable]
	public class Book
	{
		private readonly Guid id;
		private readonly string path;
		private long size;
		private DateTime timestamp;
		private string title;

		public Guid Id
		{
			get { return id; }
		}

		public string Path
		{
			get { return path; }
		}

		public long Size
		{
			get { return size; }
		}

		public DateTime Timestamp
		{
			get { return timestamp; }
		}

		public string Title
		{
			get { return title; }
		}

		protected Book()
		{
			// for serializer
		}

		public Book(string path, string title, long size, DateTime timestamp)
		{
			this.id = Guid.NewGuid();
			this.title = title;
			this.path = path;
			this.size = size;
			this.timestamp = timestamp;
		}

		public void UpdateFileInfo(Book book)
		{
			this.size = book.Size;
			this.timestamp = book.Timestamp;
		}
	}
}