using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NCI.EasyObjects;
using NCI.EasyObjects.Tests.SQL;

namespace InitializeTests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Console.WriteLine("Refreshing database");
                //UnitTestBase.RefreshDatabase();

                //SqlDynamicSqlFixture obj = new SqlDynamicSqlFixture();
                //obj.Concurrency();

                //SqlTinyIntKeyFixture obj = new SqlTinyIntKeyFixture();
                //obj.Init();
                //obj.Dynamic1Insert();

                IdentityOnlyTest obj = new IdentityOnlyTest();
                obj.DefaultCommandType = CommandType.Text;
                obj.AddNew();
                obj.Save();

                Console.WriteLine("New IDENTITY value = {0}", obj.s_ID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }

            Console.WriteLine("\n\nPress <ENTER> to continue...");
            Console.ReadLine();
        }
    }
}
