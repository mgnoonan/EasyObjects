Imports System
Imports NUnit.Framework
Imports NCI.EasyObjects

NameSpace EasyObjects.Tests.SQL

	<TestFixture()> _
	Public Class SQLViewGroupByFixture

		Dim aggTest As FullNameView = New FullNameView

		<TestFixtureSetUp()> _
		Public Sub Init()
            aggTest.DatabaseInstanceName = "SQLUnitTests"
            UnitTestBase.RefreshDatabase()
        End Sub

        <SetUp()> _
        Public Sub Init2()
            aggTest.FlushData()
        End Sub

        <Test()> _
        Public Sub OneGroupBy()
            aggTest.Query.CountAll = True
            aggTest.Query.CountAllAlias = "Count"
            aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive)
            aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive)
            Assert.IsTrue(aggTest.Query.Load())
            Assert.AreEqual(1, aggTest.RowCount)
        End Sub

        <Test()> _
        Public Sub TwoGroupBy()
            aggTest.Query.CountAll = True
            aggTest.Query.CountAllAlias = "Count"
            aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive)
            aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID)
            aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive)
            aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID)
            Assert.IsTrue(aggTest.Query.Load())
            Assert.AreEqual(7, aggTest.RowCount)
        End Sub

        <Test()> _
        Public Sub GroupByWithWhere()
            aggTest.Query.CountAll = True
            aggTest.Query.CountAllAlias = "Count"
            aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive)
            aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID)
            aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
            aggTest.Where.IsActive.Value = True
            aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive)
            aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID)
            Assert.IsTrue(aggTest.Query.Load())
            Assert.AreEqual(7, aggTest.RowCount)
        End Sub


        <Test()> _
        Public Sub GroupByWithWhereAndOrderBy()
            aggTest.Query.CountAll = True
            aggTest.Query.CountAllAlias = "Count"
            aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive)
            aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID)
            aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
            aggTest.Where.IsActive.Value = True
            aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive)
            aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID)
            aggTest.Query.AddOrderBy(FullNameViewSchema.DepartmentID, WhereParameter.Dir.ASC)
            aggTest.Query.AddOrderBy(FullNameViewSchema.IsActive, WhereParameter.Dir.ASC)
            Assert.IsTrue(aggTest.Query.Load())
            Assert.AreEqual(7, aggTest.RowCount)
        End Sub

        <Test()> _
        Public Sub GroupByWithOrderByCountAll()

            aggTest.Query.CountAll = True
            aggTest.Query.CountAllAlias = "Count"
            aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive)
            aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID)
            aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
            aggTest.Where.IsActive.Value = True
            aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive)
            aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID)
            aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
            Assert.IsTrue(aggTest.Query.Load())
            Assert.AreEqual(7, aggTest.RowCount)
        End Sub

        <Test()> _
          Public Sub GroupByWithTop()

            aggTest.Query.TopN = 3
            aggTest.Query.CountAll = True
            aggTest.Query.CountAllAlias = "Count"
            aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive)
            aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID)
            aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
            aggTest.Where.IsActive.Value = True
            aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive)
            aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID)
            aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
            Assert.IsTrue(aggTest.Query.Load())
            Assert.AreEqual(aggTest.Query.TopN, aggTest.RowCount)
        End Sub

        <Test()> _
          Public Sub GroupByWithDistinct()

            aggTest.Query.Distinct = True
            aggTest.Query.CountAll = True
            aggTest.Query.CountAllAlias = "Count"
            aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive)
            aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID)
            aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
            aggTest.Where.IsActive.Value = True
            aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive)
            aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID)
            aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
            Assert.IsTrue(aggTest.Query.Load())
            Assert.AreEqual(7, aggTest.RowCount)
        End Sub

        <Test()> _
        Public Sub GroupByWithTearoff()
            aggTest.Aggregate.Salary.Function = AggregateParameter.Func.Sum
            aggTest.Aggregate.Salary.Alias = "Sum"
            Dim ap As AggregateParameter = aggTest.Aggregate.TearOff.Salary
            ap.Function = AggregateParameter.Func.Min
            ap.Alias = "Min"
            aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive)
            aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID)
            aggTest.Where.IsActive.Operator = WhereParameter.Operand.Equal
            aggTest.Where.IsActive.Value = True
            aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive)
            aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID)
            aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
            Assert.IsTrue(aggTest.Query.Load())
            Assert.AreEqual(7, aggTest.RowCount)
        End Sub

        <Test()> _
        Public Sub GroupByWithRollup()
            aggTest.Query.WithRollup = True
            aggTest.Query.CountAll = True
            aggTest.Query.CountAllAlias = "Count"
            aggTest.Query.AddResultColumn(FullNameViewSchema.IsActive)
            aggTest.Query.AddResultColumn(FullNameViewSchema.DepartmentID)
            aggTest.Query.AddGroupBy(FullNameViewSchema.IsActive)
            aggTest.Query.AddGroupBy(FullNameViewSchema.DepartmentID)
            aggTest.Query.AddOrderBy(aggTest.Query, WhereParameter.Dir.DESC)
            Assert.IsTrue(aggTest.Query.Load())
        End Sub

        <Test()> _
        Public Sub GroupByFullName()
            aggTest.Query.CountAll = True
            aggTest.Query.CountAllAlias = "Count"
            aggTest.Query.AddResultColumn(FullNameViewSchema.FullName)
            aggTest.Query.AddGroupBy(FullNameViewSchema.FullName)
            Assert.IsTrue(aggTest.Query.Load())
        End Sub

    End Class

End Namespace
