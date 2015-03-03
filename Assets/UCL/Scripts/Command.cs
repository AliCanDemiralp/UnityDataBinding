namespace Assets.UCL.Scripts
{
    class Command
    {
        public string   Entry   { get; private set; }
        public bool     Result  { get; private set; }

        public Command(string entry, bool result)
        {
            Entry   = entry;
            Result  = result;
        }

        public override string ToString()
        {
            return Entry + " (" + Result + ")";
        }
    }
}
