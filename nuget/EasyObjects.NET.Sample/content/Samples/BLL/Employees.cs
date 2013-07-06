using System;
using System.Data;
using System.Data.Common;
using NCI.EasyObjects;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace NCI.EasyObjects.Samples.BLL
{
	/// <summary>
	/// Summary description for Employees.
	/// </summary>
    public class Employees : _Employees
	{
        public Employees()
		{
			this.DatabaseInstanceName = "Northwind";
            this.DefaultCommandType = System.Data.CommandType.Text;
		}
   
	    public virtual bool EmployeeSalesbyCountry (DateTime BeginningDate, DateTime EndingDate)
	    {
	        //  Create the Database object, using the default database service. The
	        //  default database service is determined through configuration.
	        Database db = GetDatabase();
	        
	        string sqlCommand = this.SchemaStoredProcedureWithSeparator + "[Employee Sales by Country]";
	        DbCommand cmd = db.GetStoredProcCommand(sqlCommand);
	        
	        // Add procedure parameters
            db.AddInParameter(cmd, "Beginning_Date", DbType.DateTime, BeginningDate);
            db.AddInParameter(cmd, "Ending_Date", DbType.DateTime, EndingDate);
	    
	        return base.LoadFromSql(cmd);
	    }
    }
}
