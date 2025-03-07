using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class AutokeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            string key = "";

            for (int i = 0; i < plainText.Length; i++)
            {
                int diff = ((cipherText[i] - 'A') - (plainText[i] - 'A') + 26) % 26;
                key += (char)(diff + 'A');
            }
            for (int length = 1; length <= key.Length; length++)
            {
                string candidate = key.Substring(0, length);
                string generatedKey = candidate;
                int index = 0;

                while (generatedKey.Length < plainText.Length)
                {
                    generatedKey = generatedKey + plainText[index++];
                }

                if (generatedKey == key)
                {
                    return candidate;
                }
            }

                return key;
            }
        

        public string Decrypt(string cipherText, string key)
        {
            string plainText = "";
            key = key.ToUpper();
            cipherText = cipherText.ToUpper();

            for (int i = 0; i < cipherText.Length; i++)
            {
                int diff = ((cipherText[i] - 'A') - (key[i] - 'A') + 26) % 26;
                plainText += (char)(diff + 'A');
                if (key.Length < cipherText.Length)
                {
                    key +=  plainText[i];
                }
            }

            return plainText;

        }

        public string Encrypt(string plainText, string key)
        {
            key = key.ToUpper();
            plainText = plainText.ToUpper();
            string cipherText = "";
            int keyIndex = 0;
            while (key.Length < plainText.Length)
            {

                key += plainText[keyIndex++];
            }
            for (int i = 0; i < plainText.Length; i++)
            {

                int sum = ((int)(plainText[i]) - 65) + ((int)(key[i]) - 65);
                sum %= 26;
                cipherText += (char)(sum + 65);
            }

            return cipherText;
        }
    }
}
