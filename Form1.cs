
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

/* ---------------------------- 
Created by: Dana Jamous 7/22/2025
Operations & Information Analysis Branch (OIAB)
eDiscovery Division (EDD)
Office of Records, Administrative Systems & eDiscovery (ORASE)
Office of Mission Support (OMS)
------------------------------*/

namespace Automatic_PDF_Combiner
{
    public partial class Form1 : Form
    {
        private string logFilePath = "log.txt";

        public Form1()
        {
            InitializeComponent();
            string howToUseTheToolMessage = "1. Select folder contain PDFs.\n" +
                "2. Choose Combine Option:\n" +
                "\ta. Single PDF: Combine PDs into one file. \n " +
                "\tb. Max of 100 MB: Combine into multiple files (parts), each not exceeding the 100 MB size limit.\n" +
                "\t    Parts are saved in a new folder within the selected location.\n" +
                "\t    This option is useful for sending PDFs via email.\n" +
                "3. Click Strat Combining.\n" +
                "4. View progress and final status in the app.";

            // Example of adding a tooltip in a Windows Forms application
            toolTip1.SetToolTip(toolTip, howToUseTheToolMessage);
            toolTip2.SetToolTip(toolTipLbl, howToUseTheToolMessage);
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
                    Log("Folder selected: " + dialog.SelectedPath);
                }
            }
        }

        // Handles the combine button click to combine PDF files based on selected option
        private void btnCombine_Click(object sender, EventArgs e)
        {
            const int MB = 1024 * 1024;
            string folderPath = txtFolderPath.Text;
            if (!Directory.Exists(folderPath))
            {
                UpdateStatus("Please select a valid folder.", Color.Red);
                Log("Invalid folder selected.");
                return;
            }

            string[] pdfFiles = Directory.GetFiles(folderPath, "*.pdf");
            if (pdfFiles.Length == 0)
            {
                UpdateStatus("No PDF files found.", Color.Red);
                Log("No PDF files found.");
                return;
            }

            InitializeProgressBar(pdfFiles.Length);

            string? option = combineOptionComboBox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(option))
            {
                UpdateStatus("Please select an option from the dropdown.", Color.Red);
                Log("Merge option not selected.");
                return;
            }

            try
            {
                if (option.Trim().Equals("Single PDF", StringComparison.OrdinalIgnoreCase))
                {
                    PdfDocument output = CombineIntoSinglePdf(pdfFiles);
                    string outputPath = ShowSaveFileDialog();
                    if (outputPath == null) return;
                    //save it on the disk 
                    output.Save(outputPath);
                    //release the RAM 
                    output.Close();
                    // Calculate the size of the saved PDF file
                    long fileSizeBytes = new FileInfo(outputPath).Length;
                    double fileSizeMB = (double)fileSizeBytes / MB;
                    UpdateStatus($"•  Combined PDF saved at: {outputPath} {Environment.NewLine}•  Size: {fileSizeMB:F2} MB", Color.Green);
                    Log($"Single PDF created: {outputPath}");
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
                Log("Error: " + ex.Message);
            }
        }

        // Logs messages to a log file with timestamp
        private void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(logFilePath, true))
                {
                    writer.WriteLine($"{DateTime.Now}: {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to write to log file: " + ex.Message);
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
                Log("User canceled combining.");
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
            UpdateStatus("Combining into a single PDF...", Color.Blue);
            enableControls(false);
            //All combined pahes are kept in memory untill output.save() is called.
            PdfDocument output = new PdfDocument();
            int count = 0;
            lblProgressCount.Visible = true;

            foreach (string file in pdfFiles)
            {
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
            return output;
        }

        // Combines multiple PDFs into parts, each part is Max of 100 MB. This methid ensure that one document is not splitted and is combine as a whole to the parts 
        private void CombineWithSizeLimit(string[] pdfFiles, int sizeLimitMB, string folderPath)
        {
            enableControls(false);
            const int MB = 1024 * 1024;
            long maxSizeBytes = sizeLimitMB * MB;
            long currentSizeEstimate = 0;
            int part = 1;
            int count = 0;
            //All combined pahes are kept in memory untill output.save() is called.
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

                    exceed100ErrorMsg = $"Combining stopped: This folder contains a file exceeding the {sizeLimitMB} MB Limit .{Environment.NewLine}{Environment.NewLine}" +
                                        $"File name: \"{Path.GetFileName(file)}\"{Environment.NewLine}" +
                                        $"File size: {fileSize / MB} MB.{Environment.NewLine}{Environment.NewLine}" +
                                        $"The 'Max of {sizeLimitMB} MB' option is is applicable only to files smaller than {sizeLimitMB} MB.{Environment.NewLine}" +
                                        $"To proceed, please remove the oversized file from the folder and try combining again";
                    MessageBox.Show(exceed100ErrorMsg, "File too large", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatus(exceed100ErrorMsg, Color.Red);
                    Log(exceed100ErrorMsg);
                    enableControls(true);
                    return;
                }
            }

            //Combine and save in parts
            foreach (string file in pdfFiles)
            {
                long fileSize = new FileInfo(file).Length;

                if (currentSizeEstimate + fileSize > maxSizeBytes && output.PageCount > 0)
                {
                    string partFile = Path.Combine(combinedFolder, $"Combined_Part{part}.pdf");
                    //save it on the disk 
                    output.Save(partFile);
                    //release the RAM 
                    output.Close();
                    UpdateStatus($"•  Part ({part}) Saved: {partFile} {Environment.NewLine}•  Size: {currentSizeEstimate / MB} MB {Environment.NewLine}{Environment.NewLine} Continue combining...", Color.Blue);
                    Log($"Saved part {part}: {partFile} (Estimated size: {currentSizeEstimate / MB} MB)");

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
                Log($"Saved final part {part}: {partFile}");
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

            // Perform actions based on the selected item
            switch (selectedOption)
            {
                case "Single PDF":
                    combineOptionInfoLbl.Text = "Combine all PDFs into one file PDF. You'll choose the name and save location after combing is completed.";

                    break;
                case "Max of 100 MB":
                    combineOptionInfoLbl.Text = $"Combine into multiple files (parts), each not exceeding the 100 MB size limit. \nParts are saved in a new folder within the selected folder.\nThis option is useful for sending combined PDFs via email.";
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
        }

        // Resets various UI controls to their default state
        private void resetControls()
        {
            combineOptionInfoLbl.Text = "";
            fileNameLbl.Text = "";
            lblProgressCount.Text = "";
            statusTextBox.Text = "";
            progressBar.Value = 0;
        }

        // Not fully implemented yet.This method is for the cancel button
        private void deletePartialFiles(string folderPath, List<int> partsList)
        {
            try
            {
                foreach (var part in partsList)
                {
                    foreach (var file in Directory.GetFiles(folderPath, $"Combined_Part{part}.pdf"))
                    {

                        //    file.delte    Delete(file);

                    }
                    Log("deleted partial files after stopping combing");

                }
            }
            catch (Exception ex)
            {
                Log("failed to delete partial files after stopping combing " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
