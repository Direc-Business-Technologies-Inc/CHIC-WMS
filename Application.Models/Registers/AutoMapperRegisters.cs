using DataManager.Models.InventoryTransfer;
using DataManager.Models.QCOrder;
using DataManager.Models.Receiving;
using static Application.Models.ViewModels.ScheduleViewModel;
using vm = Application.Models.ViewModels;
using models = DataManager.Models;
using appModels = Application.Models;
using Application.Models.Models;
using Application.Models.DTOs;
using DataManager.Models.DashboardNotification;
using static Application.Models.ViewModels.QCOrderViewModel;
using DataManager.Models.CertificateOfIrradiation;
using static Application.Models.ViewModels.CertificateOfIrradiationViewModel;
using DataManager.Models.Users;
using DataManager.Models.Configurations;
using static Application.Models.ViewModels.UserViewModel;

namespace Application.Models.Registers;
public class AutoMapperRegisters : Profile
{
	public AutoMapperRegisters()
	{
		CreateMap<BinMappingHeaders, BinMapping>()
			.ForMember(x => x.CreateDate, a => a.MapFrom(z => (z.CreateDate == null) ? DateTime.Now : DateTime.Now))
			.ForMember(x => x.ImageName, a => a.MapFrom(z => z.FileName))
			.ForMember(dest => dest.BinMappingPins, opt => opt.MapFrom(src => src.BinMappingPins));

		CreateMap<BinMapping, BinMappingHeaders>()
			.ForMember(x => x.FileName, a => a.MapFrom(z => z.ImageName))
			.ForMember(dest => dest.BinMappingPins, opt => opt.MapFrom(src => src.BinMappingPins));

		CreateMap<BinMappingPins, BinMappingPin>();
		CreateMap<BinMappingPin, BinMappingPins>();

		CreateMap<QCMaintenanceViewModel.QCMaintenance, models.QCMaintenance.InspectionPlan>()
			.ForMember(x => x.CreateDate, a => a.MapFrom(z => (z.CreateDate == null) ? DateTime.Now : DateTime.Now))
			.ForMember(dest => dest.ParameterList, opt => opt.MapFrom(src => src.ParameterList));

		CreateMap<models.QCMaintenance.InspectionPlan, InspectionPlans>();

		CreateMap<models.QCMaintenance.InspectionPlan, QCMaintenance>()
			.ForMember(dest => dest.ParameterList, opt => opt.MapFrom(src => src.ParameterList));

		CreateMap<InspectionPlanParameter, Parameters>();

		CreateMap<QCMaintenanceViewModel.Parameters, InspectionPlanParameter>();
		CreateMap<BinMappingPin, BinLabel>();

		CreateMap<models.QCMaintenance.InspectionPlan, QCOrderViewModel.InspectionPlan>();

		#region Receiving

		CreateMap<ReceivingViewModel, ReceivingModel>()
			.ForMember(d => d.SalesOrderId, o => o.MapFrom(s => s.selectedSo.DocEntry))
			.ForMember(d => d.SalesOrderNum, o => o.MapFrom(s => s.selectedSo.DocNum))
			.ForMember(d => d.PlannedBoxNo, o => o.MapFrom(s => s.selectedSo.PlannedBoxNo))
			.ForMember(d => d.ActualBoxNo, o => o.MapFrom(s => s.selectedSo.Pallets.Sum(x => x.Quantity)))
			.ForMember(d => d.Pallets, o => o.MapFrom(s => s.selectedSo.Pallets))
			.ReverseMap();

		CreateMap<ReceivingViewModel.Pallet, DataManager.Models.Receiving.Pallet>()
			.ReverseMap();

		#endregion

		CreateMap<InspectionPlanParameter, QCOrderViewModel.ParameterDetail>();

		CreateMap<QCOrderViewModel.QCOrderDetail, QCOrder>()
			.ForMember(dest => dest.OverallPassTolerancePercentage, opt => opt.MapFrom(src => Convert.ToSingle(src.OverallPassTolerancePercentage == "" ? "0" : src.OverallPassTolerancePercentage)))
			.ForMember(dest => dest.SamplePassTolerancePercentage, opt => opt.MapFrom(src => Convert.ToSingle(src.SamplePassTolerancePercentage == "" ? "0" : src.SamplePassTolerancePercentage)))
			.ForMember(dest => dest.QCOrderSampleDetail, opt => opt.MapFrom(src => src.SampleDetails))
			.ForMember(dest => dest.QCOrderDosimetryReport, opt => opt.MapFrom(src => src.DosimetryReport));

		CreateMap<QCOrderViewModel.Sample, QCOrderSampleDetail>()
			.ForMember(dest => dest.QCOrderSampleList, opt => opt.MapFrom(src => src.SampleDetailList));

		CreateMap<QCOrderViewModel.SampleDetail, QCOrderSampleList>()
			.ForMember(dest => dest.QCOrderParameterList, opt => opt.MapFrom(src => src.ParameterDetailList));

		CreateMap<QCOrderViewModel.ParameterDetail, QCOrderParameterList>();

		CreateMap<QCOrder, QCOrderViewModel.QCOrders>();
		CreateMap<QCOrder, QCOrderViewModel.QCOrderDetail>()
			.ForMember(dest => dest.SampleDetails, opt => opt.MapFrom(src => src.QCOrderSampleDetail))
			.ForMember(dest => dest.DosimetryReport, opt => opt.MapFrom(src => src.QCOrderDosimetryReport))
			.ForMember(dest => dest.DocDate, opt => opt.MapFrom(src => src.DocDate.ToString("MM-dd-yyyy")))
			.ForMember(dest => dest.OverallPassTolerancePercentage, opt => opt.MapFrom(src => src.OverallPassTolerancePercentage.ToString()))
			.ForMember(dest => dest.SamplePassTolerancePercentage, opt => opt.MapFrom(src => src.SamplePassTolerancePercentage.ToString()));

		CreateMap<QCOrderSampleDetail, QCOrderViewModel.Sample>()
			.ForMember(dest => dest.SampleDetailList, opt => opt.MapFrom(src => src.QCOrderSampleList));

		CreateMap<QCOrderSampleList, QCOrderViewModel.SampleDetail>()
			.ForMember(dest => dest.ParameterDetailList, opt => opt.MapFrom(src => src.QCOrderParameterList));

		CreateMap<QCOrderParameterList, QCOrderViewModel.ParameterDetail>();

		CreateMap<QCOrderDosimetryReport, QCOrderViewModel.Dosimetry>();
		CreateMap<QCOrderViewModel.Dosimetry, QCOrderDosimetryReport>();

		CreateMap<BinMapping, DashboardViewModel.BinDashboardViewModel.BinMapping>()
		.ForMember(x => x.FileName, a => a.MapFrom(z => z.ImageName))
		.ForMember(dest => dest.BinMappingPins, opt => opt.MapFrom(src => src.BinMappingPins));

		CreateMap<BinMappingPin, DashboardViewModel.BinDashboardViewModel.BinMappingPin>();

		CreateMap<FacilityLocationViewModel, FacilityLocation>();
		CreateMap<FacilityLocation, FacilityLocationViewModel>();

		CreateMap<InventoryTransfer, models.InventoryTransfer.InventoryTransferModel>();
		CreateMap<vm.InventoryTransferLine, models.InventoryTransfer.InventoryTransferLine>();
		CreateMap<vm.BatchSerialViewModel, models.InventoryTransfer.InventoryTransferBatch>();
		//Application.Models.ViewModels.InventoryTransfer -> DataManager.Models.InventoryTransfer.InventoryTransferModel

		#region Certificate of Irradiation
		CreateMap<CertificateOfIrradiationViewModel.QCOrderDetail, CertificateOfIrradiation>()
			.ForMember(dest => dest.QCOrderNo, opt => opt.MapFrom(src => src.QCOrderNo))
			.ForMember(dest => dest.DocNo, opt => opt.MapFrom(src => src.DocNo))
			.ForMember(dest => dest.CustomerCode, opt => opt.MapFrom(src => src.CustomerCode))
			.ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
			.ForMember(dest => dest.ItemCode, opt => opt.MapFrom(src => src.ItemCode))
			.ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.ItemName))
			.ForMember(dest => dest.ManufacturingLotNo, opt => opt.MapFrom(src => src.ManufacturingLotNo))
			.ForMember(dest => dest.CustomerPONo, opt => opt.MapFrom(src => src.PONo))
			.ForMember(dest => dest.QCRemarks, opt => opt.MapFrom(src => src.Remarks))
			.ForMember(dest => dest.QCOrder, opt => opt.Ignore())
			.ForMember(dest => dest.Id, opt => opt.Ignore())
			.ForMember(dest => dest.Status, opt => opt.Ignore())
			.ForMember(dest => dest.CreateDate, opt => opt.Ignore())
			.ForMember(dest => dest.UpdateDate, opt => opt.Ignore())
			.ForMember(dest => dest.IrradiationDate, opt => opt.Ignore())
			.ForMember(dest => dest.TotalNoOfBoxes, opt => opt.Ignore())
			.ForMember(dest => dest.FacilityName, opt => opt.Ignore())
			.ForMember(dest => dest.QCRequester, opt => opt.Ignore())
			.ForMember(dest => dest.RequestDate, opt => opt.Ignore())
			.ForMember(dest => dest.CertificateOfIrradiationNumber, opt => opt.Ignore())
			.ForMember(dest => dest.MinValue, opt => opt.Ignore())
			.ForMember(dest => dest.MaxValue, opt => opt.Ignore())
			.ForMember(dest => dest.ActualValue, opt => opt.Ignore())
			.ForMember(dest => dest.Layout, opt => opt.Ignore())
			.ForMember(dest => dest.ApproverName, opt => opt.Ignore())
			.ForMember(dest => dest.ApproverRemarks, opt => opt.Ignore())
			.ForMember(dest => dest.ApproverJobTitle, opt => opt.Ignore())
			.ForMember(dest => dest.ApproverIssuedDate, opt => opt.Ignore())
			.ReverseMap();

		CreateMap<QCOrder, CertificateOfIrradiationViewModel.QCOrderDetail>()
			.ForMember(dest => dest.SampleDetails, opt => opt.MapFrom(src => src.QCOrderSampleDetail))
			.ForMember(dest => dest.DosimetryReport, opt => opt.MapFrom(src => src.QCOrderDosimetryReport))
			.ForMember(dest => dest.DocDate, opt => opt.MapFrom(src => src.DocDate.ToString("MM-dd-yyyy")))
			.ForMember(dest => dest.OverallPassTolerancePercentage, opt => opt.MapFrom(src => src.OverallPassTolerancePercentage.ToString()))
			.ForMember(dest => dest.SamplePassTolerancePercentage, opt => opt.MapFrom(src => src.SamplePassTolerancePercentage.ToString()))
			.ReverseMap();

		CreateMap<QCOrderSampleDetail, CertificateOfIrradiationViewModel.Sample>()
			.ForMember(dest => dest.SampleDetailList, opt => opt.MapFrom(src => src.QCOrderSampleList))
			.ForMember(dest => dest.Open, opt => opt.MapFrom(src => src.Open))
			.ForMember(dest => dest.TotalNoOfPassed, opt => opt.MapFrom(src => src.TotalNoOfPassed))
			.ForMember(dest => dest.TotalNoOfFailed, opt => opt.MapFrom(src => src.TotalNoOfFailed))
			.ForMember(dest => dest.TotalNoSamples, opt => opt.MapFrom(src => src.TotalNoSamples))
			.ReverseMap();

		CreateMap<QCOrderSampleList, CertificateOfIrradiationViewModel.SampleDetail>()
			.ForMember(dest => dest.ParameterDetailList, opt => opt.MapFrom(src => src.QCOrderParameterList))
			.ReverseMap();

		CreateMap<QCOrderParameterList, CertificateOfIrradiationViewModel.ParameterDetail>()
			.ReverseMap();

		CreateMap<QCOrderDosimetryReport, CertificateOfIrradiationViewModel.Dosimetry>()
			.ReverseMap();

		CreateMap<InspectionPlanParameter, CertificateOfIrradiationViewModel.ParameterDetail>()
			.ReverseMap();

		CreateMap<models.QCMaintenance.InspectionPlan, CertificateOfIrradiationViewModel.InspectionPlan>()
			.ReverseMap();

		CreateMap<COISalesOrderDetails, CertificateOfIrradiation>()
			.ReverseMap();
		#endregion

		#region Dispatch

		CreateMap<DispatchViewModel, DispatchModel>()
			 .ForMember(d => d.SalesOrderId, o => o.MapFrom(s => s.selectedSo.DocEntry))
			.ForMember(d => d.DocNum, o => o.MapFrom(s => s.selectedSo.DocNum))
			.ForMember(d => d.PlannedBoxNo, o => o.MapFrom(s => s.selectedSo.PlannedBoxNo))
			.ForMember(d => d.ActualBoxNo, o => o.MapFrom(s => s.selectedSo.Pallets.Sum(x => x.Quantity)))
			.ForMember(d => d.Pallets, o => o.MapFrom(s => s.selectedSo.Pallets))
			.ForMember(d=>d.Remarks, o=>o.MapFrom(s=>s.selectedSo.Remarks))
			.ReverseMap();


		CreateMap<DispatchViewModel.Pallet, Pallet>()
			.ForMember(d => d.Code, o => o.MapFrom(s => s.Label))
			.ForMember(d => d.ActualQuantity, o => o.MapFrom(s => s.Quantity))
			.ReverseMap();

        CreateMap<CreateDispatchDTO, DispatchModel>()
            .ReverseMap();

        CreateMap<CreateDispatchDTO.Pallet, appModels.Pallet>()
            .ReverseMap();

        CreateMap<CreateDispatchDTO.Box, appModels.Box>()
            .ReverseMap();


        #endregion

        #region Dashboard Event
        CreateMap<DashboardNotificationViewModel, DashboardNotificationModel>();
        CreateMap<DashboardNotificationModel, DashboardNotificationViewModel>();
        CreateMap<
            DashboardNotificationModel.DashboardNotificationLineModel, 
            DashboardNotificationViewModel.DashboardNotificationLineViewModel
            >();
        CreateMap<
            DashboardNotificationViewModel.DashboardNotificationLineViewModel,
            DashboardNotificationModel.DashboardNotificationLineModel
            >();
        CreateMap<
            DashboardNotificationViewModel,
            DashboardNotificationViewModel
            >();
        CreateMap<
            DashboardNotificationViewModel.DashboardNotificationLineViewModel,
            DashboardNotificationViewModel.DashboardNotificationLineViewModel
            >();
		#endregion

		#region Dashboard
		CreateMap<vm.UserViewModel.UserGroupsViewModel, UserModules>()
			.ReverseMap();
		#endregion

		#region Configurations
		CreateMap<vm.ConfigurationViewModel.Configuration, Configurations>()
			.ReverseMap();

		CreateMap<ConfigurationItems, vm.ConfigurationViewModel.ConfigurationItem>()
			.ReverseMap();
		#endregion

		CreateMap<vm.UserViewModel.ModuleViewModel, Modules>();
		CreateMap<Modules, vm.UserViewModel.ModuleViewModel>();
	}
}
