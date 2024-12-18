using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Twitter.Models;

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
                var response = _httpClient.DeleteAsync(apiBaseUrl + "Delete/"+userId).Result;

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
        public List<User> SearchUser(string username)
        {
            try
            {
                var response = _httpClient.GetAsync(apiBaseUrl + "Search/" + username).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<User>>(responseData);
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

        // Method to send an email
        public bool SendEmail(string recipientEmail, string subject, string body)
        {
            try
            {
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587; // TLS Port
                string senderEmail = "sprintdmn@gmail.com";
                string senderPassword = "SprintDmn@006"; // or App Password

                // Create the email message
                

                // Configure the SMTP client
                SmtpClient smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true
                };
                MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail, subject, body);
                // Send the email
                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception)
            {
                // Log the error and return false in case of failure
                return false;
            }
        }

        // Method to notify the user about a tweet
        public void NotifyTweet(string recipientEmail, string tweetContent)
        {
            string subject = "New Tweet Notification";
            string body = $"You have a new tweet: {tweetContent}";
            SendEmail(recipientEmail, subject, body);
        }

        // Method to notify the user about a follow
        public void NotifyFollow(string recipientEmail, string followerName)
        {
            string subject = "New Follower Notification";
            string body = $"You have a new follower: {followerName}";
            SendEmail(recipientEmail, subject, body);
        }

        // Method to send registration success notification
        public void NotifyRegistration(string recipientEmail)
        {
            string subject = "Registration Successful";
            string body = "Congratulations! You have successfully registered for ThreadX. Welcome aboard!";
            SendEmail(recipientEmail, subject, body);
        }
    }
}
