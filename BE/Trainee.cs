using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Trainee : IComparable
    {
        private Address address;
        //property
        public string Id { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public Gender Gender { get; set; }
        public string Phone { get; set; }
        public Address Address { get => address; set => address = value; }
        public DateTime DOB { get; set; }
        public Car TypeOfCar { get; set; }
        public Gearbox TypeGearBox { get; set; }
        public string DrivingSchool { get; set; }
        public string DrivingTeacher { get; set; }
        public int DLessonPast { get; set; }
        public int Age { get { return DateTime.Now.Year - DOB.Year; }}
        public string Code { get; set; }
        public Uri Uri { get; set; }
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

        public Trainee(string id, string privateName, string familyName, Gender gender, string phone, Address address, DateTime dOB, Car typeOfCar, Gearbox typeGearBox, string drivingSchool, string drivingTeacher, int dLessonPast, bool criminalRecord ,string code, Uri Uri) : this(id)
        {
            if(privateName!=null)
                PrivateName = string.Copy(privateName);
            if(familyName!=null)
                FamilyName = string.Copy(familyName);
            Gender = gender;
            if(phone!=null)
                Phone = string.Copy(phone);
            Address = address;
            DOB = dOB;
            TypeOfCar = typeOfCar;
            TypeGearBox = typeGearBox;
            if(drivingSchool!=null)
                DrivingSchool = string.Copy(drivingSchool);
            if(drivingTeacher!=null)
                DrivingTeacher = string.Copy(drivingTeacher);
            DLessonPast = dLessonPast;
            this.Code = string.Copy(code);
            this.Uri = new Uri(string.Copy(Uri.ToString()));
        }

        /// <summary>
        /// copy constructor - ables to copy by value.
        /// </summary>
        /// <param name="trainee"></param>
        public Trainee(Trainee trainee)
        {
            if(trainee.PrivateName!=null)
            PrivateName = string.Copy(trainee.PrivateName);
            if(trainee.Id!=null)
            Id = string.Copy(trainee.Id);
            if(trainee.FamilyName!=null)
            FamilyName = string.Copy(trainee.FamilyName);
            if(trainee.Phone!=null)
            Phone = string.Copy(trainee.Phone);
            if(trainee.DrivingSchool != null)
            DrivingSchool = string.Copy(trainee.DrivingSchool);
            if (trainee.DrivingSchool != null)
                DrivingTeacher = string.Copy(trainee.DrivingTeacher);
            DLessonPast = trainee.DLessonPast;
            Gender = trainee.Gender;
            address.City = string.Copy(trainee.Address.City);
            address.Street = string.Copy(trainee.Address.Street);
            address.NumOfHome = string.Copy(trainee.Address.NumOfHome);
            DOB = trainee.DOB;
            TypeOfCar = trainee.TypeOfCar;
            TypeGearBox = trainee.TypeGearBox;
            this.Code = string.Copy(trainee.Code);
            this.Uri = new Uri(string.Copy(Uri.ToString()));
        }

    }
}
