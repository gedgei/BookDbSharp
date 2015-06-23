using System.IO;
using System.Runtime.Serialization.Json;

namespace BookDbSharp.WinForms
{
	public class JsonFileBookLibraryRepository: IBookLibraryRepository
	{
		private readonly string filePath;

		public JsonFileBookLibraryRepository(string filePath)
		{
			this.filePath = filePath;
		}

		public BookLibrary Load()
		{
			using (var file = File.OpenRead(filePath))
			{
				return (BookLibrary)new DataContractJsonSerializer(typeof(BookLibrary)).ReadObject(file);
			}
		}

		public void Save(BookLibrary library)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				new DataContractJsonSerializer(typeof(BookLibrary)).WriteObject(ms, library);
				ms.Position = 0;

				using (var reader = new StreamReader(ms))
				{
					File.WriteAllText(filePath, reader.ReadToEnd());
				}
			}
		}

		public bool FileExists()
		{
			return File.Exists(filePath);
		}
	}
}
