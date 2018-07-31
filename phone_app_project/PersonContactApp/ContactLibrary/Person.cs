using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using  


namespace ContactLibrary
{
    public enum State
    {
        NY, FL, VA, MD, SF, OH
    }
    public enum Country
    {
        US = 1, UK = 44, India = 91, Pakistan = 92, Australia = 61
    }
    public class Person
    {
        static Random r = new Random();
        public Person() : this(Person.r.Next())
        {
        }
        public Person(string name) : this()
        {
            this.firstName = name;
        }
        public Person(long PID)
        {
            this.Pid = PID;
            address = new Address();
            phone = new Phone();
        }
        public Person(string firstName, string lastName) : this(firstName) { this.lastName = lastName; }
        public long Pid { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Address address { get; set; }
        public Phone phone { get; set; }

        public override string ToString()
        {
            return Pid.ToString()+"\t\t"+firstName+"\t\t"+lastName;
        }

        //public operator = (Person a){

         
        public  bool Equals(Person obj) 
//note to self -- the "override" modifier is inappropriate because
//  this parameter (type Person) differs in type from the parameter
//  of the inherited Equals method of the Object class (type Object)
        {
            //Console.WriteLine("test the Equals method on its own please." +
            //    " Press any key to continue.");
            //Console.Read();
            return (
                obj.Pid == this.Pid
                //&& obj.firstName == this.firstName
                //&& obj.lastName == this.lastName
                //&& obj.address.Equals(this.address)
                //&& obj.phone.Equals(this.phone)
                );
        }
        

        public List<Person> Get()
        {

            Person p1 = new Person();
            p1.firstName = "Tammy";
            p1.lastName = "Johnson";
            p1.Pid = DateTime.Now.Ticks;
            p1.address.houseNum = "121";
            p1.address.Pid = p1.Pid;
            p1.address.street = "1st";
            p1.address.city = "New York";
            p1.address.State = State.NY;
            p1.address.Country = Country.US;
            p1.address.zipcode = "10017";
            p1.phone.Pid = p1.Pid;
            p1.phone.areaCode = "204";
            p1.phone.countrycode = Country.US;
            p1.phone.ext = "";
            p1.phone.number = "564456";


            Person p2 = new Person();
            p2.firstName = "Sammy";
            p2.lastName = "Demier";
            p2.Pid = DateTime.Now.Ticks;
            p2.address.houseNum = "123";
            p2.address.Pid = p2.Pid;
            p2.address.street = "21st";
            p2.address.city = "New York";
            p2.address.State = State.NY;
            p2.address.Country = Country.US;
            p2.address.zipcode = "10018";
            p2.phone.Pid = p2.Pid;
            p2.phone.areaCode = "289";
            p2.phone.countrycode = Country.US;
            p2.phone.ext = "";
            p2.phone.number = "456622";

            List<Person> p = new List<Person>();
            p.Add(p1);
            p.Add(p2);
            return p;
        }
     
    }

    public class Address
    {
        public long Pid { get; set; }
        public string houseNum { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public State State { get; set; }
        public Country Country { get; set; }
        public string zipcode { get; set; }
    }

    public class Phone
    {
        public long Pid { get; set; }
        public Country countrycode { get; set; }
        public string areaCode { get; set; }
        public string number { get; set; }
        public string ext { get; set; }
        public Phone() {
        //    countrycode = new Country();
        }
        public Phone(string num):this() { this.number = num; }
        public Phone(Phone phone):this() {
            this.Pid = phone.Pid;
            this.countrycode = phone.countrycode;
            this.number = phone.number;
            this.ext = phone.ext;
        } 
        public Phone(object o) :
            this((Phone)o){}

        public override bool Equals(object obj)
        {
            if (Object.ReferenceEquals
                (obj.GetType(), "string".GetType())
                ) //is it being passed a string?
            {
                if ((string)obj == number || (string)obj == areaCode + number)
                    return true;
                else return false;
            }
            else if (Object.ReferenceEquals
                (obj.GetType(), this.GetType())
                ) //is it being passed a Phone object?
                return (this.Pid == ((Phone)obj).Pid);
            else return base.Equals(obj);
        }
    }
    
}
