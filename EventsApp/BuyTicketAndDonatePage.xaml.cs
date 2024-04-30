using EventsApp.Logic.Managers;

namespace EventsApp;

public partial class BuyTicketAndDonatePage : ContentPage
{
    private string eventGuid;
    private Action onPaymentReceived;
    private PaymentMethod paymentMethod;

    public enum PaymentMethod
    {
        Ticket,
        Donation
    }

    public BuyTicketAndDonatePage(Guid eventGUID, Action onPaymentReceived, PaymentMethod paymentMethod)
    {
        this.InitializeComponent();
        this.eventGuid = eventGUID.ToString();
        this.onPaymentReceived = onPaymentReceived;
        this.paymentMethod = paymentMethod;
    }

    private void BackImageButton_Clicked(object sender, EventArgs e)
    {
    }

    private void PayForTicket()
    {
        string cardHolderName = "Name";
        string cardNumber = "1234567890123456";
        string cvv = "123";
        DateTime expirationDate = new DateTime(2023, 12, 31);

        EventsManager.BuyTicket(AppStateManager.CurrentUserGUID, Guid.Parse(this.eventGuid), cardHolderName, cardNumber, cvv, expirationDate);
    }

    private void PayForDonation()
    {
        DonationsManager.AddDonation(AppStateManager.CurrentUserGUID, Guid.Parse(this.eventGuid), 10);
    }

    private void PayButton_Clicked(object sender, EventArgs e)
    {
        if (this.paymentMethod == PaymentMethod.Ticket)
        {
            this.PayForTicket();
        }
        else if (this.paymentMethod == PaymentMethod.Donation)
        {
            this.PayForDonation();
        }

        this.onPaymentReceived.Invoke();
        this.Navigation.PopAsync();
    }
}