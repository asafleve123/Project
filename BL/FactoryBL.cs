using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class FactoryBL
    {
        static IBL instance = null;
        public static IBL getBl()
        {
            if (instance == null)
                instance = new MyBl();
            return instance;
        }
    }
}
