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

            Console.WriteLine("The following documents are available for comparison: \n1 GitRepositories_1a \n2 GitRepositories_1b \n3 GitRepositories_2a \n4 GitRepositories_2b \n5 GitRepositories_3a \n6 GitRepositories_3b");
            Console.WriteLine("Please select the first document you wish to compare: ");

            string[] Userinput = new string[3];
            bool Files_Same = true;
            try
            {
                do
                {
                    Userinput = Console.ReadLine().Split();
                    if ((Userinput.Length != 2 || Userinput[0].ToLower() == "diff"))
                    {
                        Console.WriteLine("There has been an error");
                    }
                    else
                    {
                        break;
                    }


                }
                while (true);
                string First_File = ReadFiles.readFiles(Userinput[1]);
                StreamReader First_Reader = new StreamReader(Userinput[1]);
                StreamReader Second_Reader = new StreamReader(Userinput[2]);

            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }

            // The bool from comparefiles method is returned and stored locally;

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

            string Output = "";

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
                            if (FirstSentenceWord[I] != SecondSentenceWord[R + 1])
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
                        logger($"sentence: {I}\n{Output}");
                    }

                }

            }
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
        return File.ReadAllLines(File_Choice).ToString();
    }
}
