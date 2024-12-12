using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Twitter.Entities;

namespace TwitterClone.Services
{
    public class UserService
    {
        private static readonly string apiBaseUrl = "http://localhost:51764/User/"; // Replace with your actual API URL
        private readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient();
        }

        // Add User
        public bool AddUser(User user)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = _httpClient.PostAsync(apiBaseUrl + "Add", content).Result; // Blocking on the async call

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        // Delete User
        public bool DeleteUser(string userId)
        {
            try
            {
                var response = _httpClient.DeleteAsync(apiBaseUrl + "Delete/" + userId).Result;

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        // Edit User
        public bool EditUser(User user)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = _httpClient.PutAsync(apiBaseUrl + "EditProfile", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        // Search User
        public User SearchUser(string username)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + "Search/" + username).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<User>(responseData);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        // Validate User (Login)
        public User ValidateUser(string username, string password)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + "Login/" + username + "/" + password).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<User>(responseData);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}
