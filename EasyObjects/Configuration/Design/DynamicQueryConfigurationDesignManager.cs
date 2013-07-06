//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Data Access Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Configuration;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;

namespace NCI.EasyObjects.Configuration.Design
{
	/// <summary>
	/// <para>
	/// Represents the configuration design manger for the database settings.
	/// </para>
	/// </summary>    
	public class DynamicQueryConfigurationDesignManager : IConfigurationDesignManager
	{
		/// <summary>
		/// <para>
		/// Initialize a new instance of the <see cref="DataConfigurationDesignManager"/> class.
		/// </para>
		/// </summary>
		public DynamicQueryConfigurationDesignManager()
		{
		}

		/// <summary>
		/// <para>Registers the <see cref="DatabaseSettings"/> in the application.</para>
		/// </summary>
		/// <param name="serviceProvider">
		/// <para>The a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</para>
		/// </param>
		public void Register(IServiceProvider serviceProvider)
		{
			RegisterNodeMaps(serviceProvider);
			CreateCommands(serviceProvider);
		}

		/// <summary>
		/// <para>Opens the configuration settings and registers them with the application.</para>
		/// </summary>
		/// <param name="serviceProvider">
		/// <para>The a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</para>
		/// </param>
		public void Open(IServiceProvider serviceProvider)
		{
			ConfigurationContext configurationContext = ServiceHelper.GetCurrentConfigurationContext(serviceProvider);
			if (configurationContext.IsValidSection(DynamicQuerySettings.SectionName))
			{
				DynamicQuerySettings dynamicQuerySettings = null;
				DynamicQuerySettingsNode dynamicQuerySettingsNode = null;
				try
				{
					dynamicQuerySettings = configurationContext.GetConfiguration(DynamicQuerySettings.SectionName) as DynamicQuerySettings;
					dynamicQuerySettingsNode = new DynamicQuerySettingsNode(dynamicQuerySettings);
					ConfigurationNode configurationNode = ServiceHelper.GetCurrentRootNode(serviceProvider);
					configurationNode.Nodes.Add(dynamicQuerySettingsNode);
				}
				catch (ConfigurationException e)
				{
					ServiceHelper.LogError(serviceProvider, dynamicQuerySettingsNode, e);
				}
			}
		}

		/// <summary>
		/// <para>Saves the configuration settings created for the application.</para>
		/// </summary>
		/// <param name="serviceProvider">
		/// <para>The a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</para>
		/// </param>
		public void Save(IServiceProvider serviceProvider)
		{
			ConfigurationContext configurationContext = ServiceHelper.GetCurrentConfigurationContext(serviceProvider);
			if (configurationContext.IsValidSection(DynamicQuerySettings.SectionName))
			{
				DynamicQuerySettingsNode dynamicQuerySettingsNode = null;
				try
				{
					IUIHierarchy hierarchy = ServiceHelper.GetCurrentHierarchy(serviceProvider);
					dynamicQuerySettingsNode = hierarchy.FindNodeByType(typeof(DynamicQuerySettingsNode)) as DynamicQuerySettingsNode;
					if (dynamicQuerySettingsNode == null)
					{
						return;
					}
					DynamicQuerySettings dynamicQuerySettings = dynamicQuerySettingsNode.DynamicQuerySettings;
					configurationContext.WriteConfiguration(DynamicQuerySettings.SectionName, dynamicQuerySettings);
				}
				catch (ConfigurationException e)
				{
					ServiceHelper.LogError(serviceProvider, dynamicQuerySettingsNode, e);
				}
				catch (InvalidOperationException e)
				{
					ServiceHelper.LogError(serviceProvider, dynamicQuerySettingsNode, e);
				}
			}
		}

		/// <summary>
		/// <para>Adds to the dictionary configuration data for 
		/// the enterpriselibrary.configurationSettings configuration section.</para>
		/// </summary>
		/// <param name="serviceProvider">
		/// <para>The a mechanism for retrieving a service object; that is, an object that provides custom support to other objects.</para>
		/// </param>
		/// <param name="configurationDictionary">
		/// <para>A <see cref="ConfigurationDictionary"/> to add 
		/// configuration data to.</para></param>
		public void BuildContext(IServiceProvider serviceProvider, ConfigurationDictionary configurationDictionary)
		{
			DynamicQuerySettingsNode node = GetDynamicQuerySettingsNode(serviceProvider);
			if (node != null)
			{
				DynamicQuerySettings settings = node.DynamicQuerySettings;
				configurationDictionary[DynamicQuerySettings.SectionName] = settings;
			}
		}

		private static DynamicQuerySettingsNode GetDynamicQuerySettingsNode(IServiceProvider serviceProvider)
		{
			IUIHierarchy hierarchy = ServiceHelper.GetCurrentHierarchy(serviceProvider);
			if (hierarchy == null) return null;

			return hierarchy.FindNodeByType(typeof(DynamicQuerySettingsNode)) as DynamicQuerySettingsNode;
		}

		private static void CreateCommands(IServiceProvider serviceProvider)
		{
			IUIHierarchyService hierarchyService = ServiceHelper.GetUIHierarchyService(serviceProvider);
			IUIHierarchy currentHierarchy = hierarchyService.SelectedHierarchy;
			bool containsNode = currentHierarchy.ContainsNodeType(typeof(DynamicQuerySettingsNode));

			IMenuContainerService menuService = ServiceHelper.GetMenuContainerService(serviceProvider);
			ConfigurationMenuItem item = new ConfigurationMenuItem(
					"EasyObjects.NET Dynamic Query Provider", 
					new AddConfigurationSectionCommand(serviceProvider, typeof(DynamicQuerySettingsNode), DynamicQuerySettings.SectionName), 
					ServiceHelper.GetCurrentRootNode(serviceProvider), 
					Shortcut.None, 
					"EasyObjects.NET Dynamic Query Provider", 
					InsertionPoint.New);
			item.Enabled = !containsNode;
			menuService.MenuItems.Add(item);

		}

		private static void RegisterNodeMaps(IServiceProvider serviceProvider)
		{
			INodeCreationService nodeCreationService = ServiceHelper.GetNodeCreationService(serviceProvider);
            
			Type nodeType = typeof(DatabaseTypeNode);
			NodeCreationEntry entry = NodeCreationEntry.CreateNodeCreationEntryWithMultiples(new AddChildNodeCommand(serviceProvider, nodeType), nodeType, typeof(DatabaseTypeNode), "Database Type");
			nodeCreationService.AddNodeCreationEntry(entry);
		}
	}
}