# Mermaid Diagrams

.net support for Mermaid diagrams

## Flowcharts

### Create a flowchart

```csharp
var flow = new Flowchart();
flow.Render();
```

### Setup a flowchart

- Add `init` directive to set theme
- Set a title with `SetHeader`
- Add a comment with `AddComment`
- Add nodes with `Node`
- Add links with `Link`

```csharp
flow.AddDirective(new DirectiveInitialize(KnownThemes.Forest));
flow.SetHeader(new Header("This is a test"));
flow.Add(new Comment("No comment"));

flow.Node("A", "Hard edge", Shape.Box);
flow.Node("B", "Round edge", Shape.RoundedBox);

flow.Link(flow["A"], flow["B"], Edge.Arrow.WithLabel("Link text"));

var c = flow.Node("C", "Decision", Shape.Rhombus);

var d = flow.Node("D", "Result One", Shape.Trapezoid);

var e = flow.Node("E", "Result Two", Shape.Circle);

flow.Link(flow["B"], c, Edge.Arrow);
flow.Link(c, d, Edge.Arrow.WithLabel("Yes"));
flow.Link(c, e, Edge.Arrow.WithLabel("No"));
```

### Flowchart Example

```csharp
flow.Subgraph("Outer", Identifier.Next("sg"), FlowDirection.TopBottom, 
    sub =>
	{
		sub.Node("Something", Shape.Stadium);

		var sg = sub.Subgraph("Inner", "in1");
		sg.Node("A", "Hard edge", Shape.Box);
	});
```

### Theme and Styles

Define a style by name and assign it to a node by it's identifier.

```csharp
flow.AddDirective(new DirectiveInitialize(KnownThemes.Forest));

var cd = flow.GetClassDefinitions();

cd.GetOrCreate("neat", s
    => new ClassDef(s)
        .Style("fill", "#f96")
        .Style("stroke", "#333")
        .Style("stroke-width", "2px")
        .Assign("A")
    );
```