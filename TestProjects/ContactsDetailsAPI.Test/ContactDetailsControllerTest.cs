using ContactsAPI.Application.ContactSubDetails.Commands;
using ContactsAPI.Application.ContactSubDetails.Queries;
using ContactsAPI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SharedLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace ContactsDetailsAPI.Test
{
    public class ContactDetailsControllerTest
    {
        private readonly Mock<IMediator> _mock;
        private readonly ContactDetailsController _controller;

        List<ContactDetailDTO> Iletisimler = new List<ContactDetailDTO>()
        {
            new ContactDetailDTO()
        };

        public ContactDetailsControllerTest()
        {
            _mock = new Mock<IMediator>();
            _controller = new ContactDetailsController(_mock.Object);

        }

        #region ContactsAPI.Test

        #region .Get
        [Theory]
        [InlineData("bff2299c-8967-4e04-b1b8-45cbc6c0a996")]
        public async void Get_ActionExecuted_IsSuccesfuly(string Id)
        {
            _mock.Setup(m => m.Send(It.IsAny<GetContactDetailByIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ContactDetailDTO
            {
                ContactDetailId = Guid.Parse(Id),
                Decription = "sezai@gmail.com",
                ContactDetailType = ContactDetailType.Email,
                ContactId = Guid.NewGuid(),
            });
            var result = await _controller.Get(Guid.Parse(Id));

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ContactDetailDTO>(object_.Value);
        }
        #endregion

        #region .GetAll
        [Theory]
        [InlineData(true, 0, 10)]
        [InlineData(true, 0, 2)]
        [InlineData(false, 0, 0)]
        public async void GetAll_ActionExecuted_ReturnOK(bool Paging, int StartIndex, int RecordCount)
        {
            List<ContactDetailDTO> dummy = new List<ContactDetailDTO>()
            {
                new ContactDetailDTO 
                {
                    ContactDetailId= Guid.NewGuid(),
                    Decription = "sezai@gmail.com",
                    ContactDetailType = ContactDetailType.Email,
                    ContactId = Guid.NewGuid()
                },
                new ContactDetailDTO
                {
                    ContactDetailId= Guid.NewGuid(),
                    Decription = "ANKARA",
                    ContactDetailType = ContactDetailType.Location,
                    ContactId = Guid.NewGuid()
                }
            };

            GetAllContactDetailsQuery query = new GetAllContactDetailsQuery
            {
                Paging = Paging,
                StartIndex = StartIndex,
                RecordCount = RecordCount
            };

            _mock.Setup(m => m.Send(It.IsAny<GetAllContactDetailsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(dummy);
            var result = await _controller.GetAll(query);

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ContactDetailDTO>>(object_.Value);
        }
        #endregion

        #region .Post
        [Theory]
        [InlineData("35875080-fbf2-42b3-8aba-9b8909bcee56", 1, "Deneme")]
        public async void Post_ActionExecuted_IsSuccesfuly(string KisiId, int BilgiTuru, string BilgiIcerigi)
        {
            CreateContactDetailCommand command = new CreateContactDetailCommand
            {
                ContactId = Guid.Parse(KisiId),
                ContactDetailType = (ContactDetailType)BilgiTuru,
                Description = BilgiIcerigi
            };

            _mock.Setup(m => m.Send(It.IsAny<CreateContactDetailCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(Guid.NewGuid());
            var result = await _controller.Post(command);

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<Guid>(object_.Value);
        }
        #endregion

        #region .Put
        [Theory]
        [InlineData("bdcba6ae-5763-4b18-8f05-6bebef88aafb", "35875080-fbf2-42b3-8aba-9b8909bcee56", 1, "Update Deneme")]
        public async void Update_ActionExecuted_IsSuccesfuly(string Id, string KisiId, int BilgiTuru, string BilgiIcerigi)
        {
            UpdateContactDetailCommand command = new UpdateContactDetailCommand
            {
                ContactDetailId = Guid.Parse(Id),
                ContactId = Guid.Parse(KisiId),
                ContactDetailType = (ContactDetailType)BilgiTuru,
                Description = BilgiIcerigi
            };

            _mock.Setup(m => m.Send(It.IsAny<UpdateContactDetailCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
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
            _mock.Setup(m => m.Send(It.IsAny<DeleteContactDetailCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
            var result = await _controller.Delete(Guid.Parse(Id));

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<bool>(object_.Value);
        }
        #endregion

        #endregion 
    }
}
