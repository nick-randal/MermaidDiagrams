﻿using MermaidDiagrams.Contracts;
using MermaidDiagrams.Support;

namespace MermaidDiagrams.Sequence;

public interface IMessage : IStatement
{
	Identifier From { get; }
	Identifier To { get; }
	Text Text { get; }
	bool? Activate { get; }
}

public record Message(Identifier From, Identifier To, Text Text, ArrowType Arrow, bool? Activate = null) : IMessage
{
	private string ActivateText => Activate switch
	{
		true => "+",
		false => "-",
		_ => string.Empty
	};

	public void Render(ITextBuilder textBuilder, IRenderState renderState)
	{
		textBuilder.Line($"{From}{Arrow.GetShortName()}{ActivateText}{To}:{Text}");
	}
}