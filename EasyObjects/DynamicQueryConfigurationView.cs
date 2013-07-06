//===============================================================================
// NCI.EasyObjects library
// DynamicQueryConfigurationView
//===============================================================================
// Copyright 2005 © Noonan Consulting Inc. All rights reserved.
// Adapted from Mike Griffin's dOOdads architecture. Used by permission.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;

using NCI.EasyObjects.Configuration;
using NCI.EasyObjects.Properties;

namespace NCI.EasyObjects
{
	/// <summary>
	/// <para>Represents a view for navigating the <see cref="DynamicQuerySettings"/> configuration data.</para>
	/// </summary>
	public class DynamicQueryConfigurationView
	{
        private IConfigurationSource configurationSource;
        
        /// <summary>
        /// <para>Initialize a new instance of the <see cref="DynamicQueryConfigurationView"/> class with a <see cref="IConfigurationSource"/> object.</para>
		/// </summary>
        /// <param name="configurationSource">
        /// <para>A <see cref="IConfigurationSource"/> object.</para>
		/// </param>
        public DynamicQueryConfigurationView(IConfigurationSource configurationSource)
        {
            this.configurationSource = configurationSource;
        }

        /// <summary>
        /// Gets the <see cref="DynamicQueryData"/> from configuration for the named <see cref="DynamicQuery"/>
        /// </summary>
        /// <param name="name">
        /// The name of the <see cref="DynamicQuery"/>.
        /// </param>
        /// <returns>
        /// A <see cref="DynamicQueryData"/> object.
        /// </returns>
        public DynamicQueryData GetDynamicQueryData(string name)
        {
            DynamicQuerySettings settings = this.DynamicQuerySettings;
            if (!settings.DynamicQueryProviders.Contains(name))
            {
                throw new ConfigurationErrorsException(string.Format("Dynamic query providers section '{0}' not defined.", name));
            }
            return settings.DynamicQueryProviders.Get(name);
        }

        /// <summary>
        /// Gets the name of the default <see cref="DynamicQueryData"/>.
        /// </summary>
        /// <returns>
        /// The name of the default <see cref="DynamicQueryData"/>.
        /// </returns>
        public string DefaultDynamicQueryProvider
        {
            get
            {
                DynamicQuerySettings configSettings = this.DynamicQuerySettings;
                return configSettings.DefaultDynamicQueryProvider;
            }
        }
	
		/// <summary>
		/// <para>Gets the <see cref="DynamicQuerySettings"/> configuration data.</para>
		/// </summary>
		/// <returns>
		/// <para>The <see cref="DynamicQuerySettings"/> configuration data.</para>
		/// </returns>
		public virtual DynamicQuerySettings DynamicQuerySettings
		{
            get { return (DynamicQuerySettings)configurationSource.GetSection(DynamicQuerySettings.SectionName); }
        }
    }
}
