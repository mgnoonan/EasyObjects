using System;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.Oracle
{
	[TestFixture]
	public class OracleViewAggregateFixture
	{
		FullNameView obj = new FullNameView();
		
		[TestFixtureSetUp]
		public void Init()
		{
			obj.DatabaseInstanceName = "OracleUnitTests";
			obj.DynamicQueryInstanceName = "Default Oracle";
			UnitTestBase.RefreshDatabase();
		}

		[SetUp]
		public void Init2()
		{
			obj.FlushData();
		}

		[Test]
		public void EmptyQueryReturnsSELLECTAll()
		{
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(16, obj.RowCount);
		}

		[Test]
		public void AddAggregateAvg()
		{
			obj.Aggregate.SALARY.Function = AggregateParameter.Func.Avg;
			obj.Aggregate.SALARY.Alias = "Avg";
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}

		[Test]
		public void AddAggregateCount()
		{
			obj.Aggregate.SALARY.Function = AggregateParameter.Func.Count;
			obj.Aggregate.SALARY.Alias = "Count";
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}

		[Test]
		public void AddAggregateMin()
		{
			obj.Aggregate.SALARY.Function = AggregateParameter.Func.Min;
			obj.Aggregate.SALARY.Alias = "Min";
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}

		[Test]
		public void AddAggregateMax()
		{
			obj.Aggregate.SALARY.Function = AggregateParameter.Func.Max;
			obj.Aggregate.SALARY.Alias = "Max";
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}

		[Test]
		public void AddAggregateSum()
		{
			obj.Aggregate.SALARY.Function = AggregateParameter.Func.Sum;
			obj.Aggregate.SALARY.Alias = "Sum";
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}

		[Test]
		public void AddAggregateStdDev()
		{
			obj.Aggregate.SALARY.Function = AggregateParameter.Func.StdDev;
			obj.Aggregate.SALARY.Alias = "Std Dev";
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}

		[Test]
		public void AddAggregateVar()
		{
			obj.Aggregate.SALARY.Function = AggregateParameter.Func.Var;
			obj.Aggregate.SALARY.Alias = "Var";
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}

		[Test]
		public void AddAggregateCountAll()
		{
			obj.Query.CountAll = true;
			obj.Query.CountAllAlias = "Total";
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}

		[Test]
		public void AddTwoAggregates()
		{
			obj.Query.CountAll = true;
			obj.Query.CountAllAlias = "Total";
			obj.Aggregate.SALARY.Function = AggregateParameter.Func.Sum;
			obj.Aggregate.SALARY.Alias = "Sum";
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}

		[Test]
		public void AggregateWithTearoff()
		{
			obj.Aggregate.SALARY.Function = AggregateParameter.Func.Sum;
			obj.Aggregate.SALARY.Alias = "Sum";
			AggregateParameter ap = obj.Aggregate.TearOff.SALARY;
			ap.Function = AggregateParameter.Func.Min;
			ap.Alias = "Min";
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}

		[Test]
		public void AggregateWithWhere()
		{
			obj.Query.CountAll = true;
			obj.Query.CountAllAlias = "Total";
			obj.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal;
			obj.Where.ISACTIVE.Value = true;
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual(1, obj.RowCount);
		}
		
		[Test]
		public void EmptyAliasUsesColumnName()
		{
			obj.Aggregate.SALARY.Function = AggregateParameter.Func.Sum;
			Assert.IsTrue(obj.Query.Load());
			Assert.AreEqual("SALARY", obj.Aggregate.SALARY.Alias);
		}
		
	}
}
