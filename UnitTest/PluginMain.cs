using stdlib.src.endstone;

namespace Plugin
{
    [RegisterAttribute_]
    public static class Plugin 
    {
        public static plugin plugin { get; set; }
        public static string Name { get; } = "dotnet_plugin_example";

        public static string version { get; } = "0.0.1";

        public static string website { get; } = "example.com";

        public static string describe { get; } = "This is a example";

        public static string author { get; } = "YoumiHa";

        public static void onLoad()
        {
            while (true)
            {
                Thread.Sleep(1);
                plugin.getLogger().Info(plugin.getName());
            }
        }

        public static void onEnable()
        {
            Console.WriteLine("enable");
        }
        public static void onDisable()
        {
            Console.WriteLine("disable");
        }
    }
}