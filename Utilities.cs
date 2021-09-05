using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

using System.Xml;
namespace FinalAssignmentofCSharp
{
  public  class Utilities
    {
       
        public static string adminemail = "deepakk.step2gen@gmail.com", frommail = "deepakk.step2gen@gmail.com";
        public static string CustomerDataPath = @"CustomersData.xml";
        public static string UsersDataPath = @"UsersData.xml";
        public static string LogPath = @"log.txt";
        public static string ErrorLogPath = @"error.txt";
        public static  string key = "1234567812345678";
        public static string directory="data\\";

        /*-------------------- Write log -------------------*/
        public static void writeLog(string data)
        {
            File.AppendAllText(getFullPath(Utilities.LogPath), data + "  at   " + DateTime.Now + "\n");
        }
        /*--------------------x Write log x-------------------*/
        /*-------------------- Write Error log -------------------*/
        public static void writeerrorLog(string data)
        {
            File.AppendAllText(getFullPath(Utilities.ErrorLogPath), data + "  at   " + DateTime.Now + "\n");
        }
        /*--------------------x Write Error log x-------------------*/
        /*----------------------Sending Email method definition--------------------------------*/



        public static string sendEmail(string tomail,string frommail,string subject,string messagebody)
        {
            
            MailAddress to = new MailAddress(tomail);

            
            MailAddress from = new MailAddress(frommail);

            MailMessage mail = new MailMessage(from, to);

            
            mail.Subject = subject;

            
            mail.Body = messagebody;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(
                "deepakk.step2gen@gmail.com", "Deepak@123");
            smtp.EnableSsl = true;

            smtp.Send(mail);
            return "Success";
        }
        /*--------------------x Sending Email method definition x----------------------------*/
        /*--------------------- Encryption method definition -----------------------------*/

        public static string EncryptData(string textData )
        {
            string Encryptionkey = key;
            RijndaelManaged objrij = new RijndaelManaged();
            //set the mode for operation of the algorithm
            objrij.Mode = CipherMode.CBC;
            //set the padding mode used in the algorithm.
            objrij.Padding = PaddingMode.PKCS7;
            //set the size, in bits, for the secret key.
            objrij.KeySize = 0x80;
            //set the block size in bits for the cryptographic operation.
            objrij.BlockSize = 0x80;
            //set the symmetric key that is used for encryption & decryption.
            byte[] passBytes = Encoding.UTF8.GetBytes(Encryptionkey);
            //set the initialization vector (IV) for the symmetric algorithm
            byte[] EncryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            int len = passBytes.Length;
            if (len > EncryptionkeyBytes.Length)
            {
                len = EncryptionkeyBytes.Length;
            }
            Array.Copy(passBytes, EncryptionkeyBytes, len);
            objrij.Key = EncryptionkeyBytes;
            objrij.IV = EncryptionkeyBytes;
            //Creates symmetric AES object with the current key and initialization vector IV.
            ICryptoTransform objtransform = objrij.CreateEncryptor();
            byte[] textDataByte = Encoding.UTF8.GetBytes(textData);
            //Final transform the test string.
            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }

        /*--------------------x Encryption method definition x----------------------------*/

        public static string getFullPath(string path)
        {
            return directory + path;
        }
       
        /*--------------------- Show CustemersData.Xml method definition -----------------------------*/
        public static List<Customer> getAllCustomers(string addXmlFile)

