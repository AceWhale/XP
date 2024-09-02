using App;

namespace Tests
{
    [TestClass]
    public class RomanNumberTest
    {
        [TestMethod]
        public void ParseTest()
        {
            RomanNumber rn = RomanNumber.Parse("");
            //rn = null!;
            Assert.IsNotNull(rn, "Parse result is not null");
            Assert.AreEqual(0, rn.Value, "Zero testing");
        }
    }
}

/* �������� ����� �� ���������� �������� �������� �����: 
 * - ���� ����� ���������� ������ ��������� ������
 * - ���� ����� ��������� �� � ������, � ������� Test
 * - ������ ����� ����� ���������� ������ ������������ �����
 *     � ����� � ������� Test
 *     
 * ������ ����� ��������� ������� (Assert)
 * ���� ��������� ��������� ���� �� ���� ������� ������
 * ���������� - ���� ���� � ���� ���� ������
 * 
 * 
 */