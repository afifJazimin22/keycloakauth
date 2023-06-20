using System.Net.Http;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginMicroService.Helper;
using LoginMicroService.Models;
using Newtonsoft.Json;

namespace LoginMicroService.Services
{
    public class AuthService : ServerCertificate,IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Response> Login(User user)
        {
        
            Response rs = new Response();




            try
            {
                
            HttpClientHandler handler = new HttpClientHandler();

            handler.ServerCertificateCustomValidationCallback = ServerCertificateCustomValidation;

            HttpClient client = new HttpClient(handler);

            string apiurl = _configuration.GetValue<string>("Keycloak:apiurl");
            string clientid = _configuration.GetValue<string>("Keycloak:client_id");
            string clientsecret = _configuration.GetValue<string>("Keycloak:client_secret");
            string userName = user.username;
            string password = user.password;
            var request = new HttpRequestMessage(HttpMethod.Post, apiurl);

            var collection = new List<KeyValuePair<string, string>>();
                collection.Add(new ("client_id", clientid));
                collection.Add(new ("client_secret", clientsecret));
                collection.Add(new ("username", userName));
                collection.Add(new ("password", password));
                collection.Add(new ("grant_type", "password"));
            
            var content = new FormUrlEncodedContent(collection);

            request.Content = content;

            HttpResponseMessage response = await client.SendAsync(request);

            response.Content.ReadAsStringAsync();

            response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    
                    var result = await response.Content.ReadAsStringAsync();
                    var json = JsonConvert.DeserializeObject<Token>(result);

                    rs.AccessToken = json.access_token;
                    rs.RefreshToken = json.refresh_token;
                    rs.TokenType = json.token_type;
                    rs.ExpiresIn = json.expires_in;
                    rs.RefreshExpiresIn = json.refresh_expires_in;
                    rs.Success = true;
                    rs.Message = "User Authenticated successfully";
                }
                else{
                    rs.AccessToken = null;
                    rs.RefreshToken = null;
                    rs.TokenType = null;
                    rs.ExpiresIn = null;
                    rs.RefreshExpiresIn = null;
                    rs.Success = false;
                    rs.Message = "Invalid Credentials";
                    // response.RequestMessage = "Invalid credentials";
                }
            }
            catch (HttpRequestException ex)
            {

                rs.AccessToken = null;
                rs.RefreshToken = null;
                rs.TokenType = null;
                rs.ExpiresIn = null;
                rs.RefreshExpiresIn = null;
                rs.Success = false;
                rs.Message = "Invalid User Credentials";
                return rs;
            }


            return rs;

        }
    }
}