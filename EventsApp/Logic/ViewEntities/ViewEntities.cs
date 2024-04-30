namespace EventsApp.Logic.ViewEntities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Managers;

    public class EventCard(EventInfo evt)
    {
        public Guid EventGUID { get; set; } = evt.GUID;

        public string OrganizerName { get; set; } = UsersManager.GetUser(evt.OrganizerGUID).Name;

        public string Title { get; set; } = evt.EventName;

        public string Categories { get; set; } = evt.Categories;

        public string Location { get; set; } = evt.Location;

        public int MaxParticipants { get; set; } = evt.MaxParticipants;

        public string Description { get; set; } = evt.Description;

        public string StartDate { get; set; } = evt.StartDate.ToString();

        public string EndDate { get; set; } = evt.EndDate.ToString();

        public string BannerURL { get; set; } = evt.BannerURL;

        public string LogoURL { get; set; } = evt.LogoURL;

        public int AgeLimit { get; set; } = evt.AgeLimit;

        public float EntryFee { get; set; } = evt.EntryFee;
    }
}
