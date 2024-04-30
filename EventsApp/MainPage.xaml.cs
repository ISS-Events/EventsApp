namespace EventsApp
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using EventsApp.Logic.Adapters;
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Extensions;
    using EventsApp.Logic.Managers;
    using EventsApp.Model_View.Entities;
    using Microsoft.Maui.Controls;

    public partial class MainPage : ContentPage
    {
        public int Count = 0;
        public Label Label;
        public DateTime Date = DateTime.Now;
        public string FormattedDate;

        public ObservableCollection<Event> Events
        {
            get;
            set;
        }

        private ObservableCollection<Event> GetEvents(string sortOptions = null)
        {
            List<EventInfo> eventInfos = new List<EventInfo>();

            EventsManager.GetAllEvents();
            string sortOption = this.EventsSort.SelectedItem?.ToString() ?? string.Empty;

            switch (sortOption)
            {
                case "By Popularity":
                    eventInfos = EventsManager.SortEventsByPopularityAscending();
                    break;
                case "By Date":
                    eventInfos = EventsManager.SortEventsByDateAscending();
                    break;
                case "By Price":
                    eventInfos = EventsManager.SortEventsByPriceAscending();
                    break;
                default:
                    eventInfos = EventsManager.GetAllEvents();
                    break;
            }

            ObservableCollection<Event> events = new ObservableCollection<Event>();

            foreach (EventInfo eventInfo in eventInfos)
            {
                Event newEvent = new Event(eventInfo);
                events.Add(newEvent);
                newEvent.UpdateInterestedStatus();
                this.OnPropertyChanged(nameof(newEvent.InterestedStar));
                newEvent.UpdateInterestedStatus();
            }

            return events;
        }

        public MainPage()
        {
            this.InitializeComponent();
            this.FormattedDate = this.Date.ToString("dddd, d MMM, hh:mm tt");
            /*
            Events = new ObservableCollection<Event>
            {
                new Event("event_logo.jpg", "Cloudflight", "Hackathon", "Cluj, Centru", FormattedDate, "5 EUR", 50, false, "star_empty.png"),
                new Event("ubb_logo.png", "UBB Cluj", "Job Fair", "Cluj, Dorobantilor", FormattedDate, "Free", 110, false, "star_empty.png")
            };*/

            this.RefreshEvents();
        }

        private void RefreshInterestedStars()
        {
            foreach (Event ev in this.Events)
            {
                ev.UpdateInterestedStatus();
                this.OnPropertyChanged(nameof(ev.InterestedStar));
                ev.UpdateInterestedStatus();
            }

            this.BindingContext = this;
        }

        private void RefreshAllEvents()
        {
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.RefreshEvents();
            // RefreshInterestedStars();
        }

        public void OnSortChanged(object sender, EventArgs e)
        {
            this.RefreshEvents();
        }

        public void RefreshEvents()
        {
            if (this.Events != null)
            {
                this.Events.Clear();
            }

            this.Events = this.GetEvents();
            this.OnPropertyChanged(nameof(this.Events));
            this.BindingContext = this;
        }

        #region Event Handlers
        public async void OnEventClicked(object sender, EventArgs e)
        {
            string guid = string.Empty;
            Grid imageSender = (Grid)sender;
            if (imageSender.GestureRecognizers.Count > 0)
            {
                var gesture = (TapGestureRecognizer)imageSender.GestureRecognizers[0];
                guid = (string)gesture.CommandParameter;
            }

            await this.Navigation.PushAsync(new EventPageUser(guid));
        }

        public void OnImageButtonClicked(object sender, EventArgs e)
        {
            Shell.Current.Navigation.PushAsync(new AddOrEditPage(AppStateManager.CurrentUserGUID, Guid.Empty, false));
        }

        private void OnSearchBarTextChanged(object sender, EventArgs e)
        {
            string searchText = this.EventsSearchBar.Text;
            List<string> searchResults = new List<string>
            {
                "Result 1 for " + searchText,
                "Result 2 for " + searchText,
                "Result 3 for " + searchText,
            };
            this.SearchResultsListView.ItemsSource = searchResults;
            this.SearchResultsListView.IsVisible = !string.IsNullOrEmpty(searchText);
        }

        private void OnFilterClicked(object sender, EventArgs e)
        {
            this.FilterOptions.IsVisible = !this.FilterOptions.IsVisible;
        }

        private void OnStarTapped(object sender, EventArgs e)
        {
            Image tappedStar = (Image)sender;
            Event tappedEvent = (Event)tappedStar.BindingContext;

            if (!UsersManager.IsInterested(AppStateManager.CurrentUserGUID, Guid.Parse(tappedEvent.GUID)))
            {
                UsersManager.SetInterestedStatus(AppStateManager.CurrentUserGUID, Guid.Parse(tappedEvent.GUID));
            }
            else
            {
                UsersManager.RemoveInterestedStatus(AppStateManager.CurrentUserGUID, Guid.Parse(tappedEvent.GUID));
            }

            tappedEvent.UpdateInterestedStatus();
        }
        #endregion
    }
}
