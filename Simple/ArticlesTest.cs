using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Simple.Domain;
using Simple.Core.Presenter;
using Simple.Services;
using Simple.Core.Parameters;

namespace Simple
{
    [TestClass]
    public class ArticlesPresenterTest
    {
        [TestMethod]
        public void LoadArticles()
        {
            var mockService = new Mock<IArticleService>();

            var testArticles = new List<Article>()
            {
                new Article()
                {
                    Id = 1,
                    Text = "Bla",
                    Heading = "Heading",
                    CreatedAt = DateTime.Now,
                    CreatedById = 1
                }
            };

            mockService.Setup(x => x.GetAll()).Returns(testArticles);



            var testParameter = new ArticlesOverviewParameter()
            {
                BackgroundColorHex = "#cccccc"
            };

            var testModule = new Domain.Module()
            {
                Parameter = testParameter.Serialize()
            };


            var fakeView = new FakeArticlesView();
            var presenter = new ArticleOverviewPresenter(fakeView, testModule, mockService.Object);

            presenter.LoadArticles();

            Assert.AreEqual(fakeView.Articles.Count, testArticles.Count);
            Assert.AreEqual(fakeView.BackgroundColorHex, testParameter.BackgroundColorHex);
        }
    }

    public class FakeArticlesView : IArticlesView
    {
        public List<ArticleViewModel> Articles { set; get; }
        public string BackgroundColorHex { set; get; }
    }
}
