using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooresLaw
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Introduceti un numar natural cel putin egal cu 1,\npentru a afla in cat timp se va ajunge la acea putere de procesare");
            //se folosesc blocurile try si catch pentru a prinde exceptiile legate de formatul datelor de intrare
            //daca datele de intrare introduse sunt de tip intreg, se continua executia normala a programului
            //in caz contrar, metoda int.parse va arunca o exceptie de tipul FormatException, care va fi "prinsa" de blocul catch
            try
            {
                //n reprezinta ordinul de magnitudine de crestere a puterii de procesare
                int n = int.Parse(Console.ReadLine());
                if (n >= 1)
                {

                    decimal putere = Convert.ToDecimal(Math.Log(n, 2) * 1.5);
                    //folosim functia round in afisare pentru a rotunji la 3 zecimale
                    Console.Write($"Puterea va creste de {n} ori in ");
                    Console.WriteLine("{0} ani", Decimal.Round(putere, 3));
                    //double putere = Math.Log(n, 2) * 1.5;
                    //Console.WriteLine($"Puterea va creste de {n} ori in" + " {0:0.000} ani", putere);
                    //double este mai rapid in calcule, in timp ce decimal isi gaseste aplicatii in programe financiare, datorita faptului ca are precizia dubla fata de double
                }
                else
                {
                    Console.WriteLine("Datorita datelor de intrare incorecte, programul va produce date eronate");
                    Console.WriteLine("Incercati din nou!");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Nu introduceti litere sau alte simboluri; se accepta doar numere naturale nenule");
            }
        }
    }
}
