using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NCI.EasyObjects.WCF
{
    [DataContract(Namespace = "http://tempuri.org", Name = "FilterOperand")]
    public enum FilterOperand
    {
        /// <summary>
        /// Equal Comparison
        /// </summary>
        [EnumMember()]
        Equal = 1,
        /// <summary>
        /// Not Equal Comparison
        /// </summary>
        [EnumMember()]
        NotEqual,
        /// <summary>
        /// Greater Than Comparison
        /// </summary>
        [EnumMember()]
        GreaterThan,
        /// <summary>
        /// Greater Than or Equal Comparison
        /// </summary>
        [EnumMember()]
        GreaterThanOrEqual,
        /// <summary>
        /// Less Than Comparison
        /// </summary>
        [EnumMember()]
        LessThan,
        /// <summary>
        /// Less Than or Equal Comparison
        /// </summary>
        [EnumMember()]
        LessThanOrEqual,
        /// <summary>
        /// Like Comparison, "%s%" does it have an 's' in it? "s%" does it begin with 's'?
        /// </summary>
        [EnumMember()]
        Like,
        /// <summary>
        /// Is the value null in the database
        /// </summary>
        [EnumMember()]
        IsNull,
        /// <summary>
        /// Is the value non-null in the database
        /// </summary>
        [EnumMember()]
        IsNotNull,
        /// <summary>
        /// Is the value between two parameters? see <see cref="BetweenBeginValue"/> and <see cref="BetweenEndValue"/>. 
        /// Note that Between can be for other data types than just dates.
        /// </summary>
        [EnumMember()]
        Between,
        /// <summary>
        /// Is the value in a list, ie, "4,5,6,7,8"
        /// </summary>
        [EnumMember()]
        In,
        /// <summary>
        /// NOT in a list, ie not in, "4,5,6,7,8"
        /// </summary>
        [EnumMember()]
        NotIn,
        /// <summary>
        /// Not Like Comparison, "%s%", anything that does not it have an 's' in it.
        /// </summary>
        [EnumMember()]
        NotLike,
        /// <summary>
        /// Uses the LIKE comparison to find values that match at the start, "S%", anything 
        /// that starts with an 'S'. The difference is that the provider-specific wildcard 
        /// character will be added automatically by the <see cref="DynamicQueryProvider"/>.
        /// </summary>
        [EnumMember()]
        StartsWith,
        /// <summary>
        /// Uses the LIKE comparison to find values that do not match at the start, "S%", anything 
        /// that does not start with an 'S'. The difference is that the provider-specific wildcard 
        /// character will be added automatically by the <see cref="DynamicQueryProvider"/>.
        /// </summary>
        [EnumMember()]
        NotStartsWith,
        /// <summary>
        /// Uses the LIKE comparison to find values that match at the end, "%S", anything 
        /// that ends with an 'S'. The difference is that the provider-specific wildcard 
        /// character will be added automatically by the <see cref="DynamicQueryProvider"/>.
        /// </summary>
        [EnumMember()]
        EndsWith,
        /// <summary>
        /// Uses the LIKE comparison to find values that do not match at the end, "%S", anything 
        /// that does not end with an 'S'. The difference is that the provider-specific wildcard 
        /// character will be added automatically by the <see cref="DynamicQueryProvider"/>.
        /// </summary>
        [EnumMember()]
        NotEndsWith,
        /// <summary>
        /// Uses the LIKE comparison to find values that contain any match, "%S%", anything 
        /// that contains an 'S'. The difference is that the provider-specific wildcard 
        /// character will be added automatically by the <see cref="DynamicQueryProvider"/>.
        /// </summary>
        [EnumMember()]
        Contains,
        /// <summary>
        /// Uses the LIKE comparison to find values that do not contain a match, "%S%", anything 
        /// that does not contain an 'S'. The difference is that the provider-specific wildcard 
        /// character will be added automatically by the <see cref="DynamicQueryProvider"/>.
        /// </summary>
        [EnumMember()]
        NotContains
    }

    [DataContract(Namespace = "http://tempuri.org", Name = "SortDirection")]
    public enum SortDirection
    {
        /// <summary>
        /// Sort in ascending order
        /// </summary>
        [EnumMember()]
        Ascending = 1,
        /// <summary>
        /// Sort in descending order
        /// </summary>
        [EnumMember()]
        Descending
    }

    [DataContract]
    public class QueryFilter
    {
        public QueryFilter() { }

        public QueryFilter(string fieldName, FilterOperand operand, string value)
        {
            this._fieldName = fieldName;
            this._operand = operand;
            this._value = value;
        }

        string _fieldName;
        string _value;
        FilterOperand _operand;

        [DataMember]
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }

        [DataMember]
        public string Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        [DataMember]
        public FilterOperand Operand
        {
            get { return _operand; }
            set { _operand = value; }
        }
    }

    [DataContract]
    public class QuerySort
    {
        string _fieldName;
        SortDirection _sortDirection;

        [DataMember]
        public SortDirection SortDirection
        {
            get { return _sortDirection; }
            set { _sortDirection = value; }
        }

        [DataMember]
        public string FieldName
        {
            get { return _fieldName; }
            set { _fieldName = value; }
        }
    }
}
