namespace Application.Services.Core;

public class PrintingService : IPrintingService
{
	static HttpClient client = new HttpClient();
	//public async Task<bool> Print(string Header, string args, string PrinterName, string FilePath, string Database)
	public async Task<HttpResponseMessage> Print(string Header, string args, string PrinterName, string FilePath, string Database)
	{
		bool result = false;
		//HttpResponseMessage response;

		//string responseContent = "";
		try
		{
			var response = await client.GetAsync(
			//$"http://localhost:8029/api/Print?Header={Header}&args={args}&PrinterName={PrinterName}&FilePath={FilePath}&Database={Database}");
			$"http://localhost:44308/api/Print?Header={Header}&args={args}&PrinterName={PrinterName}&FilePath={FilePath}&Database={Database}");

			//responseContent = await response.Content.ReadAsStringAsync();

			//response.EnsureSuccessStatusCode();

			//string path = FilePath;

			//string tmpFile = Path.GetTempFileName();
			////TextFile tf = new TextFile(path);
			//File.WriteAllText(tmpFile, Header + args);

			//ReportDocument cryRpt = new ReportDocument();

			//cryRpt.Load(path);

			//var setting = new System.Drawing.Printing.PrinterSettings();

			//cryRpt.PrintOptions.PrinterName = PrinterName;

			//cryRpt.SetDataSource(tmpFile);

			//cryRpt.PrintToPrinter(1, false, 0, 0); // set start and end page to 0 to print all

			//cryRpt.Dispose();
			//cryRpt.Close();

			//result = true;
			return response;
		}
		catch (Exception)
		{

			throw;
		}

		#region Bartender
		//bool result = false;
		//Engine btEngine = new Engine(true);
		//var path = FilePath;
		//btEngine.Start();
		//try
		//{
		//    LabelFormatDocument btFormat = btEngine.Documents.Open(path);
		//    btFormat.PrintSetup.PrinterName = PrinterName;

		//string tmpFile = Path.GetTempFileName();
		//TextFile tf = new TextFile(path);
		//System.IO.File.WriteAllText(tmpFile, Header + args);
		//TextFile tf = new TextFile(btFormat.DatabaseConnections[0].Name);
		//tf.FileName = tmpFile;

		//    btFormat.DatabaseConnections.SetDatabaseConnection(tf);
		//    btFormat.PrintSetup.ReloadTextDatabaseFields = true;

		//    string messageString = "\nDocument\\s: \n";
		//    Messages messages;


		//    Result res = btFormat.Print("Item List", 10000, out messages);
		//    //btFormat.Print();

		//    result = res != Result.Failure;

		//    //result = true;

		//    foreach (Message message in messages)
		//    {
		//        if (message.Category == "Printing" && !message.Text.Contains("successfully"))
		//        {
		//            messageString += "\n ";
		//        }
		//    }
		//    btEngine.Stop();
		//    return result;
		//}
		//catch (Exception ex)
		//{
		//    btEngine.Stop();
		//    throw;
		//}
		#endregion
	}
}
