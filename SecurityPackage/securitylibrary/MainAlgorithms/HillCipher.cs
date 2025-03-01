using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher :  ICryptographicTechnique<List<int>, List<int>>
    {
     
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            
            throw new NotImplementedException();
        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            throw new NotImplementedException();
        }


        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            int mod = 26;
            List<int> cypher = new List<int>();
            
            if (key.Count() == 4)
            {
                if (plainText.Count() % 2 != 0)
                {
                    plainText.Add(0);
                }
                for (int i = 0; i < plainText.Count() - 1; i++)
                {
                    int a = (key[0] * plainText[i] + key[1] * plainText[i + 1]) % mod;
                    cypher.Add(a);
                    a = key[2] * (plainText[i] + key[3] * plainText[i + 1]) % mod;
                    cypher.Add(a);
                }
                return cypher;
            }

            else if(key.Count() == 9)
            {
                if (plainText.Count() % 3 == 1)
                {
                    plainText.Add(0);
                }
                else if (plainText.Count() % 3 == 2)
                {
                    plainText.Add(0);
                    plainText.Add(0);
                }
                for (int i = 0; i < plainText.Count() - 1; i++)
                {
                    int a = (key[0] * plainText[i] + key[1] * plainText[i + 1] + key[2] * plainText[i + 2]) % mod;
                    cypher.Add(a);
                    a = (key[3] * plainText[i] + key[4] * plainText[i + 1] + key[5] * plainText[i + 2]) % mod;
                    cypher.Add(a);
                    a = (key[6] * plainText[i] + key[7] * plainText[i + 1] + key[8] * plainText[i + 2]) % mod;
                    cypher.Add(a);
                }
                return cypher;
            }

            //throw new NotImplementedException();
        }


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }

    }
}
