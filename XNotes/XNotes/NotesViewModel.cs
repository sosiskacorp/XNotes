using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using XNotes.Models;

namespace XNotes
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Note> _notes;
        private ObservableCollection<Note> _hiddenNotes;
        private Note _selectedNote;
        public bool _isHiddenNotesPage;

        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Notes)));
            }
        }

        public ObservableCollection<Note> HiddenNotes
        {
            get { return _hiddenNotes; }
            set
            {
                _hiddenNotes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HiddenNotes)));
            }
        }

        public Note SelectedNote
        {
            get { return _selectedNote; }
            set
            {
                _selectedNote = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedNote)));
            }
        }

        public NotesViewModel(bool isHiddenNotesPage = false)
        {
            _isHiddenNotesPage = isHiddenNotesPage;

            if (_isHiddenNotesPage)
            {
                LoadHiddenNotes();
            }
            else
            {
                LoadNotes();
            }

            MessagingCenter.Subscribe<NoteEntryPage, Note>(this, "AddOrUpdateNote", (sender, arg) =>
            {
                if (!arg.IsHidden)
                {
                    if (!Notes.Contains(arg))
                    {
                        Notes.Add(arg);
                    }
                    else
                    {
                        var index = Notes.IndexOf(arg);
                        Notes[index] = arg;
                    }
                }
                else
                {
                    if (!HiddenNotes.Contains(arg))
                    {
                        HiddenNotes.Add(arg);
                    }
                    else
                    {
                        var index = HiddenNotes.IndexOf(arg);
                        HiddenNotes[index] = arg;
                    }
                }

                SaveNotes();
            });

        }

        ~NotesViewModel()
        {
            MessagingCenter.Unsubscribe <NoteEntryPage, Note>(this, "AddOrUpdateNote");
        }

        public void LoadNotes()
        {
            var notesJson = Application.Current.Properties.ContainsKey("Notes") ? (string)Application.Current.Properties["Notes"] : null;

            if (notesJson != null)
            {
                Notes = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Note>>(notesJson);
            }
            else
            {
                Notes = new ObservableCollection<Note>();
            }
        }

        public void LoadHiddenNotes()
        {
            var hiddenNotesJson = Application.Current.Properties.ContainsKey("HiddenNotes") ? (string)Application.Current.Properties["HiddenNotes"] : null;

            if (hiddenNotesJson != null)
            {
                HiddenNotes = Newtonsoft.Json.JsonConvert.DeserializeObject<ObservableCollection<Note>>(hiddenNotesJson);
            }
            else
            {
                HiddenNotes = new ObservableCollection<Note>();
            }
        }

        public void DeleteAllNotes()
        {
            if (_isHiddenNotesPage)
            {
                HiddenNotes.Clear();
            }
            else
            {
                Notes.Clear();
            }

            SaveNotes();
        }

        public void ResetPassword()
        {
            SecureStorage.Remove("user_password");
        }



        public void SaveNotes()
        {
            if (_isHiddenNotesPage)
            {
                var hiddenNotesJson = Newtonsoft.Json.JsonConvert.SerializeObject(HiddenNotes);
                Application.Current.Properties["HiddenNotes"] = hiddenNotesJson;
                Application.Current.SavePropertiesAsync();
            }
            else
            {
                var notesJson = Newtonsoft.Json.JsonConvert.SerializeObject(Notes);
                Application.Current.Properties["Notes"] = notesJson;
                Application.Current.SavePropertiesAsync();
            }
        }

        public void DeleteNote()
        {
            if (_isHiddenNotesPage && SelectedNote != null)
            {
                HiddenNotes.Remove(SelectedNote);
                SelectedNote = null;
                SaveNotes();
            }
            else if (!_isHiddenNotesPage && SelectedNote != null)
            {
                Notes.Remove(SelectedNote);
                SelectedNote = null;
                SaveNotes();
            }
        }

        public void EditNote()
        {
            if (SelectedNote != null)
            {
                // Открываем страницу редактирования заметки, передавая выбранную заметку в качестве параметра.
                App.Current.MainPage.Navigation.PushAsync(new NoteEntryPage(SelectedNote, this));
            }
        }
    }
}
