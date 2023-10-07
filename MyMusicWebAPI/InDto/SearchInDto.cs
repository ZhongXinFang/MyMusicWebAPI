namespace MyMusicWebAPI.InDto;

public class SearchInDto
{
    private int mPageSize = 50;
    private int mPage = 1;

    public string? Q { get; set; } = "";
    public int Page
    {
        get => mPage; set
        {
            if (value < 0)
                mPage = 1;
            else
                mPage = value;
        }
    }
    public int PageSize
    {
        get => mPageSize; set
        {
            if (value < 200 && value > 0)
                mPageSize = value;
            else
                mPageSize = 200;
        }
    }
}
