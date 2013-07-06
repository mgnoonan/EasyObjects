using System;

namespace EasyObjectsQuickStart.BLL
{
	/// <summary>
	/// Summary description for Products.
	/// </summary>
	public class Products : _Products
	{
        public Products() { }

		public Products(string server, bool useIntegratedSecurity, string userID, string password) 
		{
			this.ConnectionServer = server;
			this.UseIntegratedSecurity = useIntegratedSecurity;
			this.ConnectionUserID = userID;
			this.ConnectionPassword = password;
			this.ConnectionDatabase = "Northwind";
		}
	}
}
