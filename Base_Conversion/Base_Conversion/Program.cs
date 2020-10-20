using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;



namespace Base_Conversion
{
    class Program
    {
        enum Hextenz
        {
            A = 10,
            B,
            C,
            D,
            E,
            F
        }
        //i need a way of storing the power to which the base is raised to, in regards
        //to the position of my element in the string
        //for now, due to time constraints, it will have to wait
        static void ToDec(ref string shrek, bool IsHex, int NumberBase)
        {
            double sum = 0;
            int digit;
            if (IsHex)
                for (int i = 0; i < shrek.Length; i++)
                {
                    switch (shrek[i])
                    {
                        case 'A':
                            digit = (int)Hextenz.A;
                            break;

                        case 'B':
                            digit = (int)Hextenz.B;
                            break;
                        case 'C':
                            digit = (int)Hextenz.C;
                            break;
                        case 'D':
                            digit = (int)Hextenz.D;
                            break;
                        case 'E':
                            digit = (int)Hextenz.E;
                            break;
                        case 'F':
                            digit = (int)Hextenz.F;
                            break;
                        case '.':
                            digit = 0;
                            break;
                        default:
                            digit = shrek[i] - '0';
                            break;
                    }
                    if(i<shrek.IndexOf('.'))
                    sum = sum + (double)digit * (Pow(NumberBase, shrek.Length - 2 - i - shrek.IndexOf('.')));
                    else
                        sum = sum + (double)digit * (Pow(NumberBase, shrek.Length - 2 - i));
                }
            shrek = Convert.ToString(sum);
        }
        static void ToBase(ref string Number,ref string IntegerPart,ref string FractionalPart,int NewBase,bool IsNewHex)
        {
            //this function converts numbers from the decimal numerical system to the desired numerical system
            int index = Number.IndexOf('.');
            StringSeparation(Number, ref IntegerPart,ref FractionalPart, index);

            int.TryParse(IntegerPart,out int TempInt);
            IntegerPart = "";
            Stack<int> NewIntDigits = new Stack<int>();

            while (TempInt!=0)
            {
                NewIntDigits.Push(TempInt%NewBase);
                TempInt /= NewBase;
            }

            while(NewIntDigits.Count!=0)
            {
                IntegerPart += NewIntDigits.Pop().ToString();
            }


            int need = FractionalPart.Length;
            int i = 0;
            double TempFract = double.Parse("0." + FractionalPart);
            Queue<int> NewFracDigits = new Queue<int>();
            while((TempFract-(long)TempFract)!=0 ||i<=need)
            {
                TempFract *= NewBase;
                NewFracDigits.Enqueue((int)TempFract);
                TempFract = TempFract - (long)TempFract;
            }

            FractionalPart = "";
            int digit;


            while(NewFracDigits.Count!=0)
            {
                if(IsNewHex==true)
                {
                    //switch(NewFracDigits.Peek())
                    //{
                    //    case 'A':
                    //        digit = (int)Hextenz.A;
                    //        break;

                    //    case 'B':
                    //        digit = (int)Hextenz.B;
                    //        break;
                    //    case 'C':
                    //        digit = (int)Hextenz.C;
                    //        break;
                    //    case 'D':
                    //        digit = (int)Hextenz.D;
                    //        break;
                    //    case 'E':
                    //        digit = (int)Hextenz.E;
                    //        break;
                    //    case 'F':
                    //        digit = (int)Hextenz.F;
                    //        break;
                    //    case '.':
                    //        digit = 0;
                    //        break;
                    //    default:
                    //        digit = NewFracDigits.Dequeue();
                    //        break;
                    //}
                }
                else
                {

                }
            }




        }
        static void StringSeparation(string Number, ref string IntegerPart, ref string FractionalPart, int index)
        {
            FractionalPart = Number.Substring(index + 1);
            IntegerPart = Number.Substring(0, index);
        }

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter a number you want to convert, then press enter");
                string Number = Console.ReadLine().ToUpper();

                Console.WriteLine("Now enter the base of the number you want to convert, then press enter");
                int NumberBase = int.Parse(Console.ReadLine());

                Console.WriteLine("We will also need the base you want the number to be converted to");
                int NewBase = int.Parse(Console.ReadLine());
                bool IsNewHex;
                if (NewBase == 16)
                    IsNewHex = true;
                //in these two strings we will store the integer part and the fractional part of our number
                string IntegerPart = "", FractionalPart = "";

                bool IsHex = false;
                foreach (var (c, index) in Number.Select((value, i) => (value, i)))
                {
                    if ((char.IsLetter(c) && c >= 'A' && c <= 'F') || NumberBase == 16)
                        IsHex = true;
                    else
                        if (((c < 'A' || c > 'F') && char.IsLetter(c)))//TODO: Check for other symbols
                        throw new Exception("Your input contains forbidden characters; please give a valid input");
                    //if (c == '.' || c == ',')
                        //StringSeparation(Number, ref IntegerPart, ref FractionalPart, index);
                    //i think i'll do this bit in the ToBase function
                }
                ToDec(ref Number, IsHex, NumberBase);
                Console.WriteLine(Number);
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }

        }
    }
}
