using StackyNotes.Views;

namespace StackyNotes;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("preview", typeof(StackPreviewPage));
        Routing.RegisterRoute("stacks", typeof(StacksPage));
        Routing.RegisterRoute("editnote", typeof(EditNotePage));
        Routing.RegisterRoute("noteideas", typeof(NoteIdeasPage));
        Routing.RegisterRoute("settings", typeof(SettingsPage));
        Routing.RegisterRoute("about", typeof(AboutPage));
    }
}
