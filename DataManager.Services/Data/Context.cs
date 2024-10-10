namespace DataManager.Services.Data;

public class Context : DbContext
{
	public DbSet<Modules> Module { get; set; }
	public DbSet<UserDetails> UserDetail { get; set; }
	public DbSet<UserGroups> UserGroup { get; set; }
	public DbSet<UserLogins> UserLogin { get; set; }
	public DbSet<UserModules> UserModule { get; set; }
	public DbSet<BinMapping> BinMappings { get; set; }
	public DbSet<BinMappingPin> BinMappingPins { get; set; }
	public DbSet<InspectionPlan> InspectionPlans { get; set; }
	public DbSet<InspectionPlanParameter> InspectionPlanParameters { get; set; }
	public DbSet<QCOrder> QCOrders { get; set; }
	public DbSet<QCOrderSampleDetail> QCOrderSampleDetails { get; set; }
	public DbSet<QCOrderSampleList> QCOrderSampleLists { get; set; }
	public DbSet<QCOrderParameterList> QCOrderParameterLists { get; set; }
	public DbSet<QCOrderDosimetryReport> QCOrderDosimetryReports { get; set; }
	public DbSet<BinAssignment> BinAssignments { get; set; }
	public DbSet<CertificateOfIrradiation> CertificateOfIrradiations { get; set; }
	public DbSet<Configurations> Configurations { get; set; }
	public DbSet<ConfigurationItems> ConfigurationItems { get; set; }

	public Context(DbContextOptions<Context> options) : base(options)
	{
	}
	//private void ConfigureProduct(EntityTypeBuilder<UserLogins> builder)
	//{
	//    builder.HasOne<UserLogins>(me => me.UserGroup)
	//        .WithMany(parent => parent.UserLogins)
	//        .HasForeignKey(me => me.UserGroupId)
	//        .HasConstraintName("FK_UserGroups_To_UserLogins_UserGroupId");

	//}

	//private void ConfigureProduct(EntityTypeBuilder<UserGroups> builder)
	//{
	//    builder.HasForeignKey<UserGroups>(o => o.UserGroupId);

	//}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<UserLogins>()
			.HasOne(login => login.UserGroup)
			.WithMany(group => group.UserLogins)
			.HasForeignKey(login => login.UserGroupId);

		modelBuilder.Entity<UserDetails>()
			.HasOne(detail => detail.UserLogins)
			.WithOne(login => login.UserDetails)
			.HasForeignKey<UserDetails>(detail => detail.UserId)
			.IsRequired();

		modelBuilder.Entity<UserModules>()
			.HasOne(module => module.UserGroups)
			.WithMany(auth => auth.UserModules)
			.HasForeignKey(module => module.UserGroupId)
			.IsRequired();

		modelBuilder.Entity<UserModules>()
			.HasOne(module => module.Modules)
			.WithMany(modules => modules.UserModules)
			.HasForeignKey(module => module.ModuleId)
			.IsRequired();

		modelBuilder.Entity<BinMapping>()
			.HasKey(e => new { e.WarehouseCode, e.Shelf });

		modelBuilder.Entity<BinMappingPin>()
			.HasOne(pin => pin.BinMapping)
			.WithMany(mapping => mapping.BinMappingPins)
			.HasForeignKey(pin => new { pin.WarehouseCode, pin.Shelf });

		modelBuilder.Entity<InspectionPlan>()
			.HasKey(e => new { e.InspectionPlanCode, e.Version });

		modelBuilder.Entity<InspectionPlanParameter>()
			.HasOne(param => param.InspectionPlan)
			.WithMany(plan => plan.ParameterList)
			.HasForeignKey(param => new { param.InspectionPlanCode, param.Version });

		modelBuilder.Entity<QCOrder>()
			.HasKey(e => new { e.QCOrderNo });

		modelBuilder.Entity<QCOrderSampleDetail>()
			.HasOne(sampleDetail => sampleDetail.QCOrder)
			.WithOne(e => e.QCOrderSampleDetail)
			.HasForeignKey<QCOrderSampleDetail>(sampleDetail => sampleDetail.QCOrderNo)
			.IsRequired();

		modelBuilder.Entity<QCOrderSampleList>()
			.HasOne(sampleList => sampleList.QCOrderSampleDetail)
			.WithMany(sampleDetail => sampleDetail.QCOrderSampleList)
			.HasForeignKey(sampleList => new { sampleList.SampleId });

		modelBuilder.Entity<QCOrderParameterList>()
			.HasOne(parameterList => parameterList.QCOrderSampleList)
			.WithMany(sampleList => sampleList.QCOrderParameterList)
			.HasForeignKey(parameterList => new { parameterList.SampleId });

