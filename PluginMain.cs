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
            int length = 0;
            List<IntPtr> nints = new List<IntPtr>();
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
                        MethodInfo? loadInfo = types.GetMethod("onLoad");
                        MethodInfo? enableInfo = types.GetMethod("onEnable");
                        MethodInfo? disableInfo = types.GetMethod("onDisable");
                        FieldInfo? nameInfo = types.GetField("Name");
                        FieldInfo? versionInfo = types.GetField("version");
                        FieldInfo? websiteInfo = types.GetField("website");
                        FieldInfo? describeInfo = types.GetField("describe");
                        FieldInfo? authorInfo = types.GetField("author");
                        nints.Add(Register.Build(
                            (() => { loadInfo.Invoke(obj,new object?[]{}); }),
                             (() => { enableInfo.Invoke(obj, new object?[] { });}),
                                        (() => { disableInfo.Invoke(obj,new object?[]{}); }),
                            (string)describeInfo.GetValue(obj),
                            (string)versionInfo.GetValue(obj),
                            (string)nameInfo.GetValue(obj),
                            (string)websiteInfo.GetValue(obj),
                            "none",
                            (string)websiteInfo.GetValue(obj)
                            ));
                        length++;
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
                if (length <= 0)
                {
                    var hh = (Int64*)(arg.ToPointer());
                    *hh = 0;
                    return 0;
                }

                void* ptrVoid = NewArray();
                foreach (IntPtr nint in nints)
                {
                    AddIntoArray(ptrVoid,nint.ToPointer());
                }

                var ptr = (long*)arg;
                *ptr = (long)ptrVoid;
                // GC.Collect();
            }
            return length;
        }
        [DllImport("EndStoneDotNetLoader.dll")]
        public static unsafe  extern void* NewArray();
        [DllImport("EndStoneDotNetLoader.dll")]
        public static unsafe extern void AddIntoArray(void* arrayVoid,void* ptrVoid);
       
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
