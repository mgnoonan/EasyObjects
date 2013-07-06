//===============================================================================
// NCI.EasyObjects library
// DynamicQuery
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
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Data;

//using NCI.EasyObjects.Configuration;
using NCI.EasyObjects.DynamicQueryProvider;

namespace NCI.EasyObjects
{
	/// <summary>
	/// DynamicQuery allows you to build provider independent dynamic queries against the database from 
	/// outside your data layer. All  selection criteria are passed in via Parameters in order to prevent 
	/// sql injection techniques often attempted by hackers.
	/// </summary>
	/// <example>
	/// VB.NET
	/// <code>
	/// Dim emps As New Employees
	///
	/// ' LastNames that have "A" anywher in them
	/// emps.Where.LastName.Value = "%A%"
	/// emps.Where.LastName.Operator = WhereParameter.Operand.Like_
	///
	/// ' Only return the EmployeeID and LastName
	/// emps.Query.AddResultColumn(EmployeesSchema.EmployeeID)
	/// emps.Query.AddResultColumn(EmployeesSchema.LastName)
	///
	/// ' Order by LastName 
	/// ' (you can add as many order by columns as you like by repeatedly calling this)
	/// emps.Query.AddOrderBy(EmployeesSchema.LastName, WhereParameter.Dir.ASC)
	///
	/// ' Bring back only distinct rows
	/// emps.Query.Distinct = True
	///
	/// ' Bring back the top 10 rows
	/// emps.Query.TopN = 10
	///
	/// emps.Query.Load()</code>
	///	C#
	///	<code>
	/// Employees emps = new Employees();
	///
	/// // LastNames that have "A" anywher in them
	/// emps.Where.LastName.Value = "%A%";
	/// emps.Where.LastName.Operator = WhereParameter.Operand.Like;
	///
	/// // Only return the EmployeeID and LastName
	/// emps.Query.AddResultColumn(EmployeesSchema.EmployeeID);
	/// emps.Query.AddResultColumn(EmployeesSchema.LastName);
	///
	/// // Order by LastName 
	/// // (you can add as many order by columns as you like by repeatedly calling this)
	/// emps.Query.AddOrderBy(EmployeesSchema.LastName, WhereParameter.Dir.ASC);
	///
	/// // Bring back only distinct rows
	/// emps.Query.Distinct = true;
	///
	/// // Bring back the top 10 rows
	/// emps.Query.TopN = 10;
	///
	/// emps.Query.Load();</code>
	/// </example>
    [ConfigurationNameMapper(typeof(DynamicQueryMapper))]
    [CustomFactory(typeof(DynamicQueryCustomFactory))]
    public abstract class DynamicQuery
	{
        //private DynamicQueryProviderFactory dqProviderFactory;

        /// <summary>
		/// Used by derived classes
		/// </summary>
		protected bool _distinct = false;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected int _topN = -1;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected ArrayList _whereParameters = null;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected string _resultColumns = string.Empty;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected string _orderBy = string.Empty;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected string _updateColumns = string.Empty;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected string _insertColumns = string.Empty;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected string _insertColumnValues = string.Empty;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected string _insertParameters = string.Empty;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected ArrayList _parameterValues = null;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected EasyObject _entity;

		private string _lastQuery = string.Empty;

		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected bool _countAll = false;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected string _countAllAlias = string.Empty;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected bool _withRollup = false;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected string _groupBy = string.Empty;
		/// <summary>
		/// Used by derived classes
		/// </summary>
		protected ArrayList _aggregateParameters = null;
        /// <summary>
        /// Used by derived classes
        /// </summary>
        private bool _useNoLock = false;

        private string _tableName = string.Empty;
        private string _querySource = string.Empty;

		/// <summary>
		/// Default constructor
		/// </summary>
		public DynamicQuery()
		{}

        /// <summary>
        /// There is no need to call this, just access your EasyObject.Query property and it will be created on the fly.
        /// </summary>
        /// <param name="entity">A callback reference to an EasyObject</param>
        public DynamicQuery(EasyObject entity)
        {
            ArgumentValidation.CheckForNullReference(entity, "EasyObject Entity");
            this._entity = entity;
        }

        ///// <summary>
        ///// There is no need to call this, just access your EasyObject.Query property and it will be created on the fly.
        ///// </summary>
        ///// <param name="entity">A callback reference to an EasyObject</param>
        //public DynamicQuery(EasyObject entity, DynamicQueryProviderFactory dqProviderFactory)
        //{
        //    ArgumentValidation.CheckForNullReference(entity, "EasyObject Entity");
        //    ArgumentValidation.CheckForNullReference(dqProviderFactory, "DynamicQueryProviderFactory");
        //    this._entity = entity;
        //    this.dqProviderFactory = dqProviderFactory;
        //}

        ///// <summary>
        ///// <para>Gets the DynamicQueryProviderFactory used by the database instance.</para>
        ///// </summary>
        ///// <seealso cref="DynamicQueryProviderFactory"/>
        //public DynamicQueryProviderFactory DynamicQueryProviderFactory
        //{
        //    get { return this.dqProviderFactory; }
        //}

