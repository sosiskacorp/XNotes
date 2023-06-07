using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace XNotes
{	
	public partial class SettingsPage : ContentPage
	{	
		public SettingsPage ()
		{
			InitializeComponent ();
		}

        private async void OnChangePasswordClicked(object sender, System.EventArgs e)
        {
            // Get the current password from SecureStorage:
            string currentPassword = await SecureStorage.GetAsync("user_password");

            // Check if the entered current password matches the saved one:
            if (currentPassword != currentPasswordEntry.Text)
            {
                await DisplayAlert("Error", "The entered current password is incorrect.", "OK");
                return;
            }

            // Check if the new and confirm passwords match:
            if (newPasswordEntry.Text != confirmPasswordEntry.Text)
            {
                await DisplayAlert("Error", "The new and confirm passwords do not match.", "OK");
                return;
            }

            // Save the new password:
            await SecureStorage.SetAsync("user_password", newPasswordEntry.Text);
            await DisplayAlert("Success", "Password changed successfully.", "OK");
        }


        private async void OnResetAppClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Подтверждение", "Вы уверены, что хотите сбросить приложение? Все заметки будут удалены.", "Да", "Нет");

            if (answer)
            {
                var notesViewModel = new NotesViewModel();

                // Сбрасываем пароль
                notesViewModel.ResetPassword();

                // Удаляем все заметки
                notesViewModel.DeleteAllNotes();

                await Navigation.PushAsync(new MainPage());
                NavigationPage.SetHasBackButton(this, false);

            }
        }



    }
}

