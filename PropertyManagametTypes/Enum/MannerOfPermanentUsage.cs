using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PropertyManagametTypes.Enum
{
    [DataContract]
    public enum MannerOfPermanentUsage
    {
        //[EnumMember(Value = "Emp")]
        //[DescriptionAttribute("Test")]
        //[EnumMember(Value ="Test")]
        // [Display(Name = "It is complicated")]
        [DataMember]
        [EnumMember]
        Residentional,
        //[EnumMember]
        Industrial ,
        // [EnumMember]
        Agricultural
    }
}
