using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using RelatedObjects.Storage;

namespace BookDbSharp.Readers
{
	public class ChmTextReader: TextReader
	{
		private readonly string fileName;

		public ChmTextReader(string fileName)
		{
			this.fileName = fileName;
		}

		public override string ReadToEnd()
		{
			StringBuilder stringBuilder = new StringBuilder();

			ITStorageWrapper iw = new ITStorageWrapper(fileName);

			foreach (IBaseStorageWrapper.FileObjects.FileObject fileObject in iw.foCollection)
			{
				if (fileObject.CanRead)
				{
					if (fileObject.FileName.EndsWith(".htm") || fileObject.FileName.EndsWith(".html"))
					{
						string fileString = fileObject.ReadFromFile();
						stringBuilder.Append(RemoveHtml(fileString));
					}
				}
			}

			return stringBuilder.ToString();
		}

		private string RemoveHtml(string someString)
		{
			string noHtml = Regex.Replace(someString, @"<[^>]+>|&nbsp;", "").Trim();
			string noHtmlNormalised = Regex.Replace(noHtml, @"\s{2,}", " ");

			return noHtmlNormalised;
		}
	}
}