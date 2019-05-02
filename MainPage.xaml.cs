using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPSoundBoard.Model;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace UWPSoundBoard
{
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Sound> Sounds;
        private List<String> Suggestions;
        private List<MenuItem> MenuItems;

        public MainPage()
        {
            this.InitializeComponent();
            Sounds = new ObservableCollection<Sound>();
            SoundManager.GetAllSounds(Sounds);  // Display all sounds when App launches

            // Add the Menu items (Icons) when App launches
            MenuItems = new List<MenuItem>();
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/animals.png", Category = SoundCategory.Animals });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/cartoon.png", Category = SoundCategory.Cartoons });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/dota2.png", Category = SoundCategory.Dota2 });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/meme.png", Category = SoundCategory.Memes });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/taunt.png", Category = SoundCategory.Taunts });
            MenuItems.Add(new MenuItem { IconFile = "Assets/Icons/warning.png", Category = SoundCategory.Warnings });

            BackButton.Visibility = Visibility.Collapsed; // hide the back button
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;   // open splitview when clicked
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.GetAllSounds(Sounds);      // display all sounds when back button is clicked
            CategoryTextBlock.Text = "All Sounds";
            MenuItemsListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;   // hide back button
        }

        private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (String.IsNullOrEmpty(sender.Text))
            {
                goBack();
            }

            SoundManager.GetAllSounds(Sounds);
            Suggestions = Sounds
                .Where(p => p.Name.StartsWith(sender.Text))     // populates the suggestion with the first letter you put
                .Select(p => p.Name)
                .ToList();
            SearchAutoSuggestBox.ItemsSource = Suggestions;
        }

        private void goBack()
        {
            SoundManager.GetAllSounds(Sounds);
            CategoryTextBlock.Text = "All Sounds";
            MenuItemsListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;
        }

        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            SoundManager.GetSoundsByName(Sounds, sender.Text);
            CategoryTextBlock.Text = sender.Text;
            MenuItemsListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Visible;
        }

        private void MenuItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;

            // Filter by category
            CategoryTextBlock.Text = menuItem.Category.ToString();         
            SoundManager.GetSoundsByCategory(Sounds, menuItem.Category);    // Filters sounds when clicked. 
            BackButton.Visibility = Visibility.Visible;
        }

        private void SoundGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sound = (Sound)e.ClickedItem;
            MyMediaElement.Source = new Uri(this.BaseUri, sound.AudioFile); // uniform resource indicator - location for the file.
        }

        private void SoundGridView_DragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;    // Treat it like a copy operation. 
                                                                // Copies the content to the target destination. 
                                                                // When implementing clipboard functionality, this corresponds to the "Copy" command.

            e.DragUIOverride.Caption = "Drop to play a song or sound"; // hint - when holding the file, found on the cursor
            e.DragUIOverride.IsCaptionVisible = true;
            e.DragUIOverride.IsContentVisible = true;   // Make the content visible - you can see what file is being dragged.
            e.DragUIOverride.IsGlyphVisible = true;     // Little icon - copy = 2 little file on top of each other.
        }

        private async void SoundGridView_Drop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))  // Makes sure what being dragged and dropped is a file.
                                                                        // StorageItems - not fragments of text or something from an app.
            {
                var items = await e.DataView.GetStorageItemsAsync();    // Get a reference to all those items. Only working with 1 item.

                if (items.Any())                                        // Make sure there are items in the drag operation
                {
                    var storageFile = items[0] as StorageFile;          // Get first item at index 0 
                    var contentType = storageFile.ContentType;          // Get the content type - image/sound/documet

                    StorageFolder folder = ApplicationData.Current.LocalFolder; // Reference to the local folder (storage). 

                    if (contentType == "audio/wav" || contentType == "audio/mpeg")
                    {
                        // Save in a folder, filename, if there's a same name, generate a unique name.
                        StorageFile newFile = await storageFile.CopyAsync(folder, storageFile.Name, NameCollisionOption.GenerateUniqueName);
                        
                        MyMediaElement.SetSource(await storageFile.OpenAsync(FileAccessMode.Read), contentType);
                        MyMediaElement.Play();  // Play the dragged song
                    }
                }
            }
        }

    } // end partial class
} // end namespace
