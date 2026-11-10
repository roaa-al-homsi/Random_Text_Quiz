using System;
namespace RandomTextQuiz
{
    internal class Program
    {
        // Stores the randomly generated text for each question.
        static string[] GeneratedTexts;

        // Stores the correct answers for each question.
        static int[] CorrectTotalLengths;
        static int[] CorrectVowelsCounts;
        static int[] CorrectLowerCharactersCounts;
        static int[] CorrectSpecialCharactersCounts;

        // Stores the answers entered by the player for each question.
        static int[] UserTotalLengths;
        static int[] UserVowelsCounts;
        static int[] UserLowerCharactersCounts;
        static int[] UserSpecialCharactersCounts;

        // Stores the evaluation code for each question:
        // 0 = All answers are wrong or player enter ignore.
        // 1 = All answers are correct.
        // 2 = Some answers are correct and some are wrong.
        static int[] EvaluationCodes;

        static Random randomGenerator = new Random();
        static int RandomNumber(int From, int To)
        {
            return randomGenerator.Next(From, To);
        }

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
            int numberOfQuestions;
            // Ask the user to enter the maximum number of questions.
            Console.WriteLine("Please, Enter The Maximum Number Of Questions :");

            // Read the number entered by the user.
            numberOfQuestions = int.Parse(Console.ReadLine());

            // Repeat until the user enters a positive number.
            while (numberOfQuestions <= 0)
            {
                Console.WriteLine("The Number Of Questions Shoud Be Greater Than Zero !!!");
                numberOfQuestions = int.Parse(Console.ReadLine());
            }

            // Return the valid number of questions.
            return numberOfQuestions;
        }
        static void InitializeArrays(int numberOfQuestions)
        {
            // Create arrays to store data for all questions.
            GeneratedTexts = new string[numberOfQuestions];
            EvaluationCodes = new int[numberOfQuestions];

            // Create arrays to store the correct answers for each question.
            CorrectLowerCharactersCounts = new int[numberOfQuestions];
            CorrectSpecialCharactersCounts = new int[numberOfQuestions];
            CorrectTotalLengths = new int[numberOfQuestions];
            CorrectVowelsCounts = new int[numberOfQuestions];

            // Create arrays to store the answers entered by the player.
            UserTotalLengths = new int[numberOfQuestions];
            UserLowerCharactersCounts = new int[numberOfQuestions];
            UserSpecialCharactersCounts = new int[numberOfQuestions];
            UserVowelsCounts = new int[numberOfQuestions];
        }


        // Check whether the entered text contains at least six accepted characters.
        static bool IsTextValid(string text)
        {
            // Initialize a counter for accepted characters.
            int acceptedCharsCount = 0;

            // Loop through all characters in the entered text.
            for (int i = 0; i < text.Length; i++)
            {
                // Check whether the current character is an uppercase letter, lowercase letter, or digit.
                if (IsAcceptedChar(text[i]))
                {
                    // Increase the counter when an accepted character is found.
                    ++acceptedCharsCount;

                }
            }

            // Return true if the text contains at least six accepted characters.
            return (acceptedCharsCount >= 6);
        }
        static string ReadInfoPlayer()
        {
            // Initialize a string to store the player's information.
            string infoPlayer = "";

            // Keep asking the user until valid information is entered.
            do
            {
                Console.WriteLine("\nPlease enter your name (first name and last name) , your id number and your skills..\n With a space between each part (Accepted Chars: A-Z a-z 0-9)\n The entered text should contain at least 6 of accepted chars.");

                // Read the player's information.
                infoPlayer = Console.ReadLine();

            } while (!IsTextValid(infoPlayer));

            // Return the valid player's information.
            return infoPlayer;
        }

