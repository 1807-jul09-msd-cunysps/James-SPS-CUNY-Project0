using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool Update (long PID, )
        {
            Person person = GetPerson(PID);

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
            string separator = " ";
            foreach (Person person in contacts) peeps += person + separator;
            return peeps;
        }



    }
}
