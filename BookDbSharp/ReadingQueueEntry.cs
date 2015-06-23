using System;

namespace BookDbSharp
{
	public class ReadingQueueEntry
	{
		public Guid BookId { get; set; }
		public bool IsStarted { get; set; }
	}
}