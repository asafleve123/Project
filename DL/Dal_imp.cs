using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
namespace DAL
{
    class Dal_imp : Idal
    {
        /// <summary>
        /// Func that add a test to  the system
        /// </summary>
        /// <param name="test"></param>
        public void AddTest(Test test)
        {
            CheckTest(test);
            Test temp = new Test(test)
            {
                NumTest = (Configuration.num++).ToString("00000000")
            };
            DataSource.tests.Add(temp);
        }
        /// <summary>
        /// Func that add a test to the system
        /// </summary>
        /// <param name="tester"></param>
        public void AddTester(Tester tester)
        {
            CheckTester(tester);
            Tester temp = new Tester(tester);
               if( DataSource.testers.Exists(T => T.CompareTo(temp) == 0))
                    throw new Exception("This tester already exist");
                DataSource.testers.Add(temp);
        }
        public void AddTrainee(Trainee trainee)
        {
            CheckTrainee(trainee);
            Trainee temp = new Trainee(trainee);
                if(DataSource.trainees.Exists(T => T.CompareTo(temp) == 0))
                    throw new Exception("This Trainee already exist");
                DataSource.trainees.Add(temp);
        }
        public void DeleteTester(Tester tester)
        {
            if (!DataSource.testers.Exists(T=>T.CompareTo(tester)==0))
            {
                throw new Exception("The tester isn't found");
            }
                DataSource.testers.Remove(DataSource.testers.Find(T => T.CompareTo(tester) == 0));
            
        }
        public void DeleteTrainee(Trainee trainee)
        {
            if (!DataSource.trainees.Exists(T=>T.CompareTo(trainee)==0))
            {
                throw new Exception("The trainee isn't found");
            }
                DataSource.trainees.Remove(DataSource.trainees.Find(T => T.CompareTo(trainee) == 0));
            
        }

        /// <summary>
        /// Func that return the Testers 
        /// </summary>
        /// <returns></returns>
        public List<Tester> TestersCollection()
        {
            return (from item in DataSource.testers select new Tester(item)).ToList();
        }
        /// <summary>
        /// Func that return the Tests
        /// </summary>
        /// <returns></returns>
        public List<Test> TestsCollection()
        {
            return (from item in DataSource.tests select new Test(item)).ToList();
        }
        /// <summary>
        /// func that return the Trainees
        /// </summary>
        /// <returns></returns>
        public List<Trainee> TraineesCollection()
        {
            return  (from item in DataSource.trainees select new Trainee(item)).ToList();
            
        }

        /// <summary>
        /// Func that update a  test
        /// </summary>
        /// <param name="test"></param>
        public void Update(Test test)
        {
            CheckTest(test);
            if (!DataSource.tests.Exists(T => T.CompareTo(test) == 0))
            {
                throw new Exception("the test isnt exist");
            }
                DataSource.tests[DataSource.tests.IndexOf(DataSource.tests.Find(T => T.CompareTo(test) == 0))] = new Test(test);
            
        }
        /// <summary>
        /// func that Update Tester
        /// </summary>
        /// <param name="tester"></param>
        public void UpdateTester(Tester tester)
        {
            CheckTester(tester);
            if (!DataSource.testers.Exists(T => T.CompareTo(tester) == 0))
            {
                throw new Exception("This tester doesn't exist");
            }
            DataSource.testers[DataSource.testers.IndexOf(DataSource.testers.Find(T => T.CompareTo(tester) == 0))] = new Tester(tester);
           
        }
        /// <summary>
        /// func thatt Update aTrainee
        /// </summary>
        /// <param name="trainee"></param>
        public void UpdateTrainee(Trainee trainee)
        {
            CheckTrainee(trainee);
            if (!DataSource.trainees.Exists(T => T.CompareTo(trainee) == 0))
            {
                throw new ArgumentNullException("This trainee doesn't exist");
            }
            DataSource.trainees[DataSource.trainees.IndexOf(DataSource.trainees.Find(T => T.CompareTo(trainee) == 0))] = new Trainee(trainee);
            
        }

