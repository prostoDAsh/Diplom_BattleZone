namespace Signals
{
    public struct AddStarSignal
    {
        private int _countStars;
        
        public int CountStars => _countStars;

        public AddStarSignal(int count)
        {
            _countStars = count;
        }
    }
}