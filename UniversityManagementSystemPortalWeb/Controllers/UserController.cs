using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using UniversityManagementSystemPortal;
using UniversityManagementSystemPortal.Authorization;
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
        public async Task<ActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                using (var handler = new HttpClientHandler())
                {
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                    using (var client = new HttpClient(handler))
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("Token"));
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        var jsonPayload = JsonConvert.SerializeObject(login);
                        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                        HttpResponseMessage responseMessage = await client.PostAsync(url, content);

                        if (responseMessage.IsSuccessStatusCode)
                        {
                            var responseContent = await responseMessage.Content.ReadAsStringAsync();
                            Console.WriteLine(responseContent);

                            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseContent);
                            var token = apiResponse?.LoginView;

                            if (token != null && token.Token != null)
                            {
                                var identity = new ClaimsIdentity(new[]
                                 {
                                new Claim(ClaimTypes.Name, login.Username),
                                new Claim("Token", token.Token),
                                new Claim("UserId", token.UserId.ToString()),
                                new Claim(ClaimTypes.Role, token.Roles.ToString())

                                }, CookieAuthenticationDefaults.AuthenticationScheme);

                                var principal = new ClaimsPrincipal(identity);
                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

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
                            var responseContent = await responseMessage.Content.ReadAsStringAsync();

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
