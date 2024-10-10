using Android.App;
using Android.Content;

namespace Application.MauiBlazor.Platforms.Android
{
    [BroadcastReceiver(Enabled = true, Label = "data", Exported = true)]
    [IntentFilter(new[] { "com.scanner.broadcast" }, Priority = (int)IntentFilterPriority.HighPriority)]
    public class ScanReceiver : BroadcastReceiver
    {
        public static string INTENT_ACTION = "com.scanner.broadcast";
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals("com.scanner.broadcast"))
            {
                var data = intent.GetSerializableExtra("data"); // barcode data
                var codeType = intent.GetSerializableExtra("codeType"); // Barcode symbology
                // Business logic processing goes here
            }
        }

    }
}
