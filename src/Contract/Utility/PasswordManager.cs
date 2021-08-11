using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace ZohoToInsightIntegrator.Contract.Utility
{
    public class PasswordManager
    {
        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }

            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }

            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }

            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }

            return ByteArraysEqual(buffer3, buffer4);
        }

        [MethodImpl(MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= a[i] == b[i];
            }

            return areSame;
        }

        public static string GeneratePassword(int lenght, int nonAlphaNumericChars)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            string allowedNonAlphaNum = "123tuvwxyzABCDEFGHJKLMN";//"!@#$%^&*()_-+=[{]};:<>|./?";
            Random rd = new Random();

            if (nonAlphaNumericChars > lenght || lenght <= 0 || nonAlphaNumericChars < 0)
                throw new ArgumentOutOfRangeException();

            char[] pass = new char[lenght];
            int[] pos = new int[lenght];
            int i = 0, j, temp;
            bool flag;

            //Random the position values of the pos array for the string Pass
            while (i < lenght - 1)
            {
                j = 0;
                flag = false;
                temp = rd.Next(0, lenght);
                for (j = 0; j < lenght; j++)
                    if (temp == pos[j])
                    {
                        flag = true;
                        j = lenght;
                    }

                if (!flag)
                {
                    pos[i] = temp;
                    i++;
                }
            }

            //Random the AlphaNumericChars
            for (i = 0; i < lenght - nonAlphaNumericChars; i++)
                pass[i] = allowedChars[rd.Next(0, allowedChars.Length)];

            //Random the NonAlphaNumericChars
            for (i = lenght - nonAlphaNumericChars; i < lenght; i++)
                pass[i] = allowedNonAlphaNum[rd.Next(0, allowedNonAlphaNum.Length)];

            //Set the sorted array values by the pos array for the rigth posistion
            char[] sorted = new char[lenght];
            for (i = 0; i < lenght; i++)
                sorted[i] = pass[pos[i]];

            string result = new string(sorted);

            return result;
        }
    }
}
