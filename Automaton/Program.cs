using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace Automaton
{
    class Program
    {
        static MachineSettings GetMachineSettings()
        {
            var input = File.ReadAllText("input.json");
            var machineSettings = JsonConvert.DeserializeObject<MachineSettings>(input);
            return machineSettings;
        }

        static void DisplayStats(MachineSettings machineSettings)
        {
            Console.WriteLine($"Input {machineSettings.Input}");
        }

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

        static void JudgeInput(Position position)
        {
            Console.WriteLine();
            Console.WriteLine();

            if (position.Acceptance == Acceptance.accepted)
                Console.WriteLine("Automaton accepts input");
            else
                Console.WriteLine("Automaton rejects input");
        }

        static Position ApplyRule(Position position)
        {
            var ratedPosition = RatePosition(position);
            return FindNextState(ratedPosition);
        }

        static Position RatePosition(Position position)
        {
            var machineSettings = GetMachineSettings();
            if (position.State == machineSettings.AcceptState)
                position.Acceptance = Acceptance.accepted;
            else
                position.Acceptance = Acceptance.rejected;

            return position;
        }

        static Position FindNextState(Position position)
        {
            var machineSettings = GetMachineSettings();
            machineSettings.States.TryGetValue(position.State, out List<string> rules);

            var currentRule = rules.Select(x => x.Split(','));
            var stateChange = currentRule.Where(x => x.Contains(position.Input.ToString()));

            foreach (var currentState in stateChange)
            {
                if (int.Parse(currentState[0])== position.State)
                    position.State = int.Parse(currentState[1]);
            }
            var p = RatePosition(position);
            Console.WriteLine($"Character {p.Input} transitions to state {p.State}");
            return p;
        }
    }
}
