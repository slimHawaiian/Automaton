using System.Collections.Generic;

namespace Automaton
{
    public class MachineSettings
    {
        public string Alphabet { get; set; }
        public string Input { get; set; }
        public int AcceptState { get; set; }
        public Dictionary<int, List<string>>States { get; set; }
    }
}
