using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace MrBildo.DMSounds.App
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private static IWindsorContainer _container;

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			_container = BuildContainer();

			var main = _container.Resolve<MainView>();

			main.Show();

		}

		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			MessageBox
				.Show($"Something very bad just happened that we weren't expecting. Sorry about that! Please contact the creators of this thing and give them this information: " +
					$"{Environment.NewLine} {Environment.NewLine} {e.Exception}");

			e.Handled = true;
		}

		private IWindsorContainer BuildContainer()
		{
			var container = new WindsorContainer();

			container.AddFacility<TypedFactoryFacility>();

			container.Register(
				Component.For<ISoundSettingsRepository>()
					.ImplementedBy<SoundSettingsRepository>()
						.LifeStyle
							.Transient
								.DependsOn(Dependency.OnValue("path", ".\\sounds"))); //TODO: configuration!

			container.Register(

				//type factories
				Types.FromAssemblyInThisApplication()
					.Where(a => a.Name.EndsWith("Factory"))
						.Configure(c => c.AsFactory()),

				//repositories
				Classes.FromAssemblyInThisApplication()
					.Where(a => a.Name.EndsWith("Repository"))
						.WithServiceDefaultInterfaces()
							.LifestyleSingleton(),
				
				//view models
				Classes.FromAssemblyInThisApplication()
					.Where(a => a.Name.EndsWith("ViewModel"))
						.WithServiceDefaultInterfaces()
							.LifestyleTransient(),

				//views
				Classes.FromAssemblyInThisApplication()
					.Where(a => a.Name.EndsWith("View"))
						.WithServiceDefaultInterfaces()
							.LifestyleTransient()

			);

			container.Register(Component.For<ISound>().ImplementedBy<Sound>().LifeStyle.Transient);

			container.Register(Component.For<ISoundSettings>().ImplementedBy<SoundSettings>().LifeStyle.Transient);

			container.Register(Component.For<IScene>().ImplementedBy<Scene>().LifeStyle.Transient);

			container.Register(Component.For<ISession>().ImplementedBy<Session>().LifeStyle.Transient);

			container.Register(Component.For<ISoundService>().ImplementedBy<SoundService>().LifeStyle.Singleton);

			container.Register(Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifeStyle.Singleton);

			return container;
		} 
	}
}
