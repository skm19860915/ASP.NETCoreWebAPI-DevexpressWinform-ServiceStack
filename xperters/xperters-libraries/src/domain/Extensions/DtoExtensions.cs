using xperters.extensions;

namespace xperters.domain.Extensions
{
    public static class DtoExtensions
    {
        public static void SetDisplayName(this UserDto dto)
        {

            if (dto.DisplayName.IsBlank())
            {
                dto.DisplayName = $"{dto.FirstName} {dto.LastName}";
            }
        }

        public static decimal CalculateTotalFees(this decimal amount, decimal flatRate, decimal flatPercent)
        {
            return flatRate + (amount * flatPercent);
        }


        public static decimal CalculateTotalAmount(this decimal amount, decimal flatRate, decimal flatPercent)
        {
            return amount + amount.CalculateTotalFees(flatRate, flatPercent);
        }
    }
}
