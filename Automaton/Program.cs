using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Automaton
{
    class Program
    {

        /// <summary>
        /// The machine settings are stored in a file called input.json
        /// </summary>
        /// <returns></returns>
        static MachineSettings GetMachineSettings()
        {
            var input = File.ReadAllText("input.json");
            var machineSettings = JsonConvert.DeserializeObject<MachineSettings>(input);
            return machineSettings;
        }

        /// <summary>
        /// Some of the stats are read from the input file and are displayed as part of the output in the console 
        /// </summary>
        /// <param name="machineSettings"></param>
        static void DisplayStats(MachineSettings machineSettings)
        {
            Console.WriteLine($"Input {machineSettings.Input}");
        }


        /// <summary>
        /// Starting point to the program
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            List<Position> positions = new List<Position>();

            var machineSettings = GetMachineSettings();
            DisplayStats(machineSettings);

            var inputStringArray = machineSettings.Input.Split(",");
            Position p = new Position {  State = 1};

            for (var i = 0;i < inputStringArray.Count();i++)
            {
                p.Input = inputStringArray[i];
                p = ApplyRule(p);
                positions.Add(p);
            }
            JudgeInput(positions.Last());
        }


        /// <summary>
        /// Test the final state and determine its state
        /// </summary>
        /// <param name="position"></param>
        static void JudgeInput(Position position)
        {
            Console.WriteLine();
            Console.WriteLine();

            if (position.Acceptance == Acceptance.accepted)
                Console.WriteLine("Automaton accepts input");
            else
                Console.WriteLine("Automaton rejects input");
        }


        /// <summary>
        /// This is a transition point for the app.  This method allows for expandability. 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static Position ApplyRule(Position position)
        {
            var ratedPosition = RatePosition(position);
            return FindNextState(ratedPosition);
        }


        /// <summary>
        /// Test each state and store the acceptance value
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static Position RatePosition(Position position)
        {
            var machineSettings = GetMachineSettings();
            if (position.State == machineSettings.AcceptState)
                position.Acceptance = Acceptance.accepted;
            else
                position.Acceptance = Acceptance.rejected;

            return position;
        }

        /// <summary>
        /// the next state is determined by the input.json file.  Use that to expand its rules
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        static Position FindNextState(Position position)
        {
            var machineSettings = GetMachineSettings();
            machineSettings.States.TryGetValue(position.State, out List<string> rules);

            var currentRule = rules.Select(x => x.Split(','));
            var stateChange = currentRule.Where(x => x.Contains(position.Input.ToString()));

            foreach (var currentState in stateChange)
            {
                if (int.Parse(currentState[0]) == position.State)
                {
                    position.State = int.Parse(currentState[1]);
                    break;
                }
            }
            var p = RatePosition(position);
            Console.WriteLine($"Character {p.Input} transitions to state {p.State}");
            return p;
        }
    }
}
