using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public struct Address
    {
       public string City { get; set; }
       public string Street { get; set; }
        public string NumOfHome { get; set; }
        public Address(string City,string Street,string NumOfHome)
        {
            this.City = string.Copy(City);
            this.Street = string.Copy(Street);
            this.NumOfHome = string.Copy(NumOfHome);
        }
        public Address(Address address)
        {
            this.City = string.Copy(address.City);
            this.Street = string.Copy(address.Street);
            this.NumOfHome = string.Copy(address.NumOfHome);
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2}",City,Street,NumOfHome);
        }
        
    }
    public struct Criterion
    {
        
        public string name { get; set; }
        public Grade grade { get; set; }
        /*
        public static Criterion Pharse(string str)
        {
            string[] values = str.Split(' ');
            return new Criterion(values[0], (Grade)Enum.Parse(typeof(Grade), values[1]));
        }*/
        public Criterion(string Name,Grade grade)
        {
            name =string.Copy( Name);
            this.grade = grade;
        }
        public override string ToString()
        {
            return name+" "+grade;
        }
    }
}
