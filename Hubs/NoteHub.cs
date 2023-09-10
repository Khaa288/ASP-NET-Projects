using Microsoft.AspNetCore.SignalR;

namespace Note.API.Hubs {
    public class NoteHub : Hub {
        public async Task DisplayNotes () {
            await Clients.All.SendAsync("Update_Notes");
        }
    }
}