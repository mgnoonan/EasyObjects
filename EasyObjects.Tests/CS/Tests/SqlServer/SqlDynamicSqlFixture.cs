using System;
using System.Data;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.SQL
{
	/// <summary>
	/// Summary description for SqlDynamicSqlFixture.
	/// </summary>
	[TestFixture]
	public class SqlDynamicSqlFixture
	{
		AggregateTest aggTest = new AggregateTest();
		
		[TestFixtureSetUp]
		public void Init()
		{
			aggTest.DatabaseInstanceName = "SQLUnitTests";
			aggTest.DefaultCommandType = CommandType.Text;
			UnitTestBase.RefreshDatabase();
		}
	
		[SetUp]
		public void Init2()
		{
			aggTest.FlushData();
			aggTest.Query.ClearAll();
		}
	
		[Test]
		public void LoadByPrimaryKey()
		{
			Assert.IsTrue(aggTest.Query.Load());
			int primaryKey = aggTest.ID;
			aggTest.FlushData();

			Assert.IsTrue(aggTest.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, aggTest.RowCount);
		}
	
		[Test]
		public void LoadAll()
		{
			Assert.IsTrue(aggTest.LoadAll());

			// NOTE: Only two rows is in the database because the 
			// Delete, Insert and InsertIdentity fixtures run first
			Assert.AreEqual(3, aggTest.RowCount);
		}
	
		[Test]
		public void Insert()
		{
			aggTest.AddNew();
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.IsActive.FieldName));
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.IsActive2.FieldName));
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.DateCreated.FieldName));
			aggTest.s_DepartmentID = "3";
			aggTest.s_FirstName = "John";
			aggTest.s_LastName = "Doe";
			aggTest.s_Age = "30";
			aggTest.s_HireDate = "2000-02-16 00:00:00";
			aggTest.s_Salary = "34.71";
			aggTest.s_IsActive = "true";
			aggTest.s_IsActive2 = "Y";
			aggTest.Save();
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.ID.FieldName));
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.Ts.FieldName));

			aggTest.AddNew();
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.IsActive.FieldName));
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.IsActive2.FieldName));
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.DateCreated.FieldName));
			aggTest.s_DepartmentID = "3";
			aggTest.s_FirstName = "Matt";
			aggTest.s_LastName = "Noonan";
			aggTest.s_Age = "35";
			aggTest.s_HireDate = "2003-01-01 00:00:00";
			aggTest.s_Salary = "65";
			aggTest.s_IsActive = "false";
			aggTest.s_IsActive2 = "N";
			aggTest.Save();
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.ID.FieldName));
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.Ts.FieldName));

			aggTest.GetChanges();
			Assert.AreEqual(0, aggTest.RowCount);
		}
		
		[Test]
		public void InsertIdentity()
		{
			aggTest.AddNew();
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.IsActive.FieldName));
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.IsActive2.FieldName));
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.DateCreated.FieldName));
			aggTest.IdentityInsert = true;
			aggTest.s_ID = "31";
			aggTest.s_DepartmentID = "3";
			aggTest.s_FirstName = "John";
			aggTest.s_LastName = "Doe";
			aggTest.s_Age = "30";
			aggTest.s_HireDate = "2000-02-16 00:00:00";
			aggTest.s_Salary = "34.71";
			aggTest.s_IsActive = "true";
			aggTest.s_IsActive2 = "Y";
			aggTest.Save();
			Assert.AreEqual(aggTest.ErrorMessage, string.Empty);
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.Ts.FieldName));

			aggTest.GetChanges();
			Assert.AreEqual(0, aggTest.RowCount);
		}

		[Test]
		public void Update()
		{
			Assert.IsTrue(aggTest.LoadAll());

			int primaryKey = aggTest.ID;
			aggTest.s_DepartmentID = "3";
			aggTest.s_FirstName = "John";
			aggTest.s_LastName = "Doe";
			aggTest.s_Age = "30";
			aggTest.s_HireDate = "2000-02-16 00:00:00";
			aggTest.s_Salary = "34.71";
			aggTest.s_IsActive = "true";
			aggTest.s_IsActive2 = "Y";
			aggTest.Save();

			aggTest.LoadByPrimaryKey(primaryKey);
			Assert.AreEqual(1, aggTest.RowCount);
			Assert.AreEqual("John", aggTest.s_FirstName);
		}
	
		[Test]
		public void Delete()
		{
			Assert.IsTrue(aggTest.LoadAll());
			aggTest.DeleteAll();
			aggTest.Save();

			Assert.IsFalse(aggTest.LoadAll());
		}
	
		/// <summary>
		/// Test for concurrency exception
		/// </summary>
		/// <remarks>
		/// Depending on whether you test this against SQL 2000 or SQL 2005, they throw
		/// different exceptions. So you will need to change the expected exception below.
		/// 
		/// SQL 2000: System.Data.SqlClient.SqlException
		/// SQL 2005: System.Data.DBConcurrencyException
		/// 
		/// </remarks>
		[Test]
        [ExpectedException(typeof(System.Data.DBConcurrencyException))]
		public void Concurrency()
		{
			int primaryKey = 1;
			Assert.IsTrue(aggTest.LoadByPrimaryKey(primaryKey));
			aggTest.s_HireDate = "2000-02-16 00:00:00";

			AggregateTest obj = new AggregateTest();
			obj.DefaultCommandType = CommandType.Text;
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			obj.HireDate = DateTime.Now;
			obj.Save();

			aggTest.Save();
		}
	}
}
