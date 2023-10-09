using System;
using Microsoft.AspNetCore.Mvc;
using TicketReservation.Models;
using TicketReservation.Services;

namespace TicketReservation.Controllers;

[Controller]
[Route("api/[controller]")]

public class UserController : Controller {

    private readonly MongoDBService _mongoDBService;

    public UserController(MongoDBService mongoDBService) {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
   
    public async Task<List<User>> Get() {

        return await _mongoDBService.GetUsersAsync();
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user) {
        await _mongoDBService.CreateAsync(user);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);


    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user) {
        var user1 = await _mongoDBService.loginAsync(user.Email, user.Password);
        if (user1 == null) {
            return NotFound();
        }
        return Ok(user1);
    }
  

    // [HttpPut("{id}")]
    // public async Task<IActionResult> AddToUser(string id, [FromBody] string userId) {
    //   await _mongoDBService.AddUserAsync(id, userId);
    //     return NoContent();
    // }
    //write a method to update a user details
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] User user) {
        await _mongoDBService.UpdateUserAsync(id, user);
        return NoContent();
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }



}


