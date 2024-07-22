using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp10
{
   
    class Program
    {
        static void LookFille(DirectoryInfo directory, TimeSpan interval)
        {
            FileInfo[] filles = directory.GetFiles();
            foreach (FileInfo file in filles)
            {
                ChekFile(file, interval);
            }
            DirectoryInfo[] dirs = directory.GetDirectories();

            foreach (DirectoryInfo dir in dirs)
            {

            } 
        }

        static void ChekFile(FileInfo file, TimeSpan interval)
        {
            Console.WriteLine($"Файл {file.Name} не используется более 30 минут");
            if (DateTime.Now - file.LastAccessTime > interval)
            { 
                try
                {
                    file.Delete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static void ChekDir(DirectoryInfo directory, TimeSpan interval)
        {
            Console.WriteLine($"Папка {directory.Name} не используется более 30 минут");
            if((DateTime.Now - directory.LastAccessTime < interval) & !UsingFile(directory, interval))
            {
                Console.WriteLine($"Папка {directory.Name} не используется более 30 минут");
                try
                {
                    directory.Delete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        static bool UsingFile(DirectoryInfo directory, TimeSpan interval)
        {
            FileInfo[] file = directory.GetFiles();

            foreach (FileInfo files in file)
            {
                if(DateTime.Now - directory.LastAccessTime < interval)
                {
                    return true;
                }
            }
            DirectoryInfo[] dir = directory.GetDirectories();
            foreach (DirectoryInfo dirs in dir)
            {
                UsingFile(dirs, interval);
            }
            return false;
        }
        static void Main(string[] args)
        {
            TimeSpan interval = TimeSpan.FromMinutes(30);
            Console.WriteLine("Введите путь к папке");

            string path = Console.ReadLine();
            if(Directory.Exists(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                LookFille(dirInfo, interval);
            }else
                Console.WriteLine("Путь к паке не найден");

        }
    }
}
