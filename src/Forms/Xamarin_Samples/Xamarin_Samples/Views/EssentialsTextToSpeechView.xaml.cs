using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin_Samples.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EssentialsTextToSpeechView : ContentPage
    {
        public EssentialsTextToSpeechView()
        {
            InitializeComponent();
        }

        private async void btnSpeak_Clicked(object sender, EventArgs e)
        {
            await TextToSpeech.SpeakAsync("Hello World");

            TextToSpeech.SpeakAsync("Hello World").ContinueWith((t) =>
            {
                // Logic that will run after utterance finishes.

            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        CancellationTokenSource cts;
        public async Task SpeakNowDefaultSettings()
        {
            cts = new CancellationTokenSource();
            await TextToSpeech.SpeakAsync("Hello World", cancelToken: cts.Token);

            // This method will block until utterance finishes.
        }

        // Cancel speech if a cancellation token exists & hasn't been already requested.
        public void CancelSpeech()
        {
            if (cts?.IsCancellationRequested ?? true)
                return;

            cts.Cancel();
        }

        bool isBusy = false;
        public void SpeakMultiple()
        {
            isBusy = true;
            Task.Run(async () =>
            {
                await TextToSpeech.SpeakAsync("Hello World 1");
                await TextToSpeech.SpeakAsync("Hello World 2");
                await TextToSpeech.SpeakAsync("Hello World 3");
                isBusy = false;
            });

            // or you can query multiple without a Task:
            Task.WhenAll(
                TextToSpeech.SpeakAsync("Hello World 1"),
                TextToSpeech.SpeakAsync("Hello World 2"),
                TextToSpeech.SpeakAsync("Hello World 3"))
                .ContinueWith((t) => { isBusy = false; }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async void btnSpeechSetting_Clicked(object sender, EventArgs e)
        {
            var settings = new SpeechOptions()
            {
                Volume = .75f,
                Pitch = 1.0f
            };

            await TextToSpeech.SpeakAsync("Hello World", settings);
        }

        private async void btnSpeechLocales_Clicked(object sender, EventArgs e)
        {
            var locales = await TextToSpeech.GetLocalesAsync();

            // Grab the first locale
            var locale = locales.FirstOrDefault();

            var settings = new SpeechOptions()
            {
                Volume = .75f,
                Pitch = 1.0f,
                Locale = locale
            };

            await TextToSpeech.SpeakAsync("Hello World", settings);
        }

        private async void btnSpeechCancel_Clicked(object sender, EventArgs e)
        {
            SpeakNowDefaultSettings();
        }

        private void btnSpeechMultiple_Clicked(object sender, EventArgs e)
        {
            SpeakMultiple();
        }
    }
}