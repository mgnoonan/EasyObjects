//===============================================================================
// NCI.EasyObjects library
// InstanceDataCollection
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
    /// <para>Represents a collection of <see cref="InstanceData"/> objects for the <see cref="EasyObject"/> in configuration for the block.</para>
    /// </summary>   
    [Serializable]
    public class InstanceDataCollection : DataCollection
    {
        /// <summary>
        /// <para>Gets or sets the <see cref="InstanceData"/> at the specified <paramref name="index"/>.</para>
        /// </summary>
        /// <param name="index">
        /// <para>The index of the <see cref="InstanceData"/> to get or set.</para>
        /// </param>
        /// <value>
        /// <para>The value associated with the specified <paramref name="index"/>. If the specified <paramref name="index"/> is not found, attempting to get it returns a <see langword="null"/> reference (Nothing in Visual Basic), and attempting to set it creates a new entry using the specified <paramref name="index"/>.</para>
        /// </value>
        public InstanceData this[int index]
        {
            get { return (InstanceData)BaseGet(index); }
            set { BaseSet(index, value); }
        }

        /// <summary>
        /// <para>Gets or sets the <see cref="InstanceData"/> associated with the specified <paramref name="name"/>.</para>
        /// </summary>
        /// <param name="name">
        /// <para>The name of the <see cref="InstanceData"/> to get or set.</para>
        /// </param>
        /// <value>
        /// <para>The value associated with the specified <paramref name="name"/>. If the specified <paramref name="name"/> is not found, attempting to get it returns a <see langword="null"/> reference (Nothing in Visual Basic), and attempting to set it creates a new entry using the specified <paramref name="name"/>.</para>
        /// </value>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="name"/> is a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </exception>
        public InstanceData this[string name]
        {
            get
            {
                ArgumentValidation.CheckForNullReference(name, "name");

                return BaseGet(name) as InstanceData;
            }
            set
            {
                ArgumentValidation.CheckForNullReference(name, "name");

                BaseSet(name, value);
            }
        }

        /// <summary>
        /// <para>Adds an <see cref="InstanceData"/> into the collection.</para>
        /// </summary>
        /// <param name="instanceData">
        /// <para>The <see cref="InstanceData"/> to add. The value can not be a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </param>
        /// <remarks>
        /// <para>If a reference already exists in the collection by <seealso cref="InstanceData.Name"/>, it will be replaced with the new reference.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <para><paramref name="instanceData"/> is a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// <para><seealso cref="InstanceData.Name"/> is a <see langword="null"/> reference (Nothing in Visual Basic).</para>
        /// </exception>
        public void Add(InstanceData instanceData)
        {
            ArgumentValidation.CheckForNullReference(instanceData, "instanceData");
            ArgumentValidation.CheckForInvalidNullNameReference(instanceData.Name, typeof(InstanceData).FullName);

            BaseAdd(instanceData.Name, instanceData);
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
        /// <para><paramref name="parameter"/> must be of type <see cref="InstanceData"/>.</para>
        /// </exception>
        public void Add(object parameter)
        {
            Add((InstanceData)parameter);
        }

        /// <summary>
        /// <para>Copies the entire <see cref="InstanceDataCollection"/> to a compatible one-dimensional <see cref="Array"/>, starting at the specified index of the target array.</para>
        /// </summary>
        /// <param name="array">
        /// <para>The one-dimensional <see cref="InstanceDataCollection"/> array that is the destination of the elements copied from <see cref="InstanceDataCollection"/>. The <see cref="InstanceData"/> array must have zero-based indexing.</para>
        /// </param>
        /// <param name="index">
        /// <para>The zero-based index in array at which copying begins.</para>
        /// </param>
        public void CopyTo(InstanceData[] array, int index)
        {
            base.CopyTo(array, index);
        }
    }
}