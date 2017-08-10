using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadPatientData
{
    class Patient
    {
        public int id { get; set; }
	    public string firstName { get; set; }
        public string lastName { get; set; }
        public int age { get; set; }
        public bool recordNotEmpty { get; set; }


    }
}
