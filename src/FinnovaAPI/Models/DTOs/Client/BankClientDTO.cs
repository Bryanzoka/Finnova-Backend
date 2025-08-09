namespace FinnovaAPI.Models.DTOs.Client
{
    public record BankClientDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public static BankClientDTO ToDTO(BankClientModel model)
        {
            return new BankClientDTO
            {
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone
            };
        }
    }
}