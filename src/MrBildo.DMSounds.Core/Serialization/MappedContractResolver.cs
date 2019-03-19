using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.Serialization
{
	public class MappedContractResolver : DefaultContractResolver
	{
		public MemberMapping Mapping { get; private set; }

		public MappedContractResolver()
		{
			Mapping = new MemberMapping();
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{

			//get all the public stuff
			var allPublicPropertiesAndFields =
				type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
					.Select(m => m as MemberInfo)
						.Union(
							type.GetFields(BindingFlags.Public | BindingFlags.Instance)
								.Select(m => m as MemberInfo))
									.ToList();

			var allNonPublicPropertiesAndFields =
				type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance)
					.Select(m => m as MemberInfo)
						.Union(
							type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
								.Select(m => m as MemberInfo))
									.ToList();

			//first remove the ignored stuff
			foreach (var mapping in Mapping.GetMapping().Where(m => m.Value._ignore))
			{
				var memberInfo = allPublicPropertiesAndFields.Where(m => m.Name == mapping.Key).SingleOrDefault();

				if (memberInfo != null)
				{
					allPublicPropertiesAndFields.Remove(memberInfo);
				}
			}

			//for clarity
			var allPropertiesAndFields = allPublicPropertiesAndFields;

			//add non-public stuff
			foreach (var mapping in Mapping.GetMapping().Where(m => m.Value._isNonPublic && !m.Value._ignore))
			{
				var memberInfo = allNonPublicPropertiesAndFields.Where(m => m.Name == mapping.Key).SingleOrDefault();

				if (memberInfo != null)
				{
					allPropertiesAndFields.Add(memberInfo);
				}
			}

			return allPropertiesAndFields.Select(m =>
			{
				var property = base.CreateProperty(m, memberSerialization);

				property.Writable = property.Readable = true;

				return property;

			}).ToList();
		}

	}

	public class MemberMapping
	{
		public class MappingInfo
		{
			protected internal bool _isNonPublic = false;
			protected internal bool _ignore = false;

			public MappingInfo()
			{

			}

			public MappingInfo(bool isNonPublic, bool ignore)
			{
				_isNonPublic = isNonPublic;
				_ignore = ignore;
			}


			public MappingInfo IsNonPublic()
			{
				_isNonPublic = true;

				return this;
			}

			public MappingInfo Ignore()
			{
				_ignore = true;

				return this;
			}
		}

		Dictionary<string, MappingInfo> _mapping = new Dictionary<string, MappingInfo>();

		public MemberMapping()
		{

		}

		public MappingInfo Map(string name)
		{
			if (!_mapping.ContainsKey(name))
			{
				_mapping.Add(name, new MappingInfo());
			}

			return _mapping[name];
		}

		public ReadOnlyDictionary<string, MappingInfo> GetMapping() => new ReadOnlyDictionary<string, MappingInfo>(_mapping);

	}

}
