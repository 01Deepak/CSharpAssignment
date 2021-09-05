using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace FinalAssignmentofCSharp
{
    public class Validation
    {
        public static char[] specialchars = "`~@#$%^&*()_-++{}[],.<>?/".ToCharArray();
        public static string regname = "([A-Za-z][           .]{0,1}){5,25}$";
        public static string regdob = "[0-9]{2,}[-]{1,}[0-9]{2,}[-]{1,}[0-9]{4,}$";
        public static string regmobile = "^[6-9]{1}[0-9]{9}$";




        public static bool isValidPassword(string s)
        {
            if (s.Length < 6)
                return false;
            char[] chars = s.ToCharArray();
            int cap = 0, small = 0, digits = 0, specialchar = 0;
            foreach(char ch in chars)
            {
                if (Char.IsLower(ch))
                    small++;
                if (Char.IsUpper(ch))
                    cap++;
                if (Char.IsDigit(ch))
                    digits++;
                if (specialchars.Contains(ch))
                    specialchar++;
            }
            if (digits <= 0 || small <= 0 || cap <= 0 || specialchar <= 0)
                return false;
            return true;
           
        }

        public static bool IsValidName(string str)
        {

            return Regex.IsMatch(str, regname);

        }

        public static bool IsValidMobile(string str)
        {
            return Regex.IsMatch(str, regmobile);
        }
        public static bool IsValiddob(string str)
        {
            return Regex.IsMatch(str, regdob);
        }


        public static bool isValidDate(int year,int month,int day)
        {
            try
            {
                
                 new DateTime(year,month,day);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string  ReadName(string prompt)
        {
            
            while(true)
            {
                Console.Write(prompt);
                string name = Console.ReadLine();
                bool b = IsValidName(name);
                if(!b)
                {
                    Console.WriteLine("Invalid Name");
                    continue;
                }
                return name;
            }
        }

        public static bool IsIdUsed(string id)
        {
            /*password = EncryptData(password);*/
            List<Customer> l = Utilities.getAllCustomers(Utilities.CustomerDataPath);
            //Console.WriteLine(id);
            var userlist = from n in l
                           where n.ID.Equals(id)
                           select n;

            Customer[] ll = userlist.ToArray();
            if (ll.Length <= 0)
                return false;
            return true;

        }
        public static string ReadPassword(string prompt)
        {

            while (true)
            {
                Console.Write(prompt);
                string pass = Console.ReadLine();
                bool b = isValidPassword(pass);
                if(!b)
                {
                    Console.WriteLine("Invalid Password");
                    continue;
                }
                string encryptedPassword = Utilities.EncryptData(pass);
                return encryptedPassword;
            }
        }
        public static string ReadDob(string prompt)
        {

            while (true)
            {
                Console.Write(prompt);
                string dob = Console.ReadLine();
               
                bool b = IsValiddob(dob);
                if (!b)
                {
                    Console.WriteLine("Invalid Date");
                    continue;
                }


                string str = dob;
                char seps = '-';
                string[] parts = str.Split(seps);
                /*Console.WriteLine("Piece for Splite:");*/
                int dobdd = Convert.ToInt32(parts[0]);
                int dobmm = Convert.ToInt32(parts[1]);
                int dobyyyy = Convert.ToInt32(parts[2]);

                bool c = isValidDate(dobyyyy, dobmm, dobdd);
                if (!c)
                {
                    Console.WriteLine("Invalid Date");
                    continue;
                }
                /*Console.WriteLine(dobdd+" "+dobmm+" "+dobyyyy);*/


                return dob;
            }
        }

        public static string ReadId(string prompt)
        {

            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    string id = Console.ReadLine();
                   int n= Convert.ToInt32(id);
                    if (n < 0)
                        throw new Exception();
                    bool b = IsIdUsed(id);
                    if (b)
                    {
                        Console.WriteLine("Id already used");
                        continue;
                    }
                    return id;
                }
                catch
                {
                    Console.WriteLine("Enter a +ve integer");
                }
            }
        }



        public static string ReadMobile(string prompt)
        {

            while (true)
            {
                Console.Write(prompt);
                string mobile = Console.ReadLine();
                bool b = IsValidMobile(mobile);
                if (!b)
                {
                    Console.WriteLine("Invalid Mobile Number");
                    continue;
                }
                return mobile;
            }
        }
        public static double  ReadAnnualOrderAmount(string prompt)
        {
            while (true)
            {
                try { 
                Console.Write(prompt);
                double amount =Convert.ToDouble( Console.ReadLine());
                    if (amount < 0)
                        throw new Exception("-ve not allowed");
                
                
                return amount;
            }
            catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static AccountStatus ReadStatus (string prompt)
        {
            while (true)
            {
                try
                {
                    
                    Console.Write(prompt);

                   string  input   = Console.ReadLine().ToLower().Trim();
                    if (input.Equals("active"))
                        return AccountStatus.Active;
                    if (input.Equals("inactive"))
                        return AccountStatus.Inactive;
                    throw new Exception("Enter either active or inactive");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }



       
       
    }
}
