namespace Notes.Views;

public partial class AboutPage : ContentPage
{
    private bool _isFlashlightOn = false;

    public AboutPage()
        => InitializeComponent();

    private async void LearnMore_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.About about)
            await NavigateURL(about.MoreInfoUrl);
    }

    private async void MeetAuthor_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Models.About about)
            await NavigateURL(about.AuthorWebPage);
    }

    // Navigate to the specified URL in the system browser.
    private async Task NavigateURL(string url)
        => await Launcher.Default.OpenAsync(url);

    private async void Taggle_Turn_On_Off_Flashlight_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (!_isFlashlightOn)
            {
                await Flashlight.Default.TurnOnAsync();
                _isFlashlightOn = true;
            }
            else
            {
                await Flashlight.Default.TurnOffAsync();
                _isFlashlightOn = false;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"{ex.Message}", "Ok");
        }
    }
}
