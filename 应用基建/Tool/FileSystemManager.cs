using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace dotNetLab.Common
{
   public class FileSystemManager
    {
        public static void RenameFile(String strSrc,string strDst)
        {
            File.Move(strSrc, strDst);
        }

        public static void BatchRenameFileByRemovePrefix(string FolderPath, string PrefixFileName, string extName = "*.*") 
        {
            String [] FileNames = Directory.GetFiles(FolderPath, extName);

            foreach (var item in FileNames)
            {
                RenameFile(item, item.Replace(PrefixFileName,""));
            }
        }

        public static void BatchRenameFileByUserDefineRule(string FolderPath,Func<String,String> UserDefineRule, string extName = "*.*")
        {
            String[] FileNames = Directory.GetFiles(FolderPath, extName);

            foreach (String item in FileNames)
            {
                RenameFile(item, UserDefineRule(item));
            }
        }

        public static void BatchRenameFileExtension(string FolderPath, string NewextName, string OldextName = "*.*")
        {
            String[] FileNames = Directory.GetFiles(FolderPath, OldextName);

            foreach (var item in FileNames)
            {
               
                RenameFile(item, Path.GetFileNameWithoutExtension(item) + NewextName  );
            }
        }
    }
}
