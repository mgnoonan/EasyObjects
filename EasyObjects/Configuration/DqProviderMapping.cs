//===============================================================================
// NCI.EasyObjects library
// DqProviderMapping
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
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.ComponentModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace NCI.EasyObjects.Configuration
{
    /// <summary>
    /// Represents the mapping from a vendor-specific query provider to an EasyObject <see cref="DynamicQuery"/>.
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <seealso cref="DynamicQueryConfigurationView.GetDynamicQueryData(string)"/>
    /// <seealso cref="System.Data.Common.DbProviderFactory"/>
    public class DqProviderMapping : NamedConfigurationElement
    {
        /// <summary>
        /// Default name for the Sql 2005 managed provider.
        /// </summary>
        public const string DefaultSql2005ProviderName = "NCI.EasyObjects.DynamicQueryProvider.Sql2005DynamicQuery";
        /// <summary>
        /// Default name for the Sql 2000 managed providers.
        /// </summary>
        public const string DefaultSqlProviderName = "NCI.EasyObjects.DynamicQueryProvider.SqlServerDynamicQuery";
        /// <summary>
        /// Default name for the Oracle managed provider.
        /// </summary>
        public const string DefaultOracleProviderName = "NCI.EasyObjects.DynamicQueryProvider.OracleDynamicQuery";

        internal const string DefaultGenericProviderName = "generic";
        private const string dynamicQueryTypeProperty = "providerType";
    
        /// <summary>
        /// Initializes a new instance of the <see cref="DqProviderMapping"/> class.
        /// </summary>
        public DqProviderMapping() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="DqProviderMapping"/> class with name and <see cref="DynamicQueryType"/> type.
		/// </summary>
        public DqProviderMapping(string dqProviderName, Type dynamicQueryType)
            : base(dqProviderName)
		{
			this.DynamicQueryType = dynamicQueryType;
		}

		/// <summary>
		/// Gets or sets the type of database to use for the mapped ADO.NET provider.
		/// </summary>
		[ConfigurationProperty(dynamicQueryTypeProperty)]
		[TypeConverter(typeof(AssemblyQualifiedTypeNameConverter))]
		[SubclassTypeValidator(typeof(DynamicQuery))]
		public Type DynamicQueryType
		{
			get { return (Type)base[dynamicQueryTypeProperty]; }
			set { base[dynamicQueryTypeProperty] = value; }
		}

		/// <summary>
		///  Gets the logical name of the ADO.NET provider.
		/// </summary>
		public string DqProviderName
		{
			get { return Name; }
		}
    }
}
