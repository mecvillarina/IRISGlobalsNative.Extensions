using InterSystems.Data.IRISClient;
using InterSystems.Data.IRISClient.ADO;
using IRISGlobalsNative.Extensions;
using IRISNativeEntity.Sample.Entity;
using System;
using System.Linq;

namespace IRISNativeEntity.Sample
{
    class Program
    {
        private static GlobalsNativeCollection<UserEntity> _userCollection;
        private static IGlobalsNativeJsonTreeProvider _jsonTreeProvider;
        static void Main(string[] args)
        {
            try
            {
                string ip = "104.197.75.13";
                string port = "27021";
                string Namespace = "USER";
                string username = "SuperUser";
                string password = "SYS";

                IRISConnection connection = new IRISConnection(ip, port, Namespace, username, password);

                connection.Open();
                IRIS irisNative = IRIS.CreateIRIS(connection);

                _jsonTreeProvider = new GlobalsNativeJsonTreeProvider(irisNative, "^JsonTree");

                //_jsonTreeProvider.ImportJsonString("{\r\n    \"glossary\": {\r\n        \"title\": \"example glossary\",\r\n\t\t\"GlossDiv\": {\r\n            \"title\": \"S\",\r\n\t\t\t\"GlossList\": {\r\n                \"GlossEntry\": {\r\n                    \"ID\": \"SGML\",\r\n\t\t\t\t\t\"SortAs\": \"SGML\",\r\n\t\t\t\t\t\"GlossTerm\": \"Standard Generalized Markup Language\",\r\n\t\t\t\t\t\"Acronym\": \"SGML\",\r\n\t\t\t\t\t\"Abbrev\": \"ISO 8879:1986\",\r\n\t\t\t\t\t\"GlossDef\": {\r\n                        \"para\": \"A meta-markup language, used to create markup languages such as DocBook.\",\r\n\t\t\t\t\t\t\"GlossSeeAlso\": [\"GML\", \"XML\"]\r\n                    },\r\n\t\t\t\t\t\"GlossSee\": \"markup\"\r\n                }\r\n            }\r\n        }\r\n    }\r\n}");

                //_jsonTreeProvider.ImportJsonString("{\"menu\": {\r\n    \"header\": \"SVG Viewer\",\r\n    \"items\": [\r\n        {\"id\": \"Open\"},\r\n        {\"id\": \"OpenNew\", \"label\": \"Open New\"},\r\n        null,\r\n        {\"id\": \"ZoomIn\", \"label\": \"Zoom In\"},\r\n        {\"id\": \"ZoomOut\", \"label\": \"Zoom Out\"},\r\n        {\"id\": \"OriginalView\", \"label\": \"Original View\"},\r\n        null,\r\n        {\"id\": \"Quality\"},\r\n        {\"id\": \"Pause\"},\r\n        {\"id\": \"Mute\"},\r\n        null,\r\n        {\"id\": \"Find\", \"label\": \"Find...\"},\r\n        {\"id\": \"FindAgain\", \"label\": \"Find Again\"},\r\n        {\"id\": \"Copy\"},\r\n        {\"id\": \"CopyAgain\", \"label\": \"Copy Again\"},\r\n        {\"id\": \"CopySVG\", \"label\": \"Copy SVG\"},\r\n        {\"id\": \"ViewSVG\", \"label\": \"View SVG\"},\r\n        {\"id\": \"ViewSource\", \"label\": \"View Source\"},\r\n        {\"id\": \"SaveAs\", \"label\": \"Save As\"},\r\n        null,\r\n        {\"id\": \"Help\"},\r\n        {\"id\": \"About\", \"label\": \"About Adobe CVG Viewer...\"}\r\n    ]\r\n}}");

                _jsonTreeProvider.ImportJsonStringToGlobal("{\"widget\": {\r\n    \"debug\": \"on\",\r\n    \"window\": {\r\n        \"title\": \"Sample Konfabulator Widget\",\r\n        \"name\": \"main_window\",\r\n        \"width\": 500,\r\n        \"height\": 500\r\n    },\r\n    \"image\": { \r\n        \"src\": \"Images\\/Sun.png\",\r\n        \"name\": \"sun1\",\r\n        \"hOffset\": 250,\r\n        \"vOffset\": 250,\r\n        \"alignment\": \"center\"\r\n    },\r\n    \"text\": {\r\n        \"data\": \"Click Here\",\r\n        \"size\": 36,\r\n        \"style\": \"bold\",\r\n        \"name\": \"text1\",\r\n        \"hOffset\": 250,\r\n        \"vOffset\": 100,\r\n        \"alignment\": \"center\",\r\n        \"onMouseUp\": \"sun1.opacity = (sun1.opacity \\/ 100) * 90;\"\r\n    }");

                _jsonTreeProvider.GetPathList();
                _jsonTreeProvider.GetObject("widget");
                Console.ReadLine();
                //var ss1 = ss[0];
                //var ss2 = ss1.PeekListTypeFamilies(1);
                //_userCollection = new GlobalsNativeCollection<UserEntity>(irisNative, "Users");

                //_userCollection.Clear();
                //var items = new List<UserEntity>();
                //for (int i = 0; i < 50; i++)
                //{
                //    items.Add(new UserEntity()
                //    {
                //        FirstName = "Mark",
                //        LastName = "Villarina",
                //        ID = Guid.NewGuid().ToString().Split("-").FirstOrDefault()
                //    });

                //    Console.WriteLine($"{i + 1}");
                //}
                //_userCollection.AddRange(items);

                //Console.WriteLine(_userCollection.Count());
                //Console.WriteLine(_userCollection.Count(x => x.ObjectId == "088d3fc0"));

                //var s = _userCollection.Update(new UserEntity()
                //{
                //    FirstName = "Mark Erwin",
                //    LastName = "Villarina",
                //    ID = "Test1234"
                //}, "14396211");
                ////_userCollection.RemoveAt(0);

                //Console.WriteLine(_userCollection.Count());

                //_userCollection.RemoveAll();
                //Console.WriteLine(_userCollection.Count());
            }
            catch (Exception ex)
            {

            }
        }
    }
}
