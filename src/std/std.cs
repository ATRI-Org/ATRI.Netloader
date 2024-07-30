using netloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class std
    {
        [DllImport("EndStoneDotNetLoader.dll")]
        private static extern void cout(byte[] text);
        public static void cout<T>(T put_in)
        {
           cout(put_in.ToString().GetBytes());
        }
    }
}
