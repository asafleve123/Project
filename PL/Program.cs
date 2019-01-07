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
                Trainee trainee3 = new Trainee("211780382", "Yosseg", "Kdtri", Gender.Female, "1234567890", new Address { City = "Jer", Street = "somewhere", NumOfHome = 22 }, new DateTime(1940, 12, 12), Car.PrivateCar, Gearbox.automatic, "toryarok", "Shimi", 20, false);
                Trainee trainee4 = new Trainee("212147870", "rrrr", "aaaaa", Gender.Female, "1234567890", new Address { City = "Jer", Street = "somewhere", NumOfHome = 22 }, new DateTime(1940, 12, 12), Car.PrivateCar, Gearbox.automatic, "torahavomadaha", "David", 30, false);

                Test test2 = new Test(trainee2, new DateTime(2019, 2, 10,13,0,0), trainee2.Address);
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
                
                test2.NumTest=myBl.AddTest(test2);
                Console.WriteLine("-------------Tests-----------");
                var tests = myBl.TestsCollection();
                foreach (var item in tests)
                    Console.WriteLine(item);
                tester1.FamilyName = "cohen";
                tester2.FamilyName = "levi";
                tester2.Gender =Gender.Female;
                myBl.UpdateTester(tester1);
                myBl.UpdateTester(tester2);
                Console.WriteLine("-------------Testers-----------");
                testers = myBl.TestersCollection();
                foreach (var item in testers)
                    Console.WriteLine(item+"      "+item.Gender);
                trainee1.TypeOfCar = Car.BigTruck;
                trainee1.PrivateName = "Dafna";
                trainee2.Gender = Gender.Male;
                myBl.UpdateTrainee(trainee1);
                myBl.UpdateTrainee(trainee2);

                Console.WriteLine("-------------Trainees-----------");
                trainees = myBl.TraineesCollection();
                foreach (var item in trainees)
                    Console.WriteLine(item + "      " + item.Gender);

                Test t1 = new Test(trainee1,new DateTime(2019,2,4,13,0,0),trainee1.Address);
                #region try to add
                try
                {
                t1.NumTest=myBl.AddTest(t1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                #endregion
                Tester tester3 = new Tester("323947739", "Levi", "Tran", new DateTime(1950, 2, 2), Gender.Male, "0503363230", new Address { City = "Jer", Street = "somewhere", NumOfHome = 11 }, 19, 20, Car.BigTruck, worktable, 10);
                myBl.AddTester(tester3);
                Console.WriteLine("-------------Testers-----------");
                testers = myBl.TestersCollection();
                foreach (var item in testers)
                    Console.WriteLine(item + "      " + item.Gender);
                t1.NumTest=myBl.AddTest(t1);
                Console.WriteLine("-------------Tests-----------");
                tests = myBl.TestsCollection();
                foreach (var item in tests)
                    Console.WriteLine(item);
                #region try to remove someone who have test in a future
                try
                {
                    myBl.DeleteTester(tester3);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                #endregion

                Console.WriteLine("-------------Testers-----------");
                testers = myBl.TestersCollection();
                foreach (var item in testers)
                    Console.WriteLine(item + "      " + item.Gender);

                Console.WriteLine("-------------Trainees-----------");
                trainees = myBl.TraineesCollection();
                foreach (var item in trainees)
                    Console.WriteLine(item + "      " + item.Gender);
                myBl.DeleteTrainee(trainee1);

                Console.WriteLine("-------------Trainees-----------");
                trainees = myBl.TraineesCollection();
                foreach (var item in trainees)
                    Console.WriteLine(item + "      " + item.Gender);
                try
                {
                    myBl.DeleteTrainee(trainee2);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("-------------Trainees-----------");
                trainees = myBl.TraineesCollection();
                foreach (var item in trainees)
                    Console.WriteLine(item + "      " + item.Gender);
                test2.Grade = Grade.pass;
                test2.TestDay = new DateTime(2001, 2, 10, 13, 0, 0);
                test2.TestTime = test2.TestDay.Day + "/" + test2.TestDay.Month + "/" + test2.TestDay.Year;

                myBl.Update(test2);
                Console.WriteLine("-------------Tests-----------");
                tests = myBl.TestsCollection();
                foreach (var item in tests)
                    Console.WriteLine(item+"  "+item.Grade+" "+item.TestTime);
                Tester tester4 = new Tester("323947739", "Levi", "Tran", new DateTime(1950, 2, 2), Gender.Male, "0503363230", new Address { City = "Jer", Street = "somewhere", NumOfHome = 11 }, 19, 20, Car.BigTruck, worktable, 10);
                myBl.AddTester(tester4);
                Console.WriteLine("------------Tester------------");
                IEnumerable<Tester> testers1 = myBl.DistanseFromAdress(trainee2.Address);
                foreach (var item in testers1 )
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("------------Tester------------");
                IEnumerable<Tester> testers2 = myBl.IsFree(new DateTime(1995,5,4,12,0,0));
                foreach (var item in testers2)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("------------Test------------");
                IEnumerable<Test> tests1=myBl.AllTestsBy(T => T.Grade == Grade.pass);
                foreach (Test item in tests1)
                {
                    Console.WriteLine(item);
                }
                //the two next functions in  had used in that code.
                myBl.AddTrainee(trainee3);
                myBl.AddTrainee(trainee4);
                Test test4 = new Test(trainee3, new DateTime(2019, 2, 12, 9, 0, 0), trainee3.Address);
                test4.NumTest=myBl.AddTest(test4);
                Console.WriteLine("-------------Tests-----------");
                tests = myBl.ListByDay();
                foreach (var item in tests)
                    Console.WriteLine(item+" "+item.TestTime);
                Console.WriteLine("------------Tester------------");
                var testers5 = myBl.ListOfTestersByCar(true);
                foreach (var items in testers5)
                {
                    switch (items.Key)
                    {
                        case Car.BigTruck:
                            Console.WriteLine("BigTruck:");
                            foreach (var item in items)
                            {
                                Console.WriteLine("       "+item);
                            }
                            break;
                        case Car.MediumTruck:

                            Console.WriteLine("MediumTruck:");
                            foreach (var item in items)
                            {
                                Console.WriteLine("       " + item);
                            }
                            break;
                        case Car.PrivateCar:

                            Console.WriteLine("PrivateCar:");
                            foreach (var item in items)
                            {
                                Console.WriteLine("       " + item);
                            }
                            break;
                        case Car.TwoWheeledCar:

                            Console.WriteLine("TwoWheeledCar:");
                            foreach (var item in items)
                            {
                                Console.WriteLine("       " + item);
                            }
                            break;
                    }
                }
                Console.WriteLine("-------------Trainees-----------");
                var trainees5 = myBl.ListOfTraineesByDTeacher(true);
                foreach (var items in trainees5)
                {
                            Console.WriteLine(items.Key);
                            foreach (var item in items)
                            {
                                Console.WriteLine("       " + item);
                            }
                }
                Console.WriteLine("-------------Trainees-----------");
                var trainees6 = myBl.ListOfTraineesByNumOfTests(true);
                foreach (var items in trainees6)
                {
                    Console.WriteLine(items.Key);
                    foreach (var item in items)
                    {
                        Console.WriteLine("       " + item);
                    }
                }
                Console.WriteLine("-------------Trainees-----------");
                var trainees7= myBl.ListOfTraineesBySchool(true);
                foreach (var items in trainees7)
                {
                    Console.WriteLine(items.Key);
                    foreach (var item in items)
                    {
                        Console.WriteLine("       " + item);
                    }
                }
            }
            catch (Exception exp) 
            {
                Console.WriteLine(exp.Message+" "+exp.GetType());
            }
    Console.ReadKey();
        }
        */
        #endregion maybe
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
            Tester tester1 = new Tester("212384507", "Levi", "Asaf", new DateTime(1990, 2, 2), Gender.Male, "0503363230", new Address{City="Jer", Street="somewhere",number= 11 },19,20,Car.PrivateCar, worktable, 10), 
                   tester2=new Tester("323947747", "Garber", "Shmuel", new DateTime(1980, 5, 10), Gender.Male, "0503363230", new Address { }, 19, 20, Car.PrivateCar, worktable, 10);
            Trainee trainee1 = new Trainee("058371246","Renana","Levi",Gender.Female,"123456789",new Address(),new DateTime(),Car.PrivateCar,Gearbox.automatic, ) , 
                 trainee2= new Trainee();
                

        }
    }
}
