//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Logging Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Configuration;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using NCI.EasyObjects.Properties;

namespace NCI.EasyObjects.Configuration
{
    /// <summary>
    /// Custom <see cref="PolymorphicConfigurationElementCollection{T}"/> that deals with <see cref="DynamicQueryData"/>.
    /// </summary>
    /// <remarks>
    /// The default implementation based on annotations on the feature types can't be used because trace listeners can't be annotated.
    /// </remarks>
    public class DynamicQueryDataCollection : PolymorphicConfigurationElementCollection<DynamicQueryData>
    {
        /// <summary>
        /// Returns the <see cref="ConfigurationElement"/> type to created for the current xml node.
        /// </summary>
        /// <remarks>
        /// The <see cref="TraceListenerData"/> include the configuration object type as a serialized attribute.
        /// </remarks>
        /// <param name="reader">The <see cref="XmlReader"/> that is deserializing the element.</param>
        protected override Type RetrieveConfigurationElementType(XmlReader reader)
        {
            Type configurationElementType = null;

            if (reader.AttributeCount > 0)
            {
                // expect the first attribute to be the name
                for (bool go = reader.MoveToFirstAttribute(); go; go = reader.MoveToNextAttribute())
                {
                    if (DynamicQueryData.typeProperty.Equals(reader.Name))
                    {
                        configurationElementType = Type.GetType(reader.Value);
                        if (configurationElementType == null)
                        {
                            throw new ConfigurationErrorsException(
                                string.Format(
                                    Resources.Culture,
                                    "Invalid DynamicQueryData type in configuration '{0}'.",
                                    reader.ReadOuterXml()));
                        }

                        break;
                    }
                }

                if (configurationElementType == null)
                {
                    throw new ConfigurationErrorsException(
                        string.Format(
                            Resources.Culture,
                            "Missing DynamicQueryData type in configuration '{0}'.",
                            reader.ReadOuterXml()));
                }

                // cover the traces
                reader.MoveToElement();
            }

            return configurationElementType;
        }
    }
}
