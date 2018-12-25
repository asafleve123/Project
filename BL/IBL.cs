using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    interface IBL
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
        List<Tester> TestersList();
        List<Trainee> TraineesList();
        List<Test> TestsList();
    }
}
