using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace Web.Pages;

public class UseCaseModel : PageModel
{
    public required UseCase UseCase { get; set; }

    public void OnGet(int id) =>
        UseCase = UcrGraph.Instance.UseCases.Single(x => x.Number == id.ToString());
}
