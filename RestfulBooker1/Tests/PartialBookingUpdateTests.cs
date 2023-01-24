using System.Net;
using NUnit.Framework;
using RestfulBooker1.Helpers;
using RestfulBooker1.Models.PartialBookingUpdate.Response;
using RestfulBooker1.Services;
using RestSharp;

namespace RestfulBooker1.Tests
{
    public class PartialBookingUpdateTests
    {
        [Test]
        public void SuccessfulFirstnameAndLastnameBookingUpdate()
        {
            PartialBookingUpdateService service = new PartialBookingUpdateService(ConfigHelper.WEB_API_URL);

            RestResponse<PartialBookingUpdateResponse> response =
                service.FirstAndLastNameUpdate(Constants.FirstName, Constants.LastName);
            
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data.FirstName, Is.EqualTo(Constants.FirstName));
            Assert.That(response.Data.LastName, Is.EqualTo(Constants.LastName));
            Assert.That(response.Data.TotalPrice, Is.EqualTo(200));
            Assert.That(response.Data.DepositPaid, Is.EqualTo(true));
            Assert.That(response.Data.AdditionalNeeds, Is.EqualTo("nothing"));
        }

        [Test]
        public void SuccessfulPriceDepositAndNeedUpdate()
        {
            PartialBookingUpdateService service = new PartialBookingUpdateService(ConfigHelper.WEB_API_URL);

            RestResponse<PartialBookingUpdateResponse> response =
                service.PriceDepositAndNeedsUpdate(400, false, "breakfast");
            
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Data.TotalPrice, Is.EqualTo(400));
            Assert.That(response.Data.DepositPaid, Is.EqualTo(false));
            Assert.That(response.Data.AdditionalNeeds, Is.EqualTo("breakfast"));
        }

        [Test]
        public void PartialBookingUpdateWithoutAuth()
        {
            PartialBookingUpdateService service = new PartialBookingUpdateService(ConfigHelper.WEB_API_URL);

            RestResponse<PartialBookingUpdateResponse> response =
                service.PartialUpdateBookingNoAuth(Constants.FirstName, Constants.LastName);
            
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }
    }
}