using System.Security.Cryptography;
using System.Text;

namespace BackEnd.Services;

public class HashService
{
    public string GetMd5Hash(string input)
    {
        var provider = new MD5CryptoServiceProvider();

        var data = provider.ComputeHash(Encoding.UTF8.GetBytes(input));

        var sBuilder = new StringBuilder();

        foreach (var t in data) sBuilder.Append(t.ToString("x2"));

        return sBuilder.ToString();
    }
}