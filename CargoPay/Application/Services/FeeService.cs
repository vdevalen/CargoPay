namespace CargoPay.Application.Services
{
    public class FeeService
    {
        private static readonly Lazy<FeeService> _instance = new Lazy<FeeService>(() => new FeeService());
        private decimal _currentFee = 0;

        private FeeService()
        {
            UpdateFee();
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromHours(1));
                    UpdateFee();
                }
            });
        }

        public static FeeService Instance => _instance.Value;

        public decimal GetCurrentFee()
        {
            return _currentFee;
        }

        private void UpdateFee()
        {
            Random random = new Random();
            decimal randomFactor = (decimal)random.NextDouble() * 2;
            _currentFee = _currentFee * randomFactor;
        }
    }
}
