namespace EventsApp.Logic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Intrinsics.Arm;
    using System.Text;
    using System.Threading.Tasks;
    using Bogus;
    using EventsApp.Logic.Entities;

    public static class Dummy
    {
        public static Random Random = new Random();

        public static void Populate()
        {
            AddCurrentlyLoggedUser();
            PopulateUsers(10);
            PopulateEvents(10);
            PopulateStatuses();
            // List<UserInfo> users = UsersManager.GetAllUsers();
            // List<EventInfo> events = EventsManager.GetAllEvents();
        }

        public static void AddCurrentlyLoggedUser()
        {
            UserInfo currentUser = new UserInfo(
                AppStateManager.CurrentUserGUID,
                AppStateManager.Name,
                AppStateManager.Password);

            UsersManager.AddNewUser(currentUser);
        }

        public static void PopulateUsers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                UserInfo userInfo = GenerateRandomUser();
                UsersManager.AddNewUser(userInfo.Name, userInfo.Password);
            }
        }

        public static void PopulateEvents(int count)
        {
            for (int i = 0; i < count; i++)
            {
                EventInfo eventInfo = GenerateRandomEvent(GetRandomUserGUID());
                EventsManager.AddNewEvent(eventInfo);
            }
        }

        public static void PopulateStatuses()
        {
            List<UserInfo> users = UsersManager.GetAllUsers();
            List<EventInfo> events = EventsManager.GetAllEvents();
            foreach (UserInfo userInfo in users)
            {
                Guid eventGuid = events[Random.Next(events.Count)].GUID;
                UsersManager.SetInterestedStatus(userInfo.GUID, eventGuid);
                UsersManager.SetGoingStatus(userInfo.GUID, eventGuid);
            }

            foreach (EventInfo eventInfo in events)
            {
                Guid userGuid = AppStateManager.CurrentUserGUID;
                UsersManager.SetInterestedStatus(userGuid, eventInfo.GUID);
            }
        }

        public static EventInfo GenerateRandomEvent(Guid organizerGuid)
        {
            // eventName, categories, location, maxParticipants, description, startDate, endDate, bannerURL
            // logoUrl, ageLimit, entryFee
            Faker faker = new Faker();

            EventInfo eventInfo = new EventInfo(
                organizerGuid,
                faker.Company.CompanyName(),
                faker.Commerce.Categories(1)[0],
                faker.Address.City(),
                faker.Random.Int(1, 100),
                faker.Lorem.Paragraph(),
                faker.Date.Future(),
                faker.Date.Future(),
                faker.Image.PicsumUrl(),
                faker.Image.PicsumUrl(),
                faker.Random.Int(1, 100),
                faker.Random.Int(0, 100));

            return eventInfo;
        }

        public static UserInfo GenerateRandomUser()
        {
            Faker faker = new Faker();

            UserInfo userInfo = new UserInfo
            {
                Name = faker.Internet.UserName(),
                Password = faker.Internet.Password(),
            };

            return userInfo;
        }

        public static Guid GetRandomUserGUID()
        {
            return UsersManager.GetAllUsers()[Random.Next(UsersManager.GetAllUsers().Count)].GUID;
        }

        public static Guid GetRandomEventGUID()
        {
            return EventsManager.GetAllEvents()[Random.Next(EventsManager.GetAllEvents().Count)].GUID;
        }
    }
}
