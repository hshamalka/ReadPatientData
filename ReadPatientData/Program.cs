using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; 
using System.Threading.Tasks;
using System.Xml;
using System.Net.Http;

namespace ReadPatientData
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Patient> patients = new List<Patient>();
            string line;
            string fileName = "patients.dat";

            Console.WriteLine(String.Format("Processing file: {0} ....\n", fileName)); 

            //get relative path to patient.dat
            string path = Directory.GetCurrentDirectory(); // returns ~\ReadPatientData\ReadPatientData\bin\Debug
            DirectoryInfo parentDir = Directory.GetParent(path); //get path to bin dir
            path = parentDir.Parent.FullName; //get string path to ReadPatientData
            path = path + "//" + fileName;

            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(path);

                while ((line = file.ReadLine()) != null)
                {
                    Patient patient = getPatient(line);

                    if(patient.recordNotEmpty) //assumed first or last name is mandatory, if both not given, record will not be inserted to DB
                    {
                        Console.WriteLine(String.Format("Inserting patient: {0} {1} into DB", patient.firstName, patient.lastName));
                        saveToDB(patient);
                        //patients.Add(patient);
                    }

                }

                testDB();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 

            }

            Console.WriteLine("\nEnd of Process!");
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }


        //read each line byte by byte and extract patient data into Patient object
        public static Patient getPatient(string line)
        {
            StringBuilder stream = new StringBuilder();
            Patient patient = new Patient();
            byte [] bytes = Encoding.ASCII.GetBytes(line);

            foreach (byte b in bytes)
            {
                Char c = Convert.ToChar(b);               
                if (Char.IsLetterOrDigit(c))
                {
                    stream = stream.Append(c);
                }

                if (!Char.IsLetterOrDigit(c) && !String.IsNullOrEmpty(stream.ToString()))
                {

                    if (patient.firstName == null)
                    {
                        patient.firstName = stream.ToString();
                        patient.recordNotEmpty = true; //incase patient has given only first name
                    }
                    else if (patient.lastName == null)
                    {
                        patient.lastName = stream.ToString();
                        patient.recordNotEmpty = true; //incase patient has given only last name
                    }
                    else
                    {
                        int age;
                        if (int.TryParse(stream.ToString(), out age))
                        {
                            patient.age = age;   
                        }
                    }
                    stream.Clear();
                }           
            }
            return patient;
        }


        //insert the read patient record to DB
        public static void saveToDB(Patient patient)
        {
            try
            {
                PatientDBEntities db = new PatientDBEntities();

                tblPatient patientTemp = new tblPatient();

                patientTemp.first_name = patient.firstName;
                patientTemp.last_name = patient.lastName;
                patientTemp.age = patient.age;

                db.tblPatients.Add(patientTemp);
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }

        }

        //retrieve data from tblPatient for tasting
        public static void testDB()
        {
            PatientDBEntities db = new PatientDBEntities();
            var patients = db.tblPatients.ToList();

            Console.WriteLine("\n######## Reading from DB #########");

            foreach (tblPatient p in patients)
            {
                string output = String.Format("firstName:{0} lastName:{1} age:{2}", p.first_name, p.last_name, p.age);
                Console.WriteLine(output); 
            }

        }
    }
}

