using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Domain.DTOs;

public class LogEntry
{
    public int Id { get; set; }
    public DateOnly Creation { get; set; }
    public string Annotation { get; set; } = string.Empty;
    public string EncryptedText { get; set; }

    [NotMapped]
    [JsonIgnore]
    public string Text { get; set; }

    public LogEntry UpdateDecryptedText()
    {
        Text = ConvertFromAesEncrypted(ConvertFromBase64(EncryptedText));

        return this;
    }

    public LogEntry UpdateEncryptedText()
    {
        // Encrypt using Base64 so that the text only contains valid characters
        EncryptedText = ConvertToBase64(ConvertToAesEncrypted(Text));

        return this;
    }

    // https://learn.microsoft.com/en-us/dotnet/standard/security/encrypting-data
    byte[] key = Encoding.UTF8.GetBytes("19lorenchorley85");

    private byte[] ConvertFromBase64(string str) => Convert.FromBase64String(str);

    private string ConvertToBase64(byte[] str) => Convert.ToBase64String(str);

    private string ConvertFromAesEncrypted(byte[] str) => DecryptStringFromBytes(str, key);

    private byte[] ConvertToAesEncrypted(string str) => EncryptStringToBytes(str, key);

    static byte[] EncryptStringToBytes(string str, byte[] keys)
    {
        byte[] encrypted;
        using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
        {
            aes.Key = keys;

            aes.GenerateIV(); // The get method of the 'IV' property of the 'SymmetricAlgorithm' automatically generates an IV if it is has not been generate before. 

            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                msEncrypt.Write(aes.IV, 0, aes.IV.Length);
                ICryptoTransform encoder = aes.CreateEncryptor();
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encoder, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(str);
                }
                encrypted = msEncrypt.ToArray();
            }
        }

        return encrypted;
    }

    static string DecryptStringFromBytes(byte[] cipherText, byte[] key)
    {
        string decrypted;
        using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
        {
            // Setting a key size disposes the previously-set key. 
            // Setting a key size will generate a new key. 
            // Setting a key size is redundant if a key going to be set after this statement. 
            //aes.KeySize = key.Length; 

            aes.Key = key;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream msDecryptor = new MemoryStream(cipherText))
            {
                byte[] readIV = new byte[16];
                msDecryptor.Read(readIV, 0, 16);
                aes.IV = readIV;
                ICryptoTransform decoder = aes.CreateDecryptor();
                using (CryptoStream csDecryptor = new CryptoStream(msDecryptor, decoder, CryptoStreamMode.Read))
                using (StreamReader srReader = new StreamReader(csDecryptor))
                {
                    decrypted = srReader.ReadToEnd();
                }
            }
        }
        return decrypted;
    }
}
