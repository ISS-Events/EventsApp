using EventsApp.Logic.Managers;
using EventsApp.Model_View.Entities;

namespace EventsApp;

public partial class EventPageUser : ContentPage
{
    public string EventGuid;
    public bool IsOrganizerMode = false;

    public Event Event { get; set; }

    public EventPageUser(string guid)
    {
        this.InitializeComponent();

        this.EventGuid = guid;
        this.Event = new Event(EventsManager.GetEvent(Guid.Parse(guid)));
        this.BindingContext = this;

        this.IsOrganizerMode = EventsManager.IsOrganizer(AppStateManager.CurrentUserGUID, Guid.Parse(guid));

        this.UpdateProperties();
        this.UpdateInterestedStatus();
        this.UpdateGoingStatus();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        this.RefreshEvent();
        // RefreshInterestedStars();
    }

    private void RefreshEvent()
    {
        this.Event = new Event(EventsManager.GetEvent(Guid.Parse(this.EventGuid)));
        this.BindingContext = this;
        this.OnPropertyChanged(nameof(this.Event));
    }

    private void UpdateProperties()
    {
        this.buyTicketButton.Text = this.IsOrganizerMode ? "Edit Event" : "Buy Ticket";
    }

    private void UpdateInterestedStatus()
    {
        if (UsersManager.IsInterested(AppStateManager.CurrentUserGUID, Guid.Parse(this.EventGuid)))
        {
            this.interestedImageButton.Source = "star_filled.png";
        }
        else
        {
            this.interestedImageButton.Source = "star_empty.png";
        }
    }

    private void DonateButton_Clicked(object sender, EventArgs e)
    {
        // Open BuyTicketPage
        if (this.Event != null)
        {
            this.Navigation.PushAsync(new BuyTicketAndDonatePage(Guid.Parse(this.Event.GUID), () => this.OnDonationPaymentReceived(), BuyTicketAndDonatePage.PaymentMethod.Donation));
        }
    }

    private void OnDonationPaymentReceived()
    {
        this.UpdateGoingStatus();
        this.UpdateInterestedStatus();
    }

    private void OnTickedPaymentReceived()
    {
        this.UpdateGoingStatus();
        this.UpdateInterestedStatus();
    }

    private void UpdateGoingStatus()
    {
        if (UsersManager.IsGoing(AppStateManager.CurrentUserGUID, Guid.Parse(this.EventGuid)))
        {
            this.goingImageButton.BackgroundColor = Color.FromHex("#FFD700");
        }
        else
        {
            this.goingImageButton.BackgroundColor = Color.FromHex("#FFFFFF");
        }
    }

    private void InterestedImageButton_Clicked(object sender, EventArgs e)
    {
        if (UsersManager.IsInterested(AppStateManager.CurrentUserGUID, Guid.Parse(this.EventGuid)))
        {
            UsersManager.RemoveInterestedStatus(AppStateManager.CurrentUserGUID, Guid.Parse(this.EventGuid));
        }
        else
        {
            UsersManager.SetInterestedStatus(AppStateManager.CurrentUserGUID, Guid.Parse(this.EventGuid));
        }

        this.UpdateInterestedStatus();
    }

    private void BuyTicketButton_Clicked(object sender, EventArgs e)
    {
        if (this.Event == null)
        {
            return;
        }

        if (this.IsOrganizerMode)
        {
            this.Navigation.PushAsync(new AddOrEditPage(AppStateManager.CurrentUserGUID, Guid.Parse(this.Event.GUID), true));
        }
        else
        {
            this.Navigation.PushAsync(new BuyTicketAndDonatePage(Guid.Parse(this.Event.GUID), () => this.OnTickedPaymentReceived(), BuyTicketAndDonatePage.PaymentMethod.Ticket));
        }

        this.UpdateGoingStatus();
    }

    private void ShareImageButton_Clicked(object sender, EventArgs e)
    {
        this.Navigation.PushAsync(new UserSharePage(AppStateManager.CurrentUserGUID, Guid.Parse(this.EventGuid)));
    }

    private void ReportImageButton_Clicked(object sender, EventArgs e)
    {
        this.Navigation.PushAsync(new ReportPage(AppStateManager.CurrentUserGUID, Guid.Parse(this.EventGuid)));
    }

    private void BackImageButton_Clicked(object sender, EventArgs e)
    {
    }
}