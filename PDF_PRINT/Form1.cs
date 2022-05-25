using System;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
using System.Windows.Forms;
using RawPrint;


namespace PrintPreviewApp
{
    public partial class Form1 : Form
    {
        private Button printPreviewButton;

        private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();
        private PrintDocument printDocument1 = new PrintDocument();

        Image image1;
        public Form1()
        {
            InitializeComponent();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
        }
 
        void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(image1, 0, 0, 270, 190);
       }

        private void button1_Click(object sender, EventArgs e)
        {
            image1 = Image.FromFile(@"E:\2022\Printer Pronon ttp 4206\2.jpeg");
            printDocument1.PrinterSettings.PrinterName = "PROTON TTP-4206";


            if (printDocument1.PrinterSettings.IsValid)
            {
                printPreviewDialog1.Document = printDocument1;
                // printPreviewDialog1.ShowDialog();
                printDocument1.Print();
            }
            else {
                MessageBox.Show("Printer is invalid.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Filepath = @"E:\2022\Printer Pronon ttp 4206\document.pdf";
            string Filename = "2.pdf";
            string PrinterName = @"EPSONA84F29 (M2170 Series)";//"PROTON TTP-4206";
            //IPrinter printer = new Printer();
            //printer.PrintRawFile(PrinterName, Filepath, Filename);
            try
            {

               IPrinter printer = new Printer();
                printer.PrintRawFile(PrinterName, Filepath, Filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            image1 = Image.FromFile(@"E:\2022\Printer Pronon ttp 4206\2.jpeg");
            printDocument1.PrinterSettings.PrinterName = "PROTON TTP-4206";
            

            if (printDocument1.PrinterSettings.IsValid)
            {
                printPreviewDialog1.Document = printDocument1;
                printPreviewDialog1.ShowDialog();
            }
            else {
                MessageBox.Show("Printer is invalid.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string S="";
            foreach (string printerName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                S = S + printerName + "\n";
            }
                            MessageBox.Show(S);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string Filepath = @"E:\2022\Printer Pronon ttp 4206\document.pdf";
            string Filename = "2.pdf";
            string PrinterName = @"EPSONA84F29 (M2170 Series)";
            System.IO.File.Copy(Filepath, PrinterName);
        }
    }
}