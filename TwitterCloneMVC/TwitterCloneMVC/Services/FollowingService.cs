using System.Net.Http;
using System.Threading.Tasks;
using Twitter.Entities;
using System.Collections.Generic;
using System;

namespace Twitter.Services
{
    public class FollowingService
    {
        private readonly HttpClient _httpClient;

        // Constructor: Initialize HttpClient for API calls
        public FollowingService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://yourapiurl.com/api/") // API base URL
            };
        }

        // Method to get all followings for a user
        public async Task<IEnumerable<Following>> GetAllFollowingsAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"followings/user/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<Following>>();
            }
            return null;
        }

        // Method to get a following by user and follower IDs
        public async Task<Following> GetFollowingByIdAsync(string userId, string followingId)
        {
            var response = await _httpClient.GetAsync($"followings/{userId}/{followingId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Following>();
            }
            return null;
        }

        // Method to create a following
        public async Task<bool> CreateFollowingAsync(Following following)
        {
            var response = await _httpClient.PostAsJsonAsync("followings", following);
            return response.IsSuccessStatusCode;
        }

        // Method to delete a following
        public async Task<bool> DeleteFollowingAsync(int followingId)
        {
            var response = await _httpClient.DeleteAsync($"followings/{followingId}");
            return response.IsSuccessStatusCode;
        }
    }
}
