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
        public string AddTest(Test test)
        {
            CheckTest(test);
            Test temp = new Test(test)
            {
                NumTest = (Configuration.num++).ToString("00000000")
            };
            DataSource.tests.Add(temp);
            return new Test(temp).NumTest;
        }
        
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

        
        public List<Tester> TestersCollection()
        {
            return (from item in DataSource.testers select new Tester(item)).ToList();
        }
        public List<Test> TestsCollection()
        {
            return (from item in DataSource.tests select new Test(item)).ToList();
        }
        public List<Trainee> TraineesCollection()
        {
            return  (from item in DataSource.trainees select new Trainee(item)).ToList();
            
        }

        public void Update(Test test)
        {
            CheckTest(test);
            if (!DataSource.tests.Exists(T => T.CompareTo(test) == 0))
            {
                throw new Exception("the test isnt exist");
            }
                DataSource.tests[DataSource.tests.IndexOf(DataSource.tests.Find(T => T.CompareTo(test) == 0))] = new Test(test);
            
        }
        public void UpdateTester(Tester tester)
        {
            CheckTester(tester);
            if (!DataSource.testers.Exists(T => T.CompareTo(tester) == 0))
            {
                throw new Exception("This tester doesn't exist");
            }
            DataSource.testers[DataSource.testers.IndexOf(DataSource.testers.Find(T => T.CompareTo(tester) == 0))] = new Tester(tester);
            
        }
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
        public static void CheckTrainee(Trainee trainee)
        {

            if (!trainee.PrivateName.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception("שם פרטי אינו תקין");

            if (!trainee.FamilyName.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception("שם משפחה אינו תקין");

            if ((trainee.Id != null) && !IdCheck(trainee.Id))
                throw new Exception("מספר תעודת זהות אינו תקין");

            if (!trainee.DrivingSchool.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception("שם בית הספר אינו תקין");

            if (!trainee.DrivingTeacher.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception("שם המורה אינו תקין");

            if (trainee.Phone.Length != 10 || !trainee.Phone.All(Char.IsDigit))
                throw new Exception("מספר הפלאפון אינו תקין");

            if (trainee.DLessonPast < 0)
                throw new Exception("מספר השיעורים שעברת אינו תקין");

            if (!trainee.Address.City.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception("שם העיר אינו תקין");

            if (!trainee.Address.Street.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception("שם הרחוב אינו תקין");

            if (!trainee.Address.NumOfHome.All(char.IsDigit))
                throw new Exception("מספר הבית אינו תקין");

        }
        public static void CheckTester(Tester tester)
        {

            if (!tester.PrivateName.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception("שם פרטי אינו תקין");

            if (!tester.FamilyName.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception("שם המשפחה אינו תקין");

            if ((tester.Id != null) && !IdCheck(tester.Id))
                throw new Exception("מספר תעודת זהות אינו תקין");

            if (tester.Phone.Length != 10 || !tester.Phone.All(Char.IsDigit))
                throw new Exception("מספר הפלאפון אינו תקין");

            if (tester.MaxTests < 0)
                throw new Exception("מספר המבחנים המקסימאלי אינו תקין");

            if (!tester.Address.City.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception("שם העיר אינו תקין");

            if (!tester.Address.Street.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception("שם הרחוב אינו תקין");
            if (!tester.Address.NumOfHome.All(char.IsDigit))
                throw new Exception("מספר הבית אינו תקין");

        }
        public static void CheckTest(Test test)
        {
            if (test.TestTime.All(char.IsLetter))
                throw new Exception(test + ":תאריך לא נכון");
            if (test.TestTime != test.TestDay.Day + "/" + test.TestDay.Month + "/" + test.TestDay.Year)
                throw new Exception(test + ":התאריכים לא זהים");
            if ((test.IdTester != null) && !IdCheck(test.IdTester))
                throw new Exception(test + ":ת'ז בוחן שגוי");
            if ((test.IdTrainee != null) && !IdCheck(test.IdTrainee))
                throw new Exception(test + ":ת'ז נבחן שגוי");
            if (!test.TestAddress.Street.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception(test + ":שם רחוב שגוי");

            if (!test.TestAddress.City.All(x => char.IsLetter(x) || x == ' '))
                throw new Exception(test + ":שם עיר שגוי");
            if (!test.TestAddress.NumOfHome.All(char.IsDigit))
                throw new Exception(test + ":מספר בית שגוי");
            foreach (Criterion item in test.Criterions)
            {
                if (!item.name.All(x => char.IsLetter(x) || x == ' '))
                {
                    throw new Exception("!שגוי" + test + "-ב" + item.name + ":שמו של הקריטריון");
                }
            }
        }
        public IEnumerable<Tester> testersByName()
        {
            return from item in TestersCollection() orderby item.ToString() select new Tester(item);
        }
    }
}
