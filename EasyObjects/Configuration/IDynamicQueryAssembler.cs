//===============================================================================
// NCI.EasyObjects library
// IDynamicQueryAssembler
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
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

using NCI.EasyObjects;
using NCI.EasyObjects.Configuration;

namespace NCI.EasyObjects.Configuration
{
    /// <summary>
    /// Represents the process to build an instance of a concrete <see cref="DynamicQuery"/> described by configuration information.
    /// </summary>
    /// <seealso cref="DynamicQueryCustomFactory"/>
    public interface IDynamicQueryAssembler
    {
        /// <summary>
        /// Builds an instance of the concrete subtype of <see cref="DynamicQuery"/> the receiver knows how to build, based on 
        /// the provided connection string and any configuration information that might be contained by the 
        /// <paramref name="entity"/>.
        /// </summary>
        /// <param name="entity">An optional reference to an <see cref="EasyObject"/> to pass to the <see cref="DynamicQuery"/> provider.</param>
        /// <returns>The new dynamic query provider.</returns>
        DynamicQuery Assemble(EasyObject entity);
    }
}
