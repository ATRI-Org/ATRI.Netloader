using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using netloader;
using stdlib.src.endstone;
using net.r_eg.Conari;
using net.r_eg.Conari.Core;
namespace stdlib
{
    public static class RegisterPlugins
    {
        
        public static List<IntPtr> Run()
        {
         
            List<IntPtr> plugins = new List<IntPtr>();
            foreach (string file in Directory.GetFiles(".\\mods","*.dll"))
            {
                Assembly loadFrom = Assembly.LoadFrom(file);
                IntPtr loadByRegister = LoadByRegister(loadFrom);
                if (loadByRegister == IntPtr.Zero)
                {
                    continue;
                }
                else
                {
                    plugins.Add(loadByRegister);
                }
            }
            return plugins;
        }
        private static IntPtr LoadByRegister(Assembly loadAssembly)
        {
            try
            {
                var classes = loadAssembly.GetTypes();
                IntPtr ptr = IntPtr.Zero;
                foreach (var clazz in classes)
                {
                    if (clazz.GetCustomAttribute<RegisterAttribute_>() == null)
                    {
                        return IntPtr.Zero;
                    }
                    var onloadInfo = clazz.GetMethod("onLoad");
                    var onenableInfo = clazz.GetMethod("onEnable");
                    var ondisableInfo = clazz.GetMethod("onDisable");
                    var nameInfo = clazz.GetProperty("Name");
                    var versionInfo = clazz.GetProperty("version");
                    var websiteInfo = clazz.GetProperty("website");
                    var describeInfo = clazz.GetProperty("describe");
                    var authorInfo = clazz.GetProperty("author");
                    var pluginInfo = clazz.GetProperty("plugin");
                    if (onloadInfo != null && onenableInfo != null && ondisableInfo != null)
                    {
                        bool isStaticClass = IsStaticClass(clazz);
                        object? instance;
                        if (!isStaticClass)
                        { instance = Activator.CreateInstance(clazz);
                           
                           ptr =   Register.Build(
                                (() => { onloadInfo.Invoke(clazz, new object?[] { }); }),
                                (() => { onenableInfo.Invoke(clazz, new object?[] { }); }),
                                (() => { ondisableInfo.Invoke(clazz, new object?[] { }); }),
                                (string)describeInfo.GetValue(clazz),
                                (string)versionInfo.GetValue(clazz),
                                (string)nameInfo.GetValue(clazz),
                                (string)websiteInfo.GetValue(clazz),
                                "none",
                                (string)websiteInfo.GetValue(clazz)
                            );
                            pluginInfo.SetValue(instance,new plugin(ptr));
                        }
                        else
                        {
                            instance = null;
                            ptr =  Register.Build(
                                (() => { onloadInfo.Invoke(null, new object?[] { }); }),
                                (() => { onenableInfo.Invoke(null, new object?[] { }); }),
                                (() => { ondisableInfo.Invoke(null, new object?[] { }); }),
                                (string)describeInfo.GetValue(null),
                                (string)versionInfo.GetValue(null),
                                (string)nameInfo.GetValue(null),
                                (string)websiteInfo.GetValue(null),
                                "none",
                                (string)websiteInfo.GetValue(null)
                            );
                            pluginInfo.SetValue(instance, new plugin(ptr));
                        }
                    }
                    else
                    {
                        continue;
                    }
                }

                return ptr;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return IntPtr.Zero;
            }
           
        }
        private static bool IsStaticClass(Type type)
        {
            return type.IsAbstract && type.IsSealed;
        }
    }
}
