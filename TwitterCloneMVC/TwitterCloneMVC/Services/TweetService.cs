using System.Net.Http;
using System.Threading.Tasks;
using Twitter.Entities;
using System.Collections.Generic;
using System;

namespace Twitter.Services
{
    public class TweetService
    {
        private readonly HttpClient _httpClient;

        // Constructor: Initialize HttpClient for API calls
        public TweetService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://yourapiurl.com/api/") // API base URL
            };
        }

        // Method to get all tweets
        public async Task<IEnumerable<Tweet>> GetAllTweetsAsync()
        {
            var response = await _httpClient.GetAsync("tweets");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Tweet>>();
            }
            return null;
        }

        // Method to get a tweet by ID
        public async Task<Tweet> GetTweetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"tweets/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Tweet>();
            }
            return null;
        }

        // Method to create a new tweet
        public async Task<bool> CreateTweetAsync(Tweet tweet)
        {
            var response = await _httpClient.PostAsJsonAsync("tweets", tweet);
            return response.IsSuccessStatusCode;
        }

        // Method to update an existing tweet
        public async Task<bool> UpdateTweetAsync(Tweet tweet)
        {
            var response = await _httpClient.PutAsJsonAsync($"tweets/{tweet.TweetId}", tweet);
            return response.IsSuccessStatusCode;
        }

        // Method to delete a tweet
        public async Task<bool> DeleteTweetAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"tweets/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
