using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;

namespace UniversityManagementSystemPortalWeb
{
    public class InstituteViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<InstituteDto> institutes = new List<InstituteDto>();

            using (var client = new HttpClient())
            {
                // Replace this URL with the actual URL of your API
                string apiURL = "https://localhost:7092/api/Institutes";
                client.BaseAddress = new Uri(apiURL);

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiURL);

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;

                    institutes = JsonConvert.DeserializeObject<List<InstituteDto>>(result);
                    institutes = institutes.OrderByDescending(x => x.CreatedAt).ToList();
                }
            }

            return View("~/Views/Components/Institute/_InstitiuteList.cshtml", institutes);
        }

    }
}

