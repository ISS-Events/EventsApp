using EventsApp.Logic.Entities;
using EventsApp.Logic.Managers;

namespace EventsApp;

public partial class ReportsPage : ContentPage
{
    public List<ViewReports> UsersReports;
    private Guid eventGuid;

    public ReportsPage(Guid eventGuid)
    {
        this.InitializeComponent();
        this.eventGuid = eventGuid;
        this.UsersReports = this.GetReports();
        this.reportsListView.ItemsSource = this.UsersReports;
        this.BindingContext = this;
    }

    private List<ViewReports> GetReports()
    {
        List<ReportInfo> reportInfos = ReportsManager.GetReportsForEvent(this.eventGuid);
        List<ViewReports> reports = new List<ViewReports>();

        foreach (ReportInfo reportInfo in reportInfos)
        {
            string userName = UsersManager.GetUser(reportInfo.UserGUID).Name;
            ViewReports newReport = new ViewReports(userName, reportInfo.ReportTypeValue.ToString());
            reports.Add(newReport);
        }

        return reports;
    }

    private void BackImageButton_Clicked(object sender, EventArgs e)
    {
        this.Navigation.PopAsync();
    }
}

public class ViewReports(string name, string report)
{
    public string UserName { get; set; } = name;

    public string Report { get; set; } = report;
}