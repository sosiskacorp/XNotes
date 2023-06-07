using Xamarin.Essentials;
using Xamarin.Forms;

namespace XNotes
{
    public partial class PasswordPage : ContentPage
    {
        public PasswordPage()
        {
            InitializeComponent();
        }

        private async void OnSavePasswordClicked(object sender, System.EventArgs e)
        {
            // Save the user's password here, for example using SecureStorage:
            await SecureStorage.SetAsync("user_password", passwordEntry.Text);
            await Navigation.PushAsync(new NotesPage());
        }
    }
}
