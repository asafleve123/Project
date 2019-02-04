using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using BE;
using System.Xml.Serialization;


namespace DAL
{
    class Dal_XML_imp : Idal
    {
        XElement TraineesRoot;
        XElement ConfigRoot;
        string ConfigPath = @"ConfigXml.xml";
        string TraineesPath = @"TraineesXml.xml";
        string TestersPath = @"TestersXml.xml";
        string TestsPath = @"TestsXml.xml";

        public Dal_XML_imp()
        {
            if (!File.Exists(TraineesPath) || !File.Exists(ConfigPath) || !File.Exists(TestersPath) || !File.Exists(TestsPath))
                CreateFiles();
            else
                LoadData();
        }
        private void LoadData()
        {
            try
            {
                TraineesRoot = XElement.Load(TraineesPath);
                ConfigRoot = XElement.Load(ConfigPath);
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }
        private void CreateFiles()
        {
            TraineesRoot = new XElement("Trainees");
            TraineesRoot.Save(TraineesPath);

            ConfigRoot = new XElement("Config");
            ConfigRoot.Add("num", 0);
            ConfigRoot.Save(ConfigPath);
        }
        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
            xmlSerializer.Serialize(file, source);
            file.Close();
        }
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(file);
            file.Close();
            return result;
        }

        //Test
        public string AddTest(Test test)
        {
            CheckTest(test);
            List<Test> tests;
            if (File.Exists(TestsPath))
                tests = LoadFromXML<List<Test>>(TestsPath);
            else
                tests = new List<Test>();

            int n = int.Parse(ConfigRoot.Element("num").Value);
            Test temp = new Test(test)
            {
                NumTest = n.ToString("00000000")
            };
            tests.Add(temp);
            ConfigRoot.Element("num").SetValue(n + 1);
            ConfigRoot.Save(ConfigPath);
            SaveToXML(tests, TestsPath);
            return new Test(temp).NumTest;
        }
        public void Update(Test test)
        {
            CheckTest(test);
            if (!File.Exists(TestsPath))
                throw new Exception("the test isnt exist");
            List<Test> tests = LoadFromXML<List<Test>>(TestsPath);
            if (!tests.Exists(T => T.CompareTo(test) == 0))
            {
                throw new Exception("the test isnt exist");
            }
            tests[tests.IndexOf(tests.Find(T => T.CompareTo(test) == 0))] = new Test(test);
            SaveToXML(test, TestsPath);
        }

        //Tester
        /// <summary>
        /// Func that add a test to  the system
        /// </summary>
        /// <param name="test"></param>
        public void AddTester(Tester tester)
        {
            CheckTester(tester);
            Tester temp = new Tester(tester);
            List<Tester> testers;
            if (File.Exists(TestersPath))
                testers = LoadFromXML<List<Tester>>(TestersPath);
            else
                testers = new List<Tester>();

            if (testers.Exists(T => T.CompareTo(temp) == 0))
                throw new Exception("This tester already exist");
            testers.Add(temp);
            SaveToXML(testers, TestersPath);
        }
        /// <summary>
        /// func that delete a tester from the system
        /// </summary>
        /// <param name="tester"></param>
        public void DeleteTester(Tester tester)
        {
            if (!File.Exists(TestersPath))
                throw new Exception("The tester isn't found");
            List<Tester> testers = LoadFromXML<List<Tester>>(TestersPath);
            if (!testers.Exists(T => T.CompareTo(tester) == 0))
            {
                throw new Exception("The tester isn't found");
            }
            testers.Remove(testers.Find(T => T.CompareTo(tester) == 0));
            SaveToXML(testers, TestersPath);
        }
        /// <summary>
        /// func that Update Tester
        /// </summary>
        /// <param name="tester"></param>
        public void UpdateTester(Tester tester)
        {
            if (!File.Exists(TestersPath))
                throw new Exception("This tester doesn't exist");
            CheckTester(tester);
            List<Tester> testers = LoadFromXML<List<Tester>>(TestersPath);
            if (!testers.Exists(T => T.CompareTo(tester) == 0))
            {
                throw new Exception("This tester doesn't exist");
            }
            testers[testers.IndexOf(testers.Find(T => T.CompareTo(tester) == 0))] = new Tester(tester);
            SaveToXML(testers, TestersPath);
        }

