namespace Notes.Models;

internal class About
{
    public string Title => AppInfo.Name;

    public string Version => AppInfo.VersionString;

    public string MoreInfoUrl => "https://aka.ms/maui";

    public string Message => "This app is written in XAML and C# with .NET MAUI.";

    public string AuthorWebPage => "https://www.linkedin.com/in/misael-da-costa-homem-8b07a158/";
}
