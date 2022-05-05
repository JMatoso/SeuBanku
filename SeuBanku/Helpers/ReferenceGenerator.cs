using SeuBanku.Models;

namespace SeuBanku.Helpers
{
    public static class ReferenceGenerator
    {
        public static string GenerateNewReference(BankOperations bankOperations)
        {
            var refCode = 
                bankOperations.ToString()[..2].ToUpper() + "-" +
                DateTime.UtcNow.ToString("d h m s");

            return refCode.Replace(" ", "");
        }

        public static string GenerateDocumentName()
            => $"invoice-{DateTime.UtcNow.ToString("MM dd yyyy hh mm ss").Replace(" ", "")}.pdf";
    }
}
