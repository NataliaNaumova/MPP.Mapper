using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoMappingTests
{
    class LandAnimal
    {
        public int Age { get; set; }
        public double NumberOfLegs { get; set; }
        public byte NumberOfEyes { get; set; }
        public List<string> Names { get; set; }
        public IEnumerable<int> Dimensions { get; set; }

        private sealed class LandAnimalEqualityComparer : IEqualityComparer<LandAnimal>
        {
            public bool Equals(LandAnimal x, LandAnimal y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.Age == y.Age && x.NumberOfLegs.Equals(y.NumberOfLegs) && x.NumberOfEyes == y.NumberOfEyes && Equals(x.Names, y.Names) && Equals(x.Dimensions, y.Dimensions);
            }

            public int GetHashCode(LandAnimal obj)
            {
                unchecked
                {
                    var hashCode = obj.Age;
                    hashCode = (hashCode*397) ^ obj.NumberOfLegs.GetHashCode();
                    hashCode = (hashCode*397) ^ obj.NumberOfEyes.GetHashCode();
                    hashCode = (hashCode*397) ^ (obj.Names != null ? obj.Names.GetHashCode() : 0);
                    hashCode = (hashCode*397) ^ (obj.Dimensions != null ? obj.Dimensions.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        private static readonly IEqualityComparer<LandAnimal> LandAnimalComparerInstance = new LandAnimalEqualityComparer();

        public static IEqualityComparer<LandAnimal> LandAnimalComparer
        {
            get { return LandAnimalComparerInstance; }
        }
    }
}
