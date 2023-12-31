﻿using System.Drawing;
using GwtUnit.XUnit;
using MermaidDiagrams.Git;

namespace MermaidDiagrams.Tests.Git;

[UsesVerify]
public sealed class GitGraphTests : XUnitTestBase<GitGraphTests.Thens>
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
	public Task ShouldHaveValidFlowchart_WhenUsingExampleB()
	{
		When(UsingExampleB, Rendering);

		return Verify(Then.Diagram);
	}
	
	protected override void Creating()
	{
		Then.Target = new GitGraph();
	}

	private void Rendering()
	{
		Then.Diagram = Then.Target.Render();
	}

	private void UsingExampleA()
	{
		var git = Then.Target;

		var hotfix = new Branch("hotfix");
		git
			.Commit()
			.Commit("bob")
			.Commit("sue", CommitType.Reverse)
			.Commit(tag: "tag")
			.Branch(hotfix)
			.Checkout(hotfix)
			.Commit()
			.Commit(CommitType.Reverse)
			.Merge(git.MainBranch)
			.Branch("POC")
			.CherryPick("sue");
	}

	private void UsingExampleB()
	{
		var git = Then.Target;

		git.SetOptions(new GitGraphInit
		{
			GitGraph = new()
			{
				ShowBranches = false,
				ShowCommitLabel = false,
				MainBranchName = "master",
				MainBranchOrder = 4,
				RotateCommitLabel = false
			},
			Theme = "base",
			ThemeVariables = new ()
			{
				Git0 = Color.Red
			}
		});
		
		git
			.Branch("A", 3)
			.Branch("B", 2)
			.Branch("C", 1)
			.Commit()
			.Commit()
			.Checkout("A")
			.Commit();
	}

	public sealed class Thens
	{
		public GitGraph Target { get; set; }
		public string Diagram { get; set; }
	}
}