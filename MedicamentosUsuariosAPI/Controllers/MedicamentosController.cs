using Microsoft.AspNetCore.Mvc;
using MedicamentosUsuariosAPI.Models;
using MedicamentosUsuariosAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MedicamentosUsuariosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicamentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MedicamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Medicamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medicamento>>> GetMedicamentos()
        {
            return await _context.Medicamentos.Include(m => m.FormaFarmaceutica).ToListAsync();
        }

        // GET: api/Medicamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medicamento>> GetMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.Include(m => m.FormaFarmaceutica)
                .FirstOrDefaultAsync(m => m.IdMedicamento == id);

            if (medicamento == null)
            {
                return NotFound();
            }

            return medicamento;
        }

        // PUT: api/Medicamentos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicamento(int id, Medicamento medicamento)
        {
            if (id != medicamento.IdMedicamento)
            {
                return BadRequest();
            }
            FormaFarmaceutica? formaFarmaceutica = await _context.FormasFarmaceuticas.FindAsync(medicamento.IdFormaFarmaceutica);
            if (formaFarmaceutica == null)
            {
                return BadRequest("La forma farmacéutica no existe.");
            }
            medicamento.FormaFarmaceutica = formaFarmaceutica;
            _context.Entry(medicamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicamentoExists(id))
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

        // POST: api/Medicamentos
        [HttpPost]
        public async Task<IActionResult> PostMedicamento(Medicamento medicamento)
        {
            FormaFarmaceutica? formaFarmaceutica = await _context.FormasFarmaceuticas.FindAsync(medicamento.IdFormaFarmaceutica);
            if (formaFarmaceutica == null)
            {
                // Si no existe la forma farmacéutica, devolver un error
                return BadRequest("La forma farmacéutica no existe.");
            }
            medicamento.FormaFarmaceutica = formaFarmaceutica;
            // Guardar el nuevo medicamento
            _context.Medicamentos.Add(medicamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicamento", new { id = medicamento.IdMedicamento }, medicamento);
        }

        // DELETE: api/Medicamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicamento(int id)
        {
            var medicamento = await _context.Medicamentos.FindAsync(id);
            if (medicamento == null)
            {
                return NotFound();
            }

            _context.Medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MedicamentoExists(int id)
        {
            return _context.Medicamentos.Any(e => e.IdMedicamento == id);
        }
    }
}
