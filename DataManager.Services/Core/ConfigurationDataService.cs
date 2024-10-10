using DataManager.Models.QCOrder;
using DataManager.Services.Data;
using System.Linq;

namespace DataManager.Services.Core;

public class ConfigurationDataService : IConfigurationDataService
{
	readonly Context _context;

	public ConfigurationDataService(Context context)
	{
		_context = context;

	}

	public List<Configurations> GetConfigurations()
	{
		return _context.Configurations.OrderBy(x => x.Sequence).ToList();
	}

	public Configurations GetConfiguration(string Code)
	{
		return _context.Configurations.Where(x => x.Code == Code)?.FirstOrDefault() ?? new Configurations();
	}

	public List<ConfigurationItems> SaveConfigurationItems(string Code, List<ConfigurationItems> Items)
	{
		using (var dbTran = _context.Database.BeginTransaction())
		{
			try
			{
				var config = _context.Configurations.Where(x => x.Code == Code)
					.Include(x => x.ConfigurationItems)
					.FirstOrDefault();

				config.ConfigurationItems = Items;

				_context.Entry(config).State = EntityState.Modified;

				_context.SaveChanges();

				dbTran.Commit();

				return Items;
			}
			catch (Exception)
			{
				dbTran.Rollback();
				throw;
			}
		}

	}

	public List<ConfigurationItems> GetConfigurationItems(string Code)
	{
			try
			{
				return _context.ConfigurationItems.Where(x => x.ConfigurationCode == Code).ToList();
			}
			catch (Exception)
			{
				throw;
			}

	}
}
