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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesPage : ContentPage
    {
        public NotesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Retrieve all the notes from the database, and set them as the
            // data source for the CollectionView.
            collectionView.ItemsSource = await App.Database.GetNotesAsync();
        }

        async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                // Navigate to the NoteEntryPage, passing the ID as a query parameter.
                Note note = (Note)e.CurrentSelection.FirstOrDefault();
                await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.ID.ToString()}");
            }
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            // Navigate to the NoteEntryPage, without passing any data.
            await Shell.Current.GoToAsync(nameof(NoteEntryPage));
        }

        // Before sqlite implementation
        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    var notes = new List<Note>();

        //    // Create a Note object from each file.
        //    var files = Directory.EnumerateFiles(App.FolderPath, "*.notes.txt");
        //    foreach (var filename in files)
        //    {
        //        notes.Add(new Note
        //        {
        //            Filename = filename,
        //            Text = File.ReadAllText(filename),
        //            Date = File.GetCreationTime(filename)
        //        });
        //    }
       
        //    // Set the data source for the CollectionView to a
        //    // sorted collection of notes.
        //    collectionView.ItemsSource = notes
        //        .OrderBy(d => d.Date)
        //        .ToList();
        //}


        //async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.CurrentSelection != null)
        //    {
        //        // Navigate to the NoteEntryPage, passing the filename as a query parameter.
        //        Note note = (Note)e.CurrentSelection.FirstOrDefault();
        //        await Shell.Current.GoToAsync($"{nameof(NoteEntryPage)}?{nameof(NoteEntryPage.ItemId)}={note.Filename}");
        //    }
        //}
    }
    
}