using System;
using System.Data;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.SQL
{
	/// <summary>
	/// Summary description for SqlGuidKeyFixture.
	/// </summary>
	[TestFixture]
	public class SqlGuidKeyFixture
	{
		Employees obj = new Employees();
		Guid primaryKey = Guid.Empty;

		[TestFixtureSetUp]
		public void Init()
		{
			obj.DatabaseInstanceName = "SQLUnitTests";
		}
	
		[SetUp]
		public void Init2()
		{
			obj.FlushData();
		}
		
		[Test]
		public void Proc2LoadByPrimaryKey()
		{
			obj.DefaultCommandType = CommandType.StoredProcedure;
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, obj.RowCount);
		}
	
		[Test]
		public void Proc1Insert()
		{
			obj.DefaultCommandType = CommandType.StoredProcedure;
			
			obj.AddNew();
			obj.s_BirthDate = "01/01/1980";
			obj.s_City = "Springboro";
			obj.s_Country = "USA";
			obj.s_FirstName = "Matthew";
			obj.HireDate = DateTime.Now;
			obj.s_LastName = "Noonan";
			obj.s_PostalCode = "45066";
			obj.s_Region = "OH";
			obj.s_Title = "President";
			obj.s_TitleOfCourtesy = "Mr.";
			obj.Save();

			Assert.IsFalse(obj.IsColumnNull(EmployeesSchema.EmployeeID.FieldName));
			primaryKey = obj.EmployeeID;

			obj.GetChanges();
			Assert.AreEqual(0, obj.RowCount);
		}
	
		[Test]
		public void Proc3Update()
		{
			obj.DefaultCommandType = CommandType.StoredProcedure;
			
			Assert.IsTrue(obj.LoadAll());
			Assert.AreEqual(1, obj.RowCount);

			obj.s_PostalCode = "45387";
			obj.s_FirstName = "Michael";
			obj.Save();
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, obj.RowCount);
			Assert.AreEqual("45387", obj.s_PostalCode);
		}
	
		[Test]
		public void Proc4Delete()
		{
			obj.DefaultCommandType = CommandType.StoredProcedure;
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			obj.DeleteAll();
			obj.Save();

			Assert.IsFalse(obj.LoadByPrimaryKey(primaryKey));
		}
		
		[Test]
		public void Dynamic2LoadByPrimaryKey()
		{
			obj.DefaultCommandType = CommandType.Text;
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, obj.RowCount);
		}
	
		[Test]
		public void Dynamic1Insert()
		{
			obj.DefaultCommandType = CommandType.Text;
			
			obj.AddNew();
			obj.s_BirthDate = "01/01/1980";
			obj.s_City = "Springboro";
			obj.s_Country = "USA";
			obj.s_FirstName = "Matthew";
			obj.HireDate = DateTime.Now;
			obj.s_LastName = "Noonan";
			obj.s_PostalCode = "45066";
			obj.s_Region = "OH";
			obj.s_Title = "President";
			obj.s_TitleOfCourtesy = "Mr.";
			obj.Save();

			Assert.IsFalse(obj.IsColumnNull(EmployeesSchema.EmployeeID.FieldName));
			primaryKey = obj.EmployeeID;

			obj.GetChanges();
			Assert.AreEqual(0, obj.RowCount);
		}
	
		[Test]
		public void Dynamic3Update()
		{
			obj.DefaultCommandType = CommandType.Text;
			
			Assert.IsTrue(obj.LoadAll());
			Assert.AreEqual(1, obj.RowCount);

			obj.s_PostalCode = "45387";
			obj.s_FirstName = "Michael";
			obj.Save();
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, obj.RowCount);
			Assert.AreEqual("45387", obj.s_PostalCode);
		}
	
		[Test]
		public void Dynamic4Delete()
		{
			obj.DefaultCommandType = CommandType.Text;
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			obj.DeleteAll();
			obj.Save();

			Assert.IsFalse(obj.LoadByPrimaryKey(primaryKey));
		}
	}
}
