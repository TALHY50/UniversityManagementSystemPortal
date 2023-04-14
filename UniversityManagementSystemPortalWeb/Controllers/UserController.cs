using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using UniversityManagementSystemPortal.ModelDto.NewFolder;

namespace UniversityManagementSystemPortalWeb.Controllers
{
    public class UserController : Controller
    {
        string url = "https://localhost:7092/api/Account/Authenticate";
        private readonly IHttpClientFactory _clientFactory;
        public UserController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
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

                            var token = JsonConvert.DeserializeObject<LoginView>(responseContent);

                            // Check if the user object is null
                            if (token != null)
                            {
                                // Store the token in a session variable
                                HttpContext.Session.SetString("Token", token.Token);

                                return RedirectToAction("Index", "Home"); // Redirect to the home page
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


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }


    }
}
