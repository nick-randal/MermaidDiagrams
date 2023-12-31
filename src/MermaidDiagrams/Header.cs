using MermaidDiagrams.Contracts;

namespace MermaidDiagrams;

public interface IHeader : IRenderable
{
}

public record Header(string Title) : IHeader
{
	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder
			.Lines(
				"---",
				$"title: {Title}",
				"---"
			);
	}
}