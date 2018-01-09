using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dsm.Models
{
    public partial class directory
    {
        public List<directory> child { get; set; }
    }
}