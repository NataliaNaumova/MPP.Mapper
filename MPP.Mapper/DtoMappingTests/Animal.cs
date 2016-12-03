using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoMappingTests
{
    class Animal
    {
        public int Age { get; set; }
        public float NumberOfLegs { get; set; }
        public long NumberOfEyes { get; set; }
        public List<string> Names { get; set; }
        public List<int> Dimensions { get; set; }
    }
}
