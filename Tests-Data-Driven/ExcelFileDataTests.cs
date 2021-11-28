using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using SpreadsheetLight;
using System.Collections.Generic;

namespace Tests_Data_Driven
{
    class ExcelFileDataTests
    {
        [TestCaseSource("LoadTestDataFromExcel")]
        public void TestZippopotamous(string countryCode, string zipCode, string expectedPlace, string expectedStateCode)
        {
            //Arrange
            var restClient = new RestClient("https://api.zippopotam.us");
            var httpRequest = new RestRequest(countryCode + "/" + zipCode);

            //Act
            var httpResponse = restClient.Execute(httpRequest);
            var location = new JsonDeserializer().Deserialize<Location>(httpResponse);

            //Assert
            StringAssert.Contains(expectedPlace, location.Places[0].PlaceName);
            StringAssert.Contains(expectedStateCode, location.Places[0].StateAbbreviation);
        }
        static IEnumerable<TestCaseData> LoadTestDataFromExcel()
        {
            using (var sheet = new SLDocument("../../../ZippopotamousTestData.xlsx"))
            {
                int endRowIndex = sheet.GetWorksheetStatistics().EndRowIndex;
                for (int row = 2; row <= endRowIndex; row++)
                {
                    string countryCode = sheet.GetCellValueAsString(row, 1);
                    string zipCode = sheet.GetCellValueAsString(row, 2);
                    string expectedPlace = sheet.GetCellValueAsString(row, 3);
                    string expectedStateCode = sheet.GetCellValueAsString(row, 4);
                    yield return new TestCaseData(countryCode, zipCode, expectedPlace, expectedStateCode);
                }
            }
            //yield return new TestCaseData("BG", "1000", "Sofija");
            //yield return new TestCaseData("BG", "5000", "Veliko Turnovo");
            //yield return new TestCaseData("CA", "M5S", "Toronto");
            //yield return new TestCaseData("GB", "B1", "Birmingham");
            //yield return new TestCaseData("DE", "01067", "Dresden");
        }
    }
}
