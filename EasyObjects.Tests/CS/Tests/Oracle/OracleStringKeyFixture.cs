using System;
using System.Data;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.Oracle
{
	/// <summary>
	/// Summary description for OracleStringKeyFixture.
	/// </summary>
	[TestFixture]
	public class OracleStringKeyFixture
	{
		Customers obj = new Customers();
		string primaryKey = "EZOBJ";

		[TestFixtureSetUp]
		public void Init()
		{
			obj.DatabaseInstanceName = "OracleUnitTests";
			obj.DynamicQueryInstanceName = "Default Oracle";
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
			obj.s_CUSTOMERID = primaryKey;
			obj.s_COMPANYNAME = "Noonan Consulting Inc.";
			obj.s_CONTACTNAME = "Matthew Noonan";
			obj.s_CONTACTTITLE = "President";
			obj.s_CITY = "Springboro";
			obj.s_REGION = "OH";
			obj.s_POSTALCODE = "45066";
			obj.s_COUNTRY = "USA";
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

			obj.s_POSTALCODE = "45387";
			obj.Save();
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, obj.RowCount);
			Assert.AreEqual("45387", obj.s_POSTALCODE);
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
			obj.s_CUSTOMERID = primaryKey;
			obj.s_COMPANYNAME = "Noonan Consulting Inc.";
			obj.s_CONTACTNAME = "Matthew Noonan";
			obj.s_CONTACTTITLE = "President";
			obj.s_CITY = "Springboro";
			obj.s_REGION = "OH";
			obj.s_POSTALCODE = "45066";
			obj.s_COUNTRY = "USA";
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

			obj.s_POSTALCODE = "45387";
			obj.Save();
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, obj.RowCount);
			Assert.AreEqual("45387", obj.s_POSTALCODE);
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
