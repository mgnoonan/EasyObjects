//===============================================================================
// NCI.EasyObjects library
// DynamicQueryCustomFactory
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
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
#if ENTLIB31
using Microsoft.Practices.ObjectBuilder;
#else
using Microsoft.Practices.ObjectBuilder2;
#endif
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
//using Microsoft.Practices.EnterpriseLibrary.Data.Properties;
//using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using NCI.EasyObjects.Configuration;
using NCI.EasyObjects.Properties;
using NCI.EasyObjects.DynamicQueryProvider;

namespace NCI.EasyObjects
{
    class DynamicQueryCustomFactory : ICustomFactory
    {
        private IDictionary<Type, IDynamicQueryAssembler> assemblersMapping = new Dictionary<Type, IDynamicQueryAssembler>(5);
        private object assemblersMappingLock = new object();

        /// <summary>
        /// This method supports the Enterprise Library infrastructure and is not intended to be used directly from your code.
        /// Returns an <see cref="IDynamicQueryAssembler"/> that represents the building process for a a concrete <see cref="DynamicQuery"/>.
        /// </summary>
        /// <param name="type">The concrete <see cref="DynamicQuery"/> type.</param>
        /// <param name="name">The name of the instance to build, or <see langword="null"/> (<b>Nothing</b> in Visual Basic).</param>
        /// <param name="reflectionCache">The cache to use retrieving reflection information.</param>
        /// <returns>The <see cref="IDynamicQueryAssembler"/> instance.</returns>
        /// <exception cref="InvalidOperationException">when concrete <see cref="DynamicQuery"/> type does have the required <see cref="DynamicQueryAssemblerAttribute"/>.</exception>
        public IDynamicQueryAssembler GetAssembler(Type type, string name, ConfigurationReflectionCache reflectionCache)
        {
            bool exists = false;
            IDynamicQueryAssembler assembler;
            lock (assemblersMappingLock)
            {
                exists = assemblersMapping.TryGetValue(type, out assembler);
            }
            if (!exists)
            {
                DynamicQueryAssemblerAttribute assemblerAttribute
                    = reflectionCache.GetCustomAttribute<DynamicQueryAssemblerAttribute>(type);
                if (assemblerAttribute == null)
                    throw new InvalidOperationException(
                        string.Format(
                            Resources.Culture,
                            Resources.ExceptionDynamicQueryTypeDoesNotHaveAssemblerAttribute,
                            type.FullName,
                            name));

                assembler
                    = (IDynamicQueryAssembler)Activator.CreateInstance(assemblerAttribute.AssemblerType);

                lock (assemblersMappingLock)
                {
                    assemblersMapping[type] = assembler;
                }
            }

            return assembler;
        }

        /// <summary>
        /// This method supports the Enterprise Library infrastructure and is not intended to be used directly from your code.
        /// Returns a new instance of a concrete <see cref="DynamicQuery"/>, described by the <see cref="DynamicQuerySettings"/> 
        /// found in the <paramref name="configurationSource"/> under the name <paramref name="name"/>, plus any additional
        /// configuration information that might describe the the concrete <b>Database</b>.
        /// </summary>
        /// <param name="context">The <see cref="IBuilderContext"/> that represents the current building process.</param>
        /// <param name="name">The name of the instance to build, or <see langword="null"/> (<b>Nothing</b> in Visual Basic).</param>
        /// <param name="configurationSource">The source for configuration objects.</param>
        /// <param name="reflectionCache">The cache to use retrieving reflection information.</param>
        /// <returns>A new instance of the appropriate subtype.</returns>
        /// <exception cref="ConfigurationErrorsException">when the configuration is invalid or <paramref name="name"/> cannot be found.</exception>
        public object CreateObject(IBuilderContext context, string name, IConfigurationSource configurationSource, ConfigurationReflectionCache reflectionCache)
        {
            DynamicQueryData objectConfiguration = GetConfiguration(name, configurationSource);

            IDynamicQueryAssembler assembler = GetAssembler(objectConfiguration.Type, name, reflectionCache);
            DynamicQuery query = assembler.Assemble(null);

            return query;
        }

        private DynamicQueryData GetConfiguration(string id, IConfigurationSource configurationSource)
        {
            DynamicQueryConfigurationView view = new DynamicQueryConfigurationView(configurationSource);
            return view.GetDynamicQueryData(id);
        }
    }
}
