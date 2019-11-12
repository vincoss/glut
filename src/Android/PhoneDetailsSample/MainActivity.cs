using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text;
using System.IO;
using Android.Net;
using Android.Telephony;

[assembly:UsesPermissionAttribute(Name = Android.Manifest.Permission.ReadPhoneState)]
[assembly: UsesPermissionAttribute(Name = Android.Manifest.Permission.AccessCoarseLocation)]

namespace PhoneDetailsSample
{
    
    [Activity(Label = "Phone Detail", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                {
                    var exception = (Exception) e.ExceptionObject;
                };

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            var text = FindViewById<TextView>(Resource.Id.PhoneDetailText);


            var sb = new StringBuilder();
            var writter = new StringWriter(sb);

            NetworkDetails(writter);
            ProcessDetails(writter);
            TelephonyDetails(writter);
            ApplicationInfoDetail(writter);
            Battery(writter);

            text.Text = sb.ToString();
        }

        private void NetworkDetails(TextWriter writer)
        {
            //AndroidEnvironment.UnhandledExceptionRaiser
            var connectivityManager = (ConnectivityManager)GetSystemService(ConnectivityService);

            writer.WriteLine("***** Network Information");
            writer.WriteLine();

            // TODO: this requires "ACCESS_NETWORK_STATE" permission. You have to check if set to call this code otherwise exception will thrown.
            var activeConnection = connectivityManager.ActiveNetworkInfo;

            if (activeConnection == null)
            {
                writer.WriteLine("IsConnected:            {0}", false);
                return;
            }
            
            writer.WriteLine("ExtraInfo:                {0}", activeConnection.ExtraInfo);
            writer.WriteLine("IsAvailable:              {0}", activeConnection.IsAvailable);
            writer.WriteLine("IsConnected:              {0}", activeConnection.IsConnected);
            writer.WriteLine("IsConnectedOrConnecting:  {0}", activeConnection.IsConnectedOrConnecting);
            writer.WriteLine("IsFailover:               {0}", activeConnection.IsFailover);
            writer.WriteLine("IsRoaming:                {0}", activeConnection.IsRoaming);
            writer.WriteLine("Reason:                   {0}", activeConnection.Reason);
            writer.WriteLine("Subtype:                  {0}", activeConnection.Subtype);
            writer.WriteLine("SubtypeName:              {0}", activeConnection.SubtypeName);
            writer.WriteLine("Type:                     {0}", activeConnection.Type);
            writer.WriteLine("TypeName:                 {0}", activeConnection.TypeName);
        }

        private void ProcessDetails(TextWriter writer)
        {
            writer.WriteLine("***** Process Information");
            writer.WriteLine();

            writer.WriteLine("BluetoothGid:             {0}", Process.BluetoothGid);
            writer.WriteLine("ElapsedCpuTime:           {0}", new TimeSpan(Process.ElapsedCpuTime));
            writer.WriteLine("FirstApplicationUid:      {0}", Process.FirstApplicationUid);
            writer.WriteLine("LastApplicationUid:       {0}", Process.LastApplicationUid);
            writer.WriteLine("PhoneUid:                 {0}", Process.PhoneUid);
            writer.WriteLine("SystemUid:                {0}", Process.SystemUid);
        }

        private void TelephonyDetails(TextWriter writer)
        {
            writer.WriteLine("***** Telephony Information");
            writer.WriteLine();

            // TODO: requires permission READ_PHONE_STATE and  ACCESS_COARSE_LOCATION
            var act = (TelephonyManager)GetSystemService(Context.TelephonyService);
           
            writer.WriteLine("CallState: {0}", act.CallState);
            writer.WriteLine("CellLocation: {0}", act.CellLocation);
            writer.WriteLine("Class: {0}", act.Class);
            writer.WriteLine("DataActivity: {0}", act.DataActivity);
            writer.WriteLine("DataState: {0}", act.DataState);
            writer.WriteLine("DeviceId: {0}", act.DeviceId);
            writer.WriteLine("DeviceSoftwareVersion: {0}", act.DeviceSoftwareVersion);
            writer.WriteLine("Handle: {0}", act.Handle);
            writer.WriteLine("HasIccCard: {0}", act.HasIccCard);
            writer.WriteLine("IsNetworkRoaming: {0}", act.IsNetworkRoaming);

            writer.WriteLine("Line1Number: {0}", act.Line1Number);
            writer.WriteLine("NeighboringCellInfo: {0}", act.NeighboringCellInfo);
            writer.WriteLine("NetworkCountryIso: {0}", act.NetworkCountryIso);
            writer.WriteLine("NetworkOperator: {0}", act.NetworkOperator);
            writer.WriteLine("NetworkOperatorName: {0}", act.NetworkOperatorName);
           
            writer.WriteLine("NetworkType: {0}", act.NetworkType);
            writer.WriteLine("PhoneType: {0}", act.PhoneType);
            writer.WriteLine("SimCountryIso: {0}", act.SimCountryIso);

            writer.WriteLine("SimOperator: {0}", act.SimOperator);
            writer.WriteLine("SimOperatorName: {0}", act.SimOperatorName);
            writer.WriteLine("SimSerialNumber: {0}", act.SimSerialNumber);
            writer.WriteLine("SimState: {0}", act.SimState);
            writer.WriteLine("SubscriberId: {0}", act.SubscriberId);

            writer.WriteLine("VoiceMailAlphaTag: {0}", act.VoiceMailAlphaTag);
            writer.WriteLine("VoiceMailNumber: {0}", act.VoiceMailNumber);
           
        }

        private void ApplicationInfoDetail(TextWriter writer)
        {
            writer.WriteLine("***** Application Information");
            writer.WriteLine();

            var info = Android.App.Application.Context.ApplicationInfo;

            writer.WriteLine("BackupAgentName:          {0}", info.BackupAgentName);
            writer.WriteLine("Class:                    {0}", info.Class);
            writer.WriteLine("ClassName:                {0}", info.ClassName);
            writer.WriteLine("CompatibleWidthLimitDp:   {0}", info.CompatibleWidthLimitDp);
            writer.WriteLine("DataDir:                  {0}", info.DataDir);
            writer.WriteLine("DescriptionRes:           {0}", info.DescriptionRes);
            writer.WriteLine("Enabled:                  {0}", info.Enabled);
            writer.WriteLine("Flags:                    {0}", info.Flags);
            writer.WriteLine("Handle:                   {0}", info.Handle);
            writer.WriteLine("Icon:                     {0}", info.Icon);
            writer.WriteLine("LabelRes:                 {0}", info.LabelRes);
            writer.WriteLine("LargestWidthLimitDp:      {0}", info.LargestWidthLimitDp);

            writer.WriteLine("Logo:                     {0}", info.Logo);
            writer.WriteLine("ManageSpaceActivityName:  {0}", info.ManageSpaceActivityName);
            writer.WriteLine("MetaData:                 {0}", info.MetaData);
            writer.WriteLine("Name:                     {0}", info.Name);
            writer.WriteLine("NativeLibraryDir:         {0}", info.NativeLibraryDir);
            writer.WriteLine("NonLocalizedLabel:        {0}", info.NonLocalizedLabel);
            writer.WriteLine("PackageName:              {0}", info.PackageName);
            writer.WriteLine("ProcessName:              {0}", info.ProcessName);
            writer.WriteLine("PublicSourceDir:          {0}", info.PublicSourceDir);

            writer.WriteLine("RequiresSmallestWidthDp:  {0}", info.RequiresSmallestWidthDp);
            writer.WriteLine("SharedLibraryFiles:       {0}", info.SharedLibraryFiles);
            writer.WriteLine("SourceDir:                {0}", info.SourceDir);
            writer.WriteLine("TargetSdkVersion:         {0}", info.TargetSdkVersion);
            writer.WriteLine("TaskAffinity:             {0}", info.TaskAffinity);
            writer.WriteLine("Theme:                    {0}", info.Theme);

            writer.WriteLine("UiOptions:                {0}", info.UiOptions);
            writer.WriteLine("Uid:                      {0}", info.Uid);

        }

        private void Battery(TextWriter writer)
        {
            writer.WriteLine("***** Battery Information");
            writer.WriteLine();

            IntentFilter ifilter = new IntentFilter(Intent.ActionBatteryChanged);
            Intent batteryStatus = this.RegisterReceiver(null, ifilter);

            var status = (Android.OS.BatteryStatus) batteryStatus.GetIntExtra(BatteryManager.ExtraHealth, -1);
            bool isCharging = status == Android.OS.BatteryStatus.Charging || status == Android.OS.BatteryStatus.Full;

            writer.WriteLine("Charging: {0}", status == BatteryStatus.Charging);
            writer.WriteLine("Discharging: {0}", status == BatteryStatus.Discharging);
            writer.WriteLine("Full: {0}", status == BatteryStatus.Full);
            writer.WriteLine("NotCharging: {0}", status == BatteryStatus.NotCharging);
            writer.WriteLine("Unknown: {0}", status == BatteryStatus.Unknown);

            writer.WriteLine("Action: {0}", batteryStatus.Action);

            if (batteryStatus.Categories != null)
            {
                foreach (var c in batteryStatus.Categories)
                {
                    writer.WriteLine("Category: {0}", c);
                }
            }

            writer.WriteLine("Class: {0}", batteryStatus.Class);
            writer.WriteLine("ClipData: {0}", batteryStatus.ClipData);

            if (batteryStatus.Component != null)
            {
                writer.WriteLine("Component.Class: {0}", batteryStatus.Component.Class);
                writer.WriteLine("Component.ClassName: {0}", batteryStatus.Component.ClassName);
                writer.WriteLine("Component.Handle: {0}", batteryStatus.Component.Handle);
                writer.WriteLine("Component.PackageName: {0}", batteryStatus.Component.PackageName);
                writer.WriteLine("Component.ShortClassName: {0}", batteryStatus.Component.ShortClassName);
            }

            writer.WriteLine("Data: {0}", batteryStatus.Data);
            writer.WriteLine("DataString: {0}", batteryStatus.DataString);

            foreach (var key in batteryStatus.Extras.KeySet())
            {
                writer.WriteLine("{0}: {1}", key, batteryStatus.Extras.GetString(key));
            }

            writer.WriteLine("Flags: {0}", batteryStatus.Flags);
            writer.WriteLine("Handle: {0}", batteryStatus.Handle);
            writer.WriteLine("HasFileDescriptors: {0}", batteryStatus.HasFileDescriptors);
            writer.WriteLine("Package: {0}", batteryStatus.Package);
            writer.WriteLine("Scheme: {0}", batteryStatus.Scheme);
            writer.WriteLine("Selector: {0}", batteryStatus.Selector);
            writer.WriteLine("SourceBounds: {0}", batteryStatus.SourceBounds);
            writer.WriteLine("Type: {0}", batteryStatus.Type);
        }
    }
}

