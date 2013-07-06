//===============================================================================
// NCI.EasyObjects library
// InstanceData
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
    /// <para>Represents a database instance in configuration.</para>
    /// </summary>
    /// <remarks>
    /// <para>The class maps to the <c>instance</c> element in configuration.</para>
    /// </remarks>
    [XmlRoot("instance", Namespace=DynamicQuerySettings.ConfigurationNamespace)]
    public class InstanceData
    {
        private string name;
        private string typeName;
//        private string connectionString;

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="InstanceData"/> class.</para>
        /// </summary>
        public InstanceData()
        {
            this.name = string.Empty;
            this.typeName = string.Empty;
//            this.connectionString = string.Empty;
        }

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="InstanceData"/> class with a name, name of the <see cref="DatabaseTypeData"/> and the name of the <see cref="DynamicQuery"/> provider.</para>
        /// </summary>
        /// <param name="name"><para>The name of the instance.</para></param>        
        public InstanceData(string name) : this(name, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// <para>Initialize a new instance of the <see cref="InstanceData"/> class with a name, name of the <see cref="DatabaseTypeData"/> and the name of the <see cref="DynamicQuery"/> provider.</para>
        /// </summary>
        /// <param name="name">The name of the instance.</param>
        /// <param name="typeName">The fully qualified assembly name of the type.</param>
        /// <param name="connectionString">The connection string for the instance.</param>
        public InstanceData(string name, string typeName, string connectionString) : this()
        {
            this.name = name;
            this.typeName = typeName;
//            this.connectionString = connectionString;
        }

        /// <summary>
        /// <para>Gets or sets the name of the instance.</para>
        /// </summary>
        /// <value>
        /// <para>The name of the instance. The default is an empty string.</para>
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
        /// <para>Gets or sets the type of the instance.</para>
        /// </summary>
        /// <value>
        /// <para>The type of the instance. The default is an empty string.</para>
        /// </value>
        /// <remarks>
        /// <para>This property maps to the <c>type</c> attribute in configuration.</para>
        /// </remarks>
        [XmlAttribute("type")]
        public string DatabaseTypeName
        {
            get { return this.typeName; }
            set { this.typeName = value; }
        }

//        /// <summary>
//        /// <para>Gets or sets name of the <see cref="ConnectionString"/> for the instance.</para>
//        /// </summary>
//        /// <value>
//        /// <para>Gets or sets name of the <see cref="ConnectionString"/> for the instance. The default is an empty string.</para>
//        /// </value>
//        /// <remarks>
//        /// <para>This property maps to the <c>connectionString</c> attribute in configuration.</para>
//        /// </remarks>
//        [XmlAttribute("connectionString")]
//        public string ConnectionString
//        {
//            get { return this.connectionString; }
//            set { this.connectionString = value; }
//        }
    }

}