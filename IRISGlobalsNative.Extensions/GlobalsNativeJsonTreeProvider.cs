using InterSystems.Data.IRISClient.ADO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace IRISGlobalsNative.Extensions
{
    public class GlobalsNativeJsonTreeProvider : IGlobalsNativeJsonTreeProvider
    {
        private readonly IRIS _irisNative;
        public string GlobalName { get; private set; }

        public GlobalsNativeJsonTreeProvider(IRIS irisNative, string globalName)
        {
            _irisNative = irisNative;

            globalName = globalName ?? string.Empty;
            if (string.IsNullOrEmpty(globalName) || !globalName.StartsWith("^"))
                throw new ArgumentException("Invalid global name format.");

            GlobalName = globalName;
        }

        private List<string> ExtractJsonPath(string jsonStr)
        {
            var jToken = JsonConvert.DeserializeObject<JToken>(jsonStr);
            List<string> jsonPathList = new List<string>();

            do
            {
                if (jToken == null)
                    break;

                var children = jToken.Children().ToList();
                if (children.Any())
                {
                    jToken = children.First();
                    jsonPathList.Add(jToken.Path);
                }
                else
                {
                    jsonPathList.Add(jToken.Path);

                    if (jToken.Parent == null)
                    {
                        jToken = null;
                        continue;
                    }

                    if (jToken.Parent.Next != null)
                    {
                        jToken = jToken.Parent.Next;
                        continue;
                    }

                    if (jToken.Next != null)
                    {
                        jToken = jToken.Next;
                        continue;
                    }

                    while (true)
                    {
                        if (jToken == null || jToken.Parent == null)
                        {
                            jToken = null;
                            break;
                        }

                        if (jToken.Next == null)
                        {
                            jToken = jToken.Parent;
                        }
                        else
                        {
                            jToken = jToken.Next;
                            break;
                        }
                    }

                }
            }
            while (true);

            jsonPathList = jsonPathList.Distinct().ToList();
            return jsonPathList;

        }

        public void ImportJsonStringToGlobal(string jsonStr)
        {
            var o = JsonConvert.DeserializeObject<JToken>(jsonStr);
            var pathList = ExtractJsonPath(jsonStr);

            foreach (var path in pathList)
            {
                object data = (object)o.SelectToken(path);
                string dataStr = JsonConvert.SerializeObject(data);
                _irisNative.Set(dataStr, GlobalName, "Node", path);
            }

            IRISList irisList = new IRISList();
            irisList.AddRange(pathList);
            _irisNative.Set(irisList, GlobalName, "Nodes");

            _irisNative.Set(jsonStr, GlobalName, "ActualJson");
        }

        public List<string> GetPathList()
        {
            var irisList = _irisNative.GetIRISList(GlobalName, "Nodes");
            irisList = irisList ?? new IRISList();
            return irisList.ToList()
                .Select(obj => Encoding.UTF8.GetString((byte[])obj))
                .ToList();
        }

        public dynamic GetObject(string path)
        {
            var dataStr = _irisNative.GetString(GlobalName, "Node", path);

            if (dataStr == null)
                throw new ArgumentException("Json path not found.");

            var expConverter = new ExpandoObjectConverter();
            return JsonConvert.DeserializeObject<ExpandoObject>(dataStr, expConverter);
        }

        public string ExportJsonStringFromGlobal()
        {
            return _irisNative.GetString(GlobalName, "ActualJson");
        }
    }
}
