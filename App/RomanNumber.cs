using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    public class RomanNumber(int Value)
    {
        private readonly int _value = Value;
        public int Value { get { return _value; } }

        public static RomanNumber Parse(string Value)
        {
            int result = 0;
            int prevDigit = 0;      // TODO: rename, 'prev' not semantics
            int pos = Value.Length;
            int maxDigit = 0;       // найбільша цифра, що пройдена
            int lessCounter = 0;    // кількість цифр, менших за неї
            int maxCounter = 1;     // кількість однакових найбільших цифр
            foreach (char c in Value.Reverse())
            {
                pos--;
                int digit;
                try
                {
                    digit = DigitValue(c);
                }
                catch (ArgumentException)
                {
                    throw new FormatException(
                        $"{nameof(RomanNumber)}.{nameof(Parse)}() found illegal symbol '{c}' in position {pos}");
                }
                // "відстань" між цифрами
                if (digit != 0 && prevDigit / digit > 10)  //цифри занадто "далекі" для віднімання
                {
                    throw new FormatException(
                        $"{nameof(RomanNumber)}.{nameof(Parse)}() " +
                        $"illegal sequence: '{c}' before '{Value[pos + 1]}' in position {pos}");
                }
                // віднімання цифр, що є "5"-ками
                if (digit < prevDigit && (digit == 5 || digit == 50 || digit == 500))
                {
                    throw new FormatException(
                        $"{nameof(RomanNumber)}.{nameof(Parse)}() " +
                        $"illegal sequence: '{c}' before '{Value[pos + 1]}' in position {pos}");
                }

                // рахуємо цифри, якщо вони менші за максимальну
                if (digit > maxDigit)
                {
                    maxDigit = digit;
                    lessCounter = 0;
                    maxCounter = 1;
                }
                else if (digit == maxDigit)
                {
                    maxCounter += 1;
                    lessCounter = 0;
                }
                else
                {
                    lessCounter += 1;
                }
                if (lessCounter > 1 || lessCounter > 0 && maxCounter > 1)
                {
                    
                    throw new FormatException(
                        $"{nameof(RomanNumber)}.{nameof(Parse)}() " +
                        $"illegal sequence: more than one smaller digits before '{Value[Value.Length -1]}' in position {Value.Length -1}");
                }
                if (Value.Length > 1 && digit == 0)
                {
                    throw new FormatException(Value);
                }

                result += digit < prevDigit ? -digit : digit;
                prevDigit = digit;
            }

            return new RomanNumber(result);
        }

        public static int DigitValue(char digit) => digit switch
        {
            'N' => 0,
            'I' => 1,
            'V' => 5,
            'X' => 10,
            'L' => 50,
            'C' => 100,
            'D' => 500,
            'M' => 1000,
            _ => throw new ArgumentException($"{nameof(RomanNumber)}.{nameof(DigitValue)}() ('{digit}')"),
        };
    }
}
