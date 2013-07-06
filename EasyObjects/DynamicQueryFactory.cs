//===============================================================================
// NCI.EasyObjects library
// DynamicQueryFactory
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
using Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation;

namespace NCI.EasyObjects
{
	/// <summary>
	/// Contains factory methods for creating DynamicQuery objects
	/// </summary>
	public class DynamicQueryFactory
	{
		/// <summary>
		/// Initializes a new instance of DynamicQueryFactory
		/// </summary>
		public DynamicQueryFactory()
		{
		}

		/// <summary>
		/// Method for invoking a default DynamicQuery object.  Reads default settings
		/// from the dynamicQuerySettings.config file.
		/// </summary>
		/// <example>
		/// <code>
		/// DynamicQuery query = DynamicQueryFactory.CreateDynamicQuery();
		/// </code>
		/// </example>
		/// <param name="entity">A reference to an instance of an <see cref="EasyObject"/></param>
		/// <returns>DynamicQuery</returns>
		/// <exception cref="System.Configuration.ConfigurationException">
		/// <para>A error occured while reading the configuration.</para>
		/// </exception>
		public static DynamicQuery CreateDynamicQuery(EasyObject entity)
		{
            try
            {
                DynamicQueryProviderFactory factory = new DynamicQueryProviderFactory(ConfigurationSourceFactory.Create());
                DynamicQuery query = factory.CreateDefault();
                query.Entity = entity;

                return query;
            }
            catch (ConfigurationErrorsException configurationException)
            {
                TryLogConfigurationError(configurationException, "default");
                throw;
            }
        }

		/// <summary>
		/// Method for invoking a specified DynamicQuery service object.  Reads service settings
		/// from the dynamicQuerySettings.config file.
		/// </summary>
		/// <example>
		/// <code>
		/// DynamicQuery query = DynamicQueryFactory.CreateDynamicQuery("Oracle");
		/// </code>
		/// </example>
		/// <param name="entity">A callback reference to an EasyObject</param>
		/// <param name="instanceName">configuration key for dynamic query provider</param>
		/// <returns>DynamicQuery</returns>
		/// <exception cref="System.Configuration.ConfigurationException">
		/// <para><paramref name="instanceName"/> is not defined in configuration.</para>
		/// <para>- or -</para>
		/// <para>An error exists in the configuration.</para>
		/// <para>- or -</para>
		/// <para>A error occured while reading the configuration.</para>        
		/// </exception>
		/// <exception cref="System.Reflection.TargetInvocationException">
		/// <para>The constructor being called throws an exception.</para>
		/// </exception>
		public static DynamicQuery CreateDynamicQuery(EasyObject entity, string instanceName)
		{
            try
            {
                DynamicQueryProviderFactory factory = new DynamicQueryProviderFactory(ConfigurationSourceFactory.Create());
                DynamicQuery query = factory.Create(instanceName);
                query.Entity = entity;

                return query;
            }
            catch (ConfigurationErrorsException configurationException)
            {
                TryLogConfigurationError(configurationException, instanceName);
                throw;
            }
        }

        private static void TryLogConfigurationError(ConfigurationErrorsException configurationException, string instanceName)
        {
            try
            {
                // Add configuration error logging
                DefaultDataEventLogger eventLogger = EnterpriseLibraryFactory.BuildUp<DefaultDataEventLogger>();
                if (eventLogger != null)
                {
                    eventLogger.LogConfigurationError(configurationException, instanceName);
                }
            }
            catch { }
        }
    }
}
