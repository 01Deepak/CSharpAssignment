using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignmentofCSharp
{
    class Program
    {
        static List<Customer> l = Utilities.getAllCustomers(Utilities.CustomerDataPath);

        
        static void showMenu()
        {

            Console.WriteLine(" Press 1 for Insert Custemers Data.\n Press 2 for Show Costemers List. \n" +
                " Press 3 for filter customer list by minimum amount or max amount or status\n" +
                " Press 4 for for Update Customer list \n 0 for Exit.\n");
            Console.Write("Your Input :");
        }

      
        static void Main(string[] args)
        {
            /*Utilities.writeerrorLog("Exception hai bhai..");*/
            try
            {

               /* string content = string.Format("<html><body><h1>{0}</h1></body></html>", "Heading");
                Utilities.sendEmail("deepakk.step2gen@gmail.com", "deepakk.step2gen@gmail.com", "Test", content);
*/
                // Console.WriteLine(  Validation.ReadId("Enter Id"));
                Console.WriteLine("Press 1 for user login.\n Press 0 for EXit.");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                        return;
                    case "1":
                        Console.WriteLine("------------ USER LOGIN-----------");
                        Console.Write("Enter Your Mobile Number : ");
                        string mobile = Console.ReadLine();
                        Console.Write("Enter Your Password : ");
                        string Password = Console.ReadLine();
                        bool b = Utilities.verifyLogin(mobile, Password);
                        if (b == true)
                        {
                            Console.WriteLine("Login Success");
                            Utilities.writeLog(mobile + " logged in ");
                            while (true)
                            {
                                showMenu();

                                string option = Console.ReadLine();
                                switch (option)
                                {
                                    case "0":
                                        return;
                                    case "1":
                                        Customer newcustomer = new Customer();
                                        Utilities.AddCustomerDataInXml(newcustomer, Utilities.CustomerDataPath);
                                        l = Utilities.getAllCustomers(Utilities.CustomerDataPath);
                                        Console.WriteLine("Success");
                                        break;
                                    case "2":
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                                        Console.WriteLine(" ID \t\t Name \t DOB \t\t Phone \t\t Status \t Order Amount");
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                                        l = Utilities.getAllCustomers(Utilities.CustomerDataPath);
                                        foreach (Customer c in l)
                                            Console.WriteLine(c);
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------------\n");
                                        break;

                                    case "3":


                                        double minamount1 = Validation.ReadAnnualOrderAmount("Please Enter min amount: ");
                                        double maxamount1 = Validation.ReadAnnualOrderAmount("Please Enter Max amount: ");
                                        AccountStatus status1 = Validation.ReadStatus("Please Enter Status :");
                                        Console.WriteLine();
                                        Console.WriteLine("Customer List after Filter by Amount and Status");
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                                        Console.WriteLine("ID \t\t Name \t DOB \t\t Phone \t\t Status \t Order Amount");
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------------");
                                        Utilities.FilterByAmountAndStatus(minamount1, maxamount1, status1);
                                        Console.WriteLine("-----------------------------------------------------------------------------------------------------\n");
                                        Console.WriteLine("min " + minamount1 + maxamount1 + status1);


                                        Console.WriteLine();
                                        break;

                                    case "4":
                                        Console.Write("Enter Id for Update : ");
                                        int id = Convert.ToInt32(Console.ReadLine());

                                        Utilities.Update(id, Utilities.CustomerDataPath);
                                        break;




                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid Users");
                            goto case "1";
                        }



                }






                //l.Sort(new CustomerComparator());

                /*string option = "-1";*/


            }
            catch(Exception ex)
            {
                Console.WriteLine("plese select Correct Input");
                Utilities.writeerrorLog("Exception hai bhai..");

            }
        }
    }
}