        /// <summary>
        /// <para>When implemented by a class, gets the parameter token used to delimit parameters for the database.</para>
        /// </summary>
        /// <value>
        /// <para>the parameter token used to delimit parameters for the database.</para>
        /// </value>
        protected abstract char ParameterToken { get; }

        /// <summary>
        /// <para>When implemented by a class, gets the wildcard token used in fuzzy (LIKE) queries for the database.</para>
        /// </summary>
        /// <value>
        /// <para>the wildcard token used in certain queries of the database.</para>
        /// </value>
        protected abstract char WildcardToken { get; }

        /// <summary>
        /// <para>When implemented by a class, gets the string used to separate schemas from objects for the database.</para>
        /// </summary>
        /// <value>
        /// <para>the parameter token used to separate objects for the database.</para>
        /// </value>
        protected abstract string SchemaSeparator { get; }

        /// <summary>
        /// <para>When implemented by a class, gets the string format used to delimit fieldnames for the database.</para>
        /// </summary>
        /// <value>
        /// <para>the string format used to delimit fieldnames for the database.</para>
        /// </value>
        /// <example>
        /// <code>
        /// // SQL Server delimits fieldnames with square brackets []
        /// protected override char ParameterToken
        /// {
        ///		get { return "[{0}]"; }
        /// }
        /// </code>
        /// </example>
        protected virtual string FieldFormat { get { return "{0}"; } }

        /// <summary>
        /// <para>When implemented by a class, gets the string format used to delimit aliases for the database.</para>
        /// </summary>
        /// <value>
        /// <para>the string format used to delimit aliases for the database.</para>
        /// </value>
        /// <example>
        /// <code>
        /// // SQL Server delimits aliases with single quotes ' '.
        /// protected override string AliasFormat
        /// {
        ///		get { return "'{0}'"; }
        /// }
        /// </code>
        /// </example>
        protected virtual string AliasFormat { get { return "{0}"; } }

        /// <summary>
        /// <para>Gets the complete table name (including schema) properly formatted for the current SQL dialect.</para>
        /// </summary>
        protected virtual string TableNameWithSchema
        {
            get
            {
                if (_tableName == string.Empty)
                {
                    StringBuilder table = new StringBuilder();

                    if (this._entity.SchemaTableView.Length > 0)
                    {
                        table.AppendFormat(this.FieldFormat, this._entity.SchemaTableView);
                        table.Append(this.SchemaSeparator);
                    }

                    table.AppendFormat(this.FieldFormat, this._entity.TableName);
                    _tableName = table.ToString();
                }

                return _tableName;
            }
        }

        /// <summary>
        /// <para>Gets the complete querysource name (including schema) properly formatted for the current SQL dialect.</para>
        /// </summary>
        protected virtual string QuerySourceWithSchema
        {
            get
            {
                if (_querySource == string.Empty)
                {
                    StringBuilder source = new StringBuilder();

                    if (this._entity.SchemaTableView.Length > 0)
                    {
                        source.AppendFormat(this.FieldFormat, this._entity.SchemaTableView);
                        source.Append(this.SchemaSeparator);
                    }

                    source.AppendFormat(this.FieldFormat, this._entity.QuerySource);
                    _querySource = source.ToString();
                }

                return _querySource;
            }
        }

        /// <summary>
        /// When implemented by a class, builds a provider-specific SQL SELECT query.
        /// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="dbCommand">A command wrapper from the Enterprise Library</param>
        /// <param name="conjunction">The conjunction to use between parameters in the WHERE clause</param>
        protected abstract void BuildSelectQuery(Database db, DbCommand dbCommand, string conjunction);
        /// <summary>
        /// When implemented by a class, builds a provider-specific SQL UPDATE query.
        /// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="dbCommand">A command wrapper from the Enterprise Library</param>
        /// <param name="conjunction">The conjunction to use between parameters in the WHERE clause</param>
        protected abstract void BuildUpdateQuery(Database db, DbCommand dbCommand, string conjunction);
        /// <summary>
        /// When implemented by a class, builds a provider-specific SQL INSERT query.
        /// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="dbCommand">A command wrapper from the Enterprise Library</param>
        /// <param name="conjunction">The conjunction to use between parameters in the WHERE clause</param>
        protected abstract void BuildInsertQuery(Database db, DbCommand dbCommand, string conjunction);
        /// <summary>
        /// When implemented by a class, builds a provider-specific SQL DELETE query.
        /// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="dbCommand">A command wrapper from the Enterprise Library</param>
        /// <param name="conjunction">The conjunction to use between parameters in the WHERE clause</param>
        protected abstract void BuildDeleteQuery(Database db, DbCommand dbCommand, string conjunction);

        /// <summary>
        /// An internal callback reference to the EasyObject. There is no need to use this externally.
        /// </summary>
        public EasyObject Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }

        /// <summary>
        /// Adds the (NOLOCK) table hint to a SELECT query
        /// </summary>
        public bool UseNoLock
        {
            get { return _useNoLock; }
            set { _useNoLock = value; }
        }

        /// <summary>
        /// Adds the DISTINCT modifier to the SELECT clause
        /// <note>This may not be support for all database providers</note>
        /// </summary>
        public bool Distinct
        {
            get { return _distinct; }
            set { _distinct = value; }
        }

