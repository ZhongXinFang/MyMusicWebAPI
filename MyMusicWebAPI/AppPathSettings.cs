namespace MyMusicWebAPI;

public static class AppPathSettings
{
    /// <summary>
    /// 当前程序的根目录
    /// </summary>
    public static string AppBaseDirectory { get; set; } = AppContext.BaseDirectory;

    #region 歌曲文件处理相关

    /// <summary>
    /// 歌曲文件处理目录
    /// </summary>
    public static string SongFileTempDirectory { get; set; } = Path.Combine(AppBaseDirectory,"SongFileTemp");
    /// <summary>
    /// 歌曲文件保持目录
    /// </summary>
    public static string SongFileSaveDirectory { get; set; } = Path.Combine(AppBaseDirectory,"SongFile");
    #endregion

}
