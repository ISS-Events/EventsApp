using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    using EventsApp.Logic.Entities;
    public class EntitiesTest_EventInfo
    {
        [Fact]
        public void EventInfo_ConstructorAllInfo_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_GUID = Guid.NewGuid();
            Guid GenerateAndExpected_OrganizerGUID = Guid.NewGuid();
            string GenerateAndExpected_EventName = "EventName";
            string GenerateAndExpected_Categories = "music"; // "music, sports, etc."
            string GenerateAndExpected_Location = "Location";
            int GenerateAndExpected_MaxParticipants = 100;
            string GenerateAndExpected_Description = "Description";
            DateTime GenerateAndExpected_StartDate = DateTime.Now;
            DateTime GenerateAndExpected_EndDate = DateTime.Now;
            string GenerateAndExpected_BannerURL = "BannerURL";
            string GenerateAndExpected_LogoURL = "LogoURL";
            int GenerateAndExpected_AgeLimit = 18;
            float GenerateAndExpected_EntryFee = 0.0f;

            EventInfo eventInfo = new EventInfo(
                               GenerateAndExpected_GUID,
                               GenerateAndExpected_OrganizerGUID,
                               GenerateAndExpected_EventName,
                               GenerateAndExpected_Categories,
                               GenerateAndExpected_Location,
                               GenerateAndExpected_MaxParticipants,
                               GenerateAndExpected_Description,
                               GenerateAndExpected_StartDate,
                               GenerateAndExpected_EndDate,
                               GenerateAndExpected_BannerURL,
                               GenerateAndExpected_LogoURL,
                               GenerateAndExpected_AgeLimit,
                               GenerateAndExpected_EntryFee);

            Assert.Equal(GenerateAndExpected_GUID, eventInfo.GUID);
            Assert.Equal(GenerateAndExpected_OrganizerGUID, eventInfo.OrganizerGUID);
            Assert.Equal(GenerateAndExpected_EventName, eventInfo.EventName);
            Assert.Equal(GenerateAndExpected_Categories, eventInfo.Categories);
            Assert.Equal(GenerateAndExpected_Location, eventInfo.Location);
            Assert.Equal(GenerateAndExpected_MaxParticipants, eventInfo.MaxParticipants);
            Assert.Equal(GenerateAndExpected_Description, eventInfo.Description);
            Assert.Equal(GenerateAndExpected_StartDate, eventInfo.StartDate);
            Assert.Equal(GenerateAndExpected_EndDate, eventInfo.EndDate);
            Assert.Equal(GenerateAndExpected_BannerURL, eventInfo.BannerURL);
            Assert.Equal(GenerateAndExpected_LogoURL, eventInfo.LogoURL);
            Assert.Equal(GenerateAndExpected_AgeLimit, eventInfo.AgeLimit);
            Assert.Equal(GenerateAndExpected_EntryFee, eventInfo.EntryFee);
        }

        [Fact]
        public void EventInfo_ConstructorNoGuid_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_OrganizerGUID = Guid.NewGuid();
            string GenerateAndExpected_EventName = "EventName";
            string GenerateAndExpected_Categories = "music"; // "music, sports, etc."
            string GenerateAndExpected_Location = "Location";
            int GenerateAndExpected_MaxParticipants = 100;
            string GenerateAndExpected_Description = "Description";
            DateTime GenerateAndExpected_StartDate = DateTime.Now;
            DateTime GenerateAndExpected_EndDate = DateTime.Now;
            string GenerateAndExpected_BannerURL = "BannerURL";
            string GenerateAndExpected_LogoURL = "LogoURL";
            int GenerateAndExpected_AgeLimit = 18;
            float GenerateAndExpected_EntryFee = 0.0f;

            EventInfo eventInfo = new EventInfo(
                               GenerateAndExpected_OrganizerGUID,
                               GenerateAndExpected_EventName,
                               GenerateAndExpected_Categories,
                               GenerateAndExpected_Location,
                               GenerateAndExpected_MaxParticipants,
                               GenerateAndExpected_Description,
                               GenerateAndExpected_StartDate,
                               GenerateAndExpected_EndDate,
                               GenerateAndExpected_BannerURL,
                               GenerateAndExpected_LogoURL,
                               GenerateAndExpected_AgeLimit,
                               GenerateAndExpected_EntryFee);

            Assert.Equal(GenerateAndExpected_OrganizerGUID, eventInfo.OrganizerGUID);
            Assert.Equal(GenerateAndExpected_EventName, eventInfo.EventName);
            Assert.Equal(GenerateAndExpected_Categories, eventInfo.Categories);
            Assert.Equal(GenerateAndExpected_Location, eventInfo.Location);
            Assert.Equal(GenerateAndExpected_MaxParticipants, eventInfo.MaxParticipants);
            Assert.Equal(GenerateAndExpected_Description, eventInfo.Description);
            Assert.Equal(GenerateAndExpected_StartDate, eventInfo.StartDate);
            Assert.Equal(GenerateAndExpected_EndDate, eventInfo.EndDate);
            Assert.Equal(GenerateAndExpected_BannerURL, eventInfo.BannerURL);
            Assert.Equal(GenerateAndExpected_LogoURL, eventInfo.LogoURL);
            Assert.Equal(GenerateAndExpected_AgeLimit, eventInfo.AgeLimit);
            Assert.Equal(GenerateAndExpected_EntryFee, eventInfo.EntryFee);
        }

        [Fact]
        public void EventInfo_ConstructorOnlyGuid_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_GUID = Guid.NewGuid();
            Guid GenerateAndExpected_OrganizerGUID = Guid.Empty;
            string GenerateAndExpected_EventName = string.Empty;
            string GenerateAndExpected_Categories = string.Empty; // "music, sports, etc."
            string GenerateAndExpected_Location = string.Empty;
            int GenerateAndExpected_MaxParticipants = 0;
            string GenerateAndExpected_Description = string.Empty;
            string GenerateAndExpected_BannerURL = string.Empty;
            string GenerateAndExpected_LogoURL = string.Empty;
            int GenerateAndExpected_AgeLimit = 0;
            float GenerateAndExpected_EntryFee = 0;

            EventInfo eventInfo = new EventInfo(GenerateAndExpected_GUID);

            Assert.Equal(GenerateAndExpected_GUID, eventInfo.GUID);
            Assert.Equal(GenerateAndExpected_OrganizerGUID, eventInfo.OrganizerGUID);
            Assert.Equal(GenerateAndExpected_EventName, eventInfo.EventName);
            Assert.Equal(GenerateAndExpected_Categories, eventInfo.Categories);
            Assert.Equal(GenerateAndExpected_Location, eventInfo.Location);
            Assert.Equal(GenerateAndExpected_MaxParticipants, eventInfo.MaxParticipants);
            Assert.Equal(GenerateAndExpected_Description, eventInfo.Description);
            Assert.Equal(GenerateAndExpected_BannerURL, eventInfo.BannerURL);
            Assert.Equal(GenerateAndExpected_LogoURL, eventInfo.LogoURL);
            Assert.Equal(GenerateAndExpected_AgeLimit, eventInfo.AgeLimit);
            Assert.Equal(GenerateAndExpected_EntryFee, eventInfo.EntryFee);
        }
    }
}
