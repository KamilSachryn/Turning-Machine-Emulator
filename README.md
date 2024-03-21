

# Turning Machine Emulator

A console based program which emulates a Turing Machine given a program and user input.

### Installation

Compilation requires 

 - `Visual Studio`
 - `C#.net`

### Usage

Given a file containing a valid Turning Machine program (examples included within the project) which contains states that the machine could exist in and user input in the form of the tape that the machine will read (0s and 1s) the machine will do one of a few things, halt and accept the program, crash and reject the program,  or run indefinitely until the user stops execution. 


Example programs and their explenations are included in the project files.

# Example Program: 
//this program accepts L=01*

//B is "blank"

//f is final (accepting) state

//the order of the 5-tuple is 

// current_state  current_tape_symbol new_state  new_tape_symbol  direction

0 0 1 1 R  // in q0, must see one 0. go to q1 ot ready many 1s

1 1 1 0 R  // in q1 need to see zero or more 1s

1 B f B R  // at end of input, read B and accept.

# Example Output:
Input word:

0

1

1

Input Alphabet: 01

Tape Alphabet: 01B

States: 01f

Start state: 0

Final state: f

Input tape: B0111111B

Execution started, Press ESC to halt.

B q_0 0 1 1 1 1 1 1 B  |- B 1 q_1 1 1 1 1 1 1 B  |- B 1 0 q_1 1 1 1 1 1 B  |- B 1 0 0 q_1 1 1 1 1 B  |- B 1 0 0 0 q_1 1 1 1 B  |- B 1 0 0 0 0 q_1 1 1 B  |- B 1 0 0 0 0 0 q_1 1 B  |- B 1 0 0 0 0 0 0 q_1 B  |- B 1 0 0 0 0 0 0 B q_f B  |-  ACCEPT

Execution complete, Program has been Accepted
