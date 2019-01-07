using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace DAL
{
    public interface Idal
    {
        //Tester
        /// <summary>
        /// Func that add a test to  the system
        /// </summary>
        /// <param name="test"></param>
        void AddTester(Tester tester);
        /// <summary>
        /// func that delete a tester from the system
        /// </summary>
        /// <param name="tester"></param>
        void DeleteTester(Tester tester);
        /// <summary>
        /// func that Update Tester
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
        /// Func that delete a trainee from the system
        /// </summary>
        /// <param name="trainee"></param>
        void DeleteTrainee(Trainee trainee);
        /// <summary>
        /// func thatt Update aTrainee
        /// </summary>
        /// <param name="trainee"></param>
        void UpdateTrainee(Trainee trainee);
        //Test
        string AddTest(Test test);
        void Update(Test test);
        //lists
        /// <summary>
        /// Func that return the Testers 
        /// </summary>
        /// <returns></returns>
        List<Tester> TestersCollection();
        /// <summary>
        /// Func that return the Tests
        /// </summary>
        /// <returns></returns>
        List<Test> TestsCollection();
        /// <summary>
        /// func that return the Trainees
        /// </summary>
        /// <returns></returns>
        List<Trainee> TraineesCollection();
    }
}
