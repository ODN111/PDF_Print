using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PdfiumViewer;
using System.Drawing.Printing;

namespace PRINT_PDF
{
    public partial class Form1 : Form
    {
        PdfiumViewer.PdfViewer pdf;
        public Form1()
        {
            InitializeComponent();
            pdf = new PdfViewer();
            this.Controls.Add(pdf);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openfile(@"E:\2022\Printer Pronon ttp 4206\document.pdf");

        }

        public void openfile(string filepath)
        {
            byte[] bytes = System.IO.File.ReadAllBytes(filepath);
            var stream = new MemoryStream(bytes);
            PdfDocument pdfDocument = PdfDocument.Load(stream);
            pdf.Document = pdfDocument;
           // pdf.Document.CreatePrintDocument();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrinterSettings printerSettings = new PrinterSettings();
            printerSettings.PrinterName = "PROTONTTP4206";

            PaperSize PP = new PaperSize() ;
            //PP.Width = 70; PP.Height = 38;
            // printerSettings.DefaultPageSettings.PaperSize = PP;
            PP.PaperName = "70 x 30";
            printerSettings.DefaultPageSettings.Landscape = true;
            printerSettings.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            PdfiumViewer.PdfDocument pdfiumDoc = PdfiumViewer.PdfDocument.Load(@"E:\2022\Printer Pronon ttp 4206\test.pdf");
            PrintDocument pd = pdfiumDoc.CreatePrintDocument(PdfiumViewer.PdfPrintMode.CutMargin);
            pd.PrinterSettings = printerSettings;
            pd.Print();
        }
    }


    }
