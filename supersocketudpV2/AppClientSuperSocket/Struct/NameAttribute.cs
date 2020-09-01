using System;

namespace IMESAgent.SocketClientEngine.FileLoaders
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NameAttribute : Attribute
    {
        public NameAttribute(string name, bool flag = true)
        {
            Name = name;
            Flag = flag;
        }

        public string Name
        {
            get;
            private set;
        }

        public bool Flag
        {
            get;
            private set;
        }
    }
}
