using System;
using Xamarin.Forms;
using XNotes.Models;

namespace XNotes
{
    public partial class NoteEntryPage : ContentPage
    {
        private Note _note;

        public NoteEntryPage(Note note)
        {
            InitializeComponent();

            _note = note;
            BindingContext = _note;
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            _note.LastModified = DateTime.Now;
            MessagingCenter.Send(this, "AddOrUpdateNote", _note);
            Navigation.PopAsync();
        }

    }
}
