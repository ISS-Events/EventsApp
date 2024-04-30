namespace EventsApp.Model_View.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Managers;

    public class Event : INotifyPropertyChanged
    {
        private bool interested;

        public event PropertyChangedEventHandler PropertyChanged;

        public string? GUID { get; set; }

        public string? Picture { get; set; }

        public string? Organizer { get; set; }

        public string? Name { get; set; }

        public string Description { get; set; }

        public string? Location { get; set; }

        public string StartDate { get; set; }

        public string? Price { get; set; }

        public int NoOfParticipants { get; set; }

        public Event(string gUID, string picture, string organizer, string name, string description, string location, string date, string price, int participants)
        {
            this.GUID = gUID;
            this.Picture = picture;
            this.Organizer = organizer;
            this.Name = name;
            this.Description = description;
            this.Location = location;
            this.StartDate = date;
            this.Price = price;
            this.NoOfParticipants = participants;
        }

        public Event(EventInfo eventInfo)
        {
            this.GUID = eventInfo.GUID.ToString();
            this.Picture = eventInfo.BannerURL;
            this.Organizer = UsersManager.GetUser(eventInfo.OrganizerGUID).Name;
            this.Name = eventInfo.EventName;
            this.Description = eventInfo.Description;
            this.Location = eventInfo.Location;
            this.StartDate = eventInfo.StartDate.Date.ToString();
            this.Price = eventInfo.EntryFee.ToString();
            this.NoOfParticipants = eventInfo.MaxParticipants;
            this.UpdateInterestedStatus();
        }

        public string HostInfoString
        {
            get
            {
                return "Hosted by " + this.Organizer;
            }
        }

        public string PriceInfoString
        {
            get
            {
                return this.Price + " EUR";
            }
        }

        public string ParticipantsInfoString
        {
            get
            {
                return $"{EventsManager.GetNumberOfParticipants(Guid.Parse(this.GUID))} / {this.NoOfParticipants.ToString()}";
            }
        }

        public string DateInfoString
        {
            get
            {
                return this.StartDate;
            }
        }

        public string InterestedStar
        {
            get
            {
                if (this.interested)
                {
                    return "star_filled.png";
                }
                else
                {
                    return "star_empty.png";
                }
            }
        }
        public string GetStar()
        {
            if (this.interested)
            {
                return "star_filled.png";
            }
            else
            {
                return "star_empty.png";
            }
        }

        public void UpdateInterestedStatus()
        {
            if (UsersManager.IsInterested(AppStateManager.CurrentUserGUID, Guid.Parse(this.GUID)) ||
                UsersManager.IsGoing(AppStateManager.CurrentUserGUID, Guid.Parse(this.GUID)))
            {
                this.interested = true;
            }
            else
            {
                this.interested = false;
            }

            this.OnPropertyChanged(nameof(this.InterestedStar));
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
