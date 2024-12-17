using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using TwitterClone.Entities;
using TwitterClone.Repositories;
using TwitterClone.Services;

namespace TwitterClone.Controllers
{
    [RoutePrefix("User")]
    public class UserController : ApiController
    {
        IUser rep;
        public UserController()
        {
            rep = new UserService();
        }
        [HttpPost, Route("Add")]
        public IHttpActionResult AddUser([FromBody] User user)
        {
            rep.Add(user);
            return Ok("Added" + user.UserName);
        }
        [HttpDelete, Route("Delete/{id}")]
        public IHttpActionResult DeleteUser(string id)
        {
            try
            {
                rep.Delete(id);
                return Ok("Deleted");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut, Route("EditProfile")]
        public IHttpActionResult EditUser([FromBody] User user)
        {
            try
            {
                rep.Edit(user);
                return Ok("Edited " + user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpGet, Route("Search/{username}")]
        public IHttpActionResult Search(string username)
        {
            try
            {
                var getusers = rep.Get(username);
                return Ok(getusers);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("Login/{username}/{password}")]
        public IHttpActionResult ValidateUser(string username, string password)
        {
            try
            {
                var user = rep.Validate(username, password);
                return Ok(user);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpPost, Route("{userId}/UploadPicture")]
        public IHttpActionResult UploadPicture(string userId)
        {
            var httprequest = HttpContext.Current.Request;
            var file = httprequest.Files["profilePicture"];
            try
            {
                if (file == null || file.ContentLength == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                var path = rep.UploadPic(userId, file);
                return Ok(path);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
