namespace DataManager.Models.CertificateOfIrradiation;

[Table("OCOI")]
public class CertificateOfIrradiation
{
	[Key]
	public int Id { get; set; }
	[ForeignKey("QCOrder")]
	public string QCOrderNo { get; set; }
	public QCOrder.QCOrder QCOrder { get; set; }
	public int DocNo { get; set; }
	public string CustomerCode { get; set; }
	public string CustomerName { get; set; }
	public string CustomerPONo { get;set; }
	public string ItemCode { get; set; }
	public string ItemName { get; set; }
	public DateTime IrradiationDate { get; set; }
	public string ManufacturingLotNo { get; set; }
	public int TotalNoOfBoxes { get; set; }
	public string FacilityName { get; set; }
	public string QCRequester { get; set; }
	public DateTime RequestDate { get; set; }
	public string QCRemarks { get; set; }
	public string CertificateOfIrradiationNumber { get; set; }
	public string MinValue { get; set; }
	public string MaxValue { get; set; }
	public string ActualValue { get; set; }
	public string Layout { get; set; }
	public string ApproverName { get; set; }
	public string ApproverRemarks { get; set; }
	public string ApproverJobTitle { get; set; }
	public DateTime? ApproverIssuedDate { get; set; }
	public string Status { get; set; }
	public DateTime CreateDate { get; set; }
	public DateTime? UpdateDate { get; set; }
	public string DosimetryFilm { get; set; } = string.Empty;
}
