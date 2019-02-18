using MrBildo.Audio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds
{
	public class Sound : ISound, IProxySerializable
	{
		private const string FILE_IDENT = "Sound";
		private const double FILE_VERSION = 1;

		public Sound(string name, string audioFile, SoundType type)
		{
			if (audioFile.IsNullorWhitespace() || !File.Exists(audioFile))
			{
				throw new ArgumentException("audioFile must be a valid file");
			}

			Name = name.WhitespaceToNull() ?? throw new ArgumentException("name is not a valid name");

			SoundType = type;
		}

		public Sound(Proxy<Sound> proxy)
		{
			Name = proxy.GetValue<string>("Name");
			AudioFile = proxy.GetValue<string>("AudioFile");
			SoundType = proxy.GetValue<SoundType>("SoundType");
			LoopEnabled = proxy.GetValue<bool>("LoopEnabled");
			MultipartLoopEnabled = proxy.GetValue<bool>("MultipartLoopEnabled");
			MultipartLoopSettings = proxy.GetValue<MultipartLoop>("MultipartLoopSettings");

			Categories = new List<string>(proxy.GetValue<string[]>("Categories"));
			Keywords = new List<string>(proxy.GetValue<string[]>("Keywords"));
		}

		public string Name { get; set; }

		public string AudioFile { get; set; }

		public SoundType SoundType { get; set; }

		public List<string> Categories { get; private set; } = new List<string>();

		public List<string> Keywords { get; private set; } = new List<string>();

		//defaults
		public bool LoopEnabled { get; set; }

		public bool MultipartLoopEnabled { get; set; }

		public MultipartLoop MultipartLoopSettings { get; set; }


		//effects go here


		//serialization stuff

		string IProxySerializable.Identifier => FILE_IDENT;

		double IProxySerializable.Version => FILE_VERSION;

		void IProxySerializable.CreateProxy(Dictionary<string, object> values)
		{
			values.Add("Name", Name);
			values.Add("AudioFile", AudioFile);
			values.Add("SoundType", SoundType);
			values.Add("LoopEnabled", LoopEnabled);
			values.Add("MultipartLoopEnabled", MultipartLoopEnabled);
			values.Add("MultipartLoopSettings", MultipartLoopSettings);

			values.Add("Categories", Categories.ToArray());

			values.Add("Keywords", Keywords.ToArray());
		}

	}
}
