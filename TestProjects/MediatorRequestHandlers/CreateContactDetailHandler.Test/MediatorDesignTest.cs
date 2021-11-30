using ContactsAPI.Application.ContactsInfo.Commands;
using ContactsAPI.Application.ContactSubDetails.Commands;
using ContactsAPI.Entities;
using ContactsAPI.Persistance;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MediatorDesign.CRUD.Test
{
    public class MediatorDesignTest
    {
        private Mock<IConfiguration> _mock;
        private CreateContactDetailHandler createContactDetailHandler;
        private CreateContactHandler createContactHandle;
        private DatabaseContext db;
        private readonly SqliteConnection connection;
        public MediatorDesignTest()
        {
            _mock = new Mock<IConfiguration>();

            connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlite(connection).Options;
            db = new DatabaseContext(options);
        }

        #region .EF Crud Test
        [Fact]
        public async void ContactAndContactDetail_AddNew_IsSuccessfully()
        {
            bool result = await AddData();

            Assert.True(result);
        }

        [Fact]
        public async Task ContactAndContactDetail_UpdateDatas_IsSuccessfully()
        {
            await AddData();

            Contact contact = await db.Contacts.FirstOrDefaultAsync();
            contact.Company = "Deneme A.Þ.";
            await db.SaveChangesAsync();

            ContactDetail contactDetail = await db.ContactDetails.Where(o=>o.ContactId == contact.ContactId).FirstOrDefaultAsync();
            contactDetail.Description = "test edildi";
            int result = await db.SaveChangesAsync();

            Assert.IsType<int>(result);
        }

        public async Task ContactAndContactDetail_DeleteDatas_IsSuccessfully()
        {
            await AddData();

            Contact contact = await db.Contacts.FirstOrDefaultAsync();
            ContactDetail contactDetail = await db.ContactDetails.Where(o => o.ContactId == contact.ContactId).FirstOrDefaultAsync();

            db.ContactDetails.Remove(contactDetail);
            db.Contacts.Remove(contact);
            int result = await db.SaveChangesAsync();

            Assert.IsType<int>(result);
        }
        #endregion

        [Theory]
        [InlineData("Metin", "aaa", "")]
        [InlineData("Ali", "bbb", "")]
        [InlineData("Feyyaz", "ccc", "")]
        public void CreateContactHandler_HandleAction_IsSuccessfully(string Name, string Surename, string Company)
        {
            createContactHandle = new CreateContactHandler(db);
            var result = createContactHandle.Handle(new CreateContactCommand
            {
                Company = Company,
                Name = Name,
                Surename = Surename
            }, new System.Threading.CancellationToken());
        }

        [Theory]
        [InlineData(1, "05371654987")]
        [InlineData(2, "asda@com.tr")]
        [InlineData(3, "ÝSTANBUL")]
        public async void CreateContactDetailHandler_HandleAction_IsSuccessfully(int ContactDetailType, string Description)
        {
            await AddData();
            var firstContact = await db.Contacts.FirstOrDefaultAsync();
            Guid ContactId = firstContact.ContactId;

            createContactDetailHandler = new CreateContactDetailHandler(db);
            var result = createContactDetailHandler.Handle(new CreateContactDetailCommand
            {
                ContactDetailType = (SharedLibrary.Domains.ContactDetailType)ContactDetailType,
                ContactId = ContactId,
                Description = Description
            }, new System.Threading.CancellationToken());

            Assert.IsType<Guid>(result.Result);
        }

        #region AddData
        public async Task<bool> AddData()
        {
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();

            Guid g = Guid.Parse("efe6b3b6-18e3-4003-964a-48e5745aac3f");
            await db.Contacts.AddAsync(new ContactsAPI.Entities.Contact
            {
                ContactId = g,
                Name = "Hasan",
                Surename = "Hüseyin",
                Company = "x"
            });
            Guid sg = Guid.Parse("9674d17a-4613-4465-ba3d-5a48c4835834");
            await db.ContactDetails.AddAsync(new ContactsAPI.Entities.ContactDetail
            {
                ContactId = g,
                ContactDetailId = sg,
                ContactDetailType = SharedLibrary.Domains.ContactDetailType.Email,
                Description = "deneme@deneme.com"
            });

            await db.SaveChangesAsync();

            return true;
        }
        #endregion
    }
}
