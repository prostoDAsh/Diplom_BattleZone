namespace Signals
{
    public struct AddCoinSignal
    {
        private readonly int _coin;
        public int Coin => _coin;
        
        public AddCoinSignal(int coin)
        {
            _coin = coin;
        }

        
    }
}