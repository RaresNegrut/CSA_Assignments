using System;
using System.Collections.Generic;
using static System.Math;



namespace Base_Conversion
{
    class Program
    {
        enum Multidecimals
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
        static void ToDec(ref string shrek, bool IsBiggerThanDec, int NumberBase)
        {
            double sum = 0;
            int digit, Initial_INT_Index = shrek.Length - 1 - (shrek.Substring(shrek.IndexOf('.'))).Length;
            //Initial_INT_Index is used to compute and store the powers necessary to convert from the original base to decimal
            if (IsBiggerThanDec)
                for (int i = 0; i < shrek.Length; i++)
                {
                    switch (shrek[i])
                    {
                        case 'A':
                            digit = (int)Multidecimals.A;
                            break;

                        case 'B':
                            digit = (int)Multidecimals.B;
                            break;
                        case 'C':
                            digit = (int)Multidecimals.C;
                            break;
                        case 'D':
                            digit = (int)Multidecimals.D;
                            break;
                        case 'E':
                            digit = (int)Multidecimals.E;
                            break;
                        case 'F':
                            digit = (int)Multidecimals.F;
                            break;
                        case '.':
                            digit = 0;
                            break;
                        default:
                            digit = shrek[i] - '0';
                            break;
                    }

                    sum = sum + (double)digit * (Pow(NumberBase, Initial_INT_Index));


                    if (shrek[i] != '.')
                        Initial_INT_Index--;
                }
            else
                for (int i = 0; i < shrek.Length; i++)
                {
                    if (shrek[i] != '.')
                        digit = shrek[i] - '0';
                    else
                        digit = 0;

                    sum = sum + (double)digit * (Pow(NumberBase, Initial_INT_Index));

                    if (shrek[i] != '.')
                        Initial_INT_Index--;
                }
            shrek = Convert.ToString(sum);
        }
        static void ToBase(ref string Number, ref string IntegerPart, ref string FractionalPart, int NewBase, bool IsNewBiggerThanDec)
        {
            //this function converts numbers from the decimal numerical system to the desired numerical system
            int index = Number.IndexOf('.');
            StringSeparation(Number, ref IntegerPart, ref FractionalPart, index);

            int.TryParse(IntegerPart, out int TempInt);
            IntegerPart = "";
            Stack<int> NewIntDigits = new Stack<int>();

            while (TempInt != 0)
            {
                NewIntDigits.Push(TempInt % NewBase);
                TempInt /= NewBase;
            }

            while (NewIntDigits.Count != 0)
            {
                int top = NewIntDigits.Pop();
                //if (IsNewBiggerThanDec == true)
                //{
                switch (top)
                {
                    case 10:
                        IntegerPart += "A";
                        break;
                    case 11:
                        IntegerPart += "B";
                        break;
                    case 12:
                        IntegerPart += "C";
                        break;
                    case 13:
                        IntegerPart += "D";
                        break;
                    case 14:
                        IntegerPart += "E";
                        break;
                    case 15:
                        IntegerPart += "F";
                        break;
                    default:
                        IntegerPart += top.ToString();
                        break;

                }
                //}
                //else
                //IntegerPart += top.ToString();
            }


            int need = FractionalPart.Length;
            int i = 0;

            double TempFract = double.Parse("0." + FractionalPart);

            Queue<int> NewFracDigits = new Queue<int>();

            while ((TempFract - (long)TempFract) != 0 && i <= need)
            {
                TempFract *= NewBase;
                NewFracDigits.Enqueue((int)TempFract);
                TempFract = TempFract - (long)TempFract;
                i++;
            }

            FractionalPart = "";


            while (NewFracDigits.Count != 0)
            {
                int head = NewFracDigits.Dequeue();
                //if (IsNewBiggerThanDec == true)
                //{
                switch (head)
                {
                    case 10:
                        FractionalPart += "A";
                        break;
                    case 11:
                        FractionalPart += "B";
                        break;
                    case 12:
                        FractionalPart += "C";
                        break;
                    case 13:
                        FractionalPart += "D";
                        break;
                    case 14:
                        FractionalPart += "E";
                        break;
                    case 15:
                        FractionalPart += "F";
                        break;
                    default:
                        FractionalPart += head.ToString();
                        break;
                }
                //}
                //else
                //{

                //}
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

                bool IsNewBiggerThanDec=false;

                if (NewBase > 10)
                    IsNewBiggerThanDec = true;
                //in these two strings we will store the integer part and the fractional part of our number

                string IntegerPart = "", FractionalPart = "";

                bool IsBiggerThanDec = false;
                foreach (char c in Number)
                {
                    if ((char.IsLetter(c) && c >= 'A' && c <= 'A' + NumberBase - 10) || NumberBase > 10)
                        IsBiggerThanDec = true;
                    else
                        if (((c < 'A' || c > 'A' + NumberBase - 10) && char.IsLetter(c)))//TODO: Check for other symbols
                        throw new Exception("Your input contains forbidden characters; please give a valid input");
                }
                ToDec(ref Number, IsBiggerThanDec, NumberBase);
                ToBase(ref Number, ref IntegerPart, ref FractionalPart,NewBase, IsNewBiggerThanDec);
                Number = IntegerPart+ '.' + FractionalPart;
                Console.WriteLine(Number);
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }

        }
    }
}
