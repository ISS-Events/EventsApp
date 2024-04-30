using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    using EventsApp.Logic.Entities;
    public class EntitiesTest_ExpenseInfo
    {
        [Fact]
        public void ExpenseInfo_ConstructorAllInfo_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_Guid = Guid.NewGuid();
            Guid GenerateAndExpected_EventGuid = Guid.NewGuid();
            string GenerateAndExpected_ExpenseName = "Food";
            float GenerateAndExpected_Cost = 5;

            ExpenseInfo expenseInfo = new ExpenseInfo(
                                 GenerateAndExpected_Guid,
                                 GenerateAndExpected_EventGuid,
                                 GenerateAndExpected_ExpenseName,
                                 GenerateAndExpected_Cost);

            Assert.Equal(GenerateAndExpected_Guid, expenseInfo.GUID);
            Assert.Equal(GenerateAndExpected_EventGuid, expenseInfo.EventGUID);
            Assert.Equal(GenerateAndExpected_ExpenseName, expenseInfo.ExpenseName);
            Assert.Equal(GenerateAndExpected_Cost, expenseInfo.Cost);
        }
    }
}
