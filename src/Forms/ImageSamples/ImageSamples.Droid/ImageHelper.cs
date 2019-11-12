using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Graphics;
using System.IO;
using Android.Content;
using Java.IO;


namespace ImageSamples.Droid
{
    public static class ImageHelper
    {
        public static Stream GetImage(string path, int height, int width)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }

            var stream = new MemoryStream();

            var options = new BitmapFactory.Options
            {
                InJustDecodeBounds = true
            };

            using (var bitmap = BitmapFactory.DecodeFile(path, options)) // NOTE: if path is resource use DecodeResource instead
            {
                if (height > 0 && width > 0)
                {
                    using (var resizeBitmap = GetResizedBitmap(bitmap, width, height))
                    {
                        resizeBitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                    }
                }
                else
                {
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                }
                stream.Position = 0;
            }
            return stream;
        }

        public static Bitmap GetResizedBitmap(this Bitmap originalBitmap, int newWidth, int newHeight)
        {
            if (originalBitmap == null)
            {
                throw new ArgumentNullException("bm");
            }
            if (newWidth <= 0)
            {
                throw new ArgumentException("newHeight");
            }
            if (newHeight <= 0)
            {
                throw new ArgumentException("newHeight");
            }

            int width = originalBitmap.Width;
            int height = originalBitmap.Height;
            float scaleWidth = ((float)newWidth) / width;
            float scaleHeight = ((float)newHeight) / height;

            // Create Matrix for the manipulation.
            Matrix matrix = new Matrix();

            // Resize Bit Map.
            matrix.PostScale(scaleWidth, scaleHeight);

            // Recreate new bitmap.
            Bitmap resizedBitmap = Bitmap.CreateBitmap(originalBitmap, 0, 0, width, height, matrix, false);
            originalBitmap.Recycle();
            return resizedBitmap;
        }

        //https://developer.xamarin.com/recipes/android/resources/general/load_large_bitmaps_efficiently/
        //http://www.mobtowers.com/xamarin-android-using-remote-images-in-lists/
        //http://windingroadway.blogspot.com.au/2014/06/xamarinforms-custom-controls-imagesource.html
        // TEMP remove

//public static void SampleOne()
//        {
//                        var assembly = this.GetType().GetTypeInfo().Assembly; // you can replace "this.GetType()" with "typeof(MyType)", where MyType is any type in your assembly.
//            byte[] buffer;
//            using (Stream s = assembly.GetManifestResourceStream("SymbolMann.jpg”))
//            {
//                long length = s.Length;
//                buffer = new byte[length];
//                s.Read(buffer, 0, (int)length);
//            }
//            // Do something with buffer
//        }
    }
}