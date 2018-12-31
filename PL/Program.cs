using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;
namespace PL
{
    class Program
    {
        #region the past...
        /*
        private BL.IBL myBl = FactoryBL.getBl();
        static void Print()
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
            int input = 18;
            bool flag = true;
            while (flag)
            {  try
                {
                    input = int.Parse(Console.ReadLine());
                    flag = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("please enter only numbers");
                }
             }
          
            while (input != 18)
            {
                switch (input)
                {
                    case 1:
                        Console.WriteLine("enter id:");

                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    case 11:
                        break;
                    case 12:
                        break;
                    case 13:
                        break;
                    case 14:
                        break;
                    case 15:
                        break;
                    case 16:
                        break;
                    case 17:
                        break;

                }
            }
            Console.WriteLine("good bye");
        }
        */
        #endregion
        bool[,] worktable = new bool[6, 5]
           {
                { true, true, true, true, true},
                { true, true, true, true, true},
                { true, true, true, true, true},
                { true, true, true, true, true},
                { true, true, true, true, true},
                { true, true, true, true, true},
           };
        Tester tester1 = new Tester("212384507", "Levi", "Asaf", new DateTime(1990, 2, 2), Gender.Male, "0503363230", new Address { City = "Jer", Street = "somewhere", NumOfHome = 11 }, 19, 20, Car.PrivateCar, worktable, 10),
               tester2 = new Tester("323947747", "Garber", "Shmuel", new DateTime(1980, 5, 10), Gender.Male, "0503363230", new Address { }, 19, 20, Car.PrivateCar, worktable, 10);
        Trainee trainee1 = new Trainee("058371246", "Renana", "Levi", Gender.Female, "123456789", new Address(), new DateTime(), Car.PrivateCar, Gearbox.automatic, ),
             trainee2 = new Trainee();
    }
}
