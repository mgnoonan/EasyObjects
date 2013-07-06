Imports System
Imports NUnit.Framework
Imports NCI.EasyObjects

Namespace NCI.EasyObjects.Tests.SQL

	<TestFixture()> _
	Public Class SQLGroupByFixture

		Dim aggTest As AggregateTest = New AggregateTest

		<TestFixtureSetUp()> _
		Public Sub Init()
			TransactionManager.ThreadTransactionMgrReset()
			aggTest.DatabaseInstanceName = "SQLUnitTests"
		End Sub

		<SetUp()> _
		Public Sub Init2()
			aggTest.FlushData()
		End Sub

		<Test()> _
		Public Sub OneGroupBy()
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.IsActive)
			aggTest.Query.AddGroupBy(AggregateTestSchema.IsActive)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(3, aggTest.RowCount)
		End Sub

		<Test()> _
		Public Sub TwoGroupBy()
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.IsActive)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DepartmentID)
			aggTest.Query.AddGroupBy(AggregateTestSchema.IsActive)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DepartmentID)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(12, aggTest.RowCount)
		End Sub

		<Test()> _
		Public Sub GroupByWithWhere()
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.IsActive)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DepartmentID)
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
			aggTest.Where.IsActive.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.IsActive)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DepartmentID)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(5, aggTest.RowCount)
		End Sub


		<Test()> _
		Public Sub GroupByWithWhereAndOrderBy()
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.IsActive)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DepartmentID)
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
			aggTest.Where.IsActive.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.IsActive)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DepartmentID)
			aggTest.Query.AddOrderBy(AggregateTestSchema.DepartmentID, WhereParameter.Dir.ASC)
			aggTest.Query.AddOrderBy(AggregateTestSchema.IsActive, WhereParameter.Dir.ASC)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(5, aggTest.RowCount)
		End Sub

		<Test()> _
		Public Sub GroupByWithOrderByCountAll()

			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.IsActive)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DepartmentID)
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
			aggTest.Where.IsActive.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.IsActive)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DepartmentID)
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(5, aggTest.RowCount)
		End Sub

		<Test()> _
		  Public Sub GroupByWithTop()

			aggTest.Query.TopN = 3
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.IsActive)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DepartmentID)
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
			aggTest.Where.IsActive.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.IsActive)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DepartmentID)
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(aggTest.Query.TopN, aggTest.RowCount)
		End Sub

		<Test()> _
		  Public Sub GroupByWithDistinct()

			aggTest.Query.Distinct = True
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.IsActive)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DepartmentID)
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
			aggTest.Where.IsActive.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.IsActive)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DepartmentID)
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(5, aggTest.RowCount)
		End Sub

		<Test()> _
		Public Sub GroupByWithTearoff()
			aggTest.Aggregate.Salary.Function = AggregateParameter.Func.Sum
			aggTest.Aggregate.Salary.Alias = "Sum"
			Dim ap As AggregateParameter = aggTest.Aggregate.TearOff.Salary
			ap.Function = AggregateParameter.Func.Min
			ap.Alias = "Min"
			aggTest.Query.AddResultColumn(AggregateTestSchema.IsActive)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DepartmentID)
			aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
			aggTest.Where.IsActive.Value = True
			aggTest.Query.AddGroupBy(AggregateTestSchema.IsActive)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DepartmentID)
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
			Assert.IsTrue(aggTest.Query.Load())
			Assert.AreEqual(5, aggTest.RowCount)
		End Sub

		<Test()> _
		Public Sub GroupByWithRollup()
			aggTest.Query.WithRollup = True
			aggTest.Query.CountAll = True
			aggTest.Query.CountAllAlias = "Count"
			aggTest.Query.AddResultColumn(AggregateTestSchema.IsActive)
			aggTest.Query.AddResultColumn(AggregateTestSchema.DepartmentID)
			aggTest.Query.AddGroupBy(AggregateTestSchema.IsActive)
			aggTest.Query.AddGroupBy(AggregateTestSchema.DepartmentID)
			aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
			aggTest.Query.Load()
		End Sub

	End Class

End Namespace
