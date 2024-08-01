using stdlib.src.endstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stdlib.src
{
    public class server 
    {
        private IntPtr ptr = IntPtr.Zero;
        private server(IntPtr ptr) 
        {
           this.ptr = ptr;
        }

        public static server CreatInstance(IntPtr ptr)
        {
            return new server(ptr);
        }


    }
}
