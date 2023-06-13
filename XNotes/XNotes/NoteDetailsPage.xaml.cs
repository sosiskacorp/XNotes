using Xamarin.Forms;
using XNotes.Models;

namespace XNotes
{
    public partial class NoteDetailsPage : ContentPage
{
    private Note _note;
    private NotesViewModel _viewModel;

    public NoteDetailsPage(Note note, NotesViewModel viewModel)
    {
        InitializeComponent();

        _note = note;
        _viewModel = viewModel;

        BindingContext = _note;
    }
}

}