        /// <summary>
        /// Adds the TOPN modifier to the SELECT clause
        /// <note>This may not be support for all database providers</note>
        /// </summary>
        public int TopN
        {
            get { return _topN; }
            set { _topN = value; }
        }

        /// <summary>
        /// Returns the current database columns in the SELECT clause
        /// </summary>
        protected string ResultColumns
        {
            get { return _resultColumns; }
            set { _resultColumns = value; }
        }

        /// <summary>
        /// Returns the current database columns in the UPDATE clause
        /// </summary>
        protected string UpdateColumns
        {
            get { return _updateColumns; }
            set { _updateColumns = value; }
        }

        /// <summary>
        /// Returns the current database columns in the INSERT clause
        /// </summary>
        protected string InsertColumns
        {
            get { return _insertColumns; }
            set { _insertColumns = value; }
        }

        /// <summary>
        /// Returns the current database columns in the INSERT clause
        /// </summary>
        protected string InsertColumnValues
        {
            get { return _insertColumnValues; }
            set { _insertColumnValues = value; }
        }

        /// <summary>
        /// Returns the current values for the WHERE clause
        /// </summary>
        protected ArrayList ParameterValues
        {
            get { return _parameterValues; }
            set { _parameterValues = value; }
        }

        /// <summary>
        /// Returns the current database columns in the ORDER BY clause
        /// </summary>
        protected string OrderBy
        {
            get { return _orderBy; }
            set { _orderBy = value; }
        }

        /// <summary>
        /// Contains the Query string from your last call to Query.Load(), this is useful for debugging purposes.
        /// </summary>
        /// <returns>A string containing the last executed SQL statement</returns>
        public string LastQuery
        {
            get { return _lastQuery; }
        }

        /// <summary>
        /// If true, add a COUNT(*) Aggregate to the selected columns list.
        /// <note>This may not be support for all database providers</note>
        /// </summary>
        public bool CountAll
        {
            get
            {
                return _countAll;
            }
            set
            {
                _countAll = value;

                //				if(value)
                //				{
                //					// We don't allow Save() to succeed once they reduce the columns
                //					this._entity._canSave = false;
                //				}
            }
        }

        /// <summary>
        /// If CountAll is set to true, use this to add a user-friendly column name.
        /// <note>This may not be support for all database providers</note>
        /// </summary>
        public string CountAllAlias
        {
            get
            {
                return _countAllAlias;
            }
            set
            {
                _countAllAlias = value;
            }
        }

        /// <summary>
        /// If true, add WITH ROLLUP to the GROUP BY clause.
        /// <note>This may not be support for all database providers</note>
        /// </summary>
        /// <example>
        /// <code>
        /// prds.Query.WithRollup = true;
        /// </code>
        /// </example>
        public bool WithRollup
        {
            get
            {
                return _withRollup;
            }
            set
            {
                _withRollup = value;
            }
        }

        /// <summary>
        /// This event is triggered when the TransactionManager calls for a commit to the current transaction.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Any event arguments</param>
        /// <remarks>
        /// EasyObjects can't perform an AcceptChanges on the internal DataTable until the TransactionManager
        /// is ready to commit the current transaction. Because there may be many objects in a single transaction, this event
        /// is setup to receive notification when the TransactionManager is ready for the commit. EasyObjects then calls the
        /// AcceptChanges method so that the internal DataTable properly reflects the current state of the data.
        /// </remarks>
        private void txMgr_TransactionCommitted(object sender, EventArgs e)
        {
            // Reset the internal DataTable
            this._entity.DataTable.AcceptChanges();

            // Unsubscribe from the event
            TransactionManager txMgr = TransactionManager.ThreadTransactionMgr();
            txMgr.TransactionCommitted -= new TransactionManager.TransactionCommittedDelegate(this.txMgr_TransactionCommitted);
        }

        /// <summary>
        /// Execute the Query and loads your BusinessEntity. The default conjunction between the WHERE parameters is "AND"
        /// </summary>
        /// <returns>True if at least one record was loaded</returns>
        public bool Load()
        {
            return Load("AND");
        }

