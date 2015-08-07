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
    public class UserController : ControllerHelper
    {
        private readonly It3Service _userService;
        private readonly IExportService _exportService;

        public UserController(It3Service userService, IExportService exportService)
        {
            _userService = userService;
            _exportService = exportService;
        }

        /// <summary>
        /// 列表
        /// </summary>
        public ActionResult List(string key, int page = 1, int size = 20)
        {
            ViewBag.Search = key;
            int pageIndex = page;
            int pageSize = size;
            int totalCount = 0;
            var persons = _userService.List(key, pageIndex, pageSize, ref totalCount);
            var list = new List<UserViewModel>();
            foreach (var p in persons)
            {
                var uvm = new UserViewModel();
                uvm.Id = p.C01;
                uvm.State = p.C02;
                uvm.Password = p.C03;
                uvm.Name = p.C04;
                uvm.Category = p.C05;
                uvm.tel = p.tel;
                uvm.Adder = p.Adder;
                list.Add(uvm);
            }
            var personsAsIPagedList = new StaticPagedList<UserViewModel>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(string id, string name,
             string password, string tel, string adder, string post, string Weibo1, string Weibo2,
            string bank, string bankzh, string bank1, string bank2, string jektx)
        {
            var user = new t3();
            user.C01 = id;
            if (_userService.GetById(id) != null)
                return JumpUrl("Create", "大亨ID重复创建失败！");

            user.C02 = "正常";
            user.C03 = password;
            user.C04 = name;
            user.C05 = "大亨";
            user.tel = tel;
            user.Adder = adder;
            user.Post = post;
            user.Weibo1 = Weibo1;
            user.Weibo2 = Weibo2;
            user.Bank = bank;
            user.Bank_zh = bankzh;
            user.Bank1 = bank1;
            user.Bank2 = bank2;
            user.Je_ktx = string.IsNullOrEmpty(jektx) ? 0 : decimal.Parse(jektx);

            _userService.Add(user);
            return JumpUrl("List", "大亨创建完成！");
        }


        public ActionResult Export(string key)
        {
            var result = _exportService.ToExcel(_userService.List(key));
            var url = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
            return Redirect(url + "/Uploads/" + result);
        }
    }
}