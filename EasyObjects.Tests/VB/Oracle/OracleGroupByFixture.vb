Imports System
Imports NUnit.Framework
Imports NCI.EasyObjects
Imports Npgsql

Namespace NCI.EasyObjects.Tests.Oracle

	<TestFixture()> _
	Public Class OracleGroupByFixture

		Dim aggTest As AggregateTest = New AggregateTest

		<TestFixtureSetUp()> _
		Public Sub Init()
			TransactionManager.ThreadTransactionMgrReset()
			aggTest.DatabaseInstanceName = "OracleUnitTests"
		End Sub

		<SetUp()> _
		Public Sub Init2()
			aggTest.FlushData()
		End Sub

		<Test()> _
		Public Sub OneGroupBy()
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(3, aggTest.RowCount)
		End Sub

		<Test()> _
		Public Sub TwoGroupBy()
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID)
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(12, aggTest.RowCount)
		End Sub

		<Test()> _
		Public Sub GroupByWithWhere()
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID)
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal
			aggTest.Where.ISACTIVE.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(5, aggTest.RowCount)
		End Sub


		<Test()> _
		Public Sub GroupByWithWhereAndOrderBy()
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID)
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal
			aggTest.Where.ISACTIVE.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID)
			aggTest.Query.AddOrderBy(AggregateTestSchema.DEPARTMENTID, WhereParameter.Dir.ASC)
			aggTest.Query.AddOrderBy(AggregateTestSchema.ISACTIVE, WhereParameter.Dir.ASC)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(5, aggTest.RowCount)
		End Sub

		<Test()> _
		Public Sub GroupByWithOrderByCountAll()

			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID)
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal
			aggTest.Where.ISACTIVE.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID)
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(5, aggTest.RowCount)
		End Sub

		<Test()> _
		  Public Sub GroupByWithTop()

			aggTest.Query.TopN = 3
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID)
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal
			aggTest.Where.ISACTIVE.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID)
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(aggTest.Query.TopN, aggTest.RowCount)
		End Sub

		<Test()> _
		  Public Sub GroupByWithDistinct()

			aggTest.Query.Distinct = True
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID)
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal
			aggTest.Where.ISACTIVE.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID)
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(5, aggTest.RowCount)
		End Sub

		<Test()> _
		Public Sub GroupByWithTearoff()
			aggTest.Aggregate.SALARY.Function = AggregateParameter.Func.Sum
			aggTest.Aggregate.SALARY.Alias = "Sum"
			Dim ap As AggregateParameter = aggTest.Aggregate.TearOff.SALARY
			ap.Function = AggregateParameter.Func.Min
			ap.Alias = "Min"
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID)
			aggTest.Where.ISACTIVE.Operator = WhereParameter.Operand.Equal
			aggTest.Where.ISACTIVE.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID)
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(5, aggTest.RowCount)
		End Sub

		<Test()> _
		Public Sub GroupByWithRollup()
			aggTest.Query.WithRollup = True
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DEPARTMENTID)
			aggTest.Query.AddGroupBy(AggregateTestSchema.ISACTIVE)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DEPARTMENTID)
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
			aggTest.Query.Load()
		End Sub

	End Class

End Namespace
