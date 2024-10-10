using System;
using System.Collections.Generic;
using Application.Libraries.SAP.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Libraries.SAP;

public partial class SapDb : DbContext
{
    public SapDb(DbContextOptions<SapDb> options)
        : base(options)
    {
    }

    public virtual DbSet<@FACILITYLOCATION> @FACILITYLOCATION { get; set; }

    public virtual DbSet<@SERVICE_DATA> @SERVICE_DATA { get; set; }

    public virtual DbSet<@SERVICE_DATA_ROW> @SERVICE_DATA_ROW { get; set; }

    public virtual DbSet<@SERVICE_TYPE> @SERVICE_TYPE { get; set; }

    public virtual DbSet<ITL1> ITL1 { get; set; }

    public virtual DbSet<OBBQ> OBBQ { get; set; }

    public virtual DbSet<OBIN> OBIN { get; set; }

    public virtual DbSet<OBTN> OBTN { get; set; }

    public virtual DbSet<OBTQ> OBTQ { get; set; }

    public virtual DbSet<OITL> OITL { get; set; }

    public virtual DbSet<OITM> OITM { get; set; }

    public virtual DbSet<OITW> OITW { get; set; }

    public virtual DbSet<ORDR> ORDR { get; set; }

    public virtual DbSet<OWHS> OWHS { get; set; }

    public virtual DbSet<OWTR> OWTR { get; set; }

    public virtual DbSet<RDR1> RDR1 { get; set; }

