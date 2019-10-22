using Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Common
{
	public static class ConnectionString
	{
		static ConnectionString()
		{
			InitConnectionString();
		}

		public static void InitConnectionString()
		{
			NZ_FUTURE_TRADE = ConfigHelper.GetConnectionString("NZ_FUTURE_TRADE");
			NZ_FUTURE_TRADESlave = ConfigHelper.GetConnectionString("NZ_FUTURE_TRADESlave");
		}

		public static string NZ_FUTURE_TRADE = "";
		/// <summary>
		/// 
		/// </summary>
		public static string NZ_FUTURE_TRADESlave = "";

	}
}
