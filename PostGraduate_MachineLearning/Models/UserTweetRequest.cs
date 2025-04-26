namespace  Models;

public class UserTweetRequest
{
    public string PoliticalGroup { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.Now;

    public bool IsRequestValid()
    {
        if (string.IsNullOrEmpty(PoliticalGroup))
            return false;

        if (StartDate > DateTime.Now) 
            return false;

        return true;
    }
}
