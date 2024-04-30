using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    using EventsApp.Logic.Entities;
    public class EntitiesTest_AdminInfo
    {
        [Fact]
        public void AdminInfo_ConstructorAllInfo_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_Guid = Guid.NewGuid();

            AdminInfo adminInfo = new AdminInfo(GenerateAndExpected_Guid);

            Assert.Equal(GenerateAndExpected_Guid, adminInfo.GUID);
        }
    }
}
