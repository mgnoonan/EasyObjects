using System;
using System.Data;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.SQL
{
	/// <summary>
	/// Summary description for SqlCompoundKeyFixture.
	/// </summary>
	[TestFixture]
	public class SqlCompoundKeyFixture
	{
		EmployeeDepartmentHistory obj = new EmployeeDepartmentHistory();
		int primaryKey1 = 1;
		short primaryKey2 = 1;

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
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey1, primaryKey2));
			Assert.AreEqual(1, obj.RowCount);
		}
	
		[Test]
		public void Proc1Insert()
		{
			obj.DefaultCommandType = CommandType.StoredProcedure;
			
			obj.AddNew();
			obj.EmployeeID = primaryKey1;
			obj.DepartmentID = primaryKey2;
			obj.s_StartDate = "01/01/2000";
			obj.ModifiedDate = DateTime.Now;
			obj.Save();

			obj.GetChanges();
			Assert.AreEqual(0, obj.RowCount);
		}
	
		[Test]
		public void Proc3Update()
		{
			obj.DefaultCommandType = CommandType.StoredProcedure;
			
			Assert.IsTrue(obj.LoadAll());
			Assert.AreEqual(1, obj.RowCount);

			obj.s_EndDate = "12/31/2001";
			obj.Save();
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey1, primaryKey2));
			Assert.AreEqual(1, obj.RowCount);
			Assert.AreEqual("12/31/2001", obj.s_EndDate);
		}
	
		[Test]
		public void Proc4Delete()
		{
			obj.DefaultCommandType = CommandType.StoredProcedure;
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey1, primaryKey2));
			obj.DeleteAll();
			obj.Save();

			Assert.IsFalse(obj.LoadByPrimaryKey(primaryKey1, primaryKey2));
		}
		
		[Test]
		public void Dynamic2LoadByPrimaryKey()
		{
			obj.DefaultCommandType = CommandType.Text;
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey1, primaryKey2));
			Assert.AreEqual(1, obj.RowCount);
		}
	
		[Test]
		public void Dynamic1Insert()
		{
			obj.DefaultCommandType = CommandType.Text;
			
			obj.AddNew();
			obj.EmployeeID = primaryKey1;
			obj.DepartmentID = primaryKey2;
			obj.s_StartDate = "01/01/2000";
			obj.ModifiedDate = DateTime.Now;
			obj.Save();

			obj.GetChanges();
			Assert.AreEqual(0, obj.RowCount);
		}
	
		[Test]
		public void Dynamic3Update()
		{
			obj.DefaultCommandType = CommandType.Text;
			
			Assert.IsTrue(obj.LoadAll());
			Assert.AreEqual(1, obj.RowCount);

			obj.s_EndDate = "12/31/2001";
			obj.Save();
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey1, primaryKey2));
			Assert.AreEqual(1, obj.RowCount);
			Assert.AreEqual("12/31/2001", obj.s_EndDate);
		}
	
		[Test]
		public void Dynamic4Delete()
		{
			obj.DefaultCommandType = CommandType.Text;
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey1, primaryKey2));
			obj.DeleteAll();
			obj.Save();

			Assert.IsFalse(obj.LoadByPrimaryKey(primaryKey1, primaryKey2));
		}
	}
}
