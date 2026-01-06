using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace Web.Pages;

public class UseCasesModel : PageModel
{
    public IEnumerable<UseCase> UseCases { get; } =
        UcrGraph.Instance.UseCases;
}
