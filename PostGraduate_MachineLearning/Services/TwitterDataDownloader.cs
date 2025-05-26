using EmotionClassifier.Models.Attributes;
using EmotionClassifier.Models.FormModels;
using Microsoft.Extensions.Configuration;
using Models;
using Models.Exceptions;
using System.Diagnostics;
using Tweetinvi;

namespace Services;

[ServiceRegistration(typeof(IDataDownloader), Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
public class TwitterDataDownloader : IDataDownloader
{
    private readonly IConfiguration _manager;

    public TwitterDataDownloader(IConfiguration manager)
    {
        this._manager = manager;
    }
    public async  Task<IEnumerable<string>> DownloadTwitterDataAsync(MenuFormModel request)
    {
        string query = $"from:{request.ChoosenParty} -is:retweet lang:pl";
        var folderPath = Path.Combine(AppContext.BaseDirectory, "Downloads");
        var filePath = Path.Combine(folderPath, $"{request.ChoosenParty}.txt");

        if (File.Exists(filePath))
        {
            var data = await File.ReadAllLinesAsync(filePath);
            var filteredData = data.Where(tweet => tweet.Contains("💬"))
                .Select(tweet => tweet.Replace(",", "")).ToList();

            return filteredData;
        }


        try
        {
            var client = GetTwitterClient();
            var tweets = await client.SearchV2.SearchTweetsAsync(query);

            if (tweets == null || tweets.Tweets.Length <= 0)
                throw new TweetsNotFoundException();

            
            Directory.CreateDirectory(folderPath);
            var tweetText = tweets.Tweets
               .Where(t => t.Text.Contains("💬") && t.CreatedAt >= request.ChoosenStartDate)
               .Select(t => t.Text.Replace(",", ""))
               .ToList();

            await File.WriteAllLinesAsync(filePath, tweetText);
            return tweetText;
        }
        catch (Tweetinvi.Exceptions.TwitterException ex) 
        {

            Debug.WriteLine(ex.Message);
            return Enumerable.Empty<string>();
        }
        catch (TweetsNotFoundException ex)
        {
            return Enumerable.Empty<string>();
        }
    }

    private static TwitterClient GetTwitterClient()
    {
        var apiKey = System.Configuration.ConfigurationManager.AppSettings["APIKey"];
        var apiSecret = System.Configuration.ConfigurationManager.AppSettings["APIKeySecret"];
        var accessToken = System.Configuration.ConfigurationManager.AppSettings["AccessToken"];
        var accessTokenSecret = System.Configuration.ConfigurationManager.AppSettings["AccessTokenSecret"];

        var client = new TwitterClient(apiKey, apiSecret, accessToken, accessTokenSecret);
        return client;
    }
}

public interface IDataDownloader
{
    Task<IEnumerable<string>> DownloadTwitterDataAsync(MenuFormModel request);
}

