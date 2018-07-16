using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactLibrary
{
    public class Roladex
    /// this is a collection of people/contacts
    {
        List<Person> contacts = new List<Person>();
        
        public void add (Person p)        {            this.contacts.Add(p);       }

        public void remove (Person P) {  }

        public override string ToString()
        {
            string peeps = "";
            foreach (Person person in contacts) peeps += " " + person;

            return peeps;
        }


    }
}
