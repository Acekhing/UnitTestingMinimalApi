using UnitTestingMinimalApi.Models;

namespace UnitTestingMinimalApi.Utils
{
    public static class PlayerExtension
    {
        public static bool IsEliglePlayer(this Player player)
        {
            if (player.Age <= 15)
            {
                return false;
            }

            if (player.Age >= 20)
            {
                return false;
            }

            return true;
        }
        public static bool CanPlay(this Player player)
        {
            if (player.ContractExpired)
            {
                return false;
            }
            return true;
        }
        public static bool IsCitizen(this Player player)
        {
            if (player.CountryCode == null || player.CountryCode == "")
            {
                return false;
            }

            if (player.CountryCode.Length > 2 || player.CountryCode.Length < 2)
            {
                return false;
            }

            if (!player.CountryCode.ToUpper().Equals("GH"))
            {
                return false;
            }

            return true;
        }
        public static string? IsValidName(this Player player)
        {
            if (player.FirstName == null || player.LastName == null) return null;
            if (player.FirstName == "" || player.LastName == "") return null;
            if (player.FirstName.Length < 4 || player.LastName.Length < 4) return null;
            return player.FirstName + " " + player.LastName;
        }
    }
}
