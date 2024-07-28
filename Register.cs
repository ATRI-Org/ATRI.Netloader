using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace netloader
{
    public static class Register
    {
        public delegate void Invoke_();
        [DllImport("EndStoneDotNetLoader.dll")] 
        private static extern IntPtr RegisterPluginVoid(
        byte[] describe,byte[] version,byte[] pluginname,byte[] website ,byte[] emil,byte[] author,Invoke_ loadInvoke,Invoke_ enabInvoke,Invoke_ disableInvoke);
        public static IntPtr Build(Invoke_ loadInvoke, Invoke_ enabInvoke, Invoke_ disableInvoke, string describe,string version, string name,string website,string emil,string author)
        {
           return RegisterPluginVoid(GetBytes(describe),GetBytes(version),GetBytes(name),GetBytes(website),GetBytes(emil),GetBytes(author),loadInvoke,enabInvoke,disableInvoke);
        }
        public static byte[] GetBytes(this string in_string)
        {
            return Encoding.UTF8.GetBytes(in_string);
        }
        
    }
}
