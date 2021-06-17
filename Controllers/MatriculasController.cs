using System;
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
    public class MatriculasController : Controller
    {
        private readonly EscolaContext _context;
        public MatriculasController(EscolaContext context) 
        {
            _context = context;
        }

        //GET:Matricula
        public async Task<IActionResult> Index() 
        {
            return View(
                await _context.Matriculas.Include(m => m.Aluno).Include(m => m.Turma).OrderBy(m => m.Id).ToListAsync()
            );
        }

        // GET: Matricula/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.Aluno).Include(m => m.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // GET: Matricula/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas.Include(m => m.Aluno).Include(m => m.Turma).Where(t => t.Id == id).FirstOrDefaultAsync();
            if (matricula == null)
            {
                return NotFound();
            }

            ViewBag.alunos = _context.Alunos.ToList();
            ViewBag.turmas = _context.Turmas.ToList();
            
            return View(matricula);
        }

        // POST: Matricula/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, DataMatricula")] Matricula matricula, [Bind("IdTurma")] int IdTurma, [Bind("IdAluno")] int IdAluno )
        {
            if (id != matricula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    matricula.Aluno = _context.Alunos.Where(a => a.Id == IdAluno).FirstOrDefault();
                    matricula.Turma = _context.Turmas.Where(t => t.Id == IdTurma).FirstOrDefault();
                    
                    _context.Update(matricula);

                    await _context.SaveChangesAsync();

                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatriculaExists(matricula.Id))
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
            return View(matricula);
        }

        // GET: Matricula/Create
        public IActionResult Create()
        {
            ViewBag.alunos = _context.Alunos.ToList();
            ViewBag.turmas = _context.Turmas.ToList();

            return View();
        }

        // POST: Matricula/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAluno")] 
                                                    int IdAluno,
                                                [Bind("IdTurma")]
                                                    int IdTurma,
                                                [Bind("DataMatricula")]
                                                    DateTime DataMatricula
                                                )
                                                
                                               
        {
            if (ModelState.IsValid)
            {
                Matricula matriculaTurma = new Matricula() {
                    DataMatricula = DataMatricula,
                    Aluno = _context.Alunos.Where(a => a.Id == IdAluno).FirstOrDefault(),
                    Turma = _context.Turmas.Where(t => t.Id == IdTurma).FirstOrDefault()
                };

                _context.Add(matriculaTurma);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Matricula/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var matricula = await _context.Matriculas
                .Include(m => m.Aluno)
                .Include(m => m.Turma)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (matricula == null)
            {
                return NotFound();
            }

            return View(matricula);
        }

        // POST: Matricula/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var matricula = await _context.Matriculas.FindAsync(id);

            _context.Matriculas.Remove(matricula);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool MatriculaExists(int id)
        {
            return _context.Matriculas.Any(e => e.Id == id);
        }
    }
}