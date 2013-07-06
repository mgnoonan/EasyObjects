//===============================================================================
// NCI.EasyObjects library
// Schema class
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
using System.Collections;

namespace NCI.EasyObjects
{
	/// <summary>
	/// Represents a database Schema for an EasyObject.
	/// </summary>
	public abstract class Schema
	{
		/// <summary>
		/// The default constructor
		/// </summary>
		public Schema() {}

		/// <summary>
		/// An internal collection of <see cref="SchemaEntries"/>.
		/// <seealso cref="SchemaEntries"/>
		/// </summary>
		/// <remarks>Used in derived classes.</remarks>
		public virtual ArrayList SchemaEntries 
		{
			get 
			{
				return null;
			}
		}

        /// <summary>
        /// Searches through the current <see cref="SchemaEntries"/> for a matching column
        /// </summary>
        /// <param name="columnName">The name of the column to retrieve the SchemaItem for</param>
        /// <returns>A SchemaItem that matches the columnName, or null for no matches</returns>
        public virtual SchemaItem FindSchemaItem(string columnName)
        {
            if (this.SchemaEntries.Count == 0)
                return null;

            for (int i = 0; i < this.SchemaEntries.Count; i++)
            {
                SchemaItem si = (SchemaItem)this.SchemaEntries[i];
                if (si.FieldName == columnName)
                {
                    return si;
                }
            }

            return null;
        }
    }
}
