using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Net.Mime;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;


namespace VII
{
    class Program
    {


        public static void Main(string[] args)
        {

            X509Certificate certificate = new X509Certificate2(@"U:\Kolmosvuos\WebService\localhost.cer", "entryPass", X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet);
            string baseURL = "http://localhost:8080/EX_VII_SERVER-0.0.1-SNAPSHOT/rest/customers/";

            string allEmpsXMLURL = baseURL + "xml/all";
            string allEmpsJSONURL = baseURL + "json/all";

            string ByNameCustomer = baseURL + "getName/";
            string ByPurchaseCustomer = baseURL + "getPurchase/";


            string addCustomerPath = baseURL + "add";

            string deleteCustomerString = baseURL + "delete/";
            string updateCustomerString = baseURL + "update";




            RequestHandler rq = new RequestHandler();

            while (true)
            {
                Console.WriteLine("1 to add customer\n2 to search by name\n3 to search by purchase\n4 to show all customers");
                Console.WriteLine("5 to update\n6 to delete");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddCustomer(addCustomerPath, rq, certificate);
                        break;
                    case 2:
                        GetByName(ByNameCustomer, rq, certificate);
                        break;
                    case 3:
                        getByPurchase(ByPurchaseCustomer, rq, certificate);
                        break;
                    case 4:
                        GetAllCustomers(allEmpsJSONURL, allEmpsXMLURL, rq, certificate);
                        break;
                    case 5:
                        UpdateCustomer(baseURL,updateCustomerString, rq, certificate);
                        break;
                    case 6:
                        DeleteCustomer(baseURL,deleteCustomerString, rq, certificate);
                        break;


                }
            }
        }


        private static void GetByID(string baseURL, RequestHandler rq, string id, X509Certificate certificate)
        {

            string contentType = "text/json";
            string getID = baseURL + "get/";
            getID += id;
            Console.WriteLine(rq.GetCustomer(getID, certificate, contentType));
        }


        private static void GetByName(string ByNameCustomer, RequestHandler rq, X509Certificate certificate)
        {
            string contentType = "text/json";

            Console.WriteLine("Enter name");
            string name = Console.ReadLine();
            ByNameCustomer += name;

            Console.WriteLine(rq.GetCustomer(ByNameCustomer, certificate, contentType));
        }

        private static void getByPurchase(string ByPurchaseCustomer, RequestHandler rq, X509Certificate certificate)
        {
            string contentType = "text/json";

            Console.WriteLine("Enter purchase amount");
            string purchase = Console.ReadLine();
            ByPurchaseCustomer += purchase;

            Console.WriteLine(rq.GetCustomer(ByPurchaseCustomer, certificate, contentType));
        }

        private static void AddCustomer(string addCustomerPath, RequestHandler rq, X509Certificate certificate)
        {

            Console.WriteLine("Adding a new employee:");

            Console.WriteLine("Enter ID, name, phone and purchases");
            string id = Console.ReadLine();

            string name = Console.ReadLine();

            string phone = Console.ReadLine();

            string purchase = Console.ReadLine();

            Dictionary<string, string> newCustomerData = new Dictionary<string, string>
            {
                {"id",id },
                {"name",name },
                {"phone",phone},
                { "purchase",purchase}
            };
            
            Console.WriteLine("Adding.....");
            Console.WriteLine(rq.AddCustomer(addCustomerPath, certificate, newCustomerData));
        }

        private static void GetAllCustomers(string allEmpsJSONURL, string allEmpsXMLURL, RequestHandler rq, X509Certificate certificate)
        {
            string contentType = "text/json";
            Console.WriteLine("Do you want XML, JSON or parse\n (type xml || json || parse");
            string choice = Console.ReadLine().ToLower();
            string nodeName = "customer";
            switch (choice)
            {
                case "xml":
                    Console.WriteLine(rq.GetCustomer(allEmpsXMLURL, certificate, contentType));
                    break;
                case "json":
                    Console.WriteLine(rq.GetCustomer(allEmpsJSONURL, certificate, contentType));
                    break;
                case "parse":
                    string customers = rq.GetCustomer(allEmpsXMLURL, certificate, contentType);
                    Console.WriteLine(XMLHelper.ParseXML(customers, nodeName));
                    break;

            }
        }

        private static void UpdateCustomer(string baseURL, string updateCustomerString, RequestHandler rq, X509Certificate certificate)
        {
            Console.WriteLine("Enter id to update");
            string id = Console.ReadLine();

            GetByID(baseURL, rq, id, certificate);

            Console.WriteLine("Enter new name, phone and purchases");

            string name = Console.ReadLine();

            string phone = Console.ReadLine();

            string purchase = Console.ReadLine();

            Dictionary<string, string> updatedCustomer = new Dictionary<string, string>
            {

                {"id",id },
                {"name",name },
                {"phone",phone},
                {"purchase",purchase}
            };
            Console.WriteLine(baseURL + updateCustomerString + id);
            Console.WriteLine(rq.UpdateCustomer(baseURL + updateCustomerString, updatedCustomer, certificate));
        }

        private static void DeleteCustomer(string baseURL, string deleteCustomerString, RequestHandler rq, X509Certificate certificate)
        {
            string contentType = "text/json";

            Console.WriteLine("Enter id to delete");
            string id = Console.ReadLine();
            GetByID(baseURL, rq, id, certificate);
            Console.WriteLine(baseURL + deleteCustomerString + id);
            Console.WriteLine("Are you sure to delete?\nY/N");
            string choice = Console.ReadLine().ToLower();

            if (string.Equals(choice, "y"))
            {
                string deleted = rq.DeleteCustomer(baseURL + deleteCustomerString + id, certificate, contentType);
                Console.WriteLine(deleted);
            }
            else
                return;

        }


    }
}
