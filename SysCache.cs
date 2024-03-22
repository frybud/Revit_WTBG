using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Revit_WTBG
{
    public class SysCache
    {
        private SysCache()
        {

        }
        private static SysCache _Instance;

        public static SysCache Instance
        {
            get
            {
                if (ReferenceEquals(_Instance, null))
                {
                    _Instance = new SysCache();
                }
                return _Instance;
            }

        }
       
    }
}
