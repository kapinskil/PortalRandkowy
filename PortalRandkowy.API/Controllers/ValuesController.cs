using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Controllers
{
    [Authorize]
    [ApiController]
    //http://localhost:5000/api
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(value);
        }

        [HttpPost]
        public async Task<IActionResult> AddValue([FromBody]Value value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditValue(int id,[FromBody]Value value)
        {
            var data = await _context.Values.FindAsync(id);
            data.Name = value.Name;
            _context.Values.Update(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValue(int id)
        {
            var data = await _context.Values.FindAsync(id);

            if(data == null)
            {
                return NoContent();
            }

            _context.Values.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }
    }
}