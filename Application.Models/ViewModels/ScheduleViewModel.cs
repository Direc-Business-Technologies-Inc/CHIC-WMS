namespace Application.Models.ViewModels;

public class ScheduleViewModel
{
	public Schedules ScheduleDetails { get; set; } = new Schedules();
	public List<Schedules> ScheduleList { get; set; }
	public List<Items> ItemList { get; set; } = new List<Items>();
	public class Schedules
	{
		public int DocEntry { get; set; }
		public int DocNum { get; set; }
		public string DocStatus { get; set; }
		public string PONo { get; set; }
		public string MnfLotNo { get; set; }
		public string SrvcOrdrNo { get; set; }
		public DateTime DeliveryDate { get; set; }
		public DateTime U_PickUpDate { get; set; }
		public DateTime U_IrridiationDate { get; set; }
		public DateTime U_IrridiationStart { get; set; }
		public DateTime U_IrridiationEnd { get; set; }
		public TimeSpan Duration { get; set; }
		public string U_PaymentSettlement { get; set; }
		public string CardName { get; set; }
		public string ItemCode { get; set; }
		public string ItemName { get; set; }
		public string NoOfBoxes { get; set; }
		public string NoOfPallets { get; set; }
		public string NoOfBoxesPerPallet { get; set; }
		public string Remarks { get; set; }
		public string NoOfDosimeters { get; set; }
		public string FacilityLocationCode { get; set; }
		public FacilityLocation FacilityLocation { get; set; }
	}

	public class FacilityLocation
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public string Duration { get; set; }
	}

	public class Items
	{
		public string ItemCode { get; set; }
		public string Dscription { get; set; }
		public int Quantity { get; set; }
		public string Status { get; set; }
		public string BoxBatch { get; set; }
		public string PalletNumber { get; set; }
	}
	public ScheduleHeader scheduleHeader { get; set; }
	public class ScheduleHeader
	{
		public string U_PaymentSettlement { get; set; }
		public string U_IrridiationDate { get; set; }
		public string U_IrridiationStart { get; set; }
		public string U_IrridiationEnd { get; set; }
		public string U_Remarks { get; set; }
		public string FacilityLocation { get; set; }
	}

	#region For Calendar View
	public List<ScheduleEvents> events { get; set; }
	public class ScheduleEvents
	{
		public int id { get; set; }
		public string url { get; set; }
		public string title { get; set; }
		public string start { get; set; }
		public string end { get; set; }
		public bool allDay { get; set; }
		public extendedProps extendedProps { get; set; }

	}

	public class extendedProps
	{
		public string calendar { get; set; }
		public string description { get; set; }
		public string itemname { get; set; }
		public string noofdosimeters { get; set; }
		public string start { get; set; }
		public string end { get; set; }
	}
	public Dictionary<string,double> availableHours { get; set; }
	#endregion
}
