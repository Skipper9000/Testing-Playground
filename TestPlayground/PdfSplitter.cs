
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace TestPlayground
{
	class PdfSplitter
	{
		static void Main()
		{
			FileInfo incomingFile = new FileInfo(@"C:\Users\skipp\Desktop\9422_MR_binder_sysid_9422.pdf");
			PdfReader reader = null;

			string splitPath = incomingFile.DirectoryName + @"\OcrIn\" + incomingFile.Name;
			Directory.CreateDirectory(splitPath);
			reader = new PdfReader(incomingFile.FullName);
			int pageNameSuffix = 0;
			int interval = 50;
			string pdfFileName = incomingFile.Name.Substring(0, incomingFile.Name.LastIndexOf(".")) + "_OCR_";

			for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber += interval) // split into separate 50 page PDF documents
			{
				pageNameSuffix++;
				string newPdfFileName = string.Format(pdfFileName + "{0}", pageNameSuffix);
				Document document = new Document();
				PdfCopy copy = new PdfCopy(document, new FileStream(splitPath + "\\" + newPdfFileName + ".pdf", FileMode.Create));
				document.Open();

				for (int page = pageNumber; page < (pageNumber + interval); page++)
				{
					if (page <= reader.NumberOfPages)
					{
						copy.AddPage(copy.GetImportedPage(reader, page));
					}
					else break;
				}

				document.Close();
			}
		}
	}
}
