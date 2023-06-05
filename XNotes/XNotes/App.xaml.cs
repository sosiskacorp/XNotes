using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;

namespace XNotes
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("ru-RU");
        }


        protected override void OnSleep()
        {
            base.OnSleep();
            if (MainPage != null)
            {
                var notesViewModel = (NotesViewModel)MainPage.BindingContext;
                notesViewModel.SaveNotes();
            }
        }


        protected override void OnResume ()
        {
        }
    }
}

