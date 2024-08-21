using Notes.Models;

namespace Notes.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class NotePage : ContentPage
{
    private const int MaxCharacters = 100;

    public NotePage()
	{
		InitializeComponent();

        string appDataPath = FileSystem.AppDataDirectory;
        string randomFileName = $"{Path.GetRandomFileName()}.notes.txt";

        LoadNote(Path.Combine(appDataPath, randomFileName));
    }

    public string ItemId
    {
        set
        {
            LoadNote(value);
        }
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(TextEditor.Text))
        {
            Vibration.Default.Vibrate();
            await DisplayAlert("Warning", "Type a text content to save a note!", "Ok");

            return;
        }

        if (BindingContext is Models.Note note)
            File.WriteAllText(note.Filename, TextEditor.Text);

        await Shell.Current.GoToAsync("..");
    }

    private async void DeleteButton_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.Note note)
        {
            // Delete the file.
            if (File.Exists(note.Filename))
                File.Delete(note.Filename);
        }

        await Shell.Current.GoToAsync("..");
    }

    private void LoadNote(string fileName)
    {
        var note = new Note();
        note.Filename = fileName;

        if(File.Exists(fileName))
        {
            note.Date = File.GetCreationTime(fileName);
            note.Text = File.ReadAllText(fileName);
        }

        BindingContext = note;
    }

    private void TextEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        int remainingCharacters = MaxCharacters - e.NewTextValue.Length;
        _characterCountLabel.Text = $"{remainingCharacters}/{MaxCharacters}";

        if (remainingCharacters < 0)
        {
            ((Editor)sender).Text = e.OldTextValue;
        }
    }
}