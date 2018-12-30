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
        public void AddTest(Test test)
        {
            Test temp = new Test(test)
            {
                NumTest = (Configuration.num).ToString("00000000")
            };
            DataSource.tests.Add(temp);
        }

        public void AddTester(Tester tester)
        {
            Tester temp = new Tester(tester);
            try
            {
                DataSource.testers.Exists(T => T.CompareTo(temp) == 0);
                //throw new Exception("This tester already exist");
            }
            catch (ArgumentNullException)
            {
                DataSource.testers.Add(temp);
            }
            
        }
        public void AddTrainee(Trainee trainee)
        {
            Trainee temp = new Trainee(trainee);
            try
            {
                DataSource.trainees.Exists(T => T.CompareTo(temp) == 0);
                //throw new Exception("This Trainee already exist");
            }
            catch (ArgumentNullException)
            {
                DataSource.trainees.Add(temp);
            }
        }
        public void DeleteTester(Tester tester)
        {
            try
            {
                DataSource.testers.Remove(DataSource.testers.Find(T => T.CompareTo(tester) == 0));
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("The tester isn't found", e);
            }
        }
        public void DeleteTrainee(Trainee trainee)
        {
            try
            {
                DataSource.trainees.Remove(DataSource.trainees.Find(T => T.CompareTo(trainee) == 0));
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("The trainee isn't found", e);
            }
        }

        public List<Tester> TestersCollection()
        {
            return (List<Tester>)from item in DataSource.testers select new Tester(item);
            /*List<Tester> Temp = new List<Tester>();
            for (int i = 0; i < DataSource.testers.Count; i++)
            {
                Temp.Add(new Tester(DataSource.testers[i]));
            }
            return Temp;*/
        }

        public List<Test> TestsCollection()
        {
            return (List<Test>)from item in DataSource.tests select new Test(item);
            /*List<Test> Temp = new List<Test>();
            for (int i = 0; i < DataSource.tests.Count; i++)
            {
                Temp.Add(new Test(DataSource.tests[i]));
            }
            return Temp;*/
        }

        public List<Trainee> TraineesCollection()
        {
            return (List<Trainee>) from item in DataSource.trainees select new Trainee(item);
            /*List<Trainee> Temp = new List<Trainee>();
            for (int i = 0; i < DataSource.trainees.Count; i++)
            {
                Temp.Add(new Trainee(DataSource.trainees[i]));
            }
            return (List<Trainee>)newList;*/
        }

        public void Update(Test test)
        {
            try
            {
                DataSource.tests[DataSource.tests.IndexOf(DataSource.tests.Find(T => T.CompareTo(test) == 0))] = new Test(test);
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("This test doesn't exist", e);
            }
        }

        public void UpdateTester(Tester tester)
        {
            try
            {
                DataSource.testers[DataSource.testers.IndexOf(DataSource.testers.Find(T => T.CompareTo(tester) == 0))] = new Tester(tester);
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("This tester doesn't exist", e);
            }
        }

        public void UpdateTrainee(Trainee trainee)
        {
            try
            {
                DataSource.trainees[DataSource.trainees.IndexOf(DataSource.trainees.Find(T => T.CompareTo(trainee) == 0))] = new Trainee(trainee);
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException("This trainee doesn't exist", e);
            }
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

            if (trainee.Phone.Length != 10 || !trainee.Phone.All(Char.IsDigit))
                throw new Exception("the Phone Number of the trainee isn't good");

            if (trainee.DLessonPast < 0)
                throw new Exception("the number of lesson cant be a negative number");
            if (!trainee.Address.Street.All(char.IsLetter))
                throw new Exception("Wrong  street name!");

            if (!trainee.Address.City.All(char.IsLetter))
                throw new Exception("Wrong city name!");
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

            if (!tester.Address.Street.All(char.IsLetter))
                throw new Exception("Wrong  street name!");

            if (!tester.Address.City.All(char.IsLetter))
                throw new Exception("Wrong city name!");
        }
        public static void CheckTest(Test test)
        {
            if (test.TestTime.All(char.IsLetter))
                throw new Exception("wrong test time");
            if (test.TestTime != test.TestDay.Day + "/" + test.TestDay.Month + "/" + test.TestDay.Year)
                throw new Exception("the dates arent same");
            if (!IdCheck(test.IdTester))
                throw new Exception("Wrong id tester!");
            if (!IdCheck(test.IdTrainee))
                throw new Exception("Wrong id trainee!");
            if (!test.TestAddress.Street.All(char.IsLetter))
                throw new Exception("Wrong street name!");

            if (!test.TestAddress.City.All(char.IsLetter))
                throw new Exception("Wrong city name!");
            foreach (Criterion item in test.Criterions)
            {
                if (!item.name.All(char.IsLetter))
                {
                    throw new Exception("wrong Criterion " + item.name + "!");
                }
            }
        }
    }
}
