using Microsoft.Maui.Controls;

namespace MySemesterApp;

public class SimplePage : ContentPage
{
    private Label MyLabel;

    public SimplePage()
    {
        BuildContent();
    }

    private void BuildContent()
    {
        MyLabel = new Label
        {
            Text = "Welcome!",
            FontSize = 24,
            HorizontalOptions = LayoutOptions.Center
        };

        var entry = new Entry
        {
            Placeholder = "Type something here"
        };

        var button = new Button
        {
            Text = "Press me"
        };
        button.Clicked += OnButtonClicked;

        var image = new Image
        {
            Source = "dotnet_bot.png",
            WidthRequest = 120,
            HeightRequest = 120,
            HorizontalOptions = LayoutOptions.Center
        };

        Content = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            Children = { MyLabel, entry, button, image }
        };
    }

    private void OnButtonClicked(object? sender, EventArgs e)
    {
        MyLabel.Text = "Hello from C#!";
    }
}