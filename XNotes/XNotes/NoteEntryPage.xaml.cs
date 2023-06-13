using System;
using Xamarin.Forms;
using XNotes.Models;

namespace XNotes
{
    public partial class NoteEntryPage : ContentPage
    {
        private Note _note;
        private NotesViewModel _viewModel;

        public NoteEntryPage(Note note, NotesViewModel viewModel)
        {
            InitializeComponent();

            _note = note;
            _viewModel = viewModel;
            BindingContext = _note;
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            _note.LastModified = DateTime.Now;

            if (_note.IsHidden)
            {
                if (!_viewModel.HiddenNotes.Contains(_note))
                {
                    _viewModel.HiddenNotes.Add(_note);
                }
            }
            else
            {
                if (!_viewModel.Notes.Contains(_note))
                {
                    _viewModel.Notes.Add(_note);
                }
            }

            _viewModel.SaveNotes();
            Navigation.PopAsync();
        }

    }
}
