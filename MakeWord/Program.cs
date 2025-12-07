using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Query.Wrapping;
using VDS.RDF;
using VDS.RDF.Parsing;

var source = args[0];
var target = args[1];
var template = args[2];

File.Copy(template, target, true);

var g = UcrGraph.Wrap(new Graph());
FileLoader.Load(g, source);

using WordprocessingDocument wordDocument = WordprocessingDocument.Open(target, true);
var body = (wordDocument.MainDocumentPart.Document = new Document(new Body())).Body;

body.Append(
    new Paragraph(
        new Run(
            new Text("Requirements")))
    { ParagraphProperties = new ParagraphProperties { ParagraphStyleId = new ParagraphStyleId { Val = "Heading1" } } }
    );

foreach (var requirement in g.Requirements)
{
    body.Append(
        new Paragraph(
            new BookmarkStart { Id = "requirement_" + requirement.Number, Name = "requirement_" + requirement.Number },
            new Run(
                new Text(
                    $"{requirement.Number} - {requirement.Title}")),
            new BookmarkEnd { Id = "requirement_" + requirement.Number })
        { ParagraphProperties = new ParagraphProperties { ParagraphStyleId = new ParagraphStyleId { Val = "Heading2" } } },
        new Paragraph(
            new Run(
                new Text(
                    requirement.Description))),
        new Paragraph(
            new Run(
                new Text(
                    "Related use cases")))
        { ParagraphProperties = new ParagraphProperties { ParagraphStyleId = new ParagraphStyleId { Val = "Heading3" } } }
        );

    foreach (var link in requirement.Links)
    {
        body.Append(
            new Paragraph(
                new Hyperlink(
                    new Run(
                        new Text(
                            $"{link.Target.Number} - {link.Target.Title}")))
                { Anchor = "usecase_" + link.Target.Number })
            { ParagraphProperties = new ParagraphProperties { ParagraphStyleId = new ParagraphStyleId { Val = "Heading4" } } },
            new Paragraph(
                new Run(
                    new Text(
                        link.Comment)))
            );
    }
}

body.Append(
    new Paragraph(
        new Run(
            new Text("Use cases")))
    { ParagraphProperties = new ParagraphProperties { ParagraphStyleId = new ParagraphStyleId { Val = "Heading1" } } }
    );

foreach (var useCase in g.UseCases)
{
    body.Append(
        new Paragraph(
            new BookmarkStart { Id = "usecase_" + useCase.Number, Name = "usecase_" + useCase.Number },
            new Run(
                new Text(
                    $"{useCase.Number} - {useCase.Title}")),
            new BookmarkEnd { Id = "usecase_" + useCase.Number })
        { ParagraphProperties = new ParagraphProperties { ParagraphStyleId = new ParagraphStyleId { Val = "Heading2" } } },
        new Paragraph(
            new Run(
                new Text(
                    useCase.Description))),
        new Paragraph(
            new Run(
                new Text(
                    "Related requirements")))
        { ParagraphProperties = new ParagraphProperties { ParagraphStyleId = new ParagraphStyleId { Val = "Heading3" } } }
        );

    foreach (var link in useCase.Links)
    {
        body.Append(
            new Paragraph(
                new Hyperlink(
                    new Run(
                        new Text(
                            $"{link.Requirement.Number} - {link.Requirement.Title}")))
                { Anchor = "requirement_" + link.Requirement.Number })
            { ParagraphProperties = new ParagraphProperties { ParagraphStyleId = new ParagraphStyleId { Val = "Heading4" } } },
            new Paragraph(
                new Run(
                    new Text(
                        link.Comment)))
            );
    }
}

wordDocument.Save();