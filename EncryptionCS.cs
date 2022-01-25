using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Security.Cryptography;

namespace EncryptionCS
{
    class Encryption
    {
        const char _block = '■';
        const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
        const string _twirl = "-\\|/";
  
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to my lame string encryptor!\n");
            sleep();
            Console.Clear();
            Console.WriteLine("All you have to do, is type any form of string!\n");
            sleep();
            Console.Clear();
            Console.WriteLine("Once you've done that, you let the program work its magic.\n");
            sleep();
            Console.Clear();
            Console.WriteLine("All that aside, have fun! :)\n");
            sleep();
            Console.Clear();
            Console.WriteLine("Enter a string to encrypt:");
            Console.ResetColor();
            var entered_string = Console.ReadLine();

            var encrypted_string = Encrypt(entered_string);
            var decrypted_string = Decrypt(encrypted_string);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Clear();
            Console.WriteLine("Encrypting: " + entered_string);
            Encryption.WriteProgressBar(0);
            for (var i = 0; i <= 100; ++i)
            {
                Encryption.WriteProgressBar(i, true);
                Thread.Sleep(78);
            }
            Console.WriteLine("\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Encryption Result: " + encrypted_string);
            Thread.Sleep(1000);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hit 'enter' to see your decrypted string!");
            Console.ReadLine();
            Thread.Sleep(500);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Decrypting: " + encrypted_string);
            Encryption.WriteProgressBar(0);
            for (var i = 0; i <= 100; ++i)
            {
                Encryption.WriteProgressBar(i, true);
                Thread.Sleep(67);
            }
            Console.WriteLine("\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Decrypted text: " + decrypted_string);
            sleep();
            Console.WriteLine("\n");
            Console.WriteLine("Thank you for using my encryption service! I hope you had fun! Hit 'enter' to end the program.");
            Console.ReadLine();
        }

        private const string EncryptionKey = "Algebra 2B";

        public static string Encrypt(string PlainText)
        {
            byte[] encryptedlib = UTF8Encoding.UTF8.GetBytes(PlainText);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] EncryptionKeyLibrary = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(EncryptionKey));

            md5.Clear();

            var tripleD = new TripleDESCryptoServiceProvider();
            tripleD.Key = EncryptionKeyLibrary;
            tripleD.Mode = CipherMode.ECB;
            tripleD.Padding = PaddingMode.PKCS7;

            var transform = tripleD.CreateEncryptor();
            byte[] results = transform.TransformFinalBlock(encryptedlib, 0, encryptedlib.Length);
            tripleD.Clear();

            return Convert.ToBase64String(results, 0, results.Length);
        }

        public static string Decrypt(string CipherText)
        {
            byte[] encryptedlib2 = Convert.FromBase64String(CipherText);
            MD5CryptoServiceProvider md52 = new MD5CryptoServiceProvider();

            byte[] EncryptionKeyLibrary = md52.ComputeHash(UTF8Encoding.UTF8.GetBytes(EncryptionKey));
            md52.Clear();

            var tripleD2 = new TripleDESCryptoServiceProvider();

            tripleD2.Key = EncryptionKeyLibrary;
            tripleD2.Mode = CipherMode.ECB;
            tripleD2.Padding = PaddingMode.PKCS7;

            var transform2 = tripleD2.CreateDecryptor();
            byte[] results2 = transform2.TransformFinalBlock(encryptedlib2, 0, encryptedlib2.Length);

            tripleD2.Clear();

            return UTF8Encoding.UTF8.GetString(results2);
        }

        public static void sleep()
        {
            Thread.Sleep(2000);
        }

        public static void WriteProgressBar(int percent, bool update = false)
        {
            if (update)
            {
                Console.Write(_back);
            }
            Console.Write("[");
            var p = (int)((percent / 10f) + .5f);
            for (var i = 0; i < 10; ++i)
            {
                if (i >= p)
                {
                    Console.Write(' ');
                }
                else
                {
                    Console.Write(_block);
                }
            }
            Console.Write("] {0,3:##0}%", percent);
        }
        public static void WriteProgress(int progress, bool update = false)
        {
            if (update)
            {
                Console.Write("\b");
            }
            Console.Write(_twirl[progress % _twirl.Length]);
        }
    }
}
