using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaiwanLivingCareer.Web.ViewModels.Franchisee
{
    /// <summary>
    /// 安置關係圖model
    /// </summary>
    public class SettleOrgModel
    {
        /// <summary>
        /// 本身ID
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 父階ID
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 推薦人姓名
        /// </summary>
        public string RecommendName { get; set; }
        /// <summary>
        /// 單數
        /// </summary>
        public int Orders { get; set; }
        /// <summary>
        /// 是不是VIP
        /// </summary>
        public bool IsVip { get; set; }
        /// <summary>
        /// 職階等第
        /// </summary>
        public int OrgLevel { get; set; }
        
    }
}