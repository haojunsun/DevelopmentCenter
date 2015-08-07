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
    public class CarouselController : ControllerHelper
    {
        private readonly ItapppicService _tapppicService;
        private readonly IHelperServices _helperServices;

        public CarouselController(ItapppicService tapppicService, IHelperServices helperServices)
        {
            _tapppicService = tapppicService;
            _helperServices = helperServices;
        }

        public ActionResult List(int page = 1, int size = 20)
        {
            int pageIndex = page;
            int pageSize = size;
            int totalCount = 0;
            var persons = _tapppicService.List(pageIndex, pageSize, ref totalCount);
            var list = new List<CarouselViewModel>();
            foreach (var p in persons)
            {
                var uvm = new CarouselViewModel();
                uvm.id = p.xh;
                uvm.pic = p.pic;
                uvm.px = p.px;

                list.Add(uvm);
            }
            var personsAsIPagedList = new StaticPagedList<CarouselViewModel>(list, pageIndex, pageSize, totalCount);
            return View(personsAsIPagedList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(int num)
        {
            var appic = new t_apppic();
            appic.px = num.ToString();
            if (Request.Files.Count > 0)
            {
                appic.pic = _helperServices.UpLoadImg("file", ""); //获取上传图片 
                if (string.IsNullOrEmpty(appic.pic))
                    return JumpUrl("Create", "图片不能为空");
            }
            _tapppicService.Add(appic);
            return JumpUrl("List", "套餐创建完成");
        }

        public ActionResult Delete(int id)
        {
            var old = _tapppicService.GetById(id);
            if (old == null)
                return JumpUrl("List", "id错误");
            _tapppicService.Delete(id);
            return JumpUrl("List", "删除成功");
        }
    }
}