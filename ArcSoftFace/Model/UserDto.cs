using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.Model
{
     public class UserDto
    {
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        //public string GuidPath { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public int LoginTimes { get; set; }
        public string Iphone { get; set; }
        public float AmountOfMoney { get; set; }
    }
}
