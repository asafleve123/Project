using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee : IComparable
    {
        //property
        public string Id { get; private set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public DateTime DOB { get; set; }
        public Car TypeOfCar { get; set; }
        public Gearbox TypeGearBox { get; set; }
        public string DrivingSchool { get; set; }
        public string DrivingTeacher { get; set; }
        public int DLessonPast { get; set; }
        public bool CriminalRecord { get; set; }
        public Nation Mynation { get; set; }
        public int Age { get { return DateTime.Now.Year - DOB.Year; }}
        //...
        //functions
        public override string ToString()
        {
            return PrivateName + " " + FamilyName;
        }
        public int CompareTo(object obj)
        {
            return Id.CompareTo(((Trainee)obj).Id);
        }
        //CTOR
        public Trainee(string id)
        {
            Id = id;
        }

        public Trainee(string id, string privateName, string familyName, Gender gender, string phone, Address address, DateTime dOB, Car typeOfCar, Gearbox typeGearBox, string drivingSchool, string drivingTeacher, int dLessonPast, bool criminalRecord, Nation mynation) : this(id)
        {
            PrivateName = string.Copy(privateName);
            FamilyName = string.Copy(familyName);
            Gender = gender;
            Phone = string.Copy(phone);
            Address = address;
            DOB = dOB;
            TypeOfCar = typeOfCar;
            TypeGearBox = typeGearBox;
            DrivingSchool = string.Copy(drivingSchool);
            DrivingTeacher = string.Copy(drivingTeacher);
            DLessonPast = dLessonPast;
            CriminalRecord = criminalRecord;
            Mynation = mynation;
        }

        /// <summary>
        /// copy constructor - ables to copy by value.
        /// </summary>
        /// <param name="trainee"></param>
        public Trainee(Trainee trainee)
        {
            PrivateName = string.Copy(trainee.PrivateName);
            Id = string.Copy(trainee.Id);
            FamilyName = string.Copy(trainee.FamilyName);
            Phone = string.Copy(trainee.Phone);
            DrivingSchool = string.Copy(trainee.DrivingSchool);
            DrivingTeacher = string.Copy(trainee.DrivingTeacher);
            DLessonPast = trainee.DLessonPast;
            Gender = trainee.Gender;
            Address = trainee.Address;
            DOB = trainee.DOB;
            TypeOfCar = trainee.TypeOfCar;
            TypeGearBox = trainee.TypeGearBox;
            CriminalRecord = trainee.CriminalRecord;
            Mynation = trainee.Mynation;
           
        }

    }
}
