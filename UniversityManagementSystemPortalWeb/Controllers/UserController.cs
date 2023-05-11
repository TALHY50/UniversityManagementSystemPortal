using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using UniversityManagementSystemPortal.ModelDto.NewFolder;
using UniversityManagementSystemPortal.ModelDto.UserDto;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortalWeb.Controllers
{
    public class UserController : Controller
    {
        string url = "https://localhost:7092/api/Account/Authenticate";

        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserController> _logger;

        public UserController(IHttpClientFactory clientFactory, HttpClient httpClient, ILogger<UserController> logger)
        {
            _clientFactory = clientFactory;
            _httpClient = httpClient;
            _logger = logger;

        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                    using (var client = new HttpClient(handler))
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var jsonPayload = JsonConvert.SerializeObject(login);

                        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                        // Send the HTTP POST request to the authentication API
                        HttpResponseMessage responseMessage = client.PostAsync(url, content).Result;

                        if (responseMessage.IsSuccessStatusCode)
                        {
                            // Parse the response to get the JWT token
                            var responseContent = responseMessage.Content.ReadAsStringAsync().Result;
                            Console.WriteLine(responseContent); // Log the response content

                            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                            var token = apiResponse?.LoginView;



                            // Check if the user object is null
                            if (token != null && token.Token != null)
                            {
                                // Store the token in a session variable
                                HttpContext.Session.SetString("Token", token.Token);

                                return RedirectToAction("EmployeeRegister", "Employe");
                            }
                            else
                            {
                                ViewBag.Message = "Invalid Username or Password. The user object is null.";
                                return View();
                            }
                        }
                        else
                        {
                            var statusCode = responseMessage.StatusCode;
                            var responseContent = responseMessage.Content.ReadAsStringAsync().Result;

                            ViewBag.Message = $"Invalid Username or Password. Status Code: {statusCode}, Response: {responseContent}";
                            return View();
                        }
                    }
                }
            }
            else
            {
                return View();
            }
        }
        public IActionResult Registor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registor(RegistorUserDto registorUserDto)
        {
            if (ModelState.IsValid)
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                    using (var client = new HttpClient(handler))
                    {
                        client.BaseAddress = new Uri("https://localhost:7092/api/Account/");
                        client.DefaultRequestHeaders.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var jsonPayload = JsonConvert.SerializeObject(registorUserDto);

                        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                        // Send the HTTP POST request to the authentication API
                        HttpResponseMessage responseMessage = client.PostAsync("post", content).Result;

                        if (responseMessage.IsSuccessStatusCode)
                        {
                            return Json(new { success = true });
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "An error occurred while creating the User.");
                            return Json(new { success = false, error = "An error occurred while creating the User." });
                        }
                    }
                }
            }
            else
            {
                return Json(new { success = false, error = "Invalid model state." });
            }
        }


        public IActionResult Logout()
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }

    }
}
