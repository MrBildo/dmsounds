using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
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

		public static void SetValue<T>(this T o, string propertyName, object value)
		{
			var allPropertiesAndFields =
				typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
					.Select(m => m as MemberInfo)
						.Union(
							typeof(T).GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic)
								.Select(m => m as MemberInfo))
									.ToList();

			var property = allPropertiesAndFields.Where(p => p.Name == propertyName).Single();

			switch (property.MemberType)
			{
				case MemberTypes.Field:
					((FieldInfo)property).SetValue(o, value);
					break;
				case MemberTypes.Property:
					((PropertyInfo)property).SetValue(o, value, null);
					break;
			}
		}

		public static string SpacesToDashes(this string s)
		{
			//first remove all extraneous spaces 
			var newString = Regex.Replace(s, " {2,}", " ");

			return newString.Replace(' ', '-');
		}

		public static bool ContainsAll<T>(this IEnumerable<T> query, IEnumerable<T> items)
		{
			return !query.Except(items).Any();
		}

		public static bool ContainsAny<T>(this IEnumerable<T> query, IEnumerable<T> items)
		{
			return query.Any(i => items.Contains(i));
		}

		public static string[] RemoveNullorWhitespace(this string[] items)
		{
			return items.Where(i => !i.IsNullorWhitespace()).ToArray();
		}
	}
}
