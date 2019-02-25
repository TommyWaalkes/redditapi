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
        //https://www.reddit.com/r/nba/.json
        public static string GetData(string Url)
        {
            HttpWebRequest request = WebRequest.CreateHttp(Url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());
            string data = rd.ReadToEnd();

            return data;
        }

        public static RedditPost GetPost(int i)
        {
            string output = GetData("https://www.reddit.com/r/nba/.json");
            RedditPost rp = new RedditPost(output, i);

            return rp;
        }

        public static List<RedditPost> GetPosts()
        {
            List<RedditPost> posts = new List<RedditPost>();
            string ouput = GetData("https://www.reddit.com/r/nba/.json");


            JObject redditJson = JObject.Parse(ouput);

            List<JToken> postTokens = redditJson["data"]["children"].ToList();

            for (int i = 0; i < posts.Count; i++)
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