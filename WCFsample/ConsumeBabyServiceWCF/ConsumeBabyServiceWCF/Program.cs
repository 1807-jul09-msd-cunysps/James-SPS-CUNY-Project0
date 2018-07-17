using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumeBabyServiceWCF.ServiceReference1;





namespace ConsumeBabyServiceWCF
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client client = new Service1Client();
            try
            {
                Console.WriteLine("let's fetch a boy's name");
                Console.WriteLine(client.getBabyName(true));
            }
            catch (System.ServiceModel.EndpointNotFoundException excep)
            {
                Console.WriteLine(excep);
            }
            finally
            {
                Console.WriteLine("press enter to exit");
                Console.ReadLine();
            }

        }
    }
}
