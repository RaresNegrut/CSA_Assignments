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
        static bool OnlyInt = false;
        static void ToDec(ref string Number, bool IsBiggerThanDec, int NumberBase)
        {

            double sum = 0;
            int digit, Initial_INT_Index;
            if (Number.IndexOf('.') != -1)
                Initial_INT_Index = Number.Length - 1 - (Number.Substring(Number.IndexOf('.'))).Length;
            else
            {
                Initial_INT_Index = Number.Length - 1;
                OnlyInt = true;
            }
            //Initial_INT_Index is used to compute and store the powers necessary to convert from the original base to decimal
            if (IsBiggerThanDec)
                for (int i = 0; i < Number.Length; i++)
                {
                    switch (Number[i])
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
                            digit = Number[i] - '0';
                            break;
                    }

                    sum = sum + (double)digit * (Pow(NumberBase, Initial_INT_Index));


                    if (Number[i] != '.')
                        Initial_INT_Index--;
                }
            else
                for (int i = 0; i < Number.Length; i++)
                {
                    if (Number[i] != '.')
                        digit = Number[i] - '0';
                    else
                        digit = 0;

                    sum = sum + (double)digit * (Pow(NumberBase, Initial_INT_Index));

                    if (Number[i] != '.')
                        Initial_INT_Index--;
                }
            Number = Convert.ToString(sum);
        }
        //this function converts from base ten to any base(between binary and hexadecimal)
        static void ToBase(ref string Number, ref string IntegerPart, ref string FractionalPart, int NewBase, bool IsNewBiggerThanDec)
        {
            //this function converts numbers from the decimal numerical system to the desired numerical system
            int index;
            if (Number.IndexOf('.') != -1)
                index = Number.IndexOf('.');
            else
                index = Number.Length - 1;
            if (OnlyInt == false)
                StringSeparation(Number, ref IntegerPart, ref FractionalPart, index);
            else
                IntegerPart = Number;
            int.TryParse(IntegerPart, out int TempInt);
            IntegerPart = "";

            //if our integer part is 0, we need to concatenate 0 to the string which holds our integer part's digits
            if (TempInt == 0)
                IntegerPart += '0';

            //in this stack we'll keep the remainders needed for the integer part of the number
            //making it easier for us to access them in a specific order
            Stack<int> NewIntDigits = new Stack<int>();

            while (TempInt != 0)
            {
                NewIntDigits.Push(TempInt % NewBase);
                TempInt /= NewBase;
            }

            while (NewIntDigits.Count != 0)
            {
                int top = NewIntDigits.Pop();

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

            }


            if (OnlyInt == false)
            {
                int i = 0;

                double TempFract = double.Parse("0." + FractionalPart);

                Queue<int> NewFracDigits = new Queue<int>();

                while ((TempFract - (long)TempFract) != 0 && i <= 5)//i could implement a way of asking the user how many digits are wanted for the fractional part(if the number is not an integer)
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

                }
            }
            else
                FractionalPart = "";



        }

        static void StringSeparation(string Number, ref string IntegerPart, ref string FractionalPart, int index)
        {
            IntegerPart = Number.Substring(0, index);
            if (Number.IndexOf('.') != -1)
                FractionalPart = Number.Substring(index + 1);
            else
                FractionalPart = "";
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

                bool IsNewBiggerThanDec = false;

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
                ToBase(ref Number, ref IntegerPart, ref FractionalPart, NewBase, IsNewBiggerThanDec);

                if (OnlyInt == true)
                    Number = IntegerPart;
                else
                    Number = IntegerPart + '.' + FractionalPart;

                Console.WriteLine(Number);
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }
            finally
            {
                Console.ReadKey();
            }

        }
    }
}
