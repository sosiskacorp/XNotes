﻿using Xamarin.Forms;
using XNotes.Models;

namespace XNotes
{
    public partial class HiddenNotesPage : ContentPage
    {
        private NotesViewModel _viewModel;

        public HiddenNotesPage()
        {
            InitializeComponent();

            _viewModel = new NotesViewModel(isHiddenNotesPage: true);
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadNotes();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _viewModel.SaveNotes();
        }

        private async void OnNewHiddenNoteClicked(object sender, System.EventArgs e)
        {
            var newNote = new Note { IsHidden = true };
            await Navigation.PushAsync(new NoteEntryPage(newNote, _viewModel));
        }

        private async void OnNoteSelected(object sender, ItemTappedEventArgs e)
        {
            var selectedNote = e.Item as Note;
            await Navigation.PushAsync(new NoteDetailsPage(selectedNote, _viewModel));
        }

        private async void OnEditClicked(object sender, System.EventArgs e)
        {
            var note = (sender as MenuItem).CommandParameter as Note;
            await Navigation.PushAsync(new NoteEntryPage(note, _viewModel));
        }

        private void OnDeleteClicked(object sender, System.EventArgs e)
        {
            var note = (sender as MenuItem).CommandParameter as Note;
            _viewModel.HiddenNotes.Remove(note);
            _viewModel.SaveNotes();
        }

        // New method to handle the Go Back button click
        private async void OnGoBackClicked(object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
