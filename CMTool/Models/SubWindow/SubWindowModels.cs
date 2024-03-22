using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMTool.Models.SubWindow
{
    public class SubWindowModels
    {
        private string _NameTable;
        public string NameTable { get { return _NameTable; } set { _NameTable = value; } }

        private string _WorkTable;
        public string WorkTable { get { return _WorkTable; } set { _WorkTable = value; } }
    }
}
