namespace EventsApp.Logic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using EventsApp.Logic.Adapters;
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Extensions;

    public static class DonationsManager
    {
        private static DataAdapter<DonationInfo> adapter;

        public static void Initialize(DataBaseAdapter<DonationInfo> adapter)
        {
            DonationsManager.adapter = adapter;
        }

        public static DonationInfo GetDonation(Guid donationId)
        {
            DonationInfo donationInfo = new DonationInfo(donationId);
            return adapter.Get(donationInfo.GetIdentifier());
        }

        public static List<DonationInfo> GetAllDonations()
        {
            return adapter.GetAll();
        }

        public static List<DonationInfo> GetAllDonationsForEvent(Guid eventId)
        {
            List<DonationInfo> donationsForEvent = new List<DonationInfo>();
            foreach (DonationInfo donation in GetAllDonations())
            {
                if (donation.EventGUID == eventId)
                {
                    donationsForEvent.Add(donation);
                }
            }

            return donationsForEvent;
        }

        public static void AddDonation(Guid userId, Guid eventId, float amount)
        {
            DonationInfo donationInfo = new DonationInfo(userId, eventId, amount);
            adapter.Add(donationInfo);
        }

        public static float GetTotalDonationsForEvent(Guid eventId)
        {
            float totalDonations = 0;
            foreach (DonationInfo donation in GetAllDonationsForEvent(eventId))
            {
                totalDonations += donation.Amount;
            }

            return totalDonations;
        }

        public static List<DonationInfo> GetDonationsFromUser(Guid userId)
        {
            List<DonationInfo> donationsFromUser = new List<DonationInfo>();
            foreach (DonationInfo donation in GetAllDonations())
            {
                if (donation.UserGUID == userId)
                {
                    donationsFromUser.Add(donation);
                }
            }

            return donationsFromUser;
        }

        public static void RemoveDonation(Guid donationId)
        {
            DonationInfo donationInfo = new DonationInfo(donationId);
            adapter.Delete(donationInfo.GetIdentifier());
        }

        public static void RemoveAllDonationsForEvent(Guid eventId)
        {
            foreach (DonationInfo donation in GetAllDonationsForEvent(eventId))
            {
                adapter.Delete(donation.GetIdentifier());
            }
        }
    }
}
