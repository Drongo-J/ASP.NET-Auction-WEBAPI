using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebApiSignalR.Hubs;

namespace WebApiSignalR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private IHubContext<MessageHub> messageHub;

        public OfferController(IHubContext<MessageHub> messageHub)
        {
            this.messageHub = messageHub;
            if (!System.IO.File.Exists("mercedes.txt"))
            {
                FileHelper.Write("mercedes",5000);
            }
            if (!System.IO.File.Exists("chevrolet.txt"))
            {
                FileHelper.Write("chevrolet",1300);
            }
        }

        [HttpGet]
        public double Get()
        {
            //messageHub.Clients.All.SendOffersToUser(data);
            return FileHelper.Read();
        }

        [HttpGet("/Room")]
        public double GetRoom(string room)
        {
            //messageHub.Clients.All.SendOffersToUser(data);
            var value = FileHelper.Read(room);
            return value;
        }

        [HttpGet("/GetRooms")]
        public IActionResult GetRooms()
        {
            return Ok(FileHelper.GetRooms());
        }

        [HttpGet("/Increase")]
        public async void Get(double number)
        {
            var data = FileHelper.Read() + number;
            FileHelper.Write(data);
        }

        [HttpGet("/IncreaseRoom")]
        public double Get(string room,double number)
        {
            var data = FileHelper.Read(room) + number;
            FileHelper.Write(room,data);
            return data;
        }
    }
}
