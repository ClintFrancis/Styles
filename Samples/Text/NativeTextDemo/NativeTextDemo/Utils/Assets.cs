using System;
using System.IO;
using System.Reflection;

namespace NativeTextDemo
{
    public static class Assets
    {
        public static Type AssemblyType { get; set; }

        public static string LoadString(string id)
        {
            if (AssemblyType == null)
            {
                throw new NullReferenceException("AssemblyType has not been set");
            }

            return LoadString(AssemblyType, id);
        }

        public static string LoadString<T>(T type, string id) where T : Type
        {
            var assembly = type.GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(id);
            string text = "";
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }
    }
}
