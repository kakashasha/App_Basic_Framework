﻿using System;
using System.Collections.Generic;
using System.IO;

using System.Text;
namespace dotNetLab
{
namespace Tools
{
   public class Traverse
    {
      public  List<String> lst_FileNames  ;
        public Traverse()
        {
            lst_FileNames = new List<string>();
        }
         void FindFiles(string strDirPath,String strSeekPattern,bool bRecordSubDir)
        {


                //在指定目录及子目录下查找文件,在list中列出子目录及文件
                try
                {
                    DirectoryInfo Dir = new DirectoryInfo(strDirPath);
                    DirectoryInfo[] DirSub = null;
                    try
                    {
                        DirSub = Dir.GetDirectories();
                    }
                    catch (Exception e)
                    {
                       
                        return;
                    }

                    if (DirSub.Length <= 0)
                    {
                        FileInfo[] fileInfoArr = Dir.GetFiles(strSeekPattern, SearchOption.TopDirectoryOnly);
                        foreach (FileInfo f in fileInfoArr) //查找文件
                        {
                            //listBox1.Items.Add(Dir+f.ToString()); //listBox1中填加文件名

                            lst_FileNames.Add(Dir + @"\" + f.ToString());
                        }
                    }
                    int t = 1;
                    foreach (DirectoryInfo d in DirSub)//查找子目录 
                    {
                        FindFiles(Dir + @"\" + d.ToString(), strSeekPattern, bRecordSubDir);
                        if (bRecordSubDir)
                            lst_FileNames.Add(Dir + @"\" + d.ToString());
                        if (t == 1)
                        {
                            FileInfo[] fileInfoArr = Dir.GetFiles(strSeekPattern, SearchOption.TopDirectoryOnly);
                            foreach (FileInfo f in fileInfoArr) //查找文件
                            {
                                lst_FileNames.Add(Dir + @"\" + f.ToString());
                            }
                            t = t + 1;
                        }
                    }

                }
                catch (Exception ex)
                {

                    
                }
           

        }
            
            public  void SeekFilesAndSubDirs(string strDirPath)
        {

            FindFiles(strDirPath, "*.*", true); 
        }
        public void SeekFiles(string strDirPath,string strSeekOption_Ext)
        {
            FindFiles(strDirPath, "*."+strSeekOption_Ext, false);
        }


            public static void CopyDirectory(string srcPath, string destPath)
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(srcPath);
                    FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //获取目录下（不包含子目录）的文件和子目录
                    foreach (FileSystemInfo i in fileinfo)
                    {
                        if (i is DirectoryInfo)     //判断是否文件夹
                        {
                            if (!Directory.Exists(destPath + "\\" + i.Name))
                            {
                                Directory.CreateDirectory(destPath + "\\" + i.Name);   //目标目录下不存在此文件夹即创建子文件夹
                            }
                            CopyDirectory(i.FullName, destPath + "\\" + i.Name);    //递归调用复制子文件夹
                        }
                        else
                        {
                            File.Copy(i.FullName, destPath + "\\" + i.Name, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
                        }
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
}

}
