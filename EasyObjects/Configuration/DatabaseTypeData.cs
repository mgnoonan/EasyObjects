//===============================================================================
// NCI.EasyObjects library
// DatabaseTypeData
//===============================================================================
// Copyright 2005 © Noonan Consulting Inc. All rights reserved.
// Adapted from Mike Griffin's dOOdads architecture. Used by permission.
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Xml.Serialization;

namespace NCI.EasyObjects.Configuration
{
    /// <summary>
    /// <para>Represents a type of database (e.g. Oracle, Sql Server) in configuration.</para>
    /// </summary>
    /// <remarks>
    /// <para>The class maps to the <c>databaseType</c> element in configuration.</para>
    /// </remarks>
    [XmlRoot("databaseType", Namespace=DynamicQuerySettings.ConfigurationNamespace)]
    public class DatabaseTypeData
    {
        private string name;
        private string typeName;

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="DatabaseTypeData"/> class.</para>
        /// </summary>
        public DatabaseTypeData()
        {
            this.name = string.Empty;
            this.typeName = string.Empty;
        }

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="DatabaseTypeData"/> class with the name and the fully qualified type name of the class.</para>
        /// </summary>
        /// <param name="name">
        /// <para>The name of the database type.</para>
        /// </param>
        /// <param name="typeName">
        /// <para>The fully qualified type name of the assembly.</para>
        /// </param>
        public DatabaseTypeData(string name, string typeName) : this()
        {
            this.name = name;
            this.typeName = typeName;
        }

        /// <summary>
        /// <para>Gets or sets the name of the <see cref="DatabaseTypeData"/>.</para>
        /// </summary>
        /// <value>
        /// <para>The name of the <see cref="DatabaseTypeData"/>. The default is an empty string.</para>
        /// </value>
        /// <remarks>
        /// <para>This property maps to the <c>name</c> attribute in configuration.</para>
        /// </remarks>
        [XmlAttribute("name")]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        /// <summary>
        /// <para>Gets or sets the fully qualified type name that implements this type.</para>
        /// </summary>
        /// <value>
        /// <para>The fully qualified type name that implements this type.</para>
        /// </value>
        /// <remarks>
        /// <para>This property maps to the <c>type</c> attribute in configuration.</para>
        /// </remarks>
        [XmlAttribute("type")]
        public string TypeName
        {
            get { return this.typeName; }
            set { this.typeName = value; }
        }
    }
}