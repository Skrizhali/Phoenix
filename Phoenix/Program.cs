namespace Phoenix
{
    internal class Program
    {
        static void Main(string[] args) => Core.Program.Main(args);
       
        public static string Encrypt(string inputString)
        {
            int stringLength = inputString.Length;
            char[] encryptedChars = new char[stringLength];

            for (int i = 0; i < encryptedChars.Length; i++)
            {
                char currentChar = inputString[i];
                byte firstXor = (byte)((int)currentChar ^ (stringLength - i));
                byte secondXor = (byte)((int)(currentChar >> 8) ^ i);

                encryptedChars[i] = (char)(((int)secondXor << 8) | (int)firstXor);
            }

            return string.Intern(new string(encryptedChars));
        }

    }
}