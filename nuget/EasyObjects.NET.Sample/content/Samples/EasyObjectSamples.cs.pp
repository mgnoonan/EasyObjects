using System;
using System.Data;
using System.Data.Common;

using NCI.EasyObjects;
using NCI.EasyObjects.Samples.BLL;

using Microsoft.Practices.EnterpriseLibrary.Data;
 
namespace ConsoleApplication2.EasyObjects.Samples
{
	public static class EasyObjectSamples 
	{
		public static void RunAll() 
		{
            LoadByPrimaryKey();
            LoadAll();
            DynamicQuery();
            LoadFromSql();
            ChangeQuerySource();
        }
        
		public static void LoadByPrimaryKey()
		{   
		    Employees obj = new Employees();
		
		    // Load a single row via the primary key
		    obj.LoadByPrimaryKey(18);

            // TODO: print the results
        }
		
		public static void LoadAll()
		{   
		    Employees obj = new Employees();
		
		    // Load a single row via the primary key
		    obj.LoadAll();

            // TODO: print the results
        }
		
		public static void DynamicQuery()
		{   
		    Employees emp = new Employees();
		    
		    // Limit the columns returned by the SELECT query
		    emp.Query.AddResultColumn(EmployeesSchema.LastName);
		    emp.Query.AddResultColumn(EmployeesSchema.FirstName);
		    emp.Query.AddResultColumn(EmployeesSchema.City);
		    emp.Query.AddResultColumn(EmployeesSchema.Region);
		    
		    // Add an ORDER BY clause
		    emp.Query.AddOrderBy(EmployeesSchema.LastName);
		    
		    // Add a WHERE clause
		    emp.Where.Region.Value = "WA";
		    
		    // Always call Query.Load() for dynamic queries
		    if (emp.Query.Load())
		    {
		    	// TODO: print the results
		    }
		    else
		    {
		    	// TODO: display error
		    }
		}
		
		public static void LoadFromSql()
		{
            DateTime beginningDate = DateTime.Parse("1996-01-01");
            DateTime endingDate = DateTime.Parse("1997-01-01");
		    Employees emp = new Employees();
			
			// LoadFromSql returns a true for success or false if no records are returned
            if (emp.EmployeeSalesbyCountry(beginningDate, endingDate))
		    {
		    	// TODO: print the results
		    }
		    else
		    {
		    	// TODO: display error
		    }
		}
		
		public static void ChangeQuerySource()
		{   
		    Employees emp = new Employees();
		    emp.QuerySource = "";	// TODO: Fill in the QuerySource
		    
		    // Always call Query.Load() when changing the QuerySource
		    if (emp.Query.Load())
		    {
		    	// TODO: print the results
		    }
		    else
		    {
		    	// TODO: display error
		    }
		}
	}
}