namespace Signals
{
    public struct CurrentLevelSignal
    {
        private readonly int _indexLevel;
        
        public int IndexLevel => _indexLevel;

        public CurrentLevelSignal(int indexLevel)
        {
            _indexLevel = indexLevel;
        }
    }
}