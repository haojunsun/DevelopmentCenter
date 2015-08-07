using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Tycoon.Core;
using Tycoon.Core.Services;
using Tycoon.Infrastructure.Services;
using Tycoon.Web.Helpers;
using Tycoon.Web.Models;

namespace Tycoon.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class TravelNotesController : ControllerHelper
    {
        private readonly ItyjfxService _tyjfxService;

        public TravelNotesController(ItyjfxService tyjfxService)
        {
            _tyjfxService = tyjfxService;
        }

        public ActionResult List(int page = 1, int size = 20)
        {
            var pageIndex = page;
            var pageSize = size;
            var totalCount = 0;
            var yjfxs = _tyjfxService.List(pageIndex, pageSize, ref totalCount);
            var list = new List<YjfxViewModel>();
            foreach (var p in yjfxs)
            {
                var yvm = new YjfxViewModel();
                yvm.id = p.xh;
                yvm.name = p.C01;

                yvm.Sort = p.Sort;
                yvm.Title = p.Title;
                yvm.weiboid = p.Id;
                yvm.Key1 = p.Key1;
                yvm.Key2 = p.Key2;
                yvm.Key3 = p.Key3;
                yvm.Key4 = p.Key4;
                yvm.Key5 = p.Key5;
                yvm.Key6 = p.Key6;

                list.Add(yvm);
            }
            var personsAsIPagedList = new StaticPagedList<YjfxViewModel>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        public ActionResult Delete(int id)
        {
            var old = _tyjfxService.GetById(id);
            if (old == null)
                return JumpUrl("List", "id错误");
            _tyjfxService.Delete(id);
            return JumpUrl("List", "删除成功");
        }


        [HttpGet]
        public ActionResult Detail(int id = 0)
        {
            var p = _tyjfxService.GetById(id);

            var yvm = new YjfxViewModel();
            yvm.id = p.xh;
            yvm.name = p.C01;

            yvm.Sort = p.Sort;
            yvm.Title = p.Title;
            yvm.weiboid = p.Id;
            yvm.Key1 = p.Key1;
            yvm.Key2 = p.Key2;
            yvm.Key3 = p.Key3;
            yvm.Key4 = p.Key4;
            yvm.Key5 = p.Key5;
            yvm.Key6 = p.Key6;

            return View(yvm);
        }
    }
}