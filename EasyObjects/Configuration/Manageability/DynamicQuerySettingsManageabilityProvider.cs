//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Manageability.Adm;

using NCI.EasyObjects.Configuration.Manageability.Properties;
using NCI.EasyObjects.DynamicQueryProvider;

namespace NCI.EasyObjects.Configuration.Manageability
{
    internal class DynamicQuerySettingsManageabilityProvider
        : ConfigurationSectionManageabilityProviderBase<DynamicQuerySettings>
    {
        public const String DefaultDynamicQueryPropertyName = "defaultProvider";
        public const String ProviderMappingsKeyName = "providerMappings";
        public const String DynamicQueryTypePropertyName = "databaseType";

        private static String[] DynamicQueryTypeNames
            = new String[] { 
				typeof(SqlServerDynamicQuery).AssemblyQualifiedName, 
				typeof(Sql2005DynamicQuery).AssemblyQualifiedName, 
				typeof(OracleDynamicQuery).AssemblyQualifiedName };

        public DynamicQuerySettingsManageabilityProvider(IDictionary<Type, ConfigurationElementManageabilityProvider> subProviders)
            : base(subProviders)
        { }

        protected override void AddAdministrativeTemplateDirectives(AdmContentBuilder contentBuilder,
            DynamicQuerySettings configurationSection,
            IConfigurationSource configurationSource, String sectionKey)
        {
            contentBuilder.StartPolicy(Resources.DynamicQuerySettingsPolicyName, sectionKey);
            {
                List<AdmDropDownListItem> connectionStrings = new List<AdmDropDownListItem>();
                ConnectionStringsSection connectionStringsSection
                    = (ConnectionStringsSection)configurationSource.GetSection("connectionStrings");
                if (connectionStringsSection != null)
                {
                    foreach (ConnectionStringSettings connectionString in connectionStringsSection.ConnectionStrings)
                    {
                        connectionStrings.Add(new AdmDropDownListItem(connectionString.Name, connectionString.Name));
                    }
                }
                contentBuilder.AddDropDownListPart(Resources.DatabaseSettingsDefaultDatabasePartName,
                    DefaultDatabasePropertyName,
                    connectionStrings,
                    configurationSection.DefaultDatabase);
            }
            contentBuilder.EndPolicy();

            if (configurationSection.ProviderMappings.Count > 0)
            {
                contentBuilder.StartCategory(Resources.ProviderMappingsCategoryName);
                {
                    foreach (DbProviderMapping providerMapping in configurationSection.ProviderMappings)
                    {
                        contentBuilder.StartPolicy(String.Format(CultureInfo.InvariantCulture,
                                                                Resources.ProviderMappingPolicyNameTemplate,
                                                                providerMapping.Name),
                            sectionKey + @"\" + ProviderMappingsKeyName + @"\" + providerMapping.Name);

                        contentBuilder.AddComboBoxPart(Resources.ProviderMappingDatabaseTypePartName,
                            DatabaseTypePropertyName,
                            providerMapping.DatabaseType.AssemblyQualifiedName,
                            255,
                            false,
                            DynamicQueryTypeNames);

                        contentBuilder.EndPolicy();
                    }
                }
                contentBuilder.EndCategory();
            }
        }

        /// <summary>
        /// Gets the name of the category that represents the whole configuration section.
        /// </summary>
        protected override string SectionCategoryName
        {
            get { return Resources.DatabaseCategoryName; }
        }

        /// <summary>
        /// Gets the name of the managed configuration section.
        /// </summary>
        protected override string SectionName
        {
            get { return DatabaseSettings.SectionName; }
        }

        /// <summary>
        /// Overrides the <paramref name="configurationSection"/>'s properties with the Group Policy values from 
        /// the registry.
        /// </summary>
        /// <param name="configurationSection">The configuration section that must be managed.</param>
        /// <param name="policyKey">The <see cref="IRegistryKey"/> which holds the Group Policy overrides.</param>
        protected override void OverrideWithGroupPoliciesForConfigurationSection(DatabaseSettings configurationSection,
            IRegistryKey policyKey)
        {
            String defaultDatabaseOverride = policyKey.GetStringValue(DefaultDatabasePropertyName);

            configurationSection.DefaultDatabase = defaultDatabaseOverride;
        }

        /// <summary>
        /// Creates the <see cref="ConfigurationSetting"/> instances that describe the <paramref name="configurationSection"/>.
        /// </summary>
        /// <param name="configurationSection">The configuration section that must be managed.</param>
        /// <param name="wmiSettings">A collection to where the generated WMI objects are to be added.</param>
        protected override void GenerateWmiObjectsForConfigurationSection(DatabaseSettings configurationSection,
            ICollection<ConfigurationSetting> wmiSettings)
        {
            wmiSettings.Add(new DatabaseBlockSetting(configurationSection.DefaultDatabase));
        }

        /// <summary>
        /// Overrides the <paramref name="configurationSection"/>'s configuration elements' properties 
        /// with the Group Policy values from the registry, if any, and creates the <see cref="ConfigurationSetting"/> 
        /// instances that describe these configuration elements.
        /// </summary>
        /// <param name="configurationSection">The configuration section that must be managed.</param>
        /// <param name="readGroupPolicies"><see langword="true"/> if Group Policy overrides must be applied; otherwise, 
        /// <see langword="false"/>.</param>
        /// <param name="machineKey">The <see cref="IRegistryKey"/> which holds the Group Policy overrides for the 
        /// configuration section at the machine level, or <see langword="null"/> 
        /// if there is no such registry key.</param>
        /// <param name="userKey">The <see cref="IRegistryKey"/> which holds the Group Policy overrides for the 
        /// configuration section at the user level, or <see langword="null"/> 
        /// if there is no such registry key.</param>
        /// <param name="generateWmiObjects"><see langword="true"/> if WMI objects must be generated; otherwise, 
        /// <see langword="false"/>.</param>
        /// <param name="wmiSettings">A collection to where the generated WMI objects are to be added.</param>
        protected override void OverrideWithGroupPoliciesAndGenerateWmiObjectsForConfigurationElements(DatabaseSettings configurationSection,
            bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey,
            bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
        {
            List<DbProviderMapping> mappingsToRemove = new List<DbProviderMapping>();

            IRegistryKey machineMappingsKey = null;
            IRegistryKey userMappingsKey = null;

            try
            {
                LoadRegistrySubKeys(ProviderMappingsKeyName,
                    machineKey, userKey,
                    out machineMappingsKey, out userMappingsKey);

                foreach (DbProviderMapping providerMapping in configurationSection.ProviderMappings)
                {
                    IRegistryKey machineMappingKey = null;
                    IRegistryKey userMappingKey = null;

                    try
                    {
                        LoadRegistrySubKeys(providerMapping.Name,
                            machineMappingsKey, userMappingsKey,
                            out machineMappingKey, out userMappingKey);

                        if (!OverrideWithGroupPoliciesAndGenerateWmiObjectsForDbProviderMapping(providerMapping,
                                readGroupPolicies, machineMappingKey, userMappingKey,
                                generateWmiObjects, wmiSettings))
                        {
                            mappingsToRemove.Add(providerMapping);
                        }
                    }
                    finally
                    {
                        ReleaseRegistryKeys(machineMappingKey, userMappingKey);
                    }
                }
            }
            finally
            {
                ReleaseRegistryKeys(machineMappingsKey, userMappingsKey);
            }

            foreach (DbProviderMapping providerMapping in mappingsToRemove)
            {
                configurationSection.ProviderMappings.Remove(providerMapping.Name);
            }
        }

        private bool OverrideWithGroupPoliciesAndGenerateWmiObjectsForDbProviderMapping(DbProviderMapping providerMapping,
            bool readGroupPolicies, IRegistryKey machineKey, IRegistryKey userKey,
            bool generateWmiObjects, ICollection<ConfigurationSetting> wmiSettings)
        {
            if (readGroupPolicies)
            {
                IRegistryKey policyKey = machineKey != null ? machineKey : userKey;
                if (policyKey != null)
                {
                    if (policyKey.IsPolicyKey && !policyKey.GetBoolValue(PolicyValueName).Value)
                    {
                        return false;
                    }
                    try
                    {
                        Type databaseTypeOverride = policyKey.GetTypeValue(DatabaseTypePropertyName);

                        providerMapping.DatabaseType = databaseTypeOverride;
                    }
                    catch (RegistryAccessException ex)
                    {
                        LogExceptionWhileOverriding(ex);
                    }
                }
            }
            if (generateWmiObjects)
            {
                wmiSettings.Add(
                    new ProviderMappingSetting(providerMapping.DbProviderName,
                        providerMapping.DatabaseType.AssemblyQualifiedName));
            }

            return true;
        }
    }
}
