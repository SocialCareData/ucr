using VDS.RDF;
using VDS.RDF.Wrapping;

namespace MakeWord.Wrapping;

internal class Link : GraphWrapperNode
{
    protected Link(INode node, IGraph graph) : base(node, graph) { }

    internal static Link Wrap(INode node, IGraph graph) => new(node, graph);

    internal static Link Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    internal Requirement Requirement { get => this.SingularReverse(Vocabulary.UseCase, Requirement.Wrap); }
    internal UseCase Target { get => this.Singular(Vocabulary.Target, UseCase.Wrap); }
    internal string Comment { get => this.Singular(Vocabulary.Comment, ValueMappings.As<string>); }
}
