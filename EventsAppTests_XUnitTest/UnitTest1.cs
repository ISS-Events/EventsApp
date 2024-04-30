namespace EventsAppTests_XUnitTest
{
    using EventsApp.Logic.Entities;
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void UsserInfo_ConstructorAllInfo_ReturnsCorrectInfo()
        {
            Guid guid = Guid.NewGuid();
            string name = "John";
            string password = "123";

            UserInfo userInfo = new UserInfo(guid, name, password);

            Assert.Equal(guid, userInfo.GUID);
            Assert.Equal(name, userInfo.Name);
            Assert.Equal(password, userInfo.Password);
        }
    }
}