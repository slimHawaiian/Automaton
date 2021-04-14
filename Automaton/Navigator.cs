using System;
namespace Automaton
{
    public class Navigator
    {
        public Position navigate(string input, int state)
        {
            Position p = new Position
            {
                Input = input,
                State = state
            };

            p.State = state++;
            p.Acceptance = Acceptance.accepted;

            return p;
        }
    }
}
