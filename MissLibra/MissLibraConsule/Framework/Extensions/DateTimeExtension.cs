using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Extensions
{
	public static class DateTimeExtension
	{
		public static DateTime ToDateTime(this int intTime)
		{
			if (intTime <= 0)
			{
				return DateTime.MinValue;
			}
			string year = intTime.ToString().Remove(4);
			string month = intTime.ToString().Remove(0, 4);
			month = month.Remove(2);
			string day = intTime.ToString().Remove(0, 6);
			return DateTime.Parse($"{year}-{month}-{day}");
		}

		public static DateTime ToDateTime(this string strTime)
		{
			return DateTime.Parse(strTime);
		}

		public static int ToIntDate(this DateTime dt)
		{
			string day = dt.ToString("yyyyMMdd");
			return int.Parse(day);
		}
		public static long ToyyyyMMddHHmmss(this DateTime dt)
		{
			string day = dt.ToString("yyyyMMddHHmmss");
			return long.Parse(day);
		}

		public static int ToHHmm(this DateTime dt)
		{
			string day = dt.ToString("HHmm");
			return int.Parse(day);
		}

		public static int ToHHmmss(this DateTime dt)
		{
			string day = dt.ToString("HHmmss");
			return int.Parse(day);
		}
		public static long ToyyyyMMddHHmm(this DateTime dt)
		{
			string day = dt.ToString("yyyyMMddHHmm");
			return long.Parse(day);
		}
	}
}
