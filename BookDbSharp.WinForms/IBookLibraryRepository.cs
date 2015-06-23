namespace BookDbSharp.WinForms
{
	public interface IBookLibraryRepository
	{
		BookLibrary Load();
		void Save(BookLibrary library);
		bool FileExists();
	}
}
