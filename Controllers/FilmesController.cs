using Microsoft.AspNetCore.Mvc;
using MovieBox.Models;
using MovieBox.Repositories;

namespace MovieBox.Controllers;

public class FilmesController : Controller
{
    private readonly FilmeRepository _repository;

    public FilmesController(FilmeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            return View(await _repository.ListarAsync());
        }
        catch (Exception ex)
        {
            ViewBag.ErroBanco = $"Não foi possível conectar ao banco de dados: {ex.Message}";
            return View(new List<Filme>());
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar(Filme filme)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ErroBanco = "Confira os dados informados.";
            return View("Index", await _repository.ListarAsync());
        }

        await _repository.AdicionarAsync(filme);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Excluir(int id)
    {
        await _repository.ExcluirAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
