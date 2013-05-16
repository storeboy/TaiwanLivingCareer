using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaiwanLivingCareer.Web.ViewModels.Franchisee;

namespace TaiwanLivingCareer.Web.Controllers.Franchisee
{
    public class FranchiseeController : Controller
    {
        /// <summary>
        /// 安置關係組織圖
        /// </summary>
        /// <returns></returns>
        public  ActionResult SettleOrgQuery()
        {
            ViewBag.Title = "安置關係組織圖";
            return View();
        }

        /// <summary>
        /// 安置關係圖-資料來源
        /// </summary>
        /// <returns></returns>
        public JsonResult SettleOrgSource()
        {
            List<SettleOrgModel> source = new List<SettleOrgModel>();


            source.Add(new SettleOrgModel { ID = "a1", ParentID = "", Name = "布魯斯", OrgLevel = 1, Orders = 10, IsVip = true, RecommendName="CC1" });
            source.Add(new SettleOrgModel { ID = "a11", ParentID = "a1", Name = "布魯斯1", OrgLevel = 2, Orders = 10, IsVip = true, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a12", ParentID = "a1", Name = "布魯斯2", OrgLevel = 3, Orders = 101, IsVip = false, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a111", ParentID = "a11", Name = "布魯斯11", OrgLevel = 2, Orders = 130, IsVip = true, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a112", ParentID = "a11", Name = "布魯斯12", OrgLevel = 3, Orders = 10, IsVip = true, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a121", ParentID = "a12", Name = "布魯斯21", OrgLevel = 1, Orders = 10, IsVip = false, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1211", ParentID = "a121", Name = "布魯斯211", OrgLevel = 1, Orders = 10, IsVip = true, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1212", ParentID = "a121", Name = "布魯斯212", OrgLevel = 3, Orders = 10, IsVip = true, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1213", ParentID = "a121", Name = "布魯斯213", OrgLevel = 1, Orders = 10, IsVip = false, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1214", ParentID = "a121", Name = "布魯斯214", OrgLevel = 1, Orders = 10, IsVip = true, RecommendName = "CC1" });

            source.Add(new SettleOrgModel { ID = "a1213a", ParentID = "a1213", Name = "布魯斯213", OrgLevel = 1, Orders = 10, IsVip = false, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1214a", ParentID = "a1213", Name = "布魯斯214", OrgLevel = 1, Orders = 10, IsVip = true, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1213b", ParentID = "a1213a", Name = "布魯斯213", OrgLevel = 1, Orders = 10, IsVip = false, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1214b", ParentID = "a1213a", Name = "布魯斯214", OrgLevel = 1, Orders = 10, IsVip = true, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1213c", ParentID = "a1213b", Name = "布魯斯213", OrgLevel = 1, Orders = 10, IsVip = false, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1214c", ParentID = "a1213b", Name = "布魯斯214", OrgLevel = 1, Orders = 10, IsVip = true, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1213d", ParentID = "a1213c", Name = "布魯斯213", OrgLevel = 1, Orders = 10, IsVip = false, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1214d", ParentID = "a1213c", Name = "布魯斯214", OrgLevel = 1, Orders = 10, IsVip = true, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1213e", ParentID = "a1214d", Name = "布魯斯213", OrgLevel = 1, Orders = 10, IsVip = false, RecommendName = "CC1" });
            source.Add(new SettleOrgModel { ID = "a1214e", ParentID = "a1214d", Name = "布魯斯214", OrgLevel = 1, Orders = 10, IsVip = true, RecommendName = "CC1" });

            JsonResult jr = new JsonResult();
            jr.Data = source;
            jr.ContentType = "application/json";
            jr.ContentEncoding = System.Text.Encoding.Default;
            jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return jr;
        }

        /// <summary>
        /// 推薦關係組織圖
        /// </summary>
        /// <returns></returns>
        public ActionResult RecommendOrgQuery()
        {
            return View();
        }
    }
}
