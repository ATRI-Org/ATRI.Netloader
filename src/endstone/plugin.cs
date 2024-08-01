using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using net.r_eg.Conari.Native;
using net.r_eg.Conari;
using net.r_eg.Conari.Types;
using netloader;

namespace stdlib.src.endstone
{
    public class plugin
    {
        public static ConariL il = new ConariL("EndStoneDotNetLoader.dll");
        internal plugin(IntPtr ptr)
        {
            ptr_ = ptr;
        }
        public  IntPtr ptr_ = IntPtr.Zero;
        [DllImport("EndStoneDotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr plugin_getserver(IntPtr plugin);


        [DllImport("EndStoneDotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr plugin_getcommand(IntPtr plugin, byte[] name);

        public server getServer()
        {
            return server.CreatInstance(plugin_getserver(ptr_));
        }

        public  string getName()
        {
            string? s = il.DLR.plugin_getname<WCharPtr>(ptr_);
            return s;
        }

        public unsafe string getDataFile()
        {
            return il.DLR.plugin_getdataflor<WCharPtr>(ptr_);
        }

        public logger getLogger()
        {
            return new logger(ptr_);
        }
    }
}
