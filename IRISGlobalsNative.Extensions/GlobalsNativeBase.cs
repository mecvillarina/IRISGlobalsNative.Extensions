using System;
using System.Linq;

namespace IRISGlobalsNative.Extensions
{
    public class GlobalsNativeBase
    {
        public string ObjectId { get;  set; }

        public GlobalsNativeBase()
        {
        }

        public void SetObjectId()
        {
            ObjectId = Guid.NewGuid().ToString().Split("-").FirstOrDefault();
        }
    }
}
