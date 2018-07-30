using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using Shop.Service;

namespace Shop.UnitTest.ServicesTest
{
    [TestClass]
    public class PostServiceTest
    {
        private Mock<IPostRepository> _mockPostRepository;
        private Mock<IPostCategoryRepository> _mockCategoryPostRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostService _postService;
        private List<Post> _listPost;
        private IPostCategoryService _categoryService;
        private List<PostCategory> _listCategory;

        [TestInitialize]
        public void Initialize()
        {
            _mockPostRepository = new Mock<IPostRepository>();
            _mockCategoryPostRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _categoryService = new PostCategoryService(_mockCategoryPostRepository.Object, _mockUnitOfWork.Object);
            _listCategory = new List<PostCategory>()
            {
                new PostCategory() {ID =1 ,Name="DM1",Status=true },
                new PostCategory() {ID =2 ,Name="DM2",Status=true },
                new PostCategory() {ID =3 ,Name="DM3",Status=true },
            };
            _postService = new PostService(_mockPostRepository.Object, _mockUnitOfWork.Object);
            _listPost = new List<Post>()
            {
                new Post()
                {
                    ID=1, Name="Post1", Alias="Alias1", CategoryID =2, Status=true
                }
            };
        }
        [TestMethod]
        public void PostService_GetAll()
        {
            ////setup method
            //_mockCategoryPostRepository.Setup(m => m.GetAll(null)).Returns(_listCategory);
            ////call action
            //var resultC = _categoryService.GetAll() as List<PostCategory>;

            _mockPostRepository.Setup(m => m.GetAll(null)).Returns(_listPost);
            //call action
            var resultP =_postService.GetAll() as List<Post>;
        }
        [TestMethod]
        public void PostService_Create()
        {
        }
    }
}
