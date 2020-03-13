using System;
using System.Linq;

namespace ConsoleTesting {
    class SerialGen {
        public static Random rng = new Random();
        public static int seed = rng.Next();
        public static int delimIteration = 5;
        public static int length = 20;
        //public static int length = (int)Math.Pow(delimIteration, 2);
        public static void Main() {
            while (true) {
                Console.Write("(g)enerate, (v)alidate: ");
                string userC = Console.ReadLine();
                if(userC == "g") {
                    Console.Write("Amount: ");
                    int l = int.Parse(Console.ReadLine());
                    for(int i = 0; i < l; i++) {
                        string serial = Generate();
                        while (true) {
                            if (serial != "null")
                                break;
                            serial = Generate();
                        }
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(i+1 + ":\t");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(serial);
                        Console.ResetColor();
                    }
                }
                if(userC == "v") {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Validate: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    string chkSerial = Console.ReadLine();
                    if (Validate(chkSerial) == "Invalid")
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Validate(chkSerial));
                    Console.ResetColor();
                }
                if (userC == "m") {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Max total: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(maxTotal());
                    Console.WriteLine();
                    Console.ResetColor();
                }
                if (userC == "n") {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("Gen total: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(maxGen());
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
        }

        // 47653
        // 2897
        const int PRIME_CHECKSUM = 47653;
        const int PRIME_DELIM = 2897;
        const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string Generate() {
            Random rand = new Random(seed++);
            
            string sOut = "";
            int nLet;
            
            int delim = 0;
            int total = 0;
            for (int i = 0; i < length; i++) {
                int x = rand.Next(0, valid.Length);
                nLet = valid[x];
                if (i % 2 != 0)
                    total += nLet + (i * (nLet * PRIME_DELIM)) + length;
                else
                    total += nLet + (i * nLet) + length;
                sOut += (char)nLet;
                delim++;
                if (delim % delimIteration == 0 && i != length - 1) {
                    sOut += '-';
                    delim = 0;
                }
            }
            if (total % PRIME_CHECKSUM == 0)
                return sOut;
            else
                return "null";
        }

        public static string Validate(string text) {
            int x = 0;
            string chk = "";
            if(text.Contains('-')) {
                for(int i = 0; i < text.Length; i++) {
                    if (text[i] != '-')
                        chk += text[i];
                }
                text = chk;
            }

            if (text.Length != length)
                return "Invalid";
            for(int i = 0; i < length; i++) {
                if (i % 2 != 0)
                    x += text[i] + (i * (text[i] * PRIME_DELIM)) + length;
                else
                    x += text[i] + (i * text[i]) + length;
            }
            if (x % PRIME_CHECKSUM == 0)
                return "Valid";
            else
                return "Invalid";
        }

        public static int maxTotal() {
            int testTotal = 0;
            for (int i = 0; i < length; i++) {
                if (i % 2 != 0)
                    testTotal += valid.Length + (i * (valid.Length * PRIME_DELIM)) + length;
                else
                    testTotal += valid.Length + (i * valid.Length) + length;
            }
            return testTotal;
        }

        public static int maxGen() {
            int genTotal = 0;
            for (int i = 1; i < maxTotal(); i++) {
                if (i % PRIME_CHECKSUM == 0)
                    genTotal++;
            }
            return genTotal;
        }
    }
}