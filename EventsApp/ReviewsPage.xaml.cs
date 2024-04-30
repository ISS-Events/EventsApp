namespace EventsApp;

public partial class ReviewsPage : ContentPage
{
    private List<ReviewsMockData> mockReviews;

    public ReviewsPage()
    {
        this.InitializeComponent();
        this.mockReviews = new List<ReviewsMockData>
        {
            new ReviewsMockData("https://cdn-icons-png.flaticon.com/512/1144/1144760.png", "https://upload.wikimedia.org/wikipedia/commons/2/2f/Star_rating_3_of_5.png", "Nice"),
            new ReviewsMockData("https://cdn-icons-png.flaticon.com/512/1144/1144760.png", "https://upload.wikimedia.org/wikipedia/commons/1/17/Star_rating_5_of_5.png", "Felt good"),
        };
        this.reviewsListView.ItemsSource = this.mockReviews;
        this.BindingContext = this;
    }

    private void BackImageButton_Clicked(object sender, EventArgs e)
    {
    }
}

public class ReviewsMockData(string userImageURL, string starsImageURL, string reviewText)
{
    public string UserImageURL { get; set; } = userImageURL;

    public string StarsImageURL { get; set; } = starsImageURL;

    public string ReviewText { get; set; } = reviewText;
}