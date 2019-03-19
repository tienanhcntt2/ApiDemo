using ApiDemo.config;
using ApiDemo.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiDemo.service
{
    public class UserService
    {
        private readonly Connect connection;
        private readonly IMongoCollection<User> user;
        public UserService(IConfiguration iPconfig)
        {
            var client = new MongoClient(iPconfig.GetConnectionString("FHS"));
            var db = client.GetDatabase("FHS");
            user = db.GetCollection<User>("User");;
        }

        public List<User> getList()
        {
            return user.Find(user => true).ToList();
        }

        public User getUser(string acount)
        {
            return user.Find<User>(user => user.acount == acount).FirstOrDefault();

        }
        public Message AddUser(User users)
        {
            Message message = new Message();
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            
            var _user = user.Find<User>(user => user.acount == users.acount).FirstOrDefault();
            if(_user != null)
            {
                message.success = false;
                message.message = "Fail";
            }
            else
            {

                User userss = new User();
                userss.acount = users.acount;
                userss.password = users.password;
                userss.acesstoken = token;
                userss.date = DateTime.Now.Date;
                user.InsertOne(userss);
                message.success = true;
                message.message = "Success";
            }
            return message;
        }

        public JObject login(User users)
        {
            JObject token = null;
            var _user = user.Find<User>(user => user.acount == users.acount && user.password == users.password).FirstOrDefault();
            if(_user != null)
            {
                token = new JObject(new JProperty("user", users.acount),
                   new JProperty("password", users.password),
                   new JProperty("message", "Ok"));
            }
            else
            {
                token = new JObject(
                   new JProperty("message", "Fail"));
            }
            return token;
        }
        /**
         * update user
         * */
        public User updateUser(string acount, User users)
        {
            string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var _user = getUser(acount);
            User userss = new User();
            userss.Id = _user.Id;
            userss.acount = acount;
            userss.password = users.password;
            userss.acesstoken = token;
            userss.date = DateTime.Now.Date;
            user.ReplaceOne(user => user.acount == acount, userss);
            return user.Find<User>(user => user.acount == acount).FirstOrDefault();
        }

        /*
         * function delete user
         */
        public Message deleteUser(string acount)
        {
            Message message = new Message();
            var _user = getUser(acount);
            if(_user == null)
            {
                message.success = false;
                message.message = "Faild";
            }
            else
            {
                message.success = true;
                message.message = "Ok";
                user.DeleteOne(user => user.acount == acount);
            }
            return message;
            
        }
      
    }
}
