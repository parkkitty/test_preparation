using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileVersionManager
{
    class MergedFolder
    {
        FileVerManager FolderA;
        FileVerManager FolderB;
        string FolderAPath;
        string FolderBPath;


        public MergedFolder(string pathA, string pathB)
        {
            FolderA = new FileVerManager(pathA);
            FolderAPath = pathA;
            FolderB = new FileVerManager(pathB);
            FolderBPath = pathB;
        }

        public void StartMerged(string FolderPath)
        {

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            } 

            List<string> AList = FolderA.GetVersionFile();
            List<string> AListFileName = new List<string>();
            foreach(string fileName in AList)
            {
                AListFileName.Add(fileName.Split(':')[0]);
            }
            List<string> BList = FolderB.GetVersionFile();
            List<string> BListFileName = new List<string>();
            foreach (string fileName in BList)
            {
                BListFileName.Add(fileName.Split(':')[0]);
            }
            List<string> Standard = new List<string>();
            List<string> ComparedFileList = new List<string>();
            List<string> ComparedFileName = new List<string>();

            List<string> MergedList = new List<string>();

            string StandardPath = "";
            string ComparedPath = "";

            if (AList.Count >= BList.Count)
            {
                Standard = AList;
                StandardPath = FolderAPath;
                ComparedFileName = BListFileName;
                ComparedFileList = BList;
                ComparedPath = FolderBPath;
            }
            else
            {
                Standard = BList;
                StandardPath = FolderBPath;
                ComparedFileName = AListFileName;
                ComparedFileList = AList;
                ComparedPath = FolderAPath;
            }

            foreach(string s in Standard)
            {
                if (ComparedFileName.Contains(s.Split(':')[0]))
                {
                    string ComparedString = "";

                    foreach(string c in ComparedFileList)
                    {
                        if (c.Split(':')[0].Equals(s.Split(':')[0]))
                        {
                            ComparedString = c;
                            break;
                        }
                    }

                    if (int.Parse(ComparedString.Split(':')[1]) >= int.Parse(s.Split(':')[1]))
                    {
                        MergedList.Add(ComparedString);
                        File.Copy(ComparedPath + @"\" + s.Split(':')[0], FolderPath + @"\" + s.Split(':')[0]);
                    }
                    else
                    {
                        MergedList.Add(s);
                        File.Copy(StandardPath + @"\" + s.Split(':')[0], FolderPath + @"\" + s.Split(':')[0]);
                    }


                }
                else
                {
                    MergedList.Add(s);
                    File.Copy(StandardPath + @"\" + s.Split(':')[0], FolderPath + @"\" + s.Split(':')[0]);
                }
                
            }


            if (File.Exists(FolderPath + @"\version.txt"))
            {
                File.Delete(FolderPath + @"\version.txt");
            }

            File.AppendAllLines(FolderPath + @"\version.txt", MergedList);

        }
    }
}
