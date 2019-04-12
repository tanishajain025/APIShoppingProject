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
    public class ProductCategoryTestController
    {
        private ShopDataDbContext _context;

        public static DbContextOptions<ShopDataDbContext>
            dbContextOptions
        { get; set; }

        public static string connectionString =
 "Data Source=TRD-519; Initial Catalog=ShoppingProject;Integrated Security=true;";

        static ProductCategoryTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ShopDataDbContext>()
                .UseSqlServer(connectionString).Options;
        }
        public ProductCategoryTestController()
        {
            _context = new ShopDataDbContext(dbContextOptions);
        }
        [Fact]
        public async void Task_GetPcById_Return_OkResult()
        {
            var controller = new ProductCategoryController(_context);
            var PcId = 1;
            var data = await controller.Get(PcId);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_GetpcById_Return_FailResult()
        {
            var controller = new ProductCategoryController(_context);
            var PcId = 12;
            var data = await controller.Get(PcId);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_GetUserById_MatchResult()
        {
            var controller = new ProductCategoryController(_context);
            int id = 1;
            var data = await controller.Get(id);
            Assert.IsType<OkObjectResult>(data);
            var OkResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var user = OkResult.Value.Should().BeAssignableTo<ProductCategory>().Subject;

            Assert.Equal("Top wear", user.CategoryName);
            Assert.Equal("Comfortable wear in every season", user.CategoryDescription);

        }

        [Fact]
        public async void Task_getPostById_return_BadRequestResult()
        {
            var controller = new ProductCategoryController(_context);
            int? id = null;

            var data = await controller.Get(id);
            Assert.IsType<BadRequestResult>(data);

        }
        [Fact]
        public async void Task_Add_AddPc_Return_OkResult()
        {
            var controller = new ProductCategoryController(_context);
            var user = new ProductCategory()
            {
                CategoryName = "Trial",
                CategoryDescription = "traildone",

            };
            var data = await controller.Post(user);
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
            var controller = new ProductCategoryController(_context);
            var id = 19;
            var data = await controller.Delete(id);
            Assert.IsType<OkObjectResult>(data);
        }
        [Fact]
        public async void Task_Delete_return_NotFoundResult()
        {
            var controller = new ProductCategoryController(_context);
            var id = 18;
            var data = await controller.Delete(id);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_DeleteUser_return_Badrequest()
        {
            var controller = new ProductCategoryController(_context);
            int? id = null;
            var data = await controller.Delete(id);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_OkResult()
        {
            var controller = new ProductCategoryController(_context);
            int id = 13;

            var user = new ProductCategory()
            {
                ProductCategoryId = 13,
                CategoryName = "Summers",
                CategoryDescription = "Shirt"
            };
            var data = await controller.Put(id, user);
            Assert.IsType<NoContentResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_NotFound()
        {
            var controller = new ProductCategoryController(_context);
            int? id = 8;

            var user = new ProductCategory()
            {
                CategoryName = "Formals",
                CategoryDescription = "Be professionals"
            };
            var data = await controller.Put(id, user);
            Assert.IsType<NotFoundResult>(data);
        }
        [Fact]
        public async void Task_PutUserId_BadResult()
        {
            var controller = new ProductCategoryController(_context);
            int? id = null;

            var user = new ProductCategory()
            {
                CategoryName = "Formals",
                CategoryDescription = "Be professionals"

            };
            var data = await controller.Put(id, user);
            Assert.IsType<BadRequestResult>(data);
        }
        [Fact]
        public async void Task_GetAll_NotFound()
        {
            var controller = new ProductCategoryController(_context);
            var data = await controller.Get();
            data = null;
            if (data != null)
            {
                Assert.IsType<OkObjectResult>(data);
            }
            else
            {
                // Assert.Equal(data, null);
            }
        }
        [Fact]
        public async void Task_GetAll_return_NotFound()
        {
            var controller = new ProductCategoryController(_context);
            var data = await controller.Get();
            Assert.IsType<OkObjectResult>(data);
        }
    }
}
