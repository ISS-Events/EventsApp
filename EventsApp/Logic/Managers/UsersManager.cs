namespace EventsApp.Logic.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.NetworkInformation;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using EventsApp.Logic.Adapters;
    using EventsApp.Logic.Attributes;
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Extensions;

    public static class UsersManager
    {
        private static DataAdapter<UserInfo> usersAdapter;
        private static DataAdapter<AdminInfo> adminsAdapter;
        private static DataAdapter<UserEventRelationInfo> userEventRelationsAdapter;

        public static void Initialize(DataAdapter<UserInfo> usersAdapter, DataAdapter<AdminInfo> adminsAdapter, DataAdapter<UserEventRelationInfo> userEventRelationsAdapter)
        {
            UsersManager.usersAdapter = usersAdapter;
            UsersManager.adminsAdapter = adminsAdapter;
            UsersManager.userEventRelationsAdapter = userEventRelationsAdapter;
        }

        public static UserInfo GetUser(Guid userId)
        {
            UserInfo userInfo = new UserInfo(userId);
            return usersAdapter.Get(userInfo.GetIdentifier());
        }

        public static List<UserInfo> GetAllUsers()
        {
            return usersAdapter.GetAll();
        }

        public static bool IsAdmin(Guid userId)
        {
            // TODO: UsersManager: Implement this method
            return false;
        }

        public static float HasPermission(Guid userId)
        {
            // TODO: UsersManager: Implement this method
            return 0;
        }

        public static List<UserInfo> SearchUsersByUsername(string usernameString)
        {
            List<UserInfo> foundUsers = new List<UserInfo>();
            foreach (UserInfo user in GetAllUsers())
            {
                if (user.Name.StartsWith(usernameString))
                {
                    foundUsers.Add(user);
                }
            }

            return foundUsers;
        }

        public static void SendNotificationToUser(Guid userInvitedId, string message)
        {
            // not implemented by us
        }

        public static void AddNewUser(UserInfo user)
        {
            usersAdapter.Add(user);
        }

        public static void AddNewUser(string name, string password)
        {
            UserInfo userInfo = new UserInfo(name, password);
            usersAdapter.Add(userInfo);
        }

        public static void InviteUser(Guid userId, Guid eventId, Guid userInvitedId)
        {
            SendNotificationToUser(userInvitedId, $"You have been invited to the event {EventsManager.GetEvent(eventId).EventName}! by {GetUser(userId).Name}");
        }

        public static void SetInterestedStatus(Guid userId, Guid eventId)
        {
            UserEventRelationInfo userEventRelationInfo = new UserEventRelationInfo(userId, eventId, "interested");

            if (!userEventRelationsAdapter.Contains(userEventRelationInfo.GetIdentifier()))
            {
                userEventRelationsAdapter.Add(userEventRelationInfo);
                return;
            }

            userEventRelationsAdapter.Update(userEventRelationInfo.GetIdentifier(), userEventRelationInfo);
        }

        public static void SetGoingStatus(Guid userId, Guid eventId)
        {
            UserEventRelationInfo userEventRelationInfo = new UserEventRelationInfo(userId, eventId, "going");

            if (!userEventRelationsAdapter.Contains(userEventRelationInfo.GetIdentifier()))
            {
                userEventRelationsAdapter.Add(userEventRelationInfo);
                return;
            }

            userEventRelationsAdapter.Update(userEventRelationInfo.GetIdentifier(), userEventRelationInfo);
        }

        public static void RemoveInterestedStatus(Guid userId, Guid eventId)
        {
            bool isGoing = IsGoing(userId, eventId);
            if (isGoing)
            {
                return;
            }

            UserEventRelationInfo userEventRelationInfo = new UserEventRelationInfo(userId, eventId);
            userEventRelationsAdapter.Delete(userEventRelationInfo.GetIdentifier());
        }

        public static bool IsInterested(Guid userId, Guid eventId)
        {
            UserEventRelationInfo userEventRelationInfo = new UserEventRelationInfo(userId, eventId);

            if (!userEventRelationsAdapter.Contains(userEventRelationInfo.GetIdentifier()))
            {
                return false;
            }

            string status = userEventRelationsAdapter.Get(userEventRelationInfo.GetIdentifier()).Status;

            return status == "interested" || status == "going";
        }

        public static bool IsGoing(Guid userId, Guid eventId)
        {
            UserEventRelationInfo userEventRelationInfo = new UserEventRelationInfo(userId, eventId);

            if (!userEventRelationsAdapter.Contains(userEventRelationInfo.GetIdentifier()))
            {
                return false;
            }

            return userEventRelationsAdapter.Get(userEventRelationInfo.GetIdentifier()).Status == "going";
        }
    }
}
