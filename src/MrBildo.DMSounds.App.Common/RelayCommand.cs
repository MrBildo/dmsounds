using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MrBildo.DMSounds.App
{
	public class RelayCommand : ICommand
	{
		readonly Action _executeAction;

		readonly Func<bool> _canExecute;

		public RelayCommand(Action executeAction, Func<bool> canExecute)
		{
			_executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
			_canExecute = canExecute;
		}

		public RelayCommand(Action executeAction) : this(executeAction, null)
		{

		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute();
		}

		public void Execute(object parameter)
		{
			_executeAction();
		}

		public void RaiseCanExecuteChanged()
		{
			CommandManager.InvalidateRequerySuggested();
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				if(_canExecute != null)
				{
					CommandManager.RequerySuggested += value;
				}
			}

			remove
			{
				if (_canExecute != null)
				{
					CommandManager.RequerySuggested += value;
				}
			}
		}

	}

	public class RelayCommand<T> : ICommand
	{
		readonly Action<T> _executeAction;

		readonly Predicate<T> _canExecute;

		public RelayCommand(Action<T> executeAction, Predicate<T> canExecute)
		{
			_executeAction = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
			_canExecute = canExecute;
		}

		public RelayCommand(Action<T> executeAction) : this(executeAction, null)
		{

		}

		public bool CanExecute(object parameter)
		{
			return _canExecute == null ? true : _canExecute((T)parameter);
		}

		public void Execute(object parameter)
		{
			_executeAction((T)parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			CommandManager.InvalidateRequerySuggested();
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (_canExecute != null)
				{
					CommandManager.RequerySuggested += value;
				}
			}

			remove
			{
				if (_canExecute != null)
				{
					CommandManager.RequerySuggested += value;
				}
			}
		}
	}
}
