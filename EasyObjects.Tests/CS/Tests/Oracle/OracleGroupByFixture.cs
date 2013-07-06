using System;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.Oracle
{
	[TestFixture]
	public class OracleGroupByFixture
	{
		Oracle.AggregateTest aggTest = new Oracle.AggregateTest();
		
		[TestFixtureSetUp]
		public void Init()
		{
			aggTest.DatabaseInstanceName = "OracleUnitTests";
			aggTest.DynamicQueryInstanceName = "Default Oracle";
			aggTest.DefaultCommandType = System.Data.CommandType.Text;
			UnitTestBase.RefreshDatabase();
		}

		[SetUp]
		public void Init2()
		{
			aggTest.FlushData();
		}

		[Test]
		public void OneGroupBy()
		{
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(3, aggTest.RowCount);
		}

		[Test]
		public void TwoGroupBy()
		{
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID);
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(12, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithWhere()
		{
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID);
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.ISACTIVE.Value = 1;
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(5, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithWhereAndOrderBy()
		{
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID);
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.ISACTIVE.Value = 1;
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID);
			aggTest.Query.AddOrderBy(AggregateTestSchema.DEPARTMENTID, WhereParameter.Dir.ASC);
			aggTest.Query.AddOrderBy(AggregateTestSchema.ISACTIVE, WhereParameter.Dir.ASC);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(5, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithOrderByCountAll()
		{
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID);
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.ISACTIVE.Value = 1;
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID);
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(5, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithTop()
		{
			aggTest.Query.TopN = 3;
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID);
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.ISACTIVE.Value = 1;
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID);
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC);
			Assert.IsTrue(aggTest.Query.Load());
			Console.WriteLine("GroupByWithTop: {0}", aggTest.Query.LastQuery);
			Assert.AreEqual(aggTest.Query.TopN, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithDistinct()
		{
			aggTest.Query.Distinct = true;
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID);
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.ISACTIVE.Value = 1;
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID);
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(5, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithTearoff()
		{
			aggTest.Aggregate.SALARY.Function = AggregateParameter.Func.Sum;
			aggTest.Aggregate.SALARY.Alias = "Sum";
			AggregateParameter ap = aggTest.Aggregate.TearOff.SALARY;
			ap.Function = AggregateParameter.Func.Min;
			ap.Alias = "Min";
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID);
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.ISACTIVE.Value = 1;
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID);
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(5, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithRollup()
		{
			aggTest.Query.WithRollup = true;
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID);
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE);
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID);
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC);
			aggTest.Query.Load();
		}
	}
}
