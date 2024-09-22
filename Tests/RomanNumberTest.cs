using App;

namespace Tests
{
    [TestClass]
    public class RomanNumberTest
    {
        private Dictionary<char, int> digits = new()
        {
            {'N', 0},
            {'I', 1},
            {'V', 5},
            {'X', 10},
            {'L', 50},
            {'C', 100},
            {'D', 500},
            {'M', 1000},
        };

        [TestMethod]
        public void TestToShort()
        {
            RomanNumber rn = new(123);
            short result = (short)rn;
            Assert.AreEqual(123, result);
        }

        [TestMethod]
        public void TestToByte()
        {
            RomanNumber rn = new(100);
            byte result = (byte)rn;
            Assert.AreEqual(100, result);
        }

        [TestMethod]
        public void TestToLong()
        {
            RomanNumber rn = new(1234);
            long result = (long)rn;
            Assert.AreEqual(1234, result);
        }

        [TestMethod]
        public void TestToInt()
        {
            RomanNumber rn = new(567);
            int result = (int)rn;
            Assert.AreEqual(567, result);
        }

        [TestMethod]
        public void TestToFloat()
        {
            RomanNumber rn = new(345);
            float result = (float)rn;
            Assert.AreEqual(345f, result);
        }

        [TestMethod]
        public void TestToDouble()
        {
            RomanNumber rn = new(678);
            double result = (double)rn;
            Assert.AreEqual(678d, result);
        }

        [TestMethod]
        public void TestParseToInt()
        {
            RomanNumber rn = RomanNumber.Parse("CXXIII"); // 123
            int result = (int)rn;
            Assert.AreEqual(123, result);
        }

        [TestMethod]
        public void ToStringTest()
        {
            var testCases = new Dictionary<int, String>()
            {
                {4 , "IV"},
                {6 , "VI"},
                {19 , "XIX"},
                {49 , "XLIX"},
                {95 , "XCV"},
                {444 , "CDXLIV"},
                {946 , "CMXLVI"},
                {3333 , "MMMCCCXXXIII"},
            }
            .Concat(digits.Select(d => new
                KeyValuePair<int, string>(d.Value, d.Key.ToString()))
            )
            .ToDictionary();
            foreach (var testCase in testCases)
            {
                RomanNumber rn = new(testCase.Key);
                var value = rn.ToString();
                Assert.IsNotNull(value);
                Assert.AreEqual(testCase.Value, value);
            }
        }

        [TestMethod]
        public void CrossTest_Parse_ToString()
        {
            // Наявність двох методів протилежної роботи дозволяє
            // використовувати крос-тести, які послідовно застосовують
            // два методи і одержують початковий результат
            // "XIX" -Parse-> 19 -ToString-> "XIX"  v
            // "IIII" -Parse-> 19 -ToString-> "IV"  x
            // 4 -ToString-> "IV -Parse-> 4     v
            for (int i = 0; i <= 1000; i++)
            {
                int c = RomanNumberParser.FromString(new RomanNumber(i).ToString()!).Value;
                Assert.AreEqual(
                    i,
                    c,
                    $"Cross test for {i}: {new RomanNumber(i)} -> {c}"
                );
            }
        }

