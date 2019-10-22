using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Framework.Helpers
{
	/// <summary>
	/// 日志记录类。
	/// </summary>
	public class LogHelper
	{
		private static object logLock = new object();

		private static string logpath = string.Empty;

		static LogHelper()
		{
			try
			{
				logpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/logs/";
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					logpath = @"d:\Logs\" + AppDomain.CurrentDomain.FriendlyName + "\\";
				}
				else
				{
					logpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/logs/" + AppDomain.CurrentDomain.FriendlyName + "/";
				}

				if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					if (string.IsNullOrEmpty(logpath))
					{
						logpath = ConfigHelper.GetAppSettingString("logpath_linux", "/home/admin/logs/");
					}
				}

				Console.WriteLine($"LogPath={logpath}");

				if (!System.IO.Directory.Exists(logpath))
				{
					System.IO.Directory.CreateDirectory(logpath);
				}
			}
			catch
			{
				throw;
			}
		}

		private static ConcurrentDictionary<string, object> fileLockDic = new ConcurrentDictionary<string, object>();

		public static void writeLogsingle(string filename, string logMessage)
		{
			object lockObject = fileLockDic.GetOrAdd(filename, new object());

			lock (lockObject)
			{
				logMessage = string.Format("{0} {1}\r\n", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), logMessage);
				string fLogName = string.Format("{0}{1}{2}", logpath, System.DateTime.Now.ToString("yyyyMMdd_HH_"), filename);
				try
				{
					if (!fLogName.EndsWith(".log"))
					{
						fLogName += ".log";
					}
					using (FileStream fs = new FileStream(fLogName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
					{
						using (BinaryWriter w = new BinaryWriter(fs))
						{
							w.Write(logMessage.ToCharArray());
						}
					}
				}
				catch (Exception exp)
				{
					string s = exp.Message;
				}
				return;
			}
		}


		public static void LogFile(string fileName, string content, bool writeConsole = false)
		{
			if (writeConsole)
			{
				Console.WriteLine(content);
			}
			LogHelper.writeLogsingle(fileName, content);
		}
		public static void LogLife(string lifeDes)
		{
			Console.WriteLine(lifeDes);
			LogHelper.writeLogsingle("Life", lifeDes);
		}
		public static void LogData(string dataContent)
		{
			LogHelper.writeLogsingle("DataLog", dataContent);
		}
		public static void LogError(string errMsg)
		{
			Console.WriteLine(errMsg);
			LogHelper.writeLogsingle("Error", errMsg);
		}


	}// End class LogRecord.


	/// <summary>
	/// 日志记录类: 一天一个文件
	/// </summary>
	public class DayLogHelper
	{
		private static object logLock = new object();

		private static string logpath = string.Empty;

		static DayLogHelper()
		{
			try
			{
				logpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/logs/";
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					logpath = @"d:\Logs\" + AppDomain.CurrentDomain.FriendlyName + "\\";
				}
				else
				{
					logpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/logs/" + AppDomain.CurrentDomain.FriendlyName + "/";
				}

				if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
				{
					if (string.IsNullOrEmpty(logpath))
					{
						logpath = ConfigHelper.GetAppSettingString("logpath_linux", "/home/admin/logs/");
					}
				}

				Console.WriteLine($"LogPath={logpath}");

				if (!System.IO.Directory.Exists(logpath))
				{
					System.IO.Directory.CreateDirectory(logpath);
				}
			}
			catch
			{
				throw;
			}
		}

		private static ConcurrentDictionary<string, object> fileLockDic = new ConcurrentDictionary<string, object>();

		public static void writeLogsingle(string filename, string logMessage)
		{
			object lockObject = fileLockDic.GetOrAdd(filename, new object());

			lock (lockObject)
			{
				logMessage = string.Format("{0} {1}\r\n", System.DateTime.Now.ToString("HH:mm:ss.fff"), logMessage);
				string fLogName = string.Format("{0}{1}{2}", logpath, System.DateTime.Now.ToString("yyyyMMdd"), filename);
				try
				{
					if (!fLogName.EndsWith(".log"))
					{
						fLogName += ".log";
					}
					using (FileStream fs = new FileStream(fLogName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
					{
						using (BinaryWriter w = new BinaryWriter(fs))
						{
							w.Write(logMessage.ToCharArray());
						}
					}
				}
				catch (Exception exp)
				{
					string s = exp.Message;
				}
				return;
			}
		}


		public static void LogFile(string fileName, string content, bool writeConsole = false)
		{
			if (writeConsole)
			{
				Console.WriteLine(content);
			}
			LogHelper.writeLogsingle(fileName, content);
		}
		public static void LogLife(string lifeDes)
		{
			Console.WriteLine(lifeDes);
			LogHelper.writeLogsingle("Life", lifeDes);
		}
		public static void LogData(string dataContent)
		{
			LogHelper.writeLogsingle("DataLog", dataContent);
		}
		public static void LogError(string errMsg)
		{
			Console.WriteLine(errMsg);
			LogHelper.writeLogsingle("Error", errMsg);
		}


	}
}
