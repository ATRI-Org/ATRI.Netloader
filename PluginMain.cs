using System.Reflection;

namespace Netmain
{
    public static class Pmain
    {
        public static int PluginMain(IntPtr arg, int argLength)
        {
            Loadstdlibs();
            if (Directory.Exists("./plugins/plugins_dotnet"))
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
                        info?.Invoke(obj, null);
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
            return 0;
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