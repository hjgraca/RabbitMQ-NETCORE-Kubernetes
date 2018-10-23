using System;
using System.Threading.Tasks;
using MassTransit;
using Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Send
{
    public class HomeController : Controller
    {
        IBus _bus;
        private readonly IOptions<RabbitMqOptions> _settings;

        public HomeController(IBus bus,IOptions<RabbitMqOptions> settings)
        {
            _bus = bus;
            this._settings = settings;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] string message)
        {
            var addUserEndpoint = await _bus.GetSendEndpoint(new Uri($"rabbitmq://{_settings.Value.Host}/example-queue-3"));

            await addUserEndpoint.Send<IExampleMessage>(new 
            {
                CorrelationId = Guid.NewGuid(),
                StringData = message,
                DateTimeData = DateTime.Now
            });

            return RedirectToAction("Index");
        }
    }
}