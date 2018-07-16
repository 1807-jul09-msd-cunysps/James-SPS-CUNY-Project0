using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalindromLibrary
{
    public static class Palindrome
    {
        public static bool isPalindrome(string word)
        {
            //Console.WriteLine(word);

            char[] chars = word.ToLower().ToArray();
            return isPalindrome(chars, 0, word.Length);
        }
        private static bool isPalindrome(char[] word, int start, int end)
        {
            //Console.WriteLine(word);
            if (end - start < 2) return true;
            if (word[start] == word[end - 1])
            {
                return isPalindrome(word, start+1, end-1);
            }
            if (word[start] == ' ') return isPalindrome(word, start + 1, end);
            if (word[end-1] == ' ') return isPalindrome(word, start, end - 1);
            return false;


        }
    }
}
