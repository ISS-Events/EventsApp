namespace EventsApp
{
    using EventsApp.Logic.Adapters;
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Extensions;
    using EventsApp.Logic.Managers;
    using EventsApp.Logic.ViewEntities;

    public partial class MainPageOld : ContentPage
    {
        public MainPageOld()
        {
            this.InitializeComponent();
            this.LoadEvents();
        }

        private void OnLoadEventsClicked(object sender, EventArgs e)
        {
            this.LoadEvents();
        }

        private async void OnEventEditClicked(object sender, EventArgs e)
        {
            string guid = (sender as Button).CommandParameter.ToString();
            Guid eventGUID = Guid.Parse(guid);

            await this.Navigation.PushAsync(new EventInfoPage(eventGUID));
        }

        private void LoadEvents()
        {
            // Example data for demonstration
            List<EventCard> events = this.GetEventCards();
            this.eventsListView.ItemsSource = events;
        }

        public List<EventCard> GetEventCards()
        {
            List<EventCard> eventCards = new List<EventCard>();
            List<EventInfo> events = EventsManager.GetAllEvents();
            foreach (EventInfo evt in events)
            {
                eventCards.Add(new EventCard(evt));
            }

            return eventCards;
        }
    }
}
