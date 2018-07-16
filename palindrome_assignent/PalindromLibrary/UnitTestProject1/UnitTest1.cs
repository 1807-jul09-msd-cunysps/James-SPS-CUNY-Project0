using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PalindromLibrary;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        string word1 = "a nut for a jar of tuna";
        string word2 = "borrow or rob";
        string word3 = "343";

        [TestMethod]
        public void TestMethod1()
        {
            bool a1 = Palindrome.isPalindrome(word1);
            //Assert a;
            Assert.AreEqual(true, a1);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.IsTrue(Palindrome.isPalindrome(word2));
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.IsTrue(Palindrome.isPalindrome(word3));
        }

    }
}
