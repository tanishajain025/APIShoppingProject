using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Controllers;
using ProjectAPI.Models;
using System;
using Xunit;

namespace ProjectAPI.Tests
{
    public class VendorTestController
    {
        private ShopDataDbContext _context;

        public static DbContextOptions<ShopDataDbContext>
            dbContextOptions
        { get; set; }

        public static string connectionString =
 "Data Source=TRD-519; Initial Catalog=ShoppingProject;Integrated Security=true;";

        static VendorTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                .UseSqlServer(connectionString).Options;
        }
        public VendorTestController()
        {
            _context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]
        public async void Task_GetVendorById_Return_OkResult()
        {
            var controller = new VendorController(_context);
            var VendorId = 1;
            var data = await controller.Get(VendorId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetVendorById_Return_FailResult()
        {
            var controller = new VendorController(_context);
            var VendorId = 20;
            var data = await controller.Get(VendorId);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_GetUserById_MatchResult()
        {
            var controller = new VendorController(_context);
            int id = 1;
            var data = await controller.Get(id);
            Assert.IsType<OkObjectResult>(data);
            var OkResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var user = OkResult.Value.Should().BeAssignableTo<Vendor>().Subject;
            Assert.Equal("tanisha", user.VendorName);
            Assert.Equal("jain@gmail.com", user.EmailId);
            Assert.Equal(9716061230, user.PhoneNo);
            Assert.Equal("good", user.VendorDescription);
        }

        [Fact]
        public async void Task_getPostById_return_BadRequestResult()
        {
            var controller = new VendorController(_context);
            int? id = null;

            var data = await controller.Get(id);
            Assert.IsType<BadRequestResult>(data);

        }
        [Fact]
        public async void Task_Add_AddUser_Return_OkResult()
        {
            var controller = new VendorController(_context);
            var user = new Vendor()
            {
                VendorName = "Trial",
                EmailId = "jain@gmail.com",
                PhoneNo = 1234569870,
                VendorDescription = "great"
            };
            var data = await controller.Post(user);
            Assert.IsType<CreatedAtActionResult>(data);
        }
        [Fact]
        public async void Task_Add_InvalidAddUser_Return_OkBadResult()
        {
            var controller = new VendorController(_context);
            var user = new Vendor()
            {
                VendorName = "Tanisha works at Niit",
                EmailId = "jain@gmail.com",
                PhoneNo = 1234569870,
                VendorDescription = "great"
            };
            var data = await controller.Post(user);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_DeleteVendor_return_OkResult()
        {
            var controller = new VendorController(_context);
            var id = 16;
            var data = await controller.Delete(id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_Delete_return_NotFoundResult()
        {
            var controller = new VendorController(_context);
            var id = 14;
            var data = await controller.Delete(id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_DeleteVendor_return_Badrequest()
        {
            var controller = new VendorController(_context);
            int? id = null;
            var data = await controller.Delete(id);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_OkResult()
        {
            var controller = new VendorController(_context);
            int id = 6;

            var user = new Vendor()
            {
               VendorId=6,
               VendorName="Trial",
               EmailId = "jains@gmail.com",
               PhoneNo = 1234569870,
               VendorDescription = "great"
            };
            var data = await controller.Put(id, user);
            Assert.IsType<NoContentResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_NotFound()
        {
            var controller = new VendorController(_context);
            int? id = 13;

            var user = new Vendor()
            {
               VendorId = 2,
                VendorName = "Naman",
                EmailId = "naman@gmail.com",
                PhoneNo = 1234567890,
                VendorDescription = "good"
            };
            var data = await controller.Put(id, user);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_BadResult()
        {
            var controller = new VendorController(_context);
            int? id = null;

            var user = new Vendor()
            {

                VendorName = "Sambhav",
                VendorDescription = "abc"
            };
            var data = await controller.Put(id, user);
            Assert.IsType<BadRequestResult>(data);
        }
    }
}