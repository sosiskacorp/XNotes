using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using XNotes.Models;

namespace XNotes
{
    public partial class NotesPage : ContentPage
    {
        private NotesViewModel _viewModel;

        public NotesPage()
        {
            InitializeComponent();

            _viewModel = new NotesViewModel();
            BindingContext = _viewModel;

            // Check if the user has set a password already
            if (string.IsNullOrEmpty(SecureStorage.GetAsync("user_password").Result))
            {
                // If not, navigate to the PasswordPage to create one
                Navigation.PushAsync(new PasswordPage());
            }
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

        private async void OnNewNoteClicked(object sender, System.EventArgs e)
        {
            var newNote = new Note();
            await Navigation.PushAsync(new NoteEntryPage(newNote, _viewModel));
        }

        private async void OnNoteSelected(object sender, ItemTappedEventArgs e)
        {
            var selectedNote = e.Item as Note;
            await Navigation.PushAsync(new NoteDetailsPage(selectedNote, _viewModel));
        }


        private async void OnEditClicked(object sender, EventArgs e)
        {
            var note = (sender as MenuItem).CommandParameter as Note;
            await Navigation.PushAsync(new NoteEntryPage(note, _viewModel));
        }


        private void OnDeleteClicked(object sender, System.EventArgs e)
        {
            var note = (sender as MenuItem).CommandParameter as Note;
            _viewModel.Notes.Remove(note);
            _viewModel.SaveNotes();
        }

        // New method to handle the hidden notes button click
        private async void OnHiddenNotesClicked(object sender, System.EventArgs e)
        {
            var storedPassword = await SecureStorage.GetAsync("user_password");

            if (string.IsNullOrEmpty(storedPassword))
            {
                // If there is no password set, navigate to the PasswordPage to create one
                await Navigation.PushAsync(new PasswordPage());
            }
            else
            {
                // If there is a password set, ask the user to enter it
                var password = await DisplayPromptAsync("Password Required", "Enter your password", maxLength: 20, keyboard: Keyboard.Default);

                if (password == storedPassword)
                {
                    // If the password is correct, navigate to the HiddenNotesPage
                    await Navigation.PushAsync(new HiddenNotesPage());
                }
                else
                {
                    // If the password is incorrect, show an error message
                    await DisplayAlert("Access Denied", "Incorrect password. Please try again.", "OK");
                }
            }
        }

        // New method to handle the settings button click
        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}