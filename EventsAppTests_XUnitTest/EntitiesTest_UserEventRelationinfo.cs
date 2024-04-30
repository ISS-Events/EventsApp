using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    using EventsApp.Logic.Entities;
    public class EntitiesTest_UserEventRelationinfo
    {
        [Fact]
        public void UserEventRelationInfo_ConstructorAllInfo_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_UserGuid = Guid.NewGuid();
            Guid GenerateAndExpected_EventGuid = Guid.NewGuid();
            string GenerateAndExpected_Status = "Going";

            UserEventRelationInfo userEventRelationInfo = new UserEventRelationInfo(
                               GenerateAndExpected_UserGuid,
                               GenerateAndExpected_EventGuid,
                               GenerateAndExpected_Status);

            Assert.Equal(GenerateAndExpected_UserGuid, userEventRelationInfo.UserGUID);
            Assert.Equal(GenerateAndExpected_EventGuid, userEventRelationInfo.EventGUID);
            Assert.Equal(GenerateAndExpected_Status, userEventRelationInfo.Status);
        }

        [Fact]
        public void UserEventRelationInfo_ConstructorNoStatus_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_UserGuid = Guid.NewGuid();
            Guid GenerateAndExpected_EventGuid = Guid.NewGuid();

            UserEventRelationInfo userEventRelationInfo = new UserEventRelationInfo(
                                              GenerateAndExpected_UserGuid,
                                                                            GenerateAndExpected_EventGuid);

            Assert.Equal(GenerateAndExpected_UserGuid, userEventRelationInfo.UserGUID);
            Assert.Equal(GenerateAndExpected_EventGuid, userEventRelationInfo.EventGUID);
            Assert.Equal(string.Empty, userEventRelationInfo.Status);
        }
    }
}
