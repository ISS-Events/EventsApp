using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    using EventsApp.Logic.Entities;
    public class EntitiesTest_UserInfo
    {
        [Fact]
        public void UserInfo_ConstructorAllInfo_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_guid = Guid.NewGuid();
            string GenerateAndExpected_name = "John";
            string GenerateAndExpected_password = "123";

            UserInfo userInfo = new UserInfo(
                GenerateAndExpected_guid,
                GenerateAndExpected_name,
                GenerateAndExpected_password);

            Assert.Equal(GenerateAndExpected_guid, userInfo.GUID);
            Assert.Equal(GenerateAndExpected_name, userInfo.Name);
            Assert.Equal(GenerateAndExpected_password, userInfo.Password);
        }

        [Fact]
        public void UserInfo_ConstructorNoGuid_ReturnsCorrectInfo()
        {
            string GenerateAndExpected_name = "John";
            string GenerateAndExpected_password = "123";

            UserInfo userInfo = new UserInfo(
                GenerateAndExpected_name,
                GenerateAndExpected_password);

            Assert.NotEqual(Guid.Empty, userInfo.GUID);
            Assert.Equal(GenerateAndExpected_name, userInfo.Name);
            Assert.Equal(GenerateAndExpected_password, userInfo.Password);
        }

        [Fact]
        public void UserInfo_ConstructorOnlyGuid_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_guid = Guid.NewGuid();

            UserInfo userInfo = new UserInfo(GenerateAndExpected_guid);

            Assert.Equal(GenerateAndExpected_guid, userInfo.GUID);
            Assert.Equal(string.Empty, userInfo.Name);
            Assert.Equal(string.Empty, userInfo.Password);
        }

        [Fact]
        public void UserInfo_ConstructorNoInfo_ReturnsCorrectInfo()
        {
            UserInfo userInfo = new UserInfo();

            Assert.NotEqual(Guid.Empty, userInfo.GUID);
            Assert.Equal(string.Empty, userInfo.Name);
            Assert.Equal(string.Empty, userInfo.Password);
        }
    }
}
