using PdfSharp.Drawing;
using PdfSharp.Pdf;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRCodeRenerator
{
    public class QRCodeGeneraotor
    {
        private static int _widht = 95;
        private static int _height = 160;
        private static string _savePath = null;
        private static XFont _fontForInfo = new XFont("Arial", 11, XFontStyle.Bold);
        private static XFont _fontForHeader = new XFont("Arial", 5, XFontStyle.Regular);

        private static string APPLICATION_NUMBER = "Номер заявления-квитанции";
        private static string TYPE_CI = "ТИП CИ";
        private static string FACTORY_NUMBER = "Заводской номер";

        public QRCodeGeneraotor()
        {

        }

        public void SetSize(int widht, int height)
        {
            _widht = widht;
            _height = height;
        }

        public int GetWight()
        {
            return _widht;
        }

        public int GetHeight()
        {
            return _height;
        }

        public void SetSavePath(String savePath)
        {
            _savePath = savePath;
        }

        public string GetSavePath()
        {
            return _savePath;
        }

        public void GenerateQRCode(String qrData, String number, String typeCi, String factoryNumber)
        {
            if (!string.IsNullOrEmpty(_savePath) && !string.IsNullOrEmpty(qrData)) 
            {
                using (PdfDocument document = new PdfDocument())
                {
                    Bitmap bitmap = GenerateQRBitmap(qrData);
                    var image = XImage.FromStream(BitmapToBytes(bitmap));
                    var page = document.AddPage();
                    page.Width = _widht;
                    page.Height = _height;

                    var gfx = XGraphics.FromPdfPage(page);
                    gfx.MUH = PdfFontEncoding.Unicode;
                    gfx.DrawImage(image, new XPoint(0, 0));
                    int prevP = 45;

                    if (!string.IsNullOrEmpty(number))
                    {
                        prevP = AddBlock(gfx, APPLICATION_NUMBER, number, prevP);
                    }

                    if (!string.IsNullOrEmpty(typeCi))
                    {
                        prevP = AddBlock(gfx, TYPE_CI, typeCi, prevP);
                    }

                    if (!string.IsNullOrEmpty(factoryNumber))
                    {
                        prevP = AddBlock(gfx, FACTORY_NUMBER, factoryNumber, prevP);
                    }

                    document.Save(_savePath);
                }
            }
        }

        public void GenerateQRCode(String savePath, String qrData, String number, String typeCi, String factoryNumber)
        {
            if (!string.IsNullOrEmpty(savePath))
            {
                SetSavePath(savePath);
                GenerateQRCode(qrData, number, typeCi, factoryNumber);
            }
        }

        private Bitmap GenerateQRBitmap(String text)
        {
            using (QRCodeGenerator qrGeneraotr = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGeneraotr.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                return qrCode.GetGraphic(3);
            }
        }

        private static MemoryStream BitmapToBytes(Bitmap img)
        {
            MemoryStream stream = new MemoryStream();

            img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            stream.Position = 0;

            return stream;
        }

        private int AddBlock(XGraphics gfx, string header, string text, int previousPosition)
        {
            const int a = 30;
            const int b = 35;
            gfx.DrawString(header, _fontForHeader, XBrushes.Black, new XPoint(5, previousPosition + a), XStringFormats.TopLeft);
            gfx.DrawString(text, _fontForInfo, XBrushes.Black, new XPoint(5, previousPosition + b), XStringFormats.TopLeft);

            return previousPosition + a;
        }
    }
}