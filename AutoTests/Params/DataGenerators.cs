using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTests.Params
{
    class DataGenerators
    {

        public static int RandomNumber(int maxValue, int minValue = 0)
        {
            Random random = new Random();
            int randomNumber = random.Next(minValue, maxValue);
            return randomNumber;
        }

        public static string ReturnRandomString(int stringLenght, string availableChars)
        {
            List<char> chars = new List<char>();

            Random random = new Random();
            for (int i = 0; i <= stringLenght; i++)
            {
                char randChar = availableChars[random.Next(0, availableChars.Length)];
                chars.Add(randChar);
            }

            return new String(chars.ToArray());
        }
 
        public static char ReturnRandomChar(string availableChars)
        {
            return availableChars[RandomNumber(availableChars.Length)];
        }

        public static string ReturnRandomEmail()
        {
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string numbersLettersAndSpecialSigns = "0123456789-_." + letters;

            string firstPart = ReturnRandomChar(letters) + ReturnRandomString(RandomNumber(30, 15), numbersLettersAndSpecialSigns);

            string domain = ReturnRandomString(RandomNumber(6, 3), letters) + "." 
                + ReturnRandomString(RandomNumber(3,2),letters);

            return firstPart + "@" + domain;
        }
        

        
    }


        


        







}
        

        

