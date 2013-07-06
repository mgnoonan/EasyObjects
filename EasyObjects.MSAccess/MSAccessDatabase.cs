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
using System.Data.OleDb;
using System.Security.Permissions;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation;
//using Microsoft.Practices.EnterpriseLibrary.Data.MSAccess.Properties;

namespace Microsoft.Practices.EnterpriseLibrary.Data.MSAccess
{
    /// <summary>
    /// <para>Represents an OleDb Database.</para>
    /// </summary>
    /// <remarks> 
    /// <para>
    /// Internally uses OleDb .NET Managed Provider from Microsoft (System.Data.OleDb) to connect to the database.
    /// </para>  
    /// </remarks>
    [OleDbPermission(SecurityAction.Demand)]
    [DatabaseAssembler(typeof(MSAccessDatabaseAssembler))]
    public class MSAccessDatabase : Database
    {
        /// <summary>
        /// Initialize a new instance of the <see cref="OleDbDatabase"/> class.
        /// </summary>
        public MSAccessDatabase(string connectionString)
            : base(connectionString, OleDbFactory.Instance)
        {
        }

        /// <summary>
        /// <para>Gets the parameter token used to delimit parameters for the OleDb Database.</para>
        /// </summary>
        /// <value>
        /// <para>The '@' symbol.</para>
        /// </value>
        protected char ParameterToken
        {
            get { return '@'; }
        }

        /// <summary>
        /// <para>Create a <see cref="OleDbDataAdapter"/> with the given update behavior and connection.</para>
        /// </summary>
        /// <param name="updateBehavior">
        /// <para>One of the <see cref="UpdateBehavior"/> values.</para>
        /// </param>
        /// <param name="connection">
        /// <para>The open connection to the database.</para>
        /// </param>
        /// <returns>An <see cref="OleDbDataAdapter"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="connection"/> can not be <see langword="null"/> (Nothing in Visual Basic).</para>
        /// </exception>
        protected virtual new DbDataAdapter GetDataAdapter(UpdateBehavior updateBehavior)
        {
            DbDataAdapter adapter = base.DbProviderFactory.CreateDataAdapter();

            this.SetUpRowUpdatedEvent(adapter);

            return adapter;
        }

        /// <summary>
        /// Sets the RowUpdated event for the data adapter.
        /// </summary>
        /// <param name="adapter">The <see cref="DbDataAdapter"/> to set the event.</param>
        protected override void SetUpRowUpdatedEvent(DbDataAdapter adapter)
        {
            ((OleDbDataAdapter)adapter).RowUpdated += new OleDbRowUpdatedEventHandler(OnRowUpdated);
        }

        /// <devdoc>
        /// Listens for the RowUpdate event on a dataadapter to support UpdateBehavior.Continue
        /// </devdoc>
        private void OnRowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
			DataColumn _autoKeyField = null;

            if (e.RecordsAffected == 0)
            {
                if (e.Errors != null)
                {
                    //e.Row.RowError = SR.ExceptionMessageUpdateDataSetRowFailure;
					e.Row.RowError = string.Format("{0}", e.Errors.Message);
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
			// NOTE: This technique only works with Access 2000 or higher
			if (_autoKeyField != null)
			{
				if(e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
				{
					int newID = 0;
					OleDbCommand idCMD = new OleDbCommand("SELECT @@IDENTITY", e.Command.Connection, e.Command.Transaction);

					// Retrieve the identity value and store it in the CategoryID column.
					newID = (int)idCMD.ExecuteScalar();
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
            OleDbCommandBuilder.DeriveParameters((OleDbCommand)discoveryCommand);
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

        /// <summary>
        /// <para>Adds a new instance of a <see cref="DbParameter"/> object to the command.</para>
        /// </summary>
        /// <param name="command">The command to add the parameter.</param>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="DbType"/> values.</para></param>
        /// <param name="size"><para>The maximum size of the data within the column.</para></param>
        /// <param name="direction"><para>One of the <see cref="ParameterDirection"/> values.</para></param>
        /// <param name="nullable"><para>A value indicating whether the parameter accepts <see langword="null"/> (<b>Nothing</b> in Visual Basic) values.</para></param>
        /// <param name="precision"><para>The maximum number of digits used to represent the <paramref name="value"/>.</para></param>
        /// <param name="scale"><para>The number of decimal places to which <paramref name="value"/> is resolved.</para></param>
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value"/>.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>       
        public override void AddParameter(DbCommand command, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            // MS Access does not support output parameters
            if (direction == ParameterDirection.Output) return;
            
            DbParameter parameter = CreateParameter(name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            command.Parameters.Add(parameter);
        }

        /// <summary>
        /// <para>Adds a new instance of a <see cref="OleDbParameter"/> object.</para>
        /// </summary>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="DbType"/> values.</para></param>
        /// <param name="size"><para>The maximum size of the data within the column.</para></param>
        /// <param name="direction"><para>One of the <see cref="ParameterDirection"/> values.</para></param>
        /// <param name="nullable"><para>A value indicating whether the parameter accepts <see langword="null"/> (<b>Nothing</b> in Visual Basic) values.</para></param>
        /// <param name="precision"><para>The maximum number of digits used to represent the <paramref name="value"/>.</para></param>
        /// <param name="scale"><para>The number of decimal places to which <paramref name="value"/> is resolved.</para></param>
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value"/>.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>  
        protected new DbParameter CreateParameter(string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            OleDbParameter param = CreateParameter(name) as OleDbParameter;
            ConfigureParameter(param, name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            return param;
        }

        /// <summary>
        /// Configures a given <see cref="DbParameter"/>.
        /// </summary>
        /// <param name="param">The <see cref="DbParameter"/> to configure.</param>
        /// <param name="name"><para>The name of the parameter.</para></param>
        /// <param name="dbType"><para>One of the <see cref="OleDbType"/> values.</para></param>
        /// <param name="size"><para>The maximum size of the data within the column.</para></param>
        /// <param name="direction"><para>One of the <see cref="ParameterDirection"/> values.</para></param>
        /// <param name="nullable"><para>A value indicating whether the parameter accepts <see langword="null"/> (<b>Nothing</b> in Visual Basic) values.</para></param>
        /// <param name="precision"><para>The maximum number of digits used to represent the <paramref name="value"/>.</para></param>
        /// <param name="scale"><para>The number of decimal places to which <paramref name="value"/> is resolved.</para></param>
        /// <param name="sourceColumn"><para>The name of the source column mapped to the DataSet and used for loading or returning the <paramref name="value"/>.</para></param>
        /// <param name="sourceVersion"><para>One of the <see cref="DataRowVersion"/> values.</para></param>
        /// <param name="value"><para>The value of the parameter.</para></param>  
        protected virtual void ConfigureParameter(OleDbParameter param, string name, DbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            param.DbType = dbType;
            param.Size = size;
            param.Value = (value == null) ? DBNull.Value : value;
            param.Direction = direction;
            param.IsNullable = nullable;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
        }
    }
}