using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactLibrary;

namespace ContactTesting
{
    [TestClass]
    public class PersonTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    
    }

    [TestClass]
    public class RoladexTest
    {
        static Roladex rol = new Roladex();
        [TestMethod]
        public void TestAdd()
        {
            rol.add(new Person("Kanisha Brown"));
            Assert.AreEqual(rol.Count, 1);
            //Assert.AreEqual(rol.p.firstName, "Kanisha");
            //Assert.AreEqual(rol.p.lastName, "Brown");

        }
    }
}
