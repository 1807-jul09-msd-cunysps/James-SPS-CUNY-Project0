using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalindromLibrary;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool a1 = Palindrome.isPalindrome("a nut for a jar of tuna");
            bool a2 = Palindrome.isPalindrome("apple");
            bool a3 = Palindrome.isPalindrome(" I ");
            Console.WriteLine(a1);
            Console.WriteLine(a2);
            Console.WriteLine(a3);
            Console.WriteLine("Press enter to quit");
            Console.Read();
        }
    }
}
