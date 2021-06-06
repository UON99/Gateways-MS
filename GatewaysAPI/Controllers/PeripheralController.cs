using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using System.ComponentModel.DataAnnotations;

namespace GatewaysAPI.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class PeripheralController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PeripheralController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Peripheral
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Peripheral>>> GetPeripherals()
        {
            return await _context.Peripherals.ToListAsync();
        }

        // GET: api/Peripheral/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Peripheral>> GetPeripheral(Guid id)
        {
            var peripheral = await _context.Peripherals.FindAsync(id);

            if (peripheral == null)
            {
                return NotFound();
            }

            return peripheral;
        }

        // PUT: api/Peripheral/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeripheral(Guid id, Peripheral peripheral)
        {
            if (id != peripheral.Uid)
            {
                return BadRequest();
            }

            _context.Entry(peripheral).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeripheralExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Peripheral
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Peripheral>> PostPeripheral([FromBody]Peripheral peripheral)
        {
            Gateway gateway = _context.Gateways.Include(g => g.Peripherals).First(g => g.SerialNumber == peripheral.GatewaySerialNumber);
            
            if (gateway == null)
            {
                return null;
            }

            if(gateway.Peripherals.Count < 10) {
                if (peripheral.Status >= 1)
                    peripheral.Status = 1;
                else
                    peripheral.Status = 0;

                _context.Peripherals.Add(peripheral);
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetPeripheral", new { id = peripheral.Uid }, peripheral);
            }
            else
            {
                throw new ValidationException("No more than 10 devices are allowed for a Gateway");
            }
        }

        // DELETE: api/Peripheral/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Peripheral>> DeletePeripheral(Guid id)
        {
            var peripheral = await _context.Peripherals.FindAsync(id);
            if (peripheral == null)
            {
                return NotFound();
            }

            _context.Peripherals.Remove(peripheral);
            await _context.SaveChangesAsync();

            return peripheral;
        }

        private bool PeripheralExists(Guid id)
        {
            return _context.Peripherals.Any(e => e.Uid == id);
        }
    }
}
