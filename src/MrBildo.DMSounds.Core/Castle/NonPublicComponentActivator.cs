using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using Castle.MicroKernel.Context;

namespace MrBildo.DMSounds.Castle
{
	[Serializable]
	public class NonPublicComponentActivator : DefaultComponentActivator
	{
		public NonPublicComponentActivator(ComponentModel model, IKernelInternal kernel, ComponentInstanceDelegate onCreation, ComponentInstanceDelegate onDestruction)
			: base(model, kernel, onCreation, onDestruction)
		{ /* do nothing */ }

		private readonly List<Type> loadedTypes = new List<Type>();
		protected override ConstructorCandidate SelectEligibleConstructor(CreationContext context)
		{
			lock (loadedTypes)
			{
				if (!loadedTypes.Contains(context.RequestedType))
				{
					loadedTypes.Add(context.RequestedType);

					// Add the missing non-public constructors too:
					var ctors = Model.Implementation.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

					foreach (var ctor in ctors)
					{
						Model.AddConstructor
						(
							new ConstructorCandidate
							(
								ctor,
								ctor.GetParameters().Select(pi => new ConstructorDependencyModel(pi)).ToArray()
							)
						);
					}
				}
			}

			return base.SelectEligibleConstructor(context);
		}
	}

}
