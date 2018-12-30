using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public interface IBL
    {
        //Tester
        void AddTester(Tester tester);
        void DeleteTester(Tester tester);
        void UpdateTester(Tester tester);

        //Trainee
        void AddTrainee(Trainee trainee);
        void DeleteTrainee(Trainee trainee);
        void UpdateTrainee(Trainee trainee);
        //Test
        void AddTest(Test test);
        void Update(Test test);
        //lists
        List<Tester> TestersCollection();
        List<Trainee> TraineesCollection();
        List<Test> TestsCollection();
        //the functions
        IEnumerable<Tester> DistanseFromAdress(Address adress);
        IEnumerable<Tester> IsFree(DateTime time);
        IEnumerable<Test> AllTestsBy(Predicate<Test> func);
        int NumOfTraineesTests(Trainee trainee);
        bool IsAllowed(Trainee trainee);
        List<Test> ListByDay();
    }
}
