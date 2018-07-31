using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ContactLibrary
{
    public class Roladex
    /// this is a collection of people/contacts. Why three forward-slashes? I don't know.
    {
        List<Person> contacts;
        Dictionary<long, int> IDs;
        public Roladex()
        {
            contacts = new List<Person>();
            IDs = new Dictionary<long, int>();
        }
        public int Count { get { return contacts.Count; }  } //represents the size/length of the roladex
        
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

        public long[] Search (string parameterName, string search_term)
            ///accepts "firstName", "lastName", "zipcode", "city", "phone" as <param name="parameterName"/>
            ///<returns>an array of PID values</returns>
        {
            List<long> results = new List<long>();
            switch (parameterName.ToLower())
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

        private Person GetPerson ( long PID)
        {
            return contacts[IDs[PID]];
        }

        public bool Update (long PID, string field, object new_data)
            ///<param name="field">accepts 'firstName', 'lastName', 'zipCode', 'city', 'phone', 'address', case-insensitive</param>
        {
            Person person = GetPerson(PID);
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

        public bool Remove (int ID)
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
//                    Console.WriteLine(dr[0].GetType());
                    Person person = new Person((long)(int)dr[0]) 
//why the fuck do I need to cast it to an int (when it's already an int) before casting it to a long? 
                    {
                        firstName = (string)dr[1],
                        lastName = (string)dr[2]
                    };
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
                    command = "insert into People values ('" + person.firstName + "', '"
                        + person.lastName + "')";
                    cmd = new SqlCommand(command, con);
                    cmd.ExecuteNonQuery();
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


    }
}
