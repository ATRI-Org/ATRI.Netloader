using netloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace stdlib.src.endstone
{
    public class logger
    {
        private static IntPtr ptr_ = IntPtr.Zero;
        
        [DllImport("EndStoneDotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getLogger(IntPtr plugin);

        [DllImport("EndStoneDotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void logger_info(IntPtr logger, byte[] text);

        [DllImport("EndStoneDotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void logger_error(IntPtr logger,string text);

        [DllImport("EndStoneDotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void logger_warn(IntPtr logger, byte[] text);

        [DllImport("EndStoneDotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void logger_trace(IntPtr logger, byte[] text);

        [DllImport("EndStoneDotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void logger_critical(IntPtr logger, byte[] text);

        [DllImport("EndStoneDotNetLoader.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void logger_debug(IntPtr logger, byte[] text);

        public unsafe logger(IntPtr ptr)
        {
            ptr_ = getLogger(ptr);
        }

        public void Info<T>(T text)
        {
            logger_info(ptr_,text.ToString().GetBytes());
        }

        public void Warn<T>(T text)
        {
            logger_warn(ptr_,text.ToString().GetBytes());
        }

        public void Error<T>(T text)
        {
            char[] chs = text.ToString().ToCharArray();
            logger_error(ptr_,text.ToString());
        }

        public void Trace<T>(T text)
        {
            logger_trace(ptr_,text.ToString().GetBytes());
        }

        public void Debug<T>(T text)
        {
            logger_debug(ptr_,text.ToString().GetBytes());
        }

        public void Critical<T>(T text)
        {
            logger_critical(ptr_,text.ToString().GetBytes());
        }

    }
}
