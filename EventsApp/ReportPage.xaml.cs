namespace EventsApp;

using EventsApp.Logic.Entities;
using EventsApp.Logic.Managers;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public partial class ReportPage : ContentPage
{
    private Guid userGuid;
    private Guid eventGuid;

    public ReportPage(Guid userGuid, Guid eventGuid)
    {
        this.InitializeComponent();
        this.userGuid = userGuid;
        this.eventGuid = eventGuid;
        this.BindingContext = this;
    }

    private void CloseButton_Clicked(object sender, EventArgs e)
    {
        this.Navigation.PopAsync();
    }

    private ReportInfo.ReportType GetFirstReportType()
    {
        if (this.SpamCB.IsChecked)
        {
            return ReportInfo.ReportType.Spam;
        }

        if (this.FraudCB.IsChecked)
        {
            return ReportInfo.ReportType.Fraud;
        }

        if (this.HarrasmentCB.IsChecked)
        {
            return ReportInfo.ReportType.Harassment;
        }

        if (this.OffensiveCB.IsChecked)
        {
            return ReportInfo.ReportType.Offensive;
        }

        if (this.ViolationsCB.IsChecked)
        {
            return ReportInfo.ReportType.Violence;
        }

        if (this.NudityCB.IsChecked)
        {
            return ReportInfo.ReportType.Nudity;
        }

        return ReportInfo.ReportType.None;
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        ReportsManager.AddReport(this.userGuid, this.eventGuid, this.GetFirstReportType());
        this.Navigation.PopAsync();
    }
}