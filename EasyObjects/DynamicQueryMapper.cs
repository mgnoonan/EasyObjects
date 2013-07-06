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

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ObjectBuilder;
using NCI.EasyObjects.Configuration;

namespace NCI.EasyObjects
{
    /// <summary>
    /// This type supports the Enterprise Library infrastructure and is not intended to be used directly from your code.
    /// Resolves default names for databases.
    /// </summary>
    public class DynamicQueryMapper : IConfigurationNameMapper
    {
        /// <summary>
        /// This method supports the Enterprise Library infrastructure and is not intended to be used directly from your code.
        /// Returns the default database name from the configuration in the <paramref name="configurationSource"/>, if the
        /// value for <paramref name="name"/> is <see langword="null"/> (<b>Nothing</b> in Visual Basic).
        /// </summary>
        /// <param name="name">The current name.</param>
        /// <param name="configurationSource">The source for configuration information.</param>
        /// <returns>The default database name if <paramref name="name"/> is <see langword="null"/> (<b>Nothing</b> in Visual Basic),
        /// otherwise the original value for <b>name</b>.</returns>
        public string MapName(string name, IConfigurationSource configurationSource)
        {
            if (name == null)
            {
                DynamicQueryConfigurationView view = new DynamicQueryConfigurationView(configurationSource);
                return view.DefaultDynamicQueryProvider;
            }

            return name;
        }
    }

}
