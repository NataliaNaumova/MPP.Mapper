using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoMapping
{
    public sealed class ImplicitNumericConversionsChecker
    {
        private static readonly Lazy<ImplicitNumericConversionsChecker> Lazy = new Lazy<ImplicitNumericConversionsChecker>(() => new ImplicitNumericConversionsChecker());
        public static ImplicitNumericConversionsChecker Instance { get { return Lazy.Value; } }
        private Dictionary<Type, List<Type>> convertibleValueTypes;

        private ImplicitNumericConversionsChecker()
        {
            convertibleValueTypes = new Dictionary<Type, List<Type>>() {
            { typeof(decimal), new List<Type> { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(char) } },
            { typeof(double), new List<Type> { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(char), typeof(float) } },
            { typeof(float), new List<Type> { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong), typeof(char), typeof(float) } },
            { typeof(ulong), new List<Type> { typeof(byte), typeof(ushort), typeof(uint), typeof(char) } },
            { typeof(long), new List<Type> { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(char) } },
            { typeof(uint), new List<Type> { typeof(byte), typeof(ushort), typeof(char) } },
            { typeof(int), new List<Type> { typeof(sbyte), typeof(byte), typeof(short), typeof(ushort), typeof(char) } },
            { typeof(ushort), new List<Type> { typeof(byte), typeof(char) } },
            { typeof(short), new List<Type> { typeof(byte) } }
            };
        }

        public bool ImplicitNumericConversionExists(Type tSource, Type tDestination)
        {
            if (tSource == null)
            {
                throw new ArgumentNullException(nameof(tSource));
            }

            if (tDestination == null)
            {
                throw new ArgumentNullException(nameof(tDestination));
            }

            if ((!tSource.IsPrimitive) ||
                (!tSource.IsValueType))
            {
                throw new ArgumentException("Type must be a primitive value type.", nameof(tSource));
            }

            if ((!tDestination.IsPrimitive) || (!tDestination.IsValueType))
            {
                throw new ArgumentException("Type must be a primitive value type.", nameof(tDestination));
            }

            if ((convertibleValueTypes.ContainsKey(tDestination)) && (convertibleValueTypes[tDestination].Contains(tSource)))
            {
                return true;
            }
            else
            {
                return false;
            }
            }
    }
}
