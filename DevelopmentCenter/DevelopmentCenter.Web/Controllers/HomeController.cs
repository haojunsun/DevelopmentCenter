using DevelopmentCenter.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevelopmentCenter.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;
        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        //首页
        public ActionResult Index()
        {
            ViewBag.lunbotu = _articleService.List().Where(x => x.ColumnTags == "轮播图").OrderByDescending(x => x.CreatedUtc).Take(5).ToList();
            ViewBag.tzgonggao = _articleService.List().Where(x => x.ColumnTags == "通知公告").OrderByDescending(x => x.CreatedUtc).Take(8).ToList();
            ViewBag.zxlingdao = _articleService.List().Where(x => x.ColumnTags == "中心领导").OrderByDescending(x => x.CreatedUtc).Take(5).ToList();
            ViewBag.jgshezhi = _articleService.List().Where(x => x.ColumnTags == "机构设置").OrderByDescending(x => x.CreatedUtc).Take(5).ToList();
            ViewBag.zxtongxun = _articleService.List().Where(x => x.ColumnTags == "中心快讯").OrderByDescending(x => x.CreatedUtc).Take(8).ToList();
            ViewBag.xwbolan = _articleService.List().Where(x => x.ColumnTags == "新闻博览").OrderByDescending(x => x.CreatedUtc).Take(8).ToList();
            ViewBag.yqlianjie = _articleService.List().Where(x => x.ColumnTags == "友情连接").OrderByDescending(x => x.CreatedUtc).Take(4).ToList();
            return View();
        }

        //单位概括----------------start
        //单位简介
        public ActionResult Dwjj()
        {

            return View();
        }
        //领导简介
        public ActionResult Ldjj()
        {

            return View();
        }
        //机构设置
        public ActionResult Jgsz()
        {

            return View();
        }
        //特约研究员
        public ActionResult Tyyjy()
        {

            return View();
        }
        //研究中心
        public ActionResult Yjzx()
        {

            return View();
        }
        //单位概括----------------end

        //详情页
        public ActionResult Detail(int? id = -1)
        {

            return View();
        }

        //科研规划---------------start
        //科研动态
        public ActionResult Kykydt()
        {

            return View();
        }
        //科研项目
        public ActionResult Kykexm()
        {

            return View();
        }
        //全国艺术科研规划
        public ActionResult Kyqggh()
        {

            return View();
        }
        //全国文化艺术资源标准化技术委员会
        public ActionResult Kyqgwyh()
        {

            return View();
        }
        //科研规划---------------end

        //重大工程--------------start
        //中国民族民间文艺集成志书
        public ActionResult Gczgmzmj()
        {

            return View();
        }
        //中国节日志
        public ActionResult Gcjrz()
        {

            return View();
        }
        //中国史诗百部
        public ActionResult Gcssbb()
        {

            return View();
        }
        //集成志书港澳台
        public ActionResult Gcgat()
        {

            return View();
        }
        //其他
        public ActionResult Gcqt()
        {

            return View();
        }
        //重大工程--------------end

        //文化科技-------------start
        //重点实验
        public ActionResult Whzdsy()
        {

            return View();
        }
        //资源建设
        public ActionResult Whzyjs()
        {

            return View();
        }
        //文化科技-------------end

        //文化交流------------start
        //展览表演
        public ActionResult Whjlzlby()
        {

            return View();
        }
        //大赛
        public ActionResult Whjlds()
        {

            return View();
        }
        //其他
        public ActionResult Whjlqt()
        {

            return View();
        }
        //文化交流------------end

        //专项管理-----------start
        //艺术学工作
        public ActionResult Zxysx()
        {

            return View();
        }
        //节庆管理
        public ActionResult Zxjqgl()
        {

            return View();
        }
        //标准化管理
        public ActionResult Zxbzh()
        {

            return View();
        }
        //专项管理-----------end
    }
}