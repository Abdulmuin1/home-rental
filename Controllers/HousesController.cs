using HouseStoreApi.Models;
using HouseStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
namespace HouseStoreApi.Controllers;

[Authorize( AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/[controller]")]
public class HousesController : ControllerBase
{
    private readonly HousesService _housesService;
    //private readonly RetailorsService _retailorService;

    public HousesController(HousesService housesService){
        _housesService = housesService;
       // _retailorService = retailorsService;

    }
        
    [HttpGet]
    public async Task<List<House>> Get() =>
        await _housesService.GetAsync();
    
    // [HttpGet("retailors")]
    // public async Task<List<Retailor>> GetRetailor() =>
    //     await _retailorService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<House>> Get(string id)
    {
        var book = await _housesService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        return book;
    }

    [HttpPost]
    public async Task<IActionResult> Post(House newHouse)
    {
        await _housesService.CreateAsync(newHouse);

        return CreatedAtAction(nameof(Get), new { id = newHouse.Id }, newHouse);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, House updatedHouse)
    {
        var book = await _housesService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        updatedHouse.Id = book.Id;

        await _housesService.UpdateAsync(id, updatedHouse);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var book = await _housesService.GetAsync(id);

        if (book is null)
        {
            return NotFound();
        }

        await _housesService.RemoveAsync(id);

        return NoContent();
    }
}