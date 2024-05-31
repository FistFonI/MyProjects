using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestXml
{
    [Serializable]
    public class Owner
    {
        public string OwnerLastName { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerMiddleName { get; set; }

        public DateTime OwnerBirthDate { get; set; }

        public Owner() { }

        public Owner(string ownerLastName, string ownerFirstName, string ownerMiddleName, DateTime ownerBirthDate)
        {
            OwnerLastName = ownerLastName;
            OwnerFirstName = ownerFirstName;
            OwnerMiddleName = ownerMiddleName;
            OwnerBirthDate = ownerBirthDate;
        }
        public override string ToString()
        {
            return $"{OwnerLastName} {OwnerFirstName} {OwnerMiddleName} {OwnerBirthDate}";
        }
    }
}
