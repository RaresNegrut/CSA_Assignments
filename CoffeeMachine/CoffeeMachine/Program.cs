using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.IO;
using  static CoffeeMachine.VendingMachine.IO;


namespace CoffeeMachine
{
    class VendingMachine
    {
        private State stateRef = null;

        public VendingMachine(State state)
        {
            this.TransitionTo(state);
        }
        public void TransitionTo(State state)
        {
            Console.WriteLine($"Current state: {state.GetType().Name}");
            this.stateRef = state;
            this.stateRef.SetState(this);
            Program.RecoveryState1 = state;
            IO.GetRef(this);
            IO.input();
        }
        public void GetsNickel()
        {
            stateRef.NickelEntered();
        }
        public void GetsDime()
        {
            stateRef.DimeEntered();
        }
        public void GetsQuarter()
        {
            stateRef.QuarterEntered();
        }

        public static class IO
        {
            static private VendingMachine machineRef = null;
            public static void GetRef(VendingMachine machine)
            {
                machineRef = machine;
            }
            public static void input()
            {
                Console.WriteLine("Enter a coin: Nickel(N), Dime(D), or Quarter(Q)");
                string line= Console.ReadLine().ToUpper();
                if (line == "")
                    throw new Exception("Nothing entered, try again");
                if (line.Length > 1)
                    throw new Exception("Input must be exactly one character long");

                var coin = char.Parse(line);
                switch (coin)
                {
                    case 'N':
                        machineRef.GetsNickel();
                        break;
                    case 'D':
                        machineRef.GetsDime();
                        break;
                    case 'Q':
                        machineRef.GetsQuarter();
                        break;
                    default:
                        throw new Exception("This machine only accepts Nickels, Dimes or Quarters");
                }
            } 
            public static void DispenseMerchandise()
            {
                Console.WriteLine("Dispensed a coffee, enjoy");
                Console.ForegroundColor = ConsoleColor.Red;
                System.IO.StreamReader f = new System.IO.StreamReader(@"..\..\Resources\TextFile1.txt");
                string line;
                while ((line = f.ReadLine()) != null)
                    Console.WriteLine(line);
                Console.ResetColor();
            }
            public static void DropChange(string change)
            {
                Console.WriteLine($"Your change is a {change}");
            }
            public static void DropChange(string change1, string change2)
            {
                Console.WriteLine($"Your change is a {change1} and a {change2}");
            }
        }

    }

    abstract class State
    {
        protected VendingMachine machineRef;
        public void SetState(VendingMachine machine)
        {
            this.machineRef = machine;
        }
        public abstract void NickelEntered();
        public abstract void DimeEntered();
        public abstract void QuarterEntered();
    }
    /// <summary>
    /// <para>0 cents in my machine</para>
    /// <para>For Nickel: Goes to state B</para>
    /// <para>For Dime: Goes to state C</para>
    /// <para>For Quarter: Goes to state A; Returns a Nickel; Dispenses Merchandise</para>           
    /// </summary>
    class StateA : State
    {
        public override void NickelEntered()
        {
            machineRef.TransitionTo(new StateB());
        }

        public override void DimeEntered()
        {
            machineRef.TransitionTo(new StateC());
        }

        public override void QuarterEntered()
        {
            DropChange("Nickel");
            DispenseMerchandise();
            machineRef.TransitionTo(new StateA());
        }
    }
    /// <summary>
    /// <para>5 cents in my machine</para>
    /// <para>For Nickel: Goes to state C</para>
    /// <para>For Dime: Goes to state D</para>
    /// <para>For Quarter: Goes to state A; Returns a Dime; Dispenses Merchandise</para>  
    /// </summary>
    class StateB : State
    {

        public override void NickelEntered()
        {
            machineRef.TransitionTo(new StateC());
        }

        public override void DimeEntered()
        {
            machineRef.TransitionTo(new StateD());
        }

        public override void QuarterEntered()
        {
            DropChange("dime");
            DispenseMerchandise();
            machineRef.TransitionTo(new StateA());
        }
    }
    /// <summary>
    /// 10 cents in my machine
    /// </summary>
    class StateC : State
    {
        public override void NickelEntered()
        {
            machineRef.TransitionTo(new StateD());
        }

        public override void DimeEntered()
        {
            DispenseMerchandise();
            machineRef.TransitionTo(new StateA());
        }

        public override void QuarterEntered()
        {
            DropChange("dime", "nickel");
            DispenseMerchandise();
            //TODO: Dispense merch and return dime+nickel
            machineRef.TransitionTo(new StateA());
        }
    }
    /// <summary>
    /// 15 cents in my machine
    /// </summary>
    class StateD : State
    {
        public override void NickelEntered()
        {
            DispenseMerchandise();
            //TODO: Dispense Merch
            machineRef.TransitionTo(new StateA());
        }

        public override void DimeEntered()
        {
            DropChange("nickel");
            DispenseMerchandise();
            //TODO: Dispense merch and return a nickel
            machineRef.TransitionTo(new StateA());
        }

        public override void QuarterEntered()
        {
            DropChange("dime", "dime");
            DispenseMerchandise();
            machineRef.TransitionTo(new StateA());
        }
    }

    class Program
    {
        private static State RecoveryState=new StateA();

        internal static State RecoveryState1 { set => RecoveryState = value; }

        static void Main(string[] args)
        {
            while(true)
            {
                try
                {
                    VendingMachine proto = new VendingMachine(RecoveryState);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
