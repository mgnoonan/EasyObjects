using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Permissions;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation;
using Microsoft.Practices.EnterpriseLibrary.Data.Properties;

namespace Microsoft.Practices.EnterpriseLibrary.Data.Sql
{
    /// <summary>
    /// <para>Represents a SQL Server 2005 database.</para>
    /// </summary>
    /// <remarks> 
    /// <para>
    /// Internally uses SQL Server .NET Managed Provider from Microsoft (System.Data.SqlClient) to connect to the database.
    /// </para>  
    /// </remarks>
    [SqlClientPermission(SecurityAction.Demand)]
    [DatabaseAssembler(typeof(SqlDatabaseAssembler))]
    public class Sql2005Database : SqlDatabase
    {
   		/// <summary>
        /// Initializes a new instance of the <see cref="Sql2005Database"/> class with a connection string.
		/// </summary>
		/// <param name="connectionString">The connection string.</param>
        public Sql2005Database(string connectionString)
			: base(connectionString)
		{
		}

        /// <summary>
        /// <para>Executes the <see cref="SqlCommand"/> and returns a new <see cref="XmlReader"/>.</para>
        /// </summary>
        /// <param name="command">
        /// <para>The <see cref="SqlCommand"/> to execute.</para>
        /// </param>
        /// <param name="reader">The reader to bulk copy the data from</param>
        /// <param name="targetTableName">The target table name to copy the data to</param>
        /// <returns>
        /// <para>An <see cref="XmlReader"/> object.</para>
        /// </returns>
        public void ExecuteSqlBulkCopy(DbCommand command, IDataReader reader, string targetTableName)
        {
            SqlCommand sqlCommand = CheckIfSqlCommand(command);

            using (ConnectionWrapper wrapper = GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                DoExecuteSqlBulkCopy(sqlCommand, reader, targetTableName);
            }
        }

        /// <summary>
        /// <para>Executes the <see cref="SqlCommand"/> in a transaction and returns a new <see cref="XmlReader"/>.</para>
        /// </summary>
        /// <param name="command">
        /// <para>The <see cref="SqlCommand"/> to execute.</para>
        /// </param>
        /// <param name="reader">The reader to bulk copy the data from</param>
        /// <param name="targetTableName">The target table name to copy the data to</param>
        /// <param name="transaction">
        /// <para>The <see cref="IDbTransaction"/> to execute the command within.</para>
        /// </param>
        /// <returns>
        /// <para>An <see cref="XmlReader"/> object.</para>
        /// </returns>
        public void ExecuteSqlBulkCopy(DbCommand command, IDataReader reader, string targetTableName, DbTransaction transaction)
        {
            SqlCommand sqlCommand = CheckIfSqlCommand(command);

            PrepareCommand(sqlCommand, transaction);
            DoExecuteSqlBulkCopy(sqlCommand, reader, targetTableName);
        }

        /// <devdoc>
        /// Execute the actual SqlBulkCopy call.
        /// </devdoc>        
        private void DoExecuteSqlBulkCopy(SqlCommand sqlCommand, IDataReader reader, string targetTableName)
        {
            try
            {
                DateTime startTime = DateTime.Now;
                using (SqlBulkCopy bulkData = new SqlBulkCopy(sqlCommand.Connection, SqlBulkCopyOptions.Default, sqlCommand.Transaction))
                {
                    bulkData.DestinationTableName = targetTableName;
                    bulkData.WriteToServer(reader);
                }
                instrumentationProvider.FireCommandExecutedEvent(startTime);
            }
            catch (Exception e)
            {
                instrumentationProvider.FireCommandFailedEvent(sqlCommand.CommandText, ConnectionStringNoCredentials, e);
                throw;
            }
        }

        private static SqlCommand CheckIfSqlCommand(DbCommand command)
        {
            SqlCommand sqlCommand = command as SqlCommand;
            if (sqlCommand == null) throw new ArgumentException("DbCommand is not a SqlCommand", "command");
            return sqlCommand;
        }

    }
}
