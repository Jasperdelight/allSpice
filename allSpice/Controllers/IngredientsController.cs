using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace allSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IngredientsService _ingredientsService;
    private readonly Auth0Provider _auth0Provider;

    public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth0Provider)
    {
        _ingredientsService = ingredientsService;
        _auth0Provider = auth0Provider;
    }
    
    [HttpPost]
    public ActionResult<Ingredient> CreateIngredient([FromBody] Ingredient iData)
    {
      try
      {
        Ingredient ingredient = _ingredientsService.CreateIngredient(iData);
      return Ok(ingredient);
      }
       catch(Exception e) 
      {
          return BadRequest(e.Message);
      }
    }
}
