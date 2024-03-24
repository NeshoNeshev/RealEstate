using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using RealEstate.Web.Shared.DistrictModels;
using RealEstate.Web.Shared.InputModels;
using RealEstate.Web.Shared.ViewModels;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using RealEstate.Web.Shared;
using RealEstate.Web.Shared.PropertyModels;
using System.Data;

namespace RealEstate.Web.Client.Pages
{
    partial class CreateProperty
    {
        private bool updateStatus = false;
        List<ImageFile> filesBase64 = new List<ImageFile>();
        string message = "InputFile";
        bool isDisabled = false;
        private PropertyInputModel inputModel = new PropertyInputModel();
        private IndexViewModel? indexModel;
        private List<DistrictViewModel> districts = new List<DistrictViewModel>();
        private List<string>? heatings = new List<string>() { "ТЕЦ", "Газ", "Друго" };
        private List<string>? furnitureLevel = new List<string>() { "Обзаведен", "Необзаведен", "До ключ" };
        private List<string>? statuses = new List<string>() { " Buy", "Sold", "Rental" };
        private string selectedTown = "";
        protected override async Task OnInitializedAsync()
        {
            Http = ClientFactory.CreateClient("RealEstate.Web.ServerAPI");
            try
            {

                indexModel = await Http.GetFromJsonAsync<IndexViewModel>("Index");

            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            await base.OnInitializedAsync();
        }
        private void ChangeStatus(bool status)
        {
            this.updateStatus = status;
            StateHasChanged();
        }
        private void SelectionChanged(ChangeEventArgs e)
        {
            selectedTown = e.Value.ToString();
            LoadDistricts(selectedTown);
            // You can add any additional logic here upon selection change
        }
        async Task OnChange(InputFileChangeEventArgs e)
        {
            var files = e.GetMultipleFiles(); // get the files selected by the users
            foreach (var file in files)
            {
                var resizedFile = await file.RequestImageFileAsync(file.ContentType, 640, 480); // resize the image file
                var buf = new byte[resizedFile.Size]; // allocate a buffer to fill with the file's data
                using (var stream = resizedFile.OpenReadStream())
                {
                    await stream.ReadAsync(buf); // copy the stream to the buffer
                }
                filesBase64.Add(new ImageFile { base64data = Convert.ToBase64String(buf), contentType = file.ContentType, fileName = file.Name }); // convert to a base64 string!!
            }
            message = "Click UPLOAD to continue";
        }

        async Task Upload()
        {
            isDisabled = true;
            using (var msg = await Http.PostAsJsonAsync<List<ImageFile>>("Administration/UploadImages", filesBase64, System.Threading.CancellationToken.None))
            {
                isDisabled = false;
                if (msg.IsSuccessStatusCode)
                {
                    message = $"{filesBase64.Count} files uploaded";
                    filesBase64.Clear();
                }
            }
        }
        private void LoadDistricts(string townId)
        {
            // Assuming indexModel contains data about towns and districts
            var selectedTown = indexModel.Towns.FirstOrDefault(town => town.Id == townId);
            if (selectedTown != null)
            {
                districts = selectedTown.Districts.ToList();
            }
            else
            {
                districts.Clear(); // Clear districts if no town is selected
            }
        }
        private async Task OnSubmit()
        {
            Upload();
            //var response = await Http.PostAsJsonAsync("Administrator/CreateLawFirm", model);
            //if (response.IsSuccessStatusCode)
            //{
            //    Lawfirm = model.Name;
            //    success = true;
            //    StateHasChanged();
            //}
        }
    }
}
