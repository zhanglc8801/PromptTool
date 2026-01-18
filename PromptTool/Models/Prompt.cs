using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptTool.Models
{
    [SugarTable("Prompts")]
    public class Prompt
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [SugarColumn(Length = 100, DefaultValue = "")]
        public string ImageName { get; set; }

        public ImageOrientation imageOrientation { get; set; }

        [SugarColumn(DefaultValue = "")]
        public string PromptStr { get; set; }

        [SugarColumn(DefaultValue = "")]
        public string Note { get; set; }
    }
    public enum ImageOrientation
    {
        Landscape, // 横屏
        Portrait   // 竖屏
    }
}
