using Microsoft.AspNetCore.Mvc.RazorPages;
using Model;

namespace Web.Pages;

public class CoreStandardsModel : PageModel
{
    public IEnumerable<CoreStandard> CoreStandards { get; } =
        UcrGraph.Instance.CoreStandards;
}
