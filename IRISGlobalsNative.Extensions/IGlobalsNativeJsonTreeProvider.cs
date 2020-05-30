using System;
using System.Collections.Generic;
using System.Text;

namespace IRISGlobalsNative.Extensions
{
    public interface IGlobalsNativeJsonTreeProvider
    {
        string GlobalName { get; }
        void ImportJsonStringToGlobal(string jsonStr);
        List<string> GetPathList();
        dynamic GetObject(string path);
        string ExportJsonStringFromGlobal();
    }
}
