using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        public Grade? Grade { get; set; }
        public string Comments { get; set; }
        public Car TypeOfCar { get; set; }
        
        //[XmlIgnore]
        public List<Criterion> Criterions { get=>criterions; set=>criterions=value; }
        //public string TmpCriterions
        //{
        //    get
        //    {
        //        if (Criterions == null)
        //            return null;
        //        string result = "";
        //        if (Criterions != null)
        //        {
        //            int size = Criterions.Count;
        //            result += "" + size ;
        //            for (int i = 0; i < size; i++)
        //                    result += "," + Criterions[i];
        //        }
        //        return result;
        //    }
        //    set {
        //        if (value != null && value.Length > 0)
        //        {
        //            string[] values = value.Split(',');
        //            int size = int.Parse(values[0]);
        //            Criterions = new List<Criterion>();
        //            int index = 2;
        //            for (int i = 0; i < size; i++)
        //                Criterions.Add ( Criterion.Pharse(values[index++]));
        //        }
        //    }
        //}


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
        public int CompareTo(object obj)
        {
            return NumTest.CompareTo(((Test)obj).NumTest);
        }

        /// <summary>
        /// Constructors
        /// </summary>
        public Test( Trainee student, DateTime date, Address ATestAdress)
        {
            IdTester = null;
            IdTrainee = student.Id;
            TestTime = date.Day + "/" + date.Month + "/" + date.Year;
            TestDay = date;
            TestAddress = ATestAdress;
            Comments = null;
            Grade = null;
            TypeOfCar = student.TypeOfCar;
        }
        /// <summary>
        /// copy constructor - ables to copy by value.
        /// </summary>
        /// <param name="test"></param>
        public Test(Test test)
        {
            TestDay = test.TestDay;
            TestAddress = test.TestAddress;
            //address.City = string.Copy(test.TestAddress.City);
            //address.Street = string.Copy(test.TestAddress.Street);
            //address.NumOfHome = string.Copy(test.TestAddress.NumOfHome);
            Grade = test.Grade;
            if (test.NumTest != null)
            NumTest = string.Copy(test.NumTest);
            if(test.IdTester!=null)
            IdTester = string.Copy(test.IdTester);
            if(test.IdTrainee!=null)
            IdTrainee = string.Copy(test.IdTrainee);
            if (test.TestTime != null)
                TestTime = string.Copy(test.TestTime);
            if(test.Comments!=null)
            Comments = string.Copy(test.Comments);
            foreach (Criterion item in test.criterions)
            {
                criterions.Add(new Criterion(string.Copy(item.name),item.grade));
            }
            TypeOfCar = test.TypeOfCar;
        }
        public Test()
        {

        }
    }
}
