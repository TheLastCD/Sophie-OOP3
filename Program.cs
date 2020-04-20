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
        //notes:
        // UI needs rebuilding, might struggle with how the verifcation is arranged
        // Still need to figure out how to define - or +
        // Line numbers need to be done.
        // Comment on Everything
        static void Main(string[] args)
        {
            Console.WriteLine(Directory.GetCurrentDirectory());

            Console.WriteLine("The following documents are available for comparison: \n1 GitRepositories_1a \n2 GitRepositories_1b \n3 GitRepositories_2a \n4 GitRepositories_2b \n5 GitRepositories_3a \n6 GitRepositories_3b");
            Console.WriteLine("Please select the first document you wish to compare: ");


            //User inputs the first file they wish to compare.
            string First_FilePath = Console.ReadLine();

            // The returned string is stored as a local variable within Main()
            string First_File = ReadFiles.readFiles(First_FilePath);
            StreamReader First_Reader = new StreamReader(First_FilePath);

            Console.WriteLine("Please select the second document you wish to compare: ");

            // The same thing is then repeated for the second file
            string Second_FilePath = Console.ReadLine();
            string Second_File = ReadFiles.readFiles(Second_FilePath);
            StreamReader Second_Reader = new StreamReader(Second_FilePath);

            // The bool from comparefiles method is returned and stored locally;
            bool Files_Same = Compare_Files(First_File, Second_File);

            // If the bool is true, then the files are the same 
            if (Files_Same == true)
            {
                Console.WriteLine(" The two files that you have selected are the same");
            }

            //Otherwise they are different
            else
            {
                Console.WriteLine("The compared that you have selected are not the same");
                //C: new method here
            }
        }

        // Method for comparing the two text files as arguments
        static bool Compare_Files(string First_File, string Second_File)
        {
            bool Files_Same;
            Files_Same = true;
            string[] FirstSentences = First_File.Split(".");
            string[] SecondSentences = Second_File.Split(".");

            string Output ="";

            for (int I = 0; I < FirstSentences.Length; I++)
            {
                if (FirstSentences[I] != SecondSentences[I])
                {
                    Files_Same = false;
                    string[] FirstSentenceWord = FirstSentences[I].Split();
                    string[] SecondSentenceWord = SecondSentences[I].Split();

                    for (int R = 0; R < FirstSentenceWord.Length; R++)
                    {
                        if (FirstSentenceWord[R] != SecondSentenceWord[R])
                        {
                            if (FirstSentenceWord[R] != SecondSentenceWord[R+1])
                            {
                                Output += SecondSentenceWord[R];
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($"{SecondSentenceWord[R]}");
                            }
                            else
                            {
                                Output += SecondSentenceWord[R];
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($"{FirstSentenceWord[R]}");
                            }

                            Console.ForegroundColor = ConsoleColor.White;
                        }

                        else
                        {
                            Output += SecondSentenceWord[R];
                            Console.Write($"{FirstSentenceWord[R]}");

                        }

                    }
                }

            }
            logger($"sentence: {I}\n{Output}");
            return Files_Same;
        }

       static void logger(string args)
        {
            File.WriteAllText(@"logfile.txt", args);
        }
    }

    // This class reads a singular file
    class ReadFiles
    {
        // The method of the class
        public static string readFiles(string File_Choice)
        {
            // Intialize a StreamReader object
            StreamReader Text_Object = new StreamReader(@"" + File_Choice + ".txt");

            //Creates a file path for StreamReader
            string File_Input = $@"" + File_Choice + ".txt";

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
                    File_Input = $@"textFiles/{File_Choice}.txt";
                }
            }

            string textfile = Text_Object.ReadToEnd();

            return textfile;
        }
    }
}
