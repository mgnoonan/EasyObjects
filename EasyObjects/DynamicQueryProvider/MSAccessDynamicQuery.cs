//===============================================================================
// NCI.EasyObjects library
// SqlServerDynamicQuery
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
using System.Collections.Specialized;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace NCI.EasyObjects.DynamicQueryProvider
{
	/// <summary>
	/// Represents provider-specific <see cref="DynamicQuery"/> functions for Microsoft Access.
	/// </summary>
	public class MSAccessDynamicQuery : DynamicQuery
	{
		/// <summary>
		/// Initializes a new instance of <see cref="MSAccessDynamicQuery "/>.
		/// </summary>
		public MSAccessDynamicQuery () : base()
		{}

		/// <summary>
		/// Initialize a new instance of the <see cref="MSAccessDynamicQuery "/> class.
		/// </summary>
		public MSAccessDynamicQuery (EasyObject entity) : base(entity)
		{}
	
		/// <summary>
		/// <para>Gets the parameter token used to delimit parameters for the Access Database.</para>
		/// </summary>
		/// <value>
		/// <para>The '@' symbol.</para>
		/// </value>
		protected override char ParameterToken
		{
			get { return '@'; }
		}
	
		/// <summary>
		/// <para>Gets the string format used to delimit fieldnames for the Access Database.</para>
		/// </summary>
		/// <value>
		/// <para>'[{0}]' is the field format string for Sql Server.</para>
		/// </value>
		protected override string FieldFormat { get { return "[{0}]"; } }
	
		/// <summary>
		/// <para>Gets the string format used to separate schema names and database objects for the Access Database.</para>
		/// </summary>
		/// <value>
		/// <para>'.' is the schema separator string for Sql Server.</para>
		/// </value>
		protected override string SchemaSeparator { get { return "."; } }

		/// <summary>
		/// <para>Gets the string format used to delimit aliases for the Access Database.</para>
		/// </summary>
		/// <value>
		/// <para>"'{0}'" is the alias format string for Sql Server.</para>
		/// </value>
		protected override string AliasFormat { get { return "'{0}'"; } }
	
		public override void AddResultColumn(string fieldName)
		{
			base.AddResultColumnFormat(fieldName);
		}

		public override void AddOrderBy(string fieldName, NCI.EasyObjects.WhereParameter.Dir direction)
		{
			base.AddOrderByFormat(fieldName, direction);
		}

		public override void AddOrderBy(DynamicQuery countAll, NCI.EasyObjects.WhereParameter.Dir direction)
		{
			if(countAll.CountAll)
			{
				base.AddOrderBy("COUNT(*)", direction);
			}
		}

		public override void AddOrderBy(AggregateParameter aggregate, NCI.EasyObjects.WhereParameter.Dir direction)
		{
			base.AddOrderBy(GetAggregate(aggregate, false), direction);
		}

		public override void AddGroupBy(string fieldName)
		{
			base.AddGroupByFormat(fieldName);
		}
	
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
		/// <param name="dbCommandWrapper">An wrapper for an Enterprise Library command object</param>
		/// <param name="conjunction">The conjunction to use between multiple <see cref="WhereParameter"/>s</param>
		protected override void BuildSelectQuery(DBCommandWrapper dbCommandWrapper, string conjunction)
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

			query.Append(" FROM ");
			query.AppendFormat(FieldFormat, this._entity.QuerySource);

			BuildWhereClause(dbCommandWrapper, conjunction, query);

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

			dbCommandWrapper.Command.CommandText = query.ToString();
		}

		/// <summary>
		/// Builds a provider-specific UPDATE query against the <see cref="EasyObject.TableName"/>.
		/// <seealso cref="EasyObject.TableName"/>
		/// </summary>
		/// <param name="dbCommandWrapper">An wrapper for an Enterprise Library command object</param>
		/// <param name="conjunction">The conjunction to use between multiple <see cref="WhereParameter"/>s</param>
		protected override void BuildUpdateQuery(DBCommandWrapper dbCommandWrapper, string conjunction)
		{
			ArgumentValidation.CheckForEmptyString(this.UpdateColumns, "Update Columns");
			ArgumentValidation.CheckForNullReference(this.ParameterValues, "Update Values");

			StringBuilder query = new StringBuilder("UPDATE ");
			StringBuilder computedColumns = new StringBuilder();
			StringBuilder keyColumns = new StringBuilder();

			query.AppendFormat(FieldFormat, this._entity.TableName);
			query.Append(" SET ");
			query.Append(this.UpdateColumns);

			foreach (ValueParameter param in this.ParameterValues)
			{
				dbCommandWrapper.AddInParameter(ParameterToken + param.SchemaItem.FieldName, param.SchemaItem.DBType, param.Value);
			}

			BuildWhereClause(dbCommandWrapper, conjunction, query);

			foreach (object entry in this._entity.SchemaEntries)
			{
				SchemaItem item = (SchemaItem)entry;
				
				if (item.IsComputed)
				{
					computedColumns.AppendFormat("{0}{1} = {2}, ", ParameterToken, item.FieldName, string.Format(this.FieldFormat, item.FieldName));
				}
				else if (item.IsInPrimaryKey)
				{
					keyColumns.AppendFormat("{0} = {1}{2} AND ", string.Format(this.FieldFormat, item.FieldName), ParameterToken, item.FieldName);
				}
			}

			if (computedColumns.Length > 0)
			{
				// Get rid of trailing separators
				computedColumns.Length -= 2;
				keyColumns.Length -= 5;

				query.AppendFormat(" SELECT {0} FROM ", computedColumns.ToString());
							
				if (this._entity.SchemaTableView.Length > 0)
				{
					query.AppendFormat(FieldFormat, this._entity.SchemaTableView);
					query.Append(this.SchemaSeparator);
				}
			
				query.AppendFormat(FieldFormat, this._entity.TableName);
				query.AppendFormat(" WHERE {1};", keyColumns.ToString());
			}

			dbCommandWrapper.Command.CommandText = query.ToString();
		}

		/// <summary>
		/// Builds a provider-specific INSERT query against the <see cref="EasyObject.TableName"/>.
		/// <seealso cref="EasyObject.TableName"/>
		/// </summary>
		/// <param name="dbCommandWrapper">An wrapper for an Enterprise Library command object</param>
		/// <param name="conjunction">The conjunction to use between multiple <see cref="WhereParameter"/>s</param>
		protected override void BuildInsertQuery(DBCommandWrapper dbCommandWrapper, string conjunction)
		{
			ArgumentValidation.CheckForEmptyString(this.InsertColumns, "Insert Columns");
			ArgumentValidation.CheckForNullReference(this.ParameterValues, "Query Values");

			StringBuilder query = new StringBuilder("INSERT INTO ");
			StringBuilder computedColumns = new StringBuilder();
			StringBuilder keyColumns = new StringBuilder();
			
			query.AppendFormat(FieldFormat, this._entity.TableName);
			query.Append(" (");
			query.Append(this.InsertColumns);
			query.Append(") VALUES (");
			query.Append(this.InsertColumnValues);
			query.Append(")");

			foreach (ValueParameter param in this.ParameterValues)
			{
				dbCommandWrapper.AddInParameter(ParameterToken + param.SchemaItem.FieldName, param.SchemaItem.DBType, param.Value);
			}

			BuildWhereClause(dbCommandWrapper, conjunction, query);

			dbCommandWrapper.Command.CommandText = query.ToString();
		}

		/// <summary>
		/// Builds a provider-specific DELETE query against the <see cref="EasyObject.TableName"/>.
		/// <seealso cref="EasyObject.TableName"/>
		/// </summary>
		/// <param name="dbCommandWrapper">An wrapper for an Enterprise Library command object</param>
		/// <param name="conjunction">The conjunction to use between multiple <see cref="WhereParameter"/>s</param>
		protected override void BuildDeleteQuery(DBCommandWrapper dbCommandWrapper, string conjunction)
		{
			StringBuilder query = new StringBuilder("DELETE FROM ");
			
			query.AppendFormat(FieldFormat, this._entity.TableName);

			BuildWhereClause(dbCommandWrapper, conjunction, query);

			dbCommandWrapper.Command.CommandText = query.ToString();
		}

		private void BuildWhereClause(DBCommandWrapper dbCommandWrapper, string conjunction, StringBuilder query)
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

							while (dbCommandWrapper.Command.Parameters.Contains(paramName))
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
								case WhereParameter.Operand.Like:
									query.AppendFormat("{0} LIKE {1} ", columnName, paramName);
									break;
								case WhereParameter.Operand.NotLike:
									query.AppendFormat("{0} NOT LIKE {1} ", columnName, paramName);
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
									dbCommandWrapper.AddInParameter(paramName + "Begin", wItem.SchemaItem.DBType, wItem.BetweenBeginValue.ToString());
									dbCommandWrapper.AddInParameter(paramName + "End", wItem.SchemaItem.DBType, wItem.BetweenEndValue.ToString());
									requiresParam = false;
									break;
							}

							if(requiresParam)
							{
								dbCommandWrapper.AddInParameter(paramName, wItem.SchemaItem.DBType, wItem.Value);
							}

							first = false;
							skipConjunction = false;
						}
					}
				}
			}
		}
		
		protected string GetAggregate(AggregateParameter param, bool withAlias)
		{
			string query = string.Empty;

			switch(param.Function)
			{
				case AggregateParameter.Func.Avg:
					query += "AVG(";
					break;
				case AggregateParameter.Func.Count:
					query += "COUNT(";
					break;
				case AggregateParameter.Func.Max:
					query += "MAX(";
					break;
				case AggregateParameter.Func.Min:
					query += "MIN(";
					break;
				case AggregateParameter.Func.Sum:
					query += "SUM(";
					break;
				case AggregateParameter.Func.StdDev:
					query += "STDEV(";
					break;
				case AggregateParameter.Func.Var:
					query += "VAR(";
					break;
			}
			
			if(param.Distinct)
			{
				query += "DISTINCT ";
			}

			query += string.Format(FieldFormat, param.Column);
			query += ")";
			
			if(withAlias && param.Alias != string.Empty)
			{
				query += " AS ";
				// Need DBMS string delimiter here
				query += string.Format(this.AliasFormat, param.Alias);
			}
			
			return query;
		}
	}
}
