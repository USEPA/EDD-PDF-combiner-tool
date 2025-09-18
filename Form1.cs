
using PdfSharp.Pdf;
using PdfSharp.Pdf.Filters;
using PdfSharp.Pdf.IO;
using System.Drawing.Text;

/* ---------------------------- 
Created by: Dana Jamous
Office of Mission Support
------------------------------*/

namespace Automatic_PDF_Combiner
{
    public partial class Form1 : Form
    {
        // Global vars
        bool cancelRequested = false;
   
        public Form1()
        {
            InitializeComponent();

            string howToUseTheToolMessage = "This tool combines multiple PDFs into one PDF file or several size-limited PDF files.\n\n" +
                "1. Select the folder containing the PDFs you want to combine.\n" +
                "\t    - PDFs in subfolders will not be included.\n" +
                "2. Select Combine Option.\n" +
                "\ta. Single PDF: Combines multiple PDFs into one PDF.  \n " +
                "\tb. Max of 100 MB per combined PDF (useful for emailing PDFs):  \n" +
                "\t    - Combines multiple PDFs into one PDF up to a 100 MB size limit.\n" +
                "\t    - If exceeded, additional combined PDFs are created (PDF names will be suffixed with _Part#).\n" +
                "\t    - No files are split across the combined PDF parts.\n" +
                "3. Click Start Combining.\n" +
                "4. View progress and final status in the app.\n"+
                "5.Combined PDFs are saved in a new folder in the selected location.";


            helpIconToolTip.SetToolTip(toolTip, howToUseTheToolMessage);
            helpTextToolTip.SetToolTip(toolTipLbl, howToUseTheToolMessage);
        }

