using System;
using System.Collections.Generic;

namespace PasswordPhilosophy
{
    class Program
    {
        private static List<Password> _passwords;
        static void Main(string[] args)
        {
            _passwords = ProcessInputFile();
        }
    }

    public class Password
    {
        public int PolicyMinimal { get; set; }
        public int PolicyMaximum { get; set; }
        public string PolicyCharachter { get; set; }
        public string Value { get; set; }
    }
}
