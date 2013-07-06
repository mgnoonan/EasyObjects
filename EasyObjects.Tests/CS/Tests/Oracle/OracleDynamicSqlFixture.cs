using System;
using System.Data;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.Oracle
{
	/// <summary>
	/// Summary description for OracleDynamicSqlFixture.
	/// </summary>
	[TestFixture]
	public class OracleDynamicSqlFixture
	{
		Oracle.AggregateTest aggTest = new Oracle.AggregateTest();
		
		[TestFixtureSetUp]
		public void Init()
		{
			aggTest.DatabaseInstanceName = "OracleUnitTests";
			aggTest.DynamicQueryInstanceName = "Default Oracle";
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
			decimal primaryKey = aggTest.ID;
			aggTest.FlushData();

			Assert.IsTrue(aggTest.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, aggTest.RowCount);
		}
	
		[Test]
		public void LoadAll()
		{
			Assert.IsTrue(aggTest.LoadAll());

			// NOTE: Only one row is in the database because the 
			// Delete and Insert fixtures run first
			Assert.AreEqual(32, aggTest.RowCount);
		}
	
		[Test]
		public void Insert()
		{
			aggTest.AddNew();
			aggTest.s_DEPARTMENTID = "3";
			aggTest.s_FIRSTNAME = "John";
			aggTest.s_LASTNAME = "Doe";
			aggTest.s_AGE = "16";
			aggTest.s_HIREDATE = "2000-02-16 00:00:00";
			aggTest.s_SALARY = "34.71";
			aggTest.s_ISACTIVE = "1";
			aggTest.Save();
			Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.ID.FieldName));

			aggTest.GetChanges();
			Assert.AreEqual(0, aggTest.RowCount);
		}
			
		[Test]
		public void InsertIdentity()
		{
			aggTest.AddNew();
			aggTest.IdentityInsert = true;
			aggTest.s_ID = "31";
			aggTest.s_DEPARTMENTID = "3";
			aggTest.s_FIRSTNAME = "John";
			aggTest.s_LASTNAME = "Doe";
			aggTest.s_AGE = "16";
			aggTest.s_HIREDATE = "2000-02-16 00:00:00";
			aggTest.s_SALARY = "34.71";
			aggTest.s_ISACTIVE = "1";
			aggTest.Save();
			Assert.AreEqual(aggTest.ErrorMessage, string.Empty);
			//Assert.IsFalse(aggTest.IsColumnNull(AggregateTestSchema.Ts.FieldName));

			aggTest.GetChanges();
			Assert.AreEqual(0, aggTest.RowCount);
		}

		[Test]
		public void Update()
		{
			Assert.IsTrue(aggTest.LoadAll());

			decimal primaryKey = aggTest.ID;
			aggTest.s_DEPARTMENTID = "3";
			aggTest.s_FIRSTNAME = "John";
			aggTest.s_LASTNAME = "Doe";
			aggTest.s_AGE = "16";
			aggTest.s_HIREDATE = "2000-02-16 00:00:00";
			aggTest.s_SALARY = "34.71";
			aggTest.s_ISACTIVE = "1";
			aggTest.Save();

			aggTest.LoadByPrimaryKey(primaryKey);
			Assert.AreEqual(1, aggTest.RowCount);
			Assert.AreEqual("John", aggTest.s_FIRSTNAME);
		}
	
		[Test]
		public void XDelete()
		{
			Assert.IsTrue(aggTest.LoadAll());
			aggTest.DeleteAll();
			aggTest.Save();

			Assert.IsFalse(aggTest.LoadAll());
		}
		
		[Test]
		[ExpectedException(typeof(System.Data.OracleClient.OracleException))]
		public void Concurrency()
		{
			int primaryKey = 1;
			Assert.IsTrue(aggTest.LoadByPrimaryKey(primaryKey));
			aggTest.s_HIREDATE = "2000-02-16 00:00:00";

			AggregateTest obj = new AggregateTest();
			obj.DatabaseInstanceName = "OracleUnitTests";
			obj.DynamicQueryInstanceName = "Default Oracle";
			obj.DefaultCommandType = CommandType.Text;
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			obj.HIREDATE = DateTime.Now;
			obj.Save();

			aggTest.Save();
		}
	}
}
