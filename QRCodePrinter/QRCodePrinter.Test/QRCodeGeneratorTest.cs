using NUnit.Framework;
using QRCodeRenerator;

namespace QRCodePrinter.Test
{
    public class QRCodeGeneratorTest
    {
        private QRCodeGeneraotor qRCodeGeneraotor;
        private static string _path = @"test.pdf";

        private static string _date = "test";
        private static string _number = "testNumber";
        private static string _type = "testType";
        private static string _factory = "testFactory";

        [Test]
        public void TestSetSize()
        {
            qRCodeGeneraotor = new QRCodeGeneraotor();

            int width = 50;
            int height = 100;

            qRCodeGeneraotor.SetSize(width, height);

            Assert.AreEqual(width, qRCodeGeneraotor.GetWight());
            Assert.AreEqual(height, qRCodeGeneraotor.GetHeight());
        }

        [Test]
        public void TestSetSavePath()
        {
            qRCodeGeneraotor = new QRCodeGeneraotor();
            qRCodeGeneraotor.SetSavePath(_path);

            Assert.AreEqual(_path, qRCodeGeneraotor.GetSavePath());
        }

        [Test]
        public void TestCreateQRCode()
        {
            qRCodeGeneraotor = new QRCodeGeneraotor();

            qRCodeGeneraotor.SetSavePath(_path);
            qRCodeGeneraotor.GenerateQRCode(_date, _number, _type, _factory);

            Assert.Pass();
        }
    }
}