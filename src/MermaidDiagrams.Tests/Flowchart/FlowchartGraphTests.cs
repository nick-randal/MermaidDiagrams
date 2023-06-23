﻿using GwtUnit.XUnit;
using MermaidDiagrams.Flowchart;

namespace MermaidDiagrams.Tests.Flowchart;

[UsesVerify]
public sealed class FlowchartGraphTests : XUnitTestBase<FlowchartGraphTests.Thens>
{
	[Fact]
	public Task ShouldHaveValidFlowchart_WhenRendering()
	{
		When(Rendering);

		return Verify(Then.Diagram);
	}

	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleA()
	{
		When(UsingExampleA, Rendering);

		return Verify(Then.Diagram);
	}
	
	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleA_AddingClassDefs()
	{
		When(UsingExampleA, AddingClassDefinitions, Rendering);

		return Verify(Then.Diagram);
	}
	
	[Fact]
	public Task ShouldHaveValidFlowchart_WhenUsingExampleB()
	{
		When(UsingExampleB, Rendering);

		return Verify(Then.Diagram);
	}

	protected override void Creating()
	{
		Then.Target = new FlowchartGraph(FlowDirection.LeftRight);
	}

	private void Rendering()
	{
		Then.Diagram = Then.Target.Render();
	}

	private void UsingExampleA()
	{
		var flow = Then.Target;

		flow.SetTheme(KnownThemes.Forest);
		flow.SetHeader(new Header("This is a test"));
		flow.AddAnd(new Comment("No comment"));

		flow.Node("A", "Hard edge", Shape.Box);
		flow.Node("B", "Round edge", Shape.RoundedBox);

		flow.Link(flow["A"], flow["B"], Edge.Arrow.WithLabel("Link text"));

		var c = flow.Node("C", "Decision", Shape.Rhombus);

		var d = flow.Node("D", "Result One", Shape.Trapezoid);

		var e = flow.Node("E", "Result Two", Shape.Circle);

		flow.Link(flow["B"], c, Edge.Arrow);
		flow.Link(c, d, Edge.Arrow.WithLabel("Yes"));
		flow.Link(c, e, Edge.Arrow.WithLabel("No"));
	}

	private void UsingExampleB()
	{
		var flow = Then.Target;

		flow.SetTheme(new ThemeVariables
		{
			PrimaryColor = "#f96",
			SecondaryColor = "#363",
			LineColor = "#363",
			TertiaryColor = "#f96",
			PrimaryBorderColor = "#333",
			PrimaryTextColor = "#633"
		});

		flow.Subgraph("Outer", Identifier.Next("sg"), FlowDirection.TopBottom, sub =>
		{
			sub.Node("Something", Shape.Stadium);

			var sg = sub.CreateSubgraph("Inner", "in1");
			sg.Node("A", "A Box", Shape.Box);

			sg.Invisible("can'tSeeme");
		});
	}

	private void AddingClassDefinitions()
	{
		var cd = Then.Target.GetClassDefinitions();

		cd.GetOrCreate("neat", s
			=> new ClassDef(s)
				.Style("fill", "#f96")
				.Style("stroke", "#333")
				.Style("stroke-width", "2px").Assign("A")
		);
	}

	public sealed class Thens
	{
		public FlowchartGraph Target { get; set; }
		public string Diagram { get; set; }
	}
}