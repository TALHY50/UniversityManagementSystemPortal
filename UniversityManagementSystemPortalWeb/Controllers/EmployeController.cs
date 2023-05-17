using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UniversityManagementSystemPortalWeb.ViewModels;
using UniversityManagementSystemPortal.ModelDto.Employee;
using UniversityManagementSystemPortal.Models.ModelDto.Department;
using UniversityManagementSystemPortal.Models.ModelDto.Position;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityManagementSystemPortalWeb.Authorization;

namespace UniversityManagementSystemPortalWeb.Controllers
{
    [CustomAuth("Admin")]
    public class EmployeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public EmployeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult EmployeeRegister()
        {
            var departments = GetDepartments();
            var positions = GetPositions();

            ViewBag.Departments = new SelectList(departments.Result, "Id", "Name");
            ViewBag.Positions = new SelectList(positions.Result, "Id", "Name");

            var model = new EmployeeUserViewModel();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
 
        public async Task<IActionResult> EmployeeRegister([FromForm] EmployeeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Saving the user details and getting the User ID
                var (success, userId, errors) = await SaveUserAsync(model.userDto);

                if (success)
                {
                    // Assigning the User ID to the Employee
                    model.createEmployeeDto.UserId = userId;

                    // Saving the employee
                    var (empSuccess, empErrors) = await SaveEmployeeAsync(model.createEmployeeDto);

                    if (empSuccess)
                    {
                        // Return success message to the user
                        return Json(new { success = true, message = "Employee registration successful." });
                    }
                    else
                    {
                        // Return the error messages to the user
                        return Json(new { success = false, message = "Error in saving Employee", errors = empErrors });
                    }
                }
                else
                {
                    // Return the error messages to the user
                    return Json(new { success = false, message = "Error in saving User", errors = errors });
                }
            }

            ViewBag.Departments = new SelectList(await GetDepartments(), "Id", "Name");
            ViewBag.Positions = new SelectList(await GetPositions(), "Id", "Name");

            return View(model);
        }
        private async Task<List<LookupDto>> GetDepartments()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:7092/api/Department/lookup");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<LookupDto>>();
            }

            return new List<LookupDto>();
        }

        private async Task<List<LookupPositiondto>> GetPositions()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "https://localhost:7092/api/Position/Lookup");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<LookupPositiondto>>();
            }

            return new List<LookupPositiondto>();
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
       
        private async Task<(bool success, List<string> errors)> SaveEmployeeAsync(CreateEmployeeDto createEmployeeDto)
        {
            try
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                    using (var client = new HttpClient(handler))
                    {
                        // Employee API call
                        client.BaseAddress = new Uri("https://localhost:7092/api/employees");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var content = new MultipartFormDataContent();
                        foreach (var prop in createEmployeeDto.GetType().GetProperties())
                        {
                            if (prop.GetValue(createEmployeeDto) != null)
                            {
                                content.Add(new StringContent(prop.GetValue(createEmployeeDto).ToString()), $"\"{prop.Name}\"");
                            }
                        }

                        HttpResponseMessage responseMessage = await client.PostAsync("", content);

                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return (true, new List<string>());
                        }
                        else
                        {
                            // Handle error when saving the Employee information
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

    }
}
