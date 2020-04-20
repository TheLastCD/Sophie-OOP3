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
        //tester diff GitRepositories_2a.txt GitRepositories_2b.txt
        //notes:
        // UI needs rebuilding, might struggle with how the verifcation is arranged
        // Still need to figure out how to define - or +
        // Line numbers need to be done.
        // Comment on Everything
        static void Main(string[] args)
        {

            Console.Write(">: [Input]  ");

            string[] Userinput = new string[3];
            bool Files_Same = true;
            try
            {
                do
                {
                    Userinput = Console.ReadLine().Split();
                    if ((Userinput.Length != 3 || Userinput[0].ToLower() != "diff"))
                    {
                        Console.WriteLine("There has been an error");
                    }
                    else
                    {
                        break;
                    }


                }
                while (true);
                ReadFiles files = new ReadFiles(Userinput[1].ToString(), Userinput[2].ToString());
                Files_Same = Compare_Files(files.First_File, files.Second_File);

            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message}");
            }

            // The bool from comparefiles method is returned and stored locally;
            
            // If the bool is true, then the files are the same 
            if (Files_Same == true)
            {
                Console.WriteLine(" The two files that you have selected are the same");
            }
            else
            {
                Console.WriteLine("The compared that you have selected are not the same");

            }
        }

        // Method for comparing the two text files as arguments
        static bool Compare_Files(string First_File, string Second_File)
        {
            bool Files_Same = true;
            string[] FirstSentences = First_File.Split(".");
            string[] SecondSentences = Second_File.Split(".");
            int overflow = 1;
            string Output ="";
            Console.Write(">: Output ");

            for (int I = 0; I < FirstSentences.Length; I++)
            {
                if (FirstSentences[I] != SecondSentences[I])
                {
                    Files_Same = false;
                    string[] FirstSentenceWord = FirstSentences[I].Split();
                    string[] SecondSentenceWord = SecondSentences[I].Split();

                    for (int R = 0; R < FirstSentenceWord.Length; R++)
                    {
                        if (overflow == 0)
                            break;
                        if (FirstSentenceWord[R] != SecondSentenceWord[R])
                        {
                            if (FirstSentenceWord[I] != SecondSentenceWord[R+1])
                            {
                                Output += SecondSentenceWord[R];
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write($"{SecondSentenceWord[R]} ");
                            }
                            else
                            {
                                Output += SecondSentenceWord[R];
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write($"{FirstSentenceWord[R]} ");
                            }

                            Console.ForegroundColor = ConsoleColor.White;
                            overflow = -5;
                        }

                        else
                        {
                            Output += SecondSentenceWord[R];
                            Console.Write($"{FirstSentenceWord[R]} ");
                           
                        }
                        overflow++;
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

    // This class holds the two files as strings
    // its also responsible for reading the files

    class ReadFiles
    {
        public string First_File, Second_File;
        public ReadFiles(string f, string s)
        {
            First_File = readFiles(f);
            Second_File = readFiles(s);
        }
        // The method of the class
        private string readFiles(string File_Choice)
        {
            return File.ReadAllText(File_Choice).ToString();
        }
    }
}
