using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class Ceaser : ICryptographicTechnique<string, int>  // DONE ;)
    {
        public string Encrypt(string plainText, int key)
        {
            // throw new NotImplementedException();
            /*
             * plaintxt = "siham"
             * key = 3
             * Encrypt :
             * ---------
             * ( S(83) + 3 ) =>  86 - 65 = 21 % 26 = 21 + 65 => V
             * (A(65) + 3) => 68 - 65 = 3 % 26 = 3 + 'A' => D
             * cipher = "V..D"
             * 
             * Decrypt : 
             * ---------
             * cipher = "V..D"
             * ( V - 3 ) => 86 - 3 = 83 - 'A' => 83 - 65 => 18 % 26 => 18 + 65=>83
             * ( C(67) - 3) => 64 - 65 = -1 % 26 (X)
             * -- circular shifting --
             * +26
             */

            string plTxt = plainText.ToUpper();
            string cipherText = "";
            for (int i = 0; i < plTxt.Length; i++)
            {
                int idx = plTxt[i] - 'A';
                cipherText += (char)((idx + key + 26) % 26 + 'A');
            }
            return cipherText;
        }

        public string Decrypt(string cipherText, int key)
        {
            // throw new NotImplementedException();
            string cTxt = cipherText.ToUpper();
            string plainText = "";
            for (int i = 0; i < cTxt.Length; i++)
            {
                int idx = cTxt[i] - 'A';
                plainText += (char)((idx - key + 26) % 26 + 'A');
            }
            return plainText;
        }

        public int Analyse(string plainText, string cipherText)
        {
            // throw new NotImplementedException();
            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            return (cipherText[0] - plainText[0] + 26) % 26;
        }
    }
}

