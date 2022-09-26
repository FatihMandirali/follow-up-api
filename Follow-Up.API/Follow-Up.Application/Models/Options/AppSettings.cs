using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Models.Options
{
    public class AppSettings
    {
        public ListLimitOptions ListLimitOptions { get; set; }
    }

    public class ListLimitOptions
    {
        public int Limit { get; set; }
    }
}
