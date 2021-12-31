//Turing Machine Simulator by Kamil Sachryn
//CSCI 36500
//4/4/2020


using System;
using System.Collections.Generic;
using System.IO;

namespace Turing_Machine_Simulator
{
    class kamilSachrynProgram
    {
        static void Main(string[] args)
        {
            string filename = "tm8.txt";
            string userInput = "";

            //Handle inputs
            //Console.Write("Name of input file: ");
            //String fileNameInput = Console.ReadLine();
           // filename = getValidFilePath(fileNameInput); //Make sure file exists, ask for new file if not
            //Console.WriteLine();


            //Loop TM and ask for a new Input every time
           // while (true)
            {
                Console.Write("Input word: ");
               // userInput = Console.ReadLine();
                Console.WriteLine();

                kamilSachrynTuringMachine tm = parseTM(filename, userInput);
                tm.Run(userInput.ToCharArray());
            }

        }


        static kamilSachrynTuringMachine parseTM(String filename, String userInput)
        {
            //Set up variables
            List<char> inputAlphabet = new List<char>();
            List<char> tapeAlphabet = new List<char>();
            List<char> states = new List<char>();
            char startState = '0';
            char finalState = 'f';
            char blankSymbol = 'B';
            List<string> lines = new List<string>(File.ReadAllLines(filename));
            List<kamilSachrynTransitionFunction> transitionFunctions = new List<kamilSachrynTransitionFunction>();

            //Parse lines to Transition functions
            for (int i = 0; i < lines.Count; i++)
            {
                //Remove comments
                if (lines[i].Contains("//"))
                {
                    lines[i] = lines[i].Substring(0, lines[i].IndexOf("//"));

                }

                //Remove spaces
                lines[i] = lines[i].Replace(" ", String.Empty);

                //Add non-empty lines
                if (lines[i].Length != 0)
                {
                    transitionFunctions.Add(new kamilSachrynTransitionFunction(lines[i][0], lines[i][1], lines[i][2], lines[i][3], lines[i][4]));
                }
            }

            //Set Tape Alphabet
            foreach (kamilSachrynTransitionFunction i in transitionFunctions)
            {
                //Symbols from "current"
                if (!tapeAlphabet.Contains(i.GetCurrentTapeSymbol()))
                {
                    tapeAlphabet.Add(i.GetCurrentTapeSymbol());
                }

                //Symbols from "new"
                if (!tapeAlphabet.Contains(i.GetNewTapeSymbol()))
                {
                    tapeAlphabet.Add(i.GetNewTapeSymbol());
                }
            }

            //Set input alphabet
            foreach (char c in userInput)
            {
                if (!inputAlphabet.Contains(c))
                {
                    inputAlphabet.Add(c);
                }
            }

            //Set States by looping through all transitions, and checking their state choices
            foreach (kamilSachrynTransitionFunction i in transitionFunctions)
            {
                char currentState = i.GetCurrentState();
                Console.WriteLine(currentState);
                //Read states
                if (!states.Contains(currentState))
                {
                    states.Add(currentState);
                    //Hard code 0 to be start state
                    if (currentState == '0')
                    {
                        startState = currentState;
                    }
                }


                char newState = i.GetNewState();

                //Write states
                if (!states.Contains(newState))
                {
                    states.Add(newState);
                    //Hard code f to be final state
                    if (newState.ToString().ToLower() == "f")
                    {
                        finalState = newState;
                    }
                }
            }

            //Output info about input file to Console
            Console.Write("Input Alphabet: ");
            foreach (char c in inputAlphabet)
            {
                Console.Write(c);
            }
            Console.WriteLine();

            Console.Write("Tape Alphabet: ");
            foreach (char c in tapeAlphabet)
            {
                Console.Write(c);
            }
            Console.WriteLine();

            Console.Write("States: ");
            foreach (char s in states)
            {
                Console.Write(s);
            }
            Console.WriteLine();


            Console.WriteLine("Start state: " + startState);
            Console.WriteLine("Final state: " + finalState);

            //Return the TM with appropriate arguments
            return new kamilSachrynTuringMachine(states.ToArray(), inputAlphabet.ToArray(), tapeAlphabet.ToArray(), transitionFunctions.ToArray(), startState, blankSymbol, finalState);
        }

        //Parse user input into actual filename incase incorrect
        static String getValidFilePath(String input)
        {

            //Trim input incase file dropped onto console
            input = input.Trim();
            input = input.Trim('\'');
            input = input.Trim('\t');
            if (input.Contains(";")) Console.WriteLine("ping");

            String path = "";
            bool found = false;

            while (!found)
            {
                //If we were given a proper file path
                if (File.Exists(input))
                {
                    path = input;
                    found = true;
                }
                //if we were given only the file name
                else if (File.Exists(Directory.GetCurrentDirectory() + Path.DirectorySeparatorChar + input))
                {
                    path = Directory.GetCurrentDirectory() + input;
                    Console.WriteLine(path);
                    found = true;
                }
                //if the file pointed to does not exist
                else
                {
                    Console.WriteLine("File " + input + "does not exist");
                    Console.Write("Enter Name of input file: ");
                    input = Console.ReadLine();
                    Console.WriteLine();
                    found = false;
                }
            }

            Console.WriteLine("Input file " + input + " loaded.");

            return path;
        }
    }
}
