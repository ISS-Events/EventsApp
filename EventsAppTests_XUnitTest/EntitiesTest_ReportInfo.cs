using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    using EventsApp.Logic.Entities;
    public class EntitiesTest_ReportInfo
    {
        [Theory]
        [InlineData(ReportInfo.ReportType.Nudity)]
        [InlineData(ReportInfo.ReportType.Violence)]
        [InlineData(ReportInfo.ReportType.Harassment)]
        [InlineData(ReportInfo.ReportType.Spam)]
        [InlineData(ReportInfo.ReportType.None)]
        [InlineData(ReportInfo.ReportType.Fraud)]
        [InlineData(ReportInfo.ReportType.Offensive)]
        [InlineData(ReportInfo.ReportType.GuidelinesViolations)]
        public void ReportInfo_ConstructorAllInfo_ReturnsCorrectInfo(
            ReportInfo.ReportType GenerateAndExpected_reportType)
        {
            Guid GenerateAndExpected_userGuid = Guid.NewGuid();
            Guid GenerateAndExpected_eventGuid = Guid.NewGuid();


            ReportInfo reportInfo = new ReportInfo(
                               GenerateAndExpected_userGuid,
                               GenerateAndExpected_eventGuid,
                               GenerateAndExpected_reportType);

            Assert.Equal(GenerateAndExpected_userGuid, reportInfo.UserGUID);
            Assert.Equal(GenerateAndExpected_eventGuid, reportInfo.EventGUID);
            Assert.Equal(GenerateAndExpected_reportType, reportInfo.ReportTypeValue);
        }
    }
}
