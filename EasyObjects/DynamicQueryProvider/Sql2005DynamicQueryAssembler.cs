using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using NCI.EasyObjects.Configuration;

namespace NCI.EasyObjects.DynamicQueryProvider
{
    class Sql2005DynamicQueryAssembler : IDynamicQueryAssembler
    {
        /// <summary>
        /// Builds an instance of <see cref="Sql2005DynamicQuery"/>, based on the provided connection string.
        /// </summary>
        /// <param name="entity">An optional reference to an <see cref="EasyObject"/> to pass on to the <see cref="DynamicQuery"/> provider.</param>
        /// <returns>The new SQL Server dynamic query instance.</returns>
        public DynamicQuery Assemble(EasyObject entity)
        {
            if (entity == null)
            {
                return new Sql2005DynamicQuery();
            }
            else
            {
                return new Sql2005DynamicQuery(entity);
            }
        }
    }
}
