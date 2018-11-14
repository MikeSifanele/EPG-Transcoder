using System;
using System.Data;
using ExcelDataReader;
using System.IO;
using System.Windows.Forms;
using BLL;

namespace EPG_Transcoder
{
    public partial class frmMain : Form
    {
        string myFileName = "";
        DataSet myExcelData;
        Extension myExtension = new Extension();
        IExcelDataReader myExcelReader = null;
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                myProgressBar.Value = 0;
                pbOverrall.Value = 0;

                using (OpenFileDialog ImportDialog = new OpenFileDialog() { Filter = "Excel Workbook|*.xls*", ValidateNames = true })
                {
                    DialogResult myResult = ImportDialog.ShowDialog();

                    if (myResult == DialogResult.OK)
                    {
                        myFileName = ImportDialog.FileName;

                        try
                        {
                            FileStream myFileStream = File.Open(myFileName, FileMode.Open, FileAccess.Read);
                            myProgressBar.Increment(5);

                            try
                            {
                                myExcelReader = ExcelReaderFactory.CreateOpenXmlReader(myFileStream);
                            }
                            catch
                            {
                                myExcelReader = ExcelReaderFactory.CreateBinaryReader(myFileStream);
                            }

                            
                            myProgressBar.Increment(5);
                            myExcelData = myExcelReader.AsDataSet();
                            myProgressBar.Increment(5);
                            cmbActiveSheet.Items.Clear();
                            myProgressBar.Increment(5);
                            foreach (System.Data.DataTable myDataTable in myExcelData.Tables)
                            {
                                cmbActiveSheet.Items.Add(myDataTable.TableName);
                            }
                            myProgressBar.Increment(5);
                            myExcelReader.Close();
                            myProgressBar.Increment(5);
                            cmbActiveSheet.Items.Insert(0, "<Please select a sheet>");
                            myProgressBar.Increment(5);
                            myProgressBar.Value = 1000;

                            lblStatus.Text = "File uploaded... select a sheet.";
                        }
                        catch (IOException)
                        {
                            MessageBox.Show($"File: {ImportDialog.SafeFileName}, is still open.\nPlease Close it and try again.", "Error");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect file format\n..Please select a file to import.", "Import file");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please select a file to import.", "Import file");
                System.Windows.Forms.Application.Restart();
            }

            pbOverrall.Increment(33);
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtFileName.Text != "")
                {
                    myProgressBar.Value = 0;
                    pbOverrall.Value = 0;

                    lblStatus.Text = "File exporting... please wait.";

                    string dirLocation = "";

                    sdSaveTo.FileName = txtFileName.Text.TrimEnd(".xml".ToCharArray()) + ".xml";

                    if (sdSaveTo.ShowDialog() == DialogResult.OK)
                    {
                        dirLocation = sdSaveTo.FileName;
                    }

                    myProgressBar.Increment(100);

                    if (myExtension.Export_to_XML(myExcelData, cmbActiveSheet.SelectedItem.ToString(), txtChannelName.Text, dirLocation))
                    {
                        lblStatus.Text = "File export successful... done.";
                    }
                    myProgressBar.Value = 1000;
                    pbOverrall.Increment(100);
                }
                else
                {
                    MessageBox.Show("Please enter file name without extension.","Error");
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}\nPlease Import an excel file.", "Error");
                System.Windows.Forms.Application.Restart();
            }            
        }

        private void cmbActiveSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblStatus.Text = "Sheet selected... specify File name.";
            pbOverrall.Increment(33);
        }       

        private void myTimer_Tick(object sender, EventArgs e)
        {
            myProgressBar.Increment(5);
        }
    }
}
