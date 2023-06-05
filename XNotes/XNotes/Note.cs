using System;

namespace XNotes.Models
{
    public class Note
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime LastModified { get; set; }

        public void UpdateNote(Note note)
        {
            Title = note.Title;
            Content = note.Content;
            LastModified = DateTime.Now;
        }

        public void DeleteNote()
        {
            
        }
    }
}