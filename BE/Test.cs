using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Test : IComparable
    {
        private List<Criterion> criterions = new List<Criterion>();
        //property
        public string NumTest { get; set; }
        public string IdTester { get; set; }
        public string IdTrainee { get; set; }
        public string TestTime { get; set; }
        public DateTime TestDay { get; set; }
        public Address TestAddress { get; set; }
        public Grade Grade { get; set; }
        public string Comments { get; set; }
        public Car TypeOfCar { get; set; }
        //functions
        public override string ToString()
        {
            return NumTest;
        }
        public void AddCretrion(Criterion item)
        {
            criterions.Add(item);
        }
        public void RemoveCretrion(Criterion item)
        {
            criterions.Remove(item);
        }
        public static Test GetTestTemp(DateTime date)
        {
            return new Test(date);
        }
        public int CompareTo(object obj)
        {
            return NumTest.CompareTo(((Test)obj).NumTest);
        }

        /// <summary>
        /// Constructors
        /// </summary>
        public Test(Tester teacher, Trainee student, DateTime date, Address ATestAdress)
        {
            IdTester = teacher.Id;
            IdTrainee = student.Id;
            TestTime = date.Day + "/" + date.Month + "/" + date.Year;
            TestDay = date;
            TestAddress = ATestAdress;
            Comments = null;
        }
        private Test(DateTime date)
        {
            TestDay = date;
        }
        /// <summary>
        /// copy constructor - ables to copy by value.
        /// </summary>
        /// <param name="test"></param>
        public Test(Test test)
        {
            TestDay = test.TestDay;
            TestAddress = test.TestAddress;
            Grade = test.Grade;
            NumTest = string.Copy(test.NumTest);
            IdTester = string.Copy(test.IdTester);
            IdTrainee = string.Copy(test.IdTrainee);
            TestTime = string.Copy(test.TestTime);
            Comments = string.Copy(test.Comments);
            foreach (Criterion item in test.criterions)
            {
                criterions.Add(item);
            }
        }
    }
}
