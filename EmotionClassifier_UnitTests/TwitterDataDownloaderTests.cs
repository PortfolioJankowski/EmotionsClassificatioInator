using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Moq;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Client.V2;

namespace EmotionClassifier_UnitTests
{
    public class TwitterDataDownloaderTests
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<ITwitterClient> _mockTwitterClient;
        private readonly Mock<ISearchV2Client> _mockSearchV2Client;

        public TwitterDataDownloaderTests()
        {
            _mockConfiguration = new Mock<IConfiguration>();
            _mockTwitterClient = new Mock<ITwitterClient>();
            _mockSearchV2Client = new Mock<ISearchV2Client>();

            // Setup the mock Twitter client to return the mock search client
            _mockTwitterClient.Setup(client => client.SearchV2).Returns(_mockSearchV2Client.Object);
        }
        private TwitterDataDownloader CreateDownloader()
        {
            return new TwitterDataDownloader(_mockConfiguration.Object);
        }



    }
}
