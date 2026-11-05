using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace RandomTextQuiz
{
    internal class Program
    {
        


        //method to print info my device
        static void PrintInfoMyDevice()
        {
            Console.WriteLine("Windows version: {0}", Environment.OSVersion);
            Console.WriteLine("64 Bit operating system? : {0}", Environment.Is64BitOperatingSystem ? "Yes" : "No");
            Console.WriteLine("PC Name : {0}", Environment.MachineName);
            Console.WriteLine("Number of CPUS : {0}", Environment.ProcessorCount);
            Console.WriteLine("Windows folder : {0}", Environment.SystemDirectory);
            Console.WriteLine("Logical Drives Available : {0}", String.Join(", ", Environment.GetLogicalDrives()).TrimEnd(',').Replace("\\", String.Empty));

        }
     
        static int ReadNumberOfQuestions()
        {
            int numQuestions = 0;
            Console.WriteLine("Please, Enter The Maximum Number Of Questions :");
            numQuestions = int.Parse(Console.ReadLine());

            while (numQuestions <= 0)
            {
                Console.WriteLine("The Number Of Questions Shoud Be An Integer!!!");
                numQuestions = int.Parse(Console.ReadLine());
            }

            return numQuestions;
        }

        static bool IsTextValid(string text)
        {
            int acceptedCharsCount = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if ((text[i] >= 'A' && text[i] <= 'Z') || (text[i] >= 'a' && text[i] <= 'z') || text[i] >= '0' && text[i] <= '9')
                {
                    ++acceptedCharsCount;

                }
            }
            return (acceptedCharsCount >= 6);
        }

        static string ReadInfoPlayer()
        {
            string infoPlayer = "";
            do
            {
                Console.WriteLine("Please enter your name (first name and last name) , your id number and your skills..\n With a space between each part (Accepted Chars: A-Z a-z 0-9)\n The entered text should contain at least 6 of accepted chars.");
                infoPlayer = Console.ReadLine();

            } while (!IsTextValid(infoPlayer));

            return infoPlayer;
        }

        static bool IsCharValid(char ch)
        {
            return ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || ch >= '0' && ch <= '9');
        }

        static string GetDistinctChar(string text)
        {
            string distinctChar = "";
            bool isFound = false;

            for (int i = 0; i < text.Length; i++)
            {
                isFound = false;
                if (IsCharValid(text[i]))
                {
                    for (int j = 0; j < distinctChar.Length; j++)
                    {
                        if (text[i] == distinctChar[j])
                        {
                            isFound = true;

                        }
                    }
                    if (!isFound)
                    {
                        distinctChar += text[i];
                    }

                }
            }
            return distinctChar;
        }

        static void GetInfoUser()
        {
            string info = ReadInfoPlayer();
            Console.WriteLine("Your name,Id and your skills :  " + info);
            Console.WriteLine("Distinct Chars are : " + GetDistinctChar(info));
        }

        static char GetRandomSymbol(int num)
        {
            //63  33  59  35  unicode for this symbols.
            //?   !   ;   # 
            char symbol =';' ;
            switch (num)
            {
                case 1:
                    symbol= '?'; break;
                case 2:
                    symbol = '!'; break;
                case 3:
                    symbol = ';'; break;
                case 4:
                    symbol = '#'; break;
            }
            return symbol;
        }
        static char GetRandomCharType(int randomType)
        {
            //int randomType = RandomNumber(1, 5);
            char oneChar = ';';
            switch (randomType)
            {
                case 1:
                    oneChar = (char)RandomNumber(65, 91);
                    break;
                case 2:
                    oneChar = (char)RandomNumber(97, 123);
                    break;

                case 3:
                    oneChar = (char)RandomNumber(48, 58);//gernerated numbers as char (0 => 9)
                    break;

                case 4:
                    oneChar = GetRandomSymbol(RandomNumber(1, 5));
                    break;

            }
           return oneChar;
        }
        static string GetRandomWord(int lengthWord)
        {
            string word = "";

            // Generate the first character as a letter, this gives 1 or 2 
            word += GetRandomCharType(RandomNumber(1, 3));

            // Generate the remaining characters.
            for (int i = 1; i < lengthWord; i++)
            {
                word += GetRandomCharType(RandomNumber(1, 5));
            }

            return word;
        }





        static void StatGame()
        {
            //int NumberOfQuestions = ReadNumberOfQuestions();
            // GetInfoUser();

            Console.WriteLine(GetRandomWord(4));
        }


        static void Main(string[] args)
        {
            //call method to print info for my device.
            PrintInfoMyDevice();
            StatGame();
        }
    }
}
