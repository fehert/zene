using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace zenelista
{
    class Program
    {
        static void Main(string[] args)
        {     

            //Beolvasás
            string[] beolvasas = File.ReadAllLines("zenek.txt");
            List<string> zenek = new List<string>();
            for (int i = 0; i < beolvasas.Length; i++)
            {
                zenek.Add(beolvasas[i]);
            }
            if (zenek.Count == 0)//ha nincs benne egy zene se hozzá adja ezt az egyet
            {
                zenek.Add("Zámbó Jimmy\tEgy jó asszony mindent megbocsájt\t1");
            }
            

            while (true)
            {
                //menüpontok kiíratása
                Console.WriteLine("1.Új zene hozzáadás");
                Console.WriteLine("2.Top 10");
                char valasz = Console.ReadKey().KeyChar;
                Console.Clear();

           
                switch (valasz)
                {
                    case '1':
                        //új szavazatok hozzáaadása
                        for (int i = 0; i < 10; i++)
                        {
                            
                            Console.WriteLine("Adja meg a {0}. zene címét",i+1);
                            string zene = Console.ReadLine();

                            Console.WriteLine("Adja meg a {0}. zene szerzőjét", i + 1);
                            string eloado = Console.ReadLine();

                            for (int j = 0; j < zenek.Count; j++)
                            {
                                
                                int szamlalo = int.Parse(zenek[j].Split('\t')[2]);//szvazat számlálás


                                if (zenek[j].Contains(zene + '\t' + eloado))//ha van ilyen zene hozzáad 1-et a szavazathoz
                                {
                                    zenek.RemoveAt(j);
                                    zenek.Add(zene + '\t' + eloado + '\t' + (szamlalo + 1));
                                    break;
                                }


                                if (j == zenek.Count - 1)//ha nincs ilyen még akkor beleírja 
                                {
                                    szamlalo = 0;
                                    zenek.Add(zene + '\t' + eloado + '\t' + (szamlalo));
                                }
                            }
                            Console.Clear();
                        }

                        StreamWriter ujfajl = new StreamWriter("zenek.txt");//összes zene fájlba írása
                        var rendezettLista = zenek.OrderByDescending(zene => int.Parse(zene.Split('\t')[2]));//rendezés csökkenő sorrendbe listába 
                        foreach (string zene in rendezettLista)//fájlba írja a zenéket a rendezett listából
                        {
                            ujfajl.WriteLine(zene);
                        }
                        ujfajl.Close();
                        break;

                    case '2'://top 10 kiíratása
                        Console.WriteLine("Címe  Szerző  Szavazatok");
                        Console.WriteLine();
                        int hossz = 10;
                        if (zenek.Count<hossz)
                        {
                            hossz=zenek.Count;
                        }
                        for (int i = 0; i < hossz; i++)
                        {
                            Console.WriteLine(zenek[i]);
                        }

                        break;

                    default:
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}