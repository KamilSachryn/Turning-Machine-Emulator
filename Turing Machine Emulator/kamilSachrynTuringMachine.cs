using System;
using System.Collections.Generic;
using System.Text;

namespace Turing_Machine_Simulator
{
    //enums to keep track of TM status
    enum RunState { Running, Accepted, Crashed, Waiting, ForceExit }
    public class kamilSachrynTuringMachine
    {
        //Set up global vars 
        //char[] states;
        char[] inputTape;
        //char[] tape;
        kamilSachrynTransitionFunction[] transitionFunctions;
        char startState;
        char BLANK;
        char finalState;
        


        int headPosition = 0;
        char currentState;


        List<String> list_IDs = new List<string>();


        RunState runState = RunState.Waiting;


        public kamilSachrynTuringMachine(char[] states, char[] input, char[] tape, kamilSachrynTransitionFunction[] transitionFunctions, char startState, char BLANK, char finalState)
        {
            //this.states = states;
            this.inputTape = input;
            //this.tape = tape;
            this.transitionFunctions = transitionFunctions;
            this.startState = startState;
            this.BLANK = BLANK;
            this.finalState = finalState;
        }


        public void Run(char[] userInput)
        {
 
            inputTape = new char[userInput.Length + 2]; //input tape is as long as the userinput + 2 blanks
            inputTape[0] = BLANK; //First pos is blank 
            
            for(int i = 1; i <= userInput.Length;i++) //set input into tape
            {
                inputTape[i] = userInput[i - 1];
            }
            inputTape[inputTape.Length - 1] = BLANK; //Last pos is blank

            Console.Write("Input tape: ");
            foreach (char c in inputTape)
            {
                Console.Write(c);
            }
            Console.WriteLine();

            currentState = startState; //go to the q0 state
            headPosition = 1; //head pos at 1 to take account for blank at start

            list_IDs.Add(GenerateID()); //Generate initial id
            runState = 0; //set state to Running
            Console.WriteLine("Execution started, Press ESC to halt.");

            //Loops untill Crashed, Accepted, or Forced to close.
            while ( runState == RunState.Running )
            {
                kamilSachrynTransitionFunction currentTransitionFunction = null;

                //make sure we have enough blanks to handle any incoming move (simulate infinite tape in both directions)
                if (headPosition == 0) //if we're at the leftmost position, make sure theres another Blank
                {
                    char[] newTape = new char[inputTape.Length + 1];
                    newTape[0] = BLANK;

                    for (int i = 1; i < newTape.Length; i++)
                    {
                        newTape[i] = inputTape[i - 1];
                    }

                    inputTape = newTape;
                    headPosition += 1;
                }
                else if (headPosition == inputTape.Length - 1) //if we're at the rightmost position, make sure theres another blank
                {
                    char[] newTape = new char[inputTape.Length + 1];
                    newTape[newTape.Length - 1] = BLANK;

                    for(int i = 0; i < inputTape.Length; i++)
                    {
                        newTape[i] = inputTape[i];
                    }
                    

                    inputTape = newTape;
                    
                }

                //Find a match for the current transition function for current head position
                foreach (kamilSachrynTransitionFunction t in transitionFunctions)
                {
                    if(t.GetCurrentState() == currentState && t.GetCurrentTapeSymbol() == inputTape[headPosition])
                    {
                        currentTransitionFunction = t;
                    }
                }

                

                if(currentState == finalState) //if final, end execution
                {
                    runState = RunState.Accepted;
                   // Console.WriteLine("Reached final state");
                   

                }
                else if (currentTransitionFunction == null) //If null, crash
                {
                    runState = RunState.Crashed;
                    //Console.WriteLine("reached transition with no exit");
                }
                else //We found a matching state
                {
                    //Set the new state
                    currentState = currentTransitionFunction.GetNewState();
                    
                    
                    //Set the new tape symbol    
                    inputTape[headPosition] = currentTransitionFunction.GetNewTapeSymbol();

                    //set the new direction
                    if(currentTransitionFunction.GetDirection() == 'L')
                    {
                        headPosition -= 1;
                    }
                    else
                    {
                        headPosition += 1;
                    }

                }


                //Add ID to list for printing at end
                if(runState == 0)
                {
                    list_IDs.Add(GenerateID());
                }
                else if (runState == RunState.Accepted)
                {
                    list_IDs.Add(" ACCEPT");
                }
                else if(runState == RunState.Crashed)
                {
                    list_IDs.Add(" CRASH");
                }

                //If the user hits ESC, terminate execution
                if (Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        runState = RunState.ForceExit;
                    }
                }

            }
            
            
            
            
            //Print all IDs gone through
            for(int i = 0; i < list_IDs.Count;i++)
            {
                Console.Write(list_IDs[i]);
                if (i != list_IDs.Count - 1)
                {
                    Console.Write(" |- ");
                }
                
            }
            Console.WriteLine();
            
            //Used only in telling the user if the TM accepted/crashed
            if(runState == RunState.Accepted)
            {
                Console.WriteLine("Execution complete, Program has been Accepted");

            }
            else if(runState == RunState.Crashed)
            {
                Console.WriteLine("Execution complete, Program has not been Accepted");
            }
            else if(runState == RunState.ForceExit)
            {
                Console.WriteLine("Execution not completed, Program has been manually halted");
            }

            Console.WriteLine();


        }

        //Generate an instantaneous description
        public String GenerateID()
        {
            String ID = "";
            for(int i = 0; i < inputTape.Length; i++)
            {
                if(i == headPosition)
                {
                    ID += "q_" + currentState + ' ';
                }

                ID += inputTape[i];
                ID += ' ';
            }


            return ID;
        }


    }
}
