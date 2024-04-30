namespace EventsApp;

public partial class RatingPage : ContentPage
{
    public RatingPage()
    {
        this.InitializeComponent();
        Entry entry = new Entry { Placeholder = "Enter text" };
        entry.TextChanged += this.OnEntryTextChanged;
        entry.Completed += this.OnEntryCompleted;
    }

    private void CloseButton_Clicked(object sender, EventArgs e)
    {
        this.Navigation.PopModalAsync();
    }

    public void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string oldText = e.OldTextValue;
        string newText = e.NewTextValue;
        string myText = this.entry.Text;
    }

    public void OnEntryCompleted(object sender, EventArgs e)
    {
        string text = ((Entry)sender).Text;
    }

    private void OnStarClicked(object sender, EventArgs e)
    {
        ImageButton star = (ImageButton)sender;
        if (star.Source is FileImageSource fileImageSource && fileImageSource.File == "yellow_star.png")
        {
            star.Source = "white_star.png";
        }
        else
        {
            star.Source = "yellow_star.png";
        }
    }
}