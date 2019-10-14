using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList.Controllers
{
  public class CategoriesController : Controller
  {

    private readonly ToDoListContext _db;
    
    public CategoriesController(ToDoListContext db)
    {
        _db = db;
    }
    

    public ActionResult Index()
    {
        List<Category> model = _db.Categories.ToList();
        return View(model);
    }

    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Create(Category category)
    {
        _db.Categories.Add(category);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category thisCategory = _db.Categories.FirstOrDefault(category => category.CategoryId == id);
        List<Item> categoryItems = _db.Items.Where(item => item.CategoryId == id).ToList();
        model.Add("category", thisCategory);
        model.Add("itemList", categoryItems);
        return View(model);
    }

    // [HttpPost]
    // public ActionResult Create(Item item)
    // {
    //     }
    
    // [HttpGet("/categories")]
    // public ActionResult Index()
    // {
    //   List<Category> allCategories = Category.GetAll();
    //   return View(allCategories);
    // }

    // [HttpGet("/categories/new")]
    // public ActionResult New()
    // {
    //   return View();
    // }

    // [HttpPost("/categories")]
    // public ActionResult Create(string categoryName)
    // {
    //   Category newCategory = new Category(categoryName);
    //   return RedirectToAction("Index");
    // }

    // [HttpGet("/categories/{id}")]
    // public ActionResult Show(int id)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Category selectedCategory = Category.Find(id);
    //   List<Item> categoryItems = selectedCategory.Items;
    //   model.Add("category", selectedCategory);
    //   model.Add("items", categoryItems);
    //   return View(model);
    // }

    // // This one creates new Items within a given Category, not new Categories:
    // [HttpPost("/categories/{categoryId}/items")]
    // public ActionResult Create(int categoryId, string itemDescription)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Category foundCategory = Category.Find(categoryId);
    //   Item newItem = new Item(itemDescription);
    //   newItem.Save();
    //   foundCategory.AddItem(newItem);
    //   List<Item> categoryItems = foundCategory.Items;
    //   model.Add("items", categoryItems);
    //   model.Add("category", foundCategory);
    //   return View("Show", model);
    // }
  }
}