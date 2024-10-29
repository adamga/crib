using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Crib.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendGameUpdate(string gameState)
        {
            await Clients.All.SendAsync("ReceiveGameUpdate", gameState);
        }

        public async Task SendScoreUpdate(int player1Score, int player2Score)
        {
            await Clients.All.SendAsync("ReceiveScoreUpdate", player1Score, player2Score);
        }

        public async Task SendMoveValidation(bool isValid, string message)
        {
            await Clients.All.SendAsync("ReceiveMoveValidation", isValid, message);
        }
    }
}
