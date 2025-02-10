namespace Infrastructure
{
    public class Cryptography
    {
        public static string Hash(string value)
        {
            var hashData = BCrypt.Net.BCrypt.HashPassword(value);
            return hashData;
        }

        public static bool Verify(string value, string hash)
        {
            bool result = BCrypt.Net.BCrypt.Verify(value, hash);
            return result;
        }
    }
}
