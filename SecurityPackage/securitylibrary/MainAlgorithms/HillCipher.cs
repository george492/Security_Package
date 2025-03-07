using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>

    public class HillCipher : ICryptographicTechnique<List<int>, List<int>>
    {
        /// [0][1] => [3]-[1]
        /// [2][3]    -[2][0]
        /// 
        private static double Determinant(List<int> matrix, int size)
        {
            if (size == 2)
            {
                return matrix[0] * matrix[3] - matrix[1] * matrix[2];
            }
            else if (size == 3)
            {
                return matrix[0] * (matrix[4] * matrix[8] - matrix[5] * matrix[7])
                     - matrix[1] * (matrix[3] * matrix[8] - matrix[5] * matrix[6])
                     + matrix[2] * (matrix[3] * matrix[7] - matrix[4] * matrix[6]);
            }
            return 0;
        }


        private static int ModInverse(int a, int mod)
        {
            a = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((a * x) % mod == 1)
                    return x;
            }
            return -1;
        }
        private static List<int> MultiplyMatrix(List<int> matrix, List<int> text, int size, int mod)
        {
            List<int> result = new List<int>(new int[text.Count]);
            for (int i = 0; i < text.Count; i += size)
            {
                for (int row = 0; row < size; row++)
                {
                    double sum = 0;
                    for (int col = 0; col < size; col++)
                    {
                        sum += (double)matrix[row * size + col] * text[i + col];
                    }
                    result[i + row] = (int)((sum % mod + mod) % mod);
                }
            }
            return result;
        }


        public static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Math.Abs(a);
        }
        public List<int> Reverse2(List<int> d)
        {
            List<int> reversed = new List<int>();

            int det = (d[0] * d[3] - d[1] * d[2]) % 26;
            det = modn(det, 26);  // Ensure positive mod

            if (det == 0 || GCD(det, 26) != 1)
                throw new InvalidOperationException("Matrix is not invertible in mod 26.");

            int invDet = ModInverse(det, 26);

            reversed.Add((d[3] * invDet) % 26);
            reversed.Add((-d[1] * invDet) % 26);
            reversed.Add((-d[2] * invDet) % 26);
            reversed.Add((d[0] * invDet) % 26);

            for (int i = 0; i < reversed.Count; i++)
            {
                reversed[i] = modn(reversed[i], 26);
            }

            return reversed;
        }

        private static List<int> InverseMatrix(List<int> key, int size, int mod)
        {
            double det = Determinant(key, size) % mod;
            if (det < 0) det += mod;

            int detInverse = ModInverse((int)det, mod);
            if (detInverse == -1) return null;

            List<double> adjugate = AdjugateMatrix(key, size, mod);
            if (adjugate == null) return null;

            return adjugate.Select(x => (int)(((x * detInverse) % mod + mod) % mod)).ToList();
        }
        int modn(int x, int m)
        {
            return (x % m + m) % m;
        }

        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {




            for (int i = 0; i < 26; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    for (int k = 0; k < 26; k++)
                    {
                        for (int l = 0; l < 26; l++)
                        {
                            List<int> key = new List<int>();
                            key.Add(i);
                            key.Add(j);
                            key.Add(k);
                            key.Add(l);
                            List<int> res = Encrypt(plainText, key);
                            if (res.SequenceEqual(cipherText))
                            {
                                return key;
                            }
                        }
                    }
                }
            }

            int det = plainText[0] * plainText[3] - plainText[1] * plainText[2];
            det = modn(det, 26);
            if (det == 0 || GCD(det, 26) != 1)
                throw new InvalidAnlysisException();

            throw new NotImplementedException();
        }


        private static double Determinant(int[,] matrix)
        {
            return matrix[0, 0] * (matrix[1, 1] * matrix[2, 2] - matrix[1, 2] * matrix[2, 1])
                 - matrix[0, 1] * (matrix[1, 0] * matrix[2, 2] - matrix[1, 2] * matrix[2, 0])
                 + matrix[0, 2] * (matrix[1, 0] * matrix[2, 1] - matrix[1, 1] * matrix[2, 0]);
        }

        private static List<double> AdjugateMatrix(List<int> matrix, int size, int mod)
        {
            if (size == 2)
            {
                return new List<double> { matrix[3], -matrix[1], -matrix[2], matrix[0] }
                    .Select(x => ((x % mod + mod) % mod)).ToList();
            }
            else if (size == 3)
            {
                List<double> adjugate = new List<double>(new double[9]);
                adjugate[0] = matrix[4] * matrix[8] - matrix[5] * matrix[7];
                adjugate[1] = -(matrix[3] * matrix[8] - matrix[5] * matrix[6]);
                adjugate[2] = matrix[3] * matrix[7] - matrix[4] * matrix[6];
                adjugate[3] = -(matrix[1] * matrix[8] - matrix[2] * matrix[7]);
                adjugate[4] = matrix[0] * matrix[8] - matrix[2] * matrix[6];
                adjugate[5] = -(matrix[0] * matrix[7] - matrix[1] * matrix[6]);
                adjugate[6] = matrix[1] * matrix[5] - matrix[2] * matrix[4];
                adjugate[7] = -(matrix[0] * matrix[5] - matrix[2] * matrix[3]);
                adjugate[8] = matrix[0] * matrix[4] - matrix[1] * matrix[3];

                return adjugate.Select(x => ((x % mod + mod) % mod)).ToList();
            }
            return null;
        }

        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            List<int> plain = new List<int>();
            int mod = 26;


            if (key.Count() == 4)
            {
                int det = key[0] * key[3] - key[1] * key[2];
                if (det == 0)
                {
                    throw new NotImplementedException();
                }

                List<int> reversed = Reverse2(key);

                if (cipherText.Count() % 2 != 0)
                {
                    cipherText.Add(0);
                }

                int a;
                for (int i = 0; i < cipherText.Count() - 1; i += 2)
                {
                    a = (reversed[0] * cipherText[i] + reversed[1] * cipherText[i + 1]) % mod;
                    a = modn(a, mod);
                    plain.Add(a);

                    a = (reversed[2] * cipherText[i] + reversed[3] * cipherText[i + 1]) % mod;
                    a = modn(a, mod);
                    plain.Add(a);
                }
                return plain;
            }
            else if (key.Count() == 9)
            {
                int det = key[0] * (key[4] * key[8] - key[5] * key[7]) - key[1] * (key[3] * key[8] - key[5] * key[6]) + key[2] * (key[3] * key[7] - key[4] * key[6]);

                if (det == 0 || GCD(det, 26) != 1)
                {
                    throw new InvalidOperationException("Matrix is not invertible in mod 26.");
                }

                List<int> inverseKey = InverseMatrix(key, 3, mod);
                if (inverseKey == null)
                {
                    throw new Exception("Key matrix is not invertible.");
                }

                // Decrypt by multiplying inverse key with ciphertext
                return MultiplyMatrix(inverseKey, cipherText, 3, mod);
            }

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
                int a;
                for (int i = 0; i < plainText.Count() - 1; i += 2)
                {
                    a = (key[0] * plainText[i] + key[1] * plainText[i + 1]) % mod;
                    a = modn(a, mod);
                    cypher.Add(a);

                    a = (key[2] * plainText[i] + key[3] * plainText[i + 1]) % mod;
                    a = modn(a, mod);
                    cypher.Add(a);
                }
                return cypher;

            }
            else if (key.Count() == 9)
            {
                int det = key[0] * (key[4] * key[8] - key[5] * key[7]) - key[1] * (key[3] * key[8] - key[5] * key[6]) + key[2] * (key[3] * key[7] - key[4] * key[6]);
                det = modn(det, mod);
                if (det == 0)
                {
                    throw new NotImplementedException();
                }

                if (plainText.Count() % 3 == 1)
                {
                    plainText.Add(0);
                    plainText.Add(0);
                }
                else if (plainText.Count() % 3 == 2)
                {
                    plainText.Add(0);
                }
                int a;
                for (int i = 0; i < plainText.Count() - 2; i += 3)
                {
                    a = (key[0] * plainText[i] + key[1] * plainText[i + 1] + key[2] * plainText[i + 2]) % mod;
                    a = modn(a, mod);
                    cypher.Add(a);
                    a = (key[3] * plainText[i] + key[4] * plainText[i + 1] + key[5] * plainText[i + 2]) % mod;
                    a = modn(a, mod);
                    cypher.Add(a);
                    a = (key[6] * plainText[i] + key[7] * plainText[i + 1] + key[8] * plainText[i + 2]) % mod;
                    a = modn(a, mod);
                    cypher.Add(a);
                }
                return cypher;
            }
            throw new NotImplementedException();
        }


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }

    }
}
