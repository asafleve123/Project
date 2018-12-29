using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Configuration
    {
        public static int num = 0;
        public static int MIN_AGE_TESTER = 40;
        public static int MIN_AGE_TRAINEE = 18;
        public static int RANGE_BETWEEN_TESTS = 7;
        public static int MIN_NUMBER_OF_LESSONS = 20;
        public static int MIN_HOUR = 9;
        public static int MAX_HOUR = 15;
        public static int THURSDAY = 5;
        public static int HOURS = MAX_HOUR - MIN_HOUR;
        public static int DISTANCE = 10;
    }
}
