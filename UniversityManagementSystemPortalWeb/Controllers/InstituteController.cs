using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UniversityManagementSystemPortal.ModelDto.Institute;
using System.Net.Http.Formatting;
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

            return View(institutes);
        }

       
        [HttpPost]
        public async Task<IActionResult> Create(InstituteCreateDto instituteCreateDto)
        {
            if (instituteCreateDto == null)
            {
                return BadRequest();
            }

            string apiUrl = "https://localhost:7092/api/Institutes";
            string jsonContent = JsonConvert.SerializeObject(instituteCreateDto);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the institute.");
                return View(instituteCreateDto);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7092/api/Institutes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var instituteUpdateDto = await response.Content.ReadAsAsync<InstituteUpdateDto>();
                return View(instituteUpdateDto);
            }
            else
            {
                // Handle error, e.g., display an error message or redirect to an error page
                return RedirectToAction("Error");
            }
        }

        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Update(InstituteUpdateDto instituteUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(instituteUpdateDto);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7092/api/Institutes/{instituteUpdateDto.Id}", instituteUpdateDto);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to update the institute.");
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error updating institute: {errorContent}");
                    return View(instituteUpdateDto);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while updating the institute.");
                Console.WriteLine($"Exception: {ex.Message}");
                return View(instituteUpdateDto);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7092/api/Institutes/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the institute.";
                return RedirectToAction("Index");
            }
        }

    }
}
