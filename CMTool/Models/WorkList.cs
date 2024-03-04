using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Models
{
    public class WorkList
    {
        [Display(Name = "项目")]
        public string Work { get; set; }
        [Display(Name = "星期一")]
        public string Monday { get; set; }
        [Display(Name = "星期二")]
        public string Tuesday { get; set; }
        [Display(Name = "星期三")]
        public string Wednesday { get;set; }
        [Display(Name = "星期四")]
        public string Thursday { get; set; }
        [Display(Name = "星期五")]
        public string Friday { get; set; }
        [Display(Name = "星期六")]
        public string Saturday { get; set; }
        [Display(Name = "星期日")]
        public string Sunday { get; set;}
    }
}
