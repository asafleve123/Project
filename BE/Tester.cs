using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// class that represents a tester
    /// </summary>
    public class Tester : IComparable
    {
        private bool[,] worktable;
        //properties
        public string Id { get; private set; }
        public string FamilyName { get; set; }
        public string PrivateName { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public int Years { get; set; }
        public int MaxTests { get; set; }
        public Car TypeOfCar { get; set; }
        public bool[,] WorkTable { get => worktable; set => worktable = value; }
        public int MaxRange { get; set; }
        //...
        //functions
        public override string ToString()
        {
            return PrivateName + " " + FamilyName;
        }

        public int CompareTo(object obj)
        {
            return Id.CompareTo(((Tester)obj).Id);
        }

        /// <summary>
        /// copy constructor - ables to copy by value.
        /// </summary>
        /// <param name="tester"></param>
        public Tester(Tester tester)
        {
            Id = string.Copy(tester.Id);
            FamilyName = string.Copy(tester.FamilyName);
            PrivateName = string.Copy(tester.PrivateName);
            DOB = tester.DOB;
            Gender = tester.Gender;
            Phone = string.Copy(tester.Phone);
            Address = tester.Address;
            Years = tester.Years;
            MaxTests = tester.MaxTests;
            TypeOfCar = tester.TypeOfCar;
            MaxRange = tester.MaxRange;
            worktable = new bool[6, 5];
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    WorkTable[i, j] = tester.WorkTable[i, j];
                }
            }
        }
        public Tester(string id)
        {
            Id = id;
        }
    }
}
