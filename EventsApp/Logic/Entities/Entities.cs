namespace EventsApp.Logic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EventsApp.Logic.Attributes;

    [Table("Users")]
    [System.Serializable]
    public struct UserInfo
    {
        public const float MINSCORE = 4.0f;

        [PrimaryKey]
        public Guid GUID;
        public string Name;
        public string Password;

        public UserInfo(Guid guid, string name, string password)
        {
            this.GUID = guid;
            this.Name = name;
            this.Password = password;
        }

        public UserInfo(string name, string password)
        {
            this.GUID = Guid.NewGuid();
            this.Name = name;
            this.Password = password;
        }

        public UserInfo(Guid guid)
        {
            this.GUID = guid;
            this.Name = string.Empty;
            this.Password = string.Empty;
        }

        public UserInfo()
        {
            this.GUID = Guid.NewGuid();
            this.Name = string.Empty;
            this.Password = string.Empty;
        }
    }

    [Table("Events")]
    [System.Serializable]
    public struct EventInfo
    {
        [PrimaryKey]
        public Guid GUID;
        public Guid OrganizerGUID;
        public string EventName;
        public string Categories; // "music, sports, etc."
        public string Location;
        public int MaxParticipants;
        public string Description;
        public DateTime StartDate;
        public DateTime EndDate;
        public string BannerURL;
        public string LogoURL;
        public int AgeLimit;
        public float EntryFee;

        public EventInfo(Guid guid, Guid userGUID, string eventName, string categories, string location, int maxParticipants, string description, DateTime startDate, DateTime endDate, string bannerURL, string logoURL, int ageLimit, float entryFee)
        {
            this.GUID = guid;
            this.OrganizerGUID = userGUID;
            this.EventName = eventName;
            this.Categories = categories;
            this.Location = location;
            this.MaxParticipants = maxParticipants;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BannerURL = bannerURL;
            this.LogoURL = logoURL;
            this.AgeLimit = ageLimit;
            this.EntryFee = entryFee;
        }

        public EventInfo(Guid userGUID, string eventName, string categories, string location, int maxParticipants, string description, DateTime startDate, DateTime endDate, string bannerURL, string logoURL, int ageLimit, float entryFee)
        {
            this.GUID = Guid.NewGuid();
            this.OrganizerGUID = userGUID;
            this.EventName = eventName;
            this.Categories = categories;
            this.Location = location;
            this.MaxParticipants = maxParticipants;
            this.Description = description;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BannerURL = bannerURL;
            this.LogoURL = logoURL;
            this.AgeLimit = ageLimit;
            this.EntryFee = entryFee;
        }

        public EventInfo(Guid guid)
        {
            this.GUID = guid;
            this.OrganizerGUID = Guid.Empty;
            this.EventName = string.Empty;
            this.Categories = string.Empty;
            this.Location = string.Empty;
            this.MaxParticipants = 0;
            this.Description = string.Empty;
            this.StartDate = DateTime.Now;
            this.EndDate = DateTime.Now;
            this.BannerURL = string.Empty;
            this.LogoURL = string.Empty;
            this.AgeLimit = 0;
            this.EntryFee = 0;
        }
    }

    [Table("UsersEventsStatus")]
    [System.Serializable]
    public struct UserEventRelationInfo
    {
        [PrimaryKey]
        public Guid UserGUID;
        [PrimaryKey]
        public Guid EventGUID;
        public string Status;

        public UserEventRelationInfo(Guid userGUID, Guid eventGUID, string status)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.Status = status;
        }

        public UserEventRelationInfo(Guid userGUID, Guid eventGUID)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.Status = string.Empty;
        }
    }

    [Table("Reports")]
    [System.Serializable]
    public struct ReportInfo
    {
        [PrimaryKey]
        public Guid UserGUID;
        [PrimaryKey]
        public Guid EventGUID;

        public ReportType ReportTypeValue;

        public ReportInfo(Guid userGUID, Guid eventGUID, ReportType reportType)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.ReportTypeValue = reportType;
        }

        public ReportInfo(Guid userGUID, Guid eventGUID)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.ReportTypeValue = ReportType.Harassment;
        }

        public enum ReportType
        {
            /// <summary>
            /// Harassment
            /// </summary>
            Harassment,

            /// <summary>
            /// Nudity
            /// </summary>
            Nudity,

            /// <summary>
            /// Spam
            /// </summary>
            Spam,

            /// <summary>
            /// Violence
            /// </summary>
            Violence,

            /// <summary>
            /// None
            /// </summary>
            None,

            /// <summary>
            /// Fraud
            /// </summary>
            Fraud,

            /// <summary>
            /// Offensive
            /// </summary>
            Offensive,

            /// <summary>
            /// GuidelinesViolations
            /// </summary>
            GuidelinesViolations,
        }
    }

    [Table("Reviews")]
    [System.Serializable]
    public struct ReviewInfo
    {
        [PrimaryKey]
        public Guid UserGUID;
        [PrimaryKey]
        public Guid EventGUID;
        public float Score;
        public string ReviewDescription;

        public ReviewInfo(Guid userGUID, Guid eventGUID, float score, string reviewDescription)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.Score = score;
            this.ReviewDescription = reviewDescription;
        }

        public ReviewInfo(Guid userGUID, Guid eventGUID)
        {
            this.UserGUID = userGUID;
            this.EventGUID = eventGUID;
            this.Score = 0;
            this.ReviewDescription = string.Empty;
        }
    }

    [Table("Expenses")]
    [System.Serializable]
    public struct ExpenseInfo(Guid guid, Guid eventGUID, string expenseName, float cost)
    {
        [PrimaryKey]
        public Guid GUID = guid;
        public Guid EventGUID = eventGUID;
        public string ExpenseName = expenseName;
        public float Cost = cost;
    }

    [Table("Donations")]
    [System.Serializable]
    public struct DonationInfo
    {
        [PrimaryKey]
        public Guid GUID;
        public Guid EventGUID;
        public Guid UserGUID;
        public float Amount;

        public DonationInfo(Guid guid, Guid eventGUID, Guid userGUID, float amount)
        {
            this.GUID = guid;
            this.EventGUID = eventGUID;
            this.UserGUID = userGUID;
            this.Amount = amount;
        }

        public DonationInfo(Guid eventGUID, Guid userGUID, float amount)
        {
            this.EventGUID = eventGUID;
            this.UserGUID = userGUID;
            this.Amount = amount;
            this.GUID = Guid.NewGuid();
        }

        public DonationInfo(Guid guid)
        {
            this.GUID = guid;
            this.EventGUID = Guid.Empty;
            this.UserGUID = Guid.Empty;
            this.Amount = 0;
        }
    }

    [Table("Admins")]
    [System.Serializable]
    public struct AdminInfo(Guid guid)
    {
        [PrimaryKey]
        public Guid GUID = guid;
    }
}