        {
            List<Customer> listofcustomers = new List<Customer>();//object of Customer

            addXmlFile = getFullPath(addXmlFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(addXmlFile);
            XmlNodeList data = doc.SelectNodes("/Customers/Customer");
            foreach (XmlNode n in data)
            {
                Customer customer = new Customer(n);
                listofcustomers.Add(customer);


            }


            return listofcustomers;

        }
        /*--------------------x Show CustemersData.Xml method definition x----------------------------*/

        /*----------------------- Update Custemer Data -----------------------------------------------*/
        public static void Update(int id,string addXmlFile)
        {
            addXmlFile = getFullPath(addXmlFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(addXmlFile);
            XmlNode Customers = doc.SelectSingleNode("Customers");
            XmlNode oldCustomers = doc.SelectSingleNode("Customers/Customer[ID= " + id + "]");
            Customer oldcustomer = new Customer(oldCustomers);
            Console.WriteLine("\n" + oldcustomer);
            
            Console.WriteLine("Enter data to update");
            oldCustomers.ChildNodes.Item(1).InnerText = Validation.ReadName("Enter Customer name : ");
            oldCustomers.ChildNodes.Item(2).InnerText = Validation.ReadDob("Enter DOB : ");
            oldCustomers.ChildNodes.Item(3).InnerText = Validation.ReadMobile("Enter Phone : ");
            oldCustomers.ChildNodes.Item(4).InnerText ="" + Validation.ReadStatus("Enter Status : ");
            oldCustomers.ChildNodes.Item(6).InnerText ="" + Validation.ReadAnnualOrderAmount("Enter Annual Amount : ");
            Customer newcustomer= new Customer(oldCustomers);
            doc.Save(addXmlFile);

            string format = "<html><body><h1><center>Update Activity</center></h1><table border=1><tr><th></th><th>ID</th><th>Name</th><th>Date of Birth</th><th>Mobile</th><th>Status</th><th>AnnualAmount</th></tr><tr><td>Before Update</td><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td><td>{5}</td></tr><tr><td>After Update</td><td>{6}</td><td>{7}</td><td>{8}</td><td>{9}</td><td>{10}</td><td>{11}</td></tr></table></body></html>";
            string content = string.Format(format, oldcustomer.ID, oldcustomer.Name, oldcustomer.DOB, oldcustomer.Phone, oldcustomer.Status, oldcustomer.AnnualOrderAmount,newcustomer.ID,newcustomer.Name,newcustomer.DOB,newcustomer.Phone,newcustomer.Status,newcustomer.AnnualOrderAmount);
            /*Console.WriteLine(content);
            Console.WriteLine(oldcustomer);
            Console.WriteLine(newcustomer);*/
            sendEmail(adminemail, frommail, "Customer updated", content);
            Console.WriteLine("Success..");

        }
        /*----------------------x Update Custemer Data x----------------------------------------------*/

        /*--------------------- add data in CustomersData.xml file -------------------------------*/

        public static void AddCustomerDataInXml(Customer customer, string xmlpath)
        {
            AddCustomerDataInXml(customer.ID, customer.Name, customer.Phone, customer.Status, customer.Password,"" + customer.AnnualOrderAmount,customer.DOB,xmlpath);


        }



        public static void AddCustomerDataInXml(string id, string name, string phone, string status, string password, string  annualorderamount, string dob, string xmlpath)
        {
            try
            {
                /*Customer objcustomer = new Customer();*/
                xmlpath = getFullPath(xmlpath);
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlpath);

                XmlNode Customers = doc.SelectSingleNode("/Customers");
                XmlNode Customer = doc.CreateElement("Customer");
                XmlNode ID = doc.CreateElement("ID");
                XmlNode Name = doc.CreateElement("Name");
                XmlNode DOB = doc.CreateElement("DOB");
                XmlNode Phone = doc.CreateElement("Phone");
                XmlNode Status = doc.CreateElement("Status");
                XmlNode Password = doc.CreateElement("Password");
                XmlNode AnnualOrderAmount = doc.CreateElement("AnnualOrderAmount");


                ID.InnerText = id;
                Name.InnerText = name;
                DOB.InnerText = dob;
                Phone.InnerText = phone;
                Status.InnerText = status;
                Password.InnerText = password;
                AnnualOrderAmount.InnerText = annualorderamount;

                Console.WriteLine();
                Console.WriteLine("--------------------------------------");


                
                Customers.AppendChild(Customer);
                Customer.AppendChild(ID);
                Customer.AppendChild(Name);
                Customer.AppendChild(DOB);
                Customer.AppendChild(Phone);
                Customer.AppendChild(Status);
                Customer.AppendChild(Password);
                Customer.AppendChild(AnnualOrderAmount);

                doc.Save(xmlpath);
                Console.WriteLine("Successfully Added Customer list..");
                Console.WriteLine("--------------------------------------\n");



            }
            catch (FormatException)
            {
                Console.WriteLine("Exception");

            }

        }

        /*--------------------- add data in CustomersData.xml file -------------------------------*/
        /*--------------------- Filter data by amount and status -----------------------------*/
            public static void FilterByAmountAndStatus(double minamount,double maxamount,AccountStatus status)
        {
             List<Customer> l = getAllCustomers(CustomerDataPath);
            var result = from n in l
                         where n.AnnualOrderAmount <= maxamount && n.AnnualOrderAmount >= minamount && n.Status == status.ToString()
                         orderby n.AnnualOrderAmount
                         select n;
            foreach (Customer sa in result)
            {
                Console.WriteLine(sa);
            }
        }
        /*--------------------- Filter data by amount and status -----------------------------*/
        //***********************************************Login Verify
         public static bool verifyLogin(string phone,string password)
        {
            /*password = EncryptData(password);*/
          List<Users> l=  Utilities.getAllUsers(Utilities.UsersDataPath);
            var userlist  = from n in l
                         where n.Phone.Equals(phone) && n.Password.Equals(password)
                         select n;

            Users[] ll= userlist.ToArray();
            if (ll.Length <= 0)
                return false;
            return true;
           
           
        }
         // /***********************************************Login Verify

        /*--------------------- Show UsersData.Xml method definition -----------------------------*/
        public static List<Users> getAllUsers(string addXmlFile)

        {
            
            List<Users> listofusers = new List<Users>();//object of Users

            addXmlFile = getFullPath(addXmlFile);
            XmlDocument doc = new XmlDocument();
            doc.Load(addXmlFile);
            XmlNodeList data = doc.SelectNodes("/Users/User");
            //Console.WriteLine(data.Count);
            foreach (XmlNode n in data)
            {
                /*Customer customer = new Customer(n);*/
                Users user = new Users(n);
               // Console.WriteLine(user);
                /*listofcustomers.Add(customer);*/
                listofusers.Add(user);


            }


            return listofusers;

        }
        /*--------------------x Show CustemersData.Xml method definition x----------------------------*/

        
    }
}
