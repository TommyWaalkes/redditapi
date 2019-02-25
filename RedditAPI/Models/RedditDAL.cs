using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace RedditAPI.Models
{
    public class RedditDAL
    {
        public static string Url = "https://www.reddit.com/r/awww/.json";
        //https://www.reddit.com/r/nba/.json
        public static string GetData(string Url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();

            return data;
        }

        public static string GetData()
        {
            HttpWebRequest request = WebRequest.CreateHttp(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();

            return data;
        }

        public static RedditPost GetPost(int i)
        {
            string output = GetData();
            RedditPost rp = new RedditPost(output, i);

            return rp;
        }

        public static List<RedditPost> GetPosts()
        {
            List<RedditPost> posts = new List<RedditPost>();
            string output = GetData();


            JObject redditJson = JObject.Parse(output);

            List<JToken> postTokens = redditJson["data"]["children"].ToList();

            for (int i = 0; i < postTokens.Count; i++)
            {
                RedditPost rp = new RedditPost();

                rp.Title = postTokens[i]["data"]["title"].ToString();
                rp.ImageURL = postTokens[i]["data"]["thumbnail"].ToString();
                rp.LinkURL = "http://reddit.com/" + postTokens[i]["data"]["permalink"].ToString();
                posts.Add(rp);
            }

            return posts;
        }
    }
}