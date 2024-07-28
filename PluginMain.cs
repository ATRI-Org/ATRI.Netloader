using netloader;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
namespace Netmain
{
    public static  class Pmain
    {
        public static int PluginMain(IntPtr arg, int argLength)
        {
           
            Loadstdlibs();
            if (!Directory.Exists("./plugins/plugins_dotnet"))
            {
                Directory.CreateDirectory("./plugins/plugins_dotnet");
            }
            string[] plugin_list = Directory.GetFiles("./plugins/plugins_dotnet/", "*.dll");
            try
            {
                foreach (string s in plugin_list)
                {
                    try
                    {
                        Assembly assembly = Assembly.LoadFrom(s);
                        Type? types = assembly.GetType("Plugin.Plugin");
                        object? obj = Activator.CreateInstance(types);
                        MethodInfo? info = types.GetMethod("Plugin_Main");
                        object? invoke = info?.Invoke(obj, null);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            unsafe
            {
                Int64* ptr = (Int64*)(arg.ToPointer());
                *ptr = Register.Build(onLoad,onEnable,onDisable,"d","e","f","g","h","1").ToInt64();
            }
            return 0;
        }

        public static void onLoad()
        {
            
        }

        public static void onEnable()
        {
           
        }
        
        public static void onDisable()
        {

        }
      
        [UnmanagedCallersOnly(EntryPoint = "init_endstone_plugin")]
        public static unsafe void* PluginVoid()
        {
            return Register.Build(onLoad, onEnable, onDisable, "d", "e", "f", "g", "h", "1").ToPointer();
        }
        private static void Loadstdlibs()
        {
            string[] libStrings = Directory.GetFiles("./plugins/plugins_dotnet/std_lib/", "*.dll");
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