using ContactsConsoleAPI.Business;
using ContactsConsoleAPI.Business.Contracts;
using ContactsConsoleAPI.Data.Models;
using ContactsConsoleAPI.DataAccess;
using ContactsConsoleAPI.DataAccess.Contrackts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsConsoleAPI.IntegrationTests.NUnit
{
    public class IntegrationTests
    {
        private TestContactDbContext dbContext;
        private IContactManager contactManager;

        [SetUp]
        public void SetUp()
        {
            this.dbContext = new TestContactDbContext();
            this.contactManager = new ContactManager(new ContactRepository(this.dbContext));
        }


        [TearDown]
        public void TearDown()
        {
            this.dbContext.Database.EnsureDeleted();
            this.dbContext.Dispose();
        }

        [Test]
        public async Task AddContactAsync_ShouldAddNewContact()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "test@gmail.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var dbContact = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);

            Assert.NotNull(dbContact);
            Assert.That(dbContact.FirstName, Is.EqualTo(newContact.FirstName));
            Assert.That(dbContact.LastName, Is.EqualTo(newContact.LastName));
            Assert.That(dbContact.Phone, Is.EqualTo(newContact.Phone));
            Assert.That(dbContact.Email, Is.EqualTo(newContact.Email));
            Assert.That(dbContact.Address, Is.EqualTo(newContact.Address));
            Assert.That(dbContact.Contact_ULID, Is.EqualTo(newContact.Contact_ULID));
        }

        [Test]
        public async Task AddContactAsync_TryToAddContactWithInvalidCredentials_ShouldThrowException()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH", //must be minimum 10 symbols - numbers or Upper case letters
                Email = "invalid_Mail", //invalid email
                Gender = "Male",
                Phone = "0889933779"
            };

            var exception = Assert.ThrowsAsync<ValidationException>(async () => await contactManager.AddAsync(newContact));
            var actual = await dbContext.Contacts.FirstOrDefaultAsync(c => c.Contact_ULID == newContact.Contact_ULID);

            Assert.IsNull(actual);
            Assert.That(exception?.Message, Is.EqualTo("Invalid contact!"));
        }

        [Test]
        public async Task DeleteContactAsync_WithValidULID_ShouldRemoveContactFromDb()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            await contactManager.DeleteAsync(newContact.Contact_ULID);

            var contactInDb = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Contact_ULID == newContact.Contact_ULID);

            Assert.That(contactInDb, Is.Null);
        }

        [TestCase("  ")]
        [TestCase(null)]
        public async Task DeleteContactAsync_TryToDeleteWithNullOrWhiteSpaceULID_ShouldThrowException(string nullOrWhiteSpace)
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var exception = Assert.ThrowsAsync<ArgumentException>(() => contactManager.DeleteAsync(nullOrWhiteSpace));

            Assert.That(exception?.Message, Is.EqualTo("ULID cannot be empty."));
        }

        [Test]
        public async Task GetAllAsync_WhenContactsExist_ShouldReturnAllContacts()
        {
            var firstContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            var secondContact = new Contact()
            {
                FirstName = "TestFirstName Second",
                LastName = "TestLastName Second",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HS",
                Email = "example2@email.com",
                Gender = "Male",
                Phone = "0889933778"
            };

            await contactManager.AddAsync(firstContact);
            await contactManager.AddAsync(secondContact);

            var actual = await contactManager.GetAllAsync();

            var firstContactInDb = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Contact_ULID == firstContact.Contact_ULID);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count, Is.EqualTo(2));
            Assert.That(firstContact.FirstName, Is.EqualTo(firstContactInDb?.FirstName));
            Assert.That(firstContact.LastName, Is.EqualTo(firstContactInDb?.LastName));
            Assert.That(firstContact.Address, Is.EqualTo(firstContactInDb?.Address));
            Assert.That(firstContact.Contact_ULID, Is.EqualTo(firstContactInDb?.Contact_ULID));
            Assert.That(firstContact.Email, Is.EqualTo(firstContactInDb?.Email));
            Assert.That(firstContact.Gender, Is.EqualTo(firstContactInDb?.Gender));
            Assert.That(firstContact.Phone, Is.EqualTo(firstContactInDb?.Phone));
        }

        [Test]
        public async Task GetAllAsync_WhenNoContactsExist_ShouldThrowKeyNotFoundException()
        {
            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.GetAllAsync());

            Assert.That(exception?.Message, Is.EqualTo("No contact found."));
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithExistingFirstName_ShouldReturnMatchingContacts()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var actual = await contactManager.SearchByFirstNameAsync(newContact.FirstName);
            var resultContact = actual.First();

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count(), Is.EqualTo(1));
            Assert.That(resultContact.FirstName, Is.EqualTo(newContact.FirstName));
            Assert.That(resultContact.LastName, Is.EqualTo(newContact.LastName));
            Assert.That(resultContact.Address, Is.EqualTo(newContact.Address));
            Assert.That(resultContact.Contact_ULID, Is.EqualTo(newContact.Contact_ULID));
            Assert.That(resultContact.Email, Is.EqualTo(newContact.Email));
            Assert.That(resultContact.Gender, Is.EqualTo(newContact.Gender));
            Assert.That(resultContact.Phone, Is.EqualTo(newContact.Phone));
        }

        [Test]
        public async Task SearchByFirstNameAsync_WithNonExistingFirstName_ShouldThrowKeyNotFoundException()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.SearchByFirstNameAsync("NonExistingFirstName"));

            Assert.That(exception.Message, Is.EqualTo("No contact found with the given first name."));
        }

        [TestCase ("   ")]
        [TestCase (null)]
        public async Task SearchByFirstNameAsync_WithNullOrWhiteSpace_ShouldThrowArgumentException(string nullOrWhiteSpace)
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var exception = Assert.ThrowsAsync<ArgumentException>(() => contactManager.SearchByFirstNameAsync(nullOrWhiteSpace));

            Assert.That(exception.Message, Is.EqualTo("First name cannot be empty."));
        }

        [Test]
        public async Task SearchByLastNameAsync_WithExistingLastName_ShouldReturnMatchingContacts()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var actual = await contactManager.SearchByLastNameAsync(newContact.LastName);
            var resultContact = actual.First();

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Count(), Is.EqualTo(1));
            Assert.That(resultContact.FirstName, Is.EqualTo(newContact.FirstName));
            Assert.That(resultContact.LastName, Is.EqualTo(newContact.LastName));
            Assert.That(resultContact.Address, Is.EqualTo(newContact.Address));
            Assert.That(resultContact.Contact_ULID, Is.EqualTo(newContact.Contact_ULID));
            Assert.That(resultContact.Email, Is.EqualTo(newContact.Email));
            Assert.That(resultContact.Gender, Is.EqualTo(newContact.Gender));
            Assert.That(resultContact.Phone, Is.EqualTo(newContact.Phone));
        }

        [Test]
        public async Task SearchByLastNameAsync_WithNonExistingLastName_ShouldThrowKeyNotFoundException()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.SearchByLastNameAsync("NonExistingFirstName"));

            Assert.That(exception.Message, Is.EqualTo("No contact found with the given last name."));
        }

        [TestCase("   ")]
        [TestCase(null)]
        public async Task SearchByLastNameAsync_WithNullOrWhiteSpace_ShouldThrowArgumentException(string nullOrWhiteSpace)
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var exception = Assert.ThrowsAsync<ArgumentException>(() => contactManager.SearchByLastNameAsync(nullOrWhiteSpace));

            Assert.That(exception.Message, Is.EqualTo("Last name cannot be empty."));
        }

        [Test]
        public async Task GetSpecificAsync_WithValidULID_ShouldReturnContact()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var actual = await contactManager.GetSpecificAsync(newContact.Contact_ULID);

            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.FirstName, Is.EqualTo(newContact.FirstName));
            Assert.That(actual.LastName, Is.EqualTo(newContact.LastName));
            Assert.That(actual.Address, Is.EqualTo(newContact.Address));
            Assert.That(actual.Contact_ULID, Is.EqualTo(newContact.Contact_ULID));
            Assert.That(actual.Email, Is.EqualTo(newContact.Email));
            Assert.That(actual.Gender, Is.EqualTo(newContact.Gender));
            Assert.That(actual.Phone, Is.EqualTo(newContact.Phone));
        }

        [Test]
        public async Task GetSpecificAsync_WithInvalidULID_ShouldThrowKeyNotFoundException()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);
            const string invalidULID = "1AAAAAAAAAA";

            var exception = Assert.ThrowsAsync<KeyNotFoundException>(() => contactManager.GetSpecificAsync(invalidULID));

            Assert.That(exception.Message, Is.EqualTo($"No contact found with ULID: {invalidULID}"));
        }

        [TestCase ("   ")]
        [TestCase (null)]
        public async Task GetSpecificAsync_WithNullOrWhiteSpace_ShouldThrowArgumentException(string nullOrWhiteSpace)
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var exception = Assert.ThrowsAsync<ArgumentException>(() => contactManager.GetSpecificAsync(nullOrWhiteSpace));

            Assert.That(exception.Message, Is.EqualTo("ULID cannot be empty."));
        }

        [Test]
        public async Task UpdateAsync_WithValidContact_ShouldUpdateContact()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var updatedContact = new Contact()
            {
                FirstName = "TestFirstName Updated",
                LastName = "TestLastName Updated",
                Address = "Anything for testing address Updated",
                Contact_ULID = "1ABC23456HA",
                Email = "example2@email.com",
                Gender = "Female",
                Phone = "0889933770"
            };

            await contactManager.UpdateAsync(updatedContact);

            var actual = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Contact_ULID == updatedContact.Contact_ULID);

            Assert.That(actual?.FirstName, Is.EqualTo(updatedContact.FirstName));
            Assert.That(actual?.LastName, Is.EqualTo(updatedContact.LastName));
            Assert.That(actual?.Address, Is.EqualTo(updatedContact.Address));
            Assert.That(actual?.Contact_ULID, Is.EqualTo(updatedContact.Contact_ULID));
            Assert.That(actual?.Email, Is.EqualTo(updatedContact.Email));
            Assert.That(actual?.Gender, Is.EqualTo(updatedContact.Gender));
            Assert.That(actual?.Phone, Is.EqualTo(updatedContact.Phone));
        }

        [Test]
        public async Task UpdateAsync_WithInvalidContact_ShouldThrowValidationException()
        {
            var newContact = new Contact()
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Address = "Anything for testing address",
                Contact_ULID = "1ABC23456HH",
                Email = "example@email.com",
                Gender = "Male",
                Phone = "0889933779"
            };

            await contactManager.AddAsync(newContact);

            var invalidContact = new Contact()
            {
                FirstName = "",
                LastName = "TestLastName Updated",
                Address = "Anything for testing address Updated",
                Contact_ULID = "123",
                Email = "123456@com",
                Gender = "Female",
                Phone = "0889933770"
            };

            var exception = Assert.ThrowsAsync<ValidationException>(() => contactManager.UpdateAsync(invalidContact));

            Assert.That(exception.Message, Is.EqualTo("Invalid contact!"));
        }
    }
}
