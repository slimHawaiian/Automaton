using System;
namespace Automaton
{

    /// <summary>
    /// not used left for historical purposes.  the obsolete flag will notify the developer that this class is no longer used.
    /// </summary>
    public class Navigator
    {
        [Obsolete]
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
