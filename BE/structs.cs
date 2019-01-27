using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public struct Address
    {
       public string City;
       public string Street;
       public string NumOfHome;
        public Address(string City,string Street,string NumOfHome)
        {
            this.City = City;
            this.Street = Street;
            this.NumOfHome = NumOfHome;
            
        }
        public override string ToString()
        {
            return City+" "+Street+" "+NumOfHome;
        }
        
    }
    public struct Criterion
    {
        
       public string name;
       public Grade grade;
        public Criterion(string Name,Grade grade)
        {
            name = Name;
            this.grade = grade;
        }
    }
}
