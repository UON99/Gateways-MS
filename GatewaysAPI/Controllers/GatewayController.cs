using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace GatewaysAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class GatewayController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GatewayController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Gateway
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Gateway>>> GetGateways()
        {
            return await _context.Gateways.Include(p => p.Peripherals).ToListAsync();
        }

        // GET: api/Gateway/5
        [HttpGet("{id}")]
        public ActionResult<Gateway> GetGateway(string id)
        {
            var gateway = _context.Gateways.Include(p => p.Peripherals).First(p => p.SerialNumber == id);

            if (gateway == null)
            {
                return NotFound();
            }

            return gateway;
        }

        // PUT: api/Gateway/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGateway(string id, Gateway gateway)
        {
            if (id != gateway.SerialNumber)
            {
                return BadRequest();
            }

            _context.Entry(gateway).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GatewayExists(id))
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

        // POST: api/Gateway
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Gateway>> PostGateway(Gateway gateway)
        {
            _context.Gateways.Add(gateway);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GatewayExists(gateway.SerialNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGateway", new { id = gateway.SerialNumber }, gateway);
        }

        // DELETE: api/Gateway/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Gateway>> DeleteGateway(string id)
        {
            var gateway = await _context.Gateways.FindAsync(id);
            if (gateway == null)
            {
                return NotFound();
            }

            _context.Gateways.Remove(gateway);
            await _context.SaveChangesAsync();

            return gateway;
        }

        private bool GatewayExists(string id)
        {
            return _context.Gateways.Any(e => e.SerialNumber == id);
        }
    }
}
