using Notes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes.Views
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class NoteEntryPage : ContentPage
    {
        public string ItemId
        {
            set
            {
                LoadNote(value);
            }
        }

        public NoteEntryPage()
        {
            InitializeComponent();

            // Set the BindingContext of the page to a new Note.
            BindingContext = new Note();
        }

        async void LoadNote(string itemId)
        {
            try
            {
                int id = Convert.ToInt32(itemId);
                // Retrieve the note and set it as the BindingContext of the page.
                Note note = await App.Database.GetNoteAsync(id);
                BindingContext = note;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load note.");
            }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            note.Date = DateTime.Now;
            if (!string.IsNullOrWhiteSpace(note.Text))
            {
                await App.Database.SaveNoteAsync(note);
            }

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (Note)BindingContext;
            await App.Database.DeleteNoteAsync(note);

            // Navigate backwards
            await Shell.Current.GoToAsync("..");
        }

        // Before sqlite implementation
        //void LoadNote(string filename)
        //{
        //    try
        //    {
        //        // Retrieve the note and set it as the BindingContext of the page.
        //        Note note = new Note
        //        {
        //            Filename = filename,
        //            Text = File.ReadAllText(filename),
        //            Date = File.GetCreationTime(filename)
        //        };
        //        BindingContext = note;
        //    }
        //    catch (Exception)
        //    {
        //        Console.WriteLine("Failed to load note.");
        //    }
        //}

        //async void OnSaveButtonClicked(object sender, EventArgs e)
        //{
        //    var note = (Note)BindingContext;

        //    if (string.IsNullOrWhiteSpace(note.Filename))
        //    {
        //        // Save the file.
        //        var filename = Path.Combine(App.FolderPath, $"{Path.GetRandomFileName()}.notes.txt");
        //        File.WriteAllText(filename, note.Text);
        //    }
        //    else
        //    {
        //        // Update the file.
        //        File.WriteAllText(note.Filename, note.Text);
        //    }

        //    // Navigate backwards
        //    await Shell.Current.GoToAsync("..");
        //}

        //async void OnDeleteButtonClicked(object sender, EventArgs e)
        //{
        //    var note = (Note)BindingContext;

        //    // Delete the file.
        //    if (File.Exists(note.Filename))
        //    {
        //        File.Delete(note.Filename);
        //    }

        //    // Navigate backwards
        //    await Shell.Current.GoToAsync("..");
        //}
    }
}