using EventsApp.Logic.Managers;
using EventsApp.Logic.ViewEntities;

namespace EventsApp;

public partial class EventInfoPage : ContentPage
{
	public EventCard CurrentEvent { get; set; }

	public EventInfoPage(Guid eventGUID)
    {
        BindingContext = this;
        CurrentEvent = new EventCard(EventsManager.GetEvent(eventGUID));
        InitializeComponent();
	}
}