        //Trainee
        /// <summary>
        /// func that add a trainee to the system
        /// </summary>
        /// <param name="trainee"></param>
        public void AddTrainee(Trainee trainee)
        {
            CheckTrainee(trainee);
            var check = (from tr in TraineesRoot.Elements() where tr.Element("Id").Value == trainee.Id select tr.Element("Id").Value).FirstOrDefault();
            if (check != null)
                throw new Exception("This Trainee already exist");
            XElement id = new XElement("Id", trainee.Id);
            XElement PrivateName = new XElement("PrivateName", trainee.PrivateName);
            XElement FamilyName = new XElement("FamilyName", trainee.FamilyName);
            XElement Gender = new XElement("Gender", trainee.Gender);
            XElement Phone = new XElement("Phone", trainee.Phone);
            XElement Address = new XElement("Address", new XElement("City", trainee.Address.City), new XElement("Street", trainee.Address.Street), new XElement("NumOfHome", trainee.Address.NumOfHome));
            XElement DOB = new XElement("DOB", trainee.DOB);
            XElement TypeOfCar = new XElement("TypeOfCar", trainee.TypeOfCar);
            XElement TypeGearBox = new XElement("TypeGearBox", trainee.TypeGearBox);
            XElement DrivingSchool = new XElement("DrivingSchool", trainee.DrivingSchool);
            XElement DrivingTeacher = new XElement("DrivingTeacher", trainee.DrivingTeacher);
            XElement DLessonPast = new XElement("DLessonPast", trainee.DLessonPast);
            XElement Age = new XElement("Age", trainee.Age);
            XElement Code = new XElement("Code", trainee.Code);
            TraineesRoot.Add(new XElement("Trainee", id, PrivateName, FamilyName, Gender, Phone, Address, DOB, TypeOfCar, TypeGearBox, DrivingSchool, DrivingSchool, DrivingTeacher, DLessonPast, Age, Code));
            TraineesRoot.Save(TraineesPath);
        }
        /// <summary>
        /// Func that delete a trainee from the system
        /// </summary>
        /// <param name="trainee"></param>
        public void DeleteTrainee(Trainee trainee)
        {
            XElement traineeElement;
            traineeElement = (from tr in TraineesRoot.Elements()
                              where tr.Element("id").Value == trainee.Id
                              select tr).FirstOrDefault();
            if (traineeElement == null)
                throw new Exception("לא ניתן למחוק נבחן שאינו קיים");
            traineeElement.Remove();
            TraineesRoot.Save(TraineesPath);
        }
        /// <summary>
        /// func thatt Update aTrainee
        /// </summary>
        /// <param name="trainee"></param>
        public void UpdateTrainee(Trainee trainee)
        {
            CheckTrainee(trainee);
            XElement traineeElement = (from tr in TraineesRoot.Elements()
                                       where tr.Element("id").Value == trainee.Id
                                       select tr).FirstOrDefault();
            if (traineeElement == null)
                throw new Exception("לא ניתן לעדכן נבחן שאינו קיים");
            traineeElement.Element("Id").Value = trainee.Id;
            traineeElement.Element("PrivateName").Value=trainee.PrivateName;
            traineeElement.Element("FamilyName").Value=trainee.FamilyName;
            traineeElement.Element("Gender").Value = trainee.Gender.ToString();
            traineeElement.Element("Phone").Value = trainee.Phone;
            traineeElement.Element("Address").Element("City").Value = trainee.Address.City;
            traineeElement.Element("Address").Element("Street").Value = trainee.Address.Street;
            traineeElement.Element("Address").Element("NumOfHome").Value = trainee.Address.NumOfHome;
            traineeElement.Element("DOB").Value = trainee.DOB.ToString();
            traineeElement.Element("TypeOfCar").Value = trainee.TypeOfCar.ToString();
            traineeElement.Element("TypeGearBox").Value = trainee.TypeGearBox.ToString();
            traineeElement.Element("DrivingSchool").Value=trainee.DrivingSchool;
            traineeElement.Element("DrivingTeacher").Value = trainee.DrivingTeacher;
            traineeElement.Element("Code").Value = trainee.Code;
            traineeElement.Element("DLessonPast").Value = trainee.DLessonPast.ToString();
            TraineesRoot.Save(TraineesPath);
        }


        public List<Tester> TestersCollection()
        {
            if (!File.Exists(TestersPath))
                return null;
            return LoadFromXML<List<Tester>>(TestersPath);
        }
        public List<Test> TestsCollection()
        {
            if (!File.Exists(TestsPath))
                return null;
            return LoadFromXML<List<Test>>(TestsPath); ;
        }
        public List<Trainee> TraineesCollection()
        {

            List<Trainee> trainees;
            try
            {
                trainees = (from trainee in TraineesRoot.Elements()
                            select new Trainee("")
                            {
                                //property
                                Id = trainee.Element("Id").Value,
                                PrivateName = trainee.Element("PrivateName").Value,
                                FamilyName = trainee.Element("FamilyName").Value,
                                Gender = (Gender)Enum.Parse(typeof(Gender), trainee.Element("Gender").Value),
                                Phone = trainee.Element("Phone").Value,
                                Address = new Address(trainee.Element("Address").Element("City").Value, trainee.Element("Address").Element("Street").Value, trainee.Element("Address").Element("NumOfHome").Value),
                                DOB = DateTime.Parse(trainee.Element("DOB").Value),
                                TypeOfCar = (Car)Enum.Parse(typeof(Car), trainee.Element("TypeOfCar").Value),
                                TypeGearBox = (Gearbox)Enum.Parse(typeof(Gearbox), trainee.Element("TypeGearBox").Value),
                                DrivingSchool = trainee.Element("DrivingSchool").Value,
                                DrivingTeacher = trainee.Element("DrivingTeacher").Value,
                                DLessonPast = int.Parse(trainee.Element("DLessonPast").Value),
                                Code = trainee.Element("Code").Value
                            }).ToList();
            }
            catch
            {
                trainees = null;
            }
            return trainees;
        }