        // Check whether a character is an uppercase letter, lowercase letter, or digit.
        static bool IsAcceptedChar(char ch)
        {
            return ((ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || ch >= '0' && ch <= '9');
        }
        static string GetDistinctCharacters(string text)
        {
            // Initialize an empty string to store distinct accepted characters.
            string distinctChar = "";

            // Initialize a flag to check whether a character already exists.
            bool isFound = false;

            // Loop through all characters in the original text.
            for (int i = 0; i < text.Length; i++)
            {
                // Reset the flag before checking each new character.
                isFound = false;

                // Check whether the current character is accepted.
                if (IsAcceptedChar(text[i]))
                {
                    // Search for the current character in the distinct characters string.
                    for (int j = 0; j < distinctChar.Length; j++)
                    {
                        // Check whether the current character already exists.
                        if (text[i] == distinctChar[j])
                        {
                            // Set the flag to true if the character is found.
                            isFound = true;

                        }
                    }

                    // Add the character only if it has not been found before.
                    if (!isFound)
                    {
                        distinctChar += text[i];
                    }
                }
            }
            // Return the string containing distinct accepted characters.
            return distinctChar;
        }
        static void DisplayPlayerInfo()
        {
            // Read the player's information.
            string info = ReadInfoPlayer();

            // Display the original information entered by the player.
            Console.WriteLine("\nYour name,Id and your skills :  " + info);

            // Display distinct accepted characters in their original order.
            Console.WriteLine("Distinct Chars are : " + GetDistinctCharacters(info) + "\n");
        }



        static char GetRandomSymbol(int num)
        {
            //63  33  59  35  unicode for this symbols.
            //?   !   ;   # 
            // Initialize the symbol with a default value.
            char symbol = ';';

            // Select the symbol according to the given random number.
            switch (num)
            {
                case 1:
                    symbol = '?'; break;
                case 2:
                    symbol = '!'; break;
                case 3:
                    symbol = ';'; break;
                case 4:
                    symbol = '#'; break;
            }

            // Return the selected symbol.
            return symbol;
        }
        static char GenerateRandomCharacter(int randomType)
        {
            // Initialize a character variable to store the generated character.
            char oneChar = ';';

            // Generate a character according to the selected random type.
            // 1 => (A-Z) 
            // 2  =>(a-z)
            // 3 =>(0 => 9)
            // 4 => (1 =>? , 2 =>!, 3 =>; , 4 => #)  Generate a random special symbol.
            switch (randomType)
            {
                case 1:
                    // Generate a random uppercase letter (A-Z).
                    oneChar = (char)RandomNumber(65, 91);
                    break;
                case 2:
                    // Generate a random lowercase letter (a-z).
                    oneChar = (char)RandomNumber(97, 123);
                    break;

                case 3:
                    // Generate a random digit as a character (0 => 9).
                    oneChar = (char)RandomNumber(48, 58);
                    break;

                case 4:
                    // Generate a random special symbol.
                    oneChar = GetRandomSymbol(RandomNumber(1, 5));
                    break;

            }

            // Return the generated character.
            return oneChar;
        }
        static string GenerateRandomWord(int lengthWord)
        {
            // Initialize an empty string to store the generated word.
            string word = "";

            // Generate the first character as a letter, this gives 1 or 2 
            word += GenerateRandomCharacter(RandomNumber(1, 3));

            // Generate the remaining characters.
            for (int i = 1; i < lengthWord; i++)
            {
                // Generate a random character and append it to the word.
                word += GenerateRandomCharacter(RandomNumber(1, 5));  // 1 => (A-Z)    ,    2  =>(a-z) ,     3 =>(0 => 9)  , 4 => (1 =>? , 2 =>!, 3 =>; , 4 => #) 
            }

            // Return the complete generated word.
            return word;
        }
        static string GenerateRandomText()
        {
            // Initialize an empty string to store the generated text.
            string text = "";

            // Generate a random number of words between 4 and 8 ,,,
            // Because the instructions state that the number of words must be between 4 and 8
            int numberOfWords = RandomNumber(4, 9);

            // Generate the required number of random words.
            do
            {
                // Generate a random word with a length between 5 and 10 characters.As per instructions
                string word = GenerateRandomWord(RandomNumber(5, 11));

                // Add the generated word to the text.
                text += word;

                // Add a space between words, except after the last word.
                if (numberOfWords > 1)
                {
                    text += " ";
                }

                // Decrease the remaining number of words.
                numberOfWords--;

            } while (numberOfWords > 0);

            // Return the complete generated text.
            return text;
        }



        static int CalculateTotalLength(string text)
        {
            // Initialize a counter for the current word length.
            int countLengthWord = 0;

            // Initialize a variable to store the total length of all words.
            int totalLenghtWords = 0;

            // Loop through all characters in the text.
            for (int i = 0; i < text.Length; i++)
            {
                // Check if the current character is not a space.
                if (text[i] != ' ')
                {
                    // Increase the current word length by one.
                    ++countLengthWord;
                }
                else
                {
                    // Add the completed word length to the total.
                    totalLenghtWords += countLengthWord;

                    // Reset the counter to start counting the next word.
                    countLengthWord = 0;
                }
            }

            // Add the length of the last word because it may not end with a space.
            totalLenghtWords += countLengthWord;

            // Return the total length of all words.
            return totalLenghtWords;
        }
        static bool IsContainsVowel(string word)
        {    // a A e E i I o O u U y Y
            // Initialize  to indicate whether the word contains a vowel.
            bool isContains = false;

            // Loop through all characters in the word.
            for (int i = 0; i < word.Length; i++)
            {
                // Convert the current character to uppercase and check if it is a vowel.
                if (char.ToUpper(word[i]) == 'A' || char.ToUpper(word[i]) == 'E' ||
                    char.ToUpper(word[i]) == 'I' || char.ToUpper(word[i]) == 'O' ||
                    char.ToUpper(word[i]) == 'U' || char.ToUpper(word[i]) == 'Y'
                   )
                {
                    // Set the flag to true because a vowel was found.
                    isContains = true;

                    // Stop checking the remaining characters.
                    break;
                }
            }

            // Return whether the word contains at least one vowel.
            return isContains;
        }
        static int CountWordsWithVowels(string text)
        {
            // a A e E i I o O u U y Y

            // Initialize a counter for words that contain at least one vowel.
            int NumWordsContainsVowels = 0;

            // Initialize an empty string to build each word.
            string word = "";

            // Loop through all characters in the text.
            for (int i = 0; i < text.Length; i++)
            {
                // Check if the current character is not a space.
                if (text[i] != ' ')
                {
                    // Add the current character to the current word.
                    word += text[i];
                }
                else
                {
                    // Check whether the completed word contains a vowel.
                    if (IsContainsVowel(word))
                    {
                        // Increase the counter if the word contains a vowel.
                        NumWordsContainsVowels++;
                    }

                    // Clear the word to start building the next one.
                    word = "";
                }
            }

            // Check the last word because it may not be followed by a space.
            if (IsContainsVowel(word))
            {
                // Increase the counter if the last word contains a vowel.
                NumWordsContainsVowels++;
            }

            // Return the total number of words containing vowels.
            return NumWordsContainsVowels;
        }

        static bool IsContainsLowerCharacter(string word)
        {
            // Loop through all characters in the word.
            for (int i = 0; i < word.Length; i++)
            {
                // Check if the current character is a lowercase letter.
                if (word[i] >= 'a' && word[i] <= 'z')
                {
                    // Return true if a lowercase letter is found.
                    return true;
                }
            }

            // Return false if no lowercase letters are found.
            return false;
        }
        static int CountWordsWithLowerCharacter(string text)
        {
            // Initialize a counter for words containing lowercase letters.
            int NumWordsWithLowerCharacter = 0;

            // Initialize an empty string to build each word.
            string word = "";

            // Loop through all characters in the text.
            for (int i = 0; i < text.Length; i++)
            {
                // Check if the current character is not a space.
                if (text[i] != ' ')
                {
                    // Add the current character to the word.
                    word += text[i];
                }
                else
                {
                    // Check if the completed word contains a lowercase letter.
                    if (IsContainsLowerCharacter(word))
                    {
                        // Increase the counter if the word contains a lowercase letter.
                        NumWordsWithLowerCharacter++;
                    }

                    // Clear the word to start building the next one.
                    word = "";
                }
            }

            // Check the last word because it may not be followed by a space.
            if (IsContainsLowerCharacter(word))
            {
                // Increase the counter if the last word contains a lowercase letter.
                NumWordsWithLowerCharacter++;
            }

            // Return the total number of words containing lowercase letters.
            return NumWordsWithLowerCharacter;
        }

        static bool IsContainsSpecialCharacter(string word)
        {
            // Loop through all characters in the word.
            for (int i = 0; i < word.Length; i++)
            {
                // Check if the current character is one of the four special characters.
                if (word[i] == '?' || word[i] == '!' ||
                    word[i] == ';' || word[i] == '#')
                {
                    // Return true if a special character is found.
                    return true;
                }
            }

            // Return false if no special characters are found.
            return false;
        }
        static int CountWordsWithSpecialCharacters(string text)
        {
            // Initialize a counter for words containing special characters.
            int NumWordsWithSpecialCharacters = 0;

            // Initialize an empty string to build each word.
            string word = "";

            // Loop through all characters in the text.
            for (int i = 0; i < text.Length; i++)
            {
                // Check if the current character is not a space.
                if (text[i] != ' ')
                {
                    // Add the current character to the word.
                    word += text[i];
                }
                else
                {
                    // Check if the completed word contains a special character.
                    if (IsContainsSpecialCharacter(word))
                    {
                        // Increase the counter if the word contains a special character.
                        NumWordsWithSpecialCharacters++;
                    }

                    // Clear the word to start building the next one.
                    word = "";
                }
            }

            // Check the last word because it may not be followed by a space.
            if (IsContainsSpecialCharacter(word))
            {
                // Increase the counter if the last word contains a special character.
                NumWordsWithSpecialCharacters++;
            }

            // Return the total number of words containing special characters.
            return NumWordsWithSpecialCharacters;
        }



        // Read the player's answer and check if they want to ignore the question.
        static string ReadPlayerAnswer()
        {
            string answer = "";
            answer = Console.ReadLine();

            // Check if the answer is IGNORE, regardless of letter case.
            if (answer.ToUpper() == "IGNORE")
            {
                return answer = "IGNORE";
            }

            // Return the player's answer if it is not IGNORE.
            return answer;
        }
        static void StartQuiz(int numberOfQuestions)
        {
            string answer = "";

            // Repeat the question process according to the requested number of questions.
            for (int i = 0; i < numberOfQuestions; i++)
            {
                // Display the current question number.
                Console.WriteLine("Question :" + (i + 1));
                Console.WriteLine("--------");
                // Generate a random text and display it to the player.
                string generatedText = GenerateRandomText();
                Console.WriteLine("**************[" + generatedText + "]**************");

                // Store the generated text in the corresponding array position.
                GeneratedTexts[i] = generatedText;

                // Calculate and store the four correct answers for the current question.
                CorrectTotalLengths[i] = CalculateTotalLength(generatedText);
                CorrectVowelsCounts[i] = CountWordsWithVowels(generatedText);
                CorrectLowerCharactersCounts[i] = CountWordsWithLowerCharacter(generatedText);
                CorrectSpecialCharactersCounts[i] = CountWordsWithSpecialCharacters(generatedText);

                // Ask the player for the total length of all words.
                Console.WriteLine("1.What is the total length of the words?");
                answer = ReadPlayerAnswer();

                // Check if the player wants to ignore the current question.
                if (answer == "IGNORE")
                {
                    // Mark the entire question as wrong and move to the next question.
                    EvaluationCodes[i] = 0;
                    Console.WriteLine("The Question Is Ignored...As you like..");
                    continue;
                }

                // Convert the player's answer to an integer and store it.
                UserTotalLengths[i] = int.Parse(answer);

                // Ask how many words contain at least one vowel.
                Console.WriteLine("2.How many words contain vowels [a A e E i I o O u U y Y] ?");
                answer = ReadPlayerAnswer();

                // Check if the player wants to ignore the current question.
                if (answer == "IGNORE")
                {
                    // Mark the entire question as wrong and move to the next question.
                    EvaluationCodes[i] = 0;
                    Console.WriteLine("The Question Is Ignored...As you like..");
                    continue;
                }

                // Convert the player's answer to an integer and store it.
                UserVowelsCounts[i] = int.Parse(answer);

                // Ask how many words contain at least one lowercase letter.
                Console.WriteLine("3.How many words contain lowercase letters?");
                answer = ReadPlayerAnswer();

                // Check if the player wants to ignore the current question.
                if (answer == "IGNORE")
                {
                    // Mark the entire question as wrong and move to the next question.
                    EvaluationCodes[i] = 0;
                    Console.WriteLine("The Question Is Ignored...As you like..");
                    continue;
                }

                // Convert the player's answer to an integer and store it.
                UserLowerCharactersCounts[i] = int.Parse(answer);

                // Ask how many words contain at least one special character.
                Console.WriteLine("4.How many words contain special characters [! ; # ? ] ? ");
                answer = ReadPlayerAnswer();

                // Check if the player wants to ignore the current question.
                if (answer == "IGNORE")
                {
                    // Mark the entire question as wrong and move to the next question.
                    EvaluationCodes[i] = 0;
                    Console.WriteLine("The Question Is Ignored...As you like..");
                    continue;
                }

                // Convert the player's answer to an integer and store it.
                UserSpecialCharactersCounts[i] = int.Parse(answer);

                EvaluateQuestion(i);// it is a questionIndex.

            }
        }
        static void EvaluateQuestion(int questionIndex)
        {
            // Initialize a counter to store the number of correct answers.
            byte correctAnswer = 0;

            // Compare the player's answer with the correct answer for each question.
            // Increase the counter if the player's answer is correct.

            if (UserVowelsCounts[questionIndex] == CorrectVowelsCounts[questionIndex])
            {
                ++correctAnswer;
            }

            if (UserTotalLengths[questionIndex] == CorrectTotalLengths[questionIndex])
            {
                ++correctAnswer;
            }

            if (UserSpecialCharactersCounts[questionIndex] == CorrectSpecialCharactersCounts[questionIndex])
            {
                ++correctAnswer;
            }

            if (UserLowerCharactersCounts[questionIndex] == CorrectLowerCharactersCounts[questionIndex])
            {
                ++correctAnswer;
            }

            // Determine the evaluation code based on the number of correct answers.
            switch (correctAnswer)
            {
                case 0: // 0 = All answers are wrong or the question was ignored.
                    EvaluationCodes[questionIndex] = 0;
                    break;

                case 4: // All four answers are correct.
                    EvaluationCodes[questionIndex] = 1;
                    break;

                default: // At least one answer is correct and at least one is wrong.
                    EvaluationCodes[questionIndex] = 2;
                    break;
            }
        }

        static int CountTotalCorrectAnswers()
        {
            // Initialize a counter to store the total number of correct answers.
            int countCorrectPlayer = 0;

            // Loop through all questions.
            for (int i = 0; i < CorrectTotalLengths.Length; i++)
            {
                // Skip the current question if it was ignored or all its answers were wrong.
                if (EvaluationCodes[i] == 0)
                {
                    continue;
                }

                // Compare the player's total length answer with the correct answer.
                // Increment the counter if the answer is correct.
                if (UserTotalLengths[i] == CorrectTotalLengths[i])
                {
                    ++countCorrectPlayer;
                }

                // Compare the player's vowels count with the correct answer.
                if (UserVowelsCounts[i] == CorrectVowelsCounts[i])
                {
                    ++countCorrectPlayer;
                }

                // Compare the player's special characters count with the correct answer.
                if (UserSpecialCharactersCounts[i] == CorrectSpecialCharactersCounts[i])
                {
                    ++countCorrectPlayer;
                }

                // Compare the player's lowercase characters count with the correct answer.
                if (UserLowerCharactersCounts[i] == CorrectLowerCharactersCounts[i])
                {
                    ++countCorrectPlayer;
                }
            }

            // Return the total number of correct answers.
            return countCorrectPlayer;
        }
        static double CalculateCorrectAnswersPercentage()
        {
            // Calculate the percentage of correct answers out of all possible answers.
            double percentage = (CountTotalCorrectAnswers() / (double)(CorrectTotalLengths.Length * 4)) * 100;

            // Return the calculated percentage.
            return percentage;
        }

        static void ShowGeneratedTexts()
        {
            // Display the section title.
            Console.WriteLine("************* All Generated Texts *************\n");

            // Loop through all generated texts.
            for (int i = 0; i < GeneratedTexts.Length; i++)
            {
                // Display the question number and its generated text.
                Console.WriteLine("Question " + (i + 1) + ": " + GeneratedTexts[i]);

                // Print an empty line to separate the questions.
                Console.WriteLine();
            }
        }
        static void ShowUserAnswers()
        {
            // Display the section title.
            Console.WriteLine("************* All User Answers *************\n");

            // Loop through all questions.
            for (int i = 0; i < UserTotalLengths.Length; i++)
            {
                // Display the question number and indicate that the following values are the player's answers.
                Console.WriteLine("Player's Answers for Question " + (i + 1));
                Console.WriteLine("*****************************************\n");

                // Check if the player ignored the current question.
                if (EvaluationCodes[i] == 0)
                {
                    // Display a message indicating that the question was ignored.
                    Console.WriteLine("Question Status: IGNORED");
                }
                else
                {
                    // Display the player's answer for the total length question.
                    Console.WriteLine("Player's Answer - Total Length: " + UserTotalLengths[i]);

                    // Display the player's answer for the number of words containing vowels.
                    Console.WriteLine("Player's Answer - Words with Vowels: " + UserVowelsCounts[i]);

                    // Display the player's answer for the number of words containing lowercase characters.
                    Console.WriteLine("Player's Answer - Words with Lowercase Characters: " + UserLowerCharactersCounts[i]);

                    // Display the player's answer for the number of words containing special characters.
                    Console.WriteLine("Player's Answer - Words with Special Characters: " + UserSpecialCharactersCounts[i]);
                }

                // Print an empty line to separate the answers of each question.
                Console.WriteLine();
            }
        }
        static void ShowCorrectAnswers()
        {
            // Display the section title.
            Console.WriteLine("************* All Correct Answers *************\n");

            // Loop through all questions.
            for (int i = 0; i < CorrectTotalLengths.Length; i++)
            {
                // Display the question number and indicate that the following values are the correct answers.
                Console.WriteLine("Correct Answers for Question " + (i + 1));
                Console.WriteLine("*****************************************\n");

                // Display the correct answer for the total length question.
                Console.WriteLine("Correct Answer - Total Length: " + CorrectTotalLengths[i]);

                // Display the correct answer for the number of words containing vowels.
                Console.WriteLine("Correct Answer - Words with Vowels: " + CorrectVowelsCounts[i]);

                // Display the correct answer for the number of words containing lowercase characters.
                Console.WriteLine("Correct Answer - Words with Lowercase Characters: " + CorrectLowerCharactersCounts[i]);

                // Display the correct answer for the number of words containing special characters.
                Console.WriteLine("Correct Answer - Words with Special Characters: " + CorrectSpecialCharactersCounts[i]);

                // Print an empty line to separate the answers of each question.
                Console.WriteLine();
            }
        }
        static void ShowUserAndCorrectAnswers()
        {
            // Display the section title.
            Console.WriteLine("************* User Answers and Correct Answers *************\n");

            // Loop through all questions.
            for (int i = 0; i < UserTotalLengths.Length; i++)
            {
                // Display the question number.
                Console.WriteLine("Answers for Question " + (i + 1));
                Console.WriteLine("*****************************************\n");

                // Display the player's answer and the correct answer for total length.
                Console.WriteLine("Total Length - User Answer: " + UserTotalLengths[i]);
                Console.WriteLine("Total Length - Correct Answer: " + CorrectTotalLengths[i] + "\n");

                // Display the player's answer and the correct answer for words containing vowels.
                Console.WriteLine("Words with Vowels - User Answer: " + UserVowelsCounts[i]);
                Console.WriteLine("Words with Vowels - Correct Answer: " + CorrectVowelsCounts[i] + "\n");

                // Display the player's answer and the correct answer for words containing lowercase characters.
                Console.WriteLine("Words with Lowercase Characters - User Answer: " + UserLowerCharactersCounts[i]);
                Console.WriteLine("Words with Lowercase Characters - Correct Answer: " + CorrectLowerCharactersCounts[i] + "\n");

                // Display the player's answer and the correct answer for words containing special characters.
                Console.WriteLine("Words with Special Characters - User Answer: " + UserSpecialCharactersCounts[i]);
                Console.WriteLine("Words with Special Characters - Correct Answer: " + CorrectSpecialCharactersCounts[i] + "\n");

                // Print an empty line to separate the answers of each question.
                Console.WriteLine();
            }
        }

        // Method to read and validate the player's choice from the result menu.
        static int ReadChoicePlayerForResultMenu()
        {
            int choice = 0;
            string input = "";

            do
            {
                // Ask the player to enter a menu choice.
                Console.WriteLine(" Please enter what do you want?");
                input = Console.ReadLine();

                // Check if the player wants to terminate the program.
                if (input.ToUpper() == "QUIT")
                {
                    // Return -1 to indicate that the player wants to exit.
                    return -1;
                }

                // Convert the player's input to an integer.
                choice = int.Parse(input);

            } while (choice < 1 || choice > 5); // Repeat until the player enters a valid choice.

            // Return the valid menu choice.
            return choice;
        }

        // Method to execute the action selected by the player.
        static void PerformChoiceStatisticMenu(int choice)
        {
            // Check the player's choice and call the corresponding method.
            switch (choice)
            {
                case 1:
                    // Display the percentage of correct answers.
                    Console.WriteLine("");
                    Console.WriteLine("*** Correct Answers Percentage : " + CalculateCorrectAnswersPercentage() + "%");
                    break;

                case 2:
                    // Display all generated texts.
                    ShowGeneratedTexts();
                    break;

                case 3:
                    // Display the player's answers without showing the generated texts.
                    ShowUserAnswers();
                    break;

                case 4:
                    // Display the correct answers without showing the generated texts.
                    ShowCorrectAnswers();
                    break;

                case 5:
                    // Display the player's answers alongside the correct answers.
                    ShowUserAndCorrectAnswers();
                    break;

                case -1:
                    // Do nothing because the player wants to exit the menu.
                    break;
            }
        }

        // Method to display the statistics and result menu repeatedly.
        static void DisplayStatisticsAndResultMenu()
        {
            int choice = 0;

            do
            {
                // Clear the console before displaying the menu.
                Console.Clear();

                // Display the statistics and result menu.
                Console.WriteLine();
                Console.WriteLine("******************* STATISTICS AND RESULT MENU *******************");
                Console.WriteLine("*************************************************************");
                Console.WriteLine();
                Console.WriteLine("           [1] Show percentage of correct answers.");
                Console.WriteLine("           [2] Show generated text for each question.");
                Console.WriteLine("           [3] Show user answers without text.");
                Console.WriteLine("           [4] Show correct answers without text.");
                Console.WriteLine("           [5] Show user answers and correct answers together without text.");
                Console.WriteLine("           Type [QUIT] to terminate the program.\n");
                Console.WriteLine("*************************************************************");

                // Read the player's choice from the menu.
                choice = ReadChoicePlayerForResultMenu();

                // Perform the action corresponding to the selected choice.
                PerformChoiceStatisticMenu(choice);

                // Wait for the player to press a key before returning to the menu.
                if (choice != -1)
                {
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }

            } while (choice != -1); // Keep displaying the menu until the player chooses to quit.
        }
        static void StatGame()
        {
            //call method to print info for my device.
            PrintInfoMyDevice();

            int numberOfQuestions = ReadNumberOfQuestions();
            InitializeArrays(numberOfQuestions);
            DisplayPlayerInfo();
            StartQuiz(numberOfQuestions);
            DisplayStatisticsAndResultMenu();
        }
        static void Main(string[] args)
        {
            StatGame();
        }
    }
}
