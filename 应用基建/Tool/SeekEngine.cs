using dotNetLab;
using dotNetLab.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace DotNetOptimization
{
    class SeekEngine
    {
        Traverse it_All;
        StringBuilder sb;
        string strDir, strExt;
        List<string> lstTopLevelDir;
       string saveDir = @"C:\Users\Public\Music";
        string strTargetFolder;
        public SeekEngine()
        {
            
            it_All = new Traverse();
            sb = new StringBuilder();
             
            lstTopLevelDir = new List<string>();
            
            
        }
         void FetchTopLevelDirs(string strPath)
          {
            int n = 0;
            for (int i = 0; i < strPath.Length; i++)
            {
                if(strPath[i]=='\\')
                {
                    n++;
                    if(n==2)
                    {
                        n = i;
                        break;
                    }
                }
            }
            string strTemp = strPath.Substring(0, n - 1);
            if(!lstTopLevelDir.Contains(strTemp))
            this.lstTopLevelDir.Add(strTemp);
             
        }
        public void Find_Sln(string strDir)
        {
            


            try
            {

            it_All.SeekFiles(strDir, "sln");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {

              strTargetFolder = Path.GetDirectoryName(it_All.lst_FileNames[0]);
            }
            catch(Exception ex)
            {
                return;
            }
            
            Zipit();
        }
       void DeleteSpecialExtFile(string strFolder)
        {
            string SolutionName = null;
            try
            {
              SolutionName = Directory.GetFiles(strFolder, "*.sln")[0];

            }
            catch (Exception ex)
            {
                return;
            }
            string str = Path.Combine(strFolder,Path.GetFileNameWithoutExtension(SolutionName), "bin\\Debug");
            DirectoryInfo dif = new DirectoryInfo(str);
            dif.Delete(true);
        }
        void Zipit()
        {
             
                string strZipFile = Path.GetFileName(strTargetFolder) + ".zip";
                strZipFile = Path.Combine(this.saveDir, strZipFile);
                if(File.Exists(strZipFile))
                {
                  File.Delete(strZipFile);
                }
               // DeleteSpecialExtFile(strTargetFolder);
                Zip.ZipFolder(strTargetFolder, strZipFile);
             
        }
    }
}

/*
 
 Traverse it_All;
        StringBuilder sb;
        public Form1()
        {
            InitializeComponent();
            it_All = new Traverse();
            sb = new StringBuilder();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            it_All.SeekFiles(this.textBox2.Text,this.textBox3.Text);
            this.sb.Clear();
            for (int i = 0; i < it_All.lst_FileNames.Count; i++)
            {
                sb.AppendLine(this.it_All.lst_FileNames[i]);
            }
            this.textBox1.Text = sb.ToString();
        }
 */