		modelBuilder.Entity<QCOrderDosimetryReport>()
			.HasOne(dosimetryReport => dosimetryReport.QCOrder)
			.WithOne(e => e.QCOrderDosimetryReport)
			.HasForeignKey<QCOrderDosimetryReport>(dosimetryReport => dosimetryReport.QCOrderNo)
			.IsRequired();

		//modelBuilder.Entity<CertificateOfIrradiation>()
		//	.HasOne(coi => coi.QCOrder)
		//	.WithOne(e => e.CertificateOfIrradiation)
		//	.HasForeignKey<CertificateOfIrradiation>(coi => coi.QCOrderNo)
		//	.IsRequired();

		SeedData(modelBuilder);

	}
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.EnableSensitiveDataLogging();
	}

	//public override int SaveChanges()
	//{
	//	foreach (var entry in ChangeTracker.Entries()
	//		.Where(e => e.State == EntityState.Added && e.Entity.GetType() == typeof(InspectionPlan)))
	//	{

	//		SqlParameter sqlresult = new SqlParameter("@result", System.Data.SqlDbType.Int)
	//		{
	//			Direction = System.Data.ParameterDirection.Output
	//		};
	//		// Get the next value in the sequence
	//		//int nextValue = Database.ExecuteSqlRaw($"SELECT NEXT VALUE FOR dbo.MySequence");
	//		var nextValue = Database.SqlQuery<int>($"SELECT CURRENT_VALUE FROM sys.sequences WHERE name = 'MySequence'").Single();
	//		//int nextValue = Database.SqlQuery<int>($"SELECT NEXT VALUE FOR dbo.MySequence").Single();
	//		//var rawQuery = Database.SqlQuery<int>($"SELECT NEXT VALUE FOR dbo.MySequence");
	//		//var task = rawQuery.SingleAsync();
	//		//int nextValue = task.Result;


	//		// Format the value as a string with the desired prefix and leading zeros
	//		var seriesValue = $"ISI-TIMS-{(nextValue + 1):0000}";
	//		//var seriesValue = $"ISI-TIMS-{nextValue.ToString("D4")}";

	//		if (entry.Property("InspectionPlanCode").CurrentValue.ToString() != "")
	//		{
	//			//Add Version
	//			entry.Property("Version").CurrentValue = (Convert.ToInt32(entry.Property("Version").CurrentValue) + 1).ToString();
	//		}
	//		else
	//		{
	//			// Set the SeriesValue property to the formatted value
	//			entry.Property("InspectionPlanCode").CurrentValue = seriesValue;
	//		}

	//		var inspectionPlan = (InspectionPlan)entry.Entity;

	//		foreach (var childEntry in ChangeTracker.Entries()
	//		.Where(e => e.State == EntityState.Added && e.Entity.GetType() == typeof(InspectionPlanParameter) &&
	//					((InspectionPlanParameter)e.Entity).InspectionPlanCode == inspectionPlan.InspectionPlanCode))
	//		{
	//			// Set properties for child entities
	//			childEntry.Property("Version").CurrentValue = inspectionPlan.Version;
	//			childEntry.Property("InspectionPlanCode").CurrentValue = inspectionPlan.InspectionPlanCode;
	//		}
	//	}

	//	// Save changes to the database
	//	var result = base.SaveChanges();

	//	// Commit the transaction

	//	return result;
	//}

	private void SeedData(ModelBuilder modelBuilder)
	{
		string userId = Guid.NewGuid().ToString("N");

		#region User Group
		var userGroup = (new UserGroups
		{
			UserGroupId = 1,
			GroupName = "Unset",
			IsActive = true,
			CreatedDate = DateTime.UtcNow,
			CreatedUserId = userId
		}, new UserGroups
		{
			UserGroupId = 2,
			GroupName = "Administrator",
			IsActive = true,
			CreatedDate = DateTime.UtcNow,
			CreatedUserId = userId
		});
		//var userGroup = ( new UserGroups
		//{
		//    UserGroupId = 1,
		//    GroupName = "Administrator",
		//    IsActive = true,
		//    CreatedDate = DateTime.UtcNow,
		//    CreatedUserId = userId
		//});
		#endregion

		#region User Logins
		var adminUserLogin = new UserLogins
		{
			UserId = userId,
			Username = "admin",
			Password = Cryption.EncryptPassword("admin", "1234"),
			IsSuperUser = true,
			IsActive = true,
			IsLocked = true,
			UserGroupId = 2,
			FailedAttemptCount = 0,
			IsLockedoutEnabled = true,
			IsTwoFactorEnabled = true,
			CreatedDate = DateTime.UtcNow,
			CreatedUserId = userId

		};
		#endregion

		#region User Details
		var adminUser = new UserDetails
		{
			Id = 1,
			UserId = userId,
			LastName = "Admin",
			FirstName = "Company",
			MiddleName = "",
			IsActive = true,
			Company = "",
			Department = "Administration",
			Email = "admin@gmail.com",
			IsEmailConfirmed = false,
			IsPhoneConfirmed = false,
			CreatedDate = DateTime.UtcNow,
			CreatedUserId = userId
		};
		#endregion

		modelBuilder
			.Entity<UserGroups>().HasData(new UserGroups
			{
				UserGroupId = 1,
				GroupName = "Unset",
				IsActive = true,
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId
			}, new UserGroups
			{
				UserGroupId = 2,
				GroupName = "Administrator",
				IsActive = true,
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId
			});
		modelBuilder
			.Entity<UserLogins>().HasData(adminUserLogin);
		modelBuilder
			.Entity<UserDetails>().HasData(adminUser);

		var moduleIds = new List<string>
		{
			"BLPR", "ILPR", "PLPR", "BNDB", "BNMP", "COAP", "QCMT", "QCOR", "DBNT",
			"DSBD", "DSCD", "ISCD", "RSCD", "DSPT", "INVT", "MNTR", "RCVN", "USRG",
			"USRM", "CNFG", "EBOP", "DPNT", "RCNT"
		};

		foreach (var moduleId in moduleIds)
		{
			modelBuilder
				.Entity<UserModules>()
				.HasData(
					new UserModules
					{
						Id = (moduleIds.IndexOf(moduleId) + 1) * -1,
						UserGroupId = 2,
						ModuleId = moduleId,
						IsReadOnly = true,
						CanCreate = true,
						CanUpdate = true,
						CreatedDate = DateTime.UtcNow,
						CreatedUserId = userId,
						UpdatedDate = null,
						UpdatedUserId = null
					}
				);
		}


		#region Configurations
		modelBuilder
			.Entity<Configurations>().HasData(
			new Configurations { 
				Id = 1,
				Code = "QC_MAINTENANCE",
				Name = "QC Maintenance",
				Sequence = 1
			});
		#endregion

		#region Modules
		modelBuilder.Entity<Modules>().HasData(
			new Modules
			{
				ModuleId = "USRM",
				LineNum = 0,
				Name = "User Management",
				Description = "This is where you can create, update and delete user information.",
				Icon = "-",
				WebLink = "/UserManagement",
				IconGroup = "bx bx-home-circle",
				GroupName = "Administration",
				IconSubGroup = "fa-solid fa-users",
				SubGroupName = "Users",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "USRG",
				LineNum = 1,
				Name = "User Group",
				Description = "This is where you can create, update and delete user group.",
				Icon = "-",
				WebLink = "/UserGroup",
				IconGroup = "bx bx-home-circle",
				GroupName = "Administration",
				IconSubGroup = "fa-solid fa-users",
				SubGroupName = "Users",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "DSBD",
				LineNum = 2,
				Name = "Dashboard",
				Description = "This is where you can view the statistics of sales orders.",
				Icon = "bx bx-home-alt",
				WebLink = "/Home",
				IconGroup = "bx bx-pie-chart-alt-2",
				GroupName = "Dashboard",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "DBNT",
				LineNum = 3,
				Name = "Dashboard Notification",
				Description = "This is where you can view sales orders that are to be received or dispatched.",
				Icon = "bx bx-home-alt",
				WebLink = "/DashboardNotification",
				IconGroup = "bx bx-pie-chart-alt-2",
				GroupName = "Dashboard",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "QCMT",
				LineNum = 4,
				Name = "QC Maintenance",
				Description = "This is where user can create Inspection Plans for specific items.",
				Icon = "bx bx-search-alt",
				WebLink = "/QCMaintenance",
				IconGroup = "bx bx-search-alt",
				GroupName = "Quality Control",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "QCOR",
				LineNum = 5,
				Name = "QC Order",
				Description = "This is where user can input QC Order details.",
				Icon = "bx bx-user-check",
				WebLink = "/QCOrder",
				IconGroup = "bx bx-search-alt",
				GroupName = "Quality Control",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "COAP",
				LineNum = 6,
				Name = "COI Approval",
				Description = "This is where user can approve and print certificate of irradiation.",
				Icon = "fa fa-person-circle-check",
				WebLink = "/CertificateOfIrradiationApproval",
				IconGroup = "bx bx-search-alt",
				GroupName = "Quality Control",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "BNMP",
				LineNum = 7,
				Name = "Bin Mapping",
				Description = "This is where user can see map the bins.",
				Icon = "bx bx-map-pin",
				WebLink = "/BinMapping",
				IconGroup = "bx bx-cabinet",
				GroupName = "Bins",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "BNDB",
				LineNum = 8,
				Name = "Bin Dashboard",
				Description = "This is where user can see the Bin Map and Bin Status.",
				Icon = "bx bx-map-alt",
				WebLink = "/BinDashboard",
				IconGroup = "bx bx-cabinet",
				GroupName = "Bins",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "RSCD",
				LineNum = 9,
				Name = "Receiving Schedule",
				Description = "This is where user can view the Receiving Schedule.",
				Icon = "bx bxs-truck",
				WebLink = "/ReceivingSchedule",
				IconGroup = "bx bx-calendar",
				GroupName = "Schedules",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "DSCD",
				LineNum = 10,
				Name = "Dispatch Schedule",
				Description = "This is where user can view the Dispatch Schedule.",
				Icon = "bx bx-package",
				WebLink = "/DispatchSchedule",
				IconGroup = "bx bx-calendar",
				GroupName = "Schedules",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "ISCD",
				LineNum = 11,
				Name = "Irradiation Schedule",
				Description = "This is where user can view the Irradiation Schedule.",
				Icon = "bx bx-shield",
				WebLink = "/IrradiationSchedule",
				IconGroup = "bx bx-calendar",
				GroupName = "Schedules",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "BLPR",
				LineNum = 12,
				Name = "Bin Label Printing",
				Description = "This is where user can print bin labels.",
				Icon = "bx bx-file",
				WebLink = "/BinLabelPrinting",
				IconGroup = "bx bx-printer",
				GroupName = "Forms and Reports",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "ILPR",
				LineNum = 13,
				Name = "Irradiation Label Printing",
				Description = "This is where user can print irradiation labels.",
				Icon = "bx bx-file",
				WebLink = "/IrradiationLabelPrinting",
				IconGroup = "bx bx-printer",
				GroupName = "Forms and Reports",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "PLPR",
				LineNum = 14,
				Name = "Pallet Label Printing",
				Description = "This is where user can print pallet labels.",
				Icon = "bx bx-file",
				WebLink = "/PalletLabelPrinting",
				IconGroup = "bx bx-printer",
				GroupName = "Forms and Reports",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "RCVN",
				LineNum = 15,
				Name = "Receiving",
				Description = "This is where user can receive sales orders.",
				Icon = "bx bx-archive-in",
				WebLink = "/Receiving",
				IconGroup = "bx bx-archive",
				GroupName = "Inventory",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "INVT",
				LineNum = 16,
				Name = "Inventory Transfer",
				Description = "This is where user can transfer inventory automatically.",
				Icon = "bx bx-transfer",
				WebLink = "/InventoryTransfer",
				IconGroup = "bx bx-archive",
				GroupName = "Inventory",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "DSPT",
				LineNum = 17,
				Name = "Dispatch",
				Description = "This is where user can release items.",
				Icon = "bx bx-archive-out",
				WebLink = "/Dispatch",
				IconGroup = "bx bx-archive",
				GroupName = "Inventory",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "MNTR",
				LineNum = 18,
				Name = "Manual Transfer",
				Description = "This is where user can transfer inventory manually.",
				Icon = "bx bx-transfer",
				WebLink = "/InventoryTransfer/ManualTransfer",
				IconGroup = "bx bx-archive",
				GroupName = "Inventory",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "FRMR",
				LineNum = 19,
				Name = "Forms And Reports",
				Description = "This is where user can print forms and reports.",
				Icon = "bx bx-library",
				WebLink = "/FormsAndReports",
				IconGroup = "bx bx-printer",
				GroupName = "Forms and Reports",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "CNFG",
				LineNum = 20,
				Name = "Configurations",
				Description = "This is where user can adjust system configurations",
				Icon = "bx bx-cog",
				WebLink = "/Configurations",
				IconGroup = "bx bx-home-circle",
				GroupName = "Administration",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "EBOP",
				LineNum = 21,
				Name = "EB Operations",
				Description = "This is where user can view and verify sales orders that are to be irradiated",
				Icon = "bx bxs-radiation",
				WebLink = "/EBOperations",
				IconGroup = "bx bx-search-alt",
				GroupName = "Quality Control",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "DPNT",
				LineNum = 22,
				Name = "Dispatch Notifications",
				Description = "This is where user can view and confirm sales orders that are to be dispatched",
				Icon = "bx bx-export",
				WebLink = "/DispatchNotifications",
				IconGroup = "bx bx-box",
				GroupName = "Dashboard",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			},
			new Modules
			{
				ModuleId = "RCNT",
				LineNum = 23,
				Name = "Receiving Notifications",
				Description = "This is where user can view and confirm sales orders that are to be received",
				Icon = "bx bx-import",
				WebLink = "/ReceivingNotifications",
				IconGroup = "bx bx-box",
				GroupName = "Dashboard",
				IconSubGroup = "-",
				SubGroupName = "-",
				CreatedDate = DateTime.UtcNow,
				CreatedUserId = userId,
				Active = true
			});
		#endregion
	}
}
