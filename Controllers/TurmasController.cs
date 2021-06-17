using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaEscola.Data;
using SistemaEscola.Models;

namespace SistemaEscola.Controllers
{
    [Authorize]
    public class TurmasController : Controller
    {
        private readonly EscolaContext _context;
        public TurmasController(EscolaContext context) 
        {
            _context = context;
        }

        //GET:Turma
        public async Task<IActionResult> Index() 
        {
            return View(
                await _context.Turmas.Include(t => t.Professor).OrderBy(t => t.Id).ToListAsync()
            );
        }

        // GET: Turma/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turmas
                .Include(t => t.Professor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        // GET: Turma/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turma = await _context.Turmas.Include(t => t.Professor).Where(t => t.Id == id).FirstOrDefaultAsync();
            if (turma == null)
            {
                return NotFound();
            }

            ViewBag.professores = _context.Professores.ToList();
            return View(turma);
        }

        // POST: Professor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Sala")] Turma turma, [Bind("IdProfessor")] int IdProfessor )
        {
            if (id != turma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    turma.Professor = _context.Professores.Where(p => p.Id == IdProfessor).FirstOrDefault();
                    
                    _context.Update(turma);

                    await _context.SaveChangesAsync();

                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessorExists(turma.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(turma);
        }

        // GET: Turma/Create
        public IActionResult Create()
        {
            ViewBag.professores = _context.Professores.ToList();
            return View();
        }

        // POST: Turma/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProfessor")] 
                                                    int IdProfessor,
                                                [Bind("Sala")]
                                                    string Sala
                                                ) 
                                               
        {
            if (ModelState.IsValid)
            {
                Turma novaTurma = new Turma() {
                    Professor = _context.Professores.Where(p => p.Id == IdProfessor).FirstOrDefault(),
                    Sala = Sala
                };

                _context.Add(novaTurma);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Turma/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Turmas
                .Include(t => t.Professor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        // POST: Turma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turma = await _context.Turmas.FindAsync(id);
            _context.Turmas.Remove(turma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessorExists(int id)
        {
            return _context.Turmas.Any(e => e.Id == id);
        }

        private bool TurmaExists(int id)
        {
            return _context.Turmas.Any(e => e.Id == id);
        }
    }
}