using System;
using System.Collections.Generic;
using System.Text;

namespace Turing_Machine_Simulator
{
    public class kamilSachrynTransitionFunction
    {
        char currentState;
        char currentTapeSymbol;
        char newState;
        char newTapeSymbol;
        char direction;


        public kamilSachrynTransitionFunction(char current_state, char current_tape_symbol, char new_state, char new_tape_symbol, char direction)
        {
            currentState = current_state;
            currentTapeSymbol = current_tape_symbol;
            newState = new_state;
            newTapeSymbol = new_tape_symbol;
            this.direction = direction;
        }

        public char GetCurrentState()
        {
            return currentState;
        }
        public char GetCurrentTapeSymbol()
        {
            return currentTapeSymbol;
        }
        public char GetNewState()
        {
            return newState;
        }
        public char GetNewTapeSymbol()
        {
            return newTapeSymbol;
        }
        public char GetDirection()
        {
            return direction;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2} {3} {4}", GetCurrentState(), GetCurrentTapeSymbol(), GetNewState(), GetNewTapeSymbol(), GetDirection());
        }
    }
}
