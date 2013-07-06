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
using NCI.EasyObjects.Properties;

namespace NCI.EasyObjects.Configuration
{
    /// <summary>
    /// Specifies what type to use to build the concrete <see cref="DynamicQuery"/> type this attribute is bound to. 
    /// This class cannot be inherited.
    /// </summary>
    /// <remarks>
    /// This attribute is used by the DynamicQueryCustomFactory once the <see cref="DynamicQuery"/> type to build is 
    /// known based on the configuration information to determine how to build the actual <b>Database</b> instance.
    /// </remarks>
    public sealed class DynamicQueryAssemblerAttribute : Attribute
    {
        private Type assemblerType;

        /// <summary>
        /// Initializes a new instance of the <see cref="DynamicQueryAssemblerAttribute"/> class with an assembler type.
        /// </summary>
        /// <param name="assemblerType">The assembler type. Must implement the <see cref="IDynamicQueryAssembler"/> interface.</param>
        public DynamicQueryAssemblerAttribute(Type assemblerType)
        {
            if (assemblerType == null)
                throw new ArgumentNullException("assemblerType");
            if (!typeof(IDynamicQueryAssembler).IsAssignableFrom(assemblerType))
                throw new ArgumentException(string.Format("Unassignable assembler attribute type '{0}'.", assemblerType), "assemblerType");

            this.assemblerType = assemblerType;
        }

        /// <summary>
        /// Gets the database assembler type.
        /// </summary>
        public Type AssemblerType
        {
            get { return assemblerType; }
        }
    }
}
