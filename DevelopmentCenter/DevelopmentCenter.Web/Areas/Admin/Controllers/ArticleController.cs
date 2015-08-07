using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DevelopmentCenter.Core;
using DevelopmentCenter.Core.Services;
using DevelopmentCenter.Infrastructure.Services;
using DevelopmentCenter.Web.Helpers;
using DevelopmentCenter.Core.Models;

namespace DevelopmentCenter.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ArticleController : ControllerHelper
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="tagtype">0 频道 1 栏目</param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public ActionResult List(string tag, int tagtype, int page = 1, int size = 20)
        {
            var pageIndex = page;
            var pageSize = size;
            var totalCount = 0;


            var list = new List<Article>();

            list = tagtype == 0 ? _articleService.GetByChannelTag(tag, pageIndex, pageSize, ref totalCount).ToList() : _articleService.GetByColumnTag(tag, pageIndex, pageSize, ref totalCount).ToList();
            var personsAsIPagedList = new StaticPagedList<Article>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        public ActionResult Delete(int id)
        {
            var old = _articleService.Get(id);
            if (old == null)
                return JumpUrl("List", "id错误");
            _articleService.Delete(id);
            return JumpUrl("List", "删除成功");
        }

        [HttpGet]
        public ActionResult Detail(int id = 0)
        {
            var article = _articleService.Get(id);
            return View(article);
        }
    }
}