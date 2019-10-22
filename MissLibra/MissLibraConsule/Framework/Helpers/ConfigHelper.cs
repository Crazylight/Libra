using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Framework.Helpers
{
	public class ConfigHelper
	{
		static ConfigHelper()
		{
			InitConfig();
		}
		/// <summary>
		/// 配置
		/// </summary>
		public static IConfiguration Configuration { get; set; }
		public static void InitConfig()
		{
			try
			{
				var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());//SetBasePath 需要添加Microsoft.Extensions.Configuration.Json引用
				string evname = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
				if (!string.IsNullOrEmpty(evname))
				{
					builder.AddJsonFile($"config/appSettings.{evname}.json");
				}
				else
				{
					builder.AddJsonFile("config/appSettings.json");
				}

				Configuration = builder.Build();
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		/// <summary>
		/// 获取Config文件appSettings节点配置值
		/// </summary>
		/// <param name="name">节点名称</param>
		/// <param name="defval">默认值</param>
		/// <returns></returns>
		public static string GetAppSettingString(string key, string defval = "")
		{
			if (Configuration == null)
			{
				return defval;
			}
			var conn = Configuration.GetSection("appSettings:" + key);
			if (!conn.Exists())
			{
				return defval;
			}
			return conn.Value;
		}

		/// <summary>
		/// 获取Config文件appSettings节点配置值
		/// </summary>
		/// <param name="name">节点名称</param>
		/// <param name="defval">默认值</param>
		/// <returns></returns>
		public static string GetSectionValue(string key, string defval = "")
		{
			if (Configuration == null)
			{
				return defval;
			}
			var conn = Configuration.GetSection(key);
			if (!conn.Exists())
			{
				return defval;
			}
			return conn.Value;
		}


		/// <summary>
		/// 获取Config文件appSettings节点配置值
		/// </summary>
		/// <param name="name">节点名称</param>
		/// <param name="defval">默认值</param>
		/// <returns></returns>
		public static string GetConnectionString(string key)
		{
			if (Configuration == null)
			{
				return "";
			}
			var conn = Configuration.GetSection("ConnectionStrings:" + key);
			if (!conn.Exists())
			{
				return "";
			}
			return conn.Value;
		}
	}
}
