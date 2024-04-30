namespace EventsAppTests
{
    using EventsApp.Logic.Entities;
    public class Tests
    {

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public void UsserInfo_ConstructorAllInfo_ReturnsCorrectInfo()
        {
            Guid guid = Guid.NewGuid();
            string name = "John";
            string password = "123";

            UserInfo userInfo = new UserInfo(guid, name, password);

            Assert.That(guid, Is.EqualTo(userInfo.GUID));
            Assert.That(name, Is.EqualTo(userInfo.Name));
            Assert.That(password, Is.EqualTo(userInfo.Password));
        }
    }
}