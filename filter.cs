using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblieoteka
{
    public class filterK
    {
        public string name { get; set; }
        public bool bbk { get; set; }
        public string bbk_name { get; set; }
        public bool krae { get; set; }
        public bool uchebnik { get; set; }
    }
    public class filterR
    {
        public string last_name { get; set; }
        public decimal num { get; set; }
        public bool other { get; set; }
        public bool teacher { get; set; }
    }
}
