using System;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using BookDbSharp.Search;
using log4net;
using log4net.Config;

namespace BookDbSharp.WinForms
{
    static class Program
    {
	    private static readonly ILog Log = LogManager.GetLogger(typeof (Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
	        var applicationName = Path.GetFileName(Application.ExecutablePath);
			XmlConfigurator.Configure(new FileInfo(applicationName + ".config"));

			Log.Debug("Starting program");

			Application.ThreadException += (sender, args) =>
			{
				Log.Fatal(args.Exception);
			};

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(CreateBookApplicationService()));
        }

	    private static BookDbApplicationService CreateBookApplicationService()
	    {
		    var indexPath = Path.Combine(Application.StartupPath, "Index");
			var libraryFilePath = Path.Combine(Application.StartupPath, "data.txt");

		    var bookFormatRegistrar = new BookFormatRegistrar();

			return new BookDbApplicationService(new JsonFileBookLibraryRepository(libraryFilePath),
				new BookDataIndexer(indexPath),
				new FsBookDirectory(ConfigurationManager.AppSettings["LibraryPath"], bookFormatRegistrar.GetSupportedExtensions()),
				() => new BookSearchService(indexPath),
				bookFormatRegistrar);
		}
    }
}
