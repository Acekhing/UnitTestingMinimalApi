namespace UnitTestingMinimalApi.Services
{
    public class PlayerService
    {
        public bool IsEliglePlayer(int age)
        {

            if (age <= 15)
            {
                return false;
            }


            if (age >= 20)
            {
                return false;
            }

            //return true;
            throw new NotImplementedException("Not implemented...");
        }
        public bool CanPlay(bool contractExpired)
        {
            if (contractExpired)
            {
                return false;
            }
            return true;
        }
    }
}
