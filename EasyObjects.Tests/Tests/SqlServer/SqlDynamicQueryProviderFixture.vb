Imports System
Imports System.Data
Imports NUnit.Framework
Imports NCI.EasyObjects

Namespace EasyObjects.Tests.SQL

    <TestFixture()> _
    Public Class SqlDynamicQueryProviderFixture
        Private obj As AggregateTest = New AggregateTest

        <TestFixtureSetUp()> _
        Public Sub Init()
            obj.DatabaseInstanceName = "SQLUnitTests"
            obj.DefaultCommandType = CommandType.Text
            UnitTestBase.RefreshDatabase()
        End Sub

        <SetUp()> _
        Public Sub Init2()
            obj.FlushData()
            obj.Query.ClearAll()
        End Sub

        <Test()> _
        Public Sub BetweenClause()
            obj.Where.HireDate.BetweenBeginValue = "01/01/1999"
            obj.Where.HireDate.BetweenEndValue = "12/31/1999"
            obj.Where.HireDate.Operator = WhereParameter.Operand.Between
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("BetweenClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(6, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub BetweenClauseWithTearoffs()
            obj.Where.HireDate.BetweenBeginValue = "01/01/1999"
            obj.Where.HireDate.BetweenEndValue = "12/31/1999"
            obj.Where.HireDate.Operator = WhereParameter.Operand.Between
            Dim wp As WhereParameter = obj.Where.TearOff.HireDate
            wp.BetweenBeginValue = "01/01/2001"
            wp.BetweenEndValue = "12/31/2001"
            wp.Operator = WhereParameter.Operand.Between
            wp = obj.Where.TearOff.HireDate
            wp.BetweenBeginValue = "01/01/2003"
            wp.BetweenEndValue = "12/31/2003"
            wp.Operator = WhereParameter.Operand.Between
            Assert.IsTrue(obj.Query.Load("OR"))
            Console.WriteLine("BetweenClauseWithTearoffs: {0}", obj.Query.LastQuery)
            Assert.AreEqual(12, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub EqualClause()
            obj.Where.IsActive.Value = True
            obj.Where.IsActive.Operator = WhereParameter.Operand.Equal
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("EqualClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(22, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub NotEqualClause()
            obj.Where.IsActive.Value = True
            obj.Where.IsActive.Operator = WhereParameter.Operand.NotEqual
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("NotEqualClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(8, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub GreaterThanClause()
            obj.Where.Age.Value = 30
            obj.Where.Age.Operator = WhereParameter.Operand.GreaterThan
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("GreaterThanClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(12, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub GreaterThanOrEqualClause()
            obj.Where.Age.Value = 30
            obj.Where.Age.Operator = WhereParameter.Operand.GreaterThanOrEqual
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("GreaterThanOrEqualClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(13, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub LessThanClause()
            obj.Where.Age.Value = 30
            obj.Where.Age.Operator = WhereParameter.Operand.LessThan
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("LessThanClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(12, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub LessThanOrEqualClause()
            obj.Where.Age.Value = 30
            obj.Where.Age.Operator = WhereParameter.Operand.LessThanOrEqual
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("LessThanOrEqualClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(13, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub LikeClause()
            obj.Where.LastName.Value = "Jo%"
            obj.Where.LastName.Operator = WhereParameter.Operand.Like
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("LikeClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(6, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub StartsWithClause()
            obj.Where.LastName.Value = "J"
            obj.Where.LastName.Operator = WhereParameter.Operand.StartsWith
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("StartsWithClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(6, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub EndsWithClause()
            obj.Where.LastName.Value = "t"
            obj.Where.LastName.Operator = WhereParameter.Operand.EndsWith
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("EndsWithClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(5, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub ContainsClause()
            obj.Where.LastName.Value = "e"
            obj.Where.LastName.Operator = WhereParameter.Operand.Contains
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("ContainsClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(14, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub NotStartsWithClause()
            obj.Where.LastName.Value = "J"
            obj.Where.LastName.Operator = WhereParameter.Operand.NotStartsWith
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("NotStartsWithClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(18, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub NotEndsWithClause()
            obj.Where.LastName.Value = "t"
            obj.Where.LastName.Operator = WhereParameter.Operand.NotEndsWith
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("NotEndsWithClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(19, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub NotContainsClause()
            obj.Where.LastName.Value = "e"
            obj.Where.LastName.Operator = WhereParameter.Operand.NotContains
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("NotContainsClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(10, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub IsNullClause()
            obj.Where.LastName.Operator = WhereParameter.Operand.IsNull
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("IsNullClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(6, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub IsNotNullClause()
            obj.Where.LastName.Operator = WhereParameter.Operand.IsNotNull
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("IsNotNullClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(24, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub InClause()
            obj.Where.ID.Value = "1,2,3,4,5"
            obj.Where.ID.Operator = WhereParameter.Operand.In
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("InClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(5, obj.RowCount)
        End Sub

        <Test()> _
        Public Sub NotInClause()
            obj.Where.ID.Value = "1,2,3,4,5"
            obj.Where.ID.Operator = WhereParameter.Operand.NotIn
            Assert.IsTrue(obj.Query.Load)
            Console.WriteLine("NotInClause: {0}", obj.Query.LastQuery)
            Assert.AreEqual(25, obj.RowCount)
        End Sub
    End Class
End Namespace