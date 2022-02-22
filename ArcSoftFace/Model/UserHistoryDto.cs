using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.Model
{
    public class UserHistoryDto
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Iphone { get; set; }
        public DateTime HistoryLoginTime { get; set; }
    }
}
