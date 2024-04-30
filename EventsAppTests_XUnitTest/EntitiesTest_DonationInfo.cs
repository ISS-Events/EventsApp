using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    using EventsApp.Logic.Entities;
    public class EntitiesTest_DonationInfo
    {
        [Fact]
        public void DonationInfo_ConstructorAllInfo_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_Guid = Guid.NewGuid();
            Guid GenerateAndExpected_EventGuid = Guid.NewGuid();
            Guid GenerateAndExpected_UserGuid = Guid.NewGuid();
            float GenerateAndExpected_Amount = 5;

            DonationInfo donationInfo = new DonationInfo(
                                 GenerateAndExpected_Guid,
                                 GenerateAndExpected_EventGuid,
                                 GenerateAndExpected_UserGuid,
                                 GenerateAndExpected_Amount);

            Assert.Equal(GenerateAndExpected_Guid, donationInfo.GUID);
            Assert.Equal(GenerateAndExpected_EventGuid, donationInfo.EventGUID);
            Assert.Equal(GenerateAndExpected_UserGuid, donationInfo.UserGUID);
            Assert.Equal(GenerateAndExpected_Amount, donationInfo.Amount);

        }

        [Fact]
        public void DonationInfo_ConstructorNoGuid_ReturnsCorrectInfo()
        {
            Guid NotExpected_Guid = Guid.NewGuid();
            Guid GenerateAndExpected_EventGuid = Guid.NewGuid();
            Guid GenerateAndExpected_UserGuid = Guid.NewGuid();
            float GenerateAndExpected_Amount = 5;

            DonationInfo donationInfo = new DonationInfo(
                                 GenerateAndExpected_EventGuid,
                                 GenerateAndExpected_UserGuid,
                                 GenerateAndExpected_Amount);

            Assert.NotEqual(NotExpected_Guid, donationInfo.GUID);
            Assert.Equal(GenerateAndExpected_EventGuid, donationInfo.EventGUID);
            Assert.Equal(GenerateAndExpected_UserGuid, donationInfo.UserGUID);
            Assert.Equal(GenerateAndExpected_Amount, donationInfo.Amount);
        }

        [Fact]
        public void DonationInfo_ConstructorOnlyGuid_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_Guid = Guid.NewGuid();
            Guid Expected_EventGuid = Guid.Empty;
            Guid Expected_UserGuid = Guid.Empty;
            float Expected_Amount = 0;

            DonationInfo donationInfo = new DonationInfo(
                                 GenerateAndExpected_Guid);

            Assert.Equal(GenerateAndExpected_Guid, donationInfo.GUID);
            Assert.Equal(Expected_EventGuid, donationInfo.EventGUID);
            Assert.Equal(Expected_UserGuid, donationInfo.UserGUID);
            Assert.Equal(Expected_Amount, donationInfo.Amount);
        }
    }
}
