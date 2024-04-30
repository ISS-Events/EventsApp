using System.Collections.ObjectModel;
using EventsApp.Logic.Entities;
using EventsApp.Logic.Managers;

namespace EventsApp;

public partial class UserSharePage : ContentPage
{
    private Guid userGuid;
    private Guid eventGuid;

    public ObservableCollection<User> Users { get; set; }

    public UserSharePage(Guid userGUID, Guid eventGUID)
    {
        this.InitializeComponent();
        this.userGuid = userGUID;

        this.Users = this.GetUsers();
        this.BindingContext = this;
    }

    private ObservableCollection<User> GetUsers()
    {
        List<UserInfo> users = UsersManager.GetAllUsers();
        ObservableCollection<User> usersList = new ObservableCollection<User>();
        foreach (UserInfo user in users)
        {
            usersList.Add(new User(user.Name, "circle1.jpeg"));
        }

        return usersList;
    }

    private void CloseButton_Clicked(object sender, EventArgs e)
    {
        this.Navigation.PopAsync();
    }

    private void InviteButton_Clicked(object sender, EventArgs e)
    {
    }
}
