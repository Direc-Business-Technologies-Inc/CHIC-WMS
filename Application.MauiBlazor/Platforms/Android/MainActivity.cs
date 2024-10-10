using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Webkit;
using static Android.Webkit.WebChromeClient;
using jb = Application.Libraries.Utilies.InvengoUtilities;

namespace Application.MauiBlazor
{
    
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity //, BarcodeManager.InitResultListerner
    {
        private IValueCallback _filePathCallback;
        private int _requestCode = 100;
        //private jb.BarcodeManager barcodeManager;


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (_requestCode == requestCode)
            {
                if (_filePathCallback == null)
                    return;

                Java.Lang.Object result = FileChooserParams.ParseResult((int)resultCode, data);
                _filePathCallback.OnReceiveValue(result);
            }
        }

        public bool ChooseFile(IValueCallback filePathCallback, Intent intent, string title)
        {
            _filePathCallback = filePathCallback;

            StartActivityForResult(Intent.CreateChooser(intent, title), _requestCode);

            return true;
        }
        public MainActivity()
        {
            initData();
        }
        private void initData()
        {
            //barcodeManager = com.android.scanner.BarcodeManager.getInstance();
            //barcodeManager.init(this, this);

            //IntentFilter intentFilter = new IntentFilter("com.scanner.broadcast");
            //Broadcast br = new();
            //this.RegisterReceiver(br, intentFilter);
            //barcodeManager.startScan();

        }

        public void initResult(bool b)
        {
            throw new NotImplementedException();
        }
    }

    //public class Broadcast : BroadcastReceiver
    //{
    //    List<t_Scan> listScan = new List<t_Scan>();
    //    public override void OnReceive(Context context, Intent intent)
    //    {
    //        if (intent.Action != null && intent.Action.Equals("com.scanner.broadcast"))
    //        {
    //            //barcode data
    //            String data = intent.GetStringExtra("data");
    //            //barcode system
    //            String codeType = intent.GetStringExtra("codeType");

    //            if (data != null)
    //            {
    //                bool isExists = false;
    //                foreach (t_Scan tagInfo in listScan)
    //                {
    //                    if (data.Equals(tagInfo.getBarcode()))
    //                    {
    //                        isExists = true;
    //                        int oldNumber = tagInfo.getQty();
    //                        tagInfo.setQty(oldNumber + 1);
    //                        tagInfo.setCodeType(codeType);
    //                        break;
    //                    }
    //                }
    //                if (!isExists)
    //                {
    //                    t_Scan newEntity = new t_Scan();
    //                    newEntity.setBarcode(data);
    //                    newEntity.setQty(1);
    //                    newEntity.setCodeType(codeType);
    //                    listScan.Add(newEntity);
    //                }
    //                //mAdapter.notifyDataSetChanged();
    //                //tv_count.setText(String.valueOf(listScan.size()));
    //                //btn_start.setText(R.string.start_scan);
    //                //isStart = false;
    //                //btn_start.setBackgroundResource(R.drawable.btn_background_gray);
    //            }
    //        }
    //    }
    //}

    //public class t_Scan
    //{
    //    public String getBarcode()
    //    {
    //        return Barcode;
    //    }

    //    public void setBarcode(String barcode)
    //    {
    //        Barcode = barcode;
    //    }

    //    public int getQty()
    //    {
    //        return Qty;
    //    }

    //    public void setQty(int qty)
    //    {
    //        Qty = qty;
    //    }

    //    public String getCodeType()
    //    {
    //        return codeType;
    //    }

    //    public void setCodeType(String codeType)
    //    {
    //        this.codeType = codeType;
    //    }

    //    private String Barcode;
    //    private String codeType;
    //    private int Qty;
    //}
}