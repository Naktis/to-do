using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Business.Profiles;
using Todo.Business.Services;
using Todo.Data.Models;
using Xunit;

namespace Todo.Business.Tests
{
    public class CategoryTests
    {
        private readonly Mock<IDataServiceAsync<CategoryDao>> mockService;
        private readonly Mock<DbSet<CategoryDao>> mockSet;
        private readonly Mock<Data.Context.AppContext> mockContext;
        private readonly CategoryVoProfile categoryProfile;
        private readonly MapperConfiguration configMapper;
        private readonly IMapper mapper;
        private readonly InDbCategoryService service;

        public CategoryTests()
        {
            // Service mock setup
            mockService = new Mock<IDataServiceAsync<CategoryDao>>();

            mockService
                .Setup(x => x.GetAll())
                .Returns(Task.FromResult(GetSampleCategories()));

            mockService
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(Task.FromResult(GetSampleCategories().First()));

            // Category DbSet mock initialization
            mockSet = new Mock<DbSet<CategoryDao>>();

            // DbContext mock setup
            mockContext = new Mock<Data.Context.AppContext>();

            mockContext.
                Setup(m => m.Add(It.IsAny<CategoryDao>())).
                Returns(mockSet.Object.Add(It.IsAny<CategoryDao>()));

            categoryProfile = new CategoryVoProfile();
            configMapper = new MapperConfiguration(cfg => cfg.AddProfile(categoryProfile));
            mapper = new Mapper(configMapper);
            service = new InDbCategoryService(mockContext.Object, mapper);
        }

        [Fact]
        public async void GetAll_Valid()
        {
            // Act
            List<CategoryDao> actual = await mockService.Object.GetAll();
            List<CategoryDao> expected = GetSampleCategories();

            // Assert
            Assert.True(actual != null);
            Assert.Equal(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i ++)
            {
                Assert.True(AreCategoriesEqual(actual[i], expected[i]));
            }
        }

        [Fact]
        public async void Get_Valid()
        {
            // Act
            CategoryDao actual = await mockService.Object.Get(1);
            CategoryDao expected = GetSampleCategories().First();

            // Assert
            Assert.True(actual != null);
            Assert.True(AreCategoriesEqual(expected, actual));
        }

        [Fact]
        public async void Add_Valid()
        {
            // Act
            await service.Add(new CategoryVo { Name = "CreatedName1" });

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<CategoryDao>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
        }

        private List<CategoryDao> GetSampleCategories()
        {
            List<CategoryDao> output = new List<CategoryDao>
            {
                new CategoryDao{Name="Name1"},
                new CategoryDao{Name="Name2"},
                new CategoryDao{Name="Name3"}
            };
            return output;
        }

        private bool AreCategoriesEqual (CategoryDao first, CategoryDao second)
        {
            return (first.ID == second.ID && first.Name == second.Name);
        }
    }
}
