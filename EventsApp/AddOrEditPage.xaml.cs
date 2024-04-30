namespace EventsApp
{
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Managers;

    public partial class AddOrEditPage : ContentPage
    {
        private List<string> selectedCategories = new List<string>();
        private List<string> selectedSponsors = new List<string>();
        private List<string> selectedExpenses = new List<string>();

        private Guid userId;
        private Guid eventId;
        private bool edit;

        public AddOrEditPage(Guid userId, Guid eventId, bool edit)
        {
            this.InitializeComponent();
            this.userId = userId;
            this.eventId = eventId;
            this.edit = edit;

            if (edit)
            {
                this.LoadEvent();
            }
        }

        private void LoadEvent()
        {
            EventInfo eventInfo = EventsManager.GetEvent(this.eventId);
            this.LoadEventInfo(eventInfo);
        }

        private EventInfo GenerateEventInfo()
        {
            var eventInfo = default(EventInfo);
            eventInfo.GUID = this.edit ? this.eventId : Guid.NewGuid();
            eventInfo.EventName = this.TitleEntry.Text;
            eventInfo.Description = this.DescriptionEntry.Text;
            eventInfo.Location = this.LocationEntry.Text;
            eventInfo.Categories = string.Join(", ", this.selectedCategories);

            TimeSpan startTime = this.StartTimePicker.Time;
            TimeSpan endTime = this.EndTimePicker.Time;
            DateTime dateTime = this.StartDatePicker.Date;
            DateTime startDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, startTime.Hours, startTime.Minutes, 0);
            DateTime endDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, endTime.Hours, endTime.Minutes, 0);
            eventInfo.StartDate = startDateTime;
            eventInfo.EndDate = endDateTime;
            float.TryParse(this.PriceEntry.Text, out eventInfo.EntryFee);
            eventInfo.BannerURL = this.LogoURLEntry.Text;
            eventInfo.LogoURL = this.LogoURLEntry.Text;
            eventInfo.MaxParticipants = 50;
            eventInfo.AgeLimit = int.Parse(this.AgeLimitEntry.Text);
            eventInfo.Categories = string.Join(", ", this.selectedCategories);
            return eventInfo;
        }

        private void LoadEventInfo(EventInfo eventInfo)
        {
            this.TitleEntry.Text = eventInfo.EventName;
            this.DescriptionEntry.Text = eventInfo.Description;
            this.LocationEntry.Text = eventInfo.Location;
            this.LogoURLEntry.Text = eventInfo.LogoURL;
            this.PriceEntry.Text = eventInfo.EntryFee.ToString();
            this.AgeLimitEntry.Text = eventInfo.AgeLimit.ToString();
            string[] categories = new string[0];
            try
            {
                eventInfo.Categories.Split(", ");
                foreach (string category in categories)
                {
                    this.selectedCategories.Add(category);
                    Frame frame = new Frame
                    {
                        CornerRadius = 20,
                        BackgroundColor = Color.FromArgb("#D9DCDD"),
                        Padding = new Thickness(5),
                        Margin = new Thickness(5),
                        BorderColor = Color.FromArgb("#E0E0E0"),
                    };

                    Label selectedLabel = new Label
                    {
                        Text = category,
                        TextColor = Color.FromArgb("#000000"),
                    };

                    frame.Content = selectedLabel;
                    this.SelectedItemsStack.Children.Add(frame);
                }
            }
            catch (Exception e)
            {
            }

            this.StartTimePicker.Time = eventInfo.StartDate.TimeOfDay;
            this.EndTimePicker.Time = eventInfo.EndDate.TimeOfDay;
            this.StartDatePicker.Date = eventInfo.StartDate.Date;
        }

        private void OnPublishedButtonClicked(object sender, EventArgs e)
        {
            EventInfo eventInfo = this.GenerateEventInfo();
            EventsManager.AddNewEvent(eventInfo);
            this.Navigation.PopAsync();
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            EventsManager.DeleteEvent(this.eventId);
            this.Navigation.PopAsync();
            this.Navigation.PopAsync();
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            EventInfo eventInfo = this.GenerateEventInfo();
            EventsManager.UpdateEvent(this.eventId, eventInfo);
            this.Navigation.PopAsync();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await this.Navigation.PopAsync();
        }

        private void OnSelectCategoriesClicked(object sender, EventArgs e)
        {
            this.CategoriesListView.IsVisible = !this.CategoriesListView.IsVisible;
        }

        private void OnSponsorsClicked(object sender, EventArgs e)
        {
            string inputText = this.SponsorEntry.Text;

            Frame frame = new Frame
            {
                CornerRadius = 20,
                BackgroundColor = Color.FromArgb("#D9DCDD"),
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                BorderColor = Color.FromArgb("#E0E0E0"),
            };
            Label newLabel = new Label
            {
                Text = inputText,
                TextColor = Color.FromArgb("#000000"),
            };
            frame.Content = newLabel;
            this.SponsorsStack.Children.Add(frame);
            this.selectedSponsors.Add(inputText);
        }

        private void OnExpensesClicked(object sender, EventArgs e)
        {
            string inputText = this.ExpenseNameEntry.Text;

            Frame frame = new Frame
            {
                CornerRadius = 20,
                BackgroundColor = Color.FromArgb("#D9DCDD"),
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                BorderColor = Color.FromArgb("#E0E0E0"),
            };

            Label newLabel = new Label
            {
                Text = inputText,
                TextColor = Color.FromArgb("#000000"),
            };
            frame.Content = newLabel;
            this.ExpensesStack.Children.Add(frame);
            this.selectedExpenses.Add(inputText);
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }

            string selectedItem = e.SelectedItem as string;

            if (this.selectedCategories.Count >= 3 || this.SelectedItemsStack.Children.Any(child => ((Label)((Frame)child).Content).Text == selectedItem))
            {
                ((ListView)sender).SelectedItem = null;
                return;
            }

            Frame frame = new Frame
            {
                CornerRadius = 20,
                BackgroundColor = Color.FromArgb("#D9DCDD"),
                Padding = new Thickness(5),
                Margin = new Thickness(5),
                BorderColor = Color.FromArgb("#E0E0E0"),
            };

            Label selectedLabel = new Label
            {
                Text = selectedItem,
                TextColor = Color.FromArgb("#000000"),
            };

            frame.Content = selectedLabel;
            this.SelectedItemsStack.Children.Add(frame);
            this.selectedCategories.Add(selectedItem);
        }
    }
}