        /// <summary>
        /// func that check if the Id is correct
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IdCheck(string id)
        {
            if (id == null)
                return false;

            int tmp, count = 0;

            if (!(int.TryParse(id, out tmp)) || id.Length != 9)
                return false;

            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            id = id.PadLeft(9, '0');

            for (int i = 0; i < 9; i++)
            {
                int num = Int32.Parse(id.Substring(i, 1)) * id_12_digits[i];

                if (num > 9)
                    num = (num / 10) + (num % 10);

                count += num;
            }

            return (count % 10 == 0);
        }
        //functions that check the correctness of the paremters enter to the system.

        public static void CheckTrainee(Trainee trainee)
        {
            if (!IdCheck(trainee.Id))
                throw new Exception(trainee + ":the ID of the trainee isn't good");

            if (!trainee.PrivateName.All(Char.IsLetter))
                throw new Exception(trainee + ":the Private Name of the trainee isn't good");

            if (!trainee.FamilyName.All(Char.IsLetter))
                throw new Exception(trainee + ":the Family Name of the trainee isn't good");

            if (!trainee.DrivingSchool.All(Char.IsLetter))
                throw new Exception(trainee + ":the Driving School Name have to be only letters!");

            if (!trainee.DrivingTeacher.All(Char.IsLetter))
                throw new Exception(trainee + ":the Driving Teacher Name have to be only letters!");

            if (trainee.Phone.Length != 10 || !trainee.Phone.All(Char.IsDigit))
                throw new Exception(trainee + ":the Phone Number of the trainee isn't good");

            if (trainee.DLessonPast < 0)
                throw new Exception(trainee + ":the number of lesson cant be a negative number");
            if (!trainee.Address.Street.All(char.IsLetter))
                throw new Exception(trainee + ":Wrong  street name!");

            if (!trainee.Address.City.All(char.IsLetter))
                throw new Exception(trainee + ":Wrong city name!");
        }
        public static void CheckTester(Tester tester)
        {
            if (!IdCheck(tester.Id))
                throw new Exception(tester + ":the ID of the trainee isn't good");

            if (!tester.PrivateName.All(Char.IsLetter))
                throw new Exception(tester + ":the Private Name of the tester isn't good");

            if (!tester.FamilyName.All(Char.IsLetter))
                throw new Exception(tester + ":the Family Name of the tester isn't good");

            if (tester.Phone.Length != 10 || !tester.Phone.All(Char.IsDigit))
                throw new Exception(tester + ":the Phone Number of the tester isn't good");

            if (tester.MaxTests < 0)
                throw new Exception(tester + ":the maximum of tests in a week cant be negative number");

            if (tester.MaxRange < 0)
                throw new Exception(tester + ":the Max Range cant be  a negative number");

            if (tester.Years < 0)
                throw new Exception(tester + ":the Years of experience cant be  a negative number");

            if (!tester.Address.Street.All(char.IsLetter))
                throw new Exception(tester + ":Wrong  street name!");

            if (!tester.Address.City.All(char.IsLetter))
                throw new Exception(tester + ":Wrong city name!");
        }
        public static void CheckTest(Test test)
        {
            if (test.TestTime.All(char.IsLetter))
                throw new Exception(test + ":wrong test time");
            if (test.TestTime != test.TestDay.Day + "/" + test.TestDay.Month + "/" + test.TestDay.Year)
                throw new Exception(test + ":the dates arent same");
            if ((test.IdTester != null) && !IdCheck(test.IdTester))
                throw new Exception(test + ":Wrong id tester!");
            if (!IdCheck(test.IdTrainee))
                throw new Exception(test + ":Wrong id trainee!");
            if (!test.TestAddress.Street.All(char.IsLetter))
                throw new Exception(test + ":Wrong street name!");

            if (!test.TestAddress.City.All(char.IsLetter))
                throw new Exception(test + ":Wrong city name!");
            foreach (Criterion item in test.Criterions)
            {
                if (!item.name.All(char.IsLetter))
                {
                    throw new Exception(test + ":wrong Criterion " + item.name + "!");
                }
            }
        }
    }
}
