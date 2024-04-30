namespace EventsApp.Logic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EventsApp.Logic.Adapters;
    using EventsApp.Logic.Attributes;
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Extensions;

    public class ReportsManager
    {
        private static DataAdapter<ReportInfo> adapter;

        public static void Initialize(DataBaseAdapter<ReportInfo> adapter)
        {
            ReportsManager.adapter = adapter;
        }

        public static List<ReportInfo> GetAllReports()
        {
            return adapter.GetAll();
        }

        public static ReportInfo GetReport(Guid userId, Guid eventId)
        {
            ReportInfo reportInfo = new ReportInfo(userId, eventId);
            return adapter.Get(reportInfo.GetIdentifier());
        }

        public static List<ReportInfo> GetReportsFromUser(Guid userId)
        {
            List<ReportInfo> reportInfos = new List<ReportInfo>();
            foreach (ReportInfo report in GetAllReports())
            {
                if (report.UserGUID == userId)
                {
                    reportInfos.Add(report);
                }
            }

            return reportInfos;
        }

        public static List<ReportInfo> GetReportsForEvent(Guid eventId)
        {
            List<ReportInfo> reportInfos = new List<ReportInfo>();
            foreach (ReportInfo report in GetAllReports())
            {
                if (report.EventGUID == eventId)
                {
                    reportInfos.Add(report);
                }
            }

            return reportInfos;
        }

        public static void AddReport(Guid userId, Guid eventId, ReportInfo.ReportType reportType)
        {
            ReportInfo reportInfo = new ReportInfo(userId, eventId, reportType);
            adapter.Add(reportInfo);
        }

        public static void RemoveReport(Guid userId, Guid eventId)
        {
            ReportInfo reportInfo = new ReportInfo(userId, eventId);
            adapter.Delete(reportInfo.GetIdentifier());
        }

        public static void RemoveAllReportsForEvent(Guid eventId)
        {
            foreach (ReportInfo report in GetReportsForEvent(eventId))
            {
                adapter.Delete(report.GetIdentifier());
            }
        }

        public static void RemoveAllReportsFromUser(Guid userId)
        {
            foreach (ReportInfo report in GetReportsFromUser(userId))
            {
                adapter.Delete(report.GetIdentifier());
            }
        }

        public static void RemoveAllReports()
        {
            adapter.Clear();
        }

        public static void RemoveAllReportsForEventAndUser(Guid userId, Guid eventId)
        {
            foreach (ReportInfo report in GetAllReports())
            {
                if (report.UserGUID == userId && report.EventGUID == eventId)
                {
                    adapter.Delete(report.GetIdentifier());
                }
            }
        }
    }
}
