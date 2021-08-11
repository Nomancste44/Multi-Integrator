using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace ZohoToInsightIntegrator.Contract.Utility
{
    public static class Extension
    {
        public static FormUrlEncodedContent ConvertToFormUrlEncodedContent(
            this List<KeyValuePair<string, string>> keyValuePairs)
        {
            return new FormUrlEncodedContent(keyValuePairs);
        }

        public static HttpRequestMessage SetZohoAccessTokenRequestingHeaders(
            this HttpRequestMessage httpRequest, string clientId, string clientSecret, string code)
        {
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Common.ApplicationJson));

            httpRequest.Content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Common.GrantTypeHeaderKey, Common.AuthorizationCodeGrantTypeHeader),
                new KeyValuePair<string, string>(Common.ClientIdHeaderKey, clientId),
                new KeyValuePair<string, string>(Common.ClientSecretHeaderKey, clientSecret),
                new KeyValuePair<string, string>(Common.ZohoRedirectHeaderKey, Common.ZohoRedirectHeaderValue),
                new KeyValuePair<string, string>(Common.CodeHeaderKey, code)
            }.ConvertToFormUrlEncodedContent();
            return httpRequest;
        }

        public static HttpRequestMessage SetTokenRefresingHeaders(
            this HttpRequestMessage httpRequest,
            string clientId,
            string clientSecret,
            string refreshToken)
        {
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Common.ApplicationJson));
            httpRequest.Content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Common.ClientIdHeaderKey, clientId),
                new KeyValuePair<string, string>(Common.ClientSecretHeaderKey, clientSecret),
                new KeyValuePair<string, string>(Common.GrantTypeHeaderKey, Common.RefreshTokenGrantTypeHeader),
                new KeyValuePair<string, string>(Common.RefreshTokenGrantTypeHeader, refreshToken)


            }.ConvertToFormUrlEncodedContent();
            return httpRequest;
        }

        public static HttpRequestMessage SetResourceRequestingHeaders(
            this HttpRequestMessage httpRequest,
            string clientId,
            string clientSecret,
            string authScheme,
            string authToken)
        {
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Common.ApplicationJson));
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue(authScheme, authToken);
            httpRequest.Content = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>(Common.ClientIdHeaderKey, clientId),
                new KeyValuePair<string, string>(Common.ClientSecretHeaderKey, clientSecret)

            }.ConvertToFormUrlEncodedContent();
            return httpRequest;
        }

        public static HttpRequestMessage SetInsightResourceRequestingHeaders(
            this HttpRequestMessage httpRequest,
            string clientId,
            string clientSecret)
        {
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(Common.ApplicationJson));
            httpRequest.Headers.Add(Common.InsightClientIdHeaderKey,clientId);
            httpRequest.Headers.Add(Common.InsightClientSecretHeaderKey,clientSecret);

            return httpRequest;
        }

        public static string DecryptStringAes(this string encryptedValue)
        {
            if (!encryptedValue.IsBase64String()) return string.Empty;
            var bytestokey = Encoding.UTF8.GetBytes("7061737323313233");
            var iv = Encoding.UTF8.GetBytes("7061737323313233");

            //DECRYPT FROM DESCRIPTOR
            var encrypted = Convert.FromBase64String(encryptedValue);
            var decryptedFromJavascript = DecryptStringFromBytes(encrypted, bytestokey, iv);

            return decryptedFromJavascript;
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException(nameof(cipherText));
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException(nameof(key));
            }

            string plaintext = null;

            // Create an RijndaelManaged object
            // with the specified key and IV.
            using var rijAlg = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                FeedbackSize = 128,
                Key = key,
                IV = iv
            };
            //Settings


            // Create a decrytor to perform the stream transform.
            var decryption = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

            // Create the streams used for decryption.
            using var msDecrypt = new MemoryStream(cipherText);
            using var csDecrypt = new CryptoStream(msDecrypt, decryption, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            // Read the decrypted bytes from the decrypting stream
            // and place them in a string.
            plaintext = srDecrypt.ReadToEnd();

            return plaintext;
        }
        public static bool IsBase64String(this string base64)
        {
            base64 = base64.Trim();
            return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }
    }
}
