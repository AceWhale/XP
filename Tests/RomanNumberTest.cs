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
            Dictionary<String, int> testCases = new()
            {
                { "N", 0},
                { "I", 1},
                { "II", 2},
                { "III", 3},
                { "IIII", 4},
                { "V", 5},
                { "X", 10},
                { "D", 500},
                { "IV", 4},
                { "VI", 6},
                { "XI", 11},
                { "IX", 9},
                { "MM", 2000},
                { "MCM", 1900},
                #region HW
                { "XL", 40},
                { "XC", 90},
                { "CD", 400},
                { "CMII", 902},
                { "DCCCC", 900},
                { "CCCC", 400},
                //{ "XIXIIII", 23},
                { "XXXXX", 50},
                //{ "DDMD", 1500},
                #endregion

            };
            foreach (var testCase in testCases)
            {
                RomanNumber rn = RomanNumber.Parse(testCase.Key);
                Assert.IsNotNull(rn, $"Parse result of '{testCase.Key}' is not null");
                Assert.AreEqual(
                    testCase.Value,
                    rn.Value,
                    $"Parse '{testCase.Key}' => {testCase.Value}"
                    );
            }
            /* Виняток парсера - окрім причини винятку містить відомості
             * про місце виникнення помилки (позиція у рядку)
             */
            Object[][] exCases = [
                ["W", "W", 0],
                ["CS", "S", 1],
                ["CX1", "1", 2],
            ];
            foreach (var exCase in exCases)
            {
                var ex = Assert.ThrowsException<FormatException>(
                    () => RomanNumber.Parse(exCase[0].ToString()!),
                    $"RomanNumber.Parse(\"{exCase[0]}\") must throw FormatException"
                    );
                // накладаємо вимоги на повідомлення
                // - має містити сам символ, що призводить до винятку
                // - має містити позицію символу в рядку
                // - має містити назву методу та класу
                Assert.IsTrue(
                    ex.Message.Contains($"illegal symbol '{exCase[1]}'"),
                    $"ex.Message must contain symbol which cause error: '{exCase[1]}', ex.Message: {ex.Message}"
                    );
                Assert.IsTrue(
                    ex.Message.Contains($"in position {exCase[2]}"),
                    "ex.Message must contain error symbol position"
                    );
                Assert.IsTrue(
                    ex.Message.Contains(nameof(RomanNumber)) &&
                    ex.Message.Contains(nameof(RomanNumber)),
                    $"ex.Message must contain names of class and method, ex.Message: {ex.Message}"
                    );
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