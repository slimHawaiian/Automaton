namespace Automaton
{

    /// <summary>
    /// the position class holds the data for each character from the input string
    /// </summary>
    public class Position
    {
        public int State { get; set; }
        public Acceptance Acceptance { get; set; }
        public string Input { get; set; }
    }


    /// <summary>
    /// the acceptance enum is used to make the acceptance easier to understan
    /// </summary>
    public enum Acceptance
    {
        accepted,
        rejected
    }
}
