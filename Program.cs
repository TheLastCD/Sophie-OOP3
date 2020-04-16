using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Git_Diff
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write(">: [Input] ");

            string[] Files_Compare = Console.ReadLine().Split();


            // The returned string is stored as a local variable within Main()
            string First_File = ReadFiles.readFiles(Files_Compare[1]);
            StreamReader First_Reader = new StreamReader(Files_Compare[1]);

            string Second_File = ReadFiles.readFiles(Files_Compare[2]);
            StreamReader Second_Reader = new StreamReader(Files_Compare[2]);

            // The bool from comparefiles method is returned and stored locally;
            bool Files_Same = Compare_Files(First_File, Second_File);

            // If the bool is true, then the files are the same 
            if (Files_Same)
            {
                Console.WriteLine(" The two files that you have selected are the same");
            }

            //Otherwise they are different
            else
            {
                Console.WriteLine("The compared that you have selected are not the same");
            }
        }

        // Method for comparing the two text files as arguments
        static bool Compare_Files(string First_File, string Second_File)
        {
            bool Files_Same;

            // if the arguments are the same, return outcome as true
            if(First_File == Second_File)
            {
                Files_Same = true;
            }

            // If not then return false
            else
            {
                Files_Same = false;
            }
            return Files_Same;
        }
    }

    // This class reads a singular file
    class ReadFiles
    {
        // The method of the class
        public static string readFiles(string File_Choice)
        {
            // Intialize a StreamReader object
            StreamReader Text_Object = new StreamReader(@"" + File_Choice);

            //Creates a file path for StreamReader
            string File_Input = $@"" +File_Choice;

            //Ensures the while loop runs until a correct file path is found
            bool File_Found = false;
            while (File_Found == false)
            {
                try
                {
                    Text_Object = new StreamReader(File_Input);

                    File_Found = true;
                }

                catch (FileNotFoundException)
                {
                    Console.WriteLine(" Invalid file selected, please try again:");

                    File_Choice = Console.ReadLine();
                    File_Input = $@"textFiles/{File_Choice}";
                }
            }

            string textfile = Text_Object.ReadToEnd();

            return textfile;
        }
    }
}

