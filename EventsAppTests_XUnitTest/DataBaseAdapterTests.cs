using EventsApp.Logic.Attributes;
using EventsApp.Logic.Adapters;
using EventsApp.Logic.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EventsAppTests_XUnitTest.DatabaseAdapters
{
    public class DataBaseAdapterTests
    {
        private readonly string connectionString = "your_database_connection_string_here";

        [Fact]
        public void DataBaseAdapter_Constructor_SetsConnectionString()
        {
            var dataBaseAdapter = new DataBaseAdapter<int>(connectionString);

            string actualConnectionString = dataBaseAdapter.ConnectionString();

            Assert.Equal(connectionString, actualConnectionString);
        }

        [Fact]
        public void DataBaseAdapter_Add_CorrectlyAddsItem()
        {
            var dataBaseAdapter = new DataBaseAdapter<EventInfo>(connectionString);
            var eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 100,
                Description = "Test Description",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                BannerURL = "Test Banner URL",
                LogoURL = "Test Logo URL",
                AgeLimit = 18,
                EntryFee = 0.0f
            };

            dataBaseAdapter.Add(eventInfo);

            var retrievedEventInfo = dataBaseAdapter.Get(new Identifier(new Dictionary<string, object> { { "GUID", eventInfo.GUID } }));
            Assert.NotNull(retrievedEventInfo);
            Assert.Equal(eventInfo, retrievedEventInfo);
        }

        [Fact]
        public void DataBaseAdapter_Clear_ClearsAllItems()
        {
            var dataBaseAdapter = new DataBaseAdapter<EventInfo>(connectionString);
            var eventInfo = new EventInfo
            {
                GUID = Guid.NewGuid(),
                OrganizerGUID = Guid.NewGuid(),
                EventName = "Test Event",
                Categories = "Test Category",
                Location = "Test Location",
                MaxParticipants = 100,
                Description = "Test Description",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(1),
                BannerURL = "Test Banner URL",
                LogoURL = "Test Logo URL",
                AgeLimit = 18,
                EntryFee = 0.0f
            };
            dataBaseAdapter.Add(eventInfo);

            dataBaseAdapter.Clear();

            var allEvents = dataBaseAdapter.GetAll();
            Assert.Empty(allEvents);
        }


    }
}