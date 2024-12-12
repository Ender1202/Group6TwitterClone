using System.Net.Http;
using System.Threading.Tasks;
using Twitter.Entities;
using System.Collections.Generic;
using System;

namespace Twitter.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        // Constructor: Initialize HttpClient for API calls
        public UserService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://yourapiurl.com/api/") // API base URL
            };
        }

        // Method to get all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var response = await _httpClient.GetAsync("users");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<User>>();
            }
            return null;
        }

        // Method to get a user by ID
        public async Task<User> GetUserByIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"users/{userId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<User>();
            }
            return null;
        }

        // Method to create a new user
        public async Task<bool> CreateUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync("users", user);
            return response.IsSuccessStatusCode;
        }

        // Method to update an existing user
        public async Task<bool> UpdateUserAsync(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"users/{user.UserId}", user);
            return response.IsSuccessStatusCode;
        }

        // Method to delete a user
        public async Task<bool> DeleteUserAsync(string userId)
        {
            var response = await _httpClient.DeleteAsync($"users/{userId}");
            return response.IsSuccessStatusCode;
        }
    }
}
