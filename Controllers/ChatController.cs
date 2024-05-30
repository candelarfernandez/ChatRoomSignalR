using ChatRoom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;

namespace ChatRoom.Controllers;

public class ChatController : Controller
{
    private static List<Bid> _bids = new List<Bid>();

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
        var roomBids = _bids.Where(b => b.ToRoomId == room).ToList();
        ViewBag.RoomId = room;
        ViewBag.Bids = roomBids;
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

    [HttpPost]
    public IActionResult AddBid([FromBody] Bid bid)
    {
        if (bid == null || bid.ToRoomId == 0 || string.IsNullOrEmpty(bid.FromUser.FullName) || bid.Amount <= 0)
        {
            return BadRequest("Datos de oferta inválidos");
        }

        var bidToSave = new Bid
        {
            Id = _bids.Count + 1,
            Amount = bid.Amount,
            FromUser = new User { FullName = bid.FromUser.FullName },
            ToRoomId = bid.ToRoomId,
            Timestamp = DateTime.Now
        };
        _bids.Add(bidToSave);
        return Ok();
    }
}
