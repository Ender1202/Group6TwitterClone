using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace TwitterClone.Services
{
    public class FollowService
    {
        private static readonly string apiBaseUrl = "http://localhost:51764/Follow/"; // Replace with your actual API URL
        private readonly HttpClient _httpClient;

        public FollowService()
        {
            _httpClient = new HttpClient();
        }

        // Follow User
        public bool FollowUser(string userId, string followerId)
        {
            try
            {
                var response = _httpClient.PostAsync(apiBaseUrl + "follow/" + userId + "/" + followerId, null).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Unfollow User
        public bool UnfollowUser(string userId, string followingId)
        {
            try
            {
                var response = _httpClient.DeleteAsync(apiBaseUrl + "unfollow/" + userId + "/" + followingId).Result;
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Get Followers
        public List<string> GetFollowers(string userId)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + "Followers/" + userId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<string>>(responseData);
                }

                return new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        // Get Following Users
        public List<string> GetFollowingUsers(string userId)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + "Following/" + userId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<string>>(responseData);
                }

                return new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        // Get Followers Count
        public int GetFollowersCount(string userId)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + "FollowersCount/" + userId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return int.Parse(responseData);
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        // Get Following Count
        public int GetFollowingCount(string userId)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + "FollowingCount/" + userId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return int.Parse(responseData);
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        // Is Following
        public bool IsFollowing(string userId, string followingId)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl+userId+"/isFollowing/"+followingId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    bool result = JsonConvert.DeserializeObject<bool>(responseData);
                    return result;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
