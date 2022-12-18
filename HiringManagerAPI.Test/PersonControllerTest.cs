using HiringManagerAPI.Controllers;
using HiringManagerAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringManagerAPI.Test
{
    public class PersonControllerTest
    {
        private readonly PersonController _controller;
        public List<Person> personList { get; set; }

        public PersonControllerTest()
        {
            personList= new List<Person>();

            personList.Add(new Person() { Id= 0, Name="Jane", LastName="Doe", Email="janedoe@gmail.com", Phone="123456789", Address="City Lorem ipsum, 1", DateOfBirth= new DateTime(1991,8,14), DateOfInterview= new DateTime(2022,11,14), OfferJob=true, Remarks= "Lorem ipsum dolor sit." });
            personList.Add(new Person() { Id = 1, Name = "John", LastName = "Doe", Email = "johndoe@gmail.com", Phone = "987654321", Address = "City Lorem ipsum, 2", DateOfBirth = new DateTime(1993, 7, 14), DateOfInterview = new DateTime(2022, 11, 1), OfferJob = false, Remarks = "Lorem ipsum dolor sit amet consectetur." });

            _controller = new PersonController();

        }

        [Fact]
        public void Add_WhenCalled_ReturnsBadRequestResult()
        {
            // Act
            var response = _controller.Add(null);

            // Assert
            Assert.IsType<BadRequestResult>(response as BadRequestResult);

        }

        [Fact]
        public void Add_WhenCalled_ReturnsPerson()
        {
            // Act
            var response = _controller.Add(personList[0]);

            // Assert
            Assert.IsType<CreatedAtActionResult>(response);

            // Act
            var response2 = _controller.Add(personList[1]);

            // Assert
            Assert.IsType<CreatedAtActionResult>(response2);
        }

        [Fact]
        public void Put_WhenCalled_ReturnsBadRequestResult()
        {
            Person p = personList[0];
            p.Id= -1;

            // Act
            var response = _controller.Put(0,p);

            // Assert
            Assert.IsType<BadRequestResult>(response as BadRequestResult);

            p.Id = 0;

            // Act
            var response2 = _controller.Put(-1, p);

            // Assert
            Assert.IsType<BadRequestResult>(response2 as BadRequestResult);

        }

        [Fact]
        public void Put_WhenCalled_ReturnsNotFoundResult()
        {
            Person p = personList[0];
            p.Email = "test@gmail.com"; ;

            // Act
            var response = _controller.Put(999, p);

            // Assert
            Assert.IsType<NotFoundResult>(response as NotFoundResult);

        }

        [Fact]
        public void Put_WhenCalled_ReturnsNoContentResult()
        {
            Person p = personList[0];
            p.Email = "test@gmail.com"; ;

            // Act
            var response = _controller.Put(p.Id, p);

            // Assert
            Assert.IsType<NoContentResult>(response as NoContentResult);

        }

        [Fact]
        public void Del_WhenCalled_ReturnsNotFoundResult()
        {
            // Act
            var response = _controller.Delete(999);
            // Assert
            Assert.IsType<NotFoundResult>(response as NotFoundResult);
        }

        [Fact]
        public void Del_WhenCalled_ReturnsNoContentResultt()
        {
            // Act
            var response = _controller.Delete(personList[1].Id);
            // Assert
            Assert.IsType<NoContentResult>(response as NoContentResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsNotFoundResult()
        {
            // Act
            var response = _controller.Get(999);
            // Assert
            Assert.IsType<NotFoundResult>(response.Result as NotFoundResult);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsPerson()
        {
            // Act
            Person p = new Person() { Id = 0, Name = "Margie", LastName = "Braidy", Email = "margieBraidy@gmail.com", Phone = "123456789", Address = "City Lorem ipsum, 3", DateOfBirth = new DateTime(1991, 8, 14), DateOfInterview = new DateTime(2022, 11, 14), OfferJob = true, Remarks = "Lorem ipsum dolor sit." };

            // Act
            var responseCreatedAtActionResult = _controller.Add(p);

            // Assert
            Assert.IsType<CreatedAtActionResult>(responseCreatedAtActionResult);

            var response = _controller.Get(p.Id);

            // Assert
            Assert.IsType<Person>(response.Value);

            Assert.Equal(response.Value.Id, p.Id);
            Assert.Equal(response.Value.Name, p.Name);
            Assert.Equal(response.Value.LastName, p.LastName);
            Assert.Equal(response.Value.Email, p.Email);
            Assert.Equal(response.Value.Phone, p.Phone);
            Assert.Equal(response.Value.Address, p.Address);
            Assert.Equal(response.Value.DateOfBirth, p.DateOfBirth);
            Assert.Equal(response.Value.OfferJob, p.OfferJob);
            Assert.Equal(response.Value.Remarks, p.Remarks);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllPersons()
        {
            // Act
            var response = _controller.GetAll();

            // Assert
            Assert.IsType<List<Person>>(response.Value);

            Assert.Equal(response.Value.Count > 0, true);
        }
    }
}
