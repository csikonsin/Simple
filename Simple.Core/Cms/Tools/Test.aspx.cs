using Simple.Core.Code.ModuleParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Simple.Core.Cms.Tools
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("HALLLO");


            var work = new Data.UnitOfWork();


            var module1 = work.ModuleRepository.GetById(1);

            var article1 = new Domain.Article()
            {
                Text = "Hallo World!",
                CreatedAt = DateTime.Now,
                CreatedById = 1,
                Heading = "Test Heading"
            };

            article1.Id = work.ArticleRepository.Save(article1);

            var par1 = new ArticleParameter()
            {
                ArticleId = article1.Id
            };

            module1.Parameter = Code.ModuleParameters.ParameterBuilder.Serialize(par1);
            module1.Id = work.ModuleRepository.Save(module1);


            var module2 = work.ModuleRepository.GetById(2);

            var article2 = new Domain.Article()
            {
                Text = "Zweiter Artikel!",
                Heading = "Zweiter",
                CreatedById = 1,
                CreatedAt = DateTime.Now
            };
            article2.Id = work.ArticleRepository.Save(article2);

            var par2 = new ArticleParameter()
            {
                ArticleId = article2.Id
            };

            module2.Parameter = Code.ModuleParameters.ParameterBuilder.Serialize(par2);
            module2.Id = work.ModuleRepository.Save(module2);

        }
    }
}