
using System;
using System.Collections.Generic;
using System.Data.Services;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Text;

namespace VII
{
    public class RequestHandler : IRequestHandler
    {
        public RequestHandler()
        {
        }


        public string GetCustomer(string url, X509Certificate certificate, string contentType = "text/json")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.ClientCertificates.Add(certificate);
            request.UserAgent = "Secure REST Client";
            request.Method = HttpMethod.Get.ToString();
            request.ContentType = contentType;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            return content;
        }
        public string DeleteCustomer(string url, X509Certificate certificate,  string contentType = "text/json")
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
           request.ClientCertificates.Add(certificate);
            request.UserAgent = "Secure REST Client";
            request.Method = HttpMethod.Delete.ToString();
            request.ContentType = contentType;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            return content;
        }

        public string AddCustomer(string baseURL, X509Certificate certificate, Dictionary<string, string> postParameters, string contentType = "application/x-www-form-urlencoded")
        {
            string postData = "";
            foreach (string key in postParameters.Keys)
            {
                postData += WebUtility.UrlEncode(key) + "="
                + WebUtility.UrlEncode(postParameters[key]) + "&";
            }
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(baseURL);
            request.ClientCertificates.Add(certificate);
            request.UserAgent = "Secure REST Client";
            //Here we set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            //Here we set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = HttpMethod.Post.ToString();
            byte[] data = Encoding.ASCII.GetBytes(postData);
            //Here we set the ContentType property of the WebRequest.
            request.ContentType = contentType;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            //Here we set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            //Here we get the request stream.
            Stream dataStream = request.GetRequestStream();
            //Here we write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            //Here we close the Stream object.
            dataStream.Close();
            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            return content;
        }
        public string UpdateCustomer(string url, Dictionary<string, string> updateParameters,  X509Certificate certificate, string contentType = "application/x-www-form-urlencoded")
        {
            string updateData = "";
            foreach (string key in updateParameters.Keys)
            {
                updateData += WebUtility.UrlEncode(key) + "="
                + WebUtility.UrlEncode(updateParameters[key]) + "&";
            }
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.ClientCertificates.Add(certificate);
            request.UserAgent = "Secure REST Client";
            //Here we set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            //Here we set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = HttpMethod.Put.ToString();
            byte[] data = Encoding.ASCII.GetBytes(updateData);
            //Here we set the ContentType property of the WebRequest.
            request.ContentType = contentType;
            byte[] byteArray = Encoding.UTF8.GetBytes(updateData);
            //Here we set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            //Here we get the request stream.
            Stream dataStream = request.GetRequestStream();
            //Here we write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            //Here we close the Stream object.
            dataStream.Close();
            var content = string.Empty;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }
            return content;
        }
        public Message ProcessRequestForMessage(Stream messageBody)
        {
            throw new NotImplementedException();
        }
    }
}