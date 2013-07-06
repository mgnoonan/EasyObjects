//===============================================================================
// NCI.EasyObjects library
// WhereParameter class
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
//using System.Collections;
//using System.Data;

namespace NCI.EasyObjects
{
	/// <summary>
	/// This class is dynamcially created when you add an AggregateParameter to your EasyObject's  <see cref="DynamicQuery"/> (See the EasyObject.Query).
	/// </summary>
	/// <remarks>
	/// Aggregate and GROUP BY Feature Support by DBMS:
	/// <code>
	///                 MS    
	/// Feature         SQL   Oracle
	/// --------------- ----- ------
	/// Avg              Y     Y
	/// Count            Y     Y
	/// Max              Y     Y
	/// Min              Y     Y
	/// Sum              Y     Y
	/// StdDev           Y    (1)
	/// Var              Y     Y
	/// Aggregate in
	///   ORDER BY       Y     Y
	///   GROUP BY       -     Y
	/// WITH ROLLUP      Y     Y
	/// COUNT(DISTINCT)  Y     Y
	/// 
	/// Notes:
	///   (1) - Uses TRUNC(STDDEV(column),10) to avoid overflow errors
	///   
	/// </code>
	/// This will be the extent of your use of the AggregateParameter class, this class is mostly used by the EasyObject architecture, not the programmer.
	/// <code>
	/// prds  = new Products();
	///
	/// // To include a COUNT(*) with NULLs included
	/// prds.Query.CountAll = true;
	/// prds.Query.CountAllAlias = "Product Count";
	///
	/// // To exclude NULLs in the COUNT for a column
	/// prds.Aggregate.UnitsInStock.Function = AggregateParameter.Func.Count;
	/// prds.Aggregate.UnitsInStock.Alias = "With Stock";
	///
	/// // To have two aggregates for the same column, use a tearoff
	/// AggregateParameter ap = prds.Aggregate.TearOff.UnitsInStock;
	/// ap.Function = AggregateParameter.Func.Sum;
	/// ap.Alias = "Total Units";
	///
	/// prds.Aggregate.ReorderLevel.Function = AggregateParameter.Func.Avg;
	/// prds.Aggregate.ReorderLevel.Alias = "Avg Reorder";
	///
	/// prds.Aggregate.UnitPrice.Function = AggregateParameter.Func.Min;
	/// prds.Aggregate.UnitPrice.Alias = "Min Price";
	///
	/// ap = prds.Aggregate.TearOff.UnitPrice;
	/// ap.Function = AggregateParameter.Func.Max;
	/// ap.Alias = "Max Price";
	///
	/// // If you have no aggregates or AddResultColumns,
	/// // Then the query defaults to SELECT *
	/// // If you have an aggregate and no AddResultColumns,
	/// // Then only aggregates are reurned in the query.
	/// prds.Query.AddResultColumn(Products.ColumnNames.CategoryID);
	/// prds.Query.AddResultColumn(Products.ColumnNames.Discontinued);
	///
	/// // If you have an Aggregate, ANSI SQL requires an AddGroupBy
	/// // for each AddResultColumn. Check your DBMS docs.
	/// prds.Query.AddGroupBy(Products.ColumnNames.CategoryID);
	/// prds.Query.AddGroupBy(Products.ColumnNames.Discontinued);
	///
	/// prds.Query.AddOrderBy(Products.ColumnNames.Discontinued, WhereParameter.Dir.ASC);
	/// 
	/// // You can use aggregates in AddOrderBy by
	/// // referencing either the EasyObject AggregateParameter or a tearoff
	/// // You must create the aggregate before using it here.
	/// prds.Query.AddOrderBy(prds.Aggregate.UnitsInStock, WhereParameter.Dir.DESC);
	/// 
	/// // Load it.
	/// prds.Query.Load();
	/// </code>
	/// </remarks>
	public class AggregateParameter : ValueParameter
	{
//		private object _value = null;
//		private IDataParameter _param;
//		private string _column;
		private Func _function = AggregateParameter.Func.Sum;
		private string _alias = string.Empty;
		private bool _isDirty = false;
		private bool _distinct = false;
		
		/// <summary>
		/// The aggregate function used by Aggregate.Function
		/// </summary>
		public enum Func
		{
			/// <summary>
			/// Average
			/// </summary>
			Avg = 1,
			/// <summary>
			/// Count
			/// </summary>
			Count,
			/// <summary>
			/// Maximum
			/// </summary>
			Max,
			/// <summary>
			/// Minimum
			/// </summary>
			Min,
			/// <summary>
			/// Standard Deviation
			/// </summary>
			StdDev,
			/// <summary>
			/// Variance
			/// </summary>
			Var,
			/// <summary>
			/// Sum
			/// </summary>
			Sum
		};

		/// <summary>
		/// This is only called by EasyObject architecture.
		/// </summary>
		/// <param name="item">A <see cref="SchemaItem"/> to use in the aggregate</param>
		public AggregateParameter(SchemaItem item) : base(item)
		{
			this._alias = item.FieldName;
			this._distinct = false;
			this._function = AggregateParameter.Func.Sum;
		}

		/// <summary>
		/// Used to determine if the AggregateParameter has a value
		/// </summary>
		public bool IsDirty
		{
			get
			{
				return _isDirty;
			}
		}

		/// <summary>
		/// The value that will be placed into the Parameter
		/// </summary>
		public override object Value
		{
			get
			{
				return _value;
			}

			set
			{
				_value = value;
				_isDirty = true;
			}
		}

		/// <summary>
		/// The type of aggregate function desired.
		/// Avg, Count, Min, Max, Sum, StdDev, or Var.
		/// (See AggregateParameter.Func enumeration.)
		/// </summary>
		public Func Function
		{
			get
			{
				return _function;
			}

			set
			{
				_function = value;
				_isDirty = true;
			}
		}

		/// <summary>
		/// The user-friendly name of the aggregate column
		/// </summary>
		public string Alias
		{
			get
			{
				return _alias;
			}

			set
			{
				_alias = value;
				_isDirty = true;
			}
		}

		/// <summary>
		/// If true, then use (DISTINCT columnName) in the aggregate.
		/// </summary>
		public bool Distinct
		{
			get
			{
				return _distinct;
			}

			set
			{
				_distinct = value;
				_isDirty = true;
			}
		}
	}
}
