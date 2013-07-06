//===============================================================================
// NCI.EasyObjects library
// DynamicDatabaseFactory
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
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace NCI.EasyObjects
{

	/// <summary>
	/// 
	/// </summary>
	public enum DBProviderType
	{
		/// <summary>
		/// Specifies the Entlib SqlServer Provider
		/// </summary>
		SqlServer,
		/// <summary>
		/// Specifies the Entlib Oracle Provider
		/// </summary>
		Oracle
	}

	/// <summary>
	/// Summary description for DynamicDatabaseFactory.
	/// </summary>
	public sealed class DynamicDatabaseFactory
	{

		internal const string _defaultInstanceName = "dynamicDAAB";
		internal const string _connectionStringName = "dynamic";
		internal const string _sqlTypeName = "Sql Server";
		internal const string _oracleTypeName = "Oracle";
		internal const string _configName = "dataConfiguration";


		/// <summary>
		/// Private constructor
		/// </summary>
		internal DynamicDatabaseFactory()
		{
		}

		/// <summary>
		/// Creates the default database instance using integrated security for the given provider type
		/// </summary>
		/// <param name="connectString">A connect string for the database</param>
		/// <param name="provider">The type of provider</param>
		/// <returns>An initialized database provider</returns>
		public static Database CreateDatabaseFromConnectString(string connectString, DBProviderType provider)
		{
			return CreateDatabase(_defaultInstanceName, GetConnectionStringData(connectString), GetTypeFromProvider(provider));
		}

		/// <summary>
		/// Creates a named database instance using integrated security for the given provider type
		/// </summary>
		/// <param name="instanceName">The instance name to use for the database</param>
		/// <param name="connectString">A connect string for the database</param>
		/// <param name="provider">The type of provider</param>
		/// <returns>An initialized database provider</returns>
		public static Database CreateDatabaseFromConnectString(string instanceName, string connectString, DBProviderType provider)
		{
			return CreateDatabase(instanceName, GetConnectionStringData(connectString), GetTypeFromProvider(provider));
		}

		/// <summary>
		/// Creates the default database instance using integrated security for the given provider type
		/// </summary>
		/// <param name="connectString">A connect string for the database</param>
		/// <param name="userID">User ID to connect with</param>
		/// <param name="password">Password to connect with</param>
		/// <param name="provider">The type of provider</param>
		/// <returns>An initialized database provider</returns>
		public static Database CreateDatabaseFromConnectString(string connectString, string userID, string password, DBProviderType provider)
		{
			return CreateDatabase(_defaultInstanceName, GetConnectionStringData(connectString, userID, password), GetTypeFromProvider(provider));
		}

		/// <summary>
		/// Creates a named database instance using integrated security for the given provider type
		/// </summary>
		/// <param name="instanceName">The instance name to use for the database</param>
		/// <param name="connectString">A connect string for the database</param>
		/// <param name="userID">User ID to connect with</param>
		/// <param name="password">Password to connect with</param>
		/// <param name="provider">The type of provider</param>
		/// <returns>An initialized database provider</returns>
		public static Database CreateDatabaseFromConnectString(string instanceName, string connectString, string userID, string password, DBProviderType provider)
		{
			return CreateDatabase(instanceName, GetConnectionStringData(connectString, userID, password), GetTypeFromProvider(provider));
		}

		/// <summary>
		/// Creates the default database instance using integrated security for the given provider type
		/// </summary>
		/// <param name="server">Name of the server to connect to</param>
		/// <param name="database">Name of the database to connect to</param>
		/// <param name="provider">The type of provider</param>
		/// <returns>An initialized database provider</returns>
		public static Database CreateDatabase(string server, string database, DBProviderType provider)
		{
			return CreateDatabase(_defaultInstanceName, GetConnectionStringData(server, database), GetTypeFromProvider(provider));
		}

		/// <summary>
		/// Creates a named database instance using integrated security for the given provider type
		/// </summary>
		/// <param name="instanceName">The instance name to use for the database</param>
		/// <param name="server">Name of the server to connect to</param>
		/// <param name="database">Name of the database to connect to</param>
		/// <param name="provider">The type of provider</param>
		/// <returns>An initialized database provider</returns>
		public static Database CreateDatabase(string instanceName, string server, string database, DBProviderType provider)
		{
			return CreateDatabase(instanceName, GetConnectionStringData(server, database), GetTypeFromProvider(provider));
		}

		/// <summary>
		/// Creates the default database instance using username and password for the given provider type
		/// </summary>
		/// <param name="server">Name of the server to connect to</param>
		/// <param name="database">Name of the database to connect to</param>
		/// <param name="userID">User Id to connect with</param>
		/// <param name="password">Password to connect with</param>
		/// <param name="provider">The type of provider</param>
		/// <returns>An initialized database provider</returns>
		public static Database CreateDatabase(string server, string database, string userID, string password, DBProviderType provider)
		{
			return CreateDatabase(_defaultInstanceName, GetConnectionStringData(server, database, userID, password), GetTypeFromProvider(provider));
		}

		/// <summary>
		/// Creates a named database instance using username and password for the given provider type
		/// </summary>
		/// <param name="instanceName">The instance name to use for the database</param>
		/// <param name="server">Name of the server to connect to</param>
		/// <param name="database">Name of the database to connect to</param>
		/// <param name="userID">User Id to connect with</param>
		/// <param name="password">Password to connect with</param>
		/// <param name="provider">The type of provider</param>
		/// <returns>An initialized database provider</returns>
		public static Database CreateDatabase(string instanceName, string server, string database, string userID, string password, DBProviderType provider)
		{
			return CreateDatabase(instanceName, GetConnectionStringData(server, database, userID, password), GetTypeFromProvider(provider));
		}


		/// <summary>
		/// Creates a named database instance from the data configuration types
		/// </summary>
		/// <param name="instanceName">The instance name to use for the database</param>
		/// <param name="connectionData">The connection string data</param>
		/// <param name="providerData">The type provider data</param>
		/// <returns>An initialized database provider</returns>
		public static Database CreateDatabase(string instanceName, ConnectionStringData connectionData, DatabaseTypeData providerData)
		{
			ArgumentValidation.CheckForEmptyString(instanceName, "Instance Name");
			ArgumentValidation.CheckForNullReference(connectionData, "Connection Data");
			ArgumentValidation.CheckForNullReference(providerData, "Provider Data");

			DatabaseSettings settings = new DatabaseSettings(); 

			// Setup the provider and connection string data
			settings.DatabaseTypes.Add(providerData); 
			settings.ConnectionStrings.Add(connectionData);

			// The instance data binds the provider and connection string together
			InstanceData instanceData = new InstanceData(); 
			instanceData.ConnectionString = connectionData.Name; 
			instanceData.DatabaseTypeName = providerData.Name; 
			instanceData.Name = instanceName;
 
			// setup your instanceData 
			settings.Instances.Add(instanceData); 

			// Setup a data dictionary
			ConfigurationDictionary configDictionary = new ConfigurationDictionary(); 
			configDictionary.Add(_configName, settings); 
			ConfigurationContext context = ConfigurationManager.CreateContext(configDictionary); 
       
			return new DatabaseProviderFactory(context).CreateDatabase(instanceName); 
		}

		/// <summary>
		/// Returns an initialized Sql Server data type provider
		/// </summary>
		/// <returns>The initialized Sql Server data type provider</returns>
		internal static DatabaseTypeData SqlTypeData()
		{
			DatabaseTypeData typeData = new DatabaseTypeData(); 
			typeData.Name = _sqlTypeName; 
			typeData.TypeName = "Microsoft.Practices.EnterpriseLibrary.Data.Sql.SqlDatabase, Microsoft.Practices.EnterpriseLibrary.Data"; 
			return typeData;
		}

		/// <summary>
		/// Returns an initialized Oracle data type provider
		/// </summary>
		/// <returns>The initialized Oracle data type provider</returns>
		internal static DatabaseTypeData OracleTypeData()
		{
			DatabaseTypeData typeData = new DatabaseTypeData(); 
			typeData.Name = _oracleTypeName; 
			typeData.TypeName = "Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase, Microsoft.Practices.EnterpriseLibrary.Data"; 
			return typeData;
		}

		/// <summary>
		/// Initializes a connection string for use with integrated security
		/// </summary>
		/// <param name="connectString">A complete connection string to the database</param>
		/// <returns>The initialized connection string data</returns>
		/// <remarks>
		/// The connectString should contain a complete functional connection string, including any necessary
		/// parameters such as userID, password and Integrated Security settings.
		/// </remarks>
		internal static ConnectionStringData GetConnectionStringData(string connectString)
		{
			ArgumentValidation.CheckForEmptyString(connectString, "Connection String");

			ConnectionStringData connectionStringData = GetConnectionStringData();
			
			// Deconstruct the complete connection string and 
			// load it into the connectionStringData object
			BuildConnectionStringDataFromConnectString(connectionStringData, connectString);

			return connectionStringData;
		}

		/// <summary>
		/// Initializes a connection string for use with integrated security
		/// </summary>
		/// <param name="connectString">A complete connection string to the database</param>
		/// <param name="userID">The UserId to connect as</param>
		/// <param name="password">The password to connect as</param>
		/// <returns>The initialized connection string data</returns>
		/// <remarks>
		/// The connectString should contain a complete functional connection string, including any necessary
		/// parameters such as userID, password and Integrated Security settings.
		/// </remarks>
		internal static ConnectionStringData GetConnectionStringData(string connectString, string userID, string password)
		{
			ArgumentValidation.CheckForEmptyString(connectString, "Connection String");
			ArgumentValidation.CheckForEmptyString(userID, "User ID");
			ArgumentValidation.CheckForEmptyString(password, "Password");

			ConnectionStringData connectionStringData = GetConnectionStringData();

			// Deconstruct the complete connection string and 
			// load it into the connectionStringData object
			BuildConnectionStringDataFromConnectString(connectionStringData, connectString);

			// Add the userID and password to the object
			connectionStringData.Parameters.Add(new ParameterData("User ID", userID)); 
			connectionStringData.Parameters.Add(new ParameterData("password", password)); 

			return connectionStringData;
		}

		/// <summary>
		/// Initializes a connection string for use with integrated security
		/// </summary>
		/// <param name="server">The name of the server to connect to</param>
		/// <param name="database">The name of the database to connect to</param>
		/// <returns>The initialized connection string data</returns>
		internal static ConnectionStringData GetConnectionStringData(string server, string database)
		{
			ArgumentValidation.CheckForEmptyString(server, "Server");
			ArgumentValidation.CheckForEmptyString(database, "Database");

			ConnectionStringData connectionStringData = GetConnectionStringData();
          
			connectionStringData.Parameters.Add(new ParameterData("Server", server)); 
			connectionStringData.Parameters.Add(new ParameterData("Database", database)); 
			connectionStringData.Parameters.Add(new ParameterData("Integrated Security", "True")); 

			return connectionStringData;
		}

		/// <summary>
		/// Creates a connection string data object initialized with username, password server and database
		/// </summary>
		/// <param name="server">The server to connect to</param>
		/// <param name="database">The database to connect to</param>
		/// <param name="userID">The UserID to connect as</param>
		/// <param name="password">The password to connect as</param>
		/// <returns>An initialized connection string object</returns>
		internal static ConnectionStringData GetConnectionStringData(string server, string database, string userID, string password)
		{
			ArgumentValidation.CheckForEmptyString(server, "Server");
			ArgumentValidation.CheckForEmptyString(database, "Database");
			ArgumentValidation.CheckForEmptyString(userID, "User ID");
			ArgumentValidation.CheckForEmptyString(password, "Password");

			ConnectionStringData connectionStringData = GetConnectionStringData();
          
			connectionStringData.Parameters.Add(new ParameterData("Server", server)); 
			connectionStringData.Parameters.Add(new ParameterData("Database", database)); 
			connectionStringData.Parameters.Add(new ParameterData("User ID", userID)); 
			connectionStringData.Parameters.Add(new ParameterData("password", password)); 

			return connectionStringData;
		}

		/// <summary>
		/// Creates a connection string data object with initialized name
		/// </summary>
		/// <returns>Initialized connection string data</returns>
		internal static ConnectionStringData GetConnectionStringData()
		{
			ConnectionStringData connectionStringData = new ConnectionStringData(); 
			connectionStringData.Name = _connectionStringName; 
			return connectionStringData;
		}

		/// <summary>
		/// Takes a connection string and breaks it down into its component name/value pairs, then loads the pairs
		/// into a <see cref="ConnectionStringData"/> object.
		/// </summary>
		/// <param name="connectionStringData">The data object to be loaded from the connectString</param>
		/// <param name="connectString">The connection string</param>
		/// <remarks>
		/// No attempt is made to validate the connection string nor any of its contents. If the string cannot be parsed
		/// an exception will be thrown.
		/// </remarks>
		internal static void BuildConnectionStringDataFromConnectString(ConnectionStringData connectionStringData, string connectString)
		{
			ArgumentValidation.CheckForNullReference(connectionStringData, "Connection String Data");
			ArgumentValidation.CheckForEmptyString(connectString, "Connect String");

			string[] items = connectString.Split(';');

			foreach (string item in items)
			{
				if (item.Length > 0)
				{
					string[] parms = item.Split('=');
					connectionStringData.Parameters.Add(new ParameterData(parms[0], parms[1])); 
				}
			}
		}

		/// <summary>
		/// Creates provider type data based on an enumerated value
		/// </summary>
		/// <param name="provider">The type of provider</param>
		/// <returns>Initialized type data</returns>
		internal static DatabaseTypeData GetTypeFromProvider(DBProviderType provider)
		{
			switch(provider)
			{
				case DBProviderType.SqlServer:
					return SqlTypeData();
				case DBProviderType.Oracle:
					return OracleTypeData();

				default:
					return null;
			}
		}
	}
}
