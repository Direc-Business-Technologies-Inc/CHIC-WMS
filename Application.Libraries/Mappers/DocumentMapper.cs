using Application.Libraries.Registers;
using Application.Libraries.SAP.DB.Models;
using Application.Libraries.SAP.SL;
using AutoMapper;

namespace Application.Libraries.Mappers
{
    public class DocumentMapper : Profile
    {
        public DocumentMapper()
        {
            //Map<DapperRow, Document>()
            //    .ForAllOtherMembers(o =>
            //    {
            //        if (!o.DestinationMember.Name.StartsWith("U_"))
            //            o.Ignore();
            //    });

            //Map<OIGE, Document>()
            //    .ForAllOtherMembers(o =>
            //    {
            //        if (!o.DestinationMember.Name.StartsWith("U_"))
            //            o.Ignore();
            //    });

            Map<ORDR, Document>();
        }

        private IMappingExpression<Ts, Td> Map<Ts, Td>()
        {

            IMappingExpression<Ts, Td> m = CreateMap<Ts, Td>();

            //foreach(var prop in typeof(Document).GetProperties().Select(x=>x.Name))
            //{
            //    if (!prop.StartsWith("U_")) continue;
            //    m.ForMember(prop, o => o.MapFrom(prop));
            //}

            return m
            .IgnoreAllNonUdfMembers()
            .ForMember("DocEntry",
                o => o.MapFrom("DocEntry"))
            .ForMember("DocNum",
                o => o.MapFrom("DocNum"))
            .ForMember("DocType",
                o => o.MapFrom("DocType"))
            .ForMember("HandWritten",
                o => o.MapFrom("Handwrtten"))
            .ForMember("Printed",
                o => o.MapFrom("Printed"))

            .ForMember("DocDate",
                o => o.MapFrom("DocDate"))
            .ForMember("DocDueDate",
                o => o.MapFrom("DocDueDate"))
            .ForMember("CardCode",
                o => o.MapFrom("CardCode"))
            .ForMember("CardName",
                o => o.MapFrom("CardName"))
            .ForMember("Address",
                o => o.MapFrom("Address"))
            .ForMember("NumAtCard",
                o => o.MapFrom("NumAtCard"))
            .ForMember("DocTotal",
                o => o.MapFrom("DocTotal"))
            .ForMember("AttachmentEntry",
                o => o.MapFrom("AtcEntry"))
            .ForMember("DocCurrency",
                o => o.MapFrom("DocCur"))
            .ForMember("DocRate",
                o => o.MapFrom("DocRate"))
            .ForMember("Reference1",
                o => o.MapFrom("Ref1"))
            .ForMember("Reference2",
                o => o.MapFrom("Ref2"))
            .ForMember("Comments",
                o => o.MapFrom("Comments"))
            .ForMember("JournalMemo",
                o => o.MapFrom("JrnlMemo"))
            .ForMember("PaymentGroupCode",
                o => o.MapFrom("GroupNum"))
            .ForMember("DocTime",
                o => o.MapFrom("DocTime"))
            .ForMember("SalesPersonCode",
                o => o.MapFrom("SlpCode"))
            .ForMember("TransportationCode",
                o => o.MapFrom("TrnspCode"))
            .ForMember("Confirmed",
                o => o.MapFrom("Confirmed"))
            .ForMember("ImportFileNum",
                o => o.MapFrom("ImportEnt"))
            .ForMember("SalesPersonCode",
                o => o.MapFrom("SlpCode"))
            .ForMember("SummeryType",
                o => o.MapFrom("SummryType"))
            .ForMember("ContactPersonCode",
                o => o.MapFrom("CntctCode"))
            .ForMember("ShowSCN",
                o => o.MapFrom("ShowSCN"))
            .ForMember("Series",
                o => o.MapFrom("Series"))
            .ForMember("TaxDate",
                o => o.MapFrom("TaxDate"))
            .ForMember("PartialSupply",
                o => o.MapFrom("PartSupply"))
            .ForMember("ShipToCode",
                o => o.MapFrom("ShipToCode"))
            .ForMember("Indicator",
                o => o.MapFrom("Indicator"))
            .ForMember("DiscountPercent",
                o => o.MapFrom("DiscPrcnt"))
            .ForMember("CreationDate",
                o => o.MapFrom("CreateDate"))
            .ForMember("UpdateDate",
                o => o.MapFrom("UpdateDate"))
            .ForMember("CancelDate",
                o => o.MapFrom("CancelDate"))
            .ForMember("Cancelled",
                o => o.MapFrom("CANCELED"))
            .ForMember("EDocType", o => o.Ignore())
            .ForMember("FatherType", o => o.Ignore())
            .ForMember("SummeryType", o => o.Ignore())
            //.ForMember(nameof(Document.Branch), o => o.Ignore())
            //.ForMember(nameof(Document.CancelDate), o => o.Ignore())
            //.ForMember(nameof(Document.Department), o => o.Ignore())
            ;
        }

