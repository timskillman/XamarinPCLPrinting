using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Print;

using Java.IO;

// Taken from https://forums.xamarin.com/discussion/171834/print-pdf-file-using-xamarin-forms, DeepakDY's code

namespace PCLPrintExample.Droid
{
    internal class CustomPrintDocumentAdapter : PrintDocumentAdapter
    {
        private string filePath;
        public CustomPrintDocumentAdapter(string filePath)
        {
            this.filePath = filePath;
        }
        public override void OnLayout(PrintAttributes oldAttributes, PrintAttributes newAttributes, CancellationSignal cancellationSignal, LayoutResultCallback callback, Bundle extras)
        {
            if (cancellationSignal.IsCanceled)
            {
                callback.OnLayoutCancelled();
                return;
            }
            callback.OnLayoutFinished(new PrintDocumentInfo.Builder(filePath)
            .SetContentType(PrintContentType.Document)
            .Build(), true);
        }

        public override void OnWrite(PageRange[] pages, ParcelFileDescriptor destination, CancellationSignal cancellationSignal, WriteResultCallback callback)
        {
            try
            {
                using (InputStream input = new FileInputStream(filePath))
                {
                    using (OutputStream output = new FileOutputStream(destination.FileDescriptor))
                    {
                        var buf = new byte[1024];
                        int bytesRead;
                        while ((bytesRead = input.Read(buf)) > 0)
                        {
                            output.Write(buf, 0, bytesRead);
                        }
                    }
                }
                callback.OnWriteFinished(new[] { PageRange.AllPages });
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                System.Diagnostics.Debug.WriteLine(fileNotFoundException);
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }
        }
    }
}
