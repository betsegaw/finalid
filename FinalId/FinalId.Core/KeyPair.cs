// <copyright file="KeyPair.cs" company="FinalID">
// Copyright (c) FinalID. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace FinalId.Core
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using Newtonsoft.Json;

    public class KeyPair
    {
        private static RSAEncryptionPadding encryptionPadding = RSAEncryptionPadding.Pkcs1;
        private static RSASignaturePadding signaturePadding = RSASignaturePadding.Pkcs1;

        public KeyPair(RSA keys)
        {
            this.Keys = keys;
            this.PublicKeyString = RSAToJsonString(this.Keys, false);

            UnicodeEncoding byteConverter = new UnicodeEncoding();
            this.PublicKeyBytes = byteConverter.GetBytes(PublicKeyString);
        }

        [JsonConstructor]
        public KeyPair(string keyAsJson)
        {
            this.KeyAsJson = keyAsJson;
        }

        public KeyPair(byte[] publicKeyAsBytes)
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();

            this.PublicKeyString = byteConverter.GetString(publicKeyAsBytes);
            this.Keys = RSAFromJsonString(PublicKeyString);

            this.PublicKeyBytes = publicKeyAsBytes;
        }

        public static KeyPair TestKeyPair { get; set; }

        public string KeyAsJson
        {
            get
            {
                return RSAToJsonString(
                    this.Keys,
                    !((RSACryptoServiceProvider)this.Keys).PublicOnly);
            }

            set
            {
                this.Keys = RSAFromJsonString(value);
                this.PublicKeyString = RSAToJsonString(this.Keys, false);

                UnicodeEncoding byteConverter = new UnicodeEncoding();
                this.PublicKeyBytes = byteConverter.GetBytes(PublicKeyString);
            }
        }

        [JsonIgnore]
        public RSA Keys { get; set; }

        [JsonIgnore]
        public string PublicKeyString { get; private set; }

        [JsonIgnore]
        public byte[] PublicKeyBytes { get; private set; }

        [JsonIgnore]
        public bool HasPrivateKey
        {
            get
            {
                return RSAToJsonString(this.Keys, true) != RSAToJsonString(this.Keys, false);
            }
        }

        [JsonIgnore]
        public string Fingerprint
        {
            get
            {
                return CreateMD5(PublicKeyString);
            }
        }

        public string ExportPublicAndPrivateKeys()
        {
            return RSAToJsonString(this.Keys, true);
        }

        public static KeyPair GenerateNewKeyPair()
        {
            if (TestKeyPair == null)
            {
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

                return new KeyPair(rsa);
            }
            else
            {
                return TestKeyPair;
            }
        }

        public string Decrypt(string content)
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] contentBytes = content.Split('-').Select(x => byte.Parse(x, NumberStyles.HexNumber)).ToArray();
            byte[] decryptedData = this.Keys.Decrypt(contentBytes, encryptionPadding);
            return byteConverter.GetString(decryptedData);
        }

        public string Encrypt(string content)
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] encryptedData = this.Keys.Encrypt(byteConverter.GetBytes(content), encryptionPadding);
            return BitConverter.ToString(encryptedData);
        }

        public byte[] Sign(string content)
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();

            byte[] originalData = byteConverter.GetBytes(content);
            return this.Keys.SignData(originalData, HashAlgorithmName.SHA256, signaturePadding);
        }

        public bool Verify(string content, byte[] signature)
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();

            byte[] contentBytes = byteConverter.GetBytes(content);
            return this.Keys.VerifyData(contentBytes, signature, HashAlgorithmName.SHA256, signaturePadding);
        }

        private string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Copy-pasted from https://github.com/dotnet/core/issues/874.
        /// </summary>
        private RSA RSAFromJsonString(string jsonString)
        {
            RSA result = new RSACryptoServiceProvider(2048);

            try
            {
                var paramsJson = JsonConvert.DeserializeObject<RSAParametersJson>(jsonString);

                RSAParameters parameters = new RSAParameters
                {
                    Modulus = paramsJson.Modulus != null ? Convert.FromBase64String(paramsJson.Modulus) : null,
                    Exponent = paramsJson.Exponent != null ? Convert.FromBase64String(paramsJson.Exponent) : null,
                    P = paramsJson.P != null ? Convert.FromBase64String(paramsJson.P) : null,
                    Q = paramsJson.Q != null ? Convert.FromBase64String(paramsJson.Q) : null,
                    DP = paramsJson.DP != null ? Convert.FromBase64String(paramsJson.DP) : null,
                    DQ = paramsJson.DQ != null ? Convert.FromBase64String(paramsJson.DQ) : null,
                    InverseQ = paramsJson.InverseQ != null ? Convert.FromBase64String(paramsJson.InverseQ) : null,
                    D = paramsJson.D != null ? Convert.FromBase64String(paramsJson.D) : null,
                };

                result.ImportParameters(parameters);
            }
            catch
            {
                throw new Exception("Invalid JSON RSA key.");
            }

            return result;
        }

        /// <summary>
        /// Copy-pasted from https://github.com/dotnet/core/issues/874.
        /// </summary>
        private string RSAToJsonString(RSA rsa, bool includePrivateParameters)
        {
            RSAParameters parameters = rsa.ExportParameters(includePrivateParameters);

            var parasJson = new RSAParametersJson()
            {
                Modulus = parameters.Modulus != null ? Convert.ToBase64String(parameters.Modulus) : null,
                Exponent = parameters.Exponent != null ? Convert.ToBase64String(parameters.Exponent) : null,
                P = parameters.P != null ? Convert.ToBase64String(parameters.P) : null,
                Q = parameters.Q != null ? Convert.ToBase64String(parameters.Q) : null,
                DP = parameters.DP != null ? Convert.ToBase64String(parameters.DP) : null,
                DQ = parameters.DQ != null ? Convert.ToBase64String(parameters.DQ) : null,
                InverseQ = parameters.InverseQ != null ? Convert.ToBase64String(parameters.InverseQ) : null,
                D = parameters.D != null ? Convert.ToBase64String(parameters.D) : null,
            };

            return JsonConvert.SerializeObject(parasJson);
        }
    }
}
