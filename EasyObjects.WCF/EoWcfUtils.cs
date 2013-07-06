using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NCI.EasyObjects;

namespace NCI.EasyObjects.WCF
{
    public class EoWcfUtils
    {
        /// <summary>
        /// Adds columns from the list to EasyObject SELECT query
        /// </summary>
        /// <param name="obj">The EasyObject to add the columns to</param>
        /// <param name="columns">A list of columns to add to the query</param>
        public static void AddColumns(EasyObject obj, List<string> columns)
        {
            // Check for no columns
            if (columns == null) return;

            // Add the requested columns
            foreach (string col in columns)
            {
                obj.Query.AddResultColumnFormat(col);
            }
        }

        /// <summary>
        /// Adds columns from the list to the EasyObject WHERE clause
        /// </summary>
        /// <param name="obj">The EasyObject to add the filters to</param>
        /// <param name="filters">A list of filters to add to the query</param>
        public static void AddFilters(EasyObject obj, List<QueryFilter> filters)
        {
            // Apply the query filters
            if (filters != null)
            {
                foreach (QueryFilter filter in filters)
                {
                    //ModuleSchema schema = new ModuleSchema();
                    //SchemaItem si = schema.FindSchemaItem(filter.FieldName);
                    SchemaItem si = null;
                    for (int i = 0; i < obj.SchemaEntries.Count; i++)
                    {
                        SchemaItem si2 = (SchemaItem)obj.SchemaEntries[i];
                        if (si2.FieldName == filter.FieldName)
                        {
                            si = si2;
                            break;
                        }
                    }

                    if (si == null)
                        throw new ArgumentException(string.Format("Invalid filter column '{0}'.", filter.FieldName));

                    WhereParameter wp = new WhereParameter(si);
                    wp.Operator = (WhereParameter.Operand)filter.Operand;
                    wp.Value = filter.Value;
                    obj.Query.AddWhereParameter(wp);
                }
            }
        }

        /// <summary>
        /// Add columns from the list to the EasyObject ORDER BY clause
        /// </summary>
        /// <param name="obj">The EasyObject to add the sorting to</param>
        /// <param name="sorting">A list of sort columns to add to the query</param>
        public static void AddSorting(EasyObject obj, List<QuerySort> sorting)
        {
            // Check for no columns
            if (sorting == null) return;

            // Add the requested columns
            foreach (QuerySort col in sorting)
            {
                if (col.SortDirection == SortDirection.Ascending)
                {
                    obj.Query.AddOrderByFormat(col.FieldName);
                }
                else
                {
                    obj.Query.AddOrderByFormat(col.FieldName, WhereParameter.Dir.DESC);
                }
            }
        }
    }
}
