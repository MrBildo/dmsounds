using MrBildo.Audio.NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace MrBildo.Audio
{
	public enum AudioTrackState
	{
		Stopped,
		Paused,
		Playing
	}

	public class AudioTrack : IAudioTrack
	{
		private RawSourceWaveStream _currentStream = null;

		private ISampleProvider _outputSample = null;

		private TimeSpan _pauseTime;

		//streams and sample providers
		private LoopingWaveStream _loopingProvider = null;
		private FadeInOutStopSampleProvider _fadingProvider = null;
		private PanningSampleProvider _panningProvider = null;
		private VolumeSampleProvider _volumeProvider = null;


		//sample configuration
		private float _volume = 1f;
		private bool _loop = false;
		private float _pan = 0f;

		//timer for multiloop functionality
		Timer _timer;

		private bool _disposed = false;

		internal AudioTrack(string filename, MixingSampleProvider mixer)
		{
			using (var reader = new AudioFileReader(filename))
			{
				//if the mixer and file wave formats do not match, re-sample
				if(!reader.WaveFormat.AreFormatsEqual(mixer.WaveFormat))	
				{
					//re-sample the audio to match the mixer wave format
					using(var sampler = new MediaFoundationResampler(reader, mixer.WaveFormat))
					{
						using(var memory = new MemoryStream())
						{
							WaveFileWriter.WriteWavFileToStream(memory, sampler);

							AudioData = memory.GetBuffer();
						}
					}
				}
				else
				{
					//no need to re-sample
					var data = new byte[reader.Length];

					reader.Read(data, 0, (int)reader.Length);

					AudioData = data;

				}

				//set a default multipart loop
				MultipartLoop = new MultipartLoop(reader.CurrentTime, reader.TotalTime);

			}

			Mixer = mixer;

			Mixer.MixerInputEnded += MixerInputEnded;

		}

		public TimeSpan CurrentTime => _currentStream.CurrentTime;

		public float Volume
		{
			get
			{
				return _volume;
			}

			set
			{
				_volume = value;

				if(_volumeProvider != null)
				{
					_volumeProvider.Volume = value;
				}
			}
		}

		public bool Loop
		{
			get
			{
				return _loop;
			}

			set
			{
				_loop = value;

				if(_loopingProvider != null)
				{
					_loopingProvider.EnableLooping = value;
				}
			}
		}

		public float Pan
		{
			get
			{
				return _pan;
			}

			set
			{
				if(value > 1)
				{
					value = 1;
				}

				if(value < -1)
				{
					value = -1;
				}

				_pan = value;

				if(_panningProvider != null)
				{
					_panningProvider.Pan = value;
				}
			}
		}

		public bool PanningEnabled { get; set; } = false;

		public bool MultipartLoopEnabled { get; set; } = false;

		public MultipartLoop MultipartLoop { get; set; }

		public AudioTrackState State { get; set; } = AudioTrackState.Stopped;

		private MixingSampleProvider Mixer { get; set; }

		private byte[] AudioData { get; set; }

		private WaveFormat AudioFormat => Mixer.WaveFormat;

		public void FadeIn(TimeSpan duration)
		{
			Play(true, duration.TotalMilliseconds);
		}

		public void FadeOut(TimeSpan duration)
		{
			if (State == AudioTrackState.Playing)
			{
				_fadingProvider.BeginFadeOut(duration.TotalMilliseconds);
			}
		}

		public void Play()
		{
			Play(false);
		}

		private void Play(bool fadeIn, double duration = 0)
		{
			if(State == AudioTrackState.Playing)
			{
				return;
			}

			_currentStream = new RawSourceWaveStream(new MemoryStream(AudioData), AudioFormat);

			_outputSample = CreateSampleChain(_currentStream);

			if (MultipartLoopEnabled)
			{
				SetupMultipartLoop();
				_currentStream.CurrentTime = MultipartLoop.StartTime;
			}

			if (State == AudioTrackState.Paused)
			{
				_currentStream.CurrentTime = _pauseTime;
			}

			if (fadeIn)
			{
				_fadingProvider.BeginFadeIn(duration);
			}

			Mixer.AddMixerInput(_outputSample);

			State = AudioTrackState.Playing;

		}

		public void Pause()
		{
			if(State == AudioTrackState.Playing)
			{
				Mixer.RemoveMixerInput(_outputSample);

				_pauseTime = _currentStream.CurrentTime;

				_currentStream.Dispose();

				State = AudioTrackState.Paused;
			}
		}

		public void Stop()
		{
			if(State != AudioTrackState.Stopped)
			{
				//make the stopping slightly less harsh
				_fadingProvider.BeginFadeOut(250);
	
			}
		}

		private ISampleProvider CreateSampleChain(WaveStream sourceStream)
		{
			//loop is implemented as a wave stream
			_loopingProvider = new LoopingWaveStream(sourceStream);
			_loopingProvider.EnableLooping = _loop;

			var outputSample = _loopingProvider.ToSampleProvider();

			//fade-in/fade-out
			_fadingProvider = new FadeInOutStopSampleProvider(outputSample);
			outputSample = _fadingProvider;

			//panning (will convert to mono)
			if (PanningEnabled)
			{
				var monoProvider = new StereoToMonoSampleProvider(outputSample);

				_panningProvider = new PanningSampleProvider(monoProvider);

				_panningProvider.PanStrategy = new StereoBalanceStrategy();
				_panningProvider.Pan = Pan;

				outputSample = _panningProvider;
			}

			//volume
			_volumeProvider = new VolumeSampleProvider(outputSample);
			_volumeProvider.Volume = _volume;

			outputSample = _volumeProvider;

			return outputSample;
		}

		private void SetupMultipartLoop()
		{
			_timer = new Timer(10);

			_timer.Elapsed += MultipartLoopTimer_Elapsed;

			_timer.Enabled = true;
		}

		private void MultipartLoopTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			//possibly use signal time for higher precision
			if (!MultipartLoop.IsAtEnd)
			{
				if(_currentStream.CurrentTime >= MultipartLoop.CurrentCue.End)
				{
					_currentStream.CurrentTime = MultipartLoop.CurrentCue.Start;
				}
			}
		}

		private void MixerInputEnded(object sender, SampleProviderEventArgs e)
		{
			if(State == AudioTrackState.Stopped || _outputSample == null)
			{
				return;
			}

			//check if this is our sample
			if(e.SampleProvider.GetHashCode() == _outputSample.GetHashCode())
			{
				if(_timer != null)
				{
					_timer.Dispose();
				}

				_currentStream.Dispose();

				State = AudioTrackState.Stopped;
			}
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			if (disposing)
			{
				State = AudioTrackState.Stopped;

				if (_timer != null)
				{
					_timer.Dispose();
				}

				if(_currentStream != null)
				{
					_currentStream.Dispose();
				}

			}

			_disposed = true;
		}
	}
}
