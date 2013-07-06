//===============================================================================
// NCI.EasyObjects library
// ValueParameter class
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

namespace NCI.EasyObjects
{
	/// <summary>
	/// Represents a database parameter and value.
	/// </summary>
	public class ValueParameter
	{
		/// <summary>
		/// The <see cref="SchemaItem"/> used to create the parameter.
		/// </summary>
		protected SchemaItem _schemaItem;
		/// <summary>
		/// The column name for the parameter.
		/// </summary>
		protected string _column;
		/// <summary>
		/// The value to assign to the parameter.
		/// </summary>
		protected object _value = null;

		/// <summary>
		/// Initializes a new instance of <see cref="ValueParameter"/>.
		/// </summary>
		/// <param name="item">A <see cref="SchemaItem"/> for the parameter</param>
		public ValueParameter(SchemaItem item) 
		{
			this._schemaItem = item;
			this._column = item.FieldName;
		}
	
		/// <summary>
		/// Returns the current <see cref="SchemaItem"/> for the parameter
		/// </summary>
		public virtual SchemaItem SchemaItem
		{
			get { return _schemaItem; }
		}
	
		/// <summary>
		/// The column that the parameter is going to query against. 
		/// </summary>
		public virtual string Column
		{
			get	{ return _column; }
		}
	
		/// <summary>
		/// The value that will be placed into the Parameter
		/// </summary>
		public virtual object Value
		{
			get	{ return _value; }
			set	{ _value = value; }
		}
	}
}
