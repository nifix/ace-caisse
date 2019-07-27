using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Globalization;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace AceCaisse
{
    public partial class Main : Form
    {

        private String _defaultFileTestingPurpose  = @"C:\\Users\\Nifix\\Documents\\sample.xlsx";
        private String _defaultExportPath = @"C:\Users\Nifix\Documents\";
        private double rowCount;
        private double colCount;
        private double montantTotal = 0;
        BackgroundWorker bgw;
        delegate void AddItemListViewCallback(string curDate, string curOperation, string curAmount);
        delegate void UpdateProgressBarCallback(int maxValue);

        

        public Main()
        {
            InitializeComponent();
            CreateBackGroundWorker();
            this.export.Enabled = false;
            this.launchParse.Enabled = false;
            this.pickImportFile.Title = "Choisissez un fichier à importer";
            this.pickImportFile.DefaultExt = "xlsx";
            this.pickImportFile.Filter = "Fichiers Excel (*.xlsx)|*.xlsx|Fichiers Excel (Anciens) (*.xls)|*.xls|(Tous les fichiers (*.*)|*.*";
            this.pickImportFile.FilterIndex = 1;
            this.pickImportFile.CheckFileExists = true;
            this.pickImportFile.CheckPathExists = true;
            this.pickImportFile.Multiselect = false;
            this.pickImportFile.InitialDirectory = @"C:\";
        }

        private void CreateBackGroundWorker()
        {
            bgw = new BackgroundWorker();
            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;

            bgw.DoWork += new DoWorkEventHandler(this.ParseExcelFile);
            bgw.ProgressChanged += new ProgressChangedEventHandler(this.bgw_ProgressChanged);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.bgw_RunWorkCompleted);
        }

        /* 
         * Function used when the user selects a file through
         * the Select File Dialog.
         * 
         * TODO : Add the SelectDialog function, check if the file is correct or not.
         * 
        */
        private void ChooseImportFile_Click(object sender, EventArgs e)
        {
           try
            {
                DialogResult dr = this.pickImportFile.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this._defaultFileTestingPurpose = this.pickImportFile.FileName;
                    this._defaultExportPath = Path.GetDirectoryName(this._defaultFileTestingPurpose);
                    this.importFileTextbox.Text = this._defaultFileTestingPurpose;
                    this.labelStatus.Text = "Fichier prêt pour l'importation.";
                    this.launchParse.Enabled = true;
                } else
                {
                    MessageBox.Show("Erreur dans la selection du fichier.", "Snif :(", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Erreur dans la selection du fichier.", "Snif :(", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
        }

        /* 
         * ProgressChanged function from Background Worker
         * Used when we load a file in the ListView.
         * 
        */
        private void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.importProgressBar.Value = e.ProgressPercentage;
            this.labelStatus.Text = e.ProgressPercentage.ToString() + " opérations traitées...";
            this.importTotal.Text = this.montantTotal.ToString() + "€";
        }

        /* 
         * RunWorkerCompleted function from BackgroundWorker
         * Used when we the import is done or cancelled.
         * 
        */
        private void bgw_RunWorkCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                labelStatus.Text = "Annulation ..";
            } else if (e.Error != null)
            {
                labelStatus.Text = "Erreur : " + e.Error.Message;
            } else
            {
                labelStatus.Text = "Réussite !";
                this.importProgressBar.Value = this.importProgressBar.Minimum;
                this.chooseImportFile.Enabled = true;
                this.launchParse.Enabled = true;
                this.export.Enabled = true;
            }
        }

        /* 
         * DoWork function from BackgroundWorker
         * It loads the data from the Excel file directly to the ListView.
         * 
        */
        private void ParseExcelFile(object sender, DoWorkEventArgs e)
        {
            this.montantTotal = 0;
            BackgroundWorker worker = sender as BackgroundWorker;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(this._defaultFileTestingPurpose);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            this.rowCount = xlRange.Rows.Count;
            this.colCount = xlRange.Columns.Count;
            this.SetProgressBar((int)this.rowCount);

            String strDate, curOperation, curAmount;
            DateTime curDate;

            for (int i = 2; i <= rowCount; i++)
            {
 
                curDate = DateTime.FromOADate(xlRange[i, 1].Value2);
                strDate = curDate.ToString("dd/MM/yyyy");
                curOperation = xlRange[i, 4].Value2.ToString();
                curAmount = xlRange[i, 5].Value2.ToString();

                if (curOperation.StartsWith("REMISE "))
                {
                    montantTotal += Convert.ToDouble(curAmount);
                    this.AddItemListView(strDate, curOperation, curAmount);
                }

                worker.ReportProgress(i);
                System.Threading.Thread.Sleep(2);
            }


            Marshal.ReleaseComObject(xlRange);
            xlRange = null;
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorksheet = null;
            xlWorkbook.Close(false, Type.Missing, Type.Missing);
            Marshal.ReleaseComObject(xlWorkbook);
            xlWorkbook = null;
            xlApp.Application.UserControl = true;
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            xlApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /* 
         * Function used to add data in the listview with delegate.
         * 
        */
        private void AddItemListView(string curDate, string curOperation, string curAmount)
        {
            if (this.listRemises.InvokeRequired)
            {
                AddItemListViewCallback d = new AddItemListViewCallback(AddItemListView);
                this.Invoke(d, new object[] { curDate, curOperation, curAmount });
            }
            else
            {
                ListViewItem newRow = new ListViewItem();
                newRow.Text = curDate;
                newRow.SubItems.Add(curOperation);
                newRow.SubItems.Add(curAmount);
                listRemises.Items.Add(newRow);
            }
        }

        /* 
         * Function used to update the progressbar with delegate.
         * 
        */
        private void SetProgressBar(int maxValue)
        {
            if (this.importProgressBar.InvokeRequired)
            {
                this.importProgressBar.Invoke(new UpdateProgressBarCallback(SetProgressBar), new object[] { maxValue });
            }
            else
            {
                this.importProgressBar.Maximum = maxValue;
                this.importProgressBar.Minimum = 2;
                this.importProgressBar.Step = 1;
            }
        }

        /* 
         * Function used to add data in the listview with delegate.
         * 
        */
        private void Main_Load(object sender, EventArgs e)
        {
            listRemises.View = View.Details;
            ColumnHeader headerDate, headerOperation, headerMontant;

            headerDate = new ColumnHeader();
            headerOperation = new ColumnHeader();
            headerMontant = new ColumnHeader();

            headerDate.Text = "Date";
            headerDate.Width = 100;
            headerOperation.Text = "Operation";
            headerOperation.Width = 300;
            headerMontant.Text = "Montant";
            headerMontant.Width = 100;

            listRemises.Columns.Add(headerDate);
            listRemises.Columns.Add(headerOperation);
            listRemises.Columns.Add(headerMontant);

            listRemises.GridLines = true;

        }

        /* 
         * On Closed
         * 
        */
        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Export_Click(object sender, EventArgs e)
        {

            int i = 2;
            this.export.Enabled = false;
            this.export.Text = "Export en cours ...";

            Excel.Application exportapp = new Excel.Application();
            Excel.Workbook exportwb = exportapp.Workbooks.Add(1);
            Excel.Worksheet exportws = (Excel.Worksheet)exportwb.Worksheets[1];
            exportws.Columns.ColumnWidth = 18;
            Excel.Range columnDate = exportws.Range["A1"].EntireColumn;
            Excel.Range columnPieceCompte = exportws.Range["B1:C1"].EntireColumn;
            Excel.Range columnLibelle = exportws.Range["D1"].EntireColumn;
            Excel.Range columnAmount = exportws.Range["E1:F1"].EntireColumn;

            columnDate.NumberFormat = "DD/MM/YYYY";
            columnDate.ColumnWidth = 10;
            columnPieceCompte.ColumnWidth = 10;
            columnLibelle.ColumnWidth = 19;
            columnAmount.ColumnWidth = 10;



            DateTime exDate;
            String strexDate, exLibelle;
            Double exAmount, exRemisecb, exRemiseCheque, exRemiseTr, exRemiseEsp;

            exportws.Cells[1, 1] = "Date";
            exportws.Cells[1, 2] = "Pièce";
            exportws.Cells[1, 3] = "Compte";
            exportws.Cells[1, 4] = "Libellé";
            exportws.Cells[1, 5] = "Débit";
            exportws.Cells[1, 6] = "Crédit";

            exRemisecb = 58000001;
            exRemiseCheque = 58000002;
            exRemiseTr = 58000003;
            exRemiseEsp = 58000004;



            foreach (ListViewItem lvi in listRemises.Items)
            {

                exDate = Convert.ToDateTime(lvi.SubItems[0].Text);
                exLibelle = lvi.SubItems[1].Text;
                exAmount = Convert.ToDouble(lvi.SubItems[2].Text);

                // Génération Ligne 1 avec les 58xxxx
                strexDate = exDate.ToString("MM/dd/yyyy");
                exportws.Cells[i, 1] = strexDate;


                // Génération dynamique selon le type de remise

                if (exLibelle.StartsWith("REMISE CB"))
                {
                    exportws.Cells[i, 3] = exRemisecb;
                } else if (exLibelle.StartsWith("REMISE CHEQUE"))
                {
                    exportws.Cells[i, 3] = exRemiseCheque;
                } else if (exLibelle.StartsWith("REMISE ESPECE"))
                {
                    exportws.Cells[i, 3] = exRemiseEsp;
                } else if (exLibelle.StartsWith("REMISE TR"))
                {
                    exportws.Cells[i, 3] = exRemiseTr;
                }

                exportws.Cells[i, 4] = exLibelle;
                exportws.Cells[i, 5] = exAmount;

                // Génération Ligne 2 avec le 530
                exportws.Cells[i + 1, 1] = strexDate;

                exportws.Cells[i + 1, 3] = 53000000;
                exportws.Cells[i + 1, 4] = exLibelle;
                exportws.Cells[i + 1, 6] = exAmount;

                i += 2;
            }

            //exportwb.SaveAs(this._defaultExportPath + "export-data.xlsx");
            DateTime timenow = DateTime.Now;
            
            exportwb.SaveAs(this._defaultExportPath + "\\Export-caisse-" + timenow.ToString("HH_mm_ss") + ".xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault);



            Marshal.ReleaseComObject(exportws);
            exportws = null;
            exportwb.Close(false, Type.Missing, Type.Missing);
            Marshal.ReleaseComObject(exportwb);
            exportwb = null;
            exportapp.Application.UserControl = true;
            exportapp.Quit();
            Marshal.ReleaseComObject(exportapp);
            exportapp = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();

            this.export.Enabled = true;
            this.export.Text = "Exporter vers Excel";

            DialogResult openYesNo = MessageBox.Show("Export réalisé !\r\n\r\nFichier disponible : \r\n" + this._defaultExportPath + "\\Export-caisse-" + timenow.ToString(
                "HH_mm_ss") + ".xlsx\r\n\r\n" +
                "Voulez-vous ouvrir le fichier maintenant ?", "Export to Excel", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            switch (openYesNo)
            {
                case DialogResult.Yes:
                    System.Diagnostics.Process.Start(this._defaultExportPath + "\\Export-caisse-" + timenow.ToString("HH_mm_ss") + ".xlsx");
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void LaunchParse_Click(object sender, EventArgs e)
        {
            listRemises.Items.Clear();
            if (bgw.IsBusy != true)
            {
                this.chooseImportFile.Enabled = false;
                this.launchParse.Enabled = false;
                this.export.Enabled = false;
                bgw.RunWorkerAsync();
            }
        }
    }
}
