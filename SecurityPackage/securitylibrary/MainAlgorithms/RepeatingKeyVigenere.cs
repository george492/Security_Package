using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RepeatingkeyVigenere : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            string key = "";
            int length = plainText.Length;
            for (int i = 0; i < length; i++)
            {
                int sum = ((int)(cipherText[i]) - 65) - ((int)(plainText[i]) - 65);
                if (sum < 0)
                    sum += 26;
                else
                    sum %= 26;
                key += (char)(sum + 65);


            }
            char first_char = key[0];
            string temp = "";
            temp += first_char;
            for (int i = 1; i < key.Length; i++)
            {
                if (key[i] == first_char)
                {
                    int temp_size = temp.Length;
                    if (temp_size > (key.Length - i))
                    {
                        string sub = key.Substring(i, key.Length - i);
                        if (temp.Contains(sub))
                            return temp;
                        else
                            temp += key[i];
                    }
                    else
                    {
                        string sub = key.Substring(i, temp_size);
                        if (sub == temp)
                        {
                            return temp;
                        }
                        else
                            temp += key[i];
                    }
                }
                else
                    temp += key[i];
            }

            return key;
            //throw new NotImplementedException();
        }
        // Function to get string length
        public string Decrypt(string cipherText, string key)
        {
            string plaintext = "";
            key = key.ToUpper();
            cipherText = cipherText.ToUpper();
            int length = cipherText.Length;
            if (cipherText.Length > key.Length)
            {

                int different_length = cipherText.Length - key.Length;
                //  Console.WriteLine(different_length);
                if (different_length > key.Length)
                {
                    int rept = different_length / key.Length;
                    int rem = different_length % key.Length;
                    string temp = key.Substring(0, key.Length);
                    string temp2 = key.Substring(0, rem);
                    for (int i = 0; i < rept; i++)
                        key += temp;
                    key += temp2;
                    //  Console.WriteLine(key);
                    //   Console.WriteLine(key);
                }
                else
                {
                    string temp = key.Substring(0, different_length);
                    key += temp;
                }

            }
            for (int i = 0; i < length; i++)
            {
                int sum = ((int)(cipherText[i]) - 65) - ((int)(key[i]) - 65);
                if (sum < 0)
                    sum += 26;
                else
                    sum %= 26;
                //   Console.WriteLine(sum);
                plaintext += (char)(sum + 65);


            }
            return plaintext;

            //throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            key = key.ToUpper();
            plainText = plainText.ToUpper();
            string cipherText = "";
            int length = plainText.Length;
            if (plainText.Length > key.Length)
            {

                int different_length = plainText.Length - key.Length;
                //  Console.WriteLine(different_length);
                if (different_length > key.Length)
                {
                    int rept = different_length / key.Length;
                    int rem = different_length % key.Length;
                    string temp = key.Substring(0, key.Length);
                    string temp2 = key.Substring(0, rem);
                    for (int i = 0; i < rept; i++)
                        key += temp;
                    key += temp2;
                    //  Console.WriteLine(key);
                    //   Console.WriteLine(key);
                }
                else
                {
                    string temp = key.Substring(0, different_length);
                    key += temp;
                }

            }
            for (int i = 0; i < length; i++)
            {
                int sum = ((int)(plainText[i]) - 65) + ((int)(key[i]) - 65);
                sum %= 26;
                cipherText += (char)(sum + 65);


            }
            return cipherText;


            //throw new NotImplementedException();
        }
    }
}