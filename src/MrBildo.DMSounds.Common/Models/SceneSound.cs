using MrBildo.Audio;
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
	public class SceneSound : ISerializable, ISceneSound
	{

		private const string FILE_IDENT = "SceneSound";
		private const int FILE_VERSION = 1;

		private AudioTrack _audioTrack = null;

		private DMSceneSoundAudioTrackInfo _audioTrackInfo = null;

		protected SceneSound(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			//ignore header stuff for now

			Name = info.GetValue<string>("Name");
			AudioFile = info.GetValue<string>("AudioFile");
			SoundType = info.GetValue<SoundType>("SoundType");

			//in order to lazy load the audio track we need to store the settings from disk
			_audioTrackInfo = new DMSceneSoundAudioTrackInfo()
			{
				Volume = info.GetValue<float>("Volume"),
				LoopEnabled = info.GetValue<bool>("LoopEnabled"),
				PanningEnabled = info.GetValue<bool>("PanningEnabled"),
				Pan = info.GetValue<float>("Pan"),
				MultipartLoopEnabled = info.GetValue<bool>("MultipartLoopEnabled"),
				MultipartLoopSettings = info.GetValue<MultipartLoop>("MultipartLoopSettings")
			};

		}

		public SceneSound(Sound sound)
		{
			//first, create the audiotrack
			AudioTrack = SoundService.AudioEngine.AddAudioTrack(sound.Name, sound.AudioFile);

			//set the name as the sound name initially
			Name = sound.Name;

			//need to save the audio file for later
			AudioFile = sound.AudioFile;

			SoundType = sound.SoundType;

			//settings for audio track
			AudioTrack.Loop = sound.LoopEnabled;
			AudioTrack.MultipartLoopEnabled = sound.MultipartLoopEnabled;
			AudioTrack.MultipartLoop = sound.MultipartLoopSettings;

		}

		public ISoundService SoundService { get; set; }

		public string Name { get; set; }

		public string AudioFile { get; private set; }

		public SoundType SoundType { get; set; }

		private AudioTrack AudioTrack
		{
			get
			{
				if(_audioTrack == null)
				{
					if(_audioTrackInfo == null)
					{
						throw new InvalidOperationException("Can't create audio track; audio track info is null.");
					}

					_audioTrack = SoundService.AudioEngine.AddAudioTrack(Name, AudioFile);

					_audioTrack.Volume = _audioTrackInfo.Volume;
					_audioTrack.Loop = _audioTrackInfo.LoopEnabled;
					_audioTrack.PanningEnabled = _audioTrackInfo.PanningEnabled;
					_audioTrack.Pan = _audioTrackInfo.Pan;
					_audioTrack.MultipartLoopEnabled = _audioTrackInfo.MultipartLoopEnabled;
					_audioTrack.MultipartLoop = _audioTrackInfo.MultipartLoopSettings;
				}

				return _audioTrack;
			}

			set
			{
				_audioTrack = value;
			}
		}

		public float Volume { get => AudioTrack.Volume; set => AudioTrack.Volume = value; }

		public bool LoopEnabled { get => AudioTrack.Loop; set => AudioTrack.Loop = value; }

		public bool PanningEnabled { get => AudioTrack.PanningEnabled; set => AudioTrack.PanningEnabled = value; }

		public float Pan { get => AudioTrack.Pan; set => AudioTrack.Pan = value; }

		public bool MultipartLoopEnabled { get => AudioTrack.MultipartLoopEnabled; set => AudioTrack.MultipartLoopEnabled = value; }

		public MultipartLoop MultipartLoopSettings => AudioTrack.MultipartLoop;

		public AudioTrackState State => AudioTrack.State;

		public void FadeIn(TimeSpan duration)
		{
			AudioTrack.FadeIn(duration);
		}

		public void FadeOut(TimeSpan duration)
		{
			AudioTrack.FadeOut(duration);
		}

		public void Play()
		{
			AudioTrack.Play();
		}

		public void Pause()
		{
			AudioTrack.Pause();
		}

		public void Stop()
		{
			AudioTrack.Stop();
		}

		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			//"header" information
			info.AddValue("FileType", FILE_IDENT);
			info.AddValue("FileVersion", FILE_VERSION);

			info.AddValue("Name", Name);
			info.AddValue("AudioFile", AudioFile);
			info.AddValue("SoundType", SoundType);
			info.AddValue("Volume", Volume);
			info.AddValue("LoopEnabled", LoopEnabled);
			info.AddValue("PanningEnabled", PanningEnabled);
			info.AddValue("Pan", Pan);
			info.AddValue("MultipartLoopEnabled", MultipartLoopEnabled);
			info.AddValue("MultipartLoopSettings", MultipartLoopSettings);
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}

			GetObjectData(info, context);
		}

		private class DMSceneSoundAudioTrackInfo
		{
			public float Volume { get; set; }

			public bool LoopEnabled { get; set; }

			public bool PanningEnabled { get; set; }

			public float Pan { get; set; }

			public bool MultipartLoopEnabled { get; set; }

			public MultipartLoop MultipartLoopSettings { get; set; }
		}
	}
}
