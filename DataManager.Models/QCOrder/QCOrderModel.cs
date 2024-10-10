namespace DataManager.Models.QCOrder;

[Table("QCOR")]
public class QCOrder
{
	public string QCOrderNo { get; set; }
	public string InspectionPlanCode { get; set; }
	public string InspectionPlanName { get; set; }
	public string InspectionPlanType { get; set; }
	public string CustomerCode { get; set; }
	public string CustomerName { get; set; }
	public string ItemCode { get; set; }
	public string ItemName { get; set; }
	public string Remarks { get; set; }
	public int DocNo { get; set; }
	public DateTime DocDate { get; set; }
	public string DocType { get; set; }
	public int Quantity { get; set; }
	public string UoM { get; set; }
	public string RefNo { get; set; }
	public string Status { get; set; }
	public int SampleSize { get; set; }
	public float SamplePassTolerancePercentage { get; set; }
	public float OverallPassTolerancePercentage { get; set; }
	public string PONo { get; set; }
	public string ManufacturingLotNo { get; set; }
	public string ServiceOrderNo { get; set; }
	public string StorageConditions { get; set; }
	public DateTime CreateDate { get; set; }
	public DateTime? UpdateDate { get; set; }
	public string DosimetryUsed { get; set; }

	public QCOrderSampleDetail QCOrderSampleDetail { get; set; } = new QCOrderSampleDetail();
	public QCOrderDosimetryReport QCOrderDosimetryReport { get; set; } = new QCOrderDosimetryReport();

	//public CertificateOfIrradiation.CertificateOfIrradiation CertificateOfIrradiation { get; set; } = new CertificateOfIrradiation.CertificateOfIrradiation();
}
