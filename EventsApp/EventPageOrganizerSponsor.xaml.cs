namespace EventsApp;

public partial class EventPageOrganizerSponsor : ContentPage
{
    public EventPageOrganizerSponsor()
    {
        this.InitializeComponent();
        List<string> items = new List<string>
            {
                "Computers - 75$",
                "Internet - 10$",
            };

        this.expensesListView.ItemsSource = items;
        List<string> items1 = new List<string>
            {
                "Mike - 75$",
                "Mike - 10$",
            };

        this.donorsListView.ItemsSource = items1;
    }

    private void InviteButton_Clicked(object sender, EventArgs e)
    {
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
    }

    private void ReportsImageButton_Clicked(object sender, EventArgs e)
    {
    }

    private void EditImageButton_Clicked(object sender, EventArgs e)
    {
    }

    private void ReviewsImageButton_Clicked(object sender, EventArgs e)
    {
    }
}