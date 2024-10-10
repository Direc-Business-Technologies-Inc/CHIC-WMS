using DataManager.Models.Configurations;
using DataManager.Services.Core;
using System.Collections.Generic;
using static Application.Models.ViewModels.ConfigurationViewModel;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Application.Services.Core;

public class ConfigurationService : IConfigurationService
{
	private readonly IConfigurationDataService _configurationDataService;
	private readonly IMsSqlDataAccess _sql;
	private readonly IMapper Mapper;
	public ConfigurationService(IConfigurationDataService configurationDataService, IMapper mapper, IMsSqlDataAccess sql)
	{
		_configurationDataService = configurationDataService;
		Mapper = mapper;
		_sql = sql;
	}
	public ConfigurationViewModel InitializeConfigurations()
	{
		ConfigurationViewModel model = new ConfigurationViewModel();

		model.Configurations = Mapper.Map<List<ConfigurationViewModel.Configuration>>(_configurationDataService.GetConfigurations());

		return model;
	}

	#region QCMaintenance Config
	public ConfigurationViewModel InitializeQCMaintenanceConfig()
	{
		ConfigurationViewModel model = new ConfigurationViewModel();

		model.ConfigurationItems = Mapper.Map<List<ConfigurationItem>>(_configurationDataService.GetConfigurationItems("QC_MAINTENANCE"));

		model.QCMaintenanceItems = ConvertQCMaintenanceConfigItemstoQCMaintenanceItems(model.ConfigurationItems);

		string qry = $@"select ""UgpCode"", ""UgpName"" from OUGP";
		model.UoMList = _sql.GetData<ConfigurationViewModel.UoM, dynamic>(qry, new { }, _sql.GetConnection("SAP"), CommandType.Text);

		ConfigurationViewModel.UoM item = new ConfigurationViewModel.UoM();
		item.UgpCode = "N/A";
		item.UgpName = "N/A";

		model.UoMList.Insert(0, item);

		return model;
	}

	public bool SaveQCMaintenanceConfig(List<ConfigurationViewModel.QCMaintenanceItem> model)
	{
		try
		{
			List<ConfigurationItems> items = new List<ConfigurationItems>();

			items = ConvertQCMaintenanceConfigDataVMtoM(model);

			_configurationDataService.SaveConfigurationItems("QC_MAINTENANCE", items);

			return true;
		}
		catch (Exception)
		{

			throw;
		}
	}

	private List<ConfigurationItems> ConvertQCMaintenanceConfigDataVMtoM(List<ConfigurationViewModel.QCMaintenanceItem> model)
	{
		try
		{
			Type type = typeof(QCMaintenanceItem);
			PropertyInfo[] properties = type.GetProperties();

			List<ConfigurationItems> items = new List<ConfigurationItems>();

			//loop model with value and index tuple
			foreach (var (item, index) in model.Select((value, i) => (value, i)))
			{
				//loop for every property in class
				foreach (PropertyInfo property in properties)
				{
					object value = property.GetValue(item);

					// You can perform any operation with the value here
					items.Add(new ConfigurationItems { 
						ConfigurationCode = "QC_MAINTENANCE", 
						SubGroup = index.ToString(), 
						Sequence = index.ToString(), 
						ItemName = property.Name,
						ItemValue = value?.ToString() ?? "",
						Active = true,
						CreateDate = DateTime.Now,
						CreateUserId = "",
						UpdateUserId = ""
					});
				}
			}


			return items;
		}
		catch (Exception)
		{

			throw;
		}
	}

	private List<QCMaintenanceItem> ConvertQCMaintenanceConfigItemstoQCMaintenanceItems(List<ConfigurationItem> model)
	{
		try
		{
			Type type = typeof(QCMaintenanceItem);

			List<QCMaintenanceItem> items = new List<QCMaintenanceItem>();

			foreach(var submodel in model.DistinctBy(x => x.SubGroup))
			{
				QCMaintenanceItem item = new QCMaintenanceItem();

				foreach(var modelitem in model.Where(x => x.SubGroup == submodel.SubGroup))
				{
					PropertyInfo property = type.GetProperty(modelitem.ItemName);
					property.SetValue(item, modelitem.ItemValue);
				}

				items.Add(item);
			}

			return items;
		}
		catch (Exception)
		{

			throw;
		}
	}
	#endregion
}
