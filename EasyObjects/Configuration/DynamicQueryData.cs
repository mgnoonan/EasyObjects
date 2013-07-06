//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Caching Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;

namespace NCI.EasyObjects.Configuration
{
    /// <summary>
    /// Configuration data defining CacheStorageData. This configuration section defines the name and type
    /// of the IBackingStore used by a CacheManager
    /// </summary>
    [Assembler(typeof(TypeInstantiationAssembler<IDynamicQueryAssembler, DynamicQueryData>))]
    public class DynamicQueryData : NameTypeConfigurationElement
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicQueryData"/> class.
        /// </summary>
        public DynamicQueryData()
        {
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="DynamicQueryData"/> class with a name and the type of <see cref="DynamicQuery"/>.
        /// </summary>
        /// <param name="name">The name of the configured <see cref="DynamicQuery"/>. </param>
        /// <param name="type">The type of <see cref="DynamicQuery"/>.</param>
        public DynamicQueryData(string name, Type type)
        {
        }
    }
}

