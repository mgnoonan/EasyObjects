using System;
using System.Data;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.SQL
{
	/// <summary>
	/// Summary description for SqlComputedColumnFixture.
	/// </summary>
	[TestFixture]
	public class SqlComputedColumnFixture
	{
		ComputedTest obj = new ComputedTest();
		int primaryKey = -1;

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
			obj.Integer1 = 1;
			obj.Integer2 = 2;
			obj.s_String1 = "One";
			obj.s_String2 = "Two";
			obj.Save();

			Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.RecordID.FieldName));
			primaryKey = obj.RecordID;

			// Computed columns
			Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.Computed1.FieldName));
			Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.Computed2.FieldName));
			Assert.AreEqual("3", obj.s_Computed1);
			Assert.AreEqual("One Two", obj.s_Computed2);

			obj.GetChanges();
			Assert.AreEqual(0, obj.RowCount);
		}
	
		[Test]
		public void Proc3Update()
		{
			obj.DefaultCommandType = CommandType.StoredProcedure;
			
			Assert.IsTrue(obj.LoadAll());
			Assert.AreEqual(1, obj.RowCount);

			obj.Integer1 = 5;
			obj.Integer2 = 10;
			obj.String1 = "Five";
			obj.String2 = "Ten";
			obj.Save();
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, obj.RowCount);
			Assert.AreEqual(15, obj.Computed1);
			Assert.AreEqual("Five Ten", obj.Computed2);
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
			obj.Integer1 = 1;
			obj.Integer2 = 2;
			obj.s_String1 = "One";
			obj.s_String2 = "Two";
			obj.Save();

			Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.RecordID.FieldName));
			primaryKey = obj.RecordID;

			// Computed columns
			Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.Computed1.FieldName));
			Assert.IsFalse(obj.IsColumnNull(ComputedTestSchema.Computed2.FieldName));
			Assert.AreEqual("3", obj.s_Computed1);
			Assert.AreEqual("One Two", obj.s_Computed2);

			obj.GetChanges();
			Assert.AreEqual(0, obj.RowCount);
		}
	
		[Test]
		public void Dynamic3Update()
		{
			obj.DefaultCommandType = CommandType.Text;
			
			Assert.IsTrue(obj.LoadAll());
			Assert.AreEqual(1, obj.RowCount);

			obj.Integer1 = 5;
			obj.Integer2 = 10;
			obj.String1 = "Five";
			obj.String2 = "Ten";
			obj.Save();
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, obj.RowCount);
			Assert.AreEqual(15, obj.Computed1);
			Assert.AreEqual("Five Ten", obj.Computed2);
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
