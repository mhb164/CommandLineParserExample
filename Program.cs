using CommandLine;
using System.Security.Cryptography;
using System.Text;

namespace CommandLineParserExample;

public class Program
{
    static void Main(string[] args)
    {
        Parser.Default.ParseArguments<HashCommandParameters>(args)
                        .WithParsed(parameters => Hash(parameters));

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine();
    }

    private static void Hash(HashCommandParameters parameters)
    {
        using var hashAlgorithm = GetHashAlgorithm(parameters.Algorithm);
        var hashPassword = hashAlgorithm.ComputeHash(parameters.PasswordAsBytes);

        Console.WriteLine($"Hash({parameters.Password},{parameters.Algorithm}) = {Convert.ToBase64String(hashPassword)}");
    }

    private static HashAlgorithm GetHashAlgorithm(string algorithm)
    {
        algorithm = algorithm.ToUpper();

        return algorithm switch
        {
            "MD5" => MD5.Create(),
            "SHA1" => SHA1.Create(),
            "SHA256" => SHA256.Create(),
            "SHA384" => SHA384.Create(),
            "SHA512" => SHA512.Create(),
            _ => throw new ArgumentException($"Unknown hash algorithm '{algorithm}'"),
        };
    }
}

public class HashCommandParameters
{
    [Option('a', "algorithm", Required = true,
        HelpText = "Hash algorithm (MD5, SHA1, SHA256, SHA384, SHA512)")]
    public string Algorithm { get; set; }

    [Option('p', "password", Required = true,
        HelpText = "Password to hash")]
    public string Password { get; set; }

    public byte[] PasswordAsBytes => Encoding.UTF8.GetBytes(Password);
}



