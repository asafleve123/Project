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
        
        private int NumOfTestsByDays(Tester tester, DateTime time)
        {
            return (TestsCollection()).Count(delegate (Test tst) { if (tester.Id == tst.IdTester && DatesAreInTheSameWeek(time, tst.TestDay)) return true; return false; });        
        }
        private bool IsHeFree(Tester item, DateTime time)
        {
            if ((int)time.DayOfWeek > Configuration.THURSDAY || (time.Hour < Configuration.MIN_HOUR || time.Hour > Configuration.MAX_HOUR))
                return false;
            if (!item.WorkTable[time.Hour-Configuration.MIN_HOUR, (int)time.DayOfWeek])
                return false;
            if (item.MaxTests <=NumOfTestsByDays(item,time))
                return false;
            if (!TestsCollection().TrueForAll(T => (T.IdTester != item.Id) || (T.TestDay != time)))
                return false;
            return true;
        }
        private Test LastTest(Trainee trainee)
        {
            var tests = AllTestsBy(T => T.IdTrainee == trainee.Id &&T.TypeOfCar==trainee.TypeOfCar&&T.ChosenTester);
            Test temp;
            try
            {
                temp = tests.First<Test>();
            }
            catch (InvalidOperationException)
            {
                throw new Exception("there is'nt last test");
            }
            foreach (Test item in tests)
                if (temp.TestDay < item.TestDay)
                    temp = item;

            return temp;
        }
        private bool DatesAreInTheSameWeek(DateTime date1, DateTime date2)
        {
            var cal = System.Globalization.DateTimeFormatInfo.CurrentInfo.Calendar;
            var d1 = date1.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date1));
            var d2 = date2.Date.AddDays(-1 * (int)cal.GetDayOfWeek(date2));

            return d1 == d2;
        }
        private bool FailedCriterion(Test test)
        {
            int Failnum = 0;
            foreach (Criterion item in test.Criterions)
            {
                if (item.grade == Grade.נכשל)
                {
                    Failnum++;
                }
            }
            return (Failnum>(test.Criterions.Count-Failnum));
        }
        private Trainee GetTrainee(Test test)
        {
            if(!TraineesCollection().Exists(T=>T.Id==test.IdTrainee))
                throw new Exception("this trainee doesnt exist");
            return (from item in TraineesCollection() where (item.Id == test.IdTrainee) select item).First();
        }
        private Tester GetTester(Test test)
        {
            if (!TestersCollection().Exists(T => T.Id == test.IdTester))
                throw new Exception("this tester doesnt exist");
            return (from item in TestersCollection() where (item.Id == test.IdTrainee) select item).First();
        }
        private void FindNewTester(Test test)
        {
            List<Tester> testersList = new List<Tester>(IsFree(test.TestDay));
            testersList.RemoveAll(T => T.TypeOfCar != test.TypeOfCar);
            if (testersList.Count() == 0)
            {
                test.ChosenTester = false;
                test.Grade = Grade.נכשל;
                test.IdTester = null;
                Update(test);
                throw new Exception(test+":there isnt tester free in that date");
            }
            test.IdTester = testersList.First().Id;
        }
        private int NumOfFutureTest(Trainee trainee)
        {
            return (AllTestsBy(T => T.IdTrainee == trainee.Id).Count() - NumOfTraineesTests(trainee));
        }
        public string AddTest(Test test)
        {
            CheckTest(test);
            if (test.TestDay <= DateTime.Now)
            {
                throw new Exception("Wrong date!");
            }
            IEnumerable<Tester> testers = TestersCollection();
            IEnumerable<Test> tests = TestsCollection();
            
            var StudentTests = AllTestsBy(T => T.IdTrainee==test.IdTrainee);
            Trainee trainee;
            try
            {
                 trainee = GetTrainee(test);

            }
            catch (Exception e)
            {
                throw e;
            }
            if (IsAllowed(trainee))
                throw new Exception("the trainee is already past the test on this type of car");
            if (test.TypeOfCar != trainee.TypeOfCar)
            {
                throw new Exception("the trainee is'nt learn on this type car!");
            }
            Test PreTest;
            try
            {
                PreTest = LastTest(trainee);
                if ((test.TestDay - PreTest.TestDay).Days < Configuration.RANGE_BETWEEN_TESTS)
                throw new Exception("it's too early to take a new test");
            }
            catch (Exception)
            {

            }
            if (trainee.DLessonPast < Configuration.MIN_NUMBER_OF_LESSONS)
                throw new Exception("You need to do "+(Configuration.MIN_NUMBER_OF_LESSONS - trainee.DLessonPast)+" lessons");

            //Leaving only the testers who work on that date
            List<Tester> testersList = new List<Tester>(IsFree(test.TestDay));
            testersList.RemoveAll(T => T.TypeOfCar != test.TypeOfCar);
            if (testersList.Count() == 0)
            {
                throw new Exception("there isn't any tester on this date");
            }
            test.IdTester = testersList.First().Id;
            test.ChosenTester = true;
            return MyDal.AddTest(test);
        }
        public void AddTester(Tester tester)
        {
            CheckTester(tester);
            if((TestersCollection()).Exists(T =>T.Id==tester.Id ))
                throw new Exception("this tester exist");
            if (tester.Age < Configuration.MIN_AGE_TESTER)
            {
                throw new Exception("You are too younger");
            }
            
            MyDal.AddTester(tester);
            
        }
        public void AddTrainee(Trainee trainee)
        {
            CheckTrainee(trainee);
            if ((TraineesCollection()).Exists(T => T.Id == trainee.Id))
                throw new Exception("this trainee exist");
            if (trainee.Age < Configuration.MIN_AGE_TRAINEE)
            {
                throw new Exception("You are too younger");
            }
            MyDal.AddTrainee(trainee);
        }

        public void DeleteTester(Tester tester)
        {
            if (!TestersCollection().Exists(T => tester.Id == T.Id))
            {
                throw new Exception("there isn't such tester");
            }
            
            MyDal.DeleteTester(tester);
            var TestersTest = from item in TestsCollection() where (item.IdTester == tester.Id) select item;
            foreach (Test item in TestersTest)
            {
                FindNewTester(item);
            }
        }
        public void DeleteTrainee(Trainee trainee)
        {
            if (!TraineesCollection().Exists(T => trainee.Id == T.Id))
            {
                throw new Exception("there isn't such trainee");
            }

            if (TestsCollection().Exists(T=>T.ChosenTester&&DateTime.Now<T.TestDay&&trainee.Id==T.IdTrainee))
            {
                throw new Exception("Can't earse trainee if you have a test");
            }
            MyDal.DeleteTrainee(trainee);
        }

        public List<Tester> TestersCollection()
        {
            return MyDal.TestersCollection();
        }
        public List<Test> TestsCollection()
        {
            return MyDal.TestsCollection();        
        }
        public List<Trainee> TraineesCollection()
        {
            return MyDal.TraineesCollection();        
        }
        
        public void Update(Test test)
        {
            CheckTest(test);
            if (test.Grade == null)
            {
                throw new Exception("you forget to fill the grade");
            }
            if (test.Grade == Grade.עבר &&FailedCriterion(test))
            {
                test.Grade = Grade.נכשל;
            }
            MyDal.Update(test);
        }
        public void UpdateTester(Tester tester)
        {
            CheckTester(tester);
            if (!TestersCollection().Exists(T => tester.Id == T.Id))
            {
                throw new Exception("there isn't such tester");
            }
            MyDal.UpdateTester(tester);
        }
        public void UpdateTrainee(Trainee trainee)
        {
            CheckTrainee(trainee);
            if (!TraineesCollection().Exists(T => trainee.Id == T.Id))
            {
                throw new Exception("there isn't such tester");
            }
            MyDal.UpdateTrainee(trainee);
        }
        //the functions
        public IEnumerable<Tester> DistanseFromAdress(Address adress)
        {
            Random r = new Random();
            return from item in TestersCollection() where (r.Next(Configuration.DISTANCE+10)<Configuration.DISTANCE) select item;
        }
        public IEnumerable<Tester> IsFree(DateTime time)
        {
            return from item in TestersCollection() where (IsHeFree(item,time)) select item;
        }
        public IEnumerable<Test> AllTestsBy(Predicate<Test> func)
        {
            return from item in TestsCollection() where (func(item)) select item;
        }
        public int NumOfTraineesTests(Trainee trainee)
        {
            return AllTestsBy(T=>T.IdTrainee==trainee.Id&&(DateTime.Now>T.TestDay)).Count<Test>();
        }
        public bool IsAllowed(Trainee trainee)
        {
            var tests = from item in TestsCollection()
                        let type = trainee.TypeOfCar
                        let id = trainee.Id
                        where (item.IdTrainee == id && item.TypeOfCar == type)
                        orderby item.TestDay descending
                        select item;
            if(tests.Count()==0)
            {
                return false;
            }
            return (tests.First().Grade == Grade.עבר);
        }
        public List<Test> ListByDay()
        {
            return new List<Test>(from item in TestsCollection() where(item.TestDay>=DateTime.Now&&item.ChosenTester) orderby item.TestDay   select item);
        }
        public IEnumerable<IGrouping<Car, Tester>> ListOfTestersByCar(bool order)
        {
            if (order)
            {
                return from item in TestersCollection() orderby item.ToString() group item by item.TypeOfCar;
            }
            return from item in TestersCollection() group item by item.TypeOfCar;
        }
        public IEnumerable<IGrouping<string, Trainee>> ListOfTraineesBySchool(bool order)
        {
            if (order)
            {
                return from item in TraineesCollection() orderby item.ToString() group item by item.DrivingSchool;
            }
            return from item in TraineesCollection() group item by item.DrivingSchool;

        }
        public IEnumerable<IGrouping<string, Trainee>> ListOfTraineesByDTeacher(bool order)
        {
            if (order)
            {
                return from item in TraineesCollection() orderby item.ToString() group item by item.DrivingTeacher;
            }
            return from item in TraineesCollection() group item by item.DrivingTeacher;
        }
        public IEnumerable<IGrouping<int, Trainee>> ListOfTraineesByNumOfTests(bool order)
        {
            if (order)
            {
                return from item in TraineesCollection() orderby item.ToString() group item by item.DLessonPast;
            }
            return from item in TraineesCollection() group item by item.DLessonPast;
        }
        //...
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

            if (!trainee.PrivateName.All(Char.IsLetter))
                throw new Exception("שם פרטי אינו תקין");

            if (!trainee.FamilyName.All(Char.IsLetter))
                throw new Exception("שם משפחה אינו תקין");

            if (!IdCheck(trainee.Id))
                throw new Exception("מספר תעודת זהות אינו תקין");

            if (!trainee.DrivingSchool.All(Char.IsLetter))
                throw new Exception("שם בית הספר אינו תקין");

            if (!trainee.DrivingTeacher.All(Char.IsLetter))
                throw new Exception("שם המורה אינו תקין");

            if (trainee.Phone.Length!=10 || !trainee.Phone.All(Char.IsDigit))
                throw new Exception("מספר הפלאפון אינו תקין");

            if (trainee.DLessonPast < 0)
                throw new Exception("מספר השיעורים שעברת אינו תקין");

            if (!trainee.Address.City.All(char.IsLetter))
                throw new Exception("שם העיר אינו תקין");

            if (!trainee.Address.Street.All(char.IsLetter))
                throw new Exception("שם הרחוב אינו תקין");

            if (!trainee.Address.NumOfHome.All(char.IsDigit))
                throw new Exception("מספר הבית אינו תקין");

        }
        public static void CheckTester(Tester tester)
        {

            if (!tester.PrivateName.All(Char.IsLetter))
                throw new Exception("שם פרטי אינו תקין");

            if (!tester.FamilyName.All(Char.IsLetter))
                throw new Exception("שם המשפחה אינו תקין");

            if (!IdCheck(tester.Id))
                throw new Exception("מספר תעודת זהות אינו תקין");

            if (tester.Phone.Length != 10 || !tester.Phone.All(Char.IsDigit))
                throw new Exception("מספר הפלאפון אינו תקין");

            if (tester.MaxTests < 0)
                throw new Exception("מספר המבחנים המקסימאלי אינו תקין");

            if (tester.MaxRange < 0)
                throw new Exception(tester + ":the Max Range cant be  a negative number");

            if (!tester.Address.City.All(char.IsLetter))
                throw new Exception("שם העיר אינו תקין");

            if (!tester.Address.Street.All(char.IsLetter))
                throw new Exception("שם הרחוב אינו תקין");
            if(!tester.Address.NumOfHome.All(char.IsDigit))
                throw new Exception("מספר הבית אינו תקין");

        }
        public static void CheckTest(Test test)
        {
            if (test.TestTime.All(char.IsLetter))
                throw new Exception(test+":wrong test time");
            if (test.TestTime != test.TestDay.Day + "/" + test.TestDay.Month + "/" + test.TestDay.Year)
                 throw new Exception(test + ":the dates arent same");
            if ((test.IdTester != null)&&!IdCheck(test.IdTester))
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
        public IEnumerable<object> TestsByDay(DateTime date)
        {
            return from item in TestsCollection() where (item.TestDay.Day == date.Day) orderby item.NumTest select new {test=item,trainee=GetTrainee(item),tester=GetTester(item)};
        }
        public IEnumerable<object> TestsNow()
        {
            return TestsByDay(DateTime.Now);
        }
    } 
}
