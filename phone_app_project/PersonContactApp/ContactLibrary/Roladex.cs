using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
//using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace ContactLibrary
{
    public class Roladex
    /// this is a collection of people/contacts. Why three forward-slashes? I don't know.
    {
        List<Person> contacts;
        Dictionary<long, int> IDs;
        public static string[] search_fields = { "firstName", "lastName", "zipcode", "city", "phone" };
        public Roladex()
        {
            contacts = new List<Person>();
            IDs = new Dictionary<long, int>();
        }
        public int Count { get { return contacts.Count; }  } //represents the size/length of the roladex
        
        public bool isEmpty() { return contacts.Count == 0; }
        public bool Add (Person p)  {
            try
            {
                IDs.Add(p.Pid, contacts.Count);
                this.contacts.Add(p);
                return true;
            }
            catch (ArgumentNullException) //if Pid is null
            { return false; }
            catch (ArgumentException) //if that Pid is already in IDs dictionary
            {
                Console.WriteLine("A person with this Pid is already in this Roladex");
                return false;
            }
        }

        public long[] Search (string field, string search_term)
        ///<param name="field">accepts "firstName", "lastName", "zipcode", "city", "phone"<param/>
        ///<returns>an array of PID values</returns>
        {
            List<long> results = new List<long>();
            switch (field.ToLower())
            {
                case "firstname":
                    foreach (Person person in contacts)
                        if (person.firstName == search_term) results.Add(person.Pid);
                    break;
                case "lastname":
                    foreach (Person person in contacts)
                        if (person.lastName == search_term) results.Add(person.Pid);
                    break;
                case "zipcode":
                    foreach (Person person in contacts)
                        if (person.address.zipcode == search_term) results.Add(person.Pid);
                    break;
                case "city":
                    foreach (Person person in contacts)
                        if (person.address.city == search_term) results.Add(person.Pid);
                    break;
                case "phone":
                    foreach (Person person in contacts)
                        if (person.phone.Equals(search_term)) results.Add(person.Pid);
                    break;

            }
            return results.ToArray();
        }
        //public long[] Search (string search_term)
        //    ///iterates through the different search fields, tries them all, then join the result arrays together
        //{
        //    long[] results = [];
        //    foreach (string field in Roladex.search_fields)
        //    {
        //        results.Join(Search(field,search_term))
        //    }
        //    return results;
        //    //a few parts of this don't work yet
        //}

        public Person GetPersonByPID ( long PID)
        {
            Person p;
            try { p = contacts[IDs[PID]];  }
            catch (Exception) { return null; }
            return p;
        }

        public bool Update (long PID, string field, object new_data)
            ///<param name="field">accepts 'firstName', 'lastName', 'zipCode', 'city', 'phone', 'address', case-insensitive</param>
        {
            Person person;
            try {
                    person = GetPersonByPID(PID);
                    if (person == null) return false;
                }
            catch (Exception) { return false; }
            switch (field.ToLower())
            {
                case "firstname":
                    person.firstName = (string) new_data;
                    return true;
                case "lastname":
                    person.lastName = (string)new_data;
                    return true;
                case "zipcode":
                    person.address.zipcode = (string)new_data;
                    return true;
                case "city":
                    person.address.city = (string)new_data;
                    return true;
                case "phone":
                    person.phone = new Phone(new_data);
                    return true;
                case "address":
                    person.address = (Address)new_data;
                    return true;
            }
            return false;

        }

        //        public void remove (Person P) {
        //            foreach (Person p in contacts)
        //            {
        //                if (p.Equals(P)) { }
        ////                if (p == P) { }
        //            }
        //        }

        public bool Remove (long ID)
        {
            try
            {
                int index;
                if (this.IDs.TryGetValue(ID, out index))
                {
                    IDs.Remove(ID);
                    contacts.RemoveAt(index);
                    return true;
                }
                return false;
            }
            catch (ArgumentNullException) { return false; }
            //Dictionary methods TryGetValue() and Remove() only throw ArgumentNullException type exceptions
            
        }

        public override string ToString()
        {
            string peeps = "";
            string separator = "\n";
            foreach (Person person in contacts) peeps += person + separator;
            return peeps;
        }

        public List<Person> All() { return contacts; }


    }

    public class RolData
    {
        SqlConnection con;
        string command;
        string conStr;

        private Roladex roladex;
        public RolData() : this(new Roladex()) { }
        public RolData(Roladex roladex)
        {
            con = null;
            this.roladex = roladex;
            conStr = "Data Source=day10testserver.database.windows.net;Initial Catalog=PhoneAppDatabase;Persist Security Info=True;User ID=James;Password=Onama25Chesture";
        }

        public bool Populate()
        {
            bool result;
            command = "select * from People";
            try
            {
                con = new SqlConnection(conStr);
                con.Open();
                //2. SQL Command
                SqlCommand cmd = new SqlCommand(command, con);
                //3. Execute query
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Person person = new Person((long)(int)dr[0])
                    //why do I need to cast it to an int (when it's already an int) before casting it to a long? 
                    //Console.WriteLine(dr[0].GetType());
                    {
                        firstName = (string)dr[1],
                        lastName = (string)dr[2]
                    };
                    try {
                        person.address.zipcode = (string)dr[3];
                        person.address.city = (string)dr[4];
                        person.phone.number = (string)dr[5];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        var logger = NLog.LogManager.GetCurrentClassLogger();
                        logger.Info("Index out of range exception during populate function");
                    }
                    catch (Exception) { } //what kind of exception could this be??
                    roladex.Add(person);
                }
                result = true;
            }
            catch (SqlException ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }
        public bool PersistDB()
        {
            bool result;
            command = "DELETE FROM People"; //deletes all records from the table
            try
            {
                con = new SqlConnection(conStr);
                con.Open();
                SqlCommand cmd = new SqlCommand(command, con);
                cmd.ExecuteNonQuery();
                foreach (Person person in roladex.All())
                {
                    if (person.firstName != null && person.lastName != null) {
                    //people with a firstName or lastName but not both will NOT be persisted
                        if (person.address.zipcode != null && person.address.city != null && person.phone.number != null)
                        {
                            command = "insert into People (firstName, lastName, zipcode, city, phone) values ('" + 
                                person.firstName       + "', '" + 
                                person.lastName        + "', '" +
                                person.address.zipcode + "', '" + 
                                person.address.city    + "', '" +
                                person.phone.number
                                + "')";
                        }
                        else { //this means that there's a first name and a last name, but no zip/city/phone
                            command = "insert into People (firstName, lastName) " +
                                "values ('" + person.firstName + "', '" + person.lastName + "')";
                        }
                        cmd = new SqlCommand(command, con);
                        cmd.ExecuteNonQuery();
                    }
                }
                result = true;
            }
            catch (SqlException ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                result = false;
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return result;
        }

        public string ToJSON()
        {
            string JSONresult;
            //Console.WriteLine(roladex.All().Count);
            MemoryStream mem = new MemoryStream();
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(List<Person>));                
                ser.WriteObject(mem, roladex.All());
                JSONresult = Encoding.UTF8.GetString(mem.ToArray());
            }
            catch (Exception)
                { JSONresult = "serialization falied"; }
            finally
                { mem.Close(); }
            return JSONresult;
        }

        public IEnumerable<Person> GetPeople() { return roladex.All(); }

    }
}
