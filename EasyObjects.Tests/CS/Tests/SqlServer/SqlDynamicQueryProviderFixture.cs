using System;
using System.Data;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.SQL
{
	/// <summary>
	/// Summary description for SqlDynamicQueryProviderFixture.
	/// </summary>
	[TestFixture]
	public class SqlDynamicQueryProviderFixture
	{
		AggregateTest obj = new AggregateTest();
		
		[TestFixtureSetUp]
		public void Init()
		{
			obj.DatabaseInstanceName = "SQLUnitTests";
			obj.DefaultCommandType = CommandType.Text;
			UnitTestBase.RefreshDatabase();
		}
	
		[SetUp]
		public void Init2()
		{
			obj.FlushData();
			obj.Query.ClearAll();
		}
	
		[Test]
		public void BetweenClause()
		{
			obj.Where.HireDate.BetweenBeginValue = "01/01/1999";
			obj.Where.HireDate.BetweenEndValue = "12/31/1999";
			obj.Where.HireDate.Operator = WhereParameter.Operand.Between;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("BetweenClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(6, obj.RowCount);
		}
	
		[Test]
		public void BetweenClauseWithTearoffs()
		{
			obj.Where.HireDate.BetweenBeginValue = "01/01/1999";
			obj.Where.HireDate.BetweenEndValue = "12/31/1999";
			obj.Where.HireDate.Operator = WhereParameter.Operand.Between;

			// Create tearoff
			WhereParameter wp = obj.Where.TearOff.HireDate;
			wp.BetweenBeginValue = "01/01/2001";
			wp.BetweenEndValue = "12/31/2001";
			wp.Operator = WhereParameter.Operand.Between;

			// Create tearoff
			wp = obj.Where.TearOff.HireDate;
			wp.BetweenBeginValue = "01/01/2003";
			wp.BetweenEndValue = "12/31/2003";
			wp.Operator = WhereParameter.Operand.Between;

			Assert.IsTrue(obj.Query.Load("OR"));
			Console.WriteLine("BetweenClauseWithTearoffs: {0}", obj.Query.LastQuery);
			Assert.AreEqual(12, obj.RowCount);
		}
		
		[Test]
		public void EqualClause()
		{
			obj.Where.IsActive.Value = true;
			obj.Where.IsActive.Operator = WhereParameter.Operand.Equal;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("EqualClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(22, obj.RowCount);
		}
		
		[Test]
		public void NotEqualClause()
		{
			obj.Where.IsActive.Value = true;
			obj.Where.IsActive.Operator = WhereParameter.Operand.NotEqual;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("NotEqualClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(8, obj.RowCount);
		}
		
		[Test]
		public void GreaterThanClause()
		{
			obj.Where.Age.Value = 30;
			obj.Where.Age.Operator = WhereParameter.Operand.GreaterThan;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("GreaterThanClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(12, obj.RowCount);
		}
		
		[Test]
		public void GreaterThanOrEqualClause()
		{
			obj.Where.Age.Value = 30;
			obj.Where.Age.Operator = WhereParameter.Operand.GreaterThanOrEqual;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("GreaterThanOrEqualClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(13, obj.RowCount);
		}
		
		[Test]
		public void LessThanClause()
		{
			obj.Where.Age.Value = 30;
			obj.Where.Age.Operator = WhereParameter.Operand.LessThan;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("LessThanClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(12, obj.RowCount);
		}
		
		[Test]
		public void LessThanOrEqualClause()
		{
			obj.Where.Age.Value = 30;
			obj.Where.Age.Operator = WhereParameter.Operand.LessThanOrEqual;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("LessThanOrEqualClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(13, obj.RowCount);
		}
		
		[Test]
		public void LikeClause()
		{
			obj.Where.LastName.Value = "Jo%";
			obj.Where.LastName.Operator = WhereParameter.Operand.Like;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("LikeClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(6, obj.RowCount);
		}
		
		[Test]
		public void StartsWithClause()
		{
			obj.Where.LastName.Value = "J";
			obj.Where.LastName.Operator = WhereParameter.Operand.StartsWith;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("StartsWithClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(6, obj.RowCount);
		}
		
		[Test]
		public void EndsWithClause()
		{
			obj.Where.LastName.Value = "t";
			obj.Where.LastName.Operator = WhereParameter.Operand.EndsWith;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("EndsWithClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(5, obj.RowCount);
		}
		
		[Test]
		public void ContainsClause()
		{
			obj.Where.LastName.Value = "e";
			obj.Where.LastName.Operator = WhereParameter.Operand.Contains;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("ContainsClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(14, obj.RowCount);
		}
		
		[Test]
		public void NotStartsWithClause()
		{
			obj.Where.LastName.Value = "J";
			obj.Where.LastName.Operator = WhereParameter.Operand.NotStartsWith;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("NotStartsWithClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(18, obj.RowCount);
		}
		
		[Test]
		public void NotEndsWithClause()
		{
			obj.Where.LastName.Value = "t";
			obj.Where.LastName.Operator = WhereParameter.Operand.NotEndsWith;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("NotEndsWithClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(19, obj.RowCount);
		}
		
		[Test]
		public void NotContainsClause()
		{
			obj.Where.LastName.Value = "e";
			obj.Where.LastName.Operator = WhereParameter.Operand.NotContains;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("NotContainsClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(10, obj.RowCount);
		}
		
		[Test]
		public void IsNullClause()
		{
			obj.Where.LastName.Operator = WhereParameter.Operand.IsNull;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("IsNullClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(6, obj.RowCount);
		}
		
		[Test]
		public void IsNotNullClause()
		{
			obj.Where.LastName.Operator = WhereParameter.Operand.IsNotNull;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("IsNotNullClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(24, obj.RowCount);
		}
		
		[Test]
		public void InClause()
		{
			obj.Where.ID.Value = "1,2,3,4,5";
			obj.Where.ID.Operator = WhereParameter.Operand.In;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("InClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(5, obj.RowCount);
		}
		
		[Test]
		public void NotInClause()
		{
			obj.Where.ID.Value = "1,2,3,4,5";
			obj.Where.ID.Operator = WhereParameter.Operand.NotIn;

			Assert.IsTrue(obj.Query.Load());
			Console.WriteLine("NotInClause: {0}", obj.Query.LastQuery);
			Assert.AreEqual(25, obj.RowCount);
		}

	}
}
