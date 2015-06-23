using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BookDbSharp
{
	[Serializable]
	public class ReadingQueue: IEnumerable<ReadingQueueEntry>
	{
		private readonly List<ReadingQueueEntry> readingQueue = new List<ReadingQueueEntry>();

		public void Enqueue(Guid bookId, int position)
		{
			readingQueue.Insert(position, new ReadingQueueEntry
			{
				BookId = bookId,
				IsStarted = false
			});
		}

		public void Reorder(Guid bookId, int newPosition)
		{
			var insertAt = newPosition;
			var bookToReorder = readingQueue.Single(x => x.BookId == bookId);

			readingQueue.Remove(bookToReorder);
			readingQueue.Insert(insertAt, bookToReorder);
		}

		public void Remove(Guid bookId)
		{
			var item = readingQueue.Single(x => x.BookId == bookId);
			readingQueue.Remove(item);
		}

		public IEnumerator<ReadingQueueEntry> GetEnumerator()
		{
			return readingQueue.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}