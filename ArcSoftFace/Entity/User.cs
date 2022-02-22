using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcSoftFace.Entity
{
    public class User
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Sex { get; set; }
        public int Age { get; set; }
        public string ImagePath { get; set; }
        public int LoginTimes { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool State { get; set; }
        public string Iphone { get; set; }

    }
}
