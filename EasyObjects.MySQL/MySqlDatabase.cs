//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation. All rights reserved.
// Adapted from ACA.NET with permission from Avanade Inc.
// ACA.NET copyright © Avanade Inc. All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Security.Permissions;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation;
//using Microsoft.Practices.EnterpriseLibrary.Data.MySQL.Properties;

namespace Microsoft.Practices.EnterpriseLibrary.Data.MySql
{
	/// <summary>
    /// <para>Represents a MySQL database.</para>
	/// </summary>
	/// <remarks> 
	/// <para>
    /// Internally uses MySQL .NET Managed Provider to connect to the database.
	/// </para>  
	/// </remarks>
    [DatabaseAssembler(typeof(MySqlDatabaseAssembler))]
	public class MySqlDatabase : Database
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MySqlDatabase"/> class with a connection string.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
        public MySqlDatabase(string connectionString)
			: base(connectionString, MySqlClientFactory.Instance)
		{
		}

		/// <summary>
		/// <para>Gets the parameter token used to delimit parameters for the MySQL database.</para>
		/// </summary>
		/// <value>
		/// <para>Currently there is no token, so just return string.Empty.</para>
		/// </value>
		protected char ParameterToken
		{
			get { return '?'; }
		}


        /// <summary>
        /// Gets the DbDataAdapter with the given update behavior and connection from the proper derived class.
        /// </summary>
        /// <param name="updateBehavior">
        /// <para>One of the <see cref="UpdateBehavior"/> values.</para>
        /// </param>        
        /// <returns>A <see cref="DbDataAdapter"/>.</returns>
        /// <seealso cref="DbDataAdapter"/>
        protected override DbDataAdapter GetDataAdapter(UpdateBehavior updateBehavior)
        {
            DbDataAdapter adapter = base.DbProviderFactory.CreateDataAdapter();

            this.SetUpRowUpdatedEvent(adapter);

            return adapter;
        }

		/// <devdoc>
		/// Listens for the RowUpdate event on a dataadapter to support UpdateBehavior.Continue
		/// </devdoc>
		private void OnSqlRowUpdated(object sender, MySqlRowUpdatedEventArgs e)
		{
            DataColumn _autoKeyField = null;
            
            if (e.RecordsAffected == 0)
			{
				if (e.Errors != null)
				{
					//e.Row.RowError = Resources.ExceptionMessageUpdateDataSetRowFailure;
                    e.Row.RowError = e.Errors.Message;
					e.Status = UpdateStatus.SkipCurrentRow;
				}
			}

            foreach (DataColumn col in e.Row.Table.Columns)
            {
                if (col.AutoIncrement)
                {
                    _autoKeyField = col;
                    break;
                }
            }

            // Include a variable and a command to retrieve the identity value from the Access database.
            if (_autoKeyField != null)
            {
                if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
                {
                    MySqlCommand idCMD = new MySqlCommand("SELECT LAST_INSERT_ID();", e.Command.Connection, e.Command.Transaction);

                    // Retrieve the identity value and store it in the autokey column.
                    object newID = idCMD.ExecuteScalar();
                    if (newID != null)
                        e.Row[_autoKeyField.ColumnName] = newID;
                }
            }
        }

		/// <summary>
		/// Retrieves parameter information from the stored procedure specified in the <see cref="DbCommand"/> and populates the Parameters collection of the specified <see cref="DbCommand"/> object. 
		/// </summary>
		/// <param name="discoveryCommand">The <see cref="DbCommand"/> to do the discovery.</param>
		/// <remarks>The <see cref="DbCommand"/> must be a <see cref="FbCommand"/> instance.</remarks>
		protected override void DeriveParameters(DbCommand discoveryCommand)
		{
			MySqlCommandBuilder.DeriveParameters((MySqlCommand)discoveryCommand);
		}

		/// <summary>
		/// Returns the starting index for parameters in a command.
		/// </summary>
		/// <returns>The starting index for parameters in a command.</returns>
		protected override int UserParametersStartIndex()
		{
			return 1;
		}

		/// <summary>
		/// Builds a value parameter name for the current database.
		/// </summary>
		/// <param name="name">The name of the parameter.</param>
		/// <returns>A correctly formated parameter name.</returns>
		public override string BuildParameterName(string name)
		{
            if (name[0] != this.ParameterToken)
            {
                return name.Insert(0, new string(this.ParameterToken, 1));
            }
			return name;
		}

		/// <summary>
		/// Sets the RowUpdated event for the data adapter.
		/// </summary>
		/// <param name="adapter">The <see cref="DbDataAdapter"/> to set the event.</param>
		protected override void SetUpRowUpdatedEvent(DbDataAdapter adapter)
		{
			((MySqlDataAdapter)adapter).RowUpdated += new MySqlRowUpdatedEventHandler(OnSqlRowUpdated);
		}

		/// <summary>
		/// Determines if the number of parameters in the command matches the array of parameter values.
		/// </summary>
		/// <param name="command">The <see cref="DbCommand"/> containing the parameters.</param>
		/// <param name="values">The array of parameter values.</param>
		/// <returns><see langword="true"/> if the number of parameters and values match; otherwise, <see langword="false"/>.</returns>
		protected override bool SameNumberOfParametersAndValues(DbCommand command, object[] values)
		{
			int returnParameterCount = 1;
			int numberOfParametersToStoredProcedure = command.Parameters.Count - returnParameterCount;
			int numberOfValuesProvidedForStoredProcedure = values.Length;
			return numberOfParametersToStoredProcedure == numberOfValuesProvidedForStoredProcedure;
		}
	}
}