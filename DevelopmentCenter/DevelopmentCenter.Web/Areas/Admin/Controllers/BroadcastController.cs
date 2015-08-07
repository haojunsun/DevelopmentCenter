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
    public class BroadcastController : ControllerHelper
    {
        private readonly ItxzbbService _txzbbService;

        public BroadcastController(ItxzbbService txzbbService)
        {
            _txzbbService = txzbbService;
        }
        public ActionResult List(int page = 1, int size = 20)
        {
            var pageIndex = page;
            var pageSize = size;
            var totalCount = 0;
            var persons = _txzbbService.List(pageIndex, pageSize, ref totalCount);
            var list = new List<XzbbViewModel>();
            foreach (var p in persons)
            {
                var xvm = new XzbbViewModel();
                xvm.id = p.xh;
                xvm.pic = p.pic;

                xvm.name = p.C01;
                xvm.addtime = p.D_pub;
                xvm.title = p.title;
                if (p.yxq != 0)
                {
                    xvm.validtime = xvm.addtime.Value.AddDays(double.Parse(p.yxq.ToString()));
                }
                list.Add(xvm);
            }
            var personsAsIPagedList = new StaticPagedList<XzbbViewModel>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        [HttpGet]
        public ActionResult Detail(int id = 0)
        {
            var p = _txzbbService.GetById(id);

            var xvm = new XzbbViewModel();
            xvm.id = p.xh;
            xvm.pic = p.pic;
            xvm.name = p.C01;
            xvm.addtime = p.D_pub;
            xvm.title = p.title;
            if (p.yxq != 0)
            {
                xvm.validtime = xvm.addtime.Value.AddDays(double.Parse(p.yxq.ToString()));
            }
            xvm.Text1 = p.Text1;
            xvm.Text2 = p.Text2;

            return View(xvm);
        }

        public ActionResult Delete(int id)
        {
            var old = _txzbbService.GetById(id);
            if (old == null)
                return JumpUrl("List", "id错误");
            _txzbbService.Delete(id);
            return JumpUrl("List", "删除成功");
        }
    }
}