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

namespace NCI.EasyObjects.Configuration.Design
{
    /// <devdoc>
    /// Represents a localized vertion of a DescriptionAttribute. It uses the local string resources for localization.
    /// This class cannot be inherited from 
    /// </devdoc>
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class SRDescriptionAttribute : DescriptionAttribute
    {
        private bool replaced;

        internal SRDescriptionAttribute(string description) : base(description)
        {
        }

        public override string Description
        {
            get
            {
                if (!this.replaced)
                {
                    this.replaced = true;
                    base.DescriptionValue = SR.Keys.GetString(base.Description);
                }
                return base.Description;
            }
        }
    }
}