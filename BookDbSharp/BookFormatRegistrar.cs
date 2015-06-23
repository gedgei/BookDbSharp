using System.IO;
using BookDbSharp.Readers;

namespace BookDbSharp
{
	/// <summary>
	/// Simplest possible file format registrar implementation
	/// </summary>
	public class BookFormatRegistrar
	{
		public string[] GetSupportedExtensions()
		{
			return new[] {"txt", "pdf", "chm"};
		}

		public TextReader CreateReader(Book book)
		{
			TextReader textReader;

			if (book.Path.EndsWith("pdf"))
				textReader = PdfTextReader.Open(book.Path);
			else if (book.Path.EndsWith("chm"))
				textReader = new ChmTextReader(book.Path);
			else
				textReader = new StreamReader(File.OpenRead(book.Path));

			return textReader;
		}
	}
}