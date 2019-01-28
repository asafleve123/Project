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
        /// <summary>
        /// func that add a tester to the system 
        /// </summary>
        /// <param name="tester"></param>
        void AddTester(Tester tester);
        /// <summary>
        /// func that delete a tester from the system
        /// </summary>
        /// <param name="tester"></param>
        void DeleteTester(Tester tester);
        /// <summary>
        /// Func that update a tester
        /// </summary>
        /// <param name="tester"></param>
        void UpdateTester(Tester tester);

        //Trainee
        /// <summary>
        /// func that add a trainee to the system 
        /// </summary>
        /// <param name="trainee"></param>
        void AddTrainee(Trainee trainee);
        /// <summary>
        /// func that delete a trainee from the system
        /// </summary>
        /// <param name="trainee"></param>
        void DeleteTrainee(Trainee trainee);
        /// <summary>
        /// Func that update a trainee
        /// </summary>
        /// <param name="trainee"></param>
        void UpdateTrainee(Trainee trainee);

        //Test
        /// <summary>
        /// func that add a test to the system 
        /// </summary>
        /// <param name="test"></param>
        string AddTest(Test test);
        /// <summary>
        /// func that update a test 
        /// </summary>
        /// <param name="test"></param>
        void Update(Test test);
        //lists
        /// <summary>
        /// return list of all the testers in the system
        /// </summary>
        /// <returns></returns>
        List<Tester> TestersCollection();
        /// <summary>
        /// return list of all the Trainees in the system
        /// </summary>
        /// <returns></returns>
        List<Trainee> TraineesCollection();
        /// <summary>
        /// return list of all the tests in the system
        /// </summary>
        /// <returns></returns>
        List<Test> TestsCollection();
        //the functions
        IEnumerable<Tester> DistanseFromAdress(Address adress);
        IEnumerable<Tester> IsFree(DateTime time);
        IEnumerable<Test> AllTestsBy(Predicate<Test> func);
        IEnumerable<Test> AllTestsBy(Predicate<Test> func, string idtester);
        IEnumerable<Test> AllTestsByTR(Predicate<Test> func, string idtrainee);
        int NumOfTraineesTests(Trainee trainee);
        bool IsAllowed(Trainee trainee);
        List<Test> ListByDay();
        Tester GetTester(string id);
        Trainee GetTrainee(string id);

        IEnumerable<IGrouping<Car, Tester>> ListOfTestersByCar(bool order);
        IEnumerable<IGrouping<string, Trainee>> ListOfTraineesBySchool(bool order);
        IEnumerable<IGrouping<string, Trainee>> ListOfTraineesByDTeacher(bool order);
        IEnumerable<IGrouping<int, Trainee>> ListOfTraineesByNumOfTests(bool order);
         Uri ConvertCriterions(Test test)
    }
}
