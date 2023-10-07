namespace MyMusicWebAPI.OutDto;

public class SongOutListDto<T>
{
    public SongOutListDto(int count,int currentPage,int totalPage,int pageSize,IEnumerable<T> data)
    {
        Count = count;
        CurrentPage = currentPage;
        TotalPage = totalPage;
        Data.AddRange(data);
        PageSize = pageSize;
    }

    public int Count { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPage { get; set; }
    public int PageSize { get; set; }
    public List<T> Data { get; set; } = new List<T>();
}