        /// <summary>
        /// Execute the Query and loads your BusinessEntity. 
        /// You can pass in the conjunction that will be used between the WHERE parameters, either "AND" or "OR". 
        /// "AND" is the default.
        /// </summary>
        /// <param name="conjunction">The conjunction to use between WHERE parameters</param>
        /// <returns>True if at least one record was loaded, False if no records were loaded</returns>
        public bool Load(string conjunction)
        {
            bool loaded = false;
            DataSet ds = null;

            ArgumentValidation.CheckForNullReference(_entity, "EasyObject");

            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            Database db = _entity.GetDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand("NULL");

            // Set the command timeout
            dbCommand.CommandTimeout = _entity.CommandTimeout;

            try
            {
                BuildSelectQuery(db, dbCommand, conjunction);
                _lastQuery = dbCommand.CommandText;

                TransactionManager txMgr = TransactionManager.ThreadTransactionMgr();

                ds = new DataSet(_entity.TableName + "DataSet");

                DbTransaction tx = txMgr.GetTransaction(db, false);
                if (tx == null)
                {
                    ds = db.ExecuteDataSet(dbCommand);
                }
                else
                {
                    ds = db.ExecuteDataSet(dbCommand, tx);
                }

                // Load the object from the dataset
                this._entity.Load(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                loaded = (this._entity.RowCount > 0);
            }

            return loaded;
        }

        /// <summary>
        /// Execute the Update query. The default conjunction between the WHERE parameters is "AND".
        /// </summary>
        /// <returns>True if at least one record was loaded</returns>
        /// <remarks>Executing an the UPDATE query has no effect on the records loaded in the Business Entity.</remarks>
        public void Update()
        {
            Update("AND");
        }

        /// <summary>
        /// Execute the Update query without affecting the Business Entity.
        /// You can pass in the conjunction that will be used between the WHERE parameters, either "AND" or "OR". 
        /// "AND" is the default.
        /// </summary>
        /// <param name="conjunction">The conjunction to use between WHERE parameters</param>
        /// <remarks>Executing an the UPDATE query has no effect on the records loaded in the Business Entity.</remarks>
        public void Update(string conjunction)
        {
            ArgumentValidation.CheckForNullReference(_entity, "EasyObject");

            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            Database db = _entity.GetDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand("NULL");
            TransactionManager txMgr = TransactionManager.ThreadTransactionMgr();

            try
            {
                BuildUpdateQuery(db, dbCommand, conjunction);
                _lastQuery = dbCommand.CommandText;

                // Initialize the transaction, including an event watcher so
                // that we get notified when the transaction is committed.
                txMgr.TransactionCommitted += new TransactionManager.TransactionCommittedDelegate(txMgr_TransactionCommitted);
                txMgr.BeginTransaction();

                // Perform the update
                DbTransaction tx = txMgr.GetTransaction(db);
                if (tx == null)
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                else
                {
                    db.ExecuteNonQuery(dbCommand, tx);
                }

                // Clean up resources
                txMgr.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (!(txMgr == null))
                {
                    txMgr.RollbackTransaction();
                    txMgr.TransactionCommitted -= new TransactionManager.TransactionCommittedDelegate(this.txMgr_TransactionCommitted);
                }
                throw ex;
            }
        }

        /// <summary>
        /// Execute the INSERT query. The default conjunction between the WHERE parameters is "AND".
        /// </summary>
        /// <returns>True if at least one record was loaded</returns>
        /// <remarks>Executing the INSERT query has no effect on the records loaded in the Business Entity.</remarks>
        public void Insert()
        {
            Insert("AND");
        }

        /// <summary>
        /// Execute the INSERT query without affecting the Business Entity.
        /// You can pass in the conjunction that will be used between the WHERE parameters, either "AND" or "OR". 
        /// "AND" is the default.
        /// </summary>
        /// <param name="conjunction">The conjunction to use between WHERE parameters</param>
        /// <remarks>Executing the INSERT query has no effect on the records loaded in the Business Entity.</remarks>
        public void Insert(string conjunction)
        {
            ArgumentValidation.CheckForNullReference(_entity, "EasyObject");

            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            Database db = _entity.GetDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand("NULL");
            TransactionManager txMgr = TransactionManager.ThreadTransactionMgr();

            try
            {
                BuildInsertQuery(db, dbCommand, conjunction);
                _lastQuery = dbCommand.CommandText;

                // Initialize the transaction, including an event watcher so
                // that we get notified when the transaction is committed.
                txMgr.TransactionCommitted += new TransactionManager.TransactionCommittedDelegate(txMgr_TransactionCommitted);
                txMgr.BeginTransaction();

                // Perform the update
                DbTransaction tx = txMgr.GetTransaction(db);
                if (tx == null)
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                else
                {
                    db.ExecuteNonQuery(dbCommand, tx);
                }

                // Clean up resources
                txMgr.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (!(txMgr == null))
                {
                    txMgr.RollbackTransaction();
                    txMgr.TransactionCommitted -= new TransactionManager.TransactionCommittedDelegate(this.txMgr_TransactionCommitted);
                }
                throw ex;
            }
        }

        /// <summary>
        /// Builds the inline SQL command for a UPDATE
        /// </summary>
        /// <returns>A <seealso cref="DbCommand"/> populated with the inline SQL and parameters</returns>
        /// <remarks>This method passes AND as the default conjunction</remarks>
        public DbCommand GetUpdateCommandWrapper()
        {
            return GetUpdateCommandWrapper("AND");
        }

        /// <summary>
        /// Builds the inline SQL command for a UPDATE
        /// </summary>
        /// <param name="conjunction">The conjunction to use between <seealso cref="WhereParameter"/>s</param>
        /// <returns>A <seealso cref="DbCommand"/> populated with the inline SQL and parameters</returns>
        public DbCommand GetUpdateCommandWrapper(string conjunction)
        {
            ArgumentValidation.CheckForNullReference(_entity, "EasyObject");

            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            Database db = _entity.GetDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand("NULL");

            try
            {
                BuildUpdateQuery(db, dbCommand, conjunction);
                _lastQuery = dbCommand.CommandText;

                return dbCommand;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Builds the inline SQL command for an INSERT
        /// </summary>
        /// <returns>A <seealso cref="DbCommand"/> populated with the inline SQL and parameters</returns>
        /// <remarks>This method passes AND as the default conjunction</remarks>
        public DbCommand GetInsertCommandWrapper()
        {
            return GetInsertCommandWrapper("AND");
        }

        /// <summary>
        /// Builds the inline SQL command for a INSERT
        /// </summary>
        /// <param name="conjunction">The conjunction to use between <seealso cref="WhereParameter"/>s</param>
        /// <returns>A <seealso cref="DbCommand"/> populated with the inline SQL and parameters</returns>
        public DbCommand GetInsertCommandWrapper(string conjunction)
        {
            ArgumentValidation.CheckForNullReference(_entity, "EasyObject");

            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            Database db = _entity.GetDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand("NULL");

            try
            {
                BuildInsertQuery(db, dbCommand, conjunction);
                _lastQuery = dbCommand.CommandText;

                return dbCommand;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Builds the inline SQL command for a DELETE
        /// </summary>
        /// <returns>A <seealso cref="DbCommand"/> populated with the inline SQL and parameters</returns>
        /// <remarks>This method passes AND as the default conjunction</remarks>
        public DbCommand GetDeleteCommandWrapper()
        {
            return GetDeleteCommandWrapper("AND");
        }

        /// <summary>
        /// Builds the inline SQL command for a DELETE
        /// </summary>
        /// <param name="conjunction">The conjunction to use between <seealso cref="WhereParameter"/>s</param>
        /// <returns>A <seealso cref="DbCommand"/> populated with the inline SQL and parameters</returns>
        public DbCommand GetDeleteCommandWrapper(string conjunction)
        {
            ArgumentValidation.CheckForNullReference(_entity, "EasyObject");

            // Create the Database object, using the default database service. The
            // default database service is determined through configuration.
            Database db = _entity.GetDatabase();

            DbCommand dbCommand = db.GetSqlStringCommand("NULL");

            try
            {
                BuildDeleteQuery(db, dbCommand, conjunction);
                _lastQuery = dbCommand.CommandText;

                return dbCommand;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// The default result set for Query.Load is all of the columns in your Table or View. Once you call 
        /// AddResultColumn this changes to only the columns you have added via this method. For instance, 
        /// if you call AddResultColumn twice then only two columns will be returned in your result set. 
        /// </summary>
        /// <param name="item">A SchemaItem to add to the SELECT statement</param>
        public virtual void AddResultColumn(SchemaItem item)
        {
            AddResultColumnFormat(item.FieldName);
        }

        /// <summary>
        /// The default result set for Query.Load is all of the columns in your Table or View. Once you call 
        /// AddResultColumn this changes to only the columns you have added via this method. For instance, 
        /// if you call AddResultColumn twice then only two columns will be returned in your result set. 
        /// </summary>
        /// <param name="fieldName">The name of a column to add to the SELECT statement</param>
        public virtual void AddResultColumn(string fieldName)
        {
            if (_resultColumns.Length > 0)
            {
                _resultColumns += ", ";
            }

            _resultColumns += fieldName;
        }

        /// <summary>
        /// Adds a provider-specific formatted column name to the SELECT statement
        /// </summary>
        /// <param name="fieldName">The name of a column to add to the SELECT statement</param>
        public virtual void AddResultColumnFormat(string fieldName)
        {
            if (_resultColumns.Length > 0)
            {
                _resultColumns += ", ";
            }

            _resultColumns += string.Format(this.FieldFormat, fieldName);
        }

        /// <summary>
        /// Clears all the result columns and parameters from the current instance
        /// </summary>
        public void ClearAll()
        {
            this.ResultColumnsClear();
            _updateColumns = string.Empty;
            _insertColumns = string.Empty;
            _insertColumnValues = string.Empty;

            this.FlushWhereParameters();
            this.FlushAggregateParameters();
        }

        /// <summary>
        /// Calling this will set the result columns back to "all".
        /// </summary>
        public void ResultColumnsClear()
        {
            _resultColumns = string.Empty;
        }

        /// <summary>
        /// The default result set for Query.Load is all of the columns in your Table or View. Once you call 
        /// AddResultColumn this changes to only the columns you have added via this method. For instance, 
        /// if you call AddResultColumn twice then only two columns will be returned in your result set. 
        /// </summary>
        /// <param name="item">A SchemaItem to add to the UPDATE statement</param>
        public virtual void AddUpdateColumn(SchemaItem item)
        {
            if (_updateColumns.Length > 0)
            {
                _updateColumns += ", ";
            }
            if (_parameterValues == null)
            {
                _parameterValues = new ArrayList();
            }

            _updateColumns += string.Format("{0} = {1}{2}", string.Format(this.FieldFormat, item.FieldName), this.ParameterToken, item.FieldName);
        }

        /// <summary>
        /// The default result set for Query.Load is all of the columns in your Table or View. Once you call 
        /// AddResultColumn this changes to only the columns you have added via this method. For instance, 
        /// if you call AddResultColumn twice then only two columns will be returned in your result set. 
        /// </summary>
        /// <param name="item">A SchemaItem to add to the UPDATE statement</param>
        /// <param name="paramValue">The value to update the column to</param>
        public virtual void AddUpdateColumnWithValue(SchemaItem item, object paramValue)
        {
            AddUpdateColumn(item);

            ValueParameter param = new ValueParameter(item);
            param.Value = paramValue;
            _parameterValues.Add(param);
        }

        /// <summary>
        /// Calling this will clear all values from the UPDATE statement.
        /// </summary>
        public void UpdateValuesClear()
        {
            _updateColumns = string.Empty;
            _parameterValues.Clear();
        }

        /// <summary>
        /// Adds a column to the INSERT query 
        /// </summary>
        /// <param name="item">A SchemaItem to add to the INSERT statement</param>
        public virtual void AddInsertColumn(SchemaItem item)
        {
            AddInsertColumn(string.Format(this.FieldFormat, item.FieldName), string.Format("{0}{1}", this.ParameterToken, item.FieldName));
        }

        /// <summary>
        /// Adds a column and column value parameter to the INSERT query 
        /// </summary>
        /// <param name="columnName">A column to add to the INSERT statement</param>
        /// <param name="columnValueName">A column parameter name for the VALUES clause</param>
        public virtual void AddInsertColumn(string columnName, string columnValueName)
        {
            if (_insertColumns.Length > 0)
            {
                _insertColumns += ", ";
                _insertColumnValues += ", ";
            }
            if (_parameterValues == null)
            {
                _parameterValues = new ArrayList();
            }

            _insertColumns += columnName;
            _insertColumnValues += columnValueName;
        }

        /// <summary>
        /// The default result set for Query.Load is all of the columns in your Table or View. Once you call 
        /// AddResultColumn this changes to only the columns you have added via this method. For instance, 
        /// if you call AddResultColumn twice then only two columns will be returned in your result set. 
        /// </summary>
        /// <param name="item">A SchemaItem to add to the UPDATE statement</param>
        /// <param name="paramValue">The value to update the column to</param>
        public virtual void AddInsertColumnWithValue(SchemaItem item, object paramValue)
        {
            AddInsertColumn(item);

            ValueParameter param = new ValueParameter(item);
            param.Value = paramValue;
            _parameterValues.Add(param);
        }

        /// <summary>
        /// Calling this will clear all values from the UPDATE statement.
        /// </summary>
        public void InsertValuesClear()
        {
            _insertColumns = string.Empty;
            _insertParameters = string.Empty;
            _parameterValues.Clear();
        }

        /// <summary>
        /// Adds the WhereParameter to the internal ArrayList.
        /// </summary>
        /// <param name="param">The WhereParameter to add</param>
        /// <remarks>		
        /// NOTE: This is called by the EasyObject framework and you should never call it. 
        /// We reserve the right to remove or change this method.
        ///</remarks>
        public void AddWhereParameter(WhereParameter param)
        {
            if (_whereParameters == null)
            {
                _whereParameters = new ArrayList();
            }

            _whereParameters.Add(param);
        }

        /// <summary>
        /// Clears the internal ArrayList of WhereParameters
        /// </summary>
        /// <remarks>		
        /// NOTE: This is called by the EasyObject framework and you should never call it. 
        /// We reserve the right to remove or change this method.
        ///</remarks>
        public void FlushWhereParameters()
        {
            if (_whereParameters != null)
            {
                _whereParameters.Clear();
            }

            _orderBy = string.Empty;
        }

        /// <overloads>
        /// Adds an ORDER BY clause to the query. If you want 
        /// to order the data by two columns you will need to call this method twice.
        /// </overloads>
        /// <summary>
        /// Adds an ORDER BY clause to the query using the FieldName. If you want 
        /// to order the data by two columns you will need to call this method twice.
        /// </summary>
        /// <param name="fieldName">A column to sort by</param>
        /// <param name="direction">A <see cref="WhereParameter.Dir" /> indicating the sort direction</param>
        /// <example>
        /// VB.NET
        /// <code>
        /// emps.Query.AddOrderBy("ExampleColumn", WhereParameter.Dir.DESC)
        /// emps.Query.AddOrderBy(EmployeesSchema.LastName, WhereParameter.Dir.ASC)
        /// emps.Query.AddOrderBy(EmployeesSchema.FirstName, WhereParameter.Dir.ASC)
        /// </code>
        /// C#
        /// <code>
        /// emps.Query.AddOrderBy("ExampleColumn", WhereParameter.Dir.DESC);
        /// emps.Query.AddOrderBy(EmployeesSchema.LastName, WhereParameter.Dir.ASC);
        /// emps.Query.AddOrderBy(EmployeesSchema.FirstName, WhereParameter.Dir.ASC);
        /// </code>
        /// </example>
        public virtual void AddOrderBy(string fieldName, WhereParameter.Dir direction)
        {
            if (_orderBy.Length > 0)
            {
                _orderBy += ", ";
            }

            _orderBy += fieldName;

            if (direction == WhereParameter.Dir.ASC)
                _orderBy += " ASC";
            else
                _orderBy += " DESC";
        }

        /// <summary>
        /// Adds an ORDER BY clause to the query using the FieldName of the SchemaItem. If you want 
        /// to order the data by two columns you will need to call this method twice.
        /// </summary>
        /// <param name="item">A SchemaItem for the column you want to sort by</param>
        /// <param name="direction">Either Descending or Ascending</param>
        /// <example>
        /// VB.NET
        /// <code>
        /// emps.Query.AddOrderBy("ExampleColumn", WhereParameter.Dir.DESC)
        /// emps.Query.AddOrderBy(EmployeesSchema.LastName, WhereParameter.Dir.ASC)
        /// emps.Query.AddOrderBy(EmployeesSchema.FirstName, WhereParameter.Dir.ASC)
        /// </code>
        /// C#
        /// <code>
        /// emps.Query.AddOrderBy("ExampleColumn", WhereParameter.Dir.DESC);
        /// emps.Query.AddOrderBy(EmployeesSchema.LastName, WhereParameter.Dir.ASC);
        /// emps.Query.AddOrderBy(EmployeesSchema.FirstName, WhereParameter.Dir.ASC);
        /// </code>
        /// </example>
        public virtual void AddOrderBy(SchemaItem item, WhereParameter.Dir direction)
        {
            AddOrderByFormat(item.FieldName, direction);
        }

        /// <summary>
        /// Adds an ORDER BY clause to the query using the FieldName of the SchemaItem. If you want 
        /// to order the data by two columns you will need to call this method twice.
        /// </summary>
        /// <param name="item">A SchemaItem for the column you want to sort by</param>
        /// <example>
        /// <code>
        /// emps.Query.AddOrderBy(EmployeesSchema.LastName)
        /// emps.Query.AddOrderBy(EmployeesSchema.FirstName)
        /// </code>
        /// </example>
        /// <remarks>The default sort order is ascending.</remarks>
        public virtual void AddOrderBy(SchemaItem item)
        {
            AddOrderByFormat(item.FieldName);
        }

        /// <summary>
        /// Adds an ORDER BY clause to the query. The field is formatted using the <see cref="FieldFormat"/>.
        /// </summary>
        /// <param name="fieldName">The field to be added to the ORDER BY</param>
        /// <param name="direction">The <see cref="WhereParameter.Dir"/> direction to sort the results by</param>
        /// <example>
        /// <code>
        /// emps.Query.AddOrderBy("LastName", WhereParameter.Dir.ASC)
        /// emps.Query.AddOrderBy("FirstName", WhereParameter.Dir.ASC)
        /// </code>
        /// </example>
        public virtual void AddOrderByFormat(string fieldName, WhereParameter.Dir direction)
        {
            if (_orderBy.Length > 0)
            {
                _orderBy += ", ";
            }

            _orderBy += string.Format(this.FieldFormat, fieldName);

            if (direction == WhereParameter.Dir.ASC)
                _orderBy += " ASC";
            else
                _orderBy += " DESC";
        }

        /// <summary>
        /// Adds an ORDER BY clause to the query. The field is formatted using the <see cref="FieldFormat"/>.
        /// </summary>
        /// <param name="fieldName">The field to be added to the ORDER BY</param>
        /// <example>
        /// <code>
        /// emps.Query.AddOrderBy("LastName")
        /// emps.Query.AddOrderBy("FirstName")
        /// </code>
        /// </example>
        /// <remarks>The default sort order is ascending.</remarks>
        public virtual void AddOrderByFormat(string fieldName)
        {
            AddOrderByFormat(fieldName, WhereParameter.Dir.ASC);
        }

        /// <summary>
        /// Adds an ORDER BY clause to the query using COUNT(*).
        /// Used with Query.CountAll set to true.
        /// Derived classes implement this, like SqlServerDynamicQuery and OracleDynamicQuery
        /// to account for differences in DBMS systems.
        /// </summary>
        /// <param name="countAll">This should be entity.Query</param>
        /// <param name="direction">Either Descending or Ascending</param>
        /// <example>
        /// <code>
        /// emps.Query.AddOrderBy(emps.Query, WhereParameter.Dir.ASC)</code>
        /// </example>
        public abstract void AddOrderBy(DynamicQuery countAll, WhereParameter.Dir direction);

        /// <summary>
        /// Adds an ORDER BY clause to the query using aggregates.
        /// Derived classes implement this, like SqlServerDynamicQuery and OracleDynamicQuery
        /// to account for differences in DBMS systems.
        /// </summary>
        /// <param name="aggregate">This should be an entry from your Aggregate class</param>
        /// <param name="direction">Either Descending or Ascending</param>
        /// <example>
        /// <code>
        /// emps.Query.AddOrderBy(emps.Aggregate.CategoryID, WhereParameter.Dir.ASC)</code>
        /// </example>
        public abstract void AddOrderBy(AggregateParameter aggregate, WhereParameter.Dir direction);

        /// <summary>
        /// A Query has a default conjunction between WHERE parameters, this method lets you intermix those 
        /// and alternate between AND/OR.
        /// </summary>
        /// <param name="conjunction">The conjunction to use, either AND or OR</param>
        public void AddConjunction(WhereParameter.Conj conjunction)
        {
            if (_whereParameters == null)
            {
                _whereParameters = new ArrayList();
            }

            if (conjunction != WhereParameter.Conj.UseDefault)
            {
                if (conjunction == WhereParameter.Conj.And)
                    _whereParameters.Add(" AND ");
                else
                    _whereParameters.Add(" OR ");
            }
        }

        /// <summary>
        /// Used for advanced queries
        /// </summary>
        public void OpenParenthesis()
        {
            if (_whereParameters == null)
            {
                _whereParameters = new ArrayList();
            }

            _whereParameters.Add("(");
        }

        /// <summary>
        /// Used for advanced queries
        /// </summary>
        public void CloseParenthesis()
        {
            if (_whereParameters == null)
            {
                _whereParameters = new ArrayList();
            }

            _whereParameters.Add(")");
        }

        /// <summary>
        /// Adds the AggregateParameter to the internal ArrayList.
        /// </summary>
        /// <param name="param">The AggregateParameter</param>
        /// <remarks>
        /// NOTE: This is called by the EasyObject framework and you should never call it. 
        /// We reserve the right to remove or change this method.
        /// </remarks>
        public void AddAggregateParameter(AggregateParameter param)
        {
            if (_aggregateParameters == null)
            {
                _aggregateParameters = new ArrayList();
            }

            _aggregateParameters.Add(param);

            // We don't allow Save() to succeed once they reduce the columns
            //			this._entity._canSave = false;
        }

        /// <summary>
        /// Clears the internal ArrayList of AggregateParameters
        /// </summary>
        /// <remarks>		
        /// NOTE: This is called by the EasyObject framework and you should never call it. 
        /// We reserve the right to remove or change this method.
        ///</remarks>
        public void FlushAggregateParameters()
        {
            if (_aggregateParameters != null)
            {
                _aggregateParameters.Clear();
            }

            _countAll = false;
            _countAllAlias = string.Empty;
            _groupBy = string.Empty;
        }

        /// <overloads>
        /// Adds a GROUP BY clause to the query. If you want 
        /// to order the data by two columns you will need to call this method twice.
        /// If you call AddGroupBy, ANSI SQL requires an AddGroupBy for each AddResultColumn
        /// that is not an aggregate. Check your DBMS docs.
        /// </overloads>
        /// <summary>
        /// Adds a GROUP BY clause to the query using the FieldName. If you want 
        /// to order the data by two columns you will need to call this method twice.
        /// </summary>
        /// <param name="fieldName">A column to sort by</param>
        /// <example>
        /// VB.NET
        /// <code>
        /// emps.Query.AddOrderBy("ExampleColumn")
        /// </code>
        /// C#
        /// <code>
        /// emps.Query.AddOrderBy("ExampleColumn");
        /// </code>
        /// </example>
        public virtual void AddGroupBy(string fieldName)
        {
            if (_groupBy.Length > 0)
            {
                _groupBy += ", ";
            }

            _groupBy += fieldName;
        }

        /// <summary>
        /// Adds a GROUP BY clause to the query using the FieldName of the SchemaItem. If you want 
        /// to order the data by two columns you will need to call this method twice.
        /// </summary>
        /// <param name="item">A SchemaItem for the column you want to sort by</param>
        /// <example>
        /// VB.NET
        /// <code>
        /// emps.Query.AddOrderBy(EmployeesSchema.LastName)
        /// emps.Query.AddOrderBy(EmployeesSchema.FirstName)
        /// </code>
        /// C#
        /// <code>
        /// emps.Query.AddOrderBy(EmployeesSchema.LastName);
        /// emps.Query.AddOrderBy(EmployeesSchema.FirstName);
        /// </code>
        /// </example>
        public virtual void AddGroupBy(SchemaItem item)
        {
            AddGroupByFormat(item.FieldName);
        }

        /// <summary>
        /// Adds a GROUP BY clause to the query using the FieldName. The field is formatted
        /// using the current data provider's <see cref="FieldFormat"/>.
        /// </summary>
        /// <param name="fieldName">The raw name of the column</param>
        /// <example>
        /// VB.NET
        /// <code>
        /// emps.Query.AddOrderBy("LastName")
        /// emps.Query.AddOrderBy("FirstName")
        /// </code>
        /// C#
        /// <code>
        /// emps.Query.AddOrderBy("LastName");
        /// emps.Query.AddOrderBy("FirstName");
        /// </code>
        /// </example>
        public virtual void AddGroupByFormat(string fieldName)
        {
            AddGroupBy(string.Format(this.FieldFormat, fieldName));
        }

        private void InternalAddGroupBy(string fieldName)
        {
        }

        /// <summary>
        /// Adds an ORDER BY clause to the query using aggregates.
        /// Derived classes implement this, like SqlServerDynamicQuery and OracleDynamicQuery
        /// to account for differences in DBMS systems.
        /// </summary>
        /// <param name="aggregate">This should be an entry from your Aggregate class</param>
        /// <example>
        /// <code>
        /// emps.Query.AddGroupBy(emps.Aggregate.City)</code>
        /// </example>
        public abstract void AddGroupBy(AggregateParameter aggregate);
    }
}
