using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Text;
using UniversityManagementSystemPortal;
using UniversityManagementSystemPortalWeb.ViewModels;

namespace UniversityManagementSystemPortalWeb.Controllers
{
    public class StudentController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public StudentController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StudentRegister()
        {
            //get department
            //get positions
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentRegister([FromForm] StudentUserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Save the User information first
                    var userResult = await SaveUserAsync(model.userDto);

                    if (userResult.success)
                    {
                        // Save the Student information with the returned UserId
                        model.studentDto.UserId = userResult.userId;
                        var studentResult = await SaveStudentAsync(model.studentDto);

                        if (studentResult.success)
                        {
                            return Json(new { success = true, message = "Student registered successfully." });
                        }
                        else
                        {
                            return Json(new { success = false, error = "An error occurred while saving the Student details.", errors = studentResult.errors });
                        }
                    }
                    else
                    {
                        return Json(new { success = false, error = "An error occurred while creating the User.", errors = userResult.errors });
                    }
                }
                else
                {
                    return Json(new { success = false, error = "Invalid model state." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        private async Task<(bool success, Guid userId, List<string> errors)> SaveUserAsync(RegistorUserDto userDto)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                    using (var client = new HttpClient(handler))
                    {
                        // Account API call
                        client.BaseAddress = new Uri("https://localhost:7092/api/Account/");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var jsonPayload = JsonConvert.SerializeObject(userDto, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
                        jsonPayload = jsonPayload.Insert(1, $"\"id\":\"{userDto.Id}\",");
                        Console.WriteLine("JSON Payload: " + jsonPayload); // Log the JSON payload
                        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                        HttpResponseMessage responseMessage = await client.PostAsync("post", content);

                        if (responseMessage.IsSuccessStatusCode) // Check if Account API returns 200 status code
                        {
                            var resultContent = await responseMessage.Content.ReadAsStringAsync();
                            Console.WriteLine("API Result Content: " + resultContent); // Log the result content
                            var result = JsonConvert.DeserializeObject<RegistorUserDto>(resultContent);

                            if (result != null && result.Id != Guid.Empty)
                            {
                                return (true, result.Id, new List<string>());
                            }
                            else
                            {
                                // Handle error when retrieving User ID
                                return (false, Guid.Empty, new List<string> { "An error occurred while retrieving the User ID. Result content: " + resultContent });
                            }
                        }
                        else
                        {
                            // Handle error when creating the User
                            var errorContent = await responseMessage.Content.ReadAsStringAsync();
                            var errors = JsonConvert.DeserializeObject<List<string>>(errorContent);
                            return (false, Guid.Empty, errors);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, Guid.Empty, new List<string> { ex.Message });
            }
        }

        private async Task<(bool success, List<string> errors)> SaveStudentAsync(AddStudentDto studentDto)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                    using (var client = new HttpClient(handler))
                    {
                        // Student API call immediately after successful Account API call
                        client.BaseAddress = new Uri("https://localhost:7092/api/Student/");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var content = new MultipartFormDataContent();
                        foreach (var prop in studentDto.GetType().GetProperties())
                        {
                            if (prop.GetValue(studentDto) != null)
                            {
                                content.Add(new StringContent(prop.GetValue(studentDto).ToString()), $"\"{prop.Name}\"");
                            }
                        }

                        HttpResponseMessage responseMessage = await client.PostAsync("https://localhost:7092/api/Student", content);

                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return (true, new List<string>());
                        }
                        else
                        {
                            // Handle error when saving the Student information
                            var errorContent = await responseMessage.Content.ReadAsStringAsync();
                            var errors = JsonConvert.DeserializeObject<List<string>>(errorContent);
                            return (false, errors);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, new List<string> { ex.Message });
            }
        }


        [HttpGet]
        public IActionResult GetCsrfToken()
        {
            return Content(HttpContext.Request.Cookies["__RequestVerificationToken"], "text/plain");
        }
    }
}
