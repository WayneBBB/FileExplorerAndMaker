using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;
using Image = System.Drawing.Image;
using System.Resources;
using System.Configuration;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Data.SqlTypes;

namespace SimpleFileManager
{
    public partial class Form1 : Form
    {

        //The application depends on the name of the file to be in a specific format!  This is the format.
        //The section of name are separated by a '_'
        //Position 0 is: the Year
        //Position 1 is: the month in the format of "08"
        //Position 2 is: the day of the month as a number "15"
        //Position 3 is: The day of the week as the full name: "Wednesday"
        //Position 4 is: The place that will be visiting or activity being done: "Paris" or "Boarding Crystal Serenity"
        //Position 5 is: The country that will be visiting: "France", can be left blank
        
        //FilePathForPics varables used in setting a folders constant in resources;
        private string filePath { get; set; }
        private string fileDate { get; set; }

        //Path info on Folder
        private string FolderFilePath { get; set; }
        private string DocFilePath { get; set; }
        private string DocNameWExten { get; set; }
        private string DocNameWOExten { get; set; }

        private string HeaderName { get; set; }

        //List of folders and files in current directory being used
        private DirectoryInfo fileList { get; set; }
        private FileInfo[] files { get; set; }

        private bool isFile = false;
        private string currentlySelectedItemName = "";
        public Form1()
        {
            //Get the file path to use to start this session
            filePath = Properties.Settings.Default.LastDirectoryPath;
            fileDate = Properties.Settings.Default.LastDirectoryDate;

            InitializeComponent();

            DialogResult dialogresult = MessageBox.Show("Do you want to create a new Travel Document"+Environment.NewLine+"No answer allows adding pictures to document.", "Simple File", MessageBoxButtons.YesNo);
            if (dialogresult == DialogResult.Yes) {
                this.CreateSIFD.Visible = true;
                this.LoadPictures.Visible = false;
            }
            if (dialogresult == DialogResult.No) {
                this.CreateSIFD.Visible = false;
                this.LoadPictures.Visible = true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            filePathTextBox.Text = filePath;
            loadFilesAndDirectories();
        }

        public void loadFilesAndDirectories()
        {
            //DirectoryInfo fileList;
            string tempFilePath = "";
            FileAttributes fileAttr;
            try
            {

                if (isFile)
                {
                    tempFilePath = filePath + "/" + currentlySelectedItemName;
                    FileInfo fileDetails = new FileInfo(tempFilePath);
                    fileNameLabel.Text = fileDetails.Name;
                    fileTypeLabel.Text = fileDetails.Extension;
                    fileAttr = File.GetAttributes(tempFilePath);
                    Process.Start(tempFilePath);
                }
                else
                {
                    fileAttr = File.GetAttributes(filePath);

                }

                if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    fileList = new DirectoryInfo(filePath);
                    files = fileList.GetFiles(); // GET ALL THE FILES
                    DirectoryInfo[] dirs = fileList.GetDirectories(); // GET ALL THE DIRS
                    string fileExtension = "";
                    listView1.Items.Clear();

                    for (int i = 0; i < files.Length; i++)
                    {
                        fileExtension = files[i].Extension.ToUpper();
                        switch (fileExtension)
                        {
                            case ".MP3":
                            case ".MP2":
                                listView1.Items.Add(files[i].Name, 5);
                                break;
                            case ".EXE":
                            case ".COM":
                                listView1.Items.Add(files[i].Name, 7);
                                break;

                            case ".MP4":
                            case ".AVI":
                            case ".MKV":
                                listView1.Items.Add(files[i].Name, 6);
                                break;
                            case ".PDF":
                                listView1.Items.Add(files[i].Name, 4);
                                break;
                            case ".DOC":
                            case ".DOCX":
                                listView1.Items.Add(files[i].Name, 3);
                                break;
                            case ".PNG":
                            case ".JPG":
                            case ".JPEG":
                                listView1.Items.Add(files[i].Name, 9);
                                break;

                            default:
                                listView1.Items.Add(files[i].Name, 8);
                                break;
                        }

                    }

                    for (int i = 0; i < dirs.Length; i++)
                    {
                        listView1.Items.Add(dirs[i].Name, 10);
                    }
                }
                else
                {
                    fileNameLabel.Text = this.currentlySelectedItemName;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("A Error Occured While Navigating "+Environment.NewLine+"Error Message: "+e.Message+Environment.NewLine+"InnerException: "+e.InnerException);
                
            }
        }

        public void loadButtonAction()
        {
            removeBackSlash();
            filePath = filePathTextBox.Text;
            loadFilesAndDirectories();
            isFile = false;
        }

        public void removeBackSlash()
        {
            string path = filePathTextBox.Text;
            if (path.LastIndexOf("/") == path.Length - 1)
            {
                filePathTextBox.Text = path.Substring(0, path.Length - 1);
            }
        }

        public void goBack()
        {
            try
            {
                removeBackSlash();
                string path = filePathTextBox.Text;
                path = path.Substring(0, path.LastIndexOf("/"));
                this.isFile = false;
                filePathTextBox.Text = path;
                removeBackSlash();
            }
            catch (Exception e)
            {

            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            loadButtonAction();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            currentlySelectedItemName = e.Item.Text;

            FileAttributes fileAttr = File.GetAttributes(filePath + "/" + currentlySelectedItemName);
            if ((fileAttr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                isFile = false;
                filePathTextBox.Text = filePath + "/" + currentlySelectedItemName;
            }
            else
            {
                isFile = true;
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            loadButtonAction();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            goBack();
            loadButtonAction();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            FindFileFolder();

            string newmsgone = DocNameWOExten;

            if (File.Exists(DocFilePath))
            {
                DialogResult dialogResult = MessageBox.Show("There is already a document by that name." + Environment.NewLine +"If you continue you will overwrite any notes or comments that has been entered"+Environment.NewLine+ "Do you want to continue.","Document Exists", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    MakeFirstDocument.CreateWordDoc(DocFilePath, newmsgone,HeaderName);
                    MessageBox.Show("Document Has Been Created");
                }
                else if (dialogResult == DialogResult.No)
                { //Do nothing. }
                }
            }
            else
            {
                MakeFirstDocument.CreateWordDoc(DocFilePath, newmsgone, HeaderName);
                MessageBox.Show("Document Has Been Created");
            }

        }


        private void LoadPictures_Click(object sender, EventArgs e)
        {
            FindFileFolder();

            //InsertPictures(DocFilePath, files);

            AddPictures.InsertPictures(DocFilePath, files,FolderFilePath);

            MessageBox.Show("Pictures Have Been Added To Document");

        }

        private void FindFileFolder()
        {
            FolderFilePath = filePathTextBox.Text;
            string filepathAT = "@" + FolderFilePath;
            string[] folders = FolderFilePath.Split('/');
            int folderlen = folders.Length;
            string lastfolder = folders[folderlen - 1];
            DocNameWExten = lastfolder + ".docx";
            DocNameWOExten = lastfolder;

            string[] headheaderArray = DocNameWOExten.Split('_');

            string month = headheaderArray[1];
            string headerMonth=string.Empty;
            
            switch (month)
            {
                case "01": { headerMonth = "Jan"; break; }
                case "02": { headerMonth = "Feb"; break; }
                case "03": { headerMonth = "Mar"; break; }
                case "04": { headerMonth = "Apr"; break; }
                case "05": { headerMonth = "May"; break; }
                case "06": { headerMonth = "Jun"; break; }
                case "07": { headerMonth = "Jul"; break; }
                case "08": { headerMonth = "Aug"; break; }
                case "09": { headerMonth = "Sep"; break; }
                case "10": { headerMonth = "Oct"; break; }
                case "11": { headerMonth = "Nov"; break; }
                case "12": { headerMonth = "Dec"; break; }
                default:
                    break;
            }

            if (headheaderArray.Length <6)
            {
                HeaderName = headheaderArray[3] + ", " + headheaderArray[2] + " " + headerMonth + ": " + headheaderArray[4] ;
            }
            else { 
                HeaderName = headheaderArray[3] + ", " + headheaderArray[2] + " " + headerMonth + ": " + headheaderArray[4] + " " + headheaderArray[5]; 
            }

           
            DocFilePath = Path.Combine(FolderFilePath, DocNameWExten);


            string newfilepath = string.Empty;
            for (int i = 0; i < folderlen - 1; i++)
            {
                //the last time through, no "/"
                if (i == folderlen - 2)
                {
                    newfilepath += folders[i];
                }
                else
                {
                    newfilepath += folders[i] + "/";

                }
            }

            Properties.Settings.Default.LastDirectoryPath = newfilepath;
            Properties.Settings.Default.LastDirectoryDate = DateTime.Now.ToLongDateString();
            Properties.Settings.Default.Save();
            
        }
    }
}

