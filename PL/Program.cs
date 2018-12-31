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
        static BL.IBL myBl = FactoryBL.getBl();
      

        static void Main(string[] args)
        {

            bool[,] worktable = new bool[6, 5]
            {
                { true, true, true, true, true},
                { true, true, true, true, true},
                { true, true, true, true, true},
                { true, true, true, true, true},
                { true, true, true, true, true},
                { true, true, true, true, true},
              };
            try
            {
                Tester tester1 = new Tester("212384507", "Levi", "Asaf", new DateTime(1950, 2, 2), Gender.Male, "0503363230", new Address { City = "Jer", Street = "somewhere", NumOfHome = 11 }, 19, 20, Car.PrivateCar, worktable, 10),
                       tester2 = new Tester("323947747", "Garber", "Shmuel", new DateTime(1950, 5, 10), Gender.Male, "0503363230", new Address { City = "Netania", Street = "somewhere", NumOfHome = 87 }, 19, 50, Car.PrivateCar, worktable, 20);
                Trainee trainee1 = new Trainee("058371246", "Renana", "Levi", Gender.Female, "1234567890", new Address { City = "Jer", Street = "somewhere", NumOfHome = 22 }, new DateTime(1940, 12, 12), Car.PrivateCar, Gearbox.automatic, "toryarok", "moshe", 30, false);
                Trainee trainee2 = new Trainee("322263310", "Yossef", "Katri", Gender.Female, "1234567890", new Address { City = "Jer", Street = "somewhere", NumOfHome = 22 }, new DateTime(1940, 12, 12), Car.PrivateCar, Gearbox.automatic, "toryarok", "Shimi", 20, false);
                Test test1 = new Test(trainee1, new DateTime(2019, 1, 2), trainee1.Address);
                Test test2 = new Test(trainee2, new DateTime(2019, 2, 10), trainee2.Address);
                myBl.AddTester(tester1);
                myBl.AddTester(tester2);
                Console.WriteLine("-------------Testers-----------");
                var testers = myBl.TestersCollection();
                foreach (var item in testers)
                    Console.WriteLine(item+"      "+item.Gender);
                myBl.AddTrainee(trainee1);
                myBl.AddTrainee(trainee2);
                Console.WriteLine("-------------Trainees-----------");
                var trainees = myBl.TraineesCollection();
                foreach (var item in trainees)
                    Console.WriteLine(item);
                
                myBl.AddTest(test1);
                Console.WriteLine("-------------Tests-----------");
                var tests = myBl.TestsCollection();
                foreach (var item in tests)
                    Console.WriteLine(item);
                tester1.FamilyName = "cohen";
                tester2.FamilyName = "levi";
                tester2.Gender =Gender.Female;
                myBl.UpdateTester(tester1);
                myBl.UpdateTester(tester2);
                Console.WriteLine("-------------Testers-after-changes-----------");
                 testers = myBl.TestersCollection();
                foreach (var item in testers)
                    Console.WriteLine(item+"      "+item.Gender);
                trainee1.TypeOfCar = Car.BigTruck;
                myBl.UpdateTrainee(trainee1);
                Test t1 = new Test(trainee1,new DateTime(2019,2,4),trainee1.Address);
                try
                {

                    myBl.AddTest(t1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception exp) 
            {
                Console.WriteLine(exp.Message+exp.GetType());
            }
            Console.ReadKey();
        }
    }
}
