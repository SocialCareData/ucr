using VDS.RDF;
using VDS.RDF.Wrapping;

namespace Model;

public class Link : GraphWrapperNode
{
    protected Link(INode node, IGraph graph) : base(node, graph) { }

    public static Link Wrap(INode node, IGraph graph) => new(node, graph);

    public static Link Wrap(GraphWrapperNode node) => Wrap(node, node.Graph);

    public Requirement Requirement { get => this.SingularReverse(Vocabulary.UseCase, Requirement.Wrap); }
    public UseCase Target { get => this.Singular(Vocabulary.Target, UseCase.Wrap); }
    public string Comment { get => this.Singular(Vocabulary.Comment, ValueMappings.As<string>); }
}
