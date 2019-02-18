using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	[Serializable]
	public class Proxy<TProxyTarget> : ISerializable where TProxyTarget : IProxySerializable
	{
		Dictionary<string, object> _values = new Dictionary<string, object>();

		public Proxy(TProxyTarget o)
		{
			o.CreateProxy(_values);
		}

		protected Proxy(SerializationInfo info, StreamingContext context)
		{
			_values = info.GetValue<Dictionary<string, object>>("Values");
		}

		public Dictionary<string, object> GetValues()
		{
			return _values;
		}

		public TValueType GetValue<TValueType>(string name)
		{
			return (TValueType)_values[name];
		} 

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Values", _values);
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}
	}
}
