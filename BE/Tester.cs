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
        public DateTime DOB { get;private set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public int Years { get; set; }
        public int MaxTests { get; set; }
        public Car TypeOfCar { get; private set; }
        public bool[,] WorkTable { get => worktable; set => worktable = value; }
        public int MaxRange { get; set; }
        public int Age { get { return DateTime.Now.Year-DOB.Year; } }
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
        public Tester(string Id, string FamilyName, string PrivateName, DateTime DOB, Gender Gender, string Phone, Address Address, int Years, int MaxTests, Car TypeOfCar, bool[,] WorkTable, int MaxRange)
        {
            this.Id = Id;
            this.FamilyName = FamilyName;
            this.PrivateName = PrivateName;
            this.DOB = DOB;
            this.Phone = Phone;
            this.Address = Address;
            this.Years = Years;
            this.MaxTests = MaxTests;
            this.TypeOfCar = TypeOfCar;
            this.WorkTable = WorkTable;
            this.MaxRange = MaxRange;
            this.Gender = Gender;
        }
        /// <summary>
        /// copy constructor - ables to copy by value.
        /// </summary>
        /// <param name="tester"></param>
        public Tester(Tester tester)
        {
            if(tester.Id!=null)
                Id = string.Copy(tester.Id);
            if (tester.FamilyName != null)
                FamilyName = string.Copy(tester.FamilyName);
            if (tester.PrivateName != null)
                PrivateName = string.Copy(tester.PrivateName);
            DOB = tester.DOB;
            Gender = tester.Gender;
            if(tester.Phone!=null)
            Phone = string.Copy(tester.Phone);
            Address = tester.Address;
            Years = tester.Years;
            MaxTests = tester.MaxTests;
            TypeOfCar = tester.TypeOfCar;
            MaxRange = tester.MaxRange;
            worktable = new bool[Configuration.HOURS, Configuration.THURSDAY];
            for (int i = 0; i < Configuration.HOURS; i++)
            {
                for (int j = 0; j < Configuration.THURSDAY; j++)
                {
                    WorkTable[i, j] = tester.WorkTable[i, j];
                }
            }
            
        }
        //constructor
        public Tester(string id)
        {
            Id = id;
        }

        
    }
}
