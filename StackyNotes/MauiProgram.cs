using StackyNotes.Data;
using StackyNotes.Services;
using StackyNotes.ViewModels;
using StackyNotes.Views;

namespace StackyNotes;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddSingleton<AppDatabase>();
        builder.Services.AddSingleton<StackService>();
        builder.Services.AddTransient<StacksViewModel>();
        builder.Services.AddTransient<StacksPage>();
        builder.Services.AddTransient<EditNotePage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsViewModel>();

        return builder.Build();
    }
}
