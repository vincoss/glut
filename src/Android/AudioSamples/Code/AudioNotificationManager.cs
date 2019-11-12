using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AudioSamples.Code
{
    //
    // Class used to manage audio notifications.
    //
    public class AudioNotificationManager
    {
        static public AudioManager audioManager = null;
        static Activity mainActivity = null;

        public static Activity MainActivity
        {
            set { mainActivity = value; }
        }

        AudioManager.IOnAudioFocusChangeListener listener = null;

        private class FocusChangeListener : Java.Lang.Object, AudioManager.IOnAudioFocusChangeListener
        {
            INotificationReceiver parent = null;

            public FocusChangeListener(INotificationReceiver parent)
            {
                this.parent = parent;
            }

            void SetStatus(String message)
            {
                if (mainActivity is AudioActivity)
                {
                    AudioActivity wact = mainActivity as AudioActivity;
                    wact.setStatus(message);
                }
            }

            public void OnAudioFocusChange(AudioFocus focusChange)
            {
                switch (focusChange)
                {
                    // We will take any flavor of AudioFocusgain that the system gives us and use it.
                    case AudioFocus.GainTransient:
                    case AudioFocus.GainTransientMayDuck:
                    case AudioFocus.Gain:
                        parent.StartAsync();
                        SetStatus("Granted");
                        break;
                    // If we get any notificationthat removes focus - just terminate what we were doing.
                    case AudioFocus.LossTransientCanDuck:
                    case AudioFocus.LossTransient:
                    case AudioFocus.Loss:
                        parent.Stop();
                        SetStatus("Removed");
                        break;
                    default:
                        break;
                }
            }
        }

        public Boolean RequestAudioResources(INotificationReceiver parent)
        {
            listener = new FocusChangeListener(parent);

            var ret = audioManager.RequestAudioFocus(listener, Stream.Music, AudioFocus.Gain);
            if (ret == AudioFocusRequest.Granted)
            {
                return (true);
            }
            else if (ret == AudioFocusRequest.Failed)
            {
                return (false);
            }
            return (false);
        }

        public void ReleaseAudioResources()
        {
            if (listener != null)
                audioManager.AbandonAudioFocus(listener);
        }
    }
}