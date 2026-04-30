using Microsoft.Maui.Storage;

namespace StackyNotes.Services
{
    public static class AppSettings
    {
        public const string DefaultSortNewest = "Newest First";
        public const string DefaultSortOldest = "Oldest First";
        public const string DefaultSortAZ = "A-Z";

        private const string DefaultStackNameKey = "pref_defaultStackName";
        private const string DefaultSortOptionKey = "pref_defaultSortOption";
        private const string ShowIdeasShortcutKey = "pref_showIdeasShortcut";
        private const string ShowHelperTipKey = "pref_showHelperTip";

        public static string DefaultStackName
        {
            get => Preferences.Default.Get(DefaultStackNameKey, "My Saved Stack");
            set => Preferences.Default.Set(DefaultStackNameKey, string.IsNullOrWhiteSpace(value) ? "My Saved Stack" : value.Trim());
        }

        public static string DefaultSortOption
        {
            get => Preferences.Default.Get(DefaultSortOptionKey, DefaultSortNewest);
            set => Preferences.Default.Set(DefaultSortOptionKey, SanitizeSortOption(value));
        }

        public static bool ShowIdeasShortcut
        {
            get => Preferences.Default.Get(ShowIdeasShortcutKey, true);
            set => Preferences.Default.Set(ShowIdeasShortcutKey, value);
        }

        public static bool ShowHelperTip
        {
            get => Preferences.Default.Get(ShowHelperTipKey, true);
            set => Preferences.Default.Set(ShowHelperTipKey, value);
        }

        public static void Reset()
        {
            DefaultStackName = "My Saved Stack";
            DefaultSortOption = DefaultSortNewest;
            ShowIdeasShortcut = true;
            ShowHelperTip = true;
        }

        public static string SanitizeSortOption(string? value)
        {
            return value switch
            {
                DefaultSortOldest => DefaultSortOldest,
                DefaultSortAZ => DefaultSortAZ,
                _ => DefaultSortNewest
            };
        }
    }
}
