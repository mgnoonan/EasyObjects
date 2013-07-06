//===============================================================================
// NCI.EasyObjects library
// Sql2005DynamicQuery
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
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using NCI.EasyObjects.Configuration;

namespace NCI.EasyObjects.DynamicQueryProvider
{
    /// <summary>
    /// Represents provider-specific <see cref="DynamicQuery"/> functions for SQL Server.
    /// </summary>
    [DynamicQueryAssembler(typeof(Sql2005DynamicQueryAssembler))]
    class Sql2005DynamicQuery : SqlServerDynamicQuery
    {
        		/// <summary>
		/// Initializes a new instance of <see cref="SqlServerDynamicQuery"/>.
		/// </summary>
		public Sql2005DynamicQuery() : base()
		{}

		/// <summary>
		/// Initialize a new instance of the <see cref="SqlServerDynamicQuery"/> class.
		/// </summary>
        public Sql2005DynamicQuery(EasyObject entity) : base(entity)
		{}

        /// <summary>
        /// Construct the correct WHERE clause using the Transact-SQL syntax (SQL Server 2005)
        /// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="dbCommand">A wrapper for an Enterprise Library command object</param>
        /// <param name="conjunction">The conjunction to use between parameters, usually AND or OR</param>
        /// <param name="query">The query string to append the WHERE clause to</param>
        protected override void BuildWhereClause(Database db, DbCommand dbCommand, string conjunction, StringBuilder query)
        {

            if (_whereParameters != null && _whereParameters.Count > 0)
            {
                query.Append(" WHERE ");

                bool first = true;

                bool requiresParam;

                WhereParameter wItem;
                bool skipConjunction = false;

                string paramName;
                string columnName;

                foreach (object obj in _whereParameters)
                {
                    // Maybe we injected text or a WhereParameter
                    if (obj.GetType().ToString() == "System.String")
                    {
                        string text = obj as string;
                        query.Append(text);

                        if (text == "(")
                        {
                            skipConjunction = true;
                        }
                    }
                    else
                    {
                        wItem = obj as WhereParameter;

                        if (wItem.IsDirty)
                        {
                            if (!first && !skipConjunction)
                            {
                                if (wItem.Conjunction != WhereParameter.Conj.UseDefault)
                                {
                                    if (wItem.Conjunction == WhereParameter.Conj.And)
                                        query.Append(" AND ");
                                    else
                                        query.Append(" OR ");
                                }
                                else
                                {
                                    query.Append(" " + conjunction + " ");
                                }
                            }

                            requiresParam = true;

                            columnName = string.Format(FieldFormat, wItem.SchemaItem.FieldName);
                            paramName = ParameterToken + wItem.SchemaItem.FieldName;

                            string originalParamName = paramName;
                            int count = 1;

                            // If necessary append a number to the name in order to avoid collisions
                            while (dbCommand.Parameters.Contains(paramName) || dbCommand.Parameters.Contains(paramName + "Begin"))
                            {
                                paramName = originalParamName + count++;
                            }

                            switch (wItem.Operator)
                            {
                                case WhereParameter.Operand.Equal:
                                    query.AppendFormat("{0} = {1} ", columnName, paramName);
                                    break;
                                case WhereParameter.Operand.NotEqual:
                                    query.AppendFormat("{0} <> {1} ", columnName, paramName);
                                    break;
                                case WhereParameter.Operand.GreaterThan:
                                    query.AppendFormat("{0} > {1} ", columnName, paramName);
                                    break;
                                case WhereParameter.Operand.LessThan:
                                    query.AppendFormat("{0} < {1} ", columnName, paramName);
                                    break;
                                case WhereParameter.Operand.LessThanOrEqual:
                                    query.AppendFormat("{0} <= {1} ", columnName, paramName);
                                    break;
                                case WhereParameter.Operand.GreaterThanOrEqual:
                                    query.AppendFormat("{0} >= {1} ", columnName, paramName);
                                    break;
                                case WhereParameter.Operand.StartsWith:
                                case WhereParameter.Operand.EndsWith:
                                case WhereParameter.Operand.Contains:
                                case WhereParameter.Operand.Like:
                                    if (wItem.SchemaItem.DBType == DbType.StringFixedLength)
                                    {
                                        // There is a bug in SQL Server for using nchar in the LIKE clause
                                        query.AppendFormat("{0} LIKE RTRIM({1}) ", columnName, paramName);
                                    }
                                    else
                                    {
                                        query.AppendFormat("{0} LIKE {1} ", columnName, paramName);
                                    }
                                    BuildWildcardValue(wItem);
                                    break;
                                case WhereParameter.Operand.NotStartsWith:
                                case WhereParameter.Operand.NotEndsWith:
                                case WhereParameter.Operand.NotContains:
                                case WhereParameter.Operand.NotLike:
                                    if (wItem.SchemaItem.DBType == DbType.StringFixedLength)
                                    {
                                        // There is a bug in SQL Server for using nchar in the LIKE clause
                                        query.AppendFormat("{0} NOT LIKE RTRIM({1}) ", columnName, paramName);
                                    }
                                    else
                                    {
                                        query.AppendFormat("{0} NOT LIKE {1} ", columnName, paramName);
                                    }
                                    BuildWildcardValue(wItem);
                                    break;
                                case WhereParameter.Operand.IsNull:
                                    query.AppendFormat("{0} IS NULL ", columnName);
                                    requiresParam = false;
                                    break;
                                case WhereParameter.Operand.IsNotNull:
                                    query.AppendFormat("{0} IS NOT NULL ", columnName);
                                    requiresParam = false;
                                    break;
                                case WhereParameter.Operand.In:
                                    query.AppendFormat("{0} IN ({1}) ", columnName, wItem.Value);
                                    requiresParam = false;
                                    break;
                                case WhereParameter.Operand.NotIn:
                                    query.AppendFormat("{0} NOT IN ({1}) ", columnName, wItem.Value);
                                    requiresParam = false;
                                    break;
                                case WhereParameter.Operand.Between:
                                    query.AppendFormat("{0} BETWEEN {1} AND {2}", columnName, paramName + "Begin", paramName + "End");
                                    db.AddInParameter(dbCommand, paramName + "Begin", wItem.SchemaItem.DBType, wItem.BetweenBeginValue.ToString());
                                    db.AddInParameter(dbCommand, paramName + "End", wItem.SchemaItem.DBType, wItem.BetweenEndValue.ToString());
                                    requiresParam = false;
                                    break;
                            }

                            if (requiresParam)
                            {
                                db.AddInParameter(dbCommand, paramName, wItem.SchemaItem.DBType, wItem.Value);
                            }

                            first = false;
                            skipConjunction = false;
                        }
                    }
                }
            }
        }
    }
}
