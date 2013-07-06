using System;
using System.Data;
using NUnit.Framework;
using NCI.EasyObjects;

namespace NCI.EasyObjects.Tests.SQL
{
    /// <summary>
    /// Summary description for SqlQueryResultFixture.
    /// </summary>
    [TestFixture]
    public class SqlQueryResultFixture
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
        public void AddResultColumnSchemaItem()
        {
            obj.Query.AddResultColumn(AggregateTestSchema.FirstName);
            obj.Query.AddResultColumn(AggregateTestSchema.LastName);
            obj.Query.AddResultColumn(AggregateTestSchema.HireDate);

            Assert.IsTrue(obj.Query.Load());
            Console.WriteLine("AddResultColumnSchemaItem: {0}", obj.Query.LastQuery);
            Assert.AreEqual(3, obj.DefaultView.Table.Columns.Count);
        }

        [Test]
        public void AddResultColumnString()
        {
            obj.Query.AddResultColumn(AggregateTestSchema.FirstName);
            obj.Query.AddResultColumn(AggregateTestSchema.LastName);
            obj.Query.AddResultColumn(AggregateTestSchema.HireDate);
            obj.Query.AddResultColumn(string.Format("{0} + ' ' + {1} AS FullName", AggregateTestSchema.FirstName.FieldName, AggregateTestSchema.LastName.FieldName));

            Assert.IsTrue(obj.Query.Load());
            Console.WriteLine("AddResultColumnString: {0}", obj.Query.LastQuery);
            Assert.AreEqual(4, obj.DefaultView.Table.Columns.Count);
        }

        [Test]
        public void AddOrderBySchemaItem()
        {
            obj.Query.AddResultColumn(AggregateTestSchema.FirstName);
            obj.Query.AddResultColumn(AggregateTestSchema.LastName);
            obj.Query.AddResultColumn(AggregateTestSchema.HireDate);

            obj.Query.AddOrderBy(AggregateTestSchema.LastName, WhereParameter.Dir.DESC);

            obj.Where.LastName.Operator = WhereParameter.Operand.IsNotNull;

            Assert.IsTrue(obj.Query.Load());
            Console.WriteLine("AddOrderBySchemaItem: {0}", obj.Query.LastQuery);
            //Console.WriteLine("AddOrderBySchemaItem: {0}", obj.ToXml());
            Assert.AreEqual(3, obj.DefaultView.Table.Columns.Count);
            Assert.AreEqual("David", obj.s_FirstName);
            Assert.AreEqual("Vincent", obj.s_LastName);
        }

        [Test]
        public void AddOrderByString()
        {
            obj.Query.AddResultColumn(AggregateTestSchema.FirstName);
            obj.Query.AddResultColumn(AggregateTestSchema.LastName);
            obj.Query.AddResultColumn(AggregateTestSchema.HireDate);
            obj.Query.AddResultColumn(string.Format("{0} + ' ' + {1} AS FullName", AggregateTestSchema.FirstName.FieldName, AggregateTestSchema.LastName.FieldName));

            obj.Query.AddOrderBy(string.Format("{0} + ' ' + {1}", AggregateTestSchema.FirstName.FieldName, AggregateTestSchema.LastName.FieldName), WhereParameter.Dir.ASC);

            obj.Where.LastName.Operator = WhereParameter.Operand.IsNotNull;

            Assert.IsTrue(obj.Query.Load());
            Console.WriteLine("AddOrderByString: {0}", obj.Query.LastQuery);
            //Console.WriteLine("AddOrderByString: {0}", obj.ToXml());
            Assert.AreEqual(4, obj.DefaultView.Table.Columns.Count);
            Assert.AreEqual("David", obj.s_FirstName);
            Assert.AreEqual("Costner", obj.s_LastName);
        }

        [Test]
        public void AddGroupBySchemaItem()
        {
            obj.Query.AddResultColumn(AggregateTestSchema.LastName);
            obj.Query.AddGroupBy(AggregateTestSchema.LastName);

            obj.Where.LastName.Operator = WhereParameter.Operand.IsNotNull;

            Assert.IsTrue(obj.Query.Load());
            Console.WriteLine("AddGroupBySchemaItem: {0}", obj.Query.LastQuery);
            //Console.WriteLine("AddOrderBySchemaItem: {0}", obj.ToXml());
            Assert.AreEqual(10, obj.RowCount);
        }

        [Test]
        public void AddGroupByString()
        {
            obj.Query.AddResultColumn(string.Format("{0} + ' ' + {1} AS FullName", AggregateTestSchema.FirstName.FieldName, AggregateTestSchema.LastName.FieldName));
            obj.Query.AddGroupBy(string.Format("{0} + ' ' + {1}", AggregateTestSchema.FirstName.FieldName, AggregateTestSchema.LastName.FieldName));

            obj.Where.LastName.Operator = WhereParameter.Operand.IsNotNull;

            Assert.IsTrue(obj.Query.Load());
            Console.WriteLine("AddGroupByString: {0}", obj.Query.LastQuery);
            //Console.WriteLine("AddOrderByString: {0}", obj.ToXml()); 
            Assert.AreEqual(23, obj.RowCount);
        }
    }
}
