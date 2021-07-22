using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using AnimeApp.Classes.AnimeApp;
using Windows.UI.Xaml.Documents;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AnimeApp.Pages.EntryView
{
    public sealed partial class EntryDialog : ContentDialog
    {
        private UserEntry entry;
        private List<EntryLink> links;

        public EntryDialog(UserEntry _entry)
        {
            entry = _entry;
            InitializeComponent();

            SetupEntry();
        }
        public void SetupEntry()
        {
            FormatDescription();
            SetupLists();

            SetupEpisodes();
            SetupRewatching();
            SetupStatus();
            SetupScore();
            SetupDates();
        }

        public void FormatDescription()
        {
            if (string.IsNullOrWhiteSpace(entry.Media.description))
            {
                DescriptionRichTextBox.Visibility = Visibility.Collapsed;
                NoDescriptionText.Visibility = Visibility.Visible;
                return;

            }
            
            Paragraph description = new Paragraph();
            Run run = new Run();
            run.Text = entry.Media.description.Replace("\n", "").Replace("<br>", "\n");

            Regex reg = new Regex("<([^<>\\\\]+?)>(.+?)<\\/\\1+?>");
            var strings = reg.Split(run.Text);

            for (int a = 0; a < strings.Length; a++)
            {
                Run srun = new Run();
                srun.Text = strings[a];

                if (srun.Text == "i")
                {
                    srun.Text = strings[++a];
                    Italic i = new Italic();
                    i.Inlines.Add(srun);
                    description.Inlines.Add(i);
                    continue;
                }

                if (srun.Text == "strong")
                {
                    srun.Text = strings[++a];
                    Bold strong = new Bold();
                    strong.Inlines.Add(srun);
                    description.Inlines.Add(strong);
                    continue;
                }

                description.Inlines.Add(srun);
            }

            DescriptionRichTextBox.Blocks.Add(description);
        }

        public void SetupLists()
        {
            links = new List<EntryLink>();
            foreach (KeyValuePair<string, string> link in entry.Media.otherLinks)
            {
                links.Add(new EntryLink() { linkName = link.Value, linkUri = new Uri(link.Key) });
            }
            if (entry.Media.genres.Count == 0)
            {
                GenresList.Visibility = Visibility.Collapsed;
                GenresNoItemsText.Visibility = Visibility.Visible;
            }

            if (entry.Media.studios.Count == 0)
            {
                StudiosList.Visibility = Visibility.Collapsed;
                StudiosNoItemsText.Visibility = Visibility.Visible;
            }

            if (entry.Media.otherLinks.Count == 0)
                LinksPanel.Visibility = Visibility.Collapsed;
        }

        public void SetupEpisodes()
        {
            if (entry.progress == -1)
                EpisodesNumberBox.Value = double.NaN;

            (int episodes, bool _) = entry.Media.GetEpisodes();
            if (episodes == -1) episodes = 0;

            EpisodesNumberBox.Maximum = episodes;
            EpisodesNumberBox.Value = entry.progress;
        }

        public void SetupRewatching()
        {
            if (entry.status == Enums.EntryStatus.Current && entry.repeat > 0)
                RewatchingCheckBox.IsChecked = true;
        }

        public void SetupStatus()
        {
            if (entry.status == Enums.EntryStatus.Unknown || entry.status == Enums.EntryStatus.Custom)
                StatusComboBox.SelectedIndex = 5;

            StatusComboBox.SelectedIndex = ((int)entry.status) - 1;
        }

        public void SetupScore()
        {
            ScoreNumberBox.Value = entry.score;
        }

        public void SetupDates()
        {
            if (entry.started == default)
                StartDatePicker.SelectedDate = null;
            else
                StartDatePicker.SelectedDate = entry.started;

            if (entry.ended == default)
                EndDatePicker.SelectedDate = null;
            else
                EndDatePicker.SelectedDate = entry.ended;
        }

        private void ClearStartDateButton_Click(object sender, RoutedEventArgs e)
        {
            StartDatePicker.SelectedDate = null;
        }

        private void ClearEndDateButton_Click(object sender, RoutedEventArgs e)
        {
            EndDatePicker.SelectedDate = null;
        }
    }

    public class EntryLink
    {
        public string linkName;
        public Uri linkUri;
    }
}