        public static void CheckTrainee(Trainee trainee)
        {

            if (!trainee.PrivateName.All(Char.IsLetter))
                throw new Exception("שם פרטי אינו תקין");

            if (!trainee.FamilyName.All(Char.IsLetter))
                throw new Exception("שם משפחה אינו תקין");

            if (!IdCheck(trainee.Id))
                throw new Exception("מספר תעודת זהות אינו תקין");

            if (!trainee.DrivingSchool.All(Char.IsLetter))
                throw new Exception("שם בית הספר אינו תקין");

            if (!trainee.DrivingTeacher.All(Char.IsLetter))
                throw new Exception("שם המורה אינו תקין");

            if (trainee.Phone.Length != 10 || !trainee.Phone.All(Char.IsDigit))
                throw new Exception("מספר הפלאפון אינו תקין");

            if (trainee.DLessonPast < 0)
                throw new Exception("מספר השיעורים שעברת אינו תקין");

            if (!trainee.Address.City.All(char.IsLetter))
                throw new Exception("שם העיר אינו תקין");

            if (!trainee.Address.Street.All(char.IsLetter))
                throw new Exception("שם הרחוב אינו תקין");

            if (!trainee.Address.NumOfHome.All(char.IsDigit))
                throw new Exception("מספר הבית אינו תקין");

        }
        public static bool IdCheck(string id)
        {
            if (id == null)
                return false;

            int tmp, count = 0;

            if (!(int.TryParse(id, out tmp)) || id.Length != 9)
                return false;

            int[] id_12_digits = { 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            id = id.PadLeft(9, '0');

            for (int i = 0; i < 9; i++)
            {
                int num = Int32.Parse(id.Substring(i, 1)) * id_12_digits[i];

                if (num > 9)
                    num = (num / 10) + (num % 10);

                count += num;
            }

            return (count % 10 == 0);
        }
        public static void CheckTester(Tester tester)
        {

            if (!tester.PrivateName.All(Char.IsLetter))
                throw new Exception("שם פרטי אינו תקין");

            if (!tester.FamilyName.All(Char.IsLetter))
                throw new Exception("שם המשפחה אינו תקין");

            if (!IdCheck(tester.Id))
                throw new Exception("מספר תעודת זהות אינו תקין");

            if (tester.Phone.Length != 10 || !tester.Phone.All(Char.IsDigit))
                throw new Exception("מספר הפלאפון אינו תקין");

            if (tester.MaxTests < 0)
                throw new Exception("מספר המבחנים המקסימאלי אינו תקין");

            if (tester.MaxRange < 0)
                throw new Exception(tester + ":the Max Range cant be  a negative number");

            if (!tester.Address.City.All(char.IsLetter))
                throw new Exception("שם העיר אינו תקין");

            if (!tester.Address.Street.All(char.IsLetter))
                throw new Exception("שם הרחוב אינו תקין");
            if (!tester.Address.NumOfHome.All(char.IsDigit))
                throw new Exception("מספר הבית אינו תקין");

        }
        public static void CheckTest(Test test)
        {
            if (test.TestTime.All(char.IsLetter))
                throw new Exception(test + ":wrong test time");
            if (test.TestTime != test.TestDay.Day + "/" + test.TestDay.Month + "/" + test.TestDay.Year)
                throw new Exception(test + ":the dates arent same");
            if ((test.IdTester != null) && !IdCheck(test.IdTester))
                throw new Exception(test + ":Wrong id tester!");
            if (!IdCheck(test.IdTrainee))
                throw new Exception(test + ":Wrong id trainee!");
            if (!test.TestAddress.Street.All(char.IsLetter))
                throw new Exception(test + ":Wrong street name!");

            if (!test.TestAddress.City.All(char.IsLetter))
                throw new Exception(test + ":Wrong city name!");
            foreach (Criterion item in test.Criterions)
            {
                if (!item.name.All(char.IsLetter))
                {
                    throw new Exception(test + ":wrong Criterion " + item.name + "!");
                }
            }
        }
        public IEnumerable<Tester> testersByName()
        {
            return from item in TestersCollection() orderby item.ToString() select new Tester(item);
        }


    }
}

