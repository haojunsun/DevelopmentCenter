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
using DevelopmentCenter.Web.Models;

namespace DevelopmentCenter.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ActivityController : ControllerHelper
    {
        private readonly IthdService _thdService;
        private readonly IthdbmService _thdbmService;


        public ActivityController(IthdService thdService, IthdbmService thdbmService)
        {
            _thdService = thdService;
            _thdbmService = thdbmService;
        }

        // GET: Admin/Activity
        public ActionResult List(int page = 1, int size = 20)
        {
            var pageIndex = page;
            var pageSize = size;
            var totalCount = 0;
            var yjfxs = _thdService.List(pageIndex, pageSize, ref totalCount);
            var list = new List<ActivityViewModel>();
            foreach (var p in yjfxs)
            {
                var avm = new ActivityViewModel();
                avm.id = p.xh;
                avm.name = p.C01;

                avm.sort = p.sort;
                avm.title = p.title;
                avm.Nr = p.Nr;
                avm.Jianjie = p.Jianjie;

                if (!string.IsNullOrEmpty(p.Date_start))
                    avm.Date_start = DateTime.Parse(p.Date_start);

                avm.Rs = p.Rs;
                avm.Rs_bm = p.Rs_bm;
                avm.fy = p.fy;

                avm.AddTime = p.Date_fb;
                if (!string.IsNullOrEmpty(p.Date_bmzj))
                    avm.Date_bmzj = DateTime.Parse(p.Date_bmzj);

                list.Add(avm);
            }
            var personsAsIPagedList = new StaticPagedList<ActivityViewModel>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        public ActionResult Delete(int id)
        {
            var old = _thdService.GetById(id);
            if (old == null)
                return JumpUrl("List", "id错误");
            _thdService.Delete(id);
            return JumpUrl("List", "删除成功");
        }

        [HttpGet]
        public ActionResult Detail(int id = 0)
        {
            var p = _thdService.GetById(id);

            var avm = new ActivityViewModel();
            avm.id = p.xh;
            avm.name = p.C01;

            avm.sort = p.sort;
            avm.title = p.title;
            avm.Nr = p.Nr;
            avm.Jianjie = p.Jianjie;

            if (!string.IsNullOrEmpty(p.Date_start))
                avm.Date_start = DateTime.Parse(p.Date_start);

            avm.Rs = p.Rs;
            avm.Rs_bm = p.Rs_bm;
            avm.fy = p.fy;

            avm.AddTime = p.Date_fb;
            if (!string.IsNullOrEmpty(p.Date_bmzj))
                avm.Date_bmzj = DateTime.Parse(p.Date_bmzj);
            avm.Arvs = new List<ActivityRegistrationViewModel>();
            var list = _thdbmService.List(avm.id);
            foreach (var bm in list)
            {
                var arvm = new ActivityRegistrationViewModel();
                arvm.id = bm.xh;
                arvm.ActivityId = bm.Xh_t_hd;
                arvm.username = bm.Code_hy;
                arvm.Xh_hy = bm.Xh_hy;
                arvm.Name = bm.Name;
                arvm.Year = bm.Year;
                arvm.sex = bm.sex;
                arvm.Tel = bm.Tel;
                avm.Arvs.Add(arvm);

            }
            return View(avm);
        }
    }
}