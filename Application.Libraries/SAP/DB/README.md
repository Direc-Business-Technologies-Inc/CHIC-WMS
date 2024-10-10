# Scaffolding SAP DB

Using package manager console using the command
(this command should be in one continuous line only when pasting in cmd or package manager console)
```
dotnet ef dbcontext 
scaffold "data source=192.168.2.35;initial catalog=SBOTEST_ISI_20240321;user id=sa;password=Sb1@dbti;MultipleActiveResultSets=True;TrustServerCertificate=True;" 
Microsoft.EntityFrameworkCore.SqlServer 
--output-dir 'SAP/DB/Models' 
--context-dir 'SAP/DB' 
--context-namespace 'Application.Libraries.SAP' 
--context 'SapDb' 
--use-database-names 
--project 'Application.Libraries' 
--no-pluralize
--no-onconfiguring
--no-build
-t "[@FACILITYLOCATION]" 
-t "[@SERVICE_DATA]" 
-t "[@SERVICE_DATA_ROW]" 
-t "[@SERVICE_TYPE]" 
-t ORDR
-t RDR1
-t OBIN
-t OITM
-t OITW
-t OBTN
-t OBTQ
-t OBBQ
-t OWTR
-t WTR1
-t OWHS
-t OITL
-t ITL1
-f
```

When including a UDO (e.g `@FACILITYLOCATION`) in the code above, you must also add a partial class inside the file `UdoTableNameMapping.cs`
and include a class attribute `[Table("@UdoTableName")]`. Example:
```
namespace Application.Libraries.SAP.DB.Models
{
    [Table("@SERVICE_DATA")]
    public partial class @SERVICE_DATA { }

    [Table("@NewUdoOrUdt")]
    public partial class @NewUdoOrUdt { }
}
```