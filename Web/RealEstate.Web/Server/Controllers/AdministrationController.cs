using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Services;
using RealEstate.Web.Shared;
using RealEstate.Web.Shared.NotificationModels;
using RealEstate.Web.Shared.PropertyModels;
using System.Data;
using System.IO;
using static RealEstate.Web.Client.Pages.About;

namespace RealEstate.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministrationController : ControllerBase
    {
        private readonly IWebHostEnvironment env;
        private readonly IPropertyService propertyService;
        private readonly INotificationService notificationService;

        public AdministrationController(IWebHostEnvironment env, IPropertyService propertyService, INotificationService notificationService)
        {
            this.env = env;
            this.propertyService = propertyService;
            this.notificationService = notificationService;
        }
        [HttpPost("UploadImages")]
        public async Task<List<string>> Post([FromBody] ImageFile[] files)
        {
            var imagesPath = new List<string>();
            foreach (var file in files)
            {

                var buf = Convert.FromBase64String(file.base64data);
                var path = Path.Combine(env.ContentRootPath, "wwwroot/images", file.fileName);
                await System.IO.File.WriteAllBytesAsync(path, buf);
                var relativePath = $"images/ +{Guid.NewGuid().ToString()}+{file.fileName}";//Path.Combine("images", file.fileName);
                imagesPath.Add(relativePath);

            }
            return imagesPath;
        }
        [HttpPost("CreateProperty")]
        public async Task CreateProperty([FromBody] PropertyInputModel model)
        {

            try
            {
                await this.propertyService.Create(model);
            }
            catch (Exception)
            {

                throw;
            }
          
        }
        [HttpPost("SendRequest")]
        public async Task SendRequest([FromBody] RequestModel model)
        {
            try
            {
                await this.propertyService.Requsts(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost("SendMesage")]
        public async Task SendMesage([FromBody] MessageModel model)
        {
            try
            {
                await this.propertyService.Message(model);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("GetNotifications")]
        public async Task<NotificationViewModel> GetNotifications()
        {
            var notifications = new NotificationViewModel();
            notifications.Messages = await this.notificationService.GetAllMessages<MessageViewModel>();
            notifications.Requests = await this.notificationService.GetAllRequests<RequestViewModel>();

            return notifications;
        }
        [HttpGet("GetPropertiesByTownId")]
        public async Task<IEnumerable<PropertyViewModel>> GetPropertiesByTownId(string Id)
        {
            var response = new List<PropertyViewModel>();
            var sresponse = await this.propertyService.GetAll<PropertyViewModel>(Id);
            response = sresponse.ToList();
            return response;
        }
    }
}
