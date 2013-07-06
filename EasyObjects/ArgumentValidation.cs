//===============================================================================
// NCI.EasyObjects library
// ArgumentValidation
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
    /// <para>Common validation routines for argument validation.</para>
    /// </summary>
    public sealed class ArgumentValidation
    {
        private ArgumentValidation()
        {
        }

        /// <summary>
        /// <para>Check if the <paramref name="variable"/> is an embpty string.</para>
        /// </summary>
        /// <param name="variable">
        /// <para>The value to check.</para>
        /// </param>
        /// <param name="variableName">
        /// <para>The name of the variable being checked.</para>
        /// </param>
        /// <remarks>
        /// <para>Before checking the <paramref name="variable"/>, a call is made to <see cref="ArgumentValidation.CheckForNullReference"/>.</para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <pararef name="variable"/> can not be <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="variableName"/> can not be <see langword="null"/> (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <pararef name="variable"/> can not be a zero length <see cref="string"/>.
        /// </exception>
        public static void CheckForEmptyString(string variable, string variableName)
        {
            CheckForNullReference(variable, variableName);
            CheckForNullReference(variableName, "variableName");
            if (variable.Length == 0)
            {
                throw new ArgumentException(string.Format("The value of '{0}' can not be an empty string.", variableName));
            }
        }

        /// <summary>
        /// <para>Check if the <paramref name="variable"/> is <see langword="null"/> (Nothing in Visual Basic).</para>
        /// </summary>
        /// <param name="variable">
        /// <para>The value to check.</para>
        /// </param>
        /// <param name="variableName">
        /// <para>The name of the variable being checked.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <pararef name="variable"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="variableName"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// </exception>
        public static void CheckForNullReference(object variable, string variableName)
        {
            if (variableName == null)
            {
                throw new ArgumentNullException("variableName");
            }

            if (null == variable)
            {
                throw new ArgumentNullException(variableName);
            }
        }

        /// <summary>
        /// Validates that the input messageName is neither null nor empty
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="messageName">Parameter value</param>
        public static void CheckForInvalidNullNameReference(string name, string messageName)
        {
            if ((null == name) || (name.Length == 0))
            {
                throw new InvalidOperationException(string.Format("The name for the '{0}' can not be null or an empty string.", messageName));
            }
        }

        /// <summary>
        /// <para>Checks <paramref name="bytes"/> for zero length and throw an <see cref="ArgumentException"/> if the length equals zero.</para>
        /// </summary>
        /// <param name="bytes">
        /// The <see cref="byte"/> array to check.
        /// </param>
        /// <param name="variableName">
        /// <para>The name of the variable being checked.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <pararef name="variable"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="variableName"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="bytes"/> can not be zero length.</para>
        /// </exception>
        public static void CheckForZeroBytes(byte[] bytes, string variableName)
        {
            CheckForNullReference(bytes, "bytes");
            CheckForNullReference(variableName, "variableName");
            if (bytes.Length == 0)
            {
                throw new ArgumentException("The value must be greater than 0 bytes.", variableName);
            }
        }

        /// <summary>
        /// <para>Check <paramref name="variable"/> to determine if it matches the <see cref="Type"/> of <paramref name="type"/>.</para>
        /// </summary>
        /// <param name="variable">
        /// <para>The value to check.</para>
        /// </param>
        /// <param name="type">
        /// <para>The <see cref="Type"/> expected type of <paramref name="variable"/>.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <pararef name="variable"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="typeName"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="variable"/> is not the expected <see cref="Type"/>.
        /// </exception>
        public static void CheckExpectedType(object variable, Type type)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(type, "type");
            if (!type.IsAssignableFrom(variable.GetType()))
            {
                throw new ArgumentException(string.Format("The type is invalid. Expected type '{0}'.", type.FullName));
            }
        }

        /// <summary>
        /// <para>Check <paramref name="variable"/> to determine if it is a valid defined enumeration for <paramref name="enumType"/>.</para>
        /// </summary>
        /// <param name="variable">
        /// <para>The value to check.</para>
        /// </param>
        /// <param name="enumType">
        /// <para>The <see cref="Type"/> expected type of <paramref name="variable"/>.</para>
        /// </param>
        /// <param name="variableName">
        /// <para>The name of the variable being checked.</para>
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <pararef name="variable"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="enumType"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// <para>- or -</para>
        /// <pararef name="variableName"/> can not <see langword="null"/> (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="variable"/> is not the expected <see cref="Type"/>.
        /// <para>- or -</para>
        /// <par><paramref name="enumType"/> is not an <see cref="Enum"/>. </par>
        /// </exception>
        public static void CheckEnumeration(Type enumType, object variable, string variableName)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(enumType, "enumType");
            CheckForNullReference(variableName, "variableName");

            if (!Enum.IsDefined(enumType, variable))
            {
                throw new ArgumentException(string.Format("{0} is not a valid value for {1}.", variable.ToString(), enumType.FullName), variableName);
            }
        }
    }
}