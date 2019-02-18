using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace System
{
	public static class Extensions
	{
		public static T GetValue<T>(this SerializationInfo info, string name)
		{
			return (T)info.GetValue(name, typeof(T));
		}

		public static bool IsNullorWhitespace(this string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}

		public static string WhitespaceToNull(this string value)
		{
			return value.IsNullorWhitespace() ? null : value;
		}

		public static void Serialize(this BinaryFormatter formatter, string filename, object o)
		{
			using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.None))
			{
				formatter.Serialize(stream, o);
			}
		}

	}
}
