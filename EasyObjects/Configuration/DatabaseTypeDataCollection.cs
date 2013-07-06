//===============================================================================
// NCI.EasyObjects library
// DatabaseTypeDataCollection
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
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace NCI.EasyObjects.Configuration
{
    /// <summary>
    /// <para>Represents a collection of <see cref="DatabaseTypeData"/>s for the <see cref="DynamicQuery"/> in configuration for the block.</para>
    /// </summary>     
    [Serializable]
    public class DatabaseTypeDataCollection : DataCollection
    {
        /// <summary>
        /// <para>Gets or sets the <see cref="DatabaseTypeData"/> at the specified <paramref name="index"/>.</para>
        /// </summary>
        /// <param name="index">
        /// <para>The index of the <see cref="DatabaseTypeData"/> to get or set.</para>
        /// </param>
        /// <value>
        /// <para>The value associated with the specified <paramref name="index"/>. If the specified <paramref name="index"/> is not found, attempting to get it returns a <see langword="null"/> reference (Nothing in Visual Basic), and attempting to set it creates a new entry using the specified <paramref name="index"/>.</para>
        /// </value>
        public DatabaseTypeData this[int index]
        {
            get { return (DatabaseTypeData)BaseGet(index); }
            set { BaseSet(index, value); }
        }

        /// <summary>
        /// <para>Gets or sets the <see cref="DatabaseTypeData"/> associated with the specified <paramref name="name"/>.</para>
        /// </summary>
        /// <param name="name">
        /// <para>The name of the <see cref="DatabaseTypeData"/> to get or set.</para>
        /// </param>
        /// <value>
        /// <para>The value associated with the specified <paramref name="name"/>. If the specified <paramref name="name"/> is not found, attempting to get it returns a <see langword="null"/> reference (Nothing in Visual Basic), and attempting to set it creates a new entry using the specified <paramref name="name"/>.</para>
        /// </value>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="name"/> is a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </exception>
        public DatabaseTypeData this[string name]
        {
            get
            {
                ArgumentValidation.CheckForNullReference(name, "name");

                return BaseGet(name) as DatabaseTypeData;
            }
            set
            {
                ArgumentValidation.CheckForNullReference(name, "name");

                BaseSet(name, value);
            }
        }

        /// <summary>
        /// <para>Adds an <see cref="DatabaseTypeData"/> into the collection.</para>
        /// </summary>
        /// <param name="databaseTypeData">
        /// <para>The <see cref="DatabaseTypeData"/> to add. The value can not be a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </param>
        /// <remarks>
        /// <para>If a reference already exists in the collection by <seealso cref="DatabaseTypeData.Name"/>, it will be replaced with the new reference.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="databaseTypeData"/> is a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <para><seealso cref="DatabaseTypeData.Name"/> is a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </exception>
        public void Add(DatabaseTypeData databaseTypeData)
        {
            ArgumentValidation.CheckForNullReference(databaseTypeData, "databaseTypeData");
            ArgumentValidation.CheckForInvalidNullNameReference(databaseTypeData.Name, typeof(DatabaseTypeData).FullName);

            BaseAdd(databaseTypeData.Name, databaseTypeData);
        }

        /// <summary>
        /// <para>Adds a value into the collection.</para>
        /// </summary>
        /// <param name="parameter">
        /// <para>The value to add. The value can not be a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </param>
        /// <remarks>
        /// <para>This method exists to support Xml Serialization.</para>
        /// </remarks>
        /// <exception cref="InvalidCastException">
        /// <para><paramref name="parameter"/> must be of type <see cref="DatabaseTypeData"/>.</para>
        /// </exception>
        public void Add(object parameter)
        {
            Add((DatabaseTypeData)parameter);
        }

        /// <summary>
        /// <para>Copies the entire <see cref="DatabaseTypeDataCollection"/> to a compatible one-dimensional <see cref="Array"/>, starting at the specified index of the target array.</para>
        /// </summary>
        /// <param name="array">
        /// <para>The one-dimensional <see cref="DatabaseTypeDataCollection"/> array that is the destination of the elements copied from <see cref="DatabaseTypeDataCollection"/>. The <see cref="DatabaseTypeData"/> array must have zero-based indexing.</para>
        /// </param>
        /// <param name="index">
        /// <para>The zero-based index in array at which copying begins.</para>
        /// </param>
        public void CopyTo(DatabaseTypeData[] array, int index)
        {
            base.CopyTo(array, index);
        }
    }

}