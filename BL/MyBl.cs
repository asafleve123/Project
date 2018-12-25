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
            var PreTest = from item in TestsList() where (test.IdTrainee == item.IdTrainee) select item;

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

        public List<Tester> TestersList()
        {
            throw new NotImplementedException();
        }

        public List<Test> TestsList()
        {
            throw new NotImplementedException();
        }

        public List<Trainee> TraineesList()
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
