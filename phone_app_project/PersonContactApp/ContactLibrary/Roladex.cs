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
        List<Person> contacts = new List<Person>();
        public int Count { get { return contacts.Count; }  } //represents the size/length of the roladex
        
        public void add (Person p)  {
            this.contacts.Add(p);
        }

        public void remove (Person P) {
            foreach (Person p in contacts)
            {
                if (p.Equals(P)) { }
//                if (p == P) { }
            }
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
