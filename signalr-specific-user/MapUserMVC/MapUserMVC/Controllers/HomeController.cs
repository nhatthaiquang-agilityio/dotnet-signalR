using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MapUserMVC.Models;
using Microsoft.AspNetCore.SignalR;
using MapUserMVC.Hubs;

namespace MapUserMVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;

        public HomeController(IHubContext<NotificationUserHub> notificationUserHubContext,
           IUserConnectionManager userConnectionManager)
        {
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult UserBoard()
        {
            ViewData["Message"] = "User Board";

            return View();
        }

        public IActionResult AdminUser()
        {
            ViewData["Message"] = "Admin User.";

            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> AdminUser([FromForm] string userId, [FromForm] string message)
        {
            //get the connection from the
            var connections = _userConnectionManager.GetUserConnections(userId);
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    //send to user
                    await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", message);
                }
            }

            return View();
        }
    }
}
