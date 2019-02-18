using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public interface IProxySerializable
	{
		string Identifier { get; }

		double Version { get; }

		void CreateProxy(Dictionary<string, object> values);
	}
}
