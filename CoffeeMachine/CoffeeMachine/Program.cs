using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine
{
    //TODO: BIG: IMPLEMENT A BALANCE SYSTEM AND I/O
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
                char coin = char.Parse(Console.ReadLine().ToUpper());
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
            //TODO: Return a nickel and dispense merch
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
            //TODO:Return a dime and dispense merch
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
            //TODO:Dispense merch
            machineRef.TransitionTo(new StateA());
        }

        public override void QuarterEntered()
        {
            //TODO: Dispense merch and return dime+quarter
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
            //TODO: Dispense Merch
            machineRef.TransitionTo(new StateA());
        }

        public override void DimeEntered()
        {
            //TODO: Dispense merch and return a nickel
            machineRef.TransitionTo(new StateA());
        }

        public override void QuarterEntered()
        {
            //TODO: Dispense merch and return two dimes
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
