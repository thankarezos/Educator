﻿using ADOPSE.Models;
using ADOPSE.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ADOPSE.Controllers;

[ApiController]
[Route("[controller]")]
public class ModuleController : ControllerBase
{
    private readonly ILogger<ModuleController> _logger;
    private readonly IModuleService _moduleService;

    public ModuleController(ILogger<ModuleController> logger, IModuleService moduleService)
    {
        _logger = logger;
        _moduleService = moduleService;
    }

    [HttpGet]
    public IEnumerable<Module> Get()
    {
        return _moduleService.GetModules();
    }

    [HttpGet("{id}")]
    public ActionResult<Module> GetModule(int id)
    {
        Module module = _moduleService.GetModuleById(id);
        if (module == null)
            return NotFound();
        _logger.LogInformation("AAAAAAAAAAAAAAAAAAAAAA -> " + module.Name);
        return _moduleService.GetModuleById(id);
    }

    [HttpGet("stack/{limit}/{offset}")]
    public IActionResult GetStack(int limit, int offset)
    {
        return _moduleService.GetModuleStacks(limit, offset);
    }

    [HttpGet("filtered")]
    public IEnumerable<Module> GetFilteresModules([FromQuery] string? ModuleTypeId, [FromQuery] string? DifficultyId,
        [FromQuery] string? Rating)
    {
        _logger.LogInformation(ModuleTypeId);
        _logger.LogInformation(DifficultyId);
        _logger.LogInformation(Rating);
        
        Dictionary<string, string> myDict1 = new Dictionary<string, string>();
        
        if(ModuleTypeId != null)
            myDict1.Add("ModuleTypeId", ModuleTypeId);
        if(DifficultyId != null)
            myDict1.Add("DifficultyId", DifficultyId);
        if(Rating != null)
            myDict1.Add("Rating", Rating);

        IEnumerable<Module> modules = _moduleService.GetFilteredModules(myDict1);
        return modules;
    }
}