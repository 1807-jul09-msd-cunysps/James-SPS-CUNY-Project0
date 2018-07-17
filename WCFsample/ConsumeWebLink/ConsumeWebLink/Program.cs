using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumeWebLink.ServiceReference1;

namespace ConsumeWebLink
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's use a calculator from the internet!");
            Console.WriteLine("W");

            
            CalculatorSoapClient client = new CalculatorSoapClient();
            Console.WriteLine(client.Add(21, 22));
            Console.Read();
        }
    }
}
