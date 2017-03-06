using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FarForFolderAndFiles
{
    class CustomFolderInfo
    {
     public   CustomFolderInfo prev;
       public FileSystemInfo[] objs;

        public CustomFolderInfo(CustomFolderInfo prev, FileSystemInfo[] list)
        {
            this.prev = prev;
            this.objs = list;
        }

        public CustomFolderInfo GetNextItem(int k)
        {
            FileSystemInfo active = objs[k];
            List<FileSystemInfo> list = new List<FileSystemInfo>();
            DirectoryInfo d = active as DirectoryInfo;
           list.AddRange(d.GetDirectories());
            list.AddRange(d.GetFiles());
            CustomFolderInfo x = new CustomFolderInfo(this, list.ToArray());
            return x;
        }


    }
        class Program
        {
 
            static void Zapis(CustomFolderInfo item)
            {
                for (int i = 0; i < item.objs.Length; ++i)
                {
                if (item.objs[i].GetType() == typeof(FileInfo))
                {
                    if (item.objs[i].Extension == ".txt")
                    {

                        FileInfo d = (FileInfo)item.objs[i];
                        string mydocpath = @"C:\Users\Lenovo\Desktop\Новая папка";
                        string line;
                        string s = d.FullName;
                        StreamReader file = new StreamReader(s);
                        while ((line = file.ReadLine()) != null)
                        {

                            using (StreamWriter outputFile = File.AppendText(mydocpath + @"\lol.txt"))
                            {
                                if (!Global.dictionary.ContainsKey(s)) { 
                                    Global.dictionary.Add(s, line);

                                outputFile.WriteLine(line);}

                            }
                        }
                    }
                }

                else
                {
                    CustomFolderInfo newItem = item.GetNextItem(i);
                    Zapis(newItem);
                }
                }
            }
            
            static void Main(string[] args)
            {
               
         
                List<FileSystemInfo> list = new List<FileSystemInfo>();
            string k = Console.ReadLine();
                var d = new DirectoryInfo(@"C:\Users\Lenovo\Desktop\work");
                list.AddRange(d.GetDirectories());
                list.AddRange(d.GetFiles());
            Console.BackgroundColor = ConsoleColor.Blue;
                CustomFolderInfo test = new CustomFolderInfo(null, list.ToArray());
                Zapis(test);/// записать все файлы в один файл
            ///и искать среди значений. 
            ///создать dictionary у которго ключ - путь к файлу а значение  - вся строка.
            ///дальше я бы искала слово среди значений и потом вывела на экран бы его ключ. 
              
            }
        }
    }


