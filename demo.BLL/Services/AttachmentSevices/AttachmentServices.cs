using System.Globalization;

namespace demo.BLL.Services.AttachmentSevices;

public class AttachmentServices : IAttachmentServices
{
    private List<string> allowedExtention = [".png", ".jpeg",".jpg"];
    private const int maxSize = 1024000;
    public string? Upload(IFormFile file, string FolderName)
    {
        //1. check for extention 
        var extention = Path.GetExtension(file.FileName).ToLowerInvariant();
        if(! allowedExtention.Contains(extention) ) return null;
        //2. chech Size 
        if(file.Length > maxSize ) return null;
        //3. Get located folder path
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\files", FolderName);
        //4. make the attachment name unique => guid
        var fileName = $"{Guid.NewGuid()}{extention}";
        //5. combine the filepath
        var filePath = Path.Combine(folderPath, fileName);
        //6. create file stream to be used for copy {unmanged}
        using var stream = new FileStream(filePath, FileMode.Create);
        //7. Use stream to copy the file
        file.CopyTo(stream);
        //8. return the fileName => to stored in DB
        return fileName; 
    }
    public bool Delete(String fileName)
    {
        if(!File.Exists(fileName)) return false;
        File.Delete(fileName);
        return true;
    }
}
