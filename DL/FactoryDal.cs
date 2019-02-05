using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        static Idal instance =null;
        public static Idal getDal()
        {
            if (instance == null)
                instance = new Dal_XML_imp();
            return instance;
        }
    }
}
