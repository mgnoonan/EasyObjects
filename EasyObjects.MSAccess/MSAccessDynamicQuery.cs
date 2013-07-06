//===============================================================================
// NCI.EasyObjects library
// MSAccessDynamicQuery
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
    [DynamicQueryAssembler(typeof(MSAccessDynamicQueryAssembler))]
    public class MSAccessDynamicQuery : DynamicQuery
	{
		/// <summary>
		/// Initializes a new instance of <see cref="MSAccessDynamicQuery"/>.
		/// </summary>
		public MSAccessDynamicQuery() : base()
		{}

		/// <summary>
		/// Initialize a new instance of the <see cref="MSAccessDynamicQuery"/> class.
		/// </summary>
		public MSAccessDynamicQuery(EasyObject entity) : base(entity)
		{}
	
		/// <summary>
		/// <para>Gets the parameter token used to delimit parameters for the Sql Database.</para>
		/// </summary>
		/// <value>
		/// <para>The '@' symbol.</para>
		/// </value>
		protected override char ParameterToken
		{
			get { return '@'; }
		}

        /// <summary>
        /// <para>Gets the wildcard token used to perform LIKE queries for the Sql Database.</para>
        /// </summary>
        /// <value>
        /// <para>The '%' symbol.</para>
        /// </value>
        protected override char WildcardToken
        {
            get { return '%'; }
        }

		/// <summary>
		/// <para>Gets the string format used to delimit fieldnames for the Sql Database.</para>
		/// </summary>
		/// <value>
		/// <para>'[{0}]' is the field format string for Sql Server.</para>
		/// </value>
		protected override string FieldFormat { get { return "[{0}]"; } }
	
		/// <summary>
		/// <para>Gets the string format used to separate schema names and database objects for the Sql Database.</para>
		/// </summary>
		/// <value>
		/// <para>'.' is the schema separator string for Sql Server.</para>
		/// </value>
		protected override string SchemaSeparator { get { return "."; } }

		/// <summary>
		/// <para>Gets the string format used to delimit aliases for the Sql Database.</para>
		/// </summary>
		/// <value>
		/// <para>"'{0}'" is the alias format string for Sql Server.</para>
		/// </value>
		protected override string AliasFormat { get { return "'{0}'"; } }
	
		/// <summary>
		/// Adds a field to the SELECT clause of the query
		/// </summary>
		/// <param name="fieldName">The field to add to the SELECT clause</param>
		public override void AddResultColumn(string fieldName)
		{
			base.AddResultColumnFormat(fieldName);
		}

		/// <summary>
		/// Adds a field to the ORDER BY clause of the query
		/// </summary>
		/// <param name="fieldName">The field to add to the ORDER BY clause</param>
		/// <param name="direction">A <seealso cref="WhereParameter.Dir"/> indicating which direction to sort the results</param>
		public override void AddOrderBy(string fieldName, NCI.EasyObjects.WhereParameter.Dir direction)
		{
			base.AddOrderByFormat(fieldName, direction);
		}

		/// <summary>
		/// Adds a COUNT(*) to the ORDER BY clause of the query
		/// </summary>
		/// <param name="countAll">A reference to a <seealso cref="DynamicQuery"/> object</param>
		/// <param name="direction">A <seealso cref="WhereParameter.Dir"/> indicating which direction to sort the results</param>
		public override void AddOrderBy(DynamicQuery countAll, NCI.EasyObjects.WhereParameter.Dir direction)
		{
			if(countAll.CountAll)
			{
				base.AddOrderBy("COUNT(*)", direction);
			}
		}

		/// <summary>
		/// Adds an aggregate parameter to the ORDER BY clause of the query
		/// </summary>
		/// <param name="aggregate">An <seealso cref="AggregateParameter"/> to add to the GROUP BY clause</param>
		/// <param name="direction">A <seealso cref="WhereParameter.Dir"/> indicating which direction to sort the results</param>
		public override void AddOrderBy(AggregateParameter aggregate, NCI.EasyObjects.WhereParameter.Dir direction)
		{
			base.AddOrderBy(GetAggregate(aggregate, false), direction);
		}

		/// <summary>
		/// Adds a GROUP BY parameter to the query
		/// </summary>
		/// <param name="aggregate">An <seealso cref="AggregateParameter"/> to add to the GROUP BY clause</param>	
		public override void AddGroupBy(AggregateParameter aggregate)
		{
			// SQL Server does not support aggregates in a GROUP BY.
			// Common method
			base.AddGroupBy(GetAggregate(aggregate, false));
		}

		/// <summary>
		/// Builds a provider-specific SELECT query against the <see cref="EasyObject.QuerySource"/>.
		/// <seealso cref="EasyObject.QuerySource"/>
		/// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="dbCommand">A wrapper for an Enterprise Library command object</param>
        /// <param name="conjunction">The conjunction to use between multiple <see cref="WhereParameter"/>s</param>
        protected override void BuildSelectQuery(Database db, DbCommand dbCommand, string conjunction)
		{
			bool hasColumn = false;
			bool selectAll = true;
			StringBuilder query = new StringBuilder("SELECT ");

			if( this.Distinct) query.Append("DISTINCT ");
			if( this.TopN >= 0) query.AppendFormat("TOP {0} ", this.TopN.ToString());

			if(this.ResultColumns.Length > 0)
			{
				query.Append(this.ResultColumns);
				hasColumn = true;
				selectAll = false;
			}
	 
			if(this._countAll)
			{
				if(hasColumn)
				{
					query.Append(", ");
				}
				
				query.Append("COUNT(*)");

				if(this._countAllAlias != string.Empty)
				{
					query.Append(" AS ");
					// Need DBMS string delimiter here
					query.AppendFormat(AliasFormat, this._countAllAlias);
				}
				
				hasColumn = true;
				selectAll = false;
			}
			
			if(_aggregateParameters != null && _aggregateParameters.Count > 0)
			{
				bool isFirst = true;
				
				if(hasColumn)
				{
					query.Append(", ");
				}
				
				AggregateParameter wItem;
	
				foreach(object obj in _aggregateParameters)
				{
					wItem = obj as AggregateParameter;
	
					if(wItem.IsDirty)
					{
						if(isFirst)
						{
							query.Append(GetAggregate(wItem, true));
							isFirst = false;
						}
						else
						{
							query.Append(", " + GetAggregate(wItem, true));
						}
					}
				}
				
				selectAll = false;
			}
			
			if(selectAll)
			{
				query.Append("*");
			}

			query.AppendFormat(" FROM {0}", this.QuerySourceWithSchema);

            BuildWhereClause(db, dbCommand, conjunction, query);

			if(_groupBy.Length > 0) 
			{
				query.Append(" GROUP BY " + _groupBy);
				
				if(this._withRollup)
				{
					query.Append(" WITH ROLLUP");
				}
			}
		
			if(this.OrderBy.Length > 0) 
			{
				query.AppendFormat(" ORDER BY {0}", this.OrderBy);
			}

			dbCommand.CommandText = query.ToString();
		}

		/// <summary>
		/// Builds a provider-specific UPDATE query against the <see cref="EasyObject.TableName"/>.
		/// <seealso cref="EasyObject.TableName"/>
		/// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="dbCommand">A wrapper for an Enterprise Library command object</param>
        /// <param name="conjunction">The conjunction to use between multiple <see cref="WhereParameter"/>s</param>
        protected override void BuildUpdateQuery(Database db, DbCommand dbCommand, string conjunction)
		{
			ArgumentValidation.CheckForEmptyString(this.UpdateColumns, "Update Columns");
			ArgumentValidation.CheckForNullReference(this.ParameterValues, "Update Values");

			StringBuilder query = new StringBuilder();
			StringBuilder computedColumns = new StringBuilder();
			StringBuilder keyColumns = new StringBuilder();

            query.AppendFormat("UPDATE {0}", this.TableNameWithSchema);
			query.Append(" SET ");
			query.Append(this.UpdateColumns);

			foreach (ValueParameter param in this.ParameterValues)
			{
				db.AddInParameter(dbCommand, ParameterToken + param.SchemaItem.FieldName, param.SchemaItem.DBType, param.Value);
			}

            BuildWhereClause(db, dbCommand, conjunction, query);

			dbCommand.CommandText = query.ToString();
		}

		/// <summary>
		/// Builds a provider-specific INSERT query against the <see cref="EasyObject.TableName"/>.
		/// <seealso cref="EasyObject.TableName"/>
		/// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="dbCommand">A wrapper for an Enterprise Library command object</param>
        /// <param name="conjunction">The conjunction to use between multiple <see cref="WhereParameter"/>s</param>
        protected override void BuildInsertQuery(Database db, DbCommand dbCommand, string conjunction)
		{
			ArgumentValidation.CheckForEmptyString(this.InsertColumns, "Insert Columns");
			ArgumentValidation.CheckForNullReference(this.ParameterValues, "Query Values");

            StringBuilder query = new StringBuilder();
            StringBuilder computedColumns = new StringBuilder();
            StringBuilder keyColumns = new StringBuilder();

            query.AppendFormat("INSERT INTO {0}", this.TableNameWithSchema);
            query.Append(" (");
            query.Append(this.InsertColumns);
			query.Append(") VALUES (");
			query.Append(this.InsertColumnValues);
			query.Append(");");

			foreach (ValueParameter param in this.ParameterValues)
			{
				db.AddInParameter(dbCommand, ParameterToken + param.SchemaItem.FieldName, param.SchemaItem.DBType, param.Value);
			}

            BuildWhereClause(db, dbCommand, conjunction, query);

			dbCommand.CommandText = query.ToString();
		}

		/// <summary>
		/// Builds a provider-specific DELETE query against the <see cref="EasyObject.TableName"/>.
		/// <seealso cref="EasyObject.TableName"/>
		/// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="dbCommand">A wrapper for an Enterprise Library command object</param>
        /// <param name="conjunction">The conjunction to use between multiple <see cref="WhereParameter"/>s</param>
        protected override void BuildDeleteQuery(Database db, DbCommand dbCommand, string conjunction)
		{
			StringBuilder query = new StringBuilder();

            query.AppendFormat("DELETE FROM {0}", this.TableNameWithSchema);

            BuildWhereClause(db, dbCommand, conjunction, query);

			dbCommand.CommandText = query.ToString();
		}

        /// <summary>
        /// Construct the correct WHERE clause using the Transact-SQL syntax (generic SQL Server)
        /// </summary>
        /// <param name="db">A database object from the Enterprise Library</param>
        /// <param name="dbCommand">A wrapper for an Enterprise Library command object</param>
        /// <param name="conjunction">The conjunction to use between parameters, usually AND or OR</param>
        /// <param name="query">The query string to append the WHERE clause to</param>
        protected virtual void BuildWhereClause(Database db, DbCommand dbCommand, string conjunction, StringBuilder query)
		{
		
			if(_whereParameters != null && _whereParameters.Count > 0)
			{
				query.Append(" WHERE ");

				bool first = true;

				bool requiresParam;

				WhereParameter wItem;
				bool skipConjunction = false;

				string paramName;
				string columnName;

				foreach(object obj in _whereParameters)
				{
					// Maybe we injected text or a WhereParameter
					if(obj.GetType().ToString() == "System.String")
					{
						string text = obj as string;
						query.Append(text);

						if(text == "(")
						{
							skipConjunction = true;
						}
					}
					else
					{
						wItem = obj as WhereParameter;

						if(wItem.IsDirty)
						{
							if(!first && !skipConjunction)
							{
								if(wItem.Conjunction != WhereParameter.Conj.UseDefault)
								{
									if(wItem.Conjunction == WhereParameter.Conj.And)
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

							switch(wItem.Operator)
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
                                    query.AppendFormat("{0} LIKE {1} ", columnName, paramName);
                                    BuildWildcardValue(wItem);
                                    break;
                                case WhereParameter.Operand.NotStartsWith:
                                case WhereParameter.Operand.NotEndsWith:
                                case WhereParameter.Operand.NotContains:
                                case WhereParameter.Operand.NotLike:
                                    query.AppendFormat("{0} NOT LIKE {1} ", columnName, paramName);
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

							if(requiresParam)
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

        /// <summary>
        /// Adds a properly formatted <see cref="WildcardToken"/> to the <see cref="WhereParameter"/> value
        /// </summary>
        /// <param name="wItem">The <see cref="WhereParameter"/> that needs the <see cref="WildcardToken"/></param>
        protected void BuildWildcardValue(WhereParameter wItem)
        {
            switch (wItem.Operator)
            {
                case WhereParameter.Operand.StartsWith:
                case WhereParameter.Operand.NotStartsWith:
                    wItem.Value = string.Format("{1}{0}", WildcardToken, wItem.Value);
                    break;
                case WhereParameter.Operand.EndsWith:
                case WhereParameter.Operand.NotEndsWith:
                    wItem.Value = string.Format("{0}{1}", WildcardToken, wItem.Value);
                    break;
                case WhereParameter.Operand.Contains:
                case WhereParameter.Operand.NotContains:
                    wItem.Value = string.Format("{0}{1}{0}", WildcardToken, wItem.Value);
                    break;
                default:
                    break;
            }
        }

		/// <summary>
		/// Builds the provider-specific aggregate portion of the query
		/// </summary>
		/// <param name="param">An <seealso cref="AggregateParameter"/> to add to the GROUP BY clause</param>
		/// <param name="withAlias">A flag to indicate if the aggregate should use an alias, if one is present</param>
		/// <returns>A formatted string for the aggregate function</returns>
		protected string GetAggregate(AggregateParameter param, bool withAlias)
		{
			StringBuilder query = new StringBuilder();

			switch(param.Function)
			{
				case AggregateParameter.Func.Avg:
					query.Append("AVG(");
					break;
				case AggregateParameter.Func.Count:
					query.Append("COUNT(");
					break;
				case AggregateParameter.Func.Max:
					query.Append("MAX(");
					break;
				case AggregateParameter.Func.Min:
					query.Append("MIN(");
					break;
				case AggregateParameter.Func.Sum:
					query.Append("SUM(");
					break;
				case AggregateParameter.Func.StdDev:
					query.Append("STDEV(");
					break;
				case AggregateParameter.Func.Var:
					query.Append("VAR(");
					break;
			}
			
			if(param.Distinct)
			{
				query.Append("DISTINCT ");
			}

			query.AppendFormat(FieldFormat, param.Column);
			query.Append(")");
			
			if(withAlias && param.Alias != string.Empty)
			{
				query.Append(" AS ");
				// Need DBMS string delimiter here
				query.AppendFormat(this.AliasFormat, param.Alias);
			}
			
			return query.ToString();
		}
	}
}
