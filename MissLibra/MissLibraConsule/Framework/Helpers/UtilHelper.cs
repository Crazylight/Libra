using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Framework.Helpers
{
	public class UtilHelper
	{
		/// <summary>
		/// 静态构造器。
		/// </summary>
		static UtilHelper()
		{
			return;
		}

		/// <summary>
		/// 多线程环境下严格随机的Random实现
		/// </summary>
		private static class RandomGen3
		{
			private static RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();

			[ThreadStatic]
			private static Random _local;

			public static int Next()
			{
				Random inst = _local;
				if (inst == null)
				{
					byte[] buffer = new byte[4];
					_global.GetBytes(buffer);
					_local = inst = new Random(BitConverter.ToInt32(buffer, 0));
				}
				return inst.Next();
			}

			public static int Next(int min, int max)
			{
				Random inst = _local;
				if (inst == null)
				{
					byte[] buffer = new byte[4];
					_global.GetBytes(buffer);
					_local = inst = new Random(BitConverter.ToInt32(buffer, 0));
				}
				return inst.Next(min, max);
			}
		}

		/// <summary>
		/// 返回介于min和max之间的一个随机数。包括min，不包括max
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		/// <returns></returns>
		public static int Random(int min, int max)
		{
			return RandomGen3.Next(min, max);
		}

		/// <summary>
		/// 返回非负随机数（多线程下也严格随机）
		/// </summary>
		/// <returns></returns>
		public static int Random()
		{
			return RandomGen3.Next();
		}

		/// <summary>
		/// 随机以rate分之一的概率返回true，其他时候返回false
		/// </summary>
		/// <param name="rate"></param>
		/// <returns></returns>
		public static bool RandomRate(int rate)
		{
			int rest = Random() % rate;

			bool ret = (rest == 0);
			return ret;
		}

		/// <summary>
		/// 生成指定长度的随机字符串（大写字母和数字）
		/// </summary>
		/// <param name="len"></param>
		/// <returns></returns>
		public static string RandomString(int len)
		{
			var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var stringChars = new char[len];

			for (int i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[Random() % chars.Length];
			}

			var finalString = new String(stringChars);
			return finalString;
		}
		/// <summary>
		/// 生成指定长度的随机字符串（小写字母、大写字母和数字）
		/// </summary>
		/// <param name="len"></param>
		/// <returns></returns>
		public static string RandomFullString(int len)
		{
			var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var stringChars = new char[len];

			for (int i = 0; i < stringChars.Length; i++)
			{
				stringChars[i] = chars[Random() % chars.Length];
			}

			var finalString = new String(stringChars);
			return finalString;
		}

		/// <summary>
		/// 取最小的一位数
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="third"></param>
		/// <returns></returns>
		private static int LowerOfThree(int first, int second, int third)
		{
			int min = Math.Min(first, second);
			return Math.Min(min, third);
		}

		/// <summary>
		/// 计算两个字符串之间的编辑距离。返回值越大表示差异越大，最大不会超过 Math.Max(str1.Length, str2.Length)
		/// </summary>
		/// <param name="str1"></param>
		/// <param name="str2"></param>
		/// <returns></returns>
		public static int GetLevenshteinDistance(string str1, string str2)
		{
			int[,] Matrix;
			int n = str1.Length;
			int m = str2.Length;

			int temp = 0;
			char ch1;
			char ch2;
			int i = 0;
			int j = 0;
			if (n == 0)
			{
				return m;
			}
			if (m == 0)
			{

				return n;
			}
			Matrix = new int[n + 1, m + 1];

			for (i = 0; i <= n; i++)
			{
				//初始化第一列
				Matrix[i, 0] = i;
			}

			for (j = 0; j <= m; j++)
			{
				//初始化第一行
				Matrix[0, j] = j;
			}

			for (i = 1; i <= n; i++)
			{
				ch1 = str1[i - 1];
				for (j = 1; j <= m; j++)
				{
					ch2 = str2[j - 1];
					if (ch1.Equals(ch2))
					{
						temp = 0;
					}
					else
					{
						temp = 1;
					}
					Matrix[i, j] = LowerOfThree(Matrix[i - 1, j] + 1, Matrix[i, j - 1] + 1, Matrix[i - 1, j - 1] + temp);
				}
			}
			//for (i = 0; i <= n; i++)
			//{
			//	for (j = 0; j <= m; j++)
			//	{
			//		Console.Write(" {0} ", Matrix[i, j]);
			//	}
			//	Console.WriteLine("");
			//}

			return Matrix[n, m];
		}

	}// End class Util.
}
