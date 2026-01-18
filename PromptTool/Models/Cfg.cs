using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromptTool.Models
{
    [SugarTable("Cfg")]
    public class Cfg
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        [SugarColumn(Length = 100)]
        public string Key { get; set; }
        [SugarColumn(Length = 500)]
        public string Value { get; set; }
    }
}
