using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographic_Technique<string, string>
    {
        public string Decrypt(string cipherText, string key)
        {
            //throw new NotImplementedException();

            key = key.ToLower().Replace("j", "i");
            cipherText = cipherText.ToLower().Replace("j", "i");
            string plainText = "";

            List<char> chars = new List<char>();
            List<char> matrixList = new List<char>();
            char[,] mat = new char[5, 5];


            for (char c = 'a'; c <= 'z'; c++)
            {
                if (c != 'j') chars.Add(c);
            }

            foreach (char c in key)
            {
                if (chars.Contains(c))
                {
                    chars.Remove(c);
                    matrixList.Add(c);
                }
            }
            matrixList.AddRange(chars);

            int index = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    mat[i, j] = matrixList[index++];
                }
            }


            List<string> pairs = new List<string>();
            for (int i = 0; i < cipherText.Length; i += 2)
            {
                char first = cipherText[i];
                char second = (i + 1 < cipherText.Length) ? cipherText[i + 1] : 'x';
                pairs.Add(first.ToString() + second.ToString());
            }

            foreach (string pair in pairs)
            {
                (int i1, int j1) = FindPosition(mat, pair[0]);
                (int i2, int j2) = FindPosition(mat, pair[1]);

                if (i1 == i2)
                {
                    plainText += mat[i1, (j1 - 1 + 5) % 5];
                    plainText += mat[i2, (j2 - 1 + 5) % 5];
                }
                else if (j1 == j2)
                {
                    plainText += mat[(i1 - 1 + 5) % 5, j1];
                    plainText += mat[(i2 - 1 + 5) % 5, j2];
                }
                else
                {
                    plainText += mat[i1, j2];
                    plainText += mat[i2, j1];
                }
            }

            if (plainText.EndsWith("x"))
            {
                plainText = plainText.Remove(plainText.Length - 1);
            }

            string cleanedPlainText = plainText[0].ToString();

            for (int i = 1; i < plainText.Length; i++)
            {
                if (i < plainText.Length - 1 && plainText[i] == 'x' &&
                    plainText[i - 1] == plainText[i + 1] && i % 2 != 0)
                {
                    continue;
                }
                cleanedPlainText += plainText[i];
            }

            plainText = cleanedPlainText;
            return plainText;


        }





        private (int, int) FindPosition(char[,] mat, char c)
        {
            if (c == 'j') c = 'i';
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (mat[i, j] == c)
                    {
                        return (i, j);
                    }
                }
            }
            return (-1, -1);
        }

        public string Encrypt(string plainText, string key)
        {
            string cipherText = "";
            key = key.ToLower().Replace("j", "i");
            plainText = plainText.ToLower().Replace("j", "i");
            List<char> chars = new List<char>();
            List<char> matrixList = new List<char>();
            char[,] mat = new char[5, 5];


            for (char c = 'a'; c <= 'z'; c++)
            {
                if (c != 'j') chars.Add(c);
            }

            foreach (char c in key)
            {
                if (chars.Contains(c))
                {
                    chars.Remove(c);
                    matrixList.Add(c);
                }
            }
            matrixList.AddRange(chars);

            int index = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    mat[i, j] = matrixList[index++];
                }
            }

            List<string> pairs = new List<string>();
            for (int i = 0; i < plainText.Length; i++)
            {
                char first = plainText[i];
                char second = (i + 1 < plainText.Length && plainText[i] != plainText[i + 1]) ? plainText[++i] : 'x';
                pairs.Add(first.ToString() + second.ToString());
            }

            foreach (string pair in pairs)
            {
                (int i1, int j1) = FindPosition(mat, pair[0]);
                (int i2, int j2) = FindPosition(mat, pair[1]);

                if (i1 == i2)
                {
                    cipherText += mat[i1, (j1 + 1) % 5];
                    cipherText += mat[i2, (j2 + 1) % 5];
                }
                else if (j1 == j2)
                {
                    cipherText += mat[(i1 + 1) % 5, j1];
                    cipherText += mat[(i2 + 1) % 5, j2];
                }
                else
                {
                    cipherText += mat[i1, j2];
                    cipherText += mat[i2, j1];
                }
            }
            return cipherText;
            //throw new NotImplementedException();
        }

    }
}