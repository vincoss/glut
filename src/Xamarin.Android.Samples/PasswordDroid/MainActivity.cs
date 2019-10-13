﻿using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace PasswordDroid
{
    [Activity(Label = "Phoneworld", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our UI controls from the loaded layout
            EditText phoneNumberText = FindViewById<EditText>(Resource.Id.PhoneNumberText);
            Button translateButton = FindViewById<Button>(Resource.Id.TranslateButton);
            Button callButton = FindViewById<Button>(Resource.Id.CallButton);
            Button callHistoryButton = FindViewById<Button>(Resource.Id.CallHistoryButton);

            // Disable the "Call" button
            callButton.Enabled = false;
           
            // Add code to translate number
            string translatedNumber = string.Empty;

            translateButton.Click += (object sender, EventArgs e) =>
            {
                // Translate user’s alphanumeric phone number to numeric
                translatedNumber = PhonewordTranslator.ToNumber(phoneNumberText.Text);
                if (String.IsNullOrWhiteSpace(translatedNumber))
                {
                    callButton.Text = "Call";
                    callButton.Enabled = false;
                }
                else
                {
                    callButton.Text = "Call " + translatedNumber;
                    callButton.Enabled = true;
                }
            };

            callButton.Click += (object sender, EventArgs e) =>
                {
                    // On "Call" button click, try to dial phone number.
                    var callDialog = new AlertDialog.Builder(this);
                    callDialog.SetMessage("Call " + translatedNumber + "?");
                    callDialog.SetNeutralButton("Call", delegate
                        {
                            // add dialed number to list of called numbers.
                            _phoneNumbers.Add(translatedNumber);
                            // enable the Call History button
                            callHistoryButton.Enabled = true;

                            // Create intent to dial phone
                            var callIntent = new Intent(Intent.ActionCall);
                            callIntent.SetData(Android.Net.Uri.Parse("tel:" + translatedNumber));
                            StartActivity(callIntent);
                        });
                    callDialog.SetNegativeButton("Cancel", delegate { });

                    // Show the alert dialog to the user and wait for response.
                    callDialog.Show();
                };

            callHistoryButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CallHistoryActivity));
                intent.PutStringArrayListExtra("phone_numbers", _phoneNumbers);
                StartActivity(intent);
            };
        }

        private List<string> _phoneNumbers = new List<string>();
    }
}

