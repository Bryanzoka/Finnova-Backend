namespace FinnovaAPI.Helpers
{
    public static class AccountValidator
    {
        public static void ValidateId(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
        }

        public static void ValidateAmount(decimal amount)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount, "amount must be greater than 0");
        }
    }
}