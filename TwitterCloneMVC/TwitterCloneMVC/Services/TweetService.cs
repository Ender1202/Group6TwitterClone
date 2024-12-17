using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Twitter.Models;

namespace TwitterClone.Services
{
    public class TweetService
    {
        private static readonly string apiBaseUrl = "http://localhost:51764/Tweet/"; // Replace with your actual API URL
        private readonly HttpClient _httpClient;

        public TweetService()
        {
            _httpClient = new HttpClient();
        }

        // Post Tweet
        public bool CreateTweet(Tweet tweet)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(tweet);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = _httpClient.PostAsync(apiBaseUrl + "PostTweet", content).Result;

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Edit Tweet
        public bool EditTweet(Tweet tweet)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(tweet);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = _httpClient.PutAsync(apiBaseUrl + "EditTweet", content).Result;

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Delete Tweet
        public bool DeleteTweet(int tweetId)
        {
            try
            {
                var response = _httpClient.DeleteAsync(apiBaseUrl + "DeleteTweet/" + tweetId).Result;

                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        // Get All Tweets by User
        public List<Tweet> GetAllTweets(string userId)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + userId + "/tweets").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Tweet>>(responseData);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
        public Tweet GetTweet(int tid)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + "/GetTweetById/" + tid).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Tweet>(responseData);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        // Get Feed for User
        public List<Tweet> GetUserFeed(string userId)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + userId + "/feed").Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Tweet>>(responseData);
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public int? GetTweetCount(string userId)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + "/TweetCount" + userId).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<int>(responseData);
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