        // Handles the browse button click to select a folder containing PDF files
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select a folder containing PDF files";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtFolderPath.Text = dialog.SelectedPath;
                    UpdateStatus("Folder selected", Color.Green);
                }
            }
        }

        // Handles the combine button click to combine PDF files based on selected option
        private void btnCombine_Click(object sender, EventArgs e)
        {
            cancelRequested = false;
            string folderPath = txtFolderPath.Text;
            if (!Directory.Exists(folderPath))
            {
                UpdateStatus("Please select a valid folder.", Color.Red);
                return;
            }

            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
            if (pdfFiles.Length == 0)
            {
                UpdateStatus("No PDF files found.", Color.Red);
                return;
            }

            InitializeProgressBar(pdfFiles.Length);

            string? option = combineOptionComboBox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(option))
            {
                UpdateStatus("Please select an option from the dropdown.", Color.Red);
                return;
            }

            try
            {
                if (option.Trim().Equals("Single PDF", StringComparison.OrdinalIgnoreCase))
                {
                    PdfDocument output = CombineIntoSinglePdf(pdfFiles);
                }
                else
                {
                    int sizeLimitMB = 100;
                    CombineWithSizeLimit(pdfFiles, sizeLimitMB, folderPath);
                }
            }
            catch (Exception ex)
            {
                UpdateStatus("Error: " + ex.Message, Color.Red);
            }
        }

        // Updates the status message displayed to the user
        private void UpdateStatus(string message, Color color)
        {
            statusTextBox.Text = message;
            statusTextBox.ForeColor = color;
            statusTextBox.Visible = true;

        }

        // Shows a SaveFileDialog to select where to save the combined PDF (For single PDF combine option)
        private string ShowSaveFileDialog()
        {
            SaveFileDialog saveDialog = new SaveFileDialog
            {
                Title = "Save Combined PDF As",
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"Single Combined PDF {DateTime.Now:dd-MM-yyyy}  {DateTime.Now:HH.mm.ss}.pdf"
            };

            if (saveDialog.ShowDialog() != DialogResult.OK)
            {
                UpdateStatus("Combining canceled.", Color.Red);
                return null;
            }

            return saveDialog.FileName;
        }

        // Initializes the progress bar with a maximum value
        private void InitializeProgressBar(int max)
        {
            progressBar.Minimum = 0;
            progressBar.Maximum = max;
            progressBar.Value = 0;
        }

        // Combines multiple PDFs into a single PDF document
        private PdfDocument CombineIntoSinglePdf(string[] pdfFiles)
        {
            // if only one PDF file exisits in this folder exist and show error message 
            if (pdfFiles.Length == 1)
            {
                resetControls();
                UpdateStatus("Only one PDF file exists in this folder, please select a folder that contains at least 2 PDF files.", Color.Red);
                return null;
            }
            UpdateStatus("Combining into a single PDF...", Color.Blue);
            enableControls(false);

            
            PdfDocument output = new PdfDocument();

            int count = 0;
            lblProgressCount.Visible = true;
            const int MB = 1024 * 1024;

            foreach (string file in pdfFiles)
            {
                if (cancelRequested)
                {
                    output.Close();
                    output = null;
                    GC.Collect();

                    resetControls();
                    enableControls(true);
                    UpdateStatus("Combing canceled by user", Color.Orange);
                    return null;
                }
                PdfDocument input = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                foreach (PdfPage page in input.Pages)
                {
                    output.AddPage(page);
                }
                input.Close();

                count++;
                progressBar.Value = count;
                lblProgressCount.Text = $"File {count} of {pdfFiles.Length}";
                fileNameLbl.Visible = true;
                fileNameLbl.Text = "PDF Name: " + Path.GetFileName(file);
                Application.DoEvents();
            }
            enableControls(true);
            string outputPath = ShowSaveFileDialog();
      
            if (outputPath == null) return null;
            output.Save(outputPath);
            output.Close();
       
            long fileSizeBytes = new FileInfo(outputPath).Length;
            double fileSizeMB = (double)fileSizeBytes / MB;
            UpdateStatus($"•  Combined PDF saved at: {outputPath} {Environment.NewLine}•  Size: {fileSizeMB:F2} MB", Color.Green);
            return output;
        }

        // Combines multiple PDFs into parts, each part is Max of 100 MB. This methid ensure that one document is not splitted and is combine as a whole to the parts 
        private void CombineWithSizeLimit(string[] pdfFiles, int sizeLimitMB, string folderPath)
        {
            const int MB = 1024 * 1024;
            long maxSizeBytes = sizeLimitMB * MB;
            long currentSizeEstimate = 0;
            int part = 1;
            int count = 0;

            // if only one PDF file exisits in this folder exist and show error message 
            if (pdfFiles.Length == 1)
            {
                resetControls();
                UpdateStatus("Only one PDF file exists in this folder, please select a folder that contains at least 2 PDF files.", Color.Red);
                return;
            }

            enableControls(false);
            PdfDocument output = new PdfDocument();
            String exceed100ErrorMsg = "";
            fileNameLbl.Visible = true;

            // Create a new folder to save combined parts
            string combinedFolder = Path.Combine(folderPath, $"Combined_Parts_{DateTime.Now:dd-MM-yyyy}  {DateTime.Now:HH.mm.ss}");
            Directory.CreateDirectory(combinedFolder);

            UpdateStatus($"Combining with size limit of {sizeLimitMB} MB...", Color.Blue);

            // Check if any file exceeds the limit if yes stop combining ad show error message
            foreach (string file in pdfFiles)
            {
                long fileSize = new FileInfo(file).Length;
                if (fileSize > maxSizeBytes)
                {
                    if (output.PageCount > 0)
                        output.Close();

                    exceed100ErrorMsg = $"Combining stopped: This folder contains a file exceeding the {sizeLimitMB} MB Limit.{Environment.NewLine}{Environment.NewLine}" +
                                        $"File name: \"{Path.GetFileName(file)}\"{Environment.NewLine}" +
                                        $"File size: {fileSize / MB} MB.{Environment.NewLine}{Environment.NewLine}" +
                                        $"The 'Max of {sizeLimitMB} MB' option is is applicable only to files smaller than {sizeLimitMB} MB.{Environment.NewLine}" +
                                        $"To proceed, please remove the oversized file from the folder and try combining again";
                    MessageBox.Show(exceed100ErrorMsg, "File too large", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatus(exceed100ErrorMsg, Color.Red);
                    enableControls(true);
                    return;
                }
            }

            //Combine and save in parts
            foreach (string file in pdfFiles)
            {
                if (cancelRequested) {
                    //clean the RAM
                    output.Close();
                    output.Close();
                    output = null;
                    GC.Collect();
                    if (Directory.Exists(combinedFolder)) {
                        //delete the entire folder with parts files 
                        Directory.Delete(combinedFolder,true);
                    }
                    resetControls();
                    enableControls(true);
                    UpdateStatus("Combing canceled by user", Color.Orange);
                    return;

                }

                long fileSize = new FileInfo(file).Length;

                if (currentSizeEstimate + fileSize > maxSizeBytes && output.PageCount > 0)
                {
                    string partFile = Path.Combine(combinedFolder, $"Combined_Part{part}.pdf");
                    output.Save(partFile);
                    output.Close();
                    UpdateStatus($"•  Part ({part}) Saved: {partFile} {Environment.NewLine}•  Size: {currentSizeEstimate / MB} MB {Environment.NewLine}{Environment.NewLine} Continue combining...", Color.Blue);
                  
                    part++;
                    output = new PdfDocument();
                    currentSizeEstimate = 0;
                }

                PdfDocument input = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                foreach (PdfPage page in input.Pages)
                {
                    output.AddPage(page);
                }
                input.Close();

                currentSizeEstimate += fileSize;

                count++;
                progressBar.Value = count;
                lblProgressCount.Visible = true;
                lblProgressCount.Text = $"File {count} of {pdfFiles.Length}";
                fileNameLbl.Text = "PDF Name: " + Path.GetFileName(file);
                Application.DoEvents();
            }
            //save the last part 
            if (output.PageCount > 0)
            {
                string partFile = Path.Combine(combinedFolder, $"Combined_Part{part}.pdf");
                output.Save(partFile);
                output.Close();
            }

            enableControls(true);
            UpdateStatus($"{part} file(s) created.{Environment.NewLine}All combined parts saved in:{Environment.NewLine}{combinedFolder}", Color.Green);
        }

        // // Handles the event when the user selects a different option from the combineOptionComboBox and show hint accordingly 
        private void combineOptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            combineOptionInfoLbl.Visible = true;
            resetControls();
            string selectedOption = combineOptionComboBox.SelectedItem.ToString();

            switch (selectedOption)
            {
                case "Single PDF":
                    combineOptionInfoLbl.Text = "Combine all PDFs into one PDF file. You'll choose the name and save location after combing is completed.";

                    break;
                case "Max of 100 MB":
                    combineOptionInfoLbl.Text = $"- Combines multiple PDFs into one PDF up to a 100 MB size limit. \n- If exceeded, additional combined PDFs are created (PDF names will be suffixed with _Part#).\n- No files are split across the combined PDF parts.\n- This option is useful for emailing PDFs. \n ";
                    break;
                default:

                    break;
            }
        }

        // Enables or disables UI controls based on the provided boolean parameter
        private void enableControls(bool enable)
        {
            btnBrowse.Enabled = enable;
            btnCombine.Enabled = enable;
            txtFolderPath.Enabled = enable;
            combineOptionComboBox.Enabled = enable;
            cancelBtn.Enabled = !enable;
            btnCombine.BackColor = enable ? Color.Honeydew : Color.LightGray;
        }

        // Resets various UI controls to their default state
        private void resetControls()
        {
            //combineOptionInfoLbl.Text = "";
            fileNameLbl.Text = "";
            lblProgressCount.Text = "";
            statusTextBox.Text = "";
            progressBar.Value = 0;
        }

        // Cancel combing PDFS.
        private void cancelBtn_Click(object sender, EventArgs e)
        {
            cancelRequested = true;
            UpdateStatus("Canceling process...", Color.Orange);
        }
    }
}
