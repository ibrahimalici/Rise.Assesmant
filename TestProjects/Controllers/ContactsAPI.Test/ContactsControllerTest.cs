using ContactsAPI.Application.Communications;
using ContactsAPI.Application.ContactsInfo.Commands;
using ContactsAPI.Application.ContactsInfo.Queries;
using ContactsAPI.Application.Reports.Commands;
using ContactsAPI.Application.ReportsInfo.Queries;
using ContactsAPI.Controllers;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SharedLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace ContactsAPI.Test
{
    public class ContactsControllerTest
    {

        private readonly Mock<IMediator> _mock;
        private readonly Mock<IMassTransitHelper> _mockMassTransit;
        private readonly Mock<ISendEndpoint> _mockMSTProvider;
        private readonly ContactsController _controller;
        private readonly ContactDetailsController _controllerDetail;

        List<ContactDTO> kisiler = new List<ContactDTO>()
        {
            new ContactDTO { Name = "", Company =""}
        };

        public ContactsControllerTest()
        {
            _mock = new Mock<IMediator>();
            _controller = new ContactsController(_mock.Object);
            _controllerDetail = new ContactDetailsController(_mock.Object);
        }

        #region ContactsAPI.Test

        #region .Get
        [Theory]
        [InlineData("18dbb468-1d86-4331-b1d0-84959dd16b3c")]
        public async void Get_ActionExecuted_IsSuccesfuly(string Id)
        {
            _mock.Setup(m => m.Send(It.IsAny<GetContactByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ContactDTO
            {
                ContactId = Guid.Parse(Id),
                Name = "Halil",
                Surename = "Sezai",
                Company = ""
            });
            var result = await _controller.Get(Guid.Parse(Id));

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ContactDTO>(object_.Value);
        }
        #endregion

        #region .GetAll
        [Theory]
        [InlineData(true, 0, 10)]
        [InlineData(true, 0, 2)]
        [InlineData(false, 0, 0)]
        public async void GetAll_ActionExecuted_ReturnOK(bool Paging, int StartIndex, int RecordCount)
        {
            List<ContactDTO> dummy = new List<ContactDTO>()
            {
                new ContactDTO { Name = "Halil", Surename = "Sezai", Company = "deneme", ContactId = Guid.NewGuid() }
            };

            GetAllContactsQuery query = new GetAllContactsQuery
            {
                Paging = Paging,
                StartIndex = StartIndex,
                RecordCount = RecordCount
            };

            _mock.Setup(m => m.Send(It.IsAny<GetAllContactsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(dummy);
            var result = await _controller.GetAll(query);

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ContactDTO>>(object_.Value);
        }
        #endregion

        #region .Post
        [Theory]
        [InlineData("Halil", "SEZAİ", "Deneme")]
        [InlineData("Safiye", "AYLA", "Deneme")]
        [InlineData("Şükriye", "TUTKUN", "Deneme")]
        [InlineData("Nisan", "AK", "Deneme")]
        public async void Post_ActionExecuted_IsSuccesfuly(string Ad, string Soyad, string Firma)
        {
            List<ContactDTO> dummy = new List<ContactDTO>()
            {
                new ContactDTO { Name = "Halil", Surename = "Sezai", Company = "deneme", ContactId = Guid.NewGuid() }
            };

            CreateContactCommand command = new CreateContactCommand
            {
                Name = Ad,
                Surename = Soyad,
                Company = Firma
            };

            _mock.Setup(m => m.Send(It.IsAny<CreateContactCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid());
            var result = await _controller.Post(command);

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Guid>(object_.Value);
        }
        #endregion

        #region .Put
        [Theory]
        [InlineData("3ae252d9-d5fa-4f1d-a126-cce3df320bd7", "Halil", "SEZAİ", "Deneme")]
        [InlineData("7df8e4c4-474a-40f5-800b-8829d48cca5b", "Safiye", "AYLA", "Deneme")]
        [InlineData("8cc127f2-9158-4e99-be35-927aa5a0bac0", "Şükriye", "TUTKUN", "Deneme")]
        [InlineData("0cfb6dce-e800-4901-9c10-7c85980a9885", "Nisan", "AK", "Deneme")]
        public async void Update_ActionExecuted_IsSuccesfuly(string Id, string Ad, string Soyad, string Firma)
        {
            List<ContactDTO> dummy = new List<ContactDTO>()
            {
                new ContactDTO { Name = "Halil", Surename = "Sezai", Company = "deneme", ContactId = Guid.NewGuid() }
            };

            UpdateContactCommand command = new UpdateContactCommand
            {
                ContactId = Guid.Parse(Id),
                Name = Ad,
                Surename = Soyad,
                Company = Firma
            };

            _mock.Setup(m => m.Send(It.IsAny<UpdateContactCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            var result = await _controller.Put(command);

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<bool>(object_.Value);
        }
        #endregion

        #region .Delete
        [Theory]
        [InlineData("18dbb468-1d86-4331-b1d0-84959dd16b3c")]
        public async void Delete_ActionExecuted_IsSuccesfuly(string Id)
        {
            _mock.Setup(m => m.Send(It.IsAny<DeleteContactCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            var result = await _controller.Delete(Guid.Parse(Id));

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<bool>(object_.Value);
        }
        #endregion

        #region .PrepareReport
        [Fact]
        public async void PrepareReport_ActionExecuted_IsSuccessfuly()
        {
            _mock.Setup(m => m.Send(It.IsAny<PrepareReportCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ReportDTO
            {
                ReportId = Guid.NewGuid(),
                ReportDemandDateTime = DateTime.Now,
                ReportStatus = ReportStatus.Preparing,
                Reports = new List<ReportDetailDTO>
                {
                    new ReportDetailDTO { ReportId = Guid.NewGuid(), ContactCount =1, PhoneCount = 1, Location="ANKARA" }
                }
            });

            var result = await _controller.PrepareReport();
            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ReportDTO>(object_.Value);
        }
        #endregion

        #region .GetPreparedReports
        [Fact]
        public async void GetPreparedReports_ActionExecuted_IsSuccessfuly()
        {
            _mock.Setup(m => m.Send(It.IsAny<GetAllReportQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<ReportDTO>
            {
                new ReportDTO {
                    ReportId = Guid.NewGuid(),
                    ReportDemandDateTime = DateTime.Now,
                    ReportStatus = ReportStatus.Preparing,
                    Reports = new List<ReportDetailDTO>
                    {
                        new ReportDetailDTO { ReportId = Guid.NewGuid(), ContactCount =1, PhoneCount = 1, Location="ANKARA" }
                    }
                }
            });

            var result = await _controller.GetPreparedReports();
            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ReportDTO>>(object_.Value);
        }
        #endregion

        #endregion
    }
}