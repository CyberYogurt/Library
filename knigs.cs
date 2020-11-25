using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblieoteka
{
    public class knigs
    {//78765
        public int id_knigi { get; set; }
        public int id_bbk { get; set; }
        public int inv_num { get; set; }
        public string knig_name { get; set; }
        public string author_name { get; set; }
        public string author_sign { get; set; }
        public DateTime god_izd { get; set; }
        public int kol_vo { get; set; }
        public bool krae { get; set; }
        public bool uchebnik { get; set; }
        public bool v_nal { get; set; }

    }
}
