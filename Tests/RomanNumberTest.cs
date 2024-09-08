using App;

namespace Tests
{
    [TestClass]
    public class RomanNumberTest
    {
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
                { "XIXIIII", 23},
                { "XXXXX", 50},
                { "DDMD", 1500},
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
        }

        [TestMethod]
        public void DigitValueChar()
        {
            //Dictionary<char, int> testCases = new()
            //{
            //    {'N', 0},
            //    {'I', 1},
            //    {'V', 5},
            //    {'X', 10},
            //    {'L', 50},
            //    {'C', 100},
            //    {'D', 500},
            //    {'M', 1000},
            //};
            //foreach (var testCase in testCases)
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

            char[] excCases = { 'N', '1', 'x', 'i', '&' };

            foreach (var digit in excCases)
            {
                var ex = Assert.ThrowsException<ArgumentException>(
                () => RomanNumber.DigitValue(digit),
                $"DigitValue({digit}) must throw ArgumentException"
                );
                Assert.IsTrue(
                    ex.Message.Contains($"'{digit}'"),
                    "Not valid Roman digit:" +
                    $" argument: ('{digit}'), ex.Message: {ex.Message}"
                    );
                Assert.IsTrue(
                    ex.Message.Contains($"{nameof(RomanNumber)}") &&
                    ex.Message.Contains($"{nameof(RomanNumber.DigitValue)}"),
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