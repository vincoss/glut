using Android.App;
using Android.Content;
using ServiceSample.Services;


namespace ServiceSample.Broadcasts
{
    [BroadcastReceiver]
    [IntentFilter(new [] { StockService.StocksUpdatedAction }, Priority = (int)IntentFilterPriority.LowPriority)]
    public class StockNotificationReceiver : BroadcastReceiver
    {
        public StockNotificationReceiver()
        {
        }

        public override void OnReceive(Context context, Intent intent)
        {
            const string message = "New stock data is available.";
            var notificationManager = (NotificationManager)context.GetSystemService(Context.NotificationService);
            var notification = new Notification(Resource.Drawable.Icon, message);
            var pendingIntent = PendingIntent.GetActivity(context, 0, new Intent(context, typeof(StockActivity)), 0);
            notification.SetLatestEventInfo(context, "Stocks Updated", message, pendingIntent);
            notificationManager.Notify(0, notification);
        }
    }
}