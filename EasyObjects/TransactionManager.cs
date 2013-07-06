//===============================================================================
// NCI.EasyObjects library
// TransactionManager class
//===============================================================================
// Copyright 2005 © Noonan Consulting Inc. All rights reserved.
// Adapted from Mike Griffin's dOOdads architecture. Used by permission.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace NCI.EasyObjects
{
	/// <summary>
	/// <see cref="TransactionManager"/> is used to seemlessly enroll an EasyObject into a transaction. 
	/// <see cref="TransactionManager"/> uses ADO.NET transactions and therefore is not a distributed transaction 
	/// as you would get with COM+. You only have to use <see cref="TransactionManager"/> if two or more EasyObjects 
	/// need to be saved as a transaction.  The EasyObject.Save method is already protected by a transaction.
	/// <seealso cref="EasyObject.Save()"/>
	/// </summary>
	/// <remarks>
	///	Transaction Rules:
	/// <list type="bullet">
	///		<item>Your transactions paths do not have to be pre-planned. At any time you can begin a transaction</item>
	///		<item>You can nest BeginTransaction/CommitTransaction any number of times as long as they are sandwiched appropriately</item>
	///		<item>Once RollbackTransaction is called the transaction is doomed, nothing can be committed even it is attempted.</item>
	///		<item>Transactions are stored in the Thread Local Storage.</item>
	///	</list>
	/// Transactions are stored in the Thread Local Storage or
	/// TLS. This way the API isn't intrusive, ie, forcing you
	/// to pass a Connection around everywhere.  There is one
	/// thing to remember, once you call <see cref="RollbackTransaction"/> you will
	/// be unable to commit anything on that thread until you
	/// call <see cref="ThreadTransactionMgrReset"/>().
	/// 
	/// In an ASP.NET application each page is handled by a thread
	/// that is pulled from a thread pool. Thus, you need to clear
	/// out the TLS (thread local storage) before your page begins
	/// execution. The best way to do this is to create a base page
	/// that inhertis from System.Web.UI.Page and clears the state
	/// like this:	
	///	</remarks>
	///	<example>
	/// VB.NET
	/// <code>
	/// Dim tx As TransactionManager = TransactionManager.ThreadTransactionMgr()
	/// 
	/// Try
	/// 	tx.BeginTransaction()
	/// 	emps.Save()
	/// 	prds.Save()
	/// 	tx.CommitTransaction()
	/// Catch ex As Exception
	/// 	tx.RollbackTransaction()
	/// 	tx.ThreadTransactionMgrReset()
	/// End Try
	/// </code>
	/// C#
	/// <code>
	/// TransactionManager tx = TransactionManager.ThreadTransactionMgr();
	/// 
	/// try
	/// {
	/// 	tx.BeginTransaction();
	/// 	emps.Save();
	/// 	prds.Save();
	/// 	tx.CommitTransaction();
	/// }
	/// catch(Exception ex)
	/// {
	/// 	tx.RollbackTransaction();
	/// 	tx.ThreadTransactionMgrReset();
	/// }
	/// </code>
	/// </example>
	public class TransactionManager
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TransactionManager"/>.
		/// </summary>
		public TransactionManager() {}
    
		/// <summary>
		/// Starts a new transaction scope
		/// </summary>
		/// <remarks>
		/// <see cref="BeginTransaction"/> should always be a followed by a call to <see cref="CommitTransaction"/> if all goes well, or
		/// <see cref="RollbackTransaction"/> if problems are detected.  <see cref="BeginTransaction"/>() can be nested any number of times
		/// as long as each call is unwound with a call to <see cref="CommitTransaction"/>().
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown when the transaction has already been rolled back.</exception>
		public void BeginTransaction() 
		{
			if (hasRolledBack) 
			{
				throw new InvalidOperationException("Transaction rolled back");
			}
			txCount = (txCount + 1);
		}
    
		/// <summary>
		/// Commits the current transaction scope to the database
		/// </summary>
		/// <remarks>
		/// The final call to <see cref="CommitTransaction"/> commits the transaction to the database, <see cref="BeginTransaction"/> and
		/// <see cref="CommitTransaction"/> calls can be nested
		/// </remarks>
		/// <exception cref="InvalidOperationException">Thrown when the transaction has already been rolled back.</exception>
		public void CommitTransaction() 
		{
			if (hasRolledBack) 
			{
				throw new InvalidOperationException("Transaction rolled back");
			}
			txCount = (txCount - 1);
			if (txCount == 0)
			{
				foreach (IDbTransaction tx in this.transactions.Values) 
				{
					tx.Commit();
					tx.Dispose();
				}

				foreach (IDbConnection conn in this.connections.Values)
				{
					conn.Close();
					conn.Dispose();
				}

				this.transactions.Clear();
				this.connections.Clear();
			
				if(this.TransactionCommitted != null)
				{
					try
					{
						this.TransactionCommitted(this, new EventArgs());	
					}
					catch {}
				}
			}
		}
    
		/// <summary>
		/// Rolls back the current transaction scope's changes
		/// </summary>
		/// <remarks>
		/// <see cref="RollbackTransaction"/> dooms the transaction regardless of nested calls to <see cref="BeginTransaction"/>. 
		/// Once this method is called nothing can be done to commit the transaction.  To reset the thread state a call 
		/// to <see cref="ThreadTransactionMgrReset"/> must be made.
		/// </remarks>
		public void RollbackTransaction() 
		{
			if ((!hasRolledBack && (txCount > 0))) 
			{
				foreach (IDbTransaction tx in this.transactions.Values) 
				{
					tx.Rollback();
					tx.Dispose();
				}

				foreach (IDbConnection conn in this.connections.Values)
				{
					conn.Close();
					conn.Dispose();
				}

				this.connections.Clear();
				this.transactions.Clear();
				this.txCount = 0;
			}
		}	
    
		/// <summary>
		/// Retrieves the current transaction from thread local storage.
		/// </summary>
		/// <param name="db">The current database from the Enterprise Library</param>
        /// <returns>An IDbTransaction from the requested database, or null (Nothing in Visual Basic) if <see cref="BeginTransaction"/> has not been called.</returns>
        public DbTransaction GetTransaction(Database db) 
		{
            return GetTransaction(db, true);
		}

		/// <summary>
		/// Retrieves the current transaction from thread local storage.
		/// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="createNewTransaction">Flag to create a new transaction if one does not already exist</param>
		/// <returns>A DbTransaction from the requested database, or null (Nothing in Visual Basic) if <see cref="BeginTransaction"/> has not been called.</returns>
		/// <remarks></remarks>
		public DbTransaction GetTransaction(Database db, bool createNewTransaction) 
		{
			DbTransaction tx = null;

			if (txCount > 0) 
			{
				string connectionString = db.ConnectionStringWithoutCredentials;

				if (connectionString == String.Empty) {	connectionString = "__Default";	}

				tx = (DbTransaction)this.transactions[connectionString];

				if (tx == null && createNewTransaction)
				{
                    DbConnection dbConn = db.CreateConnection();
                    dbConn.Open();

					if (_isolationLevel != IsolationLevel.Unspecified) 
					{
                        tx = dbConn.BeginTransaction(_isolationLevel);
					}
					else 
					{
                        tx = dbConn.BeginTransaction();
					}

					this.transactions[connectionString] = tx;
                    this.connections[connectionString] = dbConn;
				}
			}

			return tx;
		}

		private Hashtable transactions = new Hashtable();
		private Hashtable connections = new Hashtable();
		private int txCount = 0;
		private bool hasRolledBack = false;

		// Used to control AcceptChanges()
		internal delegate void TransactionCommittedDelegate(object sender, EventArgs e);  
		internal event TransactionCommittedDelegate TransactionCommitted; 

		#region Static properties & methods

		private static IsolationLevel _isolationLevel = IsolationLevel.Unspecified;
		private static LocalDataStoreSlot txMgrSlot = Thread.AllocateDataSlot();

		/// <summary>
		/// Gets or sets the thread's isolation level.
		/// </summary>
		public static IsolationLevel IsolationLevel 
		{
			get 
			{
				return _isolationLevel;
			}
			set 
			{
				_isolationLevel = value;
			}
		}
    
		/// <summary>
		/// Gets a the current thread's transaction manager
		/// </summary>
		/// <returns>The current thread's <see cref="TransactionManager"/></returns>
		public static TransactionManager ThreadTransactionMgr() 
		{
			TransactionManager txMgr = null;
			object obj = Thread.GetData(txMgrSlot);
			if (!(obj == null)) 
			{
				txMgr = ((TransactionManager)(obj));
			}
			else 
			{
				txMgr = new TransactionManager();
				Thread.SetData(txMgrSlot, txMgr);
			}
			return txMgr;
		}
    
		/// <summary>
		/// Performs a complete reset of the thread's transaction, so that new database operations can be successful
		/// </summary>
		public static void ThreadTransactionMgrReset() 
		{
			TransactionManager txMgr = TransactionManager.ThreadTransactionMgr();
			try 
			{
				if (((txMgr.txCount > 0) 
					&& (txMgr.hasRolledBack == false))) 
				{
					txMgr.RollbackTransaction();
				}
			}
			catch
			{
				//  At this point we're not worried about a failure
			}

			Thread.SetData(txMgrSlot, null);
		}	
		#endregion
	}
}
