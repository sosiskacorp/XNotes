using Xamarin.Forms;
using XNotes.Models;

namespace XNotes
{
    public partial class NoteDetailsPage : ContentPage
    {
        private Note _note;

        public NoteDetailsPage(Note note)
        {
            InitializeComponent();
            _note = note;
            Title = _note.Title;
            noteContentLabel.Text = _note.Content;
        }
    }
}
