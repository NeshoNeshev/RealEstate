using BlazorBootstrap;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using RealEstate.Data.Models.DatabaseModels;
using RealEstate.Web.Shared.NotificationModels;
using RealEstate.Web.Shared.PropertyModels;
using RealEstate.Web.Shared.ViewModels;
using System.Net.Http.Json;

namespace RealEstate.Web.Client.Pages
{
    partial class Administration
    {

        private List<ToastMessage> messages = new List<ToastMessage>();
        private List<ToastMessage> requests = new List<ToastMessage>();
        private NotificationViewModel? notifi = new();
        private int datasetsCount = 0;
        private int labelsCount = 0;
        public List<PropertyViewModel>? viewModel { get; set; }
        protected override async Task OnInitializedAsync()
        {
    
            Http = ClientFactory.CreateClient("RealEstate.Web.ServerAPI.NoAuthenticationClient");
            try
            {
                //var result = await Http.GetFromJsonAsync<IEnumerable<PropertyViewModel>>("Property/AllProperties");
                //viewModel = result.ToList();
                var response = await Http.GetFromJsonAsync<NotificationViewModel>("Administration/GetNotifications");

                if (response != null)
                {
                    notifi = response;
                }
               
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }



            await base.OnInitializedAsync();
        }

       
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
               
            }
            await base.OnAfterRenderAsync(firstRender);
        }

    }
}
