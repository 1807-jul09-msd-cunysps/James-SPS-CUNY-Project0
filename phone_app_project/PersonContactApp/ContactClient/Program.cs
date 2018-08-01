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
            ContactLibrary.Roladex roladex = new ContactLibrary.Roladex();

            Console.WriteLine("welcome to James' Roladex!");
            Console.WriteLine("");
            while (true) {
                #region user commands
                Console.WriteLine("available commands:");
                Console.WriteLine("'list' - to list all records");
                Console.WriteLine("'pop' - to populate roladex from remote database");
                Console.WriteLine("'save' - to persist the roladex into the remote database");
                Console.WriteLine("'json' - to print the roladex as a json string");
                Console.WriteLine("'add' - to add a person into the roladex");
                Console.WriteLine("'del' - to delete a person from the roladex");
                Console.WriteLine("'update' - to update a person's info in the roladex");
                Console.WriteLine("'search' - to search for a person in the roladex");
                Console.WriteLine("'clear' - to clear the screen");
                Console.WriteLine("'quit' - exit the program");
                //Console.WriteLine("command");
                Console.WriteLine("\n please enter your command (case insensitive, without quotation marks) and press <enter>");
                #endregion

                string input = Console.ReadLine().ToLower();

                while (true)
                {
                    switch (input)
                    {
                        
                        #region case "list"
                        case "list":
                            if (roladex.isEmpty()) Console.WriteLine("roladex is empty, silly!");
                            foreach (Person list_person in roladex.All())
                                Console.WriteLine(list_person);
                            break;
                        #endregion
                        #region case "pop"
                        case "pop":
                            Console.WriteLine(
                                (new RolData(roladex)).Populate() ? "success!" : "hmm, this didn't work"
                            );
                            break;
                        #endregion
                        #region case "save"
                        case "save":
                            Console.WriteLine(
                                (new RolData(roladex)).PersistDB() ? "success!" : "hmm, this didn't work"
                            );
                            break;
                        #endregion
                        #region case "json"
                        case "json":
                            Console.WriteLine((new RolData(roladex)).ToJSON());
                            //Console.WriteLine("haven't implemented this yet :(");
                            ////Console.WriteLine("Right now I'm only doing this as a SQL command (as opposed to with c# commands))");
                            break;
                        #endregion
                        #region case "add"
                        case "add":
                            Console.WriteLine("please enter a first name and last name separated by a space, then hit <enter>");
                            string[] name = Console.ReadLine().Split(' ');
                            Person person_to_add = new Person(name[0], name[1]);
                            bool success = roladex.Add(person_to_add);
                            Console.WriteLine(success ? "success!" : "hmm, this didn't work");
                            if (success)
                            {
                                Console.WriteLine("would you like to add more fields? (zipcode, city, phone)");
                                Console.WriteLine("yes/no, then press <enter>");
                                string add_input = Console.ReadLine();
                                if (add_input.ToLower() == "yes")
                                {
                                    Console.WriteLine("please enter zipcode");
                                    person_to_add.address.zipcode = Console.ReadLine();
                                    Console.WriteLine("please enter a city");
                                    person_to_add.address.city = Console.ReadLine();
                                    Console.WriteLine("please enter a phone number");
                                    person_to_add.phone.number = Console.ReadLine();
                                }
                            }
                            break;
                        #endregion
                        #region case "del"
                        case "del":
                            Console.WriteLine("please enter a Person ID, then press <enter>");
                            Console.WriteLine("(if you don't have the ID, use the search feature)");
                            try
                            {
                                long del_id = Convert.ToInt64(Console.ReadLine());
                                Console.WriteLine(
                                    roladex.Remove(del_id)
                                    ? "success!" : "hmm, this didn't work -- that person is likely not in the roladex"
                                );
                            }
                            catch (Exception)
                            { Console.WriteLine("hmm, that didn't work. Was your input a number?"); }
                            //finally { break; }
                            break;
                        #endregion
                        #region case "update"
                        case "update":
                            Console.WriteLine("please enter a Person ID, then press <enter>");
                            Console.WriteLine("(if you don't have the ID, use the search feature)");
                            long upd_id; //the user input
                            try { upd_id = Convert.ToInt64(Console.ReadLine()); }
                            catch (Exception)
                            {
                                Console.WriteLine("hmm, couldn't convert your input into a number.");
                                break;
                            }
                            Console.WriteLine("please enter the field to update, then press <enter>");
                            Console.WriteLine("options: " + String.Join(", ", Roladex.search_fields));
                            string upd_field = Console.ReadLine();
                            Console.WriteLine("please enter the new value for that field, then press <enter>");
                            string upd_value = Console.ReadLine();
                            Console.WriteLine(
                                roladex.Update(upd_id, upd_field, upd_value)
                                ? "success!" : "hmm, this didn't work"
                             );
                            break;
                        #endregion
                        #region case "search"
                        case "search":
                            Console.WriteLine("please enter the field you'd like to search, then press <enter>");
                            Console.WriteLine("available fields are " + String.Join(", ", Roladex.search_fields));
                            string search_field = Console.ReadLine();
                            Console.WriteLine("please enter the " + search_field + " you'd like to search for, then press <enter>");
                            Console.WriteLine("(exact matches only)");
                            string search_term = Console.ReadLine();
                            long[] search_results = roladex.Search(search_field, search_term);
                            if (search_results.Length == 0) Console.WriteLine("no matches!");
                            else
                            {
                                Console.WriteLine("matches:");
                                foreach (long pid in search_results)
                                {
                                    Console.WriteLine(roladex.GetPersonByPID(pid));
                                }
                            }
                            break;
                        #endregion
                        #region case "clear"
                        case "clear":
                            for (int i = 0; i < 100; i++) Console.WriteLine();
                            break;
                        #endregion
                        #region case "quit"
                        case "quit":
                            System.Environment.Exit(0);
                            break;
                        #endregion
                        
                    }
                    Console.WriteLine("\n write a command then <enter>, or just <enter> to see all commands. 'quit' to exit.");
                    input = Console.ReadLine();
                    if (input == "") break;
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
            RolData rolD = (new RolData(roladex));
            rolD.Populate();
            roladex.Add(new Person("Katrina", "Smith"));
            Console.WriteLine(roladex);
            rolD.PersistDB();
            Console.Read();
            
        }
    }
}
