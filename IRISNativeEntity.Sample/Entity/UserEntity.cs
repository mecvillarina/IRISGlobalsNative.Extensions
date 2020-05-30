using IRISGlobalsNative.Extensions;

namespace IRISNativeEntity.Sample.Entity
{
    public class UserEntity : GlobalsNativeBase
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