        /*
         
                <Property Name="FederalTaxID" Type="Edm.String"/>
                <Property Name="PaymentReference" Type="Edm.String"/>
                <Property Name="FinancialPeriod" Type="Edm.Int32"/>
                <Property Name="UserSign" Type="Edm.Int32"/>
                <Property Name="TransNum" Type="Edm.Int32"/>
                <Property Name="VatSum" Type="Edm.Double"/>
                <Property Name="VatSumSys" Type="Edm.Double"/>
                <Property Name="VatSumFc" Type="Edm.Double"/>
                <Property Name="NetProcedure" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="DocTotalFc" Type="Edm.Double"/>
                <Property Name="DocTotalSys" Type="Edm.Double"/>
                <Property Name="Form1099" Type="Edm.Int32"/>
                <Property Name="Box1099" Type="Edm.String"/>
                <Property Name="RevisionPo" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="RequriedDate" Type="Edm.DateTimeOffset"/>
                <Property Name="BlockDunning" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="Submitted" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="Segment" Type="Edm.Int32"/>
                <Property Name="PickStatus" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="Pick" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="PaymentMethod" Type="Edm.String"/>
                <Property Name="PaymentBlock" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="PaymentBlockEntry" Type="Edm.Int32"/>
                <Property Name="CentralBankIndicator" Type="Edm.String"/>
                <Property Name="MaximumCashDiscount" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="Reserve" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="Project" Type="Edm.String"/>
                <Property Name="ExemptionValidityDateFrom" Type="Edm.DateTimeOffset"/>
                <Property Name="ExemptionValidityDateTo" Type="Edm.DateTimeOffset"/>
                <Property Name="WareHouseUpdateType" Type="SAPB1.BoDocWhsUpdateTypes"/>
                <Property Name="Rounding" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="ExternalCorrectedDocNum" Type="Edm.String"/>
                <Property Name="InternalCorrectedDocNum" Type="Edm.Int32"/>
                <Property Name="NextCorrectingDocument" Type="Edm.Int32"/>
                <Property Name="DeferredTax" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="TaxExemptionLetterNum" Type="Edm.String"/>
                <Property Name="WTApplied" Type="Edm.Double"/>
                <Property Name="WTAppliedFC" Type="Edm.Double"/>
                <Property Name="BillOfExchangeReserved" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="AgentCode" Type="Edm.String"/>
                <Property Name="WTAppliedSC" Type="Edm.Double"/>
                <Property Name="TotalEqualizationTax" Type="Edm.Double"/>
                <Property Name="TotalEqualizationTaxFC" Type="Edm.Double"/>
                <Property Name="TotalEqualizationTaxSC" Type="Edm.Double"/>
                <Property Name="NumberOfInstallments" Type="Edm.Int32"/>
                <Property Name="ApplyTaxOnFirstInstallment" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="WTNonSubjectAmount" Type="Edm.Double"/>
                <Property Name="WTNonSubjectAmountSC" Type="Edm.Double"/>
                <Property Name="WTNonSubjectAmountFC" Type="Edm.Double"/>
                <Property Name="WTExemptedAmount" Type="Edm.Double"/>
                <Property Name="WTExemptedAmountSC" Type="Edm.Double"/>
                <Property Name="WTExemptedAmountFC" Type="Edm.Double"/>
                <Property Name="BaseAmount" Type="Edm.Double"/>
                <Property Name="BaseAmountSC" Type="Edm.Double"/>
                <Property Name="BaseAmountFC" Type="Edm.Double"/>
                <Property Name="WTAmount" Type="Edm.Double"/>
                <Property Name="WTAmountSC" Type="Edm.Double"/>
                <Property Name="WTAmountFC" Type="Edm.Double"/>
                <Property Name="VatDate" Type="Edm.DateTimeOffset"/>
                <Property Name="DocumentsOwner" Type="Edm.Int32"/>
                <Property Name="FolioPrefixString" Type="Edm.String"/>
                <Property Name="FolioNumber" Type="Edm.Int32"/>
                <Property Name="DocumentSubType" Type="SAPB1.BoDocumentSubType"/>
                <Property Name="BPChannelCode" Type="Edm.String"/>
                <Property Name="BPChannelContact" Type="Edm.Int32"/>
                <Property Name="Address2" Type="Edm.String"/>
                <Property Name="DocumentStatus" Type="SAPB1.BoStatus"/>
                <Property Name="PeriodIndicator" Type="Edm.String"/>
                <Property Name="PayToCode" Type="Edm.String"/>
                <Property Name="ManualNumber" Type="Edm.String"/>
                <Property Name="UseShpdGoodsAct" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="IsPayToBank" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="PayToBankCountry" Type="Edm.String"/>
                <Property Name="PayToBankCode" Type="Edm.String"/>
                <Property Name="PayToBankAccountNo" Type="Edm.String"/>
                <Property Name="PayToBankBranch" Type="Edm.String"/>
                <Property Name="BPL_IDAssignedToInvoice" Type="Edm.Int32"/>
                <Property Name="DownPayment" Type="Edm.Double"/>
                <Property Name="ReserveInvoice" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="LanguageCode" Type="Edm.Int32"/>
                <Property Name="TrackingNumber" Type="Edm.String"/>
                <Property Name="PickRemark" Type="Edm.String"/>
                <Property Name="ClosingDate" Type="Edm.DateTimeOffset"/>
                <Property Name="SequenceCode" Type="Edm.Int32"/>
                <Property Name="SequenceSerial" Type="Edm.Int32"/>
                <Property Name="SeriesString" Type="Edm.String"/>
                <Property Name="SubSeriesString" Type="Edm.String"/>
                <Property Name="SequenceModel" Type="Edm.String"/>
                <Property Name="UseCorrectionVATGroup" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="TotalDiscount" Type="Edm.Double"/>
                <Property Name="DownPaymentAmount" Type="Edm.Double"/>
                <Property Name="DownPaymentPercentage" Type="Edm.Double"/>
                <Property Name="DownPaymentType" Type="SAPB1.DownPaymentTypeEnum"/>
                <Property Name="DownPaymentAmountSC" Type="Edm.Double"/>
                <Property Name="DownPaymentAmountFC" Type="Edm.Double"/>
                <Property Name="VatPercent" Type="Edm.Double"/>
                <Property Name="ServiceGrossProfitPercent" Type="Edm.Double"/>
                <Property Name="OpeningRemarks" Type="Edm.String"/>
                <Property Name="ClosingRemarks" Type="Edm.String"/>
                <Property Name="RoundingDiffAmount" Type="Edm.Double"/>
                <Property Name="RoundingDiffAmountFC" Type="Edm.Double"/>
                <Property Name="RoundingDiffAmountSC" Type="Edm.Double"/>
                <Property Name="SignatureInputMessage" Type="Edm.String"/>
                <Property Name="SignatureDigest" Type="Edm.String"/>
                <Property Name="CertificationNumber" Type="Edm.String"/>
                <Property Name="PrivateKeyVersion" Type="Edm.Int32"/>
                <Property Name="ControlAccount" Type="Edm.String"/>
                <Property Name="InsuranceOperation347" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="ArchiveNonremovableSalesQuotation" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="GTSChecker" Type="Edm.Int32"/>
                <Property Name="GTSPayee" Type="Edm.Int32"/>
                <Property Name="ExtraMonth" Type="Edm.Int32"/>
                <Property Name="ExtraDays" Type="Edm.Int32"/>
                <Property Name="CashDiscountDateOffset" Type="Edm.Int32"/>
                <Property Name="StartFrom" Type="SAPB1.BoPayTermDueTypes"/>
                <Property Name="NTSApproved" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="ETaxWebSite" Type="Edm.Int32"/>
                <Property Name="ETaxNumber" Type="Edm.String"/>
                <Property Name="NTSApprovedNumber" Type="Edm.String"/>
                <Property Name="EDocGenerationType" Type="SAPB1.EDocGenerationTypeEnum"/>
                <Property Name="EDocSeries" Type="Edm.Int32"/>
                <Property Name="EDocNum" Type="Edm.String"/>
                <Property Name="EDocExportFormat" Type="Edm.Int32"/>
                <Property Name="EDocStatus" Type="SAPB1.EDocStatusEnum"/>
                <Property Name="EDocErrorCode" Type="Edm.String"/>
                <Property Name="EDocErrorMessage" Type="Edm.String"/>
                <Property Name="DownPaymentStatus" Type="SAPB1.BoSoStatus"/>
                <Property Name="GroupSeries" Type="Edm.Int32"/>
                <Property Name="GroupNumber" Type="Edm.Int32"/>
                <Property Name="GroupHandWritten" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="ReopenOriginalDocument" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="ReopenManuallyClosedOrCanceledDocument" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="CreateOnlineQuotation" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="POSEquipmentNumber" Type="Edm.String"/>
                <Property Name="POSManufacturerSerialNumber" Type="Edm.String"/>
                <Property Name="POSCashierNumber" Type="Edm.Int32"/>
                <Property Name="ApplyCurrentVATRatesForDownPaymentsToDraw" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="ClosingOption" Type="SAPB1.ClosingOptionEnum"/>
                <Property Name="SpecifiedClosingDate" Type="Edm.DateTimeOffset"/>
                <Property Name="OpenForLandedCosts" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="AuthorizationStatus" Type="SAPB1.DocumentAuthorizationStatusEnum"/>
                <Property Name="TotalDiscountFC" Type="Edm.Double"/>
                <Property Name="TotalDiscountSC" Type="Edm.Double"/>
                <Property Name="RelevantToGTS" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="BPLName" Type="Edm.String"/>
                <Property Name="VATRegNum" Type="Edm.String"/>
                <Property Name="AnnualInvoiceDeclarationReference" Type="Edm.Int32"/>
                <Property Name="Supplier" Type="Edm.String"/>
                <Property Name="Releaser" Type="Edm.Int32"/>
                <Property Name="Receiver" Type="Edm.Int32"/>
                <Property Name="BlanketAgreementNumber" Type="Edm.Int32"/>
                <Property Name="IsAlteration" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="CancelStatus" Type="SAPB1.CancelStatusEnum"/>
                <Property Name="AssetValueDate" Type="Edm.DateTimeOffset"/>
                <Property Name="Requester" Type="Edm.String"/>
                <Property Name="RequesterName" Type="Edm.String"/>
                <Property Name="RequesterBranch" Type="Edm.Int32"/>
                <Property Name="RequesterDepartment" Type="Edm.Int32"/>
                <Property Name="RequesterEmail" Type="Edm.String"/>
                <Property Name="SendNotification" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="ReqType" Type="Edm.Int32"/>
                <Property Name="InvoicePayment" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="DocumentDelivery" Type="SAPB1.DocumentDeliveryTypeEnum"/>
                <Property Name="AuthorizationCode" Type="Edm.String"/>
                <Property Name="StartDeliveryDate" Type="Edm.DateTimeOffset"/>
                <Property Name="StartDeliveryTime" Type="Edm.TimeOfDay"/>
                <Property Name="EndDeliveryDate" Type="Edm.DateTimeOffset"/>
                <Property Name="EndDeliveryTime" Type="Edm.TimeOfDay"/>
                <Property Name="VehiclePlate" Type="Edm.String"/>
                <Property Name="ATDocumentType" Type="Edm.String"/>
                <Property Name="ElecCommStatus" Type="SAPB1.ElecCommStatusEnum"/>
                <Property Name="ElecCommMessage" Type="Edm.String"/>
                <Property Name="ReuseDocumentNum" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="ReuseNotaFiscalNum" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="PrintSEPADirect" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="FiscalDocNum" Type="Edm.String"/>
                <Property Name="POSDailySummaryNo" Type="Edm.Int32"/>
                <Property Name="POSReceiptNo" Type="Edm.Int32"/>
                <Property Name="PointOfIssueCode" Type="Edm.String"/>
                <Property Name="Letter" Type="SAPB1.FolioLetterEnum"/>
                <Property Name="FolioNumberFrom" Type="Edm.Int32"/>
                <Property Name="FolioNumberTo" Type="Edm.Int32"/>
                <Property Name="InterimType" Type="SAPB1.BoInterimDocTypes"/>
                <Property Name="RelatedType" Type="Edm.Int32"/>
                <Property Name="RelatedEntry" Type="Edm.Int32"/>
                <Property Name="SAPPassport" Type="Edm.String"/>
                <Property Name="DocumentTaxID" Type="Edm.String"/>
                <Property Name="DateOfReportingControlStatementVAT" Type="Edm.DateTimeOffset"/>
                <Property Name="ReportingSectionControlStatementVAT" Type="Edm.String"/>
                <Property Name="ExcludeFromTaxReportControlStatementVAT" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="POS_CashRegister" Type="Edm.Int32"/>
                <Property Name="UpdateTime" Type="Edm.TimeOfDay"/>
                <Property Name="CreateQRCodeFrom" Type="Edm.String"/>
                <Property Name="PriceMode" Type="SAPB1.PriceModeDocumentEnum"/>
                <Property Name="DownPaymentTrasactionID" Type="Edm.String"/>
                <Property Name="Revision" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="OriginalRefNo" Type="Edm.String"/>
                <Property Name="OriginalRefDate" Type="Edm.DateTimeOffset"/>
                <Property Name="GSTTransactionType" Type="SAPB1.GSTTransactionTypeEnum"/>
                <Property Name="OriginalCreditOrDebitNo" Type="Edm.String"/>
                <Property Name="OriginalCreditOrDebitDate" Type="Edm.DateTimeOffset"/>
                <Property Name="ECommerceOperator" Type="Edm.String"/>
                <Property Name="ECommerceGSTIN" Type="Edm.String"/>
                <Property Name="TaxInvoiceNo" Type="Edm.String"/>
                <Property Name="TaxInvoiceDate" Type="Edm.DateTimeOffset"/>
                <Property Name="ShipFrom" Type="Edm.String"/>
                <Property Name="CommissionTrade" Type="SAPB1.CommissionTradeTypeEnum"/>
                <Property Name="CommissionTradeReturn" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="UseBillToAddrToDetermineTax" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="IssuingReason" Type="Edm.Int32"/>
                <Property Name="Cig" Type="Edm.Int32"/>
                <Property Name="Cup" Type="Edm.Int32"/>
                <Property Name="EDocType" Type="SAPB1.EDocTypeEnum"/>
                <Property Name="FCEAsPaymentMeans" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="PaidToDate" Type="Edm.Double"/>
                <Property Name="PaidToDateFC" Type="Edm.Double"/>
                <Property Name="PaidToDateSys" Type="Edm.Double"/>
                <Property Name="FatherCard" Type="Edm.String"/>
                <Property Name="FatherType" Type="SAPB1.BoFatherCardTypes"/>
                <Property Name="ShipState" Type="Edm.String"/>
                <Property Name="ShipPlace" Type="Edm.String"/>
                <Property Name="CustOffice" Type="Edm.String"/>
                <Property Name="FCI" Type="Edm.String"/>
                <Property Name="AddLegIn" Type="Edm.String"/>
                <Property Name="LegTextF" Type="Edm.Int32"/>
                <Property Name="DANFELgTxt" Type="Edm.String"/>
                <Property Name="DataVersion" Type="Edm.Int32"/>
                <Property Name="LastPageFolioNumber" Type="Edm.Int32"/>
                <Property Name="U_xDOC_NO" Type="Edm.String"/>
                <Property Name="U_xBLANKETPO_TYPE" Type="Edm.String"/>
                <Property Name="U_SINo" Type="Edm.String"/>
                <Property Name="U_PRNo" Type="Edm.String"/>
                <Property Name="U_PONo" Type="Edm.String"/>
                <Property Name="U_Remarks" Type="Edm.String"/>
                <Property Name="U_TransCat" Type="Edm.String"/>
                <Property Name="U_DRNo" Type="Edm.String"/>
                <Property Name="U_SONo" Type="Edm.String"/>
                <Property Name="U_RevNo" Type="Edm.String"/>
                <Property Name="U_CWTRef" Type="Edm.String"/>
                <Property Name="U_CWTDate" Type="Edm.DateTimeOffset"/>
                <Property Name="U_Orig" Type="Edm.String"/>
                <Property Name="U_Cancel" Type="Edm.String"/>
                <Property Name="U_OrigNo" Type="Edm.String"/>
                <Property Name="U_Driver" Type="Edm.String"/>
                <Property Name="U_PlateNo" Type="Edm.String"/>
                <Property Name="U_TimeStart" Type="Edm.Int32"/>
                <Property Name="U_TimeFin" Type="Edm.Int32"/>
                <Property Name="U_PrepBy" Type="Edm.String"/>
                <Property Name="U_RevBy" Type="Edm.String"/>
                <Property Name="U_AppBy" Type="Edm.String"/>
                <Property Name="U_RecBy" Type="Edm.String"/>
                <Property Name="U_DelBy" Type="Edm.String"/>
                <Property Name="U_JobOrder" Type="Edm.String"/>
                <Property Name="U_RecDocDate" Type="Edm.DateTimeOffset"/>
                <Property Name="U_RecItem" Type="Edm.String"/>
                <Property Name="U_Help1" Type="Edm.String"/>
                <Property Name="U_Help2" Type="Edm.String"/>
                <Property Name="U_Help3" Type="Edm.String"/>
                <Property Name="U_ProdDate" Type="Edm.DateTimeOffset"/>
                <Property Name="U_lbUpdateDate" Type="Edm.DateTimeOffset"/>
                <Property Name="U_lbUpdateTime" Type="Edm.TimeOfDay"/>
                <Property Name="U_lbUpdateTS" Type="Edm.String"/>
                <Property Name="U_lbDocEntry" Type="Edm.Int32"/>
                <Property Name="U_lbLocation" Type="Edm.String"/>
                <Property Name="U_lbGRPOExRate" Type="Edm.Double"/>
                <Property Name="U_lbEstLC" Type="Edm.Double"/>
                <Property Name="U_lbEstLCFX" Type="Edm.Double"/>
                <Property Name="U_ItemDet" Type="Edm.String"/>
                <Property Name="U_lbStatus" Type="Edm.String"/>
                <Property Name="Document_ApprovalRequests" Type="Collection(SAPB1.Document_ApprovalRequest)"/>
                <Property Name="DocumentLines" Type="Collection(SAPB1.DocumentLine)"/>
                <Property Name="EWayBillDetails" Type="SAPB1.EWayBillDetails"/>
                <Property Name="ElectronicProtocols" Type="Collection(SAPB1.ElectronicProtocol)"/>
                <Property Name="DocumentAdditionalExpenses" Type="Collection(SAPB1.DocumentAdditionalExpense)"/>
                <Property Name="DocumentDistributedExpenses" Type="Collection(SAPB1.DocumentDistributedExpense)"/>
                <Property Name="WithholdingTaxDataWTXCollection" Type="Collection(SAPB1.WithholdingTaxDataWTX)"/>
                <Property Name="WithholdingTaxDataCollection" Type="Collection(SAPB1.WithholdingTaxData)"/>
                <Property Name="DocumentPackages" Type="Collection(SAPB1.DocumentPackage)"/>
                <Property Name="DocumentSpecialLines" Type="Collection(SAPB1.DocumentSpecialLine)"/>
                <Property Name="DocumentInstallments" Type="Collection(SAPB1.DocumentInstallment)"/>
                <Property Name="DownPaymentsToDraw" Type="Collection(SAPB1.DownPaymentToDraw)"/>
                <Property Name="TaxExtension" Type="SAPB1.TaxExtension"/>
                <Property Name="AddressExtension" Type="SAPB1.AddressExtension"/>
                <Property Name="DocumentReferences" Type="Collection(SAPB1.DocumentReference)"/>
                <Property Name="BaseType" Type="Edm.Int32"/>
                <Property Name="BaseEntry" Type="Edm.Int32"/>
                <Property Name="IndFinal" Type="SAPB1.BoYesNoEnum"/>
                <Property Name="SOIWizardId" Type="Edm.Int32"/>
         
         */
    }
}
