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
            List<Tester> Temp = new List<Tester>();
            for (int i = 0; i < DataSource.testers.Count; i++)
            {
                Temp.Add(new Tester(DataSource.testers[i]));
            }
            return Temp;
        }

        public List<Test> TestsCollection()
        {
            List<Test> Temp = new List<Test>();
            for (int i = 0; i < DataSource.tests.Count; i++)
            {
                Temp.Add(new Test(DataSource.tests[i]));
            }
            return Temp;
        }

        public List<Trainee> TraineesCollection()
        {
            List<Trainee> Temp = new List<Trainee>();
            for (int i = 0; i < DataSource.trainees.Count; i++)
            {
                Temp.Add(new Trainee(DataSource.trainees[i]));
            }
            return Temp;
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
    }
}
