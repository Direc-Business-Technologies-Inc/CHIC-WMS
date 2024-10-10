using DataManager.Models.QCOrder;
using Microsoft.EntityFrameworkCore;

namespace DataManager.Services.Core;

public class CertificatOfIrradiationDataService : ICertificatOfIrradiationDataService
{
	readonly Context _context;
	public CertificatOfIrradiationDataService(Context context)
	{
		_context = context;

	}
	public List<CertificateOfIrradiation> Get()
	{
		try
		{
			//return _context.CertificateOfIrradiations.ToList();
			return _context.CertificateOfIrradiations
				.GroupBy(a => a.DocNo)
				.Select(group => group.OrderByDescending(b => b.CertificateOfIrradiationNumber).First()).ToList();
		}
		catch (Exception)
		{
			throw;
		}
	}

	public CertificateOfIrradiation Get(string QCOrderNo)
	{
		try
		{
			return _context.CertificateOfIrradiations.Where(x => x.QCOrderNo == QCOrderNo)
				.OrderByDescending(x => x.CertificateOfIrradiationNumber)
				.FirstOrDefault();
		}
		catch (Exception)
		{

			throw;
		}
	}
	public CertificateOfIrradiation Post(CertificateOfIrradiation model)
	{
		try
		{
			string Method = "";

			//Check if QC Order No is existing in table
			var cert = _context.CertificateOfIrradiations.Where(x => x.QCOrderNo == model.QCOrderNo);

			Method = cert.Any() ? "Update" : "Add";

			model.CertificateOfIrradiationNumber = GetSequence(Method, model.DocNo.ToString());
			model.CreateDate = DateTime.Now;
			model.RequestDate = DateTime.Now;
			_context.CertificateOfIrradiations.Add(model);
			_context.SaveChanges();
			return model;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public CertificateOfIrradiation Patch(CertificateOfIrradiation model)
	{
		try
		{
			var cert = _context.CertificateOfIrradiations.Where(x => x.CertificateOfIrradiationNumber == model.CertificateOfIrradiationNumber).FirstOrDefault();

			cert.ApproverRemarks = model.ApproverRemarks;
			cert.Status = model.Status;

			_context.Entry(cert).State = EntityState.Modified;
			_context.SaveChanges();

			return model;
		}
		catch (Exception)
		{

			throw;
		}
	}

	public string GetSequence(string Method, string SONo)
	{
		string model = "";
		string Sequence = "";
		int currentYear = DateTime.Now.Year % 100;

		switch (Method)
		{
			case "Add":

				model = _context.CertificateOfIrradiations.Where(x => (x.CreateDate.Year % 100) == currentYear).OrderByDescending(x => x.CertificateOfIrradiationNumber).FirstOrDefault()?.CertificateOfIrradiationNumber ?? $"{currentYear}-0";
				string secondstring = (Convert.ToInt32(model.Split("-")[1]) + 1).ToString("D5");
				Sequence = $"{currentYear}-{secondstring}-A";

				break;
			case "Update":

				model = _context.CertificateOfIrradiations.Where(x => x.DocNo.ToString() == SONo).OrderByDescending(x => x.CertificateOfIrradiationNumber).FirstOrDefault()?.CertificateOfIrradiationNumber ?? $"{currentYear}-0-@";
				char lastchar = (char)(model.Split("-")[2].ToCharArray()[0] + 1);
				Sequence = $"{currentYear}-00001-{lastchar}";

				break;
			default:
				break;
		}

		return Sequence;
	}
}
