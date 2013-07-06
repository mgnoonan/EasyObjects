using System;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.SQL
{
	[TestFixture]
	public class SqlViewGroupByFixture
	{
		FullNameView aggTest = new FullNameView();
		
		[TestFixtureSetUp]
		public void Init()
		{
			aggTest.DatabaseInstanceName = "SQLUnitTests";
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
			aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive);
			aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(1, aggTest.RowCount);
		}

		[Test]
		public void TwoGroupBy()
		{
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive);
			aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID);
			aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive);
			aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(7, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithWhere()
		{
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive);
			aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID);
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.IsActive.Value = true;
			aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive);
			aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(7, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithWhereAndOrderBy()
		{
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive);
			aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID);
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.IsActive.Value = true;
			aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive);
			aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID);
			aggTest.Query.AddOrderBy(FullNameViewSchema.DepartmentID, WhereParameter.Dir.ASC);
			aggTest.Query.AddOrderBy(FullNameViewSchema.IsActive, WhereParameter.Dir.ASC);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(7, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithOrderByCountAll()
		{
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive);
			aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID);
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.IsActive.Value = true;
			aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive);
			aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID);
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(7, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithTop()
		{
			aggTest.Query.TopN = 3;
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive);
			aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID);
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.IsActive.Value = true;
			aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive);
			aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID);
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(aggTest.Query.TopN, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithDistinct()
		{
			aggTest.Query.Distinct = true;
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive);
			aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID);
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.IsActive.Value = true;
			aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive);
			aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID);
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(7, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithTearoff()
		{
			aggTest.Aggregate.Salary.Function = AggregateParameter.Func.Sum;
			aggTest.Aggregate.Salary.Alias = "Sum";
			AggregateParameter ap = aggTest.Aggregate.TearOff.Salary;
			ap.Function = AggregateParameter.Func.Min;
			ap.Alias = "Min";
			aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive);
			aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID);
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal;
			aggTest.Where.IsActive.Value = true;
			aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive);
			aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID);
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(7, aggTest.RowCount);
		}

		[Test]
		public void GroupByWithRollup()
		{
			aggTest.Query.WithRollup = true;
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive);
			aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID);
			aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive);
			aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID);
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(9, aggTest.RowCount);
		}

		[Test]
		public void GroupByFullName()
		{
			aggTest.Query.CountAll = true;
			aggTest.Query.CountAllAlias = "Count";
			aggTest.Query.AddResultColumn(FullNameViewSchema.FullName);
			aggTest.Query.AddGroupBy(FullNameViewSchema.FullName);
			Assert.IsTrue(aggTest.Query.Load());
			Assert.AreEqual(17, aggTest.RowCount);
		}

	}
}
