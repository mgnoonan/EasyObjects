using System;
using System.Data;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.SQL
{
	/// <summary>
	/// Summary description for SqlTinyIntKeyFixture.
	/// </summary>
	[TestFixture]
	public class SqlTinyIntKeyFixture
	{
		CivilStatus obj = new CivilStatus();
		byte primaryKey = 0;

		[TestFixtureSetUp]
		public void Init()
		{
			obj.DatabaseInstanceName = "SQLUnitTests";
			obj.ResetTable();
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
			obj.s_CivilStatusEN = "Hello";
			obj.s_CivilStatusES = "Hola";
			obj.s_CivilStatusFR = "Bonjour";
			obj.Save();

			Assert.IsFalse(obj.IsColumnNull(CivilStatusSchema.CivilStatusID.FieldName));
			primaryKey = obj.CivilStatusID;

			obj.GetChanges();
			Assert.AreEqual(0, obj.RowCount);
		}
	
		[Test]
		public void Proc3Update()
		{
			obj.DefaultCommandType = CommandType.StoredProcedure;
			
			Assert.IsTrue(obj.LoadAll());
			Assert.AreEqual(1, obj.RowCount);

			obj.s_CivilStatusES = "Buenos dias";
			obj.Save();
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, obj.RowCount);
			Assert.AreEqual("Buenos dias", obj.s_CivilStatusES);
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
			obj.s_CivilStatusEN = "Hello";
			obj.s_CivilStatusES = "Hola";
			obj.s_CivilStatusFR = "Bonjour";
			obj.Save();

			Assert.IsFalse(obj.IsColumnNull(CivilStatusSchema.CivilStatusID.FieldName));
			primaryKey = obj.CivilStatusID;

			obj.GetChanges();
			Assert.AreEqual(0, obj.RowCount);
		}
	
		[Test]
		public void Dynamic3Update()
		{
			obj.DefaultCommandType = CommandType.Text;
			
			Assert.IsTrue(obj.LoadAll());
			Assert.AreEqual(1, obj.RowCount);

			obj.s_CivilStatusES = "Buenos dias";
			obj.Save();
			
			Assert.IsTrue(obj.LoadByPrimaryKey(primaryKey));
			Assert.AreEqual(1, obj.RowCount);
			Assert.AreEqual("Buenos dias", obj.s_CivilStatusES);
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
