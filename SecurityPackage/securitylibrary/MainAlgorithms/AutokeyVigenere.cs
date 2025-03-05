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

        public string Decrypt(string cipherText, string key)
        {
        string plaintext = "";
key = key.ToUpper();
cipherText = cipherText.ToUpper();
int length = cipherText.Length;
           
for (int i = 0; i < length; i++)
{
    int sum = ((int)(cipherText[i]) - 65) - ((int)(key[i]) - 65);
    if (sum < 0)
        sum += 26;
    else
        sum %= 26;
 
    plaintext += (char)(sum + 65);

    if (key.Length < cipherText.Length){
        key += plaintext[i];
    }

}
return plaintext;

           // throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
        key = key.ToUpper();
plainText = plainText.ToUpper();
string cipherText = "";
int length = plainText.Length;
          
for (int i = 0; i < length; i++)
{
    int sum = ((int)(plainText[i]) - 65) + ((int)(key[i]) - 65);
    sum %= 26;
    cipherText += (char)(sum + 65);
    if (key.Length < plainText.Length)
    {
        key += plainText[i];
    }

}
return cipherText;
            //throw new NotImplementedException();
        }
    }
}
