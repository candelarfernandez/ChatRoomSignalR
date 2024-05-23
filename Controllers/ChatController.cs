using Microsoft.AspNetCore.Mvc;

namespace ChatRoom.Controllers;

public class ChatController : Controller
{
    public static Dictionary<int, string> Rooms =
        new Dictionary<int, string>()
        {
            {1, "Cervezas"},
            {2, "Programacion"},
            {3, "Moda"}
        };
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Room(int room)
    {
        return View("Room", room);
    }

    public IActionResult CreateRoom()
    {

        return View();
    }

    [HttpPost]
    public IActionResult CreateRoom(string roomName)
    {
        if (!string.IsNullOrEmpty(roomName))
        {
            int newRoomId = Rooms.Count + 1;
            Rooms.Add(newRoomId, roomName);
            return RedirectToAction("Room", new { room = newRoomId });
        }

        return View();
    }
}
