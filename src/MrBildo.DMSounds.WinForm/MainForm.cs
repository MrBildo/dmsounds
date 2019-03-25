using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBildo.DMSounds.WinForm
{
	public partial class MainForm : Form
	{
		ISession session;

		public MainForm()
		{
			InitializeComponent();

			var testFile = @"D:\mrbildo\Music\Harvey Danger\Harvey Danger - Flagpole Sitta.mp3";
			var testFile1 = @"D:\DnD\Audio\Dark Magic Shadow Spell Cast 2.mp3";

			var settingsFactory = Program.Container.Resolve<ISoundSettingsFactory>();

			var settings = settingsFactory.Create("remove this", testFile, SoundType.MusicBed);
			var settings1 = settingsFactory.Create("remove this", testFile1, SoundType.MusicBed);

			settings1.LoopEnabled = true;

			var sceneFactory = Program.Container.Resolve<ISceneFactory>();

			var scene = sceneFactory.Create("My Scene");
			var scene1 = sceneFactory.Create("My Scene1");

			scene.AddMusicBed(settings);
			scene1.AddMusicBed(settings1);

			session = Program.Container.Resolve<ISessionFactory>().Create("My Session");

			session.AddScene(scene);
			session.AddScene(scene1);

		}

		private void button1_Click(object sender, EventArgs e)
		{
			session.Scenes.Last().Play();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			Program.Container.Resolve<ISoundService>().AudioEngine.Dispose();

			base.OnClosing(e);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var to = session.Scenes.First();
			var from = session.Scenes.Last();

			session.TransitionScenes(from, to, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(0));
		}
	}
}
