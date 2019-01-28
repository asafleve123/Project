using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    /// <summary>
    /// class that represents a tester
    /// </summary>
    public class Tester : IComparable
    {
        private bool[,] worktable;
        private Address address;
        //properties
        public string Id { get; set; }
        public string FamilyName { get; set; }
        public string PrivateName { get; set; }
        public DateTime DOB { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public Address Address { get => address; set => address = value; }
        public int Years { get { return DateTime.Now.Year - RegisterDate.Year; } }
        public int MaxTests { get; set; }
        public Car TypeOfCar { get; set; }

        [XmlIgnore]
        public bool[,] WorkTable { get => worktable; set => worktable = value; }
        public string TempWorkTable
        {
            get
            {
                if (WorkTable == null)
                    return null;
                string result = "";
                if (WorkTable != null)
                {
                    int sizeA = WorkTable.GetLength(0);
                    int sizeB = WorkTable.GetLength(1);
                    result += "" + sizeA + "," + sizeB;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            result += "," + WorkTable[i, j];
                }
                return result;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    string[] values = value.Split(',');
                    int sizeA = int.Parse(values[0]);
                    int sizeB = int.Parse(values[1]);
                    WorkTable = new bool[sizeA, sizeB];
                    int index = 2; for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            WorkTable[i, j] = bool.Parse(values[index++]);
                }
            }

        }
        public int MaxRange { get; set; }
        public int Age { get { return DateTime.Now.Year - DOB.Year; } }
        public string Code { get; set; }
        public DateTime RegisterDate { get; set; }
        public Tester()
        {
            Id = "";
        }
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

        public Tester(string Id, string FamilyName, string PrivateName, DateTime DOB, Gender Gender, string Phone, Address Address, DateTime RegisterDate, int MaxTests, Car TypeOfCar, bool[,] WorkTable, int MaxRange, string code)
        {
            this.Id = Id;
            this.FamilyName = FamilyName;
            this.PrivateName = PrivateName;
            this.DOB = DOB;
            this.Phone = Phone;
            this.Address = Address;
            this.RegisterDate = RegisterDate;
            this.MaxTests = MaxTests;
            this.TypeOfCar = TypeOfCar;
            this.WorkTable = WorkTable;
            this.MaxRange = MaxRange;
            this.Gender = Gender;
            this.Code = string.Copy(code);
        }
        /// <summary>
        /// copy constructor - ables to copy by value.
        /// </summary>
        /// <param name="tester"></param>
        public Tester(Tester tester)
        {
            if (tester.Id != null)
                Id = string.Copy(tester.Id);
            if (tester.FamilyName != null)
                FamilyName = string.Copy(tester.FamilyName);
            if (tester.PrivateName != null)
                PrivateName = string.Copy(tester.PrivateName);
            DOB = tester.DOB;
            Gender = tester.Gender;
            if (tester.Phone != null)
                Phone = string.Copy(tester.Phone);
            address.City = string.Copy(tester.Address.City);
            address.Street = string.Copy(tester.Address.Street);
            address.NumOfHome = string.Copy(tester.Address.NumOfHome);
            RegisterDate = tester.RegisterDate;
            MaxTests = tester.MaxTests;
            TypeOfCar = tester.TypeOfCar;
            MaxRange = tester.MaxRange;
            Code = tester.Code;
            worktable = new bool[Configuration.HOURS, Configuration.THURSDAY];
            for (int i = 0; i < Configuration.HOURS; i++)
            {
                for (int j = 0; j < Configuration.THURSDAY; j++)
                {
                    WorkTable[i, j] = tester.WorkTable[i, j];
                }
            }
            Code = string.Copy(tester.Code);
        }
        //constructor
        public Tester(string id)
        {
            Id = id;
        }


    }
}
