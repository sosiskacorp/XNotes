using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XNotes
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnUserAgreementClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserAgreementPage());
        }


        public async void OnStartClicked(object sender, EventArgs e)
        {
            // Check if the user has set a password already
            if (string.IsNullOrEmpty(await SecureStorage.GetAsync("user_password")))
            {
                // If not, navigate to the PasswordPage to create one
                await Navigation.PushAsync(new PasswordPage());
            }
            else
            {
                // If yes, navigate to the NotesPage
                await Navigation.PushAsync(new NotesPage());
            }
        }
    }
}
