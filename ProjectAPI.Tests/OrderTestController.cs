using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectAPI.Controllers;
using ProjectAPI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProjectAPI.Tests
{
    public class OrderTestController
    {
        private ShopDataDbContext _context;

        public static DbContextOptions<ShopDataDbContext>
            dbContextOptions
        { get; set; }

        public static string connectionString =
 "Data Source=TRD-519; Initial Catalog=ShoppingProject;Integrated Security=true;";
        static OrderTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                .UseSqlServer(connectionString).Options;
        }
        public OrderTestController()
        {
            _context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]
        public async void Task_GetPcById_Return_OkResult()
        {
            var controller = new OrdersController(_context);
            var PcId = 55;
            var data = await controller.GetOrder(PcId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetpcById_Return_FailResult()
        {
            var controller = new OrdersController(_context);
            var PcId = 12;
            var data = await controller.GetOrder(PcId);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_GetUserById_MatchResult()
        {
            var controller = new OrdersController(_context);
            int id = 40;
            var data = await controller.GetOrder(id);
            Assert.IsType<OkObjectResult>(data);
            var OkResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var user = OkResult.Value.Should().BeAssignableTo<Order>().Subject;

            Assert.Equal(6501, user.OrderPrice);
           // Assert.Equal("'2019 - 04 - 03 13:01:41.6851845'", user.OrderDate);
            Assert.Equal(45,user.CustomerId);

        }
        [Fact]
        public async void Task_getPostById_return_BadRequestResult()
        {
            var controller = new OrdersController(_context);
            int? id = null;

            var data = await controller.GetOrder(id);
            Assert.IsType<BadRequestResult>(data);
         }
        [Fact]
        public async void Task_Add_AddPc_Return_OkResult()
        {
            var controller = new OrdersController(_context);
            var user = new Order()
            {
               // OrderDate = "'2019-04-03 13:01:41.6851845'",
                OrderPrice = 6501,
                CustomerId =45

            };
            var data = await controller.PostOrder(user);
            Assert.IsType<CreatedAtActionResult>(data);
        }
        //[Fact]
        //public async void Task_Add_InvalidAddPC_Return_BadResult()
        //{
        //    var controller = new ProductCategoryController(_context);
        //    var user = new ProductCategory()
        //    {
        //        CategoryName = "Sum",
        //        CategoryDescription = "Shirts"

        //    };
        //    var data = await controller.Post(user);
        //    Assert.IsType<BadRequestResult>(data);
        //}
        [Fact]
        public async void Task_DeleteUser_return_OkResult()
        {
            var controller = new OrdersController(_context);
            var id = 47;
            var data = await controller.DeleteOrder(id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_Delete_return_NotFoundResult()
        {
            var controller = new OrdersController(_context);
            var id = 100;
            var data = await controller.DeleteOrder(id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_DeleteUser_return_Badrequest()
        {
            var controller = new OrdersController(_context);
            int? id = null;
            var data = await controller.DeleteOrder(id);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_OkResult()
        {
            var controller = new OrdersController(_context);
            int id = 13;

            var user = new Order()
            {
                OrderId =40,
               // OrderDate =,
                OrderPrice =6501,
                CustomerId =45
            };
            var data = await controller.PutOrder(id, user);
            Assert.IsType<NoContentResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_NotFound()
        {
            var controller = new OrdersController(_context);
            int? id =40;

            var user = new Order()
            {
                OrderId = 40,
                //OrderDate =,
                OrderPrice = 6501,
                CustomerId = 45
            };
            var data = await controller.PutOrder(id, user);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_BadResult()
        {
            var controller = new OrdersController(_context);
            int? id = null;

            var user = new Order()
            {
                OrderId = 40,
                //OrderDate =,
                OrderPrice = 6501,
                CustomerId = 45

            };
            var data = await controller.PutOrder(id, user);
            Assert.IsType<BadRequestResult>(data);
        }
        //[Fact]
        //public async void Task_GetAll_NotFound()
        //{
        //    var controller = new OrdersController(_context);
        //    var data = await controller.GetOrder();
        //    data = null;
        //    if (data != null)
        //    {
        //        Assert.IsType<OkObjectResult>(data);
        //    }
        //    else
        //    {
        //        // Assert.Equal(data, null);
        //    }
        //}
        //[Fact]
        //public async void Task_GetAll_return_NotFound()
        //{
        //    var controller = new OrdersController(_context);
        //    var data = await controller.GetOrder();
        //    Assert.IsType<OkObjectResult>(data);
        //}
    }
}
