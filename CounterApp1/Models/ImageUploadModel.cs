using Microsoft.AspNetCore.Http;
namespace CounterApp1.Models;


public class ImageUploadModel
{
    public IFormFile ImageFile { get; set; }
}
