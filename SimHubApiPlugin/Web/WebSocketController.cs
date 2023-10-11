using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SimHubApiPlugin.DataManager;
using SimHubApiPlugin.Models;

namespace SimHubApiPlugin.Web
{
    [Route("[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly JsonSerializerSettings jsonSettings = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        private readonly IDataManager dataManager;

        public DataController(IDataManager dataManager)
        {
            this.dataManager = dataManager;
            jsonSettings.Converters.Add(new StringEnumConverter());
        }

        // GET api/values
        [HttpGet]
        public ActionResult<GameDataDto?> Get() => dataManager.GameDataDto;

        [HttpGet("/ws")]
        public async Task GetWs()
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                await Echo(webSocket);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }

        private async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var wasSuccess = int.TryParse(Encoding.UTF8.GetString(buffer), out var fps);
            if (!wasSuccess) fps = 60;
            var delay = 1000f / fps;
            var closed = false;

            _ = Task.Run(async () =>
            {
                var closeBuffer = new byte[1024 * 4];
                while (!closed)
                {
                    await webSocket.ReceiveAsync(new ArraySegment<byte>(closeBuffer), CancellationToken.None);
                    if (webSocket.State == WebSocketState.CloseReceived)
                    {
                        closed = true;
                    }
                }
            });

            await Task.Run(async () =>
            {
                while (!closed)
                {
                    var data = JsonConvert.SerializeObject(dataManager.GameDataDto, jsonSettings);
                    var serverMsg = Encoding.UTF8.GetBytes(data);
                    await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
                    await Task.Delay((int)delay);
                }
            });

        }
    }
}
