using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace Web.Pages;

public class RequirementsModel : PageModel
{
    public IEnumerable<Requirement> Requirements { get; } =
        UcrGraph.Instance.Requirements;
}
