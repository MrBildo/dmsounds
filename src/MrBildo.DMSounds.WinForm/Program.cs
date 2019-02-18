using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
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

			var factory = Container.Resolve<ISoundFactory>();

			//var sound = factory.CreateSound("Test", @"c:\eula.1028.txt", SoundType.AmbientSound);
			//var sound = factory.CreateSound();

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

			//container.Register(Component.For<ISound>().ImplementedBy<Sound>().LifeStyle.Transient);


			return container;
		}

	}
}
