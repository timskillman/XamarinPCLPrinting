# XamarinPCLPrinting

Updated bushberts code to print on Android - was missing assembly declaration and 'CustomPrintAdapter.cs' taken from;

https://forums.xamarin.com/discussion/171834/print-pdf-file-using-xamarin-forms

CustomPrintAdapter.cs was written by DeepakDY 

Please note that correctly formed PDF needs creating before printing to Android.  
SkiaSharp makes is easy to do this, see;

https://github.com/mono/SkiaSharp/blob/master/samples/Gallery/Shared/Samples/CreatePdfSample.cs#L47

Currently, this example continues to pass a bitmap resulting in the error 'cannot print a malformed PDF file'
