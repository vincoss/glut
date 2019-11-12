using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Provider;
using Java.IO;


namespace CameraSamples.Droid
{
    [Activity(Label = "CameraSamples", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        private File _file = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            _file = GetFile();

            (Xamarin.Forms.Application.Current as App).ShouldTakePicture += () =>
            {
                var intent = new Intent(MediaStore.ActionImageCapture);

                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
                StartActivityForResult(intent, 0);
            };
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            (Xamarin.Forms.Application.Current as App).SetImagePath(_file.Path);
        }

        private File GetFile()
        {
            File pictureFolder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures);
            File corePhotoDirectory = new File(pictureFolder, "CorePhoto");
            File file = new File(corePhotoDirectory, "Temp.jpg");

            var exists = corePhotoDirectory.Exists();

            if (exists == false)
            {
                corePhotoDirectory.Mkdirs();
            }

            return file;
        }
    }
}

