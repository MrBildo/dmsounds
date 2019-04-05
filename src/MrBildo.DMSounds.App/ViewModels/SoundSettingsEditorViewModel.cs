using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrBildo.DMSounds.App.ViewModels
{
    public class SoundSettingsEditorViewModel : ViewModel, ISoundSettingsEditorViewModel
    {
		private ISoundSettings _soundSettings;

		//name*
		//audio file*
		//type*
		//categories
		//keywords (todo?)

		//defaults---->
		//loop
		//multipart loop and settings

		//how to handle add new
		//many suggest removing from the collection, however I only know about the new item
		//so then the OK, Cancel functionality is part of the parent?
		//this control only edits, it doesn't care if its new or not

		//use ExplicitUpdate trigger?
		//https://stackoverflow.com/questions/2559819/edit-dialog-with-bindings-and-ok-cancel-in-wpf
		
		//do I need to add something like DialogResult?

		//add property to determine dialog result -- container?

		public ISoundSettings SelectedItem
		{
			get
			{
				return _soundSettings;
			}

			set
			{
				SetProperty(ref _soundSettings, value);
			}
		}

    }
}
