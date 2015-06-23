using System;

namespace BookDbSharp.Search
{
    public class SearchResult
    {
	    public Guid BookId { get; set; }
	    public float Score { get; set; }
    }
}