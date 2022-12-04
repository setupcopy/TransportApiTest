using Moq;
using Moq.Protected;
using System.Net;
using Transport.Api.Api;
using Transport.Api.Applications;
using Transport.Api.Models;

namespace Transport.Test
{
    public class VehicleApplicationTest
    {
        private Mock<HttpMessageHandler> _handMock;
        public VehicleApplicationTest()
        {
            MockRepository mockRepository = new MockRepository(MockBehavior.Default);
            _handMock = mockRepository.Create<HttpMessageHandler>();
        }

        private void GetResponseWithoutContent()
        {
            _handMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'from':'Sydney','to':'Parramatta','listings':[]}")
            })
            .Verifiable();
        }

        private void GetResponseWithContent()
        {
            _handMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{'from':'Sydney','to':'Parramatta','listings':[{'name':'Listing 1','pricePerPassenger':'34','vehicleType':{'name':'Hatchback','maxPassengers':2}}]}")
            })
            .Verifiable();
        }

        /// <summary>
        /// Get instance of VehicleApplication
        /// </summary>
        /// <param name="flag">0:no content in response. 1: response with content</param>
        /// <returns></returns>
        private VehicleApplication GetVehicleApplicationInstnce(int flag) 
        {
            if (flag == 0)
            {
                GetResponseWithoutContent();
            }
            else
            {
                GetResponseWithContent();
            }

            var mockHttpCilent = new HttpClient(_handMock.Object);
            var vehicleServic = new VehicleService(mockHttpCilent);

            return new VehicleApplication(vehicleServic);
        }

        [Fact]
        public async void ShouldReturnNullGetVehiclesAsync()
        {
            _handMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.BadRequest
                    })
                    .Verifiable();

            var mockHttpCilent = new HttpClient(_handMock.Object);
            var vehicleServic = new  VehicleService(mockHttpCilent);

            var vehicleApplication = new VehicleApplication(vehicleServic);
            var result = await vehicleApplication.GetVehiclesAsync();

            Assert.Null(result);
        }

        [Fact]
        public async void ShouldReturnCountZeroGetVehiclesAsyncWithNumberOfPassengers()
        {
            var vehicleApplication = GetVehicleApplicationInstnce(0);
            var result = await vehicleApplication.GetVehiclesAsync(5);

            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async void ShouldReturnCountZeroGetVehiclesAsyncGivenZeroToNumberOfPassengers()
        {
            var vehicleApplication = GetVehicleApplicationInstnce(1);
            var result = await vehicleApplication.GetVehiclesAsync(0);

            Assert.Equal(0, result.Count);
        }
    }
}