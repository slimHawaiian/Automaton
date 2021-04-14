using System;
namespace Automaton
{
    public class Position
    {
        public int State { get; set; }
        public Acceptance Acceptance { get; set; }
        public string Input { get; set; }
    }

    public enum Acceptance
    {
        accepted,
        rejected
    }
}
