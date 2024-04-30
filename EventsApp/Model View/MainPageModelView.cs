using CommunityToolkit.Mvvm.ComponentModel;

public partial class MainPageModelView : ObservableObject
{
    [ObservableProperty]
#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
    public string text = string.Empty;
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter
}