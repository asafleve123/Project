using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
namespace BL
{
    public class MyBl : IBL
    {
        private DAL.Idal MyDal = FactoryDal.getDal();
        public void AddTest(Test test)
        {
            /*int index;
            index = (TraineesCollection()).FindIndex(item => item.Id == test.IdTrainee);
            if (index == -1)
                throw new Exception("there isnt any trainee with that ID");
            Trainee trainee = (TraineesCollection())[index];
            */
            var StudentTests = from item in TestsCollection() where (test.IdTrainee == item.IdTrainee) select item;
            
            var Trainee = from item in TraineesCollection() where (test.IdTrainee == item.Id) select item;
            Trainee trainee = Trainee.First();

            Test PreTest = Test.GetTestTemp(new DateTime(trainee.DOB.Year, trainee.DOB.Month, trainee.DOB.Day));
            foreach (var item in StudentTests)
                if (PreTest.TestDay < item.TestDay)
                    PreTest = item;
            
            if ((test.TestDay - PreTest.TestDay).Days < Configuration.RANGE_BETWEEN_TESTS)
                throw new Exception("it's too early to take a new test");
            
            if (trainee.DLessonPast < Configuration.MIN_NUMBER_OF_LESSONS)
                throw new Exception("You need to do "+(Configuration.MIN_NUMBER_OF_LESSONS - trainee.DLessonPast)+" lessons");

            /*try
            {
                if ((test.TestDay.Hour >= Configuration.MIN_HOUR && test.TestDay.Hour < Configuration.MAX_HOUR) || (int)test.TestDay.DayOfWeek <= Configuration.THURSDAY) 
                {
                    test.IdTester = (TestersCollection()).Find(T => T.WorkTable[test.TestDay.Hour - Configuration.MIN_HOUR, (int)test.TestDay.DayOfWeek]).Id;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch (ArgumentNullException)
            {
                throw new Exception("There is'nt tester that free");
            }*/

            IEnumerable<Tester> testers = TestersCollection();
            testers = from item in testers where item.WorkTable[test.TestDay.Hour - Configuration.MIN_HOUR, (int)test.TestDay.DayOfWeek] select item;

            testers = from tester in testers
                      let id = tester.Id
                      where tester.MaxTests > (TestsCollection()).Count(tst => id == tst.IdTester) select tester;


            MyDal.AddTest(test);
        }

        public void AddTester(Tester tester)
        {
            if (tester.Age < Configuration.MIN_AGE_TESTER)
            {
                throw new Exception("You are too younger");
            }
            MyDal.AddTester(tester);
        }
        public void AddTrainee(Trainee trainee)
        {
            if (trainee.Age < Configuration.MIN_AGE_TRAINEE)
            {
                throw new Exception("You are too younger");
            }
            MyDal.AddTrainee(trainee);
        }

        public void DeleteTester(Tester tester)
        {
            throw new NotImplementedException();
        }
        public void DeleteTrainee(Trainee trainee)
        {
            throw new NotImplementedException();
        }

        public List<Tester> TestersCollection()
        {
            throw new NotImplementedException();
        }
        public List<Test> TestsCollection()
        {
            throw new NotImplementedException();
        }
        public List<Trainee> TraineesCollection()
        {
            throw new NotImplementedException();
        }

        public void Update(Test test)
        {
            throw new NotImplementedException();
        }
        public void UpdateTester(Tester tester)
        {

            throw new NotImplementedException();
        }
        public void UpdateTrainee(Trainee trainee)
        {
            throw new NotImplementedException();
        }

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
        public static void CheckTrainee(Trainee trainee)
        {
            if (!IdCheck(trainee.Id))
                throw new Exception("the ID of the trainee isn't good");

            if (!trainee.PrivateName.All(Char.IsLetter))
                throw new Exception("the Private Name of the trainee isn't good");

            if (!trainee.FamilyName.All(Char.IsLetter))
                throw new Exception("the Family Name of the trainee isn't good");

            if (!trainee.DrivingSchool.All(Char.IsLetter))
                throw new Exception("the Driving School Name isn't good");

            if (!trainee.DrivingTeacher.All(Char.IsLetter))
                throw new Exception("the Driving Teacher Name isn't good");

            if (trainee.Phone.Length!=10 || !trainee.Phone.All(Char.IsDigit))
                throw new Exception("the Phone Number of the trainee isn't good");

            if (trainee.DLessonPast < 0)
                throw new Exception("the number of lesson cant be a negative number");
        }
        public static void CheckTester(Tester tester)
        {
            if (!IdCheck(tester.Id))
                throw new Exception("the ID of the trainee isn't good");

            if (!tester.PrivateName.All(Char.IsLetter))
                throw new Exception("the Private Name of the tester isn't good");

            if (!tester.FamilyName.All(Char.IsLetter))
                throw new Exception("the Family Name of the tester isn't good");

            if (tester.Phone.Length != 10 || !tester.Phone.All(Char.IsDigit))
                throw new Exception("the Phone Number of the tester isn't good");

            if (tester.MaxTests < 0)
                throw new Exception("the maximum of tests in a week cant be negative number");

            if (tester.MaxRange < 0)
                throw new Exception("the Max Range cant be  a negative number");

            if (tester.Years < 0)
                throw new Exception("the Years of experience cant be  a negative number");
        }
        public static void CheckTest(Test test)
        {
            /*
             לבדוק שהשעה נמצאת בתחום הנכון של הטסטרים
            TestDay = test.TestDay;
            TestAddress = test.TestAddress;
            Grade = test.Grade;
            NumTest = string.Copy(test.NumTest);
            IdTester = string.Copy(test.IdTester);
            IdTrainee = string.Copy(test.IdTrainee);
            TestTime = string.Copy(test.TestTime);
            test.Comments
            
            */
        }
    }
}
