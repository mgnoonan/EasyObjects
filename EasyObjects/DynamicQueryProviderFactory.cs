//===============================================================================
// NCI.EasyObjects library
// DynamicQueryProviderFactory
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
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

//using NCI.EasyObjects.Configuration;

namespace NCI.EasyObjects
{
	/// <summary>
	/// Represents a factory pattern which generates a provider-specific <see cref="DynamicQuery"/> object from configuration settings.
	/// </summary>
    public class DynamicQueryProviderFactory : NameTypeFactoryBase<DynamicQuery>
	{
		/// <summary>
		/// <para>Initialize a new instance of the <see cref="DynamicQueryProviderFactory"/> class.</para>
		/// </summary>
		public DynamicQueryProviderFactory() : base()
		{}

		/// <summary>
		/// <para>
        /// Initializes a new instance of the <see cref="DynamicQueryProviderFactory"/> class with the specified <see cref="IConfigurationSource"/>.
		/// </para>
		/// </summary>
        /// <param name="configurationSource">
		/// <para>Configuration context to use when creating factory</para>
		/// </param>
        public DynamicQueryProviderFactory(IConfigurationSource configurationSource)
            : base(configurationSource)
		{}
	}
}
