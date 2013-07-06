EasyObjects
===========

New home of the EasyObjects 2.x repository. Version 1.2 of EasyObjects can still be found at [Tigris](http://easyobjects.tigris.org).

EasyObjects.NET is an object relational mapper (ORM) architecture based on a combination of the popular MyGeneration dOOdads architecture and the Microsoft Enterprise Library.

dOOdads provides a rich set of functionality for an application's business object and data layer, including support for transactions, null value handling, and standard CRUD operations.

In January, Microsoft released a major update to its popular Data Access Application Block called the Enterprise Library (EntLib). While the EntLib provides a reusable pure data layer, it lacks the rich features of a true ORM. But because it has the backing of Microsoft and their legions of developers, many companies will adopt the EntLib as their standard platform.

The goal of this project is to provide an architecture that takes advantage of all the Enterprise Library has to offer, but also offers developers the rich functionality that an ORM like dOOdads brings to the table.

Credit where credit is definitely due: EasyObjects is based largely on work done by Mike Griffin on the dOOdads architecture. In fact, they even go so far as to share some code (with permission). I want to thank Mike for his great work, and allowing this project to go forward.

#Basic operations

##Load all rows from a table:

Load all the rows from a given table, and iterate through the collection.


        Employees emp = new Employees();
        if (!emp.LoadAll())
        {
            string lastName;
            do
                lastName = emps.LastName;
            while(emps.MoveNext());
        }


##Load one row from a table:

EasyObjects provides the user with "s_" properties that eliminate the need to check for null column values.

        Products prod = new Products();
        
        // Load a single row via the primary key
        prod.LoadByPrimaryKey(4);
        
        string productName = prod.s_ProductName;


##Powerful query syntax:

One of the most popular features, the ability to query data without writing custom stored procedures.

        Employees emp = new Employees();
        
        // Limit the columns returned by the SELECT query
        emp.Query.AddResultColumn(EmployeesSchema.LastName);
        emp.Query.AddResultColumn(EmployeesSchema.FirstName);
        emp.Query.AddResultColumn(EmployeesSchema.City);
        emp.Query.AddResultColumn(EmployeesSchema.Region);
        
        // Add a WHERE clause
        emp.Where.LastName.Value = "S%";
        emp.Where.LastName.Operator = WhereParameter.Operand.Like;
        
        // Add an ORDER BY clause
        emp.Query.AddOrderBy(EmployeesSchema.LastName);
        
        //  Bring back only distinct rows
        emps.Query.Distinct = true;
        
        // Bring back the top 10 rows
        emps.Query.Top = 10;
        
        emp.Query.Load()


##Built-in support for transactions:

        Products prod = new Products();
        Employees emp = new Employees();
    
        // Update the requested product
        prod.LoadByPrimaryKey(4);
        prod.UnitsInStock += 1;
    
        // Update the requested employee
        emp.LoadByPrimaryKey(1);
        emp.s_Country = "USA";
    
        // Retrieve the current transaction manager
        TransactionManager tx = TransactionManager.ThreadTransactionMgr();
    
        try
        {
            tx.BeginTransaction();
    
            // Save both objects within the same transaction
            emp.Save();
            prod.Save();
    
            // Deliberately throw an error, to cause the transaction to rollback
            throw new Exception("Deliberate exception, transaction rolled back.");
    
            tx.CommitTransaction();
        }
        catch(Exception ex)
        {
            tx.RollbackTransaction();
            TransactionManager.ThreadTransactionMgrReset();
        }


##Provider independence:

Because EasyObjects are based on the Enterprise Library, you can connect to any database that is supported by the library. SQL Server and Oracle are included by default.

##Dynamic query provider:

EasyObjects allows developers to substitute their own dynamic query provider through the use of the Enterprise Library Configuration Application Block. A standard query provider for SQL Server and Oracle is included by default. 
