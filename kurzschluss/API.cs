using System.Runtime.InteropServices;
using System.Text;

namespace Ediabas
{
	public class API
	{
		private class Class2
		{
			private sbyte[] sbyte_0;

			public Class2()
			{
			}

			public Class2(int int_0)
			{
				if (int_0 >= 1)
				{
					sbyte_0 = new sbyte[int_0];
					sbyte_0[0] = 0;
				}
			}

			public Class2(string string_0)
			{
				sbyte_0 = smethod_7(string_0);
			}

			public static Class2 smethod_0(string string_0)
			{
				return new Class2(string_0);
			}

			public static string smethod_1(Class2 class2_0)
			{
				return class2_0.ToString();
			}

			public static bool smethod_2(Class2 class2_0, Class2 class2_1)
			{
				return class2_0.ToString() == class2_1.ToString();
			}

			public static bool smethod_3(Class2 class2_0, Class2 class2_1)
			{
				return class2_0.ToString() != class2_1.ToString();
			}

			public override bool Equals(object object_0)
			{
				if (object_0 is Class2)
				{
					return ToString() == ((Class2)object_0).ToString();
				}
				return false;
			}

			public override int GetHashCode()
			{
				return ToString().GetHashCode();
			}

			public static int smethod_4(sbyte[] sbyte_1)
			{
				int i;
				for (i = 0; sbyte_1[i] != 0; i++)
				{
				}
				return i;
			}

			public static char[] smethod_5(sbyte[] sbyte_1)
			{
				char[] array = new char[smethod_4(sbyte_1) + 1];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (char)sbyte_1[i];
				}
				return array;
			}

			public static string smethod_6(sbyte[] sbyte_1)
			{
				int i;
				for (i = 0; sbyte_1[i] != 0; i++)
				{
				}
				StringBuilder stringBuilder = new StringBuilder(i);
				for (int j = 0; j < i; j++)
				{
					stringBuilder.Append((char)sbyte_1[j]);
				}
				return stringBuilder.ToString();
			}

			public override string ToString()
			{
				return smethod_6(sbyte_0);
			}

			public static sbyte[] smethod_7(string string_0)
			{
				sbyte[] array = new sbyte[string_0.Length + 1];
				int i;
				for (i = 0; i < string_0.Length; i++)
				{
					array[i] = (sbyte)string_0[i];
				}
				array[i] = 0;
				return array;
			}

			public sbyte[] method_0()
			{
				return sbyte_0;
			}

			public sbyte[] method_1(int int_0)
			{
				if (sbyte_0 == null || sbyte_0.Length < int_0)
				{
					sbyte_0 = new sbyte[int_0];
				}
				return method_0();
			}

			public string method_2()
			{
				return ToString();
			}

			public void method_3(string string_0)
			{
				sbyte_0 = smethod_7(string_0);
			}
		}

		private static uint uint_0;

		[DllImport("api32.dll", CharSet = CharSet.Ansi)]
		private static extern bool __apiResultSets(uint uint_1, out ushort ushort_0);

		[DllImport("api32.dll", CharSet = CharSet.Ansi)]
		private static extern bool __apiInit(out uint uint_1);

		[DllImport("api32.dll", CharSet = CharSet.Ansi)]
		private static extern bool __apiGetConfig(uint uint_1, string string_0, sbyte[] sbyte_0);

		[DllImport("api32.dll", CharSet = CharSet.Ansi)]
		private static extern void __apiEnd(uint uint_1);

		[DllImport("api32.dll", CharSet = CharSet.Ansi)]
		private static extern void __apiJob(uint uint_1, string string_0, string string_1, string string_2, string string_3);

		[DllImport("api32.dll", CharSet = CharSet.Ansi)]
		private static extern bool __apiResultText(uint uint_1, sbyte[] sbyte_0, string string_0, ushort ushort_0, string string_1);

		public static bool apiInit()
		{
			return __apiInit(out uint_0);
		}

		public static bool apiGetConfig(string cfgName, out string cfgValue)
		{
			Class2 @class = new Class2(256);
			bool result = __apiGetConfig(uint_0, cfgName, @class.method_0());
			cfgValue = @class.ToString();
			return result;
		}

		public static void apiEnd()
		{
			__apiEnd(uint_0);
		}

		public static void apiJob(string string_0, string string_1, string para, string result)
		{
			__apiJob(uint_0, string_0, string_1, para, result);
		}

		public static bool apiResultText(out string buffer, string result, ushort rset, string format)
		{
			Class2 @class = new Class2(1024);
			bool result2 = __apiResultText(uint_0, @class.method_0(), result, rset, format);
			buffer = @class.ToString();
			return result2;
		}

		public static bool apiResultText(out char[] buffer, string result, ushort rset, string format)
		{
			string buffer2;
			bool result2 = apiResultText(out buffer2, result, rset, format);
			buffer = new char[buffer2.Length + 1];
			int i;
			for (i = 0; i < buffer2.Length; i++)
			{
				buffer[i] = buffer2[i];
			}
			buffer[i] = '\0';
			buffer2 = null;
			return result2;
		}

		public static bool apiResultSets(out ushort rsets)
		{
			return __apiResultSets(uint_0, out rsets);
		}
	}
}
