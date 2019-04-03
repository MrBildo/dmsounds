using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App
{
	public interface IEventAggregator
	{
		bool Exists<T>(object subscriber);
		bool Exists<T>(object subscriber, Action<T> handler);
		void Publish<T>(T data = default(T));
		void Publish<T>(object sender, T data = default(T));
		void Subscribe<T>(Action<T> handler);
		void Subscribe<T>(object subscriber, Action<T> handler);
		void Unsubscribe();
		void Unsubscribe(object subscriber);
		void Unsubscribe<T>();
		void Unsubscribe<T>(Action<T> handler);
		void Unsubscribe<T>(object subscriber, Action<T> handler = null);
	}
}
