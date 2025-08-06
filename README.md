**Auto PDF Combiner**
This tool combines multiple PDF files into a single or multiple PDFs effortlessly, saving time and simplifying document management.

**Features**
- Single PDF Combining: Combine  multiple PDF files into a single PDF.
- Size-Limited Parts: Combine PDFs into multiple PDF files with a 100MB size limit, ideal for sending PDF via email or managing storage constraints.
- Cancellation Option: Cancel the combining process at any time.
- Real-Time Status Updates: real-time progress updates and detailed logs of all combining activities.

**Usage Workflow**
1.	Select folder contain PDFs
2.	Choose Combine Option:
    - Single PDF: Combine PDs into one file.
    - Max of 100 MB: Combine into multiple files (parts), each not exceeding the 100 MB size limit. Parts are saved in a new folder within the selected location. This option is useful for sending PDFs via email.
4.	Click Strat Combining.
5.	Cancel the combining at any time.
6.	View progress and final status in the app. 

**Build and Deployment**
-	Built using C# (.Net WinForms)
-	.Net Framework (8.0): Long-term support (LTS) release of the .NET platform
-	Supported OS version: The minimum OS version that the project will run on is 7.0 (Can be changed).
-	Published as self-contained Single EXE. All dependencies are contained in the EXE. dependencies used:
    -	PDFSharp 
-	No updates are required. 
-	Target Runtime: win-x64 (Can be changed). 
-	Changes to the code requires publishing new EXE. 
-	Final EXE size: 149 MB.
-	Enabling "Ready to Run Compilation" increases the file size to 167 MB.
    -	The "Ready to Run" option precompiles the application's assemblies into native machine code at publish time, rather than relying on the Just-In-Time compiler to compile them at runtime. As a result, it reduces the tool's startup time by 1-2 seconds, albeit at the cost of a larger file size.
    -	The 1-2 second reduction in startup time will be most noticeable during the first launch.

 
**CPU**
-	Light usage, single-threaded.

**RAM usage**
-	PDFDocument object holds the Combined PDF in Ram during the combining process.
-	Once saved to the disk the in-memory object is cleared and released.  

**Security**
-	Entire process is local, no network transfers.
-	No leftover temporary files. 
