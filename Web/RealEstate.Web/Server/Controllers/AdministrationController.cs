using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Web.Shared;
using System.Data;
using System.IO;
namespace RealEstate.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministrationController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        public AdministrationController(IWebHostEnvironment env)
        {
            this.env = env;
        }
        [HttpPost("UploadImages")]
        public async Task Post([FromBody] ImageFile[] files)
        {
            var imagesPath = new List<string>();
            foreach (var file in files)
            {

                var buf = Convert.FromBase64String(file.base64data);
                var path = Path.Combine(env.ContentRootPath, "wwwroot/images", file.fileName);
                await System.IO.File.WriteAllBytesAsync(path, buf);
                var relativePath = Path.Combine("images", file.fileName);
                imagesPath.Add(relativePath);

            }
        }
      
    }
}
