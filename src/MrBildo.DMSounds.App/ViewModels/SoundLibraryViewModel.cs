using MrBildo.DMSounds.App.Messages;
using MrBildo.DMSounds.App.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MrBildo.DMSounds.App.ViewModels
{
	public class SoundLibraryViewModel : ViewModel, ISoundLibraryViewModel
	{

		readonly ISoundSettingsRepository _soundSettingsRepository;
		readonly IEventAggregator _events;

		ObservableCollection<SoundLibraryViewItem> _categoryItems = new ObservableCollection<SoundLibraryViewItem>();
		ObservableCollection<SoundLibraryViewSoundSetting> _items = new ObservableCollection<SoundLibraryViewSoundSetting>();
		SoundLibraryViewSoundSetting _selectedItem;

		readonly Stack<string> _selectedCategories = new Stack<string>();
		SoundType? _selectedType = null;

		public event Action<ISoundSettings> SelectedItemChanged = delegate { };

		public SoundLibraryViewModel(ISoundSettingsRepository repository, IEventAggregator events)
		{
			_soundSettingsRepository = repository;
			_events = events;

			BackCommand = new RelayCommand(OnBack);
			CategorySelectCommand = new RelayCommand<SoundLibraryViewCategory>(OnCategorySelected);
			TypeSelectCommand = new RelayCommand<SoundLibraryViewType>(OnTypeSelected);
		}

		public ObservableCollection<SoundLibraryViewItem> Categories
		{
			get
			{
				return _categoryItems;
			}

			set
			{
				SetProperty(ref _categoryItems, value);
			}
		}

		public ObservableCollection<SoundLibraryViewSoundSetting> Items
		{
			get
			{
				return _items;
			}

			set
			{
				SetProperty(ref _items, value);
			}
		}

		public SoundLibraryViewSoundSetting SelectedItem
		{
			get
			{
				return _selectedItem;
			}
			set
			{
				SetProperty(ref _selectedItem, value);

				SelectedItemChanged(value?.SoundSettings);
			}
		}

		public RelayCommand BackCommand { get; private set; }

		public RelayCommand<SoundLibraryViewCategory> CategorySelectCommand { get; private set; }

		public RelayCommand<SoundLibraryViewType> TypeSelectCommand { get; private set; }

		public async void Load()
		{
			await LoadItems();
		}

		private async Task LoadItems()
		{
			_categoryItems.Clear();
			_items.Clear();

			//SelectedItem = null;

			//if nothing is selected then default to the 3 types
			if (_selectedType == null)
			{
				_categoryItems.Add(new SoundLibraryViewType(SoundType.AmbientSound));
				_categoryItems.Add(new SoundLibraryViewType(SoundType.SoundEffect));
				_categoryItems.Add(new SoundLibraryViewType(SoundType.MusicBed));

				return;
			}

			_categoryItems.Add(new BackSoundLibraryViewCategory());

			var categoryItems = await _soundSettingsRepository.LoadAllByTypeAndCategories(_selectedType.Value, _selectedCategories.ToArray());

			foreach(var category in categoryItems.Categories)
			{
				_categoryItems.Add(new SoundLibraryViewCategory(category));
			}

			foreach(var item in categoryItems.Items)
			{
				_items.Add(new SoundLibraryViewSoundSetting(item));
			}
		}

		private async void OnBack()
		{
			if(_selectedCategories.Count == 0)
			{
				_selectedType = null;
			}
			else
			{
				_selectedCategories.Pop();
			}

			await LoadItems();
		}

		private async void OnCategorySelected(SoundLibraryViewCategory category)
		{
			_selectedCategories.Push(category.Name);

			await LoadItems();
		}

		private async void OnTypeSelected(SoundLibraryViewType type)
		{
			_selectedType = type.Type;

			await LoadItems();
		}
	}
}
