using LoL_Game.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.Diagnostics;
using Windows.System;

namespace LoL_Game
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public StorageFolder AppDir {
            get
            {
                Debug.WriteLine(ApplicationData.Current.LocalFolder.Path);
                return ApplicationData.Current.LocalFolder;
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public LanguageModel SelectedCbDefaultLanguageListItem = new LanguageModel { Name = "English", Id = -1 };

        public ObservableCollection<LanguageModel> LanguagesList { get; set; }

        private int _selectedLanguageID;

        public int SelectedLanguageId
        {
            get { return _selectedLanguageID; }
            set
            {
                if (_selectedLanguageID != value)
                {
                    _selectedLanguageID = value;

                    RaisePropertyChanged("SelectedLanguageId");
                }
            }
        }

        public MainViewModel()
        {
            LanguagesList = new ObservableCollection<LanguageModel>();
            int count = 0;
            if (!Directory.Exists(AppDir.Path + "/lang")) {
                Directory.CreateDirectory(AppDir.Path + "/lang");
            }
            
         
            foreach (var v in Directory.GetFiles(AppDir.Path + "/lang")) {
                var t = File.ReadAllText(v);
                if (t == "")
                {
                    File.WriteAllText(v, "<test>");
                }
                else {
                    LanguagesList.Insert(0, (new LanguageModel { Id = count, Name = v.Split('\\')[v.Split('\\').Length-1]}));
                    count++;
                }
            }

            LanguagesList.Insert(0, SelectedCbDefaultLanguageListItem);

            SelectedLanguageId = SelectedCbDefaultLanguageListItem.Id;
        }

        public void ResetSelectedItem()
        {
            SelectedLanguageId = SelectedCbDefaultLanguageListItem.Id;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
