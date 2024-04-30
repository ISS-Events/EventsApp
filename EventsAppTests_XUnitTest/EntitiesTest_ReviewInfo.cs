using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    using EventsApp.Logic.Entities;
    public class EntitiesTest_ReviewInfo
    {
        [Fact]
        public void ReviewInfo_ConstructorAllInfo_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_UserGuid = Guid.NewGuid();
            Guid GenerateAndExpected_EventGuid = Guid.NewGuid();
            float GenerateAndExpected_Score = 5;
            string GenerateAndExpected_ReviewDescription = "Good";


            ReviewInfo reviewInfo = new ReviewInfo(
                               GenerateAndExpected_UserGuid,
                               GenerateAndExpected_EventGuid,
                               GenerateAndExpected_Score,
                               GenerateAndExpected_ReviewDescription);

            Assert.Equal(GenerateAndExpected_UserGuid, reviewInfo.UserGUID);
            Assert.Equal(GenerateAndExpected_EventGuid, reviewInfo.EventGUID);
            Assert.Equal(GenerateAndExpected_Score, reviewInfo.Score);
            Assert.Equal(GenerateAndExpected_ReviewDescription, reviewInfo.ReviewDescription);
        }

        [Fact]
        public void ReviewInfo_ConstructorNoScoreAndReviewDescription_ReturnsCorrectInfo()
        {
            Guid GenerateAndExpected_UserGuid = Guid.NewGuid();
            Guid GenerateAndExpected_EventGuid = Guid.NewGuid();
            float GenerateAndExpected_Score = 0;
            string GenerateAndExpected_ReviewDescription = string.Empty;

            ReviewInfo reviewInfo = new ReviewInfo(
                                GenerateAndExpected_UserGuid,
                                GenerateAndExpected_EventGuid);

            Assert.Equal(GenerateAndExpected_UserGuid, reviewInfo.UserGUID);
            Assert.Equal(GenerateAndExpected_EventGuid, reviewInfo.EventGUID);
            Assert.Equal(GenerateAndExpected_Score, reviewInfo.Score);
            Assert.Equal(GenerateAndExpected_ReviewDescription, reviewInfo.ReviewDescription);
        }
    }
}
