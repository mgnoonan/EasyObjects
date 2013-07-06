using System;

namespace NCI.EasyObjects.Samples.BLL
{
	/// <summary>
	/// Summary description for Products.
	/// </summary>
	public class Products : _Products
	{
        public Products()
		{
			this.DatabaseInstanceName = "Northwind";
            this.DefaultCommandType = System.Data.CommandType.Text;
		}
	}
}
