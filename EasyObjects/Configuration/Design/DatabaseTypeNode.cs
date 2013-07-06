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
using System.ComponentModel;
using System.Drawing.Design;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design;
using Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Validation;

namespace NCI.EasyObjects.Configuration.Design
{
    /// <summary>
    /// <para>
    /// Represents a database type in the data settings for an application.
    /// </para>
    /// </summary>
    [Image(typeof(DatabaseTypeNode))]
    public class DatabaseTypeNode : ConfigurationNode
    {
        private DatabaseTypeData databaseTypeData;

        /// <summary>
        /// <para>
        /// Initialize a new instance of the <see cref="DatabaseTypeNode"/> class.
        /// </para>
        /// </summary>
        public DatabaseTypeNode() : this(new DatabaseTypeData(SR.DefaultDatabaseTypeNodeName, string.Empty))
        {
        }

        /// <summary>
        /// <para>
        /// Initialize a new instance of the <see cref="DatabaseTypeNode"/> class with a <see cref="DatabaseTypeData"/> object.
        /// </para>
        /// </summary>
        /// <param name="databaseType">
        /// <para>The <see cref="DatabaseTypeData"/> runtime configuration.</para>
        /// </param>
        public DatabaseTypeNode(DatabaseTypeData databaseType) : base()
        {
            if (databaseType == null)
            {
                throw new ArgumentNullException("databaseType");
            }
            this.databaseTypeData = databaseType;
        }

        /// <summary>
        /// <para>
        /// Gets or sets the fully qualified type name of the instance.
        /// </para>
        /// </summary>
        /// <value>
        /// <para>
        /// The name of the connection string.
        /// </para>
        /// </value>
        [Required]
        [Editor(typeof(TypeSelectorEditor), typeof(UITypeEditor))]
        [BaseType(typeof(DynamicQuery))]
        [SRCategory(SR.Keys.CategoryGeneral)]
        [SRDescription(SR.Keys.DatabaseTypeFullyQualifedNameDescription)]
        public string TypeName
        {
            get { return this.databaseTypeData.TypeName; }
            set { this.databaseTypeData.TypeName = value; }
        }

        /// <summary>
        /// <para>Gets the <see cref="DatabaseTypeData"/> configuration.</para>
        /// </summary>
        /// <returns>
        /// <para>The <see cref="DatabaseTypeData"/> configuration.</para>
        /// </returns>
        [Browsable(false)]
        public virtual DatabaseTypeData DatabaseTypeData
        {
            get { return this.databaseTypeData; }
        }

        /// <summary>
        /// <para>Sets the <seeals cref="ConfigurationNode.Name"/> to the <seealso cref="ParameterData.Name"/></para>
        /// </summary>
        protected override void OnSited()
        {
            base.OnSited();
            Site.Name = databaseTypeData.Name;
        }

        /// <summary>
        /// <para>Sets the <seeals cref="Microsoft.Practices.EnterpriseLibrary.Data.Configuration.DatabaseTypeData.Name"/> to the <seealso cref="ConfigurationNode.Name"/></para>
        /// </summary>
        /// <param name="e"><para>A <see cref="ConfigurationNodeChangedEventArgs"/> that contains the event data.</para></param>
        protected override void OnRenamed(ConfigurationNodeChangedEventArgs e)
        {
            base.OnRenamed(e);
            databaseTypeData.Name = e.Node.Name;
        }
    }
}