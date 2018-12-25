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
            var StudentTests = from item in TestsCollection() where (test.IdTrainee == item.IdTrainee) select item;
            var Trainee = from item in TraineesCollection() where (test.IdTrainee == item.Id) select item;
            Trainee trainee = Trainee.First();
            Test PreTest = Test.GetTestTemp(new DateTime(trainee.DOB.Year, trainee.DOB.Month, trainee.DOB.Day));
            foreach (var item in StudentTests)
            {
                if (PreTest.TestDay < item.TestDay)
                {
                    PreTest = item;
                }
            }
            if ((test.TestDay - PreTest.TestDay).Days < Configuration.RANGE_BETWEEN_TESTS)
            {
                throw new Exception("it's too early");
            }
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
    }
}
