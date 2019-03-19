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
	public class SoundSettings : ISoundSettings
	{
		public SoundSettings(string name, string audioFile, SoundType type)
		{
			if (audioFile.IsNullorWhitespace() || !File.Exists(audioFile))
			{
				throw new ArgumentException("audioFile must be an existing file");
			}

			Name = name.WhitespaceToNull() ?? throw new ArgumentException("name cannot be null");

			Type = type;
		}

		internal SoundSettings(string filename)
		{
			if (filename.IsNullorWhitespace() || !File.Exists(filename))
			{
				throw new ArgumentException("filename must be an existing file");
			}

			Filename = filename;
		}

		public string Name { get; set; }

		public string AudioFile { get; set; }

		public SoundType Type { get; set; }

		public List<string> Categories { get; private set; } = new List<string>();

		public List<string> Keywords { get; private set; } = new List<string>();

		//defaults
		public bool LoopEnabled { get; set; }

		public bool MultipartLoopEnabled { get; set; }

		public MultipartLoop MultipartLoopSettings { get; set; }

		public string Filename { get; private set; }


		//effects go here

	}
}
