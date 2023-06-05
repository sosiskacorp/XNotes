using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using XNotes.Models;

namespace XNotes
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<Note> _notes;
        private Note _selectedNote;

        public ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Notes)));
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

        public NotesViewModel()
        {
            LoadNotes();

            MessagingCenter.Subscribe<NoteEntryPage, Note>(this, "AddOrUpdateNote", (sender, arg) =>
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

                SaveNotes();
            });

        }

        ~NotesViewModel()
        {
            MessagingCenter.Unsubscribe<NoteEntryPage, Note>(this, "AddOrUpdateNote");
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

        

        public void SaveNotes()
        {
            var notesJson = Newtonsoft.Json.JsonConvert.SerializeObject(Notes);
            Application.Current.Properties["Notes"] = notesJson;
            Application.Current.SavePropertiesAsync();
        }

        public void DeleteNote()
        {
            if (SelectedNote != null)
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
                App.Current.MainPage.Navigation.PushAsync(new NoteEntryPage(SelectedNote));
            }
        }
    }
}