using Microsoft.Office.Interop.Word;
using static System.Net.Mime.MediaTypeNames;

public class DocumentService
{
    public byte[] CreateWordDocument(string content)
    {
        Microsoft.Office.Interop.Word.Application wordApp = null;
        Document doc = null;
        MemoryStream memoryStream = new MemoryStream();

        try
        {
            wordApp = new Microsoft.Office.Interop.Word.Application();
            wordApp.Visible = false;

            doc = wordApp.Documents.Add();
            doc.Content.Text = content;

            // Save to a memory stream
            doc.SaveAs2(memoryStream, WdSaveFormat.wdFormatDocumentDefault);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream.ToArray();
        }
        catch (Exception ex)
        {
            // Log the exception (ex) here
            throw new Exception("Error creating Word document.", ex);
        }
        finally
        {
            if (doc != null)
            {
                doc.Close(false);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(doc);
            }

            if (wordApp != null)
            {
                wordApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApp);
            }

            memoryStream.Close();
        }
    }
}
