using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MonitorDataAccess.Config;
using Domain.DTOs;
using Newtonsoft.Json;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MonitorDataAccess.DataAccess;

public class FileLogDataAccess : IDataAccess<LogEntry>
{
    //private readonly string _filePath;
    private readonly IOptions<LocalFileData> localFileData;
    private readonly ILogger<FileLogDataAccess> _logger;

    public FileLogDataAccess(IOptions<LocalFileData> configuration, ILogger<FileLogDataAccess> logger)
    {
        localFileData = configuration;
        _logger = logger;
    }

    public async Task Add(LogEntry entry)
    {
        var fullPath = Path.GetFullPath(localFileData.Value.LogData);
        var logEntries = await GetAll();
        logEntries.Add(entry);
        logEntries = logEntries.Select(UpdateEncryptedText).ToList();
        var json = JsonConvert.SerializeObject(logEntries);
        await File.WriteAllTextAsync(fullPath, json);
    }

    public async Task<List<LogEntry>> GetAll()
    {
        var fullPath = Path.GetFullPath(localFileData.Value.LogData);

        if (!File.Exists(fullPath))
        {
            _logger.LogWarning("Local log data file not present : {path}", fullPath);
            var list = new List<LogEntry>()
            {
                UpdateEncryptedText(new LogEntry()
                {
                    Creation = DateOnly.FromDateTime(DateTime.Now),
                    Text = "test entry"
                })
            };

            var json = JsonConvert.SerializeObject(list);
            File.WriteAllText(fullPath, json);
        }

        var text = File.ReadAllText(fullPath);
        List<LogEntry>? logEntries = JsonConvert.DeserializeObject<List<LogEntry>>(text);
        logEntries = logEntries.Select(UpdateDecryptedText).ToList();

        return await Task.FromResult(logEntries);

    }

    private LogEntry UpdateDecryptedText(LogEntry dto)
    {
        dto.Text = ConvertFromAesEncrypted(ConvertFromBase64(dto.EncryptedText));

        return dto;
    }

    private LogEntry UpdateEncryptedText(LogEntry dto)
    {
        // Encrypt using Base64 so that the text only contains valid characters
        dto.EncryptedText = ConvertToBase64(ConvertToAesEncrypted(dto.Text));

        return dto;
    }

    private byte[] ConvertFromBase64(string str)
        => Convert.FromBase64String(str);
    
    private string ConvertToBase64(byte[] str)
        => Convert.ToBase64String(str);

    byte[] key = Encoding.UTF8.GetBytes("19lorenchorley85");

    // https://learn.microsoft.com/en-us/dotnet/standard/security/encrypting-data
    private string ConvertFromAesEncrypted(byte[] str) // TODO
    {
        return DecryptStringFromBytes(str, key);
    }
    
    private byte[] ConvertToAesEncrypted(string str) // TODO
    {
        return EncryptStringToBytes(str, key);
    }

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
