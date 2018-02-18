using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace PdsBusinessSystems.Domain
{
    public partial class Member
    {
        public string FullTitle
        {
            get; set;
        }

        public string Party
        {
            get; set;
        }

        public string MemberFrom
        {
            get; set;
        }
    }
}
