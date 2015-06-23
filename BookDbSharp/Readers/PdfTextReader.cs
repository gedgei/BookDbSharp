using System;
using System.IO;
using log4net;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;

namespace BookDbSharp.Readers
{
	public class PdfTextReader : TextReader
	{
		private static readonly ILog Log = LogManager.GetLogger(typeof(PdfTextReader));

		private readonly PDDocument document;

		private PdfTextReader(PDDocument document)
		{
			this.document = document;
		}

		public override string ReadToEnd()
		{
			PDFTextStripper stripper = new PDFTextStripper();
			return stripper.getText(document);
		}

		public override void Close()
		{
			document.close();
		}

		public static PdfTextReader Open(string fileName)
		{
			try
			{
				var pdDocument = PDDocument.load(fileName);
				return new PdfTextReader(pdDocument);
			}
			catch (Exception e)
			{
				Log.Error(e);
				throw;
			}
		}
	}
}
