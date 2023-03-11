using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PizzaTypesController : ControllerBase
{
    private readonly List<PizzaType> _pizzaTypes = new List<PizzaType>
    {
        new PizzaType { Id = 1, Name = "Margherita", Description = "Tomato sauce, mozzarella, and basil", Price = 10.99m },
        new PizzaType { Id = 2, Name = "Pepperoni", Description = "Tomato sauce, mozzarella, and pepperoni", Price = 12.99m },
        new PizzaType { Id = 3, Name = "Hawaiian", Description = "Tomato sauce, mozzarella, ham, and pineapple", Price = 11.99m },
    };

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_pizzaTypes);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var pizzaType = _pizzaTypes.FirstOrDefault(p => p.Id == id);
        if (pizzaType == null)
        {
            return NotFound();
        }
        return Ok(pizzaType);
    }

    [HttpPost]
    public IActionResult Create(PizzaType pizzaType)
    {
        pizzaType.Id = _pizzaTypes.Count + 1;
        _pizzaTypes.Add(pizzaType);
        return CreatedAtAction(nameof(GetById), new { id = pizzaType.Id }, pizzaType);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, PizzaType pizzaType)
    {
        var index = _pizzaTypes.FindIndex(p => p.Id == id);
        if (index == -1)
        {
            return NotFound();
        }
        _pizzaTypes[index] = pizzaType;
        pizzaType.Id = id;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var index = _pizzaTypes.FindIndex(p => p.Id == id);
        if (index == -1)
        {
            return NotFound();
        }
        _pizzaTypes.RemoveAt(index);
        return NoContent();
    }
}
