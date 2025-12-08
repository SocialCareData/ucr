using VDS.RDF;
using VDS.RDF.Wrapping;

namespace Model;

public class UcrGraph : WrapperGraph
{
    private UcrGraph(IGraph original) : base(original) { }

	public static UcrGraph Wrap(IGraph g) => new(g);

	public IEnumerable<Requirement> Requirements => GetTriplesWithPredicate(Vocabulary.Title)
        .Select(t => t.Subject)
        .Select(s => s.In(this))
        .Select(Requirement.Wrap);

	public IEnumerable<UseCase> UseCases => GetTriplesWithPredicate(Vocabulary.UseCaseTitle)
        .Select(t => t.Subject)
        .Select(s => s.In(this))
        .Select(UseCase.Wrap);

	public IEnumerable<Link> Links => GetTriplesWithPredicate(Vocabulary.UseCase)
        .Select(t => t.Object)
        .Select(s => s.In(this))
        .Select(Link.Wrap);
}
