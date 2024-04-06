using System.ComponentModel.DataAnnotations;

namespace CMTool.Models
{
    public class ClassList
    {
        [Display(Name = "节数")]
        public int ClassNum { get; set; }

        [Display(Name = "星期一")]
        public string Monday { get; set; }
        [Display(Name = "星期二")]
        public string Tuesday { get; set; }
        [Display(Name = "星期三")]
        public string Wednesday { get; set; }
        [Display(Name = "星期四")]
        public string Thursday { get; set; }
        [Display(Name = "星期五")]
        public string Friday { get; set; }
        [Display(Name = "星期六")]
        public string Saturday { get; set; }
        [Display(Name = "星期日")]
        public string Sunday { get; set; }
    }
}
