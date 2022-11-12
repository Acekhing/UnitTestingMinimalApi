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
        public static string GetFullName(this Player player)
        {
            if (player.FirstName == null || player.FirstName == "")
            {
                throw new ArgumentException(message:"Firstname cannot be null or empty", paramName: nameof(player.FirstName));
            }
            if (player.LastName == null || player.LastName == "")
            {
                throw new ArgumentException(message: "Lastname cannot be null or empty", paramName: nameof(player.LastName));
            }
            if (player.FirstName.Length < 4)
            {
                throw new FormatException(message: "Firstname must be more than 3 characters");
            };
            if ( player.LastName.Length < 4)
            {
                throw new FormatException("Lastname must be more than 3 characters");
            };

            return player.FirstName + " " + player.LastName;
        }
    }
}
