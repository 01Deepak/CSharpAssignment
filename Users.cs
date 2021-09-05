using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FinalAssignmentofCSharp
{
  public  class Users
    {
     public   string ID, Name, Phone, Password;
        public Users(XmlNode node)
        {
            this.ID = node["Id"].InnerText;
            this.Name = node["Name"].InnerText;
            this.Phone = node["Phone"].InnerText;
            this.Password = node["Password"].InnerText;

        }
    }
}
