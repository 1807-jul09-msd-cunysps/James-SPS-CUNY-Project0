using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactLibrary;
using System.Data.SqlClient;

namespace ContactClient
{
    class DatabaseNotResponding:ApplicationException
    {
        public DatabaseNotResponding():base()
        {
            // logic to exception
        }
    }
    class Program
    {
        static int Result(int a, int b)
        {
            var logger = NLog.LogManager.GetCurrentClassLogger();
            ArrayList result = new ArrayList();
            result.Add("test 1");
            result.Add("test 2");
            result.Add("test 3");
            result.Add(5);
            result.Add('a');
            result.Add(a);
            result.Add(b);
            try
            {
                foreach (string item in result)
                {
                    Console.WriteLine(item);
                }
            }
            catch (DivideByZeroException ex)
            {
                //Log the exception                
                logger.Info(ex.Message);
                Console.WriteLine("Please provide non zero denominator");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //closing resources code here
                Console.WriteLine("I always make sure to close unmanaged resources");
            }

            return 0;
        }
        static void Main(string[] args)
        {
            ContactLibrary.Roladex contacts = new ContactLibrary.Roladex();

            Console.WriteLine("welcome to James' Roladex!");
            Console.WriteLine("");
            while (true) {
                Console.WriteLine("available commands:");
                Console.WriteLine("'list' - to list all records");
                Console.WriteLine("'pop' - to populate roladex from database");
                Console.WriteLine("'save' - to persist the roladex into the database");
                Console.WriteLine("'json' - to print the roladex as a json file");
                Console.WriteLine("'add' - to add a person into the roladex");
                Console.WriteLine("'del' - to delete a person from the roladex");
                Console.WriteLine("'update' - to update a person's info in the roladex");
                Console.WriteLine("'search' - to search for a person in the roladex");
                Console.WriteLine("'quit' - exit the program");
                Console.WriteLine("\n please enter your command (case insensitive, without quotation marks) and press <enter>");

                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "list":
                        Console.WriteLine(contacts.All());
                        break;
                    case "pop":
                        Console.WriteLine(
                            (new RolData(contacts)).Populate() ? "success!" : "hmm, this didn't work"
                        );
                        break;
                    case "save":
                        Console.WriteLine(
                            (new RolData(contacts)).PersistDB() ? "success!" : "hmm, this didn't work"
                        );
                        break;
                    case "json":
                        Console.WriteLine("haven't implemented this yet :(");
                        //Console.WriteLine("Right now I'm only doing this as a SQL command (as opposed to with c# commands))");
                        break;
                    case "add":
                        Console.WriteLine("please enter a first name and last name separated by a space, then hit <enter>");
                        string[] name = Console.ReadLine().Split(' ');
                        Console.WriteLine(
                            contacts.Add(new Person(name[0],name[1]))
                            ? "success!" : "hmm, this didn't work"
                         );
                        break;
                }
            }


        


            #region LINQ
            /*  Person p = new Person();
              var persons = p.Get();
              //LINQ-Language Integrate Query
              //Query Syntax
              /*var query = from p1 in persons
                          where p1.firstName.StartsWith("T")
                          select p1;
             var query = from p1 in persons
                           where p1.address.houseNum.Equals("121")
                           select p1;

              int[] marks = new int[] {45,56,89,78,98 };

              /*var query = from m in marks
                          where m > 60 && m<80
                          select m;
              foreach (var item in query)
              {
                  Console.WriteLine($"{item.firstName} {item.lastName} Phone +{item.phone.countrycode+"-"+item.phone.areaCode+"-"+item.phone.number}");
              }*/
            #endregion
            RolData rolD = (new RolData(contacts));
            rolD.Populate();
            contacts.Add(new Person("Katrina", "Smith"));
            Console.WriteLine(contacts);
            rolD.PersistDB();
            Console.Read();
            
        }
    }
}
