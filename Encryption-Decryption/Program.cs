using System.Security.Cryptography;
using System.Text;


byte[] passwordHash, passwordKey;
Cryptography c = new();

string password = "kerim123";

c.PasswordEncrypt(password, out passwordHash, out passwordKey);

Console.WriteLine(password);
Console.WriteLine(Encoding.UTF8.GetString(passwordKey));
Console.WriteLine(Encoding.UTF8.GetString(passwordHash));

var result = c.VeriyfPassword(password,passwordHash,passwordKey);
Console.WriteLine(result);

Console.ReadLine();

public class Cryptography
{
    public void PasswordEncrypt(string password, out byte[] passwordHash, out byte[] passwordKey)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordKey = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }

    public bool VeriyfPassword(string password, byte[] passwordHash, byte[] passwordKey)
    {
        using (var hmac = new HMACSHA512(passwordKey))
        {
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (hash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}