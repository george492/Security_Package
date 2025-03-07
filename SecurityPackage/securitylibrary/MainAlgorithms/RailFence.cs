using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            //throw new NotImplementedException();
            //cipherText = cipherText.ToUpper();
            //COMPARISON ;)
            int key = 0;
            int len = plainText.Length;
            for (int z = 1; z <= len; z++)
            {
                if (Encrypt(plainText, z).Equals(cipherText, StringComparison.InvariantCultureIgnoreCase))
                {
                    key = z;
                    break;
                }
            }
            return key;
        }

        public string Decrypt(string cipherText, int key)
        {
            //throw new NotImplementedException();
            int i = 0;
            int ceil = (int)Math.Ceiling((double)cipherText.Length / key);
            //int round = (int)(plainText.Length / key)== (double)(plainText.Length / key) ? (plainText.Length / key) : (plainText.Length / key)+1;
            char[,] plain = new char[key, ceil];  //3,(19/2+1)=>10
            for (int k = 0; k < key; k++)  //3 thabet el key
            {
                for (int j = 0; j < ceil; j++) //6 7arak el col
                {
                    if (i < cipherText.Length)
                        plain[k, j] = cipherText[i];
                    else
                        plain[k, j] = ' ';
                    i++;
                }
            }
            //col then rows
            string cipherstr = "";
            for (int j = 0; j < ceil; j++) //6 thabet el col
            {
                for (int k = 0; k < key; k++)  //3 7arak el key
                {
                    if (plain[k, j] == ' ')
                        cipherstr += "";
                    else
                        cipherstr += plain[k, j];
                }
            }
            return cipherstr.ToUpper();
        }
        /*
         * meetmeaftertheparty => 19(len)
         * 
         * chiper[3,(19/3)+1]
         * 
         * 
         * 
         */
        public string Encrypt(string plainText, int key)
        {
            //throw new NotImplementedException();
            int i = 0;

            int ceil = (int)Math.Ceiling((double)plainText.Length / key);
            //int round = (int)(plainText.Length / key)== (double)(plainText.Length / key) ? (plainText.Length / key) : (plainText.Length / key)+1;
            char[,] chiper = new char[key, ceil];  //3,10
            //cols then rows
            for (int j = 0; j < ceil; j++) //6
            {
                for (int k = 0; k < key; k++)  //3
                {
                    if (i < plainText.Length)
                        chiper[k, j] = plainText[i];
                    else
                        chiper[k, j] = ' ';
                    i++;
                }
            }
            string plainstr = "";
            //rows then cols
            for (int k = 0; k < key; k++)
            {
                for (int j = 0; j < ceil; j++)
                {
                    if (chiper[k, j] == ' ')
                        plainstr += "";
                    else
                        plainstr += chiper[k, j];
                }
            }

            return plainstr.ToLower();
        }
    }
}