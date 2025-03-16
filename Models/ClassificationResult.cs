using Microsoft.ML.Data;

namespace Models;

public class ClassificationResult
{
    [LoadColumn(0)]
    public string TweetText { get; set; } = string.Empty;
    [LoadColumn(1)]
    public string Emotion   { get; set; } = string.Empty ;
}

