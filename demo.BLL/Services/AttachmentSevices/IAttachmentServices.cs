global using Microsoft.AspNetCore.Http;
using System.Globalization;
namespace demo.BLL.Services.AttachmentSevices;

public interface IAttachmentServices 
{
    public string? Upload(IFormFile file, string FolderName);
    public bool Delete (String fileName);
}
