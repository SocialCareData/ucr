using MakeWord.Wrapping;
using VDS.RDF;
using VDS.RDF.Wrapping;

namespace Query.Wrapping;

internal class UcrGraph : WrapperGraph
{
    private UcrGraph(IGraph original) : base(original) { }

    internal static UcrGraph Wrap(IGraph g) => new(g);

    internal IEnumerable<Requirement> Requirements => GetTriplesWithPredicate(Vocabulary.Title)
        .Select(t => t.Subject)
        .Select(s => s.In(this))
        .Select(Requirement.Wrap);

    internal IEnumerable<UseCase> UseCases => GetTriplesWithPredicate(Vocabulary.UseCaseTitle)
        .Select(t => t.Subject)
        .Select(s => s.In(this))
        .Select(UseCase.Wrap);
}
