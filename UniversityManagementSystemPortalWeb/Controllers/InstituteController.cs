using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UniversityManagementSystemPortal.ModelDto.Institute;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.Models.ModelDto.Institute;

namespace UniversityManagementSystemPortalWeb.Controllers
{
    public class InstituteController : Controller
    {
        private readonly HttpClient _httpClient;

        public InstituteController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            // Just return the view since we will load the data asynchronously with AJAX
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetInstitutes()
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
                }
            }

            return PartialView(institutes);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InstituteCreateDto instituteCreateDto)
        {
            if (instituteCreateDto == null)
            {
                return Json(new { success = false, error = "Invalid data." });
            }

            string apiUrl = "https://localhost:7092/api/Institutes";
            string jsonContent = JsonConvert.SerializeObject(instituteCreateDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the institute.");
                return Json(new { success = false, error = "An error occurred while creating the institute." });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetInstituteById(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7092/api/Institutes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var institute = await response.Content.ReadAsAsync<InstituteUpdateDto>();
                return Json(new { success = true, data = institute });
            }
            else
            {
                return Json(new { success = false, error = "Failed to fetch the institute." });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(InstituteUpdateDto instituteUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, error = "Invalid model state." });
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7092/api/Institutes/{instituteUpdateDto.Id}", instituteUpdateDto);

                if (response.IsSuccessStatusCode)
                {
                    return Json(new { success = true });
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update the institute.");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error updating institute: {errorContent}");
                    return Json(new { success = false, error = "Failed to update the institute." });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the institute.");
                Console.WriteLine($"Exception: {ex.Message}");
                return Json(new { success = false, error = "An error occurred while updating the institute." });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7092/api/Institutes/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, error = "Failed to delete the institute." });
            }
        }

    }
}
