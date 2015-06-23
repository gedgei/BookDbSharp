using System.Collections.Generic;
using System.IO;

namespace BookDbSharp
{
	/// <summary>
	/// Iterates through a directory recursively in search for supported book files
	/// </summary>
	public class FsBookDirectory: IBookDirectory
	{
		private readonly string path;
		private readonly string[] extensions;

		public FsBookDirectory(string path, string[] extensions)
		{
			this.path = path;
			this.extensions = extensions;
		}

		public IEnumerable<Book> Iterate()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(path);

			foreach (var extension in extensions)
			{
				foreach (var file in directoryInfo.EnumerateFiles("*." + extension, SearchOption.AllDirectories))
				{
					yield return new Book(file.FullName, Path.GetFileNameWithoutExtension(file.FullName), file.Length, file.LastWriteTimeUtc);
				}
			}
		}
	}
}