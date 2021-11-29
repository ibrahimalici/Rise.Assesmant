using Microsoft.AspNetCore.Mvc;
using Moq;
using ReportsAPI.Controllers;
using ReportsAPI.Data;
using SharedLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ReportsAPI.Test
{
    public class ReportsControllerTest
    {
        private readonly Mock<IDataRepository> _mock;
        private readonly ReportsController _controller;

        public ReportsControllerTest()
        {
            _mock = new Mock<IDataRepository>();
            _controller = new ReportsController(_mock.Object);
        }

        #region .Get
        [Theory]
        [InlineData("bff2299c-8967-4e04-b1b8-45cbc6c0a996")]
        public async void Get_ExecuteAction_IsSuccessfully(string id)
        {
            _mock.Setup(x => x.GetReportObject(Guid.Parse(id)).Result).Returns(new ReportDTO
            {
                ReportDemandDateTime = DateTime.Now,
                ReportId = Guid.NewGuid(),
                Reports = new List<ReportDetailDTO>(),
                ReportStatus = ReportStatus.Preparing
            });

            var result = await _controller.Get(Guid.Parse(id));

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<ReportDTO>(object_.Value);
        }
        #endregion

        #region .GetAll
        [Fact]
        public async void GetAll_ExecuteAction_IsSuccessfully()
        {
            _mock.Setup(x => x.GetAllReportObjects().Result).Returns(new List<ReportDTO>
            {
                new ReportDTO
                {
                    ReportDemandDateTime = DateTime.Now,
                    ReportId = Guid.NewGuid(),
                    Reports = new List<ReportDetailDTO>(),
                    ReportStatus = ReportStatus.Preparing
                }
            });

            var result = await _controller.GetAll();

            var object_ = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<ReportDTO>>(object_.Value);
        }
        #endregion
    }
}
