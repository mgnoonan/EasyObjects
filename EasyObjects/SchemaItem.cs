//===============================================================================
// NCI.EasyObjects library
// SchemaItem class
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
using System.Data;
using System.Collections;
using System.Runtime.Serialization;

namespace NCI.EasyObjects
{
    /// <summary>
    /// An enumeration for justifying string fields in the database.
    /// </summary>
    /// <remarks>This has been depracated in anticipation of removal.</remarks>
    public enum SchemaItemJustify : int
    {
        /// <summary>
        /// No justification.
        /// </summary>
        None = 0,
        /// <summary>
        /// The value should be padded to the right with spaces to the maximum length of the field
        /// </summary>
        Left,
        /// <summary>
        /// The value should be padded to the left with spaces so that the value's last character is at the length of the field
        /// </summary>
        Right
    }

    /// <summary>
    /// A representation of meta data about the columns in a database.
    /// </summary>
    public class SchemaItem
    {
        private string _fieldName;
        private SchemaItemJustify _justify;
        private int _len;
        private DbType _dbType;
        private bool _isAutoKey = false;
        private bool _isNullable = false;
        private bool _isComputed = false;
        private bool _isInPrimaryKey = false;
        private bool _isInForeignKey = false;
        private bool _hasDefault = false;
        private bool _isRowID = false;
        private Hashtable _properties = new Hashtable();

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaItem"/> with meta data from the table
        /// </summary>
        /// <param name="fieldName">The name of the database column</param>
        /// <param name="dbType">The datatype of the column</param>
        public SchemaItem(string fieldName, DbType dbType)
        {
            _fieldName = fieldName;
            _justify = SchemaItemJustify.None;
            _len = 0;
            _dbType = dbType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaItem"/> with meta data from the table
        /// </summary>
        /// <param name="fieldName">The name of the database column</param>
        /// <param name="dbType">The datatype of the column</param>
        /// <param name="isAutoKey">A flag indicating if this <see cref="SchemaItem"/> is an autokey field</param>
        public SchemaItem(string fieldName, DbType dbType, bool isAutoKey)
        {
            _fieldName = fieldName;
            _justify = SchemaItemJustify.None;
            _len = 0;
            _dbType = dbType;
            _isAutoKey = isAutoKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaItem"/> with meta data from the table
        /// </summary>
        /// <param name="fieldName">The name of the database column</param>
        /// <param name="dbType">The datatype of the column</param>
        /// <param name="justify"><see cref="SchemaItemJustify"/> value indicating the justification of the string value</param>
        /// <param name="len">The length of the string value</param>
        public SchemaItem(string fieldName, DbType dbType, SchemaItemJustify justify, int len)
        {
            _fieldName = fieldName;
            _justify = justify;
            _len = len;
            _dbType = dbType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaItem"/> with meta data from the table
        /// </summary>
        /// <param name="fieldName">The name of the database column</param>
        /// <param name="dbType">The datatype of the column</param>
        /// <param name="isAutoKey">A flag indicating if this <see cref="SchemaItem"/> is an autokey field</param>
        /// <param name="isNullable">A flag indicating if this <see cref="SchemaItem"/> can contain a NULL value in the database</param>
        /// <param name="isComputed">A flag indicating if this <see cref="SchemaItem"/> is a computed field</param>
        /// <param name="isInPrimaryKey">A flag indicating if this <see cref="SchemaItem"/> is part of the primary key for the table</param>
        /// <param name="isInForeignKey">A flag indicating if this <see cref="SchemaItem"/> is part of a foreign key</param>
        public SchemaItem(string fieldName, DbType dbType, bool isAutoKey, bool isNullable, bool isComputed, bool isInPrimaryKey, bool isInForeignKey)
        {
            _fieldName = fieldName;
            _justify = SchemaItemJustify.None;
            _len = 0;
            _dbType = dbType;
            _isAutoKey = isAutoKey;
            _isNullable = isNullable;
            _isComputed = isComputed;
            _isInPrimaryKey = isInPrimaryKey;
            _isInForeignKey = isInForeignKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaItem"/> with meta data from the table
        /// </summary>
        /// <param name="fieldName">The name of the database column</param>
        /// <param name="dbType">The datatype of the column</param>
        /// <param name="isAutoKey">A flag indicating if this <see cref="SchemaItem"/> is an autokey field</param>
        /// <param name="isNullable">A flag indicating if this <see cref="SchemaItem"/> can contain a NULL value in the database</param>
        /// <param name="isComputed">A flag indicating if this <see cref="SchemaItem"/> is a computed field</param>
        /// <param name="isInPrimaryKey">A flag indicating if this <see cref="SchemaItem"/> is part of the primary key for the table</param>
        /// <param name="isInForeignKey">A flag indicating if this <see cref="SchemaItem"/> is part of a foreign key</param>
        /// <param name="hasDefault">A flag indicating if this <see cref="SchemaItem"/> has a default value</param>
        public SchemaItem(string fieldName, DbType dbType, bool isAutoKey, bool isNullable, bool isComputed, bool isInPrimaryKey, bool isInForeignKey, bool hasDefault)
        {
            _fieldName = fieldName;
            _justify = SchemaItemJustify.None;
            _len = 0;
            _dbType = dbType;
            _isAutoKey = isAutoKey;
            _isNullable = isNullable;
            _isComputed = isComputed;
            _isInPrimaryKey = isInPrimaryKey;
            _isInForeignKey = isInForeignKey;
            _hasDefault = hasDefault;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaItem"/> with meta data from the table
        /// </summary>
        /// <param name="fieldName">The name of the database column</param>
        /// <param name="dbType">The datatype of the column</param>
        /// <param name="len">The maximum length of the column</param>
        /// <param name="isAutoKey">A flag indicating if this <see cref="SchemaItem"/> is an autokey field</param>
        /// <param name="isNullable">A flag indicating if this <see cref="SchemaItem"/> can contain a NULL value in the database</param>
        /// <param name="isComputed">A flag indicating if this <see cref="SchemaItem"/> is a computed field</param>
        /// <param name="isInPrimaryKey">A flag indicating if this <see cref="SchemaItem"/> is part of the primary key for the table</param>
        /// <param name="isInForeignKey">A flag indicating if this <see cref="SchemaItem"/> is part of a foreign key</param>
        public SchemaItem(string fieldName, DbType dbType, int len, bool isAutoKey, bool isNullable, bool isComputed, bool isInPrimaryKey, bool isInForeignKey)
        {
            _fieldName = fieldName;
            _justify = SchemaItemJustify.None;
            _len = len;
            _dbType = dbType;
            _isAutoKey = isAutoKey;
            _isNullable = isNullable;
            _isComputed = isComputed;
            _isInPrimaryKey = isInPrimaryKey;
            _isInForeignKey = isInForeignKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaItem"/> with meta data from the table
        /// </summary>
        /// <param name="fieldName">The name of the database column</param>
        /// <param name="dbType">The datatype of the column</param>
        /// <param name="justify"><see cref="SchemaItemJustify"/> value indicating the justification of the string value</param>
        /// <param name="len">The maximum length of the column</param>
        /// <param name="isNullable">A flag indicating if this <see cref="SchemaItem"/> can contain a NULL value in the database</param>
        /// <param name="isInPrimaryKey">A flag indicating if this <see cref="SchemaItem"/> is part of the primary key for the table</param>
        /// <param name="isInForeignKey">A flag indicating if this <see cref="SchemaItem"/> is part of a foreign key</param>
        public SchemaItem(string fieldName, DbType dbType, SchemaItemJustify justify, int len, bool isNullable, bool isInPrimaryKey, bool isInForeignKey)
        {
            _fieldName = fieldName;
            _justify = justify;
            _len = len;
            _dbType = dbType;
            _isNullable = isNullable;
            _isInPrimaryKey = isInPrimaryKey;
            _isInForeignKey = isInForeignKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SchemaItem"/> with meta data from the table
        /// </summary>
        /// <param name="fieldName">The name of the database column</param>
        /// <param name="dbType">The datatype of the column</param>
        /// <param name="justify"><see cref="SchemaItemJustify"/> value indicating the justification of the string value</param>
        /// <param name="len">The maximum length of the column</param>
        /// <param name="isNullable">A flag indicating if this <see cref="SchemaItem"/> can contain a NULL value in the database</param>
        /// <param name="isInPrimaryKey">A flag indicating if this <see cref="SchemaItem"/> is part of the primary key for the table</param>
        /// <param name="isInForeignKey">A flag indicating if this <see cref="SchemaItem"/> is part of a foreign key</param>
        /// <param name="hasDefault">A flag indicating if this <see cref="SchemaItem"/> has a default value</param>
        public SchemaItem(string fieldName, DbType dbType, SchemaItemJustify justify, int len, bool isNullable, bool isInPrimaryKey, bool isInForeignKey, bool hasDefault)
        {
            _fieldName = fieldName;
            _justify = justify;
            _len = len;
            _dbType = dbType;
            _isNullable = isNullable;
            _isInPrimaryKey = isInPrimaryKey;
            _isInForeignKey = isInForeignKey;
            _hasDefault = hasDefault;
        }

        /// <summary>
        /// The datatype of the column
        /// </summary>
        public virtual DbType DBType
        { get { return _dbType; } }

        /// <summary>
        /// The <see cref="SchemaItemJustify"/> value indicating the field justification
        /// </summary>
        /// <remarks>This has been depracated in anticipation of removal.</remarks>
        public virtual SchemaItemJustify Justify
        { get { return _justify; } }

        /// <summary>
        /// The maximum length of the column
        /// </summary>
        public virtual int Length
        { get { return _len; } }

        /// <summary>
        /// The column name
        /// </summary>
        public virtual string FieldName
        { get { return _fieldName; } }

        /// <summary>
        /// Indicates this column's value is generated automatically
        /// </summary>
        public virtual bool IsAutoKey
        { get { return _isAutoKey; } }

        /// <summary>
        /// Indicates this column can be set to null. 
        /// <seealso cref="EasyObject.SetColumnNull"/>
        /// </summary>
        public virtual bool IsNullable
        { get { return _isNullable; } }

        /// <summary>
        /// Indicates this column contains a computed value.
        /// </summary>
        public virtual bool IsComputed
        { get { return _isComputed; } set { _isComputed = value; } }

        /// <summary>
        /// Indicates this column is part of the primary key for the table.
        /// </summary>
        public virtual bool IsInPrimaryKey
        { get { return _isInPrimaryKey; } }

        /// <summary>
        /// Indicates this column is part of a foreign key for another table.
        /// </summary>
        public virtual bool IsInForeignKey
        { get { return _isInForeignKey; } }

        /// <summary>
        /// Indicates this column has a default value defined in the database engine.
        /// </summary>
        public virtual bool HasDefault
        { get { return _hasDefault; } }

        /// <summary>
        /// Indicates this column acts as a row identifier.
        /// </summary>
        public virtual bool IsRowID
        { get { return _isRowID; } set { _isRowID = value; } }

        /// <summary>
        /// A HashTable to which custom properties may be added
        /// </summary>
        /// <remarks>These are generally only used by the custom dynamic query providers</remarks>
        public virtual Hashtable Properties
        { get { return _properties; } }
    }
}
