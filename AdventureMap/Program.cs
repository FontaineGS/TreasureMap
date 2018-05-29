using System;
using System.Threading;
using TreasureMap;

namespace AdventureMap
{
    class Program
    {
        static void Main(string[] args)
        {
            //args
            int nbArgs = args.Length;
            string source = ""; ;
            string result = @"./result.txt";
            if (nbArgs <= 0)
            {
                Console.WriteLine("Erreur : fichier d'entrée non spécifié");
            }
            if (nbArgs >= 1)
            {
                source = args[0];
                Console.WriteLine("sourceok");
            }
            if (nbArgs >= 2)
            {
                result = args[1];
                Console.WriteLine("result ok");
            }

            Console.WriteLine("Hello World" + nbArgs);
            AdventureContext a = new AdventureContext();
            a.Load(source);
            a.Compute();
            a.Write(result);
        }
    }
}
