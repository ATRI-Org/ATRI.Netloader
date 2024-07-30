#pragma warning disable
using Microsoft.Win32;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
namespace Netmain
{
    public static class Pmain
    {

        public static int PluginMain(IntPtr arg, int argLength)
        {
            int length = 0;
            List<IntPtr> nints = new List<IntPtr>();
            Lib();
            if (!Directory.Exists("./mods"))
            {
                Directory.CreateDirectory("./mods");
            }
            string[] plugin_list = Directory.GetFiles("./mods", "*.dll");
            try
            {
                Assembly loadFrom = Assembly.LoadFrom("./plugins/plugins_dotnet/stdlib/stdlib.dll");
                Type? type = loadFrom.GetType("stdlib.RegisterPlugins");
                MethodInfo? methodInfo = type.GetMethod("Run");
                nints = (List<nint>)methodInfo.Invoke(null, new object[]{});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            unsafe
            {
                if (nints.Count == 0)
                {
                    var hh = (Int64*)(arg.ToPointer());
                    *hh = 0;
                    return 0;
                }

                void* ptrVoid = NewArray();
                foreach (IntPtr nint in nints)
                {
                    AddIntoArray(ptrVoid, nint.ToPointer());
                }
                var ptr = (long*)arg;
                *ptr = (long)ptrVoid;
                // GC.Collect();
            }
            return length;
        }
        [DllImport("EndStoneDotNetLoader.dll")]
        public static unsafe extern void* NewArray();
        [DllImport("EndStoneDotNetLoader.dll")]
        public static unsafe extern void AddIntoArray(void* arrayVoid, void* ptrVoid);

        private static void Lib()
        {
            string[] libStrings = Directory.GetFiles("./mods/lib", "*.dll",SearchOption.AllDirectories);
            foreach (var libPlugin in libStrings)
            {
                try
                {
                    Assembly.LoadFrom(libPlugin);
                }
                catch (Exception e)
                {
                    
                    continue;
                }
            }
        }
    }
}
