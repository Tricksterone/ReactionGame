namespace ReactionGame
{
    class Highscore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Time { get; set; }

        public override string ToString()
        {
            return Name + "  -  " + Time;
        }
    }
}