    public virtual DbSet<WTR1> WTR1 { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP850_CI_AS");

        modelBuilder.Entity<@FACILITYLOCATION>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("KFACILITYLOCATION_PR");

            entity.HasIndex(e => e.Name, "KFACILITYLOCATION_NAME").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.U_Duration)
                .HasDefaultValueSql("((6.000000))")
                .HasColumnType("numeric(19, 6)");
        });

        modelBuilder.Entity<@SERVICE_DATA>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("KSERVICE_DATA_PR");

            entity.HasIndex(e => e.DocEntry, "KSERVICE_DATA_IK").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Canceled)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Object).HasMaxLength(20);
            entity.Property(e => e.Transfered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<@SERVICE_DATA_ROW>(entity =>
        {
            entity.HasKey(e => new { e.Code, e.LineId }).HasName("KSERVICE_DATA_ROW_PR");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Object).HasMaxLength(20);
            entity.Property(e => e.U_DisplayStatus).HasMaxLength(50);
            entity.Property(e => e.U_FieldBatchDate).HasMaxLength(254);
            entity.Property(e => e.U_FieldBatchTime).HasMaxLength(254);
            entity.Property(e => e.U_FromWarehouseCode).HasMaxLength(8);
            entity.Property(e => e.U_TransferType).HasMaxLength(50);
            entity.Property(e => e.U_WarehouseCode).HasMaxLength(8);
        });

        modelBuilder.Entity<@SERVICE_TYPE>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("KSERVICE_TYPE_PR");

            entity.HasIndex(e => e.Name, "KSERVICE_TYPE_NAME").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ITL1>(entity =>
        {
            entity.HasKey(e => new { e.LogEntry, e.ItemCode, e.SysNumber }).HasName("ITL1_PRIMARY");

            entity.HasIndex(e => new { e.ItemCode, e.SysNumber }, "ITL1_SNB_SYSID");

            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.AllocQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OrderedQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PickedQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Quantity).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ReleaseQty).HasColumnType("numeric(19, 6)");
        });

        modelBuilder.Entity<OBBQ>(entity =>
        {
            entity.HasKey(e => e.AbsEntry).HasName("OBBQ_PRIMARY");

            entity.HasIndex(e => e.BinAbs, "OBBQ_BIN_ABS");

            entity.HasIndex(e => new { e.SnBMDAbs, e.BinAbs }, "OBBQ_BUSINESS_K").IsUnique();

            entity.Property(e => e.AbsEntry).ValueGeneratedNever();
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.OnHandQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WhsCode).HasMaxLength(8);
        });

        modelBuilder.Entity<OBIN>(entity =>
        {
            entity.HasKey(e => e.AbsEntry).HasName("OBIN_PRIMARY");

            entity.HasIndex(e => e.BinCode, "OBIN_BIN_CODE").IsUnique();

            entity.HasIndex(e => new { e.WhsCode, e.SL1Code, e.SL2Code, e.SL3Code, e.SL4Code }, "OBIN_BUSINESS_K");

            entity.HasIndex(e => new { e.WhsCode, e.SysBin }, "OBIN_WHS_CODE");

            entity.Property(e => e.AbsEntry).ValueGeneratedNever();
            entity.Property(e => e.AltSortCod).HasMaxLength(50);
            entity.Property(e => e.Attr10Val).HasMaxLength(20);
            entity.Property(e => e.Attr1Val).HasMaxLength(20);
            entity.Property(e => e.Attr2Val).HasMaxLength(20);
            entity.Property(e => e.Attr3Val).HasMaxLength(20);
            entity.Property(e => e.Attr4Val).HasMaxLength(20);
            entity.Property(e => e.Attr5Val).HasMaxLength(20);
            entity.Property(e => e.Attr6Val).HasMaxLength(20);
            entity.Property(e => e.Attr7Val).HasMaxLength(20);
            entity.Property(e => e.Attr8Val).HasMaxLength(20);
            entity.Property(e => e.Attr9Val).HasMaxLength(20);
            entity.Property(e => e.BarCode).HasMaxLength(100);
            entity.Property(e => e.BinCode).HasMaxLength(228);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Deleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Descr).HasMaxLength(50);
            entity.Property(e => e.Disabled)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Instance).HasDefaultValueSql("((0))");
            entity.Property(e => e.ItmRtrictT).HasDefaultValueSql("((0))");
            entity.Property(e => e.LogInstanc).HasDefaultValueSql("((0))");
            entity.Property(e => e.MaxLevel).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MaxWeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MaxWeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MinLevel).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NoAutoAllc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ReceiveBin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.RtrictDate).HasColumnType("datetime");
            entity.Property(e => e.RtrictResn).HasMaxLength(254);
            entity.Property(e => e.RtrictType).HasDefaultValueSql("((0))");
            entity.Property(e => e.SL1Code).HasMaxLength(50);
            entity.Property(e => e.SL2Code).HasMaxLength(50);
            entity.Property(e => e.SL3Code).HasMaxLength(50);
            entity.Property(e => e.SL4Code).HasMaxLength(50);
            entity.Property(e => e.SngBatch)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SpcItmCode).HasMaxLength(50);
            entity.Property(e => e.SysBin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Transfered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.UoMRtrict).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.WhsCode).HasMaxLength(8);
        });

        modelBuilder.Entity<OBTN>(entity =>
        {
            entity.HasKey(e => e.AbsEntry).HasName("OBTN_PRIMARY");

            entity.HasIndex(e => new { e.ItemCode, e.DistNumber }, "OBTN_DIST_KEY");

            entity.HasIndex(e => new { e.ItemCode, e.SysNumber }, "OBTN_SYSTEM_KEY").IsUnique();

            entity.Property(e => e.AbsEntry).ValueGeneratedNever();
            entity.Property(e => e.Balance).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CostTotal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DistNumber).HasMaxLength(36);
            entity.Property(e => e.ExpDate).HasColumnType("datetime");
            entity.Property(e => e.GrntExp).HasColumnType("datetime");
            entity.Property(e => e.GrntStart).HasColumnType("datetime");
            entity.Property(e => e.InDate).HasColumnType("datetime");
            entity.Property(e => e.Instance).HasDefaultValueSql("((0))");
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.Location).HasMaxLength(100);
            entity.Property(e => e.LotNumber).HasMaxLength(36);
            entity.Property(e => e.MnfDate).HasColumnType("datetime");
            entity.Property(e => e.MnfSerial).HasMaxLength(36);
            entity.Property(e => e.Notes).HasColumnType("ntext");
            entity.Property(e => e.ObjType).HasMaxLength(20);
            entity.Property(e => e.PriceDiff).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.QuantOut).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Quantity).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('0')")
                .IsFixedLength();
            entity.Property(e => e.Transfered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.U_BatchStatus).HasMaxLength(50);
            entity.Property(e => e.U_LocShelf).HasMaxLength(8);
            entity.Property(e => e.U_TIMS_DispatchDate).HasColumnType("datetime");
            entity.Property(e => e.U_TIMS_ITBinDispDate).HasColumnType("datetime");
            entity.Property(e => e.U_TIMS_ITBinILB).HasColumnType("datetime");
            entity.Property(e => e.U_TIMS_ITRecBin).HasColumnType("datetime");
            entity.Property(e => e.U_TIMS_IUBBin).HasColumnType("datetime");
            entity.Property(e => e.U_TIMS_IrradLoading).HasColumnType("datetime");
            entity.Property(e => e.U_TIMS_IrradUnloading).HasColumnType("datetime");
            entity.Property(e => e.U_TIMS_OngoingStatus).HasMaxLength(50);
            entity.Property(e => e.U_TIMS_RecDateTime).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.itemName).HasMaxLength(200);
        });

        modelBuilder.Entity<OBTQ>(entity =>
        {
            entity.HasKey(e => e.AbsEntry).HasName("OBTQ_PRIMARY");

            entity.HasIndex(e => new { e.ItemCode, e.SysNumber, e.WhsCode }, "OBTQ_SYSTEM_KEY").IsUnique();

            entity.Property(e => e.AbsEntry).ValueGeneratedNever();
            entity.Property(e => e.CCDQuant).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CommitQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CountQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.Quantity).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WhsCode).HasMaxLength(8);
        });

        modelBuilder.Entity<OITL>(entity =>
        {
            entity.HasKey(e => e.LogEntry).HasName("OITL_PRIMARY");

            entity.HasIndex(e => new { e.ManagedBy, e.ActBaseTp, e.ActBaseEnt, e.ActBaseLn, e.ActBasSubL }, "OITL_ACTUAL_INF");

            entity.HasIndex(e => new { e.AllocateTp, e.AllocatEnt, e.AllocateLn }, "OITL_ALLOC_REF");

            entity.HasIndex(e => new { e.ApplyType, e.ApplyEntry, e.ApplyLine, e.AppSubLine }, "OITL_APPLYREF");

            entity.HasIndex(e => new { e.BaseType, e.BaseEntry, e.BaseLine, e.BSubLineNo }, "OITL_BASEREF");

            entity.HasIndex(e => new { e.ManagedBy, e.DocType, e.DocEntry, e.DocLine, e.SubLineNum }, "OITL_DOC_INFO");

            entity.HasIndex(e => new { e.DocType, e.DocEntry, e.DocLine, e.LogEntry }, "OITL_DOC_REF");

            entity.HasIndex(e => new { e.ItemCode, e.LocCode, e.ManagedBy, e.Instance, e.ApplyEntry, e.ApplyType, e.LogEntry }, "OITL_ITEM_CODE");

            entity.HasIndex(e => e.TransId, "OITL_TRANSID");

            entity.Property(e => e.LogEntry).ValueGeneratedNever();
            entity.Property(e => e.ActBasSubL).HasDefaultValueSql("((-1))");
            entity.Property(e => e.ActBaseLn).HasDefaultValueSql("((-1))");
            entity.Property(e => e.ActBaseTp).HasDefaultValueSql("((-1))");
            entity.Property(e => e.AllocatEnt).HasDefaultValueSql("((-1))");
            entity.Property(e => e.AllocateLn).HasDefaultValueSql("((-1))");
            entity.Property(e => e.AllocateTp).HasDefaultValueSql("((-1))");
            entity.Property(e => e.AppSubLine).HasDefaultValueSql("((-1))");
            entity.Property(e => e.ApplyType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.BSubLineNo).HasDefaultValueSql("((-1))");
            entity.Property(e => e.BaseEntry).HasDefaultValueSql("((-1))");
            entity.Property(e => e.BaseLine).HasDefaultValueSql("((-1))");
            entity.Property(e => e.BaseType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.CardCode).HasMaxLength(15);
            entity.Property(e => e.CardName).HasMaxLength(100);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CreateTime).HasDefaultValueSql("((0))");
            entity.Property(e => e.DefinedQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocDate).HasColumnType("datetime");
            entity.Property(e => e.DocQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.Instance).HasDefaultValueSql("((0))");
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ItemName).HasMaxLength(200);
            entity.Property(e => e.LocCode).HasMaxLength(8);
            entity.Property(e => e.ManagedBy).HasDefaultValueSql("((4))");
            entity.Property(e => e.StockEff).HasDefaultValueSql("((0))");
            entity.Property(e => e.StockQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SubLineNum).HasDefaultValueSql("((-1))");
            entity.Property(e => e.Transfered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.VersionNum).HasMaxLength(13);
        });

        modelBuilder.Entity<OITM>(entity =>
        {
            entity.HasKey(e => e.ItemCode).HasName("OITM_PRIMARY");

            entity.HasIndex(e => e.CommisGrp, "OITM_COM_GROUP");

            entity.HasIndex(e => e.InvntItem, "OITM_INVENTORY");

            entity.HasIndex(e => e.ItemName, "OITM_ITEM_NAME");

            entity.HasIndex(e => e.PrchseItem, "OITM_PURCHASE");

            entity.HasIndex(e => e.SellItem, "OITM_SALE");

            entity.HasIndex(e => e.TreeType, "OITM_TREE_TYPE");

            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.AcqDate).HasColumnType("datetime");
            entity.Property(e => e.AssVal4WTR).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AssblValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AssetAmnt1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AssetAmnt2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AssetClass).HasMaxLength(20);
            entity.Property(e => e.AssetGroup).HasMaxLength(15);
            entity.Property(e => e.AssetItem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.AssetRmk1).HasMaxLength(100);
            entity.Property(e => e.AssetRmk2).HasMaxLength(100);
            entity.Property(e => e.AssetSerNo).HasMaxLength(32);
            entity.Property(e => e.AsstStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Attachment).HasColumnType("ntext");
            entity.Property(e => e.AutoBatch)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.AvgPrice).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BHeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BHeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BLength1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BVolume).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BWeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BWeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BWidth1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BWidth2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseUnit).HasMaxLength(20);
            entity.Property(e => e.BeverGrpC).HasMaxLength(2);
            entity.Property(e => e.BeverTM).HasDefaultValueSql("((-1))");
            entity.Property(e => e.BeverTblC).HasMaxLength(2);
            entity.Property(e => e.Blength2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BlncTrnsfr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BlockOut)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.BuyUnitMsr).HasMaxLength(100);
            entity.Property(e => e.ByWh)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CESTCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.Canceled)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.CapDate).HasColumnType("datetime");
            entity.Property(e => e.CardCode).HasMaxLength(15);
            entity.Property(e => e.Cession)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ChapterID).HasDefaultValueSql("((-1))");
            entity.Property(e => e.CntUnitMsr).HasMaxLength(100);
            entity.Property(e => e.CodeBars).HasMaxLength(254);
            entity.Property(e => e.CommisGrp).HasDefaultValueSql("((0))");
            entity.Property(e => e.CommisPcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CommisSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CompoWH)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('B')")
                .IsFixedLength();
            entity.Property(e => e.Consig).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Counted).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CountryOrg).HasMaxLength(3);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CstGrpCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.CstmActing)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.CtrSealQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CustomPer).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DNFEntry).HasDefaultValueSql("((-1))");
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DataVers).HasDefaultValueSql("((1))");
            entity.Property(e => e.DeacAftUL)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Deleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DeprGroup).HasMaxLength(15);
            entity.Property(e => e.DfltWH).HasMaxLength(8);
            entity.Property(e => e.ECExpAcc).HasMaxLength(15);
            entity.Property(e => e.ECInAcct).HasMaxLength(15);
            entity.Property(e => e.EnAstSeri)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EvalSystem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ExcFixAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExcRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Excisable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ExitCur).HasMaxLength(3);
            entity.Property(e => e.ExitPrice).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExitWH).HasMaxLength(8);
            entity.Property(e => e.ExmptIncom).HasMaxLength(15);
            entity.Property(e => e.ExpensAcct).HasMaxLength(15);
            entity.Property(e => e.ExportCode).HasMaxLength(20);
            entity.Property(e => e.FREE)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.FREE1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FirmCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.FixCurrCms).HasMaxLength(3);
            entity.Property(e => e.FrgnExpAcc).HasMaxLength(15);
            entity.Property(e => e.FrgnInAcct).HasMaxLength(15);
            entity.Property(e => e.FrgnName).HasMaxLength(200);
            entity.Property(e => e.FrozenComm).HasMaxLength(30);
            entity.Property(e => e.FuelCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.GLMethod)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('W')")
                .IsFixedLength();
            entity.Property(e => e.GLPickMeth)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('A')")
                .IsFixedLength();
            entity.Property(e => e.GSTRelevnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.GstTaxCtg)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('R')")
                .IsFixedLength();
            entity.Property(e => e.ISvcCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.IWeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.IWeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Imported)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.InCostRoll)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.IncomeAcct).HasMaxLength(15);
            entity.Property(e => e.IndirctTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.InventryNo).HasMaxLength(12);
            entity.Property(e => e.InvntItem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.InvntryUom).HasMaxLength(100);
            entity.Property(e => e.IsCommited).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.IssueMthd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ItemClass)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('2')")
                .IsFixedLength();
            entity.Property(e => e.ItemName).HasMaxLength(200);
            entity.Property(e => e.ItemType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('I')")
                .IsFixedLength();
            entity.Property(e => e.ItmsGrpCod).HasDefaultValueSql("((100))");
            entity.Property(e => e.LastPurCur).HasMaxLength(3);
            entity.Property(e => e.LastPurDat).HasColumnType("datetime");
            entity.Property(e => e.LastPurPrc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LegalText).HasMaxLength(250);
            entity.Property(e => e.LinkRsc).HasMaxLength(50);
            entity.Property(e => e.LogInstanc).HasDefaultValueSql("((0))");
            entity.Property(e => e.LstEvlDate).HasColumnType("datetime");
            entity.Property(e => e.LstEvlPric).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LstSalDate).HasColumnType("datetime");
            entity.Property(e => e.ManBtchNum)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ManOutOnly)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ManSerNum)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.MatGrp).HasDefaultValueSql("((-1))");
            entity.Property(e => e.MatType)
                .HasMaxLength(3)
                .HasDefaultValueSql("('1')");
            entity.Property(e => e.MaxLevel).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MgrByQty)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.MinLevel).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MinOrdrQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MngMethod)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('R')")
                .IsFixedLength();
            entity.Property(e => e.NCMCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NVECode).HasMaxLength(6);
            entity.Property(e => e.NoDiscount)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.NotifyASN).HasMaxLength(40);
            entity.Property(e => e.NumInBuy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NumInCnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NumInSale).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OSvcCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.ObjType)
                .HasMaxLength(20)
                .HasDefaultValueSql("('4')");
            entity.Property(e => e.OnHand).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OnHldPert).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OnOrder).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OneBOneRec)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.OpenBlnc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OrdrMulti).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Phantom)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PicturName).HasMaxLength(200);
            entity.Property(e => e.PlaningSys)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PrchseItem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.PrcrmntMtd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('B')")
                .IsFixedLength();
            entity.Property(e => e.PrdStdCst).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PricingPrc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ProAssNum).HasMaxLength(20);
            entity.Property(e => e.ProductSrc)
                .HasMaxLength(2)
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.PurFactor1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PurFactor2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PurFactor3).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PurFactor4).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PurFormula).HasMaxLength(40);
            entity.Property(e => e.PurPackMsr).HasMaxLength(30);
            entity.Property(e => e.PurPackUn).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.QRCodeSrc).HasColumnType("ntext");
            entity.Property(e => e.QryGroup1)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup10)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup11)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup12)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup13)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup14)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup15)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup16)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup17)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup18)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup19)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup2)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup20)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup21)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup22)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup23)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup24)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup25)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup26)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup27)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup28)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup29)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup3)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup30)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup31)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup32)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup33)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup34)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup35)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup36)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup37)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup38)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup39)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup4)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup40)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup41)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup42)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup43)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup44)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup45)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup46)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup47)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup48)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup49)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup5)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup50)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup51)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup52)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup53)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup54)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup55)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup56)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup57)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup58)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup59)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup6)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup60)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup61)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup62)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup63)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup64)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup7)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup8)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QryGroup9)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.QueryGroup).HasDefaultValueSql("((0))");
            entity.Property(e => e.ReorderPnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ReorderQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetDate).HasColumnType("datetime");
            entity.Property(e => e.RetilrTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.RuleCode).HasMaxLength(2);
            entity.Property(e => e.SACEntry).HasDefaultValueSql("((-1))");
            entity.Property(e => e.SHeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SHeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SLength1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SOIExc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('4')")
                .IsFixedLength();
            entity.Property(e => e.SVolume).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SWW).HasMaxLength(16);
            entity.Property(e => e.SWeight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SWeight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SWidth1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SWidth2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalFactor1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalFactor2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalFactor3).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalFactor4).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalFormula).HasMaxLength(40);
            entity.Property(e => e.SalPackMsr).HasMaxLength(30);
            entity.Property(e => e.SalPackUn).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SalUnitMsr).HasMaxLength(100);
            entity.Property(e => e.ScsCode).HasMaxLength(10);
            entity.Property(e => e.SellItem)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.SerialNum).HasMaxLength(17);
            entity.Property(e => e.ServiceCtg).HasDefaultValueSql("((-1))");
            entity.Property(e => e.ServiceGrp).HasDefaultValueSql("((-1))");
            entity.Property(e => e.Slength2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SouVirAsst).HasMaxLength(50);
            entity.Property(e => e.SpProdType).HasMaxLength(2);
            entity.Property(e => e.SpcialDisc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Spec).HasMaxLength(30);
            entity.Property(e => e.StatAsset)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.StockValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SuppCatNum).HasMaxLength(50);
            entity.Property(e => e.TNVED).HasMaxLength(10);
            entity.Property(e => e.TaxCatCode).HasMaxLength(50);
            entity.Property(e => e.TaxCodeAP).HasMaxLength(8);
            entity.Property(e => e.TaxCodeAR).HasMaxLength(8);
            entity.Property(e => e.TaxCtg).HasMaxLength(4);
            entity.Property(e => e.TaxType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.Traceable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TrackSales)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Transfered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TreeQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TreeType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.U_BoxHeight).HasMaxLength(50);
            entity.Property(e => e.U_BoxLength).HasMaxLength(50);
            entity.Property(e => e.U_BoxVol).HasMaxLength(50);
            entity.Property(e => e.U_BoxWeight).HasMaxLength(50);
            entity.Property(e => e.U_BoxWidth).HasMaxLength(50);
            entity.Property(e => e.U_CustCode).HasMaxLength(10);
            entity.Property(e => e.U_Density).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_Dim1).HasMaxLength(20);
            entity.Property(e => e.U_Dim2).HasMaxLength(20);
            entity.Property(e => e.U_Dim3).HasMaxLength(20);
            entity.Property(e => e.U_Dim4).HasMaxLength(20);
            entity.Property(e => e.U_Dim5).HasMaxLength(20);
            entity.Property(e => e.U_Dosage).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_Dose).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_ISIServices).HasMaxLength(50);
            entity.Property(e => e.U_IndividualItmSz).HasMaxLength(10);
            entity.Property(e => e.U_IndividualWghtItms).HasMaxLength(10);
            entity.Property(e => e.U_IrriFeeType).HasMaxLength(50);
            entity.Property(e => e.U_ItmSubgroup).HasMaxLength(25);
            entity.Property(e => e.U_MinAmount).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_MinVol).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_PackagingMat).HasMaxLength(50);
            entity.Property(e => e.U_PackingSpecification).HasMaxLength(50);
            entity.Property(e => e.U_ReceivingTemp).HasMaxLength(10);
            entity.Property(e => e.U_TIMS_BeamCurrent).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_BeamEnergy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_BeamPower).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_ConvSpeed).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_DoseRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_DoseUnifRatio).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_EstTrpHr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_Frequency).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_NoOfPass).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_PackDensity).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_RDMP).HasMaxLength(100);
            entity.Property(e => e.U_TIMS_RelDoseMax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_RelDoseMin).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_SidePass).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_StorCon)
                .HasMaxLength(25)
                .HasDefaultValueSql("('Dry')");
            entity.Property(e => e.U_TIMS_SurfDensity).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_TIMS_Whse).HasMaxLength(15);
            entity.Property(e => e.U_WeightPallet).HasMaxLength(10);
            entity.Property(e => e.U_WhseFeeType).HasMaxLength(50);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UserText).HasColumnType("ntext");
            entity.Property(e => e.VATLiable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.ValidComm).HasMaxLength(30);
            entity.Property(e => e.VatGourpSa).HasMaxLength(8);
            entity.Property(e => e.VatGroupPu).HasMaxLength(8);
            entity.Property(e => e.VirtAstItm)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.WTLiable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.WarrntTmpl).HasMaxLength(20);
            entity.Property(e => e.WasCounted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.WholSlsTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.frozenFor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.frozenFrom).HasColumnType("datetime");
            entity.Property(e => e.frozenTo).HasColumnType("datetime");
            entity.Property(e => e.onHldLimt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.validFor)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.validFrom).HasColumnType("datetime");
            entity.Property(e => e.validTo).HasColumnType("datetime");
        });

        modelBuilder.Entity<OITW>(entity =>
        {
            entity.HasKey(e => new { e.ItemCode, e.WhsCode }).HasName("OITW_PRIMARY");

            entity.HasIndex(e => e.DftBinAbs, "OITW_DFT_BIN");

            entity.HasIndex(e => e.WhsCode, "OITW_WHS");

            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.WhsCode).HasMaxLength(8);
            entity.Property(e => e.APCMAct).HasMaxLength(15);
            entity.Property(e => e.APCMEUAct).HasMaxLength(15);
            entity.Property(e => e.APCMFrnAct).HasMaxLength(15);
            entity.Property(e => e.ARCMAct).HasMaxLength(15);
            entity.Property(e => e.ARCMEUAct).HasMaxLength(15);
            entity.Property(e => e.ARCMExpAct).HasMaxLength(15);
            entity.Property(e => e.ARCMFrnAct).HasMaxLength(15);
            entity.Property(e => e.AvgPrice).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BalInvntAc).HasMaxLength(15);
            entity.Property(e => e.BalanceAcc).HasMaxLength(15);
            entity.Property(e => e.CNJPMan).HasMaxLength(14);
            entity.Property(e => e.Consig).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CostRvlAct).HasMaxLength(15);
            entity.Property(e => e.Counted).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CstOffsAct).HasMaxLength(15);
            entity.Property(e => e.DecreasAc).HasMaxLength(15);
            entity.Property(e => e.DecresGlAc).HasMaxLength(15);
            entity.Property(e => e.DftBinEnfd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EUExpensAc).HasMaxLength(15);
            entity.Property(e => e.EURevenuAc).HasMaxLength(15);
            entity.Property(e => e.ExchangeAc).HasMaxLength(15);
            entity.Property(e => e.ExmptIncom).HasMaxLength(15);
            entity.Property(e => e.ExpClrAct).HasMaxLength(15);
            entity.Property(e => e.ExpOfstAct).HasMaxLength(15);
            entity.Property(e => e.ExpensesAc).HasMaxLength(15);
            entity.Property(e => e.FrExpensAc).HasMaxLength(15);
            entity.Property(e => e.FrRevenuAc).HasMaxLength(15);
            entity.Property(e => e.FreeChrgPU).HasMaxLength(15);
            entity.Property(e => e.FreeChrgSA).HasMaxLength(15);
            entity.Property(e => e.Freezed)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IncreasAc).HasMaxLength(15);
            entity.Property(e => e.IncresGlAc).HasMaxLength(15);
            entity.Property(e => e.IndEscala)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.IsCommited).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Locked)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.MaxStock).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MinOrder).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MinStock).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NegStckAct).HasMaxLength(15);
            entity.Property(e => e.Object)
                .HasMaxLength(20)
                .HasDefaultValueSql("('31')");
            entity.Property(e => e.OnHand).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OnOrder).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PAReturnAc).HasMaxLength(15);
            entity.Property(e => e.PriceDifAc).HasMaxLength(15);
            entity.Property(e => e.PurBalAct).HasMaxLength(15);
            entity.Property(e => e.PurchOfsAc).HasMaxLength(15);
            entity.Property(e => e.PurchaseAc).HasMaxLength(15);
            entity.Property(e => e.ReturnAc).HasMaxLength(15);
            entity.Property(e => e.RevRetAct).HasMaxLength(15);
            entity.Property(e => e.RevenuesAc).HasMaxLength(15);
            entity.Property(e => e.SaleCostAc).HasMaxLength(15);
            entity.Property(e => e.ShpdGdsAct).HasMaxLength(15);
            entity.Property(e => e.StkInTnAct).HasMaxLength(15);
            entity.Property(e => e.StkOffsAct).HasMaxLength(15);
            entity.Property(e => e.StockOffst).HasMaxLength(15);
            entity.Property(e => e.StockValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StokRvlAct).HasMaxLength(15);
            entity.Property(e => e.TransferAc).HasMaxLength(15);
            entity.Property(e => e.VarianceAc).HasMaxLength(15);
            entity.Property(e => e.VatRevAct).HasMaxLength(15);
            entity.Property(e => e.WasCounted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.WhICenAct).HasMaxLength(15);
            entity.Property(e => e.WhOCenAct).HasMaxLength(15);
            entity.Property(e => e.WipAcct).HasMaxLength(15);
            entity.Property(e => e.WipOffset).HasMaxLength(15);
            entity.Property(e => e.WipVarAcct).HasMaxLength(15);
            entity.Property(e => e.createDate).HasColumnType("datetime");
            entity.Property(e => e.updateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ORDR>(entity =>
        {
            entity.HasKey(e => e.DocEntry).HasName("ORDR_PRIMARY");

            entity.HasIndex(e => new { e.NumAtCard, e.CardCode }, "ORDR_AT_CARD");

            entity.HasIndex(e => e.CardCode, "ORDR_CUSTOMER");

            entity.HasIndex(e => new { e.DocDate, e.PIndicator }, "ORDR_DATE_PIND");

            entity.HasIndex(e => new { e.DocStatus, e.CANCELED }, "ORDR_DOC_STATUS");

            entity.HasIndex(e => new { e.ESeries, e.EDocNum }, "ORDR_ESERIES");

            entity.HasIndex(e => new { e.FatherCard, e.FatherType }, "ORDR_FTHR_CARD");

            entity.HasIndex(e => new { e.DocNum, e.Instance, e.Segment, e.DocSubType, e.PIndicator }, "ORDR_NUM").IsUnique();

            entity.HasIndex(e => e.OwnerCode, "ORDR_OWNER_CODE");

            entity.HasIndex(e => e.Series, "ORDR_SERIES");

            entity.Property(e => e.DocEntry).ValueGeneratedNever();
            entity.Property(e => e.AddLegIn).HasMaxLength(100);
            entity.Property(e => e.Address).HasMaxLength(254);
            entity.Property(e => e.Address2).HasMaxLength(254);
            entity.Property(e => e.AgentCode).HasMaxLength(32);
            entity.Property(e => e.AggregDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.AltBaseTyp).HasDefaultValueSql("((-1))");
            entity.Property(e => e.AqcsTax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AqcsTaxFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AqcsTaxSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AssetDate).HasColumnType("datetime");
            entity.Property(e => e.AtDocType).HasMaxLength(2);
            entity.Property(e => e.Attachment).HasColumnType("ntext");
            entity.Property(e => e.AuthCode).HasMaxLength(250);
            entity.Property(e => e.AutoCrtFlw)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BPChCode).HasMaxLength(15);
            entity.Property(e => e.BPLName).HasMaxLength(100);
            entity.Property(e => e.BPNameOW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BankCode).HasMaxLength(30);
            entity.Property(e => e.BaseAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseAmntFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseAmntSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseDisc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseDiscFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseDiscPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseDiscSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.BaseVtAt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseVtAtFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseVtAtSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BillToOW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BlkCredMmo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BlockDunn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BnkAccount).HasMaxLength(50);
            entity.Property(e => e.BnkBranch).HasMaxLength(50);
            entity.Property(e => e.BnkCntry).HasMaxLength(3);
            entity.Property(e => e.BoeReserev)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Box1099).HasMaxLength(20);
            entity.Property(e => e.BuildDesc).HasMaxLength(50);
            entity.Property(e => e.CANCELED)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.CEECFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.CancelDate).HasColumnType("datetime");
            entity.Property(e => e.CardCode).HasMaxLength(15);
            entity.Property(e => e.CardName).HasMaxLength(100);
            entity.Property(e => e.CashDiscFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CashDiscPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CashDiscSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CashDiscnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CdcOffset).HasDefaultValueSql("((0))");
            entity.Property(e => e.CertNum).HasMaxLength(31);
            entity.Property(e => e.CertifNum).HasMaxLength(50);
            entity.Property(e => e.CheckDigit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ClosingOpt).HasDefaultValueSql("((1))");
            entity.Property(e => e.ClsDate).HasColumnType("datetime");
            entity.Property(e => e.CntrlBnk).HasMaxLength(15);
            entity.Property(e => e.ComTrade)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('E')")
                .IsFixedLength();
            entity.Property(e => e.ComTradeRt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Comments).HasMaxLength(254);
            entity.Property(e => e.Confirmed)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.CopyNumber).HasDefaultValueSql("((0))");
            entity.Property(e => e.CorrExt).HasMaxLength(25);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CreateTran)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.CtActTax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CtActTaxFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CtActTaxSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CtlAccount).HasMaxLength(15);
            entity.Property(e => e.CurSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength();
            entity.Property(e => e.CustOffice).HasMaxLength(60);
            entity.Property(e => e.DANFELgTxt).HasColumnType("ntext");
            entity.Property(e => e.DPPStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DataVers).HasDefaultValueSql("((1))");
            entity.Property(e => e.DateReport).HasColumnType("datetime");
            entity.Property(e => e.DeferrTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DiscPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DiscSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DiscSumFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DiscSumSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DmpTransID).HasMaxLength(20);
            entity.Property(e => e.DocCur).HasMaxLength(3);
            entity.Property(e => e.DocDate).HasColumnType("datetime");
            entity.Property(e => e.DocDlvry)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DocDueDate).HasColumnType("datetime");
            entity.Property(e => e.DocManClsd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DocRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.DocSubType)
                .HasMaxLength(2)
                .HasDefaultValueSql("('--')");
            entity.Property(e => e.DocTaxID).HasMaxLength(32);
            entity.Property(e => e.DocTotal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocTotalFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocTotalSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('I')")
                .IsFixedLength();
            entity.Property(e => e.DpmAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAmntFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAmntSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAppVat).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAppVatF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAppVatS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAppl).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmApplFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmApplSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAsDscnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DpmDrawn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DpmPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.DpmVat).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmVatFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmVatSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DutyStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.EComerGSTN).HasMaxLength(15);
            entity.Property(e => e.ECommerBP).HasMaxLength(15);
            entity.Property(e => e.EDocCancel)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EDocCntnt).HasColumnType("ntext");
            entity.Property(e => e.EDocErrCod).HasMaxLength(50);
            entity.Property(e => e.EDocErrMsg).HasColumnType("ntext");
            entity.Property(e => e.EDocGenTyp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EDocNum).HasMaxLength(50);
            entity.Property(e => e.EDocPrefix).HasMaxLength(10);
            entity.Property(e => e.EDocProces)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength();
            entity.Property(e => e.EDocStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength();
            entity.Property(e => e.EDocTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EDocType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('F')")
                .IsFixedLength();
            entity.Property(e => e.EWBGenType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ElCoMsg).HasMaxLength(254);
            entity.Property(e => e.ElCoStatus).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EnBnkAcct).HasColumnType("ntext");
            entity.Property(e => e.EncryptIV).HasMaxLength(100);
            entity.Property(e => e.EndDlvDate).HasColumnType("datetime");
            entity.Property(e => e.EnvTypeNFe).HasDefaultValueSql("((-1))");
            entity.Property(e => e.EquVatSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EquVatSumF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EquVatSumS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExcDocDate).HasColumnType("datetime");
            entity.Property(e => e.ExcRefDate).HasColumnType("datetime");
            entity.Property(e => e.ExcRmvTime).HasMaxLength(8);
            entity.Property(e => e.Excised)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.ExclTaxRep)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ExepAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExepAmntFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExepAmntSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExepCuAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExepCuFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExepCuSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpAnFrgn).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpAnSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpAnSys).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpAppl).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpApplFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpApplSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Exported)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ExptVAt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExptVAtFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExptVAtSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FCEPmnMean)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.FCI).HasMaxLength(36);
            entity.Property(e => e.FatherCard).HasMaxLength(15);
            entity.Property(e => e.FatherType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('P')")
                .IsFixedLength();
            entity.Property(e => e.Filler).HasMaxLength(8);
            entity.Property(e => e.FiscDocNum).HasMaxLength(100);
            entity.Property(e => e.Flags).HasDefaultValueSql("((0))");
            entity.Property(e => e.FlwRefDate).HasColumnType("datetime");
            entity.Property(e => e.FlwRefNum).HasMaxLength(100);
            entity.Property(e => e.FoCFrght).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FoCFrghtFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FoCFrghtSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FoCTax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FoCTaxFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FoCTaxSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FolioPref).HasMaxLength(4);
            entity.Property(e => e.Footer).HasColumnType("ntext");
            entity.Property(e => e.FreeChrg).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FreeChrgFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FreeChrgSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FrmBpDate).HasColumnType("datetime");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.GSTTranTyp).HasMaxLength(2);
            entity.Property(e => e.GTSRlvnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.GrosProfFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrosProfSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrosProfit).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrossBase).HasDefaultValueSql("((0))");
            entity.Property(e => e.Handwrtten)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Header).HasColumnType("ntext");
            entity.Property(e => e.ISRCodLine).HasMaxLength(53);
            entity.Property(e => e.IgnRelDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IndFinal)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Indicator).HasMaxLength(2);
            entity.Property(e => e.Installmnt).HasDefaultValueSql("((1))");
            entity.Property(e => e.InsurOp347)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.InterimTyp).HasDefaultValueSql("((0))");
            entity.Property(e => e.InvntDirec)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('X')")
                .IsFixedLength();
            entity.Property(e => e.InvntSttus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.IsAlt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsICT)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsPaytoBnk)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsReuseNFN)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsReuseNum)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IssReason).HasDefaultValueSql("((1))");
            entity.Property(e => e.JrnlMemo).HasMaxLength(254);
            entity.Property(e => e.KVVATCode).HasColumnType("ntext");
            entity.Property(e => e.LYPmtAt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LYPmtAtFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LYPmtAtSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LastPmnTyp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Letter)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LetterNum).HasMaxLength(50);
            entity.Property(e => e.LicTradNum).HasMaxLength(32);
            entity.Property(e => e.LogInstanc).HasDefaultValueSql("((0))");
            entity.Property(e => e.MInvDate).HasColumnType("datetime");
            entity.Property(e => e.MYFtype).HasMaxLength(2);
            entity.Property(e => e.ManualNum).HasMaxLength(20);
            entity.Property(e => e.Max1099).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MaxDscn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Model)
                .HasMaxLength(6)
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.NTSApprNo).HasMaxLength(50);
            entity.Property(e => e.NTSApprov)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.NTSeTaxNo).HasMaxLength(50);
            entity.Property(e => e.NbSbAmntFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NbSbVAtFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NetProc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.NfePrntFo).HasDefaultValueSql("((0))");
            entity.Property(e => e.NfeValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbAmntSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbCuAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbCuFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbCuSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbVAt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbVAtSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Notify)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NumAtCard).HasMaxLength(100);
            entity.Property(e => e.ObjType)
                .HasMaxLength(20)
                .HasDefaultValueSql("('17')");
            entity.Property(e => e.OnlineQuo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.OpenForLaC)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.Ordered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.OriginType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('M')")
                .IsFixedLength();
            entity.Property(e => e.PIndicator)
                .HasMaxLength(10)
                .HasDefaultValueSql("(' ')");
            entity.Property(e => e.POSEqNum).HasMaxLength(20);
            entity.Property(e => e.POSManufSN).HasMaxLength(20);
            entity.Property(e => e.PQTGrpHW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PTICode).HasMaxLength(5);
            entity.Property(e => e.PaidDpm).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidDpmF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidDpmS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidSumFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidSumSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidSys).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidToDate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PartSupply)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.PayBlock)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PayDuMonth)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PayToCode).HasMaxLength(50);
            entity.Property(e => e.PaymentRef).HasMaxLength(27);
            entity.Property(e => e.PermitNo).HasMaxLength(20);
            entity.Property(e => e.PeyMethod).HasMaxLength(15);
            entity.Property(e => e.Pick)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PickRmrk).HasMaxLength(254);
            entity.Property(e => e.PickStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PoDropPrss)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PoPrss)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PostPmntWT)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Posted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.PriceMode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PrintSEPA)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Printed)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Project).HasMaxLength(20);
            entity.Property(e => e.QRCodeSPGn).HasColumnType("ntext");
            entity.Property(e => e.QRCodeSrc).HasColumnType("ntext");
            entity.Property(e => e.Ref1).HasMaxLength(11);
            entity.Property(e => e.Ref2).HasMaxLength(11);
            entity.Property(e => e.RelatedTyp).HasDefaultValueSql("((-1))");
            entity.Property(e => e.ReopManCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ReopOriDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.RepSection).HasMaxLength(3);
            entity.Property(e => e.ReqDate).HasColumnType("datetime");
            entity.Property(e => e.ReqName).HasMaxLength(155);
            entity.Property(e => e.ReqType).HasDefaultValueSql("((12))");
            entity.Property(e => e.Requester).HasMaxLength(25);
            entity.Property(e => e.Reserve)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ResidenNum)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('1')")
                .IsFixedLength();
            entity.Property(e => e.RetInvoice)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.RevCreRefD).HasColumnType("datetime");
            entity.Property(e => e.RevCreRefN).HasMaxLength(100);
            entity.Property(e => e.RevRefDate).HasColumnType("datetime");
            entity.Property(e => e.RevRefNo).HasMaxLength(100);
            entity.Property(e => e.Revision)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.RevisionPo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.RoundDif).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RoundDifFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RoundDifSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Rounding)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SAPPassprt).HasColumnType("ntext");
            entity.Property(e => e.SSIExmpt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SelfPosted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SeriesStr).HasMaxLength(3);
            entity.Property(e => e.ShipPlace).HasMaxLength(60);
            entity.Property(e => e.ShipState).HasMaxLength(3);
            entity.Property(e => e.ShipToCode).HasMaxLength(50);
            entity.Property(e => e.ShipToOW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ShowSCN)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SignDigest).HasColumnType("ntext");
            entity.Property(e => e.SignMsg).HasColumnType("ntext");
            entity.Property(e => e.SlpCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.SpecDate).HasColumnType("datetime");
            entity.Property(e => e.SplitPmnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SplitTax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SplitTaxFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SplitTaxSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SrvGpPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SrvTaxRule)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.StDlvDate).HasColumnType("datetime");
            entity.Property(e => e.StampNum).HasMaxLength(16);
            entity.Property(e => e.SubStr).HasMaxLength(3);
            entity.Property(e => e.SumAbsId).HasDefaultValueSql("((-1))");
            entity.Property(e => e.SumRptDate).HasColumnType("datetime");
            entity.Property(e => e.SummryType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SupplCode).HasMaxLength(254);
            entity.Property(e => e.Supplier).HasMaxLength(15);
            entity.Property(e => e.SysRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxDate).HasColumnType("datetime");
            entity.Property(e => e.TaxInvNo).HasMaxLength(100);
            entity.Property(e => e.TaxOnExAp).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnExApF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnExApS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnExp).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnExpFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnExpSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ToBinCode).HasMaxLength(228);
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.ToWhsCode).HasMaxLength(8);
            entity.Property(e => e.TotalExpFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TotalExpSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TotalExpns).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TrackNo).HasMaxLength(30);
            entity.Property(e => e.Transfered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TrnspCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.TxInvRptDt).HasColumnType("datetime");
            entity.Property(e => e.TxInvRptNo).HasMaxLength(10);
            entity.Property(e => e.U_1601rep).HasMaxLength(5);
            entity.Property(e => e.U_1601repdate).HasColumnType("datetime");
            entity.Property(e => e.U_2307rep).HasMaxLength(5);
            entity.Property(e => e.U_2307repdate).HasColumnType("datetime");
            entity.Property(e => e.U_ARDPNo).HasMaxLength(50);
            entity.Property(e => e.U_ActIrridiationDt).HasColumnType("datetime");
            entity.Property(e => e.U_AppBy).HasMaxLength(50);
            entity.Property(e => e.U_Budgeted).HasMaxLength(3);
            entity.Property(e => e.U_CMType).HasMaxLength(50);
            entity.Property(e => e.U_CWTValidated).HasMaxLength(10);
            entity.Property(e => e.U_CancelType).HasMaxLength(50);
            entity.Property(e => e.U_CheckBy).HasMaxLength(20);
            entity.Property(e => e.U_CustItems).HasMaxLength(25);
            entity.Property(e => e.U_DRNo).HasMaxLength(50);
            entity.Property(e => e.U_DelBy).HasMaxLength(20);
            entity.Property(e => e.U_Department).HasMaxLength(20);
            entity.Property(e => e.U_Division).HasMaxLength(50);
            entity.Property(e => e.U_Driver).HasMaxLength(25);
            entity.Property(e => e.U_DueDatePayment).HasColumnType("datetime");
            entity.Property(e => e.U_EBStatus).HasMaxLength(50);
            entity.Property(e => e.U_EmpCode).HasMaxLength(30);
            entity.Property(e => e.U_ExcessDays).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_FacilityLoc)
                .HasMaxLength(50)
                .HasDefaultValueSql("('Tanay 1')");
            entity.Property(e => e.U_IPNo).HasMaxLength(10);
            entity.Property(e => e.U_IrridiationDate).HasColumnType("datetime");
            entity.Property(e => e.U_Location).HasMaxLength(50);
            entity.Property(e => e.U_PONo).HasMaxLength(50);
            entity.Property(e => e.U_PRNo).HasMaxLength(20);
            entity.Property(e => e.U_PaymentSettlement).HasMaxLength(20);
            entity.Property(e => e.U_PickUpDate).HasColumnType("datetime");
            entity.Property(e => e.U_PlateNo).HasMaxLength(15);
            entity.Property(e => e.U_PrepBy).HasMaxLength(50);
            entity.Property(e => e.U_ReasonARCM).HasMaxLength(50);
            entity.Property(e => e.U_RecBy).HasMaxLength(25);
            entity.Property(e => e.U_RegName).HasMaxLength(30);
            entity.Property(e => e.U_Remarks).HasMaxLength(254);
            entity.Property(e => e.U_ReqBy).HasMaxLength(20);
            entity.Property(e => e.U_ReqForCancel)
                .HasMaxLength(3)
                .HasDefaultValueSql("('No')");
            entity.Property(e => e.U_RetType).HasMaxLength(15);
            entity.Property(e => e.U_ReturnRsn).HasMaxLength(30);
            entity.Property(e => e.U_RevBy).HasMaxLength(20);
            entity.Property(e => e.U_SINo).HasMaxLength(20);
            entity.Property(e => e.U_SONo).HasMaxLength(15);
            entity.Property(e => e.U_SOStatus).HasMaxLength(50);
            entity.Property(e => e.U_ServiceType).HasMaxLength(50);
            entity.Property(e => e.U_Submitted2307)
                .HasMaxLength(3)
                .HasDefaultValueSql("('N')");
            entity.Property(e => e.U_TIMS_ManufLotNo).HasMaxLength(20);
            entity.Property(e => e.U_TIMS_SerOrNo).HasMaxLength(20);
            entity.Property(e => e.U_TransCat)
                .HasMaxLength(20)
                .HasDefaultValueSql("('Declared')");
            entity.Property(e => e.U_TransType).HasMaxLength(10);
            entity.Property(e => e.U_TransferType).HasMaxLength(50);
            entity.Property(e => e.U_WtaxValDate).HasColumnType("datetime");
            entity.Property(e => e.U_WtaxValStat)
                .HasMaxLength(10)
                .HasDefaultValueSql("('N')");
            entity.Property(e => e.UpdCardBal)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.UpdInvnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UseBilAddr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UseCorrVat)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.UseShpdGd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.VATFirst)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.VATRegNum).HasMaxLength(32);
            entity.Property(e => e.VatDate).HasColumnType("datetime");
            entity.Property(e => e.VatJENum).HasDefaultValueSql("((-1))");
            entity.Property(e => e.VatPaid).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatPaidFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatPaidSys).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatPercent).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSumFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSumSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VclPlate).HasMaxLength(20);
            entity.Property(e => e.VersionNum).HasMaxLength(13);
            entity.Property(e => e.Volume).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTApplied).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTAppliedF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTAppliedS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTDetails).HasMaxLength(100);
            entity.Property(e => e.WTSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTSumFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTSumSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WddStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .IsFixedLength();
            entity.Property(e => e.Weight).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.draftKey).HasDefaultValueSql("((-1))");
            entity.Property(e => e.isCrin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.isIns)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.selfInv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.submitted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
        });

        modelBuilder.Entity<OWHS>(entity =>
        {
            entity.HasKey(e => e.WhsCode).HasName("OWHS_PRIMARY");

            entity.HasIndex(e => e.DftBinAbs, "OWHS_DFT_BIN");

            entity.Property(e => e.WhsCode).HasMaxLength(8);
            entity.Property(e => e.APCMAct).HasMaxLength(15);
            entity.Property(e => e.APCMEUAct).HasMaxLength(15);
            entity.Property(e => e.APCMFrnAct).HasMaxLength(15);
            entity.Property(e => e.ARCMAct).HasMaxLength(15);
            entity.Property(e => e.ARCMEUAct).HasMaxLength(15);
            entity.Property(e => e.ARCMExpAct).HasMaxLength(15);
            entity.Property(e => e.ARCMFrnAct).HasMaxLength(15);
            entity.Property(e => e.AddrType).HasMaxLength(100);
            entity.Property(e => e.Address2).HasMaxLength(50);
            entity.Property(e => e.Address3).HasMaxLength(50);
            entity.Property(e => e.AutoIssMtd).HasDefaultValueSql("((0))");
            entity.Property(e => e.AutoRecvMd).HasDefaultValueSql("((0))");
            entity.Property(e => e.BalInvntAc).HasMaxLength(15);
            entity.Property(e => e.BalanceAcc).HasMaxLength(15);
            entity.Property(e => e.BinActivat)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BinSeptor)
                .HasMaxLength(5)
                .HasDefaultValueSql("('-')");
            entity.Property(e => e.Block).HasMaxLength(100);
            entity.Property(e => e.Building).HasColumnType("ntext");
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.CostRvlAct).HasMaxLength(15);
            entity.Property(e => e.Country).HasMaxLength(3);
            entity.Property(e => e.County).HasMaxLength(100);
            entity.Property(e => e.CstOffsAct).HasMaxLength(15);
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DecreasAc).HasMaxLength(15);
            entity.Property(e => e.DecresGlAc).HasMaxLength(15);
            entity.Property(e => e.DftBinEnfd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DropShip)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EUExpensAc).HasMaxLength(15);
            entity.Property(e => e.EURevenuAc).HasMaxLength(15);
            entity.Property(e => e.ExchangeAc).HasMaxLength(15);
            entity.Property(e => e.Excisable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ExmptIncom).HasMaxLength(15);
            entity.Property(e => e.ExpClrAct).HasMaxLength(15);
            entity.Property(e => e.ExpOfstAct).HasMaxLength(15);
            entity.Property(e => e.ExpensesAc).HasMaxLength(15);
            entity.Property(e => e.External)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.FedTaxID).HasMaxLength(32);
            entity.Property(e => e.FrExpensAc).HasMaxLength(15);
            entity.Property(e => e.FrRevenuAc).HasMaxLength(15);
            entity.Property(e => e.FreeChrgPU).HasMaxLength(15);
            entity.Property(e => e.FreeChrgSA).HasMaxLength(15);
            entity.Property(e => e.GlblLocNum).HasMaxLength(50);
            entity.Property(e => e.Grp_Code).HasMaxLength(4);
            entity.Property(e => e.Inactive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IncreasAc).HasMaxLength(15);
            entity.Property(e => e.IncresGlAc).HasMaxLength(15);
            entity.Property(e => e.LegalText).HasMaxLength(250);
            entity.Property(e => e.Locked)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ManageSnB)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.NegStckAct).HasMaxLength(15);
            entity.Property(e => e.Nettable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.OwnerCode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('1')")
                .IsFixedLength();
            entity.Property(e => e.PAReturnAc).HasMaxLength(15);
            entity.Property(e => e.PriceDifAc).HasMaxLength(15);
            entity.Property(e => e.PurBalAct).HasMaxLength(15);
            entity.Property(e => e.PurchOfsAc).HasMaxLength(15);
            entity.Property(e => e.PurchaseAc).HasMaxLength(15);
            entity.Property(e => e.RecBinEnab)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.RecItemsBy).HasDefaultValueSql("((0))");
            entity.Property(e => e.RecvEmpBin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.RecvMaxQty)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.RecvMaxWT)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.RecvUpTo)
                .HasMaxLength(6)
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.ReturnAc).HasMaxLength(15);
            entity.Property(e => e.RevRetAct).HasMaxLength(15);
            entity.Property(e => e.RevenuesAc).HasMaxLength(15);
            entity.Property(e => e.SaleCostAc).HasMaxLength(15);
            entity.Property(e => e.Shipper).HasMaxLength(15);
            entity.Property(e => e.ShpdGdsAct).HasMaxLength(15);
            entity.Property(e => e.State).HasMaxLength(3);
            entity.Property(e => e.StkInTnAct).HasMaxLength(15);
            entity.Property(e => e.StkOffsAct).HasMaxLength(15);
            entity.Property(e => e.StockOffst).HasMaxLength(15);
            entity.Property(e => e.StokRvlAct).HasMaxLength(15);
            entity.Property(e => e.Street).HasMaxLength(100);
            entity.Property(e => e.StreetNo).HasMaxLength(100);
            entity.Property(e => e.TaxOffice).HasMaxLength(50);
            entity.Property(e => e.TransferAc).HasMaxLength(15);
            entity.Property(e => e.UseTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.VarianceAc).HasMaxLength(15);
            entity.Property(e => e.VatGroup).HasMaxLength(8);
            entity.Property(e => e.VatRevAct).HasMaxLength(15);
            entity.Property(e => e.WhICenAct).HasMaxLength(15);
            entity.Property(e => e.WhOCenAct).HasMaxLength(15);
            entity.Property(e => e.WhShipTo).HasMaxLength(100);
            entity.Property(e => e.WhsName).HasMaxLength(100);
            entity.Property(e => e.WipAcct).HasMaxLength(15);
            entity.Property(e => e.WipOffset).HasMaxLength(15);
            entity.Property(e => e.WipVarAcct).HasMaxLength(15);
            entity.Property(e => e.ZipCode).HasMaxLength(20);
            entity.Property(e => e.createDate).HasColumnType("datetime");
            entity.Property(e => e.objType)
                .HasMaxLength(20)
                .HasDefaultValueSql("('64')");
            entity.Property(e => e.updateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<OWTR>(entity =>
        {
            entity.HasKey(e => e.DocEntry).HasName("OWTR_PRIMARY");

            entity.HasIndex(e => new { e.NumAtCard, e.CardCode }, "OWTR_AT_CARD");

            entity.HasIndex(e => e.CardCode, "OWTR_CUSTOMER");

            entity.HasIndex(e => new { e.DocDate, e.PIndicator }, "OWTR_DATE_PIND");

            entity.HasIndex(e => new { e.DocStatus, e.CANCELED }, "OWTR_DOC_STATUS");

            entity.HasIndex(e => new { e.ESeries, e.EDocNum }, "OWTR_ESERIES");

            entity.HasIndex(e => e.FolSeries, "OWTR_FOL_SERIES");

            entity.HasIndex(e => new { e.FatherCard, e.FatherType }, "OWTR_FTHR_CARD");

            entity.HasIndex(e => new { e.DocNum, e.Instance, e.Segment, e.DocSubType, e.PIndicator }, "OWTR_NUM").IsUnique();

            entity.HasIndex(e => e.OwnerCode, "OWTR_OWNER_CODE");

            entity.HasIndex(e => e.Series, "OWTR_SERIES");

            entity.Property(e => e.DocEntry).ValueGeneratedNever();
            entity.Property(e => e.AddLegIn).HasMaxLength(100);
            entity.Property(e => e.Address).HasMaxLength(254);
            entity.Property(e => e.Address2).HasMaxLength(254);
            entity.Property(e => e.AgentCode).HasMaxLength(32);
            entity.Property(e => e.AggregDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.AltBaseTyp).HasDefaultValueSql("((-1))");
            entity.Property(e => e.AqcsTax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AqcsTaxFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AqcsTaxSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.AssetDate).HasColumnType("datetime");
            entity.Property(e => e.AtDocType).HasMaxLength(2);
            entity.Property(e => e.Attachment).HasColumnType("ntext");
            entity.Property(e => e.AuthCode).HasMaxLength(250);
            entity.Property(e => e.AutoCrtFlw)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BPChCode).HasMaxLength(15);
            entity.Property(e => e.BPLName).HasMaxLength(100);
            entity.Property(e => e.BPNameOW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BankCode).HasMaxLength(30);
            entity.Property(e => e.BaseAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseAmntFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseAmntSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseDisc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseDiscFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseDiscPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseDiscSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.BaseVtAt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseVtAtFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseVtAtSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BillToOW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BlkCredMmo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BlockDunn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.BnkAccount).HasMaxLength(50);
            entity.Property(e => e.BnkBranch).HasMaxLength(50);
            entity.Property(e => e.BnkCntry).HasMaxLength(3);
            entity.Property(e => e.BoeReserev)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Box1099).HasMaxLength(20);
            entity.Property(e => e.BuildDesc).HasMaxLength(50);
            entity.Property(e => e.CANCELED)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.CEECFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.CancelDate).HasColumnType("datetime");
            entity.Property(e => e.CardCode).HasMaxLength(15);
            entity.Property(e => e.CardName).HasMaxLength(100);
            entity.Property(e => e.CashDiscFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CashDiscPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CashDiscSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CashDiscnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CdcOffset).HasDefaultValueSql("((0))");
            entity.Property(e => e.CertNum).HasMaxLength(31);
            entity.Property(e => e.CertifNum).HasMaxLength(50);
            entity.Property(e => e.CheckDigit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ClosingOpt).HasDefaultValueSql("((1))");
            entity.Property(e => e.ClsDate).HasColumnType("datetime");
            entity.Property(e => e.CntrlBnk).HasMaxLength(15);
            entity.Property(e => e.ComTrade)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('E')")
                .IsFixedLength();
            entity.Property(e => e.ComTradeRt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Comments).HasMaxLength(254);
            entity.Property(e => e.Confirmed)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.CopyNumber).HasDefaultValueSql("((0))");
            entity.Property(e => e.CorrExt).HasMaxLength(25);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.CreateTran)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.CtActTax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CtActTaxFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CtActTaxSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.CtlAccount).HasMaxLength(15);
            entity.Property(e => e.CurSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength();
            entity.Property(e => e.CustOffice).HasMaxLength(60);
            entity.Property(e => e.DANFELgTxt).HasColumnType("ntext");
            entity.Property(e => e.DPPStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DataSource)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DataVers).HasDefaultValueSql("((1))");
            entity.Property(e => e.DateReport).HasColumnType("datetime");
            entity.Property(e => e.DeferrTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DiscPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DiscSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DiscSumFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DiscSumSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DmpTransID).HasMaxLength(20);
            entity.Property(e => e.DocCur).HasMaxLength(3);
            entity.Property(e => e.DocDate).HasColumnType("datetime");
            entity.Property(e => e.DocDlvry)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DocDueDate).HasColumnType("datetime");
            entity.Property(e => e.DocManClsd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DocRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.DocSubType)
                .HasMaxLength(2)
                .HasDefaultValueSql("('--')");
            entity.Property(e => e.DocTaxID).HasMaxLength(32);
            entity.Property(e => e.DocTotal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocTotalFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocTotalSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('I')")
                .IsFixedLength();
            entity.Property(e => e.DpmAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAmntFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAmntSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAppVat).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAppVatF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAppVatS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAppl).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmApplFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmApplSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmAsDscnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DpmDrawn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DpmPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.DpmVat).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmVatFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DpmVatSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DutyStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.EComerGSTN).HasMaxLength(15);
            entity.Property(e => e.ECommerBP).HasMaxLength(15);
            entity.Property(e => e.EDocCancel)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EDocCntnt).HasColumnType("ntext");
            entity.Property(e => e.EDocErrCod).HasMaxLength(50);
            entity.Property(e => e.EDocErrMsg).HasColumnType("ntext");
            entity.Property(e => e.EDocGenTyp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EDocNum).HasMaxLength(50);
            entity.Property(e => e.EDocPrefix).HasMaxLength(10);
            entity.Property(e => e.EDocProces)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength();
            entity.Property(e => e.EDocStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('C')")
                .IsFixedLength();
            entity.Property(e => e.EDocTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EDocType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('F')")
                .IsFixedLength();
            entity.Property(e => e.EWBGenType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ElCoMsg).HasMaxLength(254);
            entity.Property(e => e.ElCoStatus).HasMaxLength(10);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EnBnkAcct).HasColumnType("ntext");
            entity.Property(e => e.EncryptIV).HasMaxLength(100);
            entity.Property(e => e.EndDlvDate).HasColumnType("datetime");
            entity.Property(e => e.EnvTypeNFe).HasDefaultValueSql("((-1))");
            entity.Property(e => e.EquVatSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EquVatSumF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EquVatSumS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExcDocDate).HasColumnType("datetime");
            entity.Property(e => e.ExcRefDate).HasColumnType("datetime");
            entity.Property(e => e.ExcRmvTime).HasMaxLength(8);
            entity.Property(e => e.Excised)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.ExclTaxRep)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ExepAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExepAmntFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExepAmntSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExepCuAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExepCuFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExepCuSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpAnFrgn).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpAnSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpAnSys).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpAppl).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpApplFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpApplSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Exported)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ExptVAt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExptVAtFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExptVAtSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FCEPmnMean)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.FCI).HasMaxLength(36);
            entity.Property(e => e.FatherCard).HasMaxLength(15);
            entity.Property(e => e.FatherType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('P')")
                .IsFixedLength();
            entity.Property(e => e.Filler).HasMaxLength(8);
            entity.Property(e => e.FiscDocNum).HasMaxLength(100);
            entity.Property(e => e.Flags).HasDefaultValueSql("((0))");
            entity.Property(e => e.FlwRefDate).HasColumnType("datetime");
            entity.Property(e => e.FlwRefNum).HasMaxLength(100);
            entity.Property(e => e.FoCFrght).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FoCFrghtFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FoCFrghtSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FoCTax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FoCTaxFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FoCTaxSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FolioPref).HasMaxLength(4);
            entity.Property(e => e.Footer).HasColumnType("ntext");
            entity.Property(e => e.FreeChrg).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FreeChrgFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FreeChrgSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FrmBpDate).HasColumnType("datetime");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.GSTTranTyp).HasMaxLength(2);
            entity.Property(e => e.GTSRlvnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.GrosProfFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrosProfSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrosProfit).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrossBase).HasDefaultValueSql("((0))");
            entity.Property(e => e.Handwrtten)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Header).HasColumnType("ntext");
            entity.Property(e => e.ISRCodLine).HasMaxLength(53);
            entity.Property(e => e.IgnRelDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IndFinal)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Indicator).HasMaxLength(2);
            entity.Property(e => e.Installmnt).HasDefaultValueSql("((1))");
            entity.Property(e => e.InsurOp347)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.InterimTyp).HasDefaultValueSql("((0))");
            entity.Property(e => e.InvntDirec)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('X')")
                .IsFixedLength();
            entity.Property(e => e.InvntSttus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.IsAlt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsICT)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsPaytoBnk)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsReuseNFN)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsReuseNum)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IssReason).HasDefaultValueSql("((1))");
            entity.Property(e => e.JrnlMemo).HasMaxLength(254);
            entity.Property(e => e.KVVATCode).HasColumnType("ntext");
            entity.Property(e => e.LYPmtAt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LYPmtAtFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LYPmtAtSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LastPmnTyp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Letter)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LetterNum).HasMaxLength(50);
            entity.Property(e => e.LicTradNum).HasMaxLength(32);
            entity.Property(e => e.LogInstanc).HasDefaultValueSql("((0))");
            entity.Property(e => e.MInvDate).HasColumnType("datetime");
            entity.Property(e => e.MYFtype).HasMaxLength(2);
            entity.Property(e => e.ManualNum).HasMaxLength(20);
            entity.Property(e => e.Max1099).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MaxDscn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Model)
                .HasMaxLength(6)
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.NTSApprNo).HasMaxLength(50);
            entity.Property(e => e.NTSApprov)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.NTSeTaxNo).HasMaxLength(50);
            entity.Property(e => e.NbSbAmntFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NbSbVAtFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NetProc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.NfePrntFo).HasDefaultValueSql("((0))");
            entity.Property(e => e.NfeValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbAmntSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbCuAmnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbCuFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbCuSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbVAt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NnSbVAtSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Notify)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NumAtCard).HasMaxLength(100);
            entity.Property(e => e.ObjType)
                .HasMaxLength(20)
                .HasDefaultValueSql("('67')");
            entity.Property(e => e.OnlineQuo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.OpenForLaC)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.Ordered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.OriginType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('M')")
                .IsFixedLength();
            entity.Property(e => e.PIndicator)
                .HasMaxLength(10)
                .HasDefaultValueSql("(' ')");
            entity.Property(e => e.POSEqNum).HasMaxLength(20);
            entity.Property(e => e.POSManufSN).HasMaxLength(20);
            entity.Property(e => e.PQTGrpHW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PTICode).HasMaxLength(5);
            entity.Property(e => e.PaidDpm).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidDpmF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidDpmS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidSumFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidSumSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidSys).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PaidToDate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PartSupply)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.PayBlock)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PayDuMonth)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PayToCode).HasMaxLength(50);
            entity.Property(e => e.PaymentRef).HasMaxLength(27);
            entity.Property(e => e.PermitNo).HasMaxLength(20);
            entity.Property(e => e.PeyMethod).HasMaxLength(15);
            entity.Property(e => e.Pick)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PickRmrk).HasMaxLength(254);
            entity.Property(e => e.PickStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PoDropPrss)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PoPrss)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PostPmntWT)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Posted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.PriceMode)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PrintSEPA)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Printed)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Project).HasMaxLength(20);
            entity.Property(e => e.QRCodeSPGn).HasColumnType("ntext");
            entity.Property(e => e.QRCodeSrc).HasColumnType("ntext");
            entity.Property(e => e.Ref1).HasMaxLength(11);
            entity.Property(e => e.Ref2).HasMaxLength(11);
            entity.Property(e => e.RelatedTyp).HasDefaultValueSql("((-1))");
            entity.Property(e => e.ReopManCls)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ReopOriDoc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.RepSection).HasMaxLength(3);
            entity.Property(e => e.ReqDate).HasColumnType("datetime");
            entity.Property(e => e.ReqName).HasMaxLength(155);
            entity.Property(e => e.ReqType).HasDefaultValueSql("((12))");
            entity.Property(e => e.Requester).HasMaxLength(25);
            entity.Property(e => e.Reserve)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ResidenNum)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('1')")
                .IsFixedLength();
            entity.Property(e => e.RetInvoice)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.RevCreRefD).HasColumnType("datetime");
            entity.Property(e => e.RevCreRefN).HasMaxLength(100);
            entity.Property(e => e.RevRefDate).HasColumnType("datetime");
            entity.Property(e => e.RevRefNo).HasMaxLength(100);
            entity.Property(e => e.Revision)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.RevisionPo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.RoundDif).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RoundDifFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RoundDifSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Rounding)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SAPPassprt).HasColumnType("ntext");
            entity.Property(e => e.SSIExmpt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SelfPosted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SeriesStr).HasMaxLength(3);
            entity.Property(e => e.ShipPlace).HasMaxLength(60);
            entity.Property(e => e.ShipState).HasMaxLength(3);
            entity.Property(e => e.ShipToCode).HasMaxLength(50);
            entity.Property(e => e.ShipToOW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ShowSCN)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SignDigest).HasColumnType("ntext");
            entity.Property(e => e.SignMsg).HasColumnType("ntext");
            entity.Property(e => e.SlpCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.SpecDate).HasColumnType("datetime");
            entity.Property(e => e.SplitPmnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SplitTax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SplitTaxFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SplitTaxSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SrvGpPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SrvTaxRule)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.StDlvDate).HasColumnType("datetime");
            entity.Property(e => e.StampNum).HasMaxLength(16);
            entity.Property(e => e.SubStr).HasMaxLength(3);
            entity.Property(e => e.SumAbsId).HasDefaultValueSql("((-1))");
            entity.Property(e => e.SumRptDate).HasColumnType("datetime");
            entity.Property(e => e.SummryType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SupplCode).HasMaxLength(254);
            entity.Property(e => e.Supplier).HasMaxLength(15);
            entity.Property(e => e.SysRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxDate).HasColumnType("datetime");
            entity.Property(e => e.TaxInvNo).HasMaxLength(100);
            entity.Property(e => e.TaxOnExAp).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnExApF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnExApS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnExp).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnExpFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnExpSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ToBinCode).HasMaxLength(228);
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.ToWhsCode).HasMaxLength(8);
            entity.Property(e => e.TotalExpFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TotalExpSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TotalExpns).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TrackNo).HasMaxLength(30);
            entity.Property(e => e.Transfered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TrnspCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.TxInvRptDt).HasColumnType("datetime");
            entity.Property(e => e.TxInvRptNo).HasMaxLength(10);
            entity.Property(e => e.U_1601rep).HasMaxLength(5);
            entity.Property(e => e.U_1601repdate).HasColumnType("datetime");
            entity.Property(e => e.U_2307rep).HasMaxLength(5);
            entity.Property(e => e.U_2307repdate).HasColumnType("datetime");
            entity.Property(e => e.U_ARDPNo).HasMaxLength(50);
            entity.Property(e => e.U_ActIrridiationDt).HasColumnType("datetime");
            entity.Property(e => e.U_AppBy).HasMaxLength(50);
            entity.Property(e => e.U_Budgeted).HasMaxLength(3);
            entity.Property(e => e.U_CMType).HasMaxLength(50);
            entity.Property(e => e.U_CWTValidated).HasMaxLength(10);
            entity.Property(e => e.U_CancelType).HasMaxLength(50);
            entity.Property(e => e.U_CheckBy).HasMaxLength(20);
            entity.Property(e => e.U_CustItems).HasMaxLength(25);
            entity.Property(e => e.U_DRNo).HasMaxLength(50);
            entity.Property(e => e.U_DelBy).HasMaxLength(20);
            entity.Property(e => e.U_Department).HasMaxLength(20);
            entity.Property(e => e.U_Division).HasMaxLength(50);
            entity.Property(e => e.U_Driver).HasMaxLength(25);
            entity.Property(e => e.U_DueDatePayment).HasColumnType("datetime");
            entity.Property(e => e.U_EBStatus).HasMaxLength(50);
            entity.Property(e => e.U_EmpCode).HasMaxLength(30);
            entity.Property(e => e.U_ExcessDays).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_FacilityLoc)
                .HasMaxLength(50)
                .HasDefaultValueSql("('Tanay 1')");
            entity.Property(e => e.U_IPNo).HasMaxLength(10);
            entity.Property(e => e.U_IrridiationDate).HasColumnType("datetime");
            entity.Property(e => e.U_Location).HasMaxLength(50);
            entity.Property(e => e.U_PONo).HasMaxLength(50);
            entity.Property(e => e.U_PRNo).HasMaxLength(20);
            entity.Property(e => e.U_PaymentSettlement).HasMaxLength(20);
            entity.Property(e => e.U_PickUpDate).HasColumnType("datetime");
            entity.Property(e => e.U_PlateNo).HasMaxLength(15);
            entity.Property(e => e.U_PrepBy).HasMaxLength(50);
            entity.Property(e => e.U_ReasonARCM).HasMaxLength(50);
            entity.Property(e => e.U_RecBy).HasMaxLength(25);
            entity.Property(e => e.U_RegName).HasMaxLength(30);
            entity.Property(e => e.U_Remarks).HasMaxLength(254);
            entity.Property(e => e.U_ReqBy).HasMaxLength(20);
            entity.Property(e => e.U_ReqForCancel)
                .HasMaxLength(3)
                .HasDefaultValueSql("('No')");
            entity.Property(e => e.U_RetType).HasMaxLength(15);
            entity.Property(e => e.U_ReturnRsn).HasMaxLength(30);
            entity.Property(e => e.U_RevBy).HasMaxLength(20);
            entity.Property(e => e.U_SINo).HasMaxLength(20);
            entity.Property(e => e.U_SONo).HasMaxLength(15);
            entity.Property(e => e.U_SOStatus).HasMaxLength(50);
            entity.Property(e => e.U_ServiceType).HasMaxLength(50);
            entity.Property(e => e.U_Submitted2307)
                .HasMaxLength(3)
                .HasDefaultValueSql("('N')");
            entity.Property(e => e.U_TIMS_ManufLotNo).HasMaxLength(20);
            entity.Property(e => e.U_TIMS_SerOrNo).HasMaxLength(20);
            entity.Property(e => e.U_TransCat)
                .HasMaxLength(20)
                .HasDefaultValueSql("('Declared')");
            entity.Property(e => e.U_TransType).HasMaxLength(10);
            entity.Property(e => e.U_TransferType).HasMaxLength(50);
            entity.Property(e => e.U_WtaxValDate).HasColumnType("datetime");
            entity.Property(e => e.U_WtaxValStat)
                .HasMaxLength(10)
                .HasDefaultValueSql("('N')");
            entity.Property(e => e.UpdCardBal)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.UpdInvnt)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
            entity.Property(e => e.UseBilAddr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UseCorrVat)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.UseShpdGd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.VATFirst)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.VATRegNum).HasMaxLength(32);
            entity.Property(e => e.VatDate).HasColumnType("datetime");
            entity.Property(e => e.VatJENum).HasDefaultValueSql("((-1))");
            entity.Property(e => e.VatPaid).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatPaidFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatPaidSys).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatPercent).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSumFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSumSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VclPlate).HasMaxLength(20);
            entity.Property(e => e.VersionNum).HasMaxLength(13);
            entity.Property(e => e.Volume).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTApplied).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTAppliedF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTAppliedS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTDetails).HasMaxLength(100);
            entity.Property(e => e.WTSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTSumFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WTSumSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WddStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('-')")
                .IsFixedLength();
            entity.Property(e => e.Weight).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.draftKey).HasDefaultValueSql("((-1))");
            entity.Property(e => e.isCrin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.isIns)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.selfInv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.submitted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
        });

        modelBuilder.Entity<RDR1>(entity =>
        {
            entity.HasKey(e => new { e.DocEntry, e.LineNum }).HasName("RDR1_PRIMARY");

            entity.HasIndex(e => e.AcctCode, "RDR1_ACCOUNT");

            entity.HasIndex(e => new { e.BaseEntry, e.BaseType, e.BaseLine }, "RDR1_BASE_ENTRY");

            entity.HasIndex(e => e.Currency, "RDR1_CURRENCY");

            entity.HasIndex(e => new { e.ItemCode, e.WhsCode, e.OpenQty }, "RDR1_ITM_WHS_OQ");

            entity.HasIndex(e => new { e.ItemCode, e.WhsCode, e.ShipDate }, "RDR1_ITM_WHS_SH");

            entity.HasIndex(e => e.OwnerCode, "RDR1_OWNER_CODE");

            entity.HasIndex(e => e.LineStatus, "RDR1_STATUS");

            entity.HasIndex(e => new { e.DocEntry, e.VisOrder }, "RDR1_VIS_ORDER");

            entity.Property(e => e.AcctCode).HasMaxLength(15);
            entity.Property(e => e.ActDelDate).HasColumnType("datetime");
            entity.Property(e => e.Address).HasMaxLength(254);
            entity.Property(e => e.AllocBinC).HasMaxLength(11);
            entity.Property(e => e.AssblValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BackOrdr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BaseAtCard).HasMaxLength(100);
            entity.Property(e => e.BaseCard).HasMaxLength(15);
            entity.Property(e => e.BaseOpnQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BasePrice)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('E')")
                .IsFixedLength();
            entity.Property(e => e.BaseQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseRef).HasMaxLength(16);
            entity.Property(e => e.BaseType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.BlockNum).HasMaxLength(100);
            entity.Property(e => e.CEECFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength();
            entity.Property(e => e.CFOPCode).HasMaxLength(6);
            entity.Property(e => e.CNJPMan).HasMaxLength(14);
            entity.Property(e => e.CSTCode).HasMaxLength(6);
            entity.Property(e => e.CSTfCOFINS).HasMaxLength(2);
            entity.Property(e => e.CSTfIPI).HasMaxLength(2);
            entity.Property(e => e.CSTfPIS).HasMaxLength(2);
            entity.Property(e => e.CUSplit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ChgAsmBoMW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CiOppLineN).HasDefaultValueSql("((-1))");
            entity.Property(e => e.CodeBars).HasMaxLength(254);
            entity.Property(e => e.CogsAcct).HasMaxLength(15);
            entity.Property(e => e.CogsOcrCo2).HasMaxLength(8);
            entity.Property(e => e.CogsOcrCo3).HasMaxLength(8);
            entity.Property(e => e.CogsOcrCo4).HasMaxLength(8);
            entity.Property(e => e.CogsOcrCo5).HasMaxLength(8);
            entity.Property(e => e.CogsOcrCod).HasMaxLength(8);
            entity.Property(e => e.Commission).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ConsumeFCT)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CountryOrg).HasMaxLength(3);
            entity.Property(e => e.CredOrigin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CtrSealQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Currency).HasMaxLength(3);
            entity.Property(e => e.DIOTNat).HasMaxLength(3);
            entity.Property(e => e.DedVatSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DedVatSumF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DedVatSumS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DefBreak).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DeferrTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DelivrdQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DescOW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DetailsOW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DiscPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DistribExp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DistribIS)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DistribSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocDate).HasColumnType("datetime");
            entity.Property(e => e.DropShip)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Dscription).HasMaxLength(200);
            entity.Property(e => e.DstrbSumFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DstrbSumSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EnSetCost)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EncryptIV).HasMaxLength(100);
            entity.Property(e => e.EquVatPer).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EquVatSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EquVatSumF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EquVatSumS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExLineNo).HasMaxLength(10);
            entity.Property(e => e.Excisable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ExciseAmt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpOpType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ExpType).HasMaxLength(4);
            entity.Property(e => e.ExpUUID).HasMaxLength(50);
            entity.Property(e => e.ExtTaxRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExtTaxSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExtTaxSumF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExtTaxSumS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Factor1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Factor2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Factor3).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Factor4).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FisrtBin).HasMaxLength(228);
            entity.Property(e => e.Flags).HasDefaultValueSql("((0))");
            entity.Property(e => e.FreeChrgBP)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.FreeTxt).HasMaxLength(100);
            entity.Property(e => e.FromWhsCod).HasMaxLength(8);
            entity.Property(e => e.GPBefDisc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GPTtlBasPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GTotal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GTotalFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GTotalSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrossBuyPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrssProfFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrssProfSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrssProfit).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Height1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Height2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.INMPrice).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ISDistrb).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ISDistrbFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ISDistrbSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ISDtCryImp).HasMaxLength(3);
            entity.Property(e => e.ISOrCryExp).HasMaxLength(3);
            entity.Property(e => e.ImportLog).HasMaxLength(20);
            entity.Property(e => e.Incoterms).HasDefaultValueSql("((0))");
            entity.Property(e => e.IndEscala)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.InvQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.InvQtyOnly)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.InvntSttus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.IsAqcuistn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsByPrdct)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsCstmAct)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsPrscGood)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ItemType).HasDefaultValueSql("((4))");
            entity.Property(e => e.ItmTaxType).HasMaxLength(2);
            entity.Property(e => e.LegalTCD).HasMaxLength(250);
            entity.Property(e => e.LegalTIMD).HasMaxLength(250);
            entity.Property(e => e.LegalTTCA).HasMaxLength(250);
            entity.Property(e => e.LegalTW).HasMaxLength(250);
            entity.Property(e => e.LegalText).HasMaxLength(254);
            entity.Property(e => e.Length1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LicTradNum).HasMaxLength(32);
            entity.Property(e => e.LinManClsd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.LinePoPrss)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.LineStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.LineTotal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LineType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('R')")
                .IsFixedLength();
            entity.Property(e => e.LineVat).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LineVatS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LineVatlF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LineVendor).HasMaxLength(15);
            entity.Property(e => e.LnExcised)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LogInstanc).HasDefaultValueSql("((0))");
            entity.Property(e => e.LstBINMPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LstByDsFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LstByDsSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LstByDsSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MYFtype).HasMaxLength(2);
            entity.Property(e => e.NCMCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NVECode).HasMaxLength(6);
            entity.Property(e => e.NeedQty)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.NoInvtryMv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.NumPerMsr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NumPerMsr2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ObjType)
                .HasMaxLength(20)
                .HasDefaultValueSql("('17')");
            entity.Property(e => e.OcrCode).HasMaxLength(8);
            entity.Property(e => e.OcrCode2).HasMaxLength(8);
            entity.Property(e => e.OcrCode3).HasMaxLength(8);
            entity.Property(e => e.OcrCode4).HasMaxLength(8);
            entity.Property(e => e.OcrCode5).HasMaxLength(8);
            entity.Property(e => e.OpenCreQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenInvQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenRtnQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenSumFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenSumSys).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OrderedQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OrigItem).HasMaxLength(50);
            entity.Property(e => e.PQTReqDate).HasColumnType("datetime");
            entity.Property(e => e.PQTReqQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PackQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PartRetire)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PcDocType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.PcQuantity).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PickOty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PickStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PoNum).HasMaxLength(20);
            entity.Property(e => e.PoTrgEntry).HasMaxLength(11);
            entity.Property(e => e.PostTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PriceAfVAT).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PriceBefDi).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PriceEdit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Project).HasMaxLength(20);
            entity.Property(e => e.QtyToShip).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Quantity).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Rate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ReleasQtty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetCost).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetirAPCFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetirAPCSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetireAPC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetireQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ReturnAct).HasDefaultValueSql("((-1))");
            entity.Property(e => e.ReturnRsn).HasDefaultValueSql("((-1))");
            entity.Property(e => e.RevCharge)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SWW).HasMaxLength(16);
            entity.Property(e => e.SerialNum).HasMaxLength(17);
            entity.Property(e => e.ShipDate).HasColumnType("datetime");
            entity.Property(e => e.ShipFromCo).HasMaxLength(50);
            entity.Property(e => e.ShipFromDe).HasMaxLength(254);
            entity.Property(e => e.ShipToCode).HasMaxLength(50);
            entity.Property(e => e.ShipToDesc).HasMaxLength(254);
            entity.Property(e => e.Shortages).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SlpCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.SpecPrice)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.StckAppD).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckAppDFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckAppDSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckAppFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckAppSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckDstFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckDstSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckDstSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckINMPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckSumApp).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StgDesc).HasMaxLength(100);
            entity.Property(e => e.StockPrice).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StockSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StockSumFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StockSumSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StockValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SubCatNum).HasMaxLength(50);
            entity.Property(e => e.Surpluses).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TargetType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.TaxAmtSrc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength();
            entity.Property(e => e.TaxCode).HasMaxLength(8);
            entity.Property(e => e.TaxDistSFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxDistSSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxDistSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnly)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TaxPerUnit).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxRelev)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.TaxStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TaxType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Text).HasColumnType("ntext");
            entity.Property(e => e.ThirdParty)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ToDiff).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ToStock).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TotInclTax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TotalFrgn).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TotalSumSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TranType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TransMod).HasDefaultValueSql("((0))");
            entity.Property(e => e.TreeType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TrnsCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.UFFiscBene).HasMaxLength(10);
            entity.Property(e => e.U_API_Address).HasColumnType("ntext");
            entity.Property(e => e.U_API_TIN).HasMaxLength(30);
            entity.Property(e => e.U_API_Vendor).HasMaxLength(100);
            entity.Property(e => e.U_Bir_validated).HasMaxLength(10);
            entity.Property(e => e.U_DateValid).HasColumnType("datetime");
            entity.Property(e => e.U_Disc1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_Disc2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_Disc3).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_Disc4).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_Disc5).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_InvyUoM).HasMaxLength(15);
            entity.Property(e => e.U_ORNo).HasMaxLength(25);
            entity.Property(e => e.U_Ref).HasMaxLength(10);
            entity.Property(e => e.U_TransType).HasMaxLength(30);
            entity.Property(e => e.U_WtaxCode).HasMaxLength(10);
            entity.Property(e => e.U_bir_tax_grp).HasMaxLength(10);
            entity.Property(e => e.U_birvalid).HasMaxLength(10);
            entity.Property(e => e.UomCode).HasMaxLength(20);
            entity.Property(e => e.UomCode2).HasMaxLength(20);
            entity.Property(e => e.UomEntry).HasDefaultValueSql("((0))");
            entity.Property(e => e.UomEntry2).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdInvntry)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.UseBaseUn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.VatAppld).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatAppldFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatAppldSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatDscntPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatGroup).HasMaxLength(8);
            entity.Property(e => e.VatGrpSrc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.VatPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSumFrgn).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSumSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatWoDpm).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatWoDpmFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatWoDpmSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VendorNum).HasMaxLength(50);
            entity.Property(e => e.Volume).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Weight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Weight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WhsCode).HasMaxLength(8);
            entity.Property(e => e.Width1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Width2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WtCalced)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.WtLiable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.isSrvCall)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.length2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.unitMsr).HasMaxLength(100);
            entity.Property(e => e.unitMsr2).HasMaxLength(100);
        });

        modelBuilder.Entity<WTR1>(entity =>
        {
            entity.HasKey(e => new { e.DocEntry, e.LineNum }).HasName("WTR1_PRIMARY");

            entity.HasIndex(e => e.AcctCode, "WTR1_ACCOUNT");

            entity.HasIndex(e => new { e.BaseEntry, e.BaseType, e.BaseLine }, "WTR1_BASE_ENTRY");

            entity.HasIndex(e => e.Currency, "WTR1_CURRENCY");

            entity.HasIndex(e => new { e.ItemCode, e.WhsCode, e.OpenQty }, "WTR1_ITM_WHS_OQ");

            entity.HasIndex(e => e.OwnerCode, "WTR1_OWNER_CODE");

            entity.HasIndex(e => e.LineStatus, "WTR1_STATUS");

            entity.HasIndex(e => new { e.DocEntry, e.VisOrder }, "WTR1_VIS_ORDER");

            entity.Property(e => e.AcctCode).HasMaxLength(15);
            entity.Property(e => e.ActDelDate).HasColumnType("datetime");
            entity.Property(e => e.Address).HasMaxLength(254);
            entity.Property(e => e.AllocBinC).HasMaxLength(11);
            entity.Property(e => e.AssblValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BackOrdr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BaseAtCard).HasMaxLength(100);
            entity.Property(e => e.BaseCard).HasMaxLength(15);
            entity.Property(e => e.BaseOpnQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BasePrice)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('E')")
                .IsFixedLength();
            entity.Property(e => e.BaseQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.BaseRef).HasMaxLength(16);
            entity.Property(e => e.BaseType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.BlockNum).HasMaxLength(100);
            entity.Property(e => e.CEECFlag)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength();
            entity.Property(e => e.CFOPCode).HasMaxLength(6);
            entity.Property(e => e.CNJPMan).HasMaxLength(14);
            entity.Property(e => e.CSTCode).HasMaxLength(6);
            entity.Property(e => e.CSTfCOFINS).HasMaxLength(2);
            entity.Property(e => e.CSTfIPI).HasMaxLength(2);
            entity.Property(e => e.CSTfPIS).HasMaxLength(2);
            entity.Property(e => e.CUSplit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ChgAsmBoMW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CiOppLineN).HasDefaultValueSql("((-1))");
            entity.Property(e => e.CodeBars).HasMaxLength(254);
            entity.Property(e => e.CogsAcct).HasMaxLength(15);
            entity.Property(e => e.CogsOcrCo2).HasMaxLength(8);
            entity.Property(e => e.CogsOcrCo3).HasMaxLength(8);
            entity.Property(e => e.CogsOcrCo4).HasMaxLength(8);
            entity.Property(e => e.CogsOcrCo5).HasMaxLength(8);
            entity.Property(e => e.CogsOcrCod).HasMaxLength(8);
            entity.Property(e => e.Commission).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ConsumeFCT)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CountryOrg).HasMaxLength(3);
            entity.Property(e => e.CredOrigin)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CtrSealQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Currency).HasMaxLength(3);
            entity.Property(e => e.DIOTNat).HasMaxLength(3);
            entity.Property(e => e.DedVatSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DedVatSumF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DedVatSumS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DefBreak).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DeferrTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DelivrdQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DescOW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DetailsOW)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DiscPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DistribExp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DistribIS)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.DistribSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DocDate).HasColumnType("datetime");
            entity.Property(e => e.DropShip)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Dscription).HasMaxLength(200);
            entity.Property(e => e.DstrbSumFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.DstrbSumSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EnSetCost)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.EncryptIV).HasMaxLength(100);
            entity.Property(e => e.EquVatPer).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EquVatSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EquVatSumF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.EquVatSumS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExLineNo).HasMaxLength(10);
            entity.Property(e => e.Excisable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ExciseAmt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExpOpType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ExpType).HasMaxLength(4);
            entity.Property(e => e.ExpUUID).HasMaxLength(50);
            entity.Property(e => e.ExtTaxRate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExtTaxSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExtTaxSumF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ExtTaxSumS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Factor1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Factor2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Factor3).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Factor4).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.FisrtBin).HasMaxLength(228);
            entity.Property(e => e.Flags).HasDefaultValueSql("((0))");
            entity.Property(e => e.FreeChrgBP)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.FreeTxt).HasMaxLength(100);
            entity.Property(e => e.FromWhsCod).HasMaxLength(8);
            entity.Property(e => e.GPBefDisc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GPTtlBasPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GTotal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GTotalFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GTotalSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrossBuyPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrssProfFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrssProfSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.GrssProfit).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Height1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Height2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.INMPrice).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ISDistrb).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ISDistrbFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ISDistrbSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ISDtCryImp).HasMaxLength(3);
            entity.Property(e => e.ISOrCryExp).HasMaxLength(3);
            entity.Property(e => e.ImportLog).HasMaxLength(20);
            entity.Property(e => e.Incoterms).HasDefaultValueSql("((0))");
            entity.Property(e => e.IndEscala)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.InvQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.InvQtyOnly)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.InvntSttus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.IsAqcuistn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsByPrdct)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsCstmAct)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.IsPrscGood)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ItemType).HasDefaultValueSql("((4))");
            entity.Property(e => e.ItmTaxType).HasMaxLength(2);
            entity.Property(e => e.LegalTCD).HasMaxLength(250);
            entity.Property(e => e.LegalTIMD).HasMaxLength(250);
            entity.Property(e => e.LegalTTCA).HasMaxLength(250);
            entity.Property(e => e.LegalTW).HasMaxLength(250);
            entity.Property(e => e.LegalText).HasMaxLength(254);
            entity.Property(e => e.Length1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LicTradNum).HasMaxLength(32);
            entity.Property(e => e.LinManClsd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.LinePoPrss)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.LineStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('O')")
                .IsFixedLength();
            entity.Property(e => e.LineTotal).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LineType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('R')")
                .IsFixedLength();
            entity.Property(e => e.LineVat).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LineVatS).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LineVatlF).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LineVendor).HasMaxLength(15);
            entity.Property(e => e.LnExcised)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LogInstanc).HasDefaultValueSql("((0))");
            entity.Property(e => e.LstBINMPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LstByDsFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LstByDsSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.LstByDsSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.MYFtype).HasMaxLength(2);
            entity.Property(e => e.NCMCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.NVECode).HasMaxLength(6);
            entity.Property(e => e.NeedQty)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.NoInvtryMv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.NumPerMsr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.NumPerMsr2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ObjType)
                .HasMaxLength(20)
                .HasDefaultValueSql("('67')");
            entity.Property(e => e.OcrCode).HasMaxLength(8);
            entity.Property(e => e.OcrCode2).HasMaxLength(8);
            entity.Property(e => e.OcrCode3).HasMaxLength(8);
            entity.Property(e => e.OcrCode4).HasMaxLength(8);
            entity.Property(e => e.OcrCode5).HasMaxLength(8);
            entity.Property(e => e.OpenCreQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenInvQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenRtnQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenSumFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OpenSumSys).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OrderedQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.OrigItem).HasMaxLength(50);
            entity.Property(e => e.PQTReqDate).HasColumnType("datetime");
            entity.Property(e => e.PQTReqQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PackQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PartRetire)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PcDocType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.PcQuantity).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PickOty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PickStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.PoNum).HasMaxLength(20);
            entity.Property(e => e.PoTrgEntry).HasMaxLength(11);
            entity.Property(e => e.PostTax)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.Price).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PriceAfVAT).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PriceBefDi).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.PriceEdit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.Project).HasMaxLength(20);
            entity.Property(e => e.QtyToShip).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Quantity).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Rate).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ReleasQtty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetCost).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetirAPCFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetirAPCSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetireAPC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.RetireQty).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ReturnAct).HasDefaultValueSql("((-1))");
            entity.Property(e => e.ReturnRsn).HasDefaultValueSql("((-1))");
            entity.Property(e => e.RevCharge)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.SWW).HasMaxLength(16);
            entity.Property(e => e.SerialNum).HasMaxLength(17);
            entity.Property(e => e.ShipDate).HasColumnType("datetime");
            entity.Property(e => e.ShipFromCo).HasMaxLength(50);
            entity.Property(e => e.ShipFromDe).HasMaxLength(254);
            entity.Property(e => e.ShipToCode).HasMaxLength(50);
            entity.Property(e => e.ShipToDesc).HasMaxLength(254);
            entity.Property(e => e.Shortages).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SlpCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.SpecPrice)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.StckAppD).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckAppDFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckAppDSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckAppFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckAppSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckDstFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckDstSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckDstSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckINMPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StckSumApp).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StgDesc).HasMaxLength(100);
            entity.Property(e => e.StockPrice).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StockSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StockSumFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StockSumSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.StockValue).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.SubCatNum).HasMaxLength(50);
            entity.Property(e => e.Surpluses).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TargetType).HasDefaultValueSql("((-1))");
            entity.Property(e => e.TaxAmtSrc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('S')")
                .IsFixedLength();
            entity.Property(e => e.TaxCode).HasMaxLength(8);
            entity.Property(e => e.TaxDistSFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxDistSSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxDistSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxOnly)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TaxPerUnit).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TaxRelev)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.TaxStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TaxType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Text).HasColumnType("ntext");
            entity.Property(e => e.ThirdParty)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.ToDiff).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.ToStock).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TotInclTax).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TotalFrgn).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TotalSumSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.TranType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TransMod).HasDefaultValueSql("((0))");
            entity.Property(e => e.TreeType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.TrnsCode).HasDefaultValueSql("((-1))");
            entity.Property(e => e.UFFiscBene).HasMaxLength(10);
            entity.Property(e => e.U_API_Address).HasColumnType("ntext");
            entity.Property(e => e.U_API_TIN).HasMaxLength(30);
            entity.Property(e => e.U_API_Vendor).HasMaxLength(100);
            entity.Property(e => e.U_Bir_validated).HasMaxLength(10);
            entity.Property(e => e.U_DateValid).HasColumnType("datetime");
            entity.Property(e => e.U_Disc1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_Disc2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_Disc3).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_Disc4).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_Disc5).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.U_InvyUoM).HasMaxLength(15);
            entity.Property(e => e.U_ORNo).HasMaxLength(25);
            entity.Property(e => e.U_Ref).HasMaxLength(10);
            entity.Property(e => e.U_TransType).HasMaxLength(30);
            entity.Property(e => e.U_WtaxCode).HasMaxLength(10);
            entity.Property(e => e.U_bir_tax_grp).HasMaxLength(10);
            entity.Property(e => e.U_birvalid).HasMaxLength(10);
            entity.Property(e => e.UomCode).HasMaxLength(20);
            entity.Property(e => e.UomCode2).HasMaxLength(20);
            entity.Property(e => e.UomEntry).HasDefaultValueSql("((0))");
            entity.Property(e => e.UomEntry2).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdInvntry)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.UseBaseUn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('Y')")
                .IsFixedLength();
            entity.Property(e => e.VatAppld).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatAppldFC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatAppldSC).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatDscntPr).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatGroup).HasMaxLength(8);
            entity.Property(e => e.VatGrpSrc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.VatPrcnt).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSum).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSumFrgn).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatSumSy).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatWoDpm).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatWoDpmFc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VatWoDpmSc).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.VendorNum).HasMaxLength(50);
            entity.Property(e => e.Volume).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Weight1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Weight2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WhsCode).HasMaxLength(8);
            entity.Property(e => e.Width1).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.Width2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.WtCalced)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.WtLiable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.isSrvCall)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("('N')")
                .IsFixedLength();
            entity.Property(e => e.length2).HasColumnType("numeric(19, 6)");
            entity.Property(e => e.unitMsr).HasMaxLength(100);
            entity.Property(e => e.unitMsr2).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
