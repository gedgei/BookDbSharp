using System.Collections.Generic;

namespace BookDbSharp
{
	public interface IBookDirectory
	{
		IEnumerable<Book> Iterate();
	}
}