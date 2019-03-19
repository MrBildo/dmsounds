using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MrBildo.DMSounds.Castle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MrBildo.DMSounds.WinForm
{
	static class Program
	{
		public static IWindsorContainer Container { get; set; }
		
		[STAThread]
		static void Main()
		{
			Container = BuildContainer();

			var testFile = @"D:\mrbildo\Music\Harvey Danger\Harvey Danger - Flagpole Sitta.mp3";
			var testFile1 = @"D:\DnD\Audio\Dark Magic Shadow Spell Cast 2.mp3";

			var settingsFactory = Container.Resolve<ISoundSettingsFactory>();
			//var factory = Container.Resolve<ISceneFactory>();

			var settings = settingsFactory.Create("remove this", testFile, SoundType.MusicBed);
			var settings1 = settingsFactory.Create("remove this", testFile1, SoundType.MusicBed);

			settings1.LoopEnabled = true;

			var sceneFactory = Container.Resolve<ISceneFactory>();

			var scene = sceneFactory.Create("My Scene");

			scene.AddMusicBed(settings);
			scene.AddMusicBed(settings1);

			scene.Play();

			//var soundFactory = Container.Resolve<ISoundFactory>();

			//var sound = soundFactory.Create(settings);
			//var sound1 = soundFactory.Create(settings1);

			//sound.Play();


			//var scene = factory.Create("Test Scene");

			//var setting = factory.Create("Test", @"c:\eula.1028.txt", SoundType.AmbientSound);
			////var sound = factory.CreateSound();
			//var sound = factory.Create("test");

			//var repository = Container.Resolve<ISoundRepository>();

			//repository.SaveSoundSettings(setting, @"c:\sound.json");

			//var newSetting = repository.LoadSoundSettings(@"c:\sound.json");

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}

		static void InitializeObjects()
		{

		}

		static IWindsorContainer BuildContainer()
		{
			var container = new WindsorContainer();

			container.AddFacility<TypedFactoryFacility>();

			container.Register(
				Component.For<ISound>().ImplementedBy<Sound>().LifeStyle.Transient,
				Component.For<ISoundFactory>().AsFactory()
			);

			container.Register(
				Component.For<ISoundSettings>().ImplementedBy<SoundSettings>().LifeStyle.Transient,
				Component.For<ISoundSettingsFactory>().AsFactory()
			);

			container.Register(
				Component.For<IScene>().ImplementedBy<Scene>().LifeStyle.Transient,
				Component.For<ISceneFactory>().AsFactory()
			);

			//container.Register(
			//	Component.For<ISound>().ImplementedBy<Sound>().LifeStyle.Transient.Activator<NonPublicComponentActivator>(),
			//	Component.For<ISoundFactory>().AsFactory()
			//);

			//container.Register(
			//	Component.For<ISoundSettings>().ImplementedBy<SoundSettings>().LifeStyle.Transient.Activator<NonPublicComponentActivator>(),
			//	Component.For<ISoundSettingsFactory>().AsFactory()
			//);

			//container.Register(
			//	Component.For<IScene>().ImplementedBy<Scene>().LifeStyle.Transient.Activator<NonPublicComponentActivator>(),
			//	Component.For<ISceneFactory>().AsFactory()
			//);

			container.Register(
				Component.For<ISoundRepository>().ImplementedBy<SoundRepository>().LifeStyle.Singleton	
			);

			container.Register(
				Component.For<ISoundService>().ImplementedBy<SoundService>().LifeStyle.Singleton
			);

			return container;
		}

	}
}