        [TestMethod]
        public void ParseTest()
        {
            TestCase[] testCases =
            [
                new( "N", 0),
                new( "I",  1),
                new( "II",  2),
                new( "III",  3),
                new( "IIII",  4),
                new( "V",  5),
                new( "X",  10),
                new( "D",  500),
                new( "IV",  4),
                new( "VI",  6),
                new( "XI",  11),
                new( "IX",  9),
                new( "MM",  2000),
                new( "MCM", 1900),
                #region HW
                new( "XL", 40),
                new( "XC", 90),
                new( "CD", 400),
                new( "CMII", 902),
                new( "DCCCC", 900),
                new( "CCCC", 400),
                new( "XXXXX", 50),
                #endregion

            ];
            foreach (var testCase in testCases)
            {
                RomanNumber rn = RomanNumber.Parse(testCase.Source);
                Assert.IsNotNull(rn, $"Parse result of '{testCase.Source}' is not null");
                Assert.AreEqual(
                    testCase.Value,
                    rn.Value,
                    $"Parse '{testCase.Source}' => {testCase.Value}"
                    );
            }
            /* Виняток парсера - окрім причини винятку містить відомості
             * про місце виникнення помилки (позиція у рядку)
             */
            String tpl1 = "illegal symbol '%r1'";
            String tpl2 = "in position %r1";
            String tpl3 = "RomanNumber.Parse";
            String tpl4 = "illegal sequence: more than one smaller digits before '%r1'";
            String tpl5 = "illegal sequence: '%r1' before '%r2'";
            testCases = [
                new( "W",   [ tpl1.R(["W"]), tpl2.R(["0"]), tpl3 ] ),
                new( "CS",  [ tpl1.R(["S"]), tpl2.R(["1"]), tpl3 ] ),
                new( "CX1", [ tpl1.R(["1"]), tpl2.R(["2"]), tpl3 ] ),
                // Перед цифрою є декілька цифр, менших за неї
                // !! кожна пара цифр - правильна комбінація,
                //    проблема створюється щонайменше трьома цифрами                exCase3  
                new( "IIX", [ tpl4.R(["X"]), tpl2.R(["2"]), tpl3 ] ),
                new( "VIX", [ tpl4.R(["X"]), tpl2.R(["2"]), tpl3 ] ),
                new( "XXC", [ tpl4.R(["C"]), tpl2.R(["2"]), tpl3 ] ),
                new( "IXC", [ tpl4.R(["C"]), tpl2.R(["2"]), tpl3 ] ),
                //                                                                  exCase4
                new(  "VX",   [ tpl5.R(["V", "X"]), tpl2.R(["0"]), tpl3 ] ),
                new(  "LC",   [ tpl5.R(["L", "C"]), tpl2.R(["0"]), tpl3 ] ),
                new(  "DM",   [ tpl5.R(["D", "M"]), tpl2.R(["0"]), tpl3 ] ),
                new(  "IC",   [ tpl5.R(["I", "C"]), tpl2.R(["0"]), tpl3 ] ),
                new(  "MIM",  [ tpl5.R(["I", "M"]), tpl2.R(["1"]), tpl3 ] ),
                new(  "MVM",  [ tpl5.R(["V", "M"]), tpl2.R(["1"]), tpl3 ] ),
                new(  "MXM",  [ tpl5.R(["X", "M"]), tpl2.R(["1"]), tpl3 ] ),
                new(  "CVC",  [ tpl5.R(["V", "C"]), tpl2.R(["1"]), tpl3 ] ),
                new(  "MCVC", [ tpl5.R(["V", "C"]), tpl2.R(["2"]), tpl3 ] ),
                new(  "DCIC", [ tpl5.R(["I", "C"]), tpl2.R(["2"]), tpl3 ] ),
                new(  "IM",   [ tpl5.R(["I", "M"]), tpl2.R(["0"]), tpl3 ] ),
                ];
            foreach (var exCase in testCases)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(exCase.Source),
                    $"RomanNumber.Parse(\"{exCase.Source}\") must throw FormatException"
                    );
                // накладаємо вимоги на повідомлення
                // - має містити сам символ, що призводить до винятку
                // - має містити позицію символу в рядку
                // - має містити назву методу та класу
                foreach (String part in exCase.ExMessageParts!)
                {
                    Assert.IsTrue(
                    ex.Message.Contains(part),
                    $"ex.Message must contain '{part}'; ex.Message: {ex.Message}"
                    );
                }
            }

        }

        [TestMethod]
        public void DigitValueChar()
        {
            //foreach (var testCase in digits)
            //{
            //    Assert.AreEqual(testCase.Value,
            //        RomanNumber.DigitValue(testCase.Key),
            //        $"{testCase.Key} => {testCase.Value}"
            //        );
            //}

            //char[] excCases = { '1', 'x', 'i', '&' };

            //foreach (var testCase in excCases)
            //{
            //    var ex = Assert.ThrowsException<ArgumentException>(
            //    () => RomanNumber.DigitValue(testCase),
            //    $"DigitValue({testCase}) must throw ArgumentException"
            //    );
            //    Assert.IsTrue(
            //    ex.Message.Contains($"'{testCase}'"),
            //        "DigitValue ex.Message should contain a symbol which cause exception:" +
            //        $" symbol: '{testCase}', ex.Message: '{ex.Message}'"
            //        );
            //    Assert.IsTrue(
            //        ex.Message.Contains($"{nameof(RomanNumber)}") &&
            //        ex.Message.Contains($"{nameof(RomanNumber.DigitValue)}"),
            //        "DigitValue ex.Message should contain a symbol which cause exception:" +
            //        $" symbol: '{testCase}', ex.Message: '{ex.Message}'"
            //        );
            //}

            #region HW2

            char[] excCases = { '0', '1', 'x', 'i', '&' };

            foreach (var digit in excCases)
            {
                var ex = Assert.ThrowsException<ArgumentException>(
                () => RomanNumberParser.DigitValue(digit),
                $"DigitValue({digit}) must throw ArgumentException"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{digit}'"),
                    "Not valid Roman digit:" +
                    $" argument: ('{digit}'), ex.Message: {ex.Message}"
                    );
                Assert.IsTrue(
                    ex.Message.Contains($"{nameof(RomanNumber)}") &&
                    ex.Message.Contains($"DigitValue"),
                    "Not valid Roman digit:" +
                    $" argument: ('{digit}'), ex.Message: {ex.Message}"
                    );
            }

            #endregion
        }
    }

    record TestCase(String Source, int? Value, IEnumerable<String>? ExMessageParts = null)
    {
        public TestCase(String Source, IEnumerable<String> parts)
            : this(Source, null, parts) { }
    }

    public static class StringExtension
    {
        public static String F(this String Source, IEnumerable<String> olds,
            IEnumerable<String> news)
        {
            String res = Source;
            foreach (var item in olds.Zip(news))
                res = res.Replace(item.First, item.Second);
            return res;
        }
        public static String R(
            this String Source,
            IEnumerable<String> replaces)
        {
            String res = Source;
            int i = 0;
            foreach (var r in replaces)
            {
                ++i;
                res = res.Replace($"%r{i}", r);
            }
            return res;
        }
    }

}

/* Тестовий проєкт за структурою відтворює основний проєкт: 
 * - його папки відповідають папкам основного проєкту
 * - його класи називають як і проєктні, з дописом Test
 * - методи класів також відтворюють методи випробуваних класів
 *     і також з дописом Test
 *     
 * Основу тестів складають вислови (Assert)
 * Тест вважається пройденим якщо всі його вислови істинні
 * проваленим - якщо хоча б один висів хибний
 * 
 * 
 */