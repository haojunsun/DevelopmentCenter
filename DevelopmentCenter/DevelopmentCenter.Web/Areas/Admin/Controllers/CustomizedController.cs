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
    public class CustomizedController : ControllerHelper
    {
           private readonly ItdzlyService _tdzlyService;

           public CustomizedController(ItdzlyService tdzlyService)
           {
               _tdzlyService = tdzlyService;
           }

           public ActionResult List(int page = 1, int size = 20)
           {
               var pageIndex = page;
               var pageSize = size;
               var totalCount = 0;
               var yjfxs = _tdzlyService.List(pageIndex, pageSize, ref totalCount);
               var list = new List<CustomizedViewModel>();
               foreach (var p in yjfxs)
               {
                   var avm = new CustomizedViewModel();
                   avm.id = p.xh;
                   avm.name = p.C01;

                   avm.Sort = p.Sort;
                   avm.Date_tc = p.Date_tc;
                   avm.Date_cl = p.Date_cl;
                   avm.Text_cl = p.Text_cl;
                   avm.Op_cl = "Admin";

                   avm.Yq_mdd = p.Yq_mdd;
                   avm.Yq_sj = p.Yq_sj;
                   avm.Yq_fy = p.Yq_fy;
                   avm.Yq_qt = p.Yq_qt;
                
                   list.Add(avm);
               }
               var personsAsIPagedList = new StaticPagedList<CustomizedViewModel>(list, pageIndex, pageSize, totalCount);
               return View(personsAsIPagedList);
           }

           public ActionResult Delete(int id)
           {
               var old = _tdzlyService.GetById(id);
               if (old == null)
                   return JumpUrl("List", "id错误");
               _tdzlyService.Delete(id);
               return JumpUrl("List", "删除成功");
           }

           [HttpGet]
           public ActionResult Edit(int id = 0)
           {
               var p = _tdzlyService.GetById(id);
               var avm = new CustomizedViewModel();
               avm.id = p.xh;
               avm.name = p.C01;

               avm.Sort = p.Sort;
               avm.Date_tc = p.Date_tc;
               avm.Date_cl = p.Date_cl;
               avm.Text_cl = p.Text_cl;
               avm.Op_cl = "Admin";

               avm.Yq_mdd = p.Yq_mdd;
               avm.Yq_sj = p.Yq_sj;
               avm.Yq_fy = p.Yq_fy;
               avm.Yq_qt = p.Yq_qt;
               return View(avm);
           }

           [HttpPost]
           public ActionResult Edit(CustomizedViewModel cvm)
           {
               var p = _tdzlyService.GetById(cvm.id);
               p.Date_cl = DateTime.Now;
               p.Sort = "已经处理";
               p.Text_cl = cvm.Text_cl;
               p.Op_cl = "Admin";
               _tdzlyService.Update(p);
               return JumpUrl("List", "处理成功");
           }
    }
}