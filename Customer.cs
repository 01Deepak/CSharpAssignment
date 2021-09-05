using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace FinalAssignmentofCSharp
{
  public  class Customer
    {
      public   string ID, Name, Phone, Status, Password;
       public double AnnualOrderAmount;
        public string DOB;

        public Customer(string ID,string Name,string Phone,string Status,string Password,double AnnualOrderAmount,string DOB)
        {
            this.ID = ID;
            this.Name = Name;
            this.Phone = Phone;
            this.Status = Status;
            this.Password = Password;
            this.AnnualOrderAmount = AnnualOrderAmount;
            this.DOB = DOB;

        }

       /* public XmlNode getNode(XmlDocument doc)
        {
            XmlElement node = doc.CreateElement("Customers");
            node.SetAttribute("ID", ID);
            return node;
        }*/
       
        public Customer()
        {
            
            this.ID = Validation.ReadId("Enter ID : ");
            this.Name = Validation.ReadName("Enter Name : ");
            
            this.DOB = Validation.ReadDob("Enter dob (dd-mm-yyyy):");

            this.Phone = Validation.ReadMobile("Enter Mobile Number :");
            this.Status ="" + Validation.ReadStatus("Enter Status : ");

            this.Password = Validation.ReadPassword("Enter Password : ");
            this.AnnualOrderAmount = Validation.ReadAnnualOrderAmount("Enter Amount : ");
        }

        public Customer(XmlNode node)
        {
            this.ID = node["ID"].InnerText;
            this.Name = node["Name"].InnerText;
            this.DOB =node["DOB"].InnerText;
            this.Phone = node["Phone"].InnerText;
            this.Status = node["Status"].InnerText;
            this.Password = node["Password"].InnerText;
            this.AnnualOrderAmount = Convert.ToDouble(node["AnnualOrderAmount"].InnerText);
        }
        public int Age
        {

            get
            {
                TimeSpan span = DateTime.Now -Convert.ToDateTime( DOB);
                return Convert.ToInt32( span.TotalDays / 365);

            }

        }
        public override string ToString()
        {
            return string.Format("{0,3}{1,20}{2,12}{3,14}{4,15}{5,20}",ID,Name,DOB,Phone,Status,AnnualOrderAmount);
            /*return ID + "\t" + Name + "\t" + DOB + "\t" + Phone + "\t" + Status + "\t\t" + Password + "\t" + AnnualOrderAmount;*/
        }
    }
}
