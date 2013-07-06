//===============================================================================
// NCI.EasyObjects library
// DynamicQuerySettings
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

namespace NCI.EasyObjects.Configuration
{
	/// <summary>
	/// <para>Represents the root configuration for data.</para>
	/// </summary>
	/// <remarks>
	/// <para>The class maps to the <c>dynamicQuerySettings</c> element in configuration.</para>
	/// </remarks>
    public class DynamicQuerySettings : SerializableConfigurationSection
	{
        private const string defaultDynamicQueryProperty = "defaultProvider";
        private const string dynamicQueryProvidersProperty = "dynamicQueryProviders";
        private const string dqProviderMappingsProperty = "providerMappings";

        /// <summary>
		/// The name of the data configuration section.
		/// </summary>
        public const string SectionName = "dynamicQueryConfiguration";

        /// <summary>
        /// Retrieves the <see cref="DynamicQuerySettings"/> from a configuration source.
        /// </summary>
        /// <param name="configurationSource">The <see cref="IConfigurationSource"/> to query for the database settings.</param>
        /// <returns>The database settings from the configuration source, or <see langword="null"/> (<b>Nothing</b> in Visual Basic) if the 
        /// configuration source does not contain database settings.</returns>
        public static DynamicQuerySettings GetDynamicQuerySettings(IConfigurationSource configurationSource)
        {
            return (DynamicQuerySettings)configurationSource.GetSection(SectionName);
        }

        /// <summary>
        /// Defines the default manager instance to use when no other manager is specified
        /// </summary>
        [ConfigurationProperty(defaultDynamicQueryProperty, IsRequired = true)]
        public string DefaultDynamicQueryProvider
        {
            get { return (string)this[defaultDynamicQueryProperty]; }
            set { this[defaultDynamicQueryProperty] = value; }
        }

        /// <summary>
        /// Gets the collection of defined <see cref="DynamicQueryData"/> objects.
        /// </summary>
        /// <value>
        /// The collection of defined <see cref="DynamicQueryData"/> objects.
        /// </value>
        [ConfigurationProperty(dynamicQueryProvidersProperty, IsRequired = true)]
        public NamedElementCollection<DynamicQueryData> DynamicQueryProviders
        {
            get { return (NamedElementCollection<DynamicQueryData>)base[dynamicQueryProvidersProperty]; }
        }

        /// <summary>
        /// Gets the collection of defined <see cref="DqProviderMapping"/> objects.
        /// </summary>
        /// <value>
        /// The collection of defined <see cref="DqProviderMapping"/> objects.
        /// </value>
        [ConfigurationProperty(dqProviderMappingsProperty, IsRequired = true)]
        public NamedElementCollection<DqProviderMapping> DynamicQueryProviderMappings
        {
            get { return (NamedElementCollection<DqProviderMapping>)base[dqProviderMappingsProperty]; }
        }
    }
}