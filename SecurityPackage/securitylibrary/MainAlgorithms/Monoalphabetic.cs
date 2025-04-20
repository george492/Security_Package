using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace SecurityLibrary
{
    public class Monoalphabetic : ICryptographicTechnique<string, string>
    {
        public string Analyse(string plainText, string cipherText)
        {
            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            char[] chars = new char[26];
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            alpha = alpha.ToUpper();
            int length = plainText.Length;
            for (int i = 0; i < length; i++)
            {
                int temp = (int)(plainText[i]);
                temp -= 65;
                chars[temp] = cipherText[i];
                alpha = alpha.Replace(cipherText[i].ToString(), "");
            }
            int x = 0;
            string key = "";
            for (int i = 0; i < 26; i++)
            {
                if (chars[i] == '\0')
                {

                    key += alpha[x];
                    x++;
                }
                else
                    key += chars[i];
            }
            return key.ToLower();
            throw new NotImplementedException();
        }

        public string Decrypt(string cipherText, string key)
        {
            key = key.ToUpper();
            string plainText = "";
            int length = cipherText.Length;
            for (int i = 0; i < length; i++)
            {
                int index = key.IndexOf(cipherText[i]);
                index += 65;
                plainText += (char)(index);

            }
            return plainText;
            //throw new NotImplementedException();
        }

        public string Encrypt(string plainText, string key)
        {
            plainText = plainText.ToUpper();
            string cipherText = "";
            int length = plainText.Length;
            for (int i = 0; i < length; i++)
            {
                int temp = (int)(plainText[i]);
                temp -= 65;
                char c = key[temp];
                cipherText += c;


            }
            return cipherText;
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Frequency Information:
        /// E   12.51%
        /// T	9.25
        /// A	8.04
        /// O	7.60
        /// I	7.26
        /// N	7.09
        /// S	6.54
        /// R	6.12
        /// H	5.49
        /// L	4.14
        /// D	3.99
        /// C	3.06
        /// U	2.71
        /// M	2.53
        /// F	2.30
        /// P	2.00
        /// G	1.96
        /// W	1.92
        /// Y	1.73
        /// B	1.54
        /// V	0.99
        /// K	0.67
        /// X	0.19
        /// J	0.16
        /// Q	0.11
        /// Z	0.09
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns>Plain text</returns>
        public string AnalyseUsingCharFrequency(string cipher)
        {
            cipher = cipher.ToUpper();
            string key = "";
            Dictionary<char, double> freq_alpha = new Dictionary<char, double>();
            bool[] checkarr = new bool[27];
            double[] arr = new double[27];
            char[] temparr = new char[26];
            int length = cipher.Length;
            for (int i = 0; i < length; i++)
            {
                int temp = (int)(cipher[i]);
                temp -= 65;
                if (!checkarr[temp])
                {

                    freq_alpha[cipher[i]] = 0;
                    int index = cipher.IndexOf(cipher[i]);
                    while (index != -1)
                    {
                        freq_alpha[cipher[i]]++;
                        // Console.WriteLine(  freqarr[temp]);
                        index = cipher.IndexOf(cipher[i], index + 1);
                    }

                    freq_alpha[cipher[i]] = freq_alpha[cipher[i]] / length;
                    freq_alpha[cipher[i]] *= 100;
                    checkarr[temp] = true;
                }
            }
            freq_alpha = freq_alpha.OrderByDescending(pair => pair.Value)
             .ToDictionary(pair => pair.Key, pair => pair.Value);
            int k = 0;
            string morefreq = "ETAOINSRHLDCUMFPGWYBVKXJQZ";
            //string alphaP = "abcdefghijklmnopqrstuvwxyz";
            // foreach (var pair in freq_alpha)
            //     {
            //         Console.WriteLine(pair.Key + ": " + pair.Value);
            //     }
            foreach (var pair in freq_alpha)
            {
                int x = (int)(morefreq[k]);
                x -= 65;
                temparr[x] = pair.Key;
                // alphaP = alphaP.Replace(pair.Key.ToString(), "");
                k++;

            }
            int y = freq_alpha.Count;
            for (int i = 0; i < 26; i++)
            {
                if (temparr[i] == '\0')
                {

                    key += morefreq[y];
                    y++;
                }
                else
                    key += temparr[i];
            }
            string plain = Decrypt(cipher, key);
            return plain.ToLower();
            // throw new NotImplementedException();
        }

    }
}