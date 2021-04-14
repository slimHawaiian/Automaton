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
            Console.WriteLine($"Accept State {machineSettings.AcceptState}");
            Console.WriteLine($"Alphabet {machineSettings.Alphabet}");
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
                if(i > 0)
                    p.State = p.State < machineSettings.States.Count()? p.State + 1 : p.State;
                p = ApplyRule(p);
                positions.Add(p);
            }
            JudgeInput(positions);
        }

        static void JudgeInput(List<Position> positions)
        {
            var hasAcceptedPositions = positions.Where(x => x.Acceptance == Acceptance.accepted);
            Console.WriteLine();
            Console.WriteLine();

            if (hasAcceptedPositions != null)
                Console.WriteLine("Automaton accepts input");
            else
                Console.WriteLine("Automaton rejects input");
        }

        static Position ApplyRule(Position position)
        {
            var ratedPosition = RatePosition(position);
            Console.WriteLine($"Character {position.Input} at state {position.State} is {ratedPosition.Acceptance}");
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
                if (currentState[0].Equals(position.State))
                    position.State = int.Parse(currentState[1]);
            }
           
            return position;
        }
    }
}
