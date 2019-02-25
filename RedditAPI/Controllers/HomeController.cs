﻿using Newtonsoft.Json.Linq;
using RedditAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RedditAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //HttpWebRequest request = WebRequest.CreateHttp("https://www.reddit.com/r/nba/.json");
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            //StreamReader rd = new StreamReader(response.GetResponseStream());
            //string data = rd.ReadToEnd();

            //JObject redditJson = JObject.Parse(data);
            //RedditPost p = new RedditPost(data, 0);

            RedditPost rp = RedditDAL.GetPost(0);

            return View(rp);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Reddit()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://www.reddit.com/r/nba/.json");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();
            List<RedditPost> output = RedditDAL.GetPosts();

            return View(output);
        }
    }
}