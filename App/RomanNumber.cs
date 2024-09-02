using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class RomanNumber(int  Value)
    {
        private readonly int _value = Value;
        public int Value { get { return _value; } }

        public static RomanNumber Parse(string Value)
        {
            return new(0);
        }
    }
}
