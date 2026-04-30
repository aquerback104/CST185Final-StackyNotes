# StackyNotes

StackyNotes is a .NET MAUI semester project designed to help users create, organize, preview, search, and manage note cards in a clean multi-page app. The project was built as a final course application to demonstrate object-oriented programming, MVVM structure, local data storage, navigation, user settings, and final UI polish.

## Project Overview

StackyNotes acts like a simple stacked note-card manager. Users can create quick reminders, preview notes, save cards into a stack, search through saved cards, sort their notes, browse note ideas, and customize basic app behavior through a settings page.

The final version of the project focuses on making the app feel complete and presentation-ready. It includes clearer page headers, grouped sections, reusable visual styles, improved guidance text, and a final app summary page that explains the main features included in the finished build.

## Main Features

- Create quick notes from the home page
- Preview a note before moving into the saved-card workflow
- Save note cards with a title, content, stack name, order, and creation date
- Store saved cards locally using SQLite
- Display saved cards in a stack-style list
- Search saved cards by title, content, or stack name
- Sort saved cards by:
  - Newest First
  - Oldest First
  - A-Z
- Edit or delete saved cards
- Browse note ideas loaded from a local JSON file
- Customize app behavior through the settings page
- Save user preferences using MAUI Preferences
- Navigate between multiple app pages using Shell routing
- View a final app summary page explaining the completed project

## Technologies Used

- C#
- .NET 8
- .NET MAUI
- XAML
- SQLite
- `sqlite-net-pcl`
- JSON
- MVVM design pattern
- Git and GitHub

## Project Structure

```text
StackyNotes/
├── Data/
│   └── AppDatabase.cs
├── Models/
│   ├── NoteCard.cs
│   ├── NoteIdea.cs
│   └── NoteStack.cs
├── Services/
│   ├── AppSettings.cs
│   ├── NoteIdeasService.cs
│   └── StackService.cs
├── ViewModels/
│   ├── BaseViewModel.cs
│   ├── MainPageViewModel.cs
│   ├── SettingsViewModel.cs
│   ├── StackDetailViewModel.cs
│   └── StacksViewModel.cs
├── Views/
│   ├── AboutPage.xaml
│   ├── CardDetailPage.xaml
│   ├── EditNotePage.xaml
│   ├── MainPage.xaml
│   ├── NoteIdeasPage.xaml
│   ├── SettingsPage.xaml
│   ├── StackDetailPage.xaml
│   ├── StackPreviewPage.xaml
│   └── StacksPage.xaml
├── Resources/
│   ├── Raw/
│   │   └── noteideas.json
│   ├── Styles/
│   ├── Fonts/
│   ├── Images/
│   ├── AppIcon/
│   └── Splash/
├── Platforms/
├── AppShell.xaml
├── AppShell.xaml.cs
├── MauiProgram.cs
└── StackyNotes.csproj
