using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SDK
{
    public class Plug
    {
        public string pName;
        public string pFName;
        public string pPath;
    }

    public interface IPlugin
    {
        string action(string text);
        string name();
    }
    public class Kit
    {
        public static List<Plug> GetAllPlugins(string path)
        {
            List<Plug> myPlugs = new List<Plug>();
            if (!Directory.Exists(path))
            {
                return myPlugs;
            }

            string[] dlls = Directory.GetFiles(path, "*.dll");
            foreach (var dll in dlls)
            {
                Assembly asm = Assembly.LoadFile(dll);
                Type[] types = asm.GetTypes();
                foreach (Type t in types)
                {
                    if (t.GetInterface("IPlugin") != null)
                    {
                        Plug plug = new Plug();
                        plug.pFName = t.FullName;
                        plug.pPath = dll;
                        object obj = asm.CreateInstance(t.FullName);
                        plug.pName = obj.GetType().InvokeMember("name", BindingFlags.InvokeMethod, null, obj, null).ToString();
                        myPlugs.Add(plug);
                    }
                }
            }
            return myPlugs;
        }

        public static object CreateObject(Plug p)
        {
            Assembly asm = Assembly.LoadFile(p.pPath);
            object obj = asm.CreateInstance(p.pFName);
            return obj;
        }
    }

}
