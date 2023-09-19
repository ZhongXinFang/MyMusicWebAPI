namespace MyMusicWebAPI.Service.FileService;

public class SongFileService
{
    public string? SaveFile(IFormFile formFile)
    {
        _ = formFile ?? throw new ArgumentNullException(nameof(formFile));
        string? fullName = null!;
        try
        {
            var fileExtension = Path.GetExtension(formFile.FileName);
            var fileName = Guid.NewGuid().ToString("N").ToUpper();
            fullName = $"{fileName}{fileExtension}";
            var path = Path.Combine(AppPathSettings.SongFileSaveDirectory,fullName);

            using Stream stream = formFile.OpenReadStream();
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes,0,bytes.Length);
            stream.Seek(0,SeekOrigin.Begin);
            FileStream fs = new(path,FileMode.Create);
            BinaryWriter bw = new(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }
        catch (Exception)
        {
            return null;
        }
        return fullName;
    }
}
