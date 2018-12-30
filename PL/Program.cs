using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
namespace PL
{
    class Program
    {
        private BL.IBL myBl = FactoryBL.getBl();
         void Print()
        {

            Console.WriteLine("1: add a tester");
            Console.WriteLine("2: delete a tester");
            Console.WriteLine("3: update a tester");
            Console.WriteLine("4: add a trainee");
            Console.WriteLine("5: delete a trainee");
            Console.WriteLine("6: update a trainee");
            Console.WriteLine("7: add a test");
            Console.WriteLine("8: update a test");
            Console.WriteLine("9: to get all testers");
            Console.WriteLine("10: to get all trainees");
            Console.WriteLine("11: to get all tests");
            Console.WriteLine("12: to get all testers that lived in radios " + BE.Configuration.DISTANCE + " from adress");
            Console.WriteLine("13: to get all testers that free at spetific time");
            Console.WriteLine("14: to get all tests with specific condition");
            Console.WriteLine("15: to get the num of tests that are done by specific trainee");
            Console.WriteLine("16: to check if student are allowed to driving license");
            Console.WriteLine("17: to get all tests that order bt days");
            Console.WriteLine("18: exit");
        }
        static void Main(string[] args)
        {
            Print();
            int input;
            try
            {
             input = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("please enter only numbers");
            }
            while (input != 18)
            {
                switch (input)
                {
                    case 1:
                }
            }
        }
    }
}
