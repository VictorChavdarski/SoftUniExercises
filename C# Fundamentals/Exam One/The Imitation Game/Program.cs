using System;

namespace _01.TheImitationGame
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputMessage = Console.ReadLine();

            string command = string.Empty;

            while ((command=Console.ReadLine())!="Decode")
            {
                string[] input = command.Split("|");
                string commOne = input[0];
                if (commOne=="Move")
                {
                    int count = int.Parse(input[1]);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < count; i++)
                    {
                        sb.Append(inputMessage[i]);
                    }
                    string substring = sb.ToString();
                    inputMessage= inputMessage.Replace(substring, string.Empty);
                    StringBuilder newsb = new StringBuilder(inputMessage);
                    newsb.Append(substring);
                    inputMessage = newsb.ToString();
                }
                else if (commOne=="Insert")
                {
                    int index = int.Parse(input[1]);
                    string value = input[2];
                   inputMessage = inputMessage.Insert(index, value);
                }
                else if (commOne=="ChangeAll")
                {
                    string substring = input[1];
                    string replacement = input[2];

                    inputMessage = inputMessage.Replace(substring, replacement);
                }
            }

            Console.WriteLine($"The decrypted message is: {inputMessage}");
        }
    }
}
