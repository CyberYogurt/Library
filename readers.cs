using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblieoteka
{
    public class readers
    {
        public int id_reader { get; set; }
        public string first_n { get; set; }
        public string last_n { get; set; }
        public int class_num { get; set; }
        public string class_let { get; set; }
        public int inv_num { get; set; }
        public DateTime birth_date { get; set; }
        public DateTime reg_date { get; set; }
        public string liv_add { get; set; }
        public bool other { get; set; }
        public bool teacher { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        
    }
}
