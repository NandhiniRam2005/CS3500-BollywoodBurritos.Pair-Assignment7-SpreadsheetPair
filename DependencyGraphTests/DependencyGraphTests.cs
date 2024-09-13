// <copyright file="DependencyGraphTests.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500 All rights reserved.
// </copyright>
// <summary>
// Author:    Joel Rodriguez
// Partner:   None
// Date:      September 13, 2024
// Course:    CS 3500, University of Utah, School of Computing
// Copyright: CS 3500 and [Joel Rodriguez] - This work may not
//            be copied for use in Academic Coursework.
//
// I, Joel Rodriguez, certify that I wrote this code from scratch and
// did not copy it in part or whole from another source.  All
// references used in the completion of the assignments are cited
// in my README file.
//
// File Contents
//
//    This project is made to determine the correctness of the DependencyGraph project using various tests.
//
// </summary>

// Ignore Spelling: Dependees Dependee
namespace DependencyGraphTests;

using CS3500.DependencyGraph;

/// <summary>
///   This is a test class for DependencyGraphTest and is intended
///   to contain all DependencyGraphTest Unit Tests.
/// </summary>
[TestClass]
public class DependencyGraphTests
{
    // DependencyGraph Constructor  Tests -----------------

    /// <summary>
    /// Ensures that no exception is thrown upon creation of a DependencyGraph.
    /// </summary>
    [TestMethod]
    public void DependencyGraphConstructor_DependencyGraphIsCreated_True()
    {
       _ = new DependencyGraph();
    }

    // HasDependents  Tests -----------------

    /// <summary>
    /// Checks to make sure a node with dependents does appear to have dependents.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphHasDependents_GraphWithOnePair_Valid()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        bool graphHasDependents = graph.HasDependents("a");
        Assert.IsTrue(graphHasDependents);
    }

    /// <summary>
    /// Checks to make sure a node which depends on multiple nodes does appear to have dependents.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphHasDependents_GraphWithMultipleDependents_True()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "q");
        bool graphHasDependents = graph.HasDependents("a");
        Assert.IsTrue(graphHasDependents);
    }

    /// <summary>
    /// Checks to make sure two nodes which depend on multiple nodes do appear to both have dependents.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphHasDependents_GraphWithMultipleDependeesAndDependents_True()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "z");
        graph.AddDependency("a", "f");
        graph.AddDependency("a", "v");
        graph.AddDependency("q", "t");
        graph.AddDependency("q", "e");
        bool bHasDependents = graph.HasDependents("a");
        bool qHasDependents = graph.HasDependents("q");
        Assert.IsTrue(qHasDependents);
        Assert.IsTrue(bHasDependents);
    }

    /// <summary>
    /// Checks to make sure a node with no dependents does appear to not have dependents.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphHasDependents_GraphWithOnePair_NoDependents()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        bool graphHasDependents = graph.HasDependents("b");
        Assert.IsFalse(graphHasDependents);
    }

    // HasDependees Tests -----------------

    /// <summary>
    /// Checks to make sure a node with dependees does appear to have dependees those dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphHasDependees_GraphWithOnePair_True()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        bool graphHasDependees = graph.HasDependees("b");
        Assert.IsTrue(graphHasDependees);
    }

    /// <summary>
    /// Checks to make sure a node which is depended on multiple nodes does appear to have dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphHasDependees_GraphWithMultipleDependees_True()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        graph.AddDependency("c", "b");
        graph.AddDependency("d", "b");
        graph.AddDependency("q", "b");
        bool graphHasdependees = graph.HasDependees("b");
        Assert.IsTrue(graphHasdependees);
    }

    /// <summary>
    /// Checks to make sure two nodes which are depended on multiple nodes do appear to both have dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphHasDependees_GraphWithMultipleDependentsAndDependees_True()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        graph.AddDependency("x", "b");
        graph.AddDependency("f", "b");
        graph.AddDependency("r", "b");
        graph.AddDependency("e", "t");
        graph.AddDependency("q", "t");
        bool bHasDependees = graph.HasDependees("b");
        bool tHasDependees = graph.HasDependees("t");
        Assert.IsTrue(bHasDependees);
        Assert.IsTrue(tHasDependees);
    }

    /// <summary>
    /// Checks to make sure a node with no dependees does appear to not have dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphHasDependees_GraphWithOnePair_NoDependeesFalse()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        bool graphHasDependees = graph.HasDependees("a");
        Assert.IsFalse(graphHasDependees);
    }

    // GetDependents Tests -----------------

    /// <summary>
    /// Checks to make sure a node with a dependent does appear to have that dependent.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependents_GraphWithOnePair_ReturnsOneNode()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("b");
        bool sameContents = actualDependents.ToHashSet().SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Checks to make sure a node which depends on multiple nodes does appear to have those specific dependents.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependents_GraphWithMultipleDependents_ReturnsMultipleNode()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "q");
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("b");
        expectedDependents.Add("c");
        expectedDependents.Add("d");
        expectedDependents.Add("q");
        bool sameContents = actualDependents.ToHashSet().SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Checks to make sure two nodes which depend on multiple nodes do appear to both have those specific dependents.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependents_GraphWithMultipleDependeesAndDependents_ReturnsMultipleNodes()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "z");
        graph.AddDependency("a", "f");
        graph.AddDependency("a", "v");
        graph.AddDependency("q", "t");
        graph.AddDependency("q", "e");
        IEnumerable<string> aActualDependents = graph.GetDependents("a");
        HashSet<string> aExpectedDependents = new ();
        aExpectedDependents.Add("b");
        aExpectedDependents.Add("z");
        aExpectedDependents.Add("f");
        aExpectedDependents.Add("v");
        IEnumerable<string> qActualDependents = graph.GetDependents("q");
        HashSet<string> qExpectedDependents = new ();
        qExpectedDependents.Add("t");
        qExpectedDependents.Add("e");
        bool sameContents = aActualDependents.ToHashSet().SetEquals(aExpectedDependents);
        bool sameContentsTwo = qActualDependents.ToHashSet().SetEquals(qExpectedDependents);
        Assert.IsTrue(sameContents);
        Assert.IsTrue(sameContentsTwo);
    }

    /// <summary>
    /// Checks to make sure a node with no dependents does appear to not have any dependents.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependents_GraphWithOnePair_NoDependents()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        IEnumerable<string> actualDependents = graph.GetDependents("b");
        HashSet<string> expectedDependents = new ();
        bool sameContents = actualDependents.ToHashSet().SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    // GetDependees  Tests -----------------

    /// <summary>
    /// Checks to make sure a node with a dependee does appear to have that dependee.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependees_GraphWithOnePair_ReturnsOneNode()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        IEnumerable<string> actualDependees = graph.GetDependees("b");
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("a");
        bool sameContents = actualDependees.ToHashSet().SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Checks to make sure a node which depends on multiple nodes does appear to have those specific dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependees_GraphWithMultipleDependees_ReturnsMultipleNodes()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "a");
        graph.AddDependency("d", "a");
        graph.AddDependency("q", "a");
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("b");
        expectedDependees.Add("c");
        expectedDependees.Add("d");
        expectedDependees.Add("q");
        bool sameContents = actualDependees.ToHashSet().SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Checks to make sure two nodes which depend on multiple nodes do appear to both have those specific dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependees_GraphWithMultipleDependeesAndDependents_ReturnsMultipleDependents()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("z", "a");
        graph.AddDependency("f", "a");
        graph.AddDependency("v", "a");
        graph.AddDependency("t", "q");
        graph.AddDependency("e", "q");
        IEnumerable<string> aActualDependees = graph.GetDependees("a");
        HashSet<string> aExpectedDependees = new ();
        aExpectedDependees.Add("b");
        aExpectedDependees.Add("z");
        aExpectedDependees.Add("f");
        aExpectedDependees.Add("v");
        IEnumerable<string> qActualDependees = graph.GetDependees("q");
        HashSet<string> qExpectedDependees = new ();
        qExpectedDependees.Add("t");
        qExpectedDependees.Add("e");
        bool sameContents = aActualDependees.ToHashSet().SetEquals(aExpectedDependees);
        bool sameContentsTwo = qActualDependees.ToHashSet().SetEquals(qExpectedDependees);
        Assert.IsTrue(sameContents);
        Assert.IsTrue(sameContentsTwo);
    }

    /// <summary>
    /// Checks to make sure a node with no dependees does appear to not have any dependees.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependees_GraphWithOnePair_NoDependees()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> expectedDependees = new ();
        bool sameContents = actualDependees.ToHashSet().SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    // Remove Dependency Tests -------------------

    /// <summary>
    /// Checks to make sure a node with a dependee can have all of its dependency's removed and return the proper
    /// dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphRemoveDependency_GraphWithOnePair_ReturnsOnePair()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.RemoveDependency("b", "a");
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();

        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);

        bool sameContentsOne = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContentsOne);
    }

    /// <summary>
    /// Checks to make sure a node with a dependee can have all of its dependency's removed and return the proper
    /// dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphRemoveDependency_RemoveWhereItContainsTheDependeeButNotDependency_ReturnsChangedDependee()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.RemoveDependency("b", "l");
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("b");
        IEnumerable<string> actualDependents = graph.GetDependents("b");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("a");
        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
        bool sameContentsOne = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContentsOne);
    }

    /// <summary>
    /// Checks to make sure a node which depends on multiple nodes can have some of its nodes removed and return
    /// the correct nodes for its dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphRemoveDependency_GraphWithMultipleDependees_ReturnsChangedDependees()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "a");
        graph.AddDependency("d", "a");
        graph.AddDependency("q", "a");
        graph.RemoveDependency("q", "a");
        graph.RemoveDependency("d", "a");
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("b");
        expectedDependees.Add("c");
        IEnumerable<string> actualDependentsOfQ = graph.GetDependents("q");
        HashSet<string> hashActualDependentsOfQ = actualDependentsOfQ.ToHashSet();
        HashSet<string> expectedDependents = new ();
        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
        bool sameContentsOne = hashActualDependentsOfQ.SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Checks to make sure two nodes which depend on multiple nodes can have their nodes removed and return the correct
    /// nodes for its dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphRemoveDependency_GraphWithMultipleDependentsAndDependees_ReturnsChangedDependees()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("z", "a");
        graph.AddDependency("f", "a");
        graph.AddDependency("v", "a");
        graph.AddDependency("t", "q");
        graph.AddDependency("e", "q");
        graph.RemoveDependency("f", "a");
        graph.RemoveDependency("v", "a");
        graph.RemoveDependency("e", "q");
        IEnumerable<string> aActualDependees = graph.GetDependees("a");
        HashSet<string> hashAActualDependees = aActualDependees.ToHashSet();
        HashSet<string> aExpectedDependees = new ();
        aExpectedDependees.Add("b");
        aExpectedDependees.Add("z");
        IEnumerable<string> qActualDependees = graph.GetDependees("q");
        HashSet<string> hashQActualDependees = qActualDependees.ToHashSet();
        HashSet<string> qExpectedDependees = new ();
        qExpectedDependees.Add("t");
        bool sameContentsOne = hashAActualDependees.SetEquals(aExpectedDependees);
        bool sameContentsTwo = hashQActualDependees.SetEquals(qExpectedDependees);
        Assert.IsTrue(sameContentsOne && sameContentsTwo);
    }

    /// <summary>
    /// Checks to make sure a node with no dependees does appear to not have any dependees and when attempting to
    /// remove a nonexistent dependee that no exception will be thrown.
    /// </summary>
    [TestMethod]
    public void DependencyGraphRemoveDependency_RemovingNodeThatDoesNotExistButIsClose_DoesNotChangeDependees()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.RemoveDependency("a", "b");
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("b");
        bool sameContents = actualDependees.ToHashSet().SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
        IEnumerable<string> actualDependents = graph.GetDependents("b");
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("a");
        bool sameContents1 = actualDependents.ToHashSet().SetEquals(expectedDependents);
        Assert.IsTrue(sameContents1);
    }

    // ReplaceDependents  Tests -----------------

    /// <summary>
    /// Tests replacing one nodes only dependent with a new  dependent.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceOneDependentWithOneDependent_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("a", "b");
        List<string> replacementDependents = new ();
        replacementDependents.Add("f");
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("f");
        bool sameContents = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing one nodes only dependent with many new  dependents.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceOneDependentWithMultipleDependent_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("a", "b");
        List<string> replacementDependents = new ();
        replacementDependents.Add("f");
        replacementDependents.Add("g");
        replacementDependents.Add("h");
        replacementDependents.Add("p");
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("f");
        expectedDependents.Add("g");
        expectedDependents.Add("h");
        expectedDependents.Add("p");
        bool sameContents = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing one nodes many dependents with one dependent.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceManyDependentWithOneDependent_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependents = new ();
        replacementDependents.Add("f");
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("f");
        bool sameContents = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing a nodes dependents successfully updates both data structures housing our dependents
    /// and dependees.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceUpdatesDependentsAndDependees_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependents = new ();
        replacementDependents.Add("f");
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("f");
        bool sameContents = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
        IEnumerable<string> actualDependees = graph.GetDependees("f");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("a");
        bool sameContentsOne = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing one nodes many dependents with many new  dependents.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceManyDependentWithManyDependents_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependents = new ();
        replacementDependents.Add("f");
        replacementDependents.Add("g");
        replacementDependents.Add("h");
        replacementDependents.Add("p");
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("f");
        expectedDependents.Add("g");
        expectedDependents.Add("h");
        expectedDependents.Add("p");
        bool sameContents = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing one nodes many dependents with no dependents.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceManyDependentWithNoDependents_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependents = new ();
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();
        bool sameContents = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing one nodes only dependent with no dependents.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceOneDependentWithNoDependents_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("a", "b");
        List<string> replacementDependents = new ();
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();
        bool sameContents = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests "replacing" a nodes dependents (none) with many dependents. Effectively making the method an add all nodes method.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceNoDependentsWithManyDependents_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        List<string> replacementDependents = new ();
        replacementDependents.Add("f");
        replacementDependents.Add("g");
        replacementDependents.Add("h");
        replacementDependents.Add("p");
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("f");
        expectedDependents.Add("g");
        expectedDependents.Add("h");
        expectedDependents.Add("p");
        bool sameContents = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContents);
    }

    // ReplaceDependees  Tests -----------------

    /// <summary>
    /// Tests replacing one nodes only dependee with a new  dependee.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceOneDependeeWithOneDependee_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("b", "a");
        List<string> replacementDependees = new ();
        replacementDependees.Add("f");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("f");
        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing one nodes only dependee with many new  dependees.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceOneDependentWithMultipleDependent_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("b", "a");
        List<string> replacementDependees = new ();
        replacementDependees.Add("f");
        replacementDependees.Add("g");
        replacementDependees.Add("h");
        replacementDependees.Add("p");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("f");
        expectedDependees.Add("g");
        expectedDependees.Add("h");
        expectedDependees.Add("p");
        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing one nodes many dependees with one dependee.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceManyDependentWithOneDependee_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "a");
        graph.AddDependency("d", "a");
        graph.AddDependency("e", "a");
        List<string> replacementDependees = new ();
        replacementDependees.Add("f");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("f");
        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing one nodes many dependees successfully updates data structures that house dependents
    /// and dependees.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceUpdatesDataStructures_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "a");
        graph.AddDependency("d", "a");
        graph.AddDependency("e", "a");
        List<string> replacementDependees = new ();
        replacementDependees.Add("f");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("f");
        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
        IEnumerable<string> actualDependents = graph.GetDependents("f");
        HashSet<string> hashActualDependents = actualDependents.ToHashSet();
        HashSet<string> expectedDependents = new ();
        expectedDependents.Add("a");
        bool sameContentsOne = hashActualDependents.SetEquals(expectedDependents);
        Assert.IsTrue(sameContentsOne);
    }

    /// <summary>
    /// Tests replacing one nodes many dependees with many new  dependees.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceManyDependeesWithManyDependees_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "a");
        graph.AddDependency("d", "a");
        graph.AddDependency("e", "a");
        List<string> replacementDependees = new ();
        replacementDependees.Add("f");
        replacementDependees.Add("g");
        replacementDependees.Add("h");
        replacementDependees.Add("p");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("f");
        expectedDependees.Add("g");
        expectedDependees.Add("h");
        expectedDependees.Add("p");
        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing one nodes many dependees with no dependees.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceManyDependentWithNoDependees_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("b", "a");
        graph.AddDependency("d", "a");
        graph.AddDependency("c", "a");
        graph.AddDependency("e", "a");
        List<string> replacementDependees = new ();
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests replacing one nodes only dependee with no dependees works and is valid.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceOneDependentWithNoDependees_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        graph.AddDependency("b", "a");
        List<string> replacementDependees = new ();
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    /// <summary>
    /// Tests "replacing" a nodes dependees (none) with many dependees. Effectively making the method an add all nodes method.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceNoDependeesWithManyDependees_ChangesDependencyGraph()
    {
        DependencyGraph graph = new ();
        List<string> replacementDependees = new ();
        replacementDependees.Add("f");
        replacementDependees.Add("g");
        replacementDependees.Add("h");
        replacementDependees.Add("p");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        HashSet<string> hashActualDependees = actualDependees.ToHashSet();
        HashSet<string> expectedDependees = new ();
        expectedDependees.Add("f");
        expectedDependees.Add("g");
        expectedDependees.Add("h");
        expectedDependees.Add("p");
        bool sameContents = hashActualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents);
    }

    // Size tests ---------------------

    /// <summary>
    ///  Tests that size updates after adding dependencies.
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterAdding_SizeIncreases()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "b");
        graph.AddDependency("d", "c");
        graph.AddDependency("b", "d");
        Assert.IsTrue(graph.Size == 4);
    }

    /// <summary>
    /// Tests that size updates after adding and then removing dependencies.
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterAddingAndRemoving_SizeIncreasesThenDecreases()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "b");
        graph.AddDependency("d", "c");
        graph.AddDependency("b", "d");
        graph.RemoveDependency("d", "c");
        graph.RemoveDependency("c", "b");
        Assert.IsTrue(graph.Size == 2);
    }

    /// <summary>
    /// Test that size updates after replacing dependents. (Adding more).
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterReplacingDependentsMore_SizeIncreases()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "b");
        List<string> replacementDependents = new ();
        replacementDependents.Add("f");
        replacementDependents.Add("g");
        replacementDependents.Add("h");
        replacementDependents.Add("p");
        graph.ReplaceDependents("b", replacementDependents);
        Assert.IsTrue(graph.Size == 5);
    }

    /// <summary>
    /// Tests that size updates after replacing dependees. (Adding more).
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterReplacingDependeesMore_SizeIncreases()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "b");
        List<string> replacementDependees = new ();
        replacementDependees.Add("f");
        replacementDependees.Add("g");
        replacementDependees.Add("h");
        replacementDependees.Add("p");
        graph.ReplaceDependees("a", replacementDependees);
        Assert.IsTrue(graph.Size == 5);
    }

    /// <summary>
    /// Test that size updates after replacing dependents. (Adding less).
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterReplacingDependentsLess_SizeDecreases()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("b", "d");
        graph.AddDependency("b", "q");
        List<string> replacementDependents = new ();
        replacementDependents.Add("f");
        graph.ReplaceDependents("b", replacementDependents);
        Assert.IsTrue(graph.Size == 1);
    }

    /// <summary>
    /// Tests that size updates after replacing dependees. (Adding less).
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterReplacingDependeesLess_SizeDecreases()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("q", "a");
        graph.AddDependency("d", "a");
        List<string> replacementDependees = new ();
        replacementDependees.Add("f");
        graph.ReplaceDependees("a", replacementDependees);
        Assert.IsTrue(graph.Size == 1);
    }

    /// <summary>
    /// Tests that size updates properly after removing the same node twice.
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterRemovingSameNodeTwice_SizeDecreasesThenRemainsUnchanged()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("q", "a");
        graph.AddDependency("d", "a");
        graph.RemoveDependency("d", "a");
        graph.RemoveDependency("d", "a");
        Assert.IsTrue(graph.Size == 2);
    }

    /// <summary>
    /// Tests that size updates properly after adding same node twice.
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterAddingSameNodeTwice_SizeIncreasesThenRemainsUnchanged()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("q", "a");
        graph.AddDependency("d", "a");
        graph.AddDependency("d", "a");
        Assert.IsTrue(graph.Size == 3);
    }

    /// <summary>
    /// Tests that size updates properly after replacing nothing with nothing. The size should be 0.
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterReplacingNothingWithNothingDependents_NoChange()
    {
        DependencyGraph graph = new DependencyGraph();
        List<string> replacementDependents = new ();
        graph.ReplaceDependents("a", replacementDependents);
        Assert.IsTrue(graph.Size == 0);
    }

    /// <summary>
    /// Tests that size updates after replacing dependees. (Adding less).
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterReplacingNothingWithNothingDependees_NoChange()
    {
        DependencyGraph graph = new DependencyGraph();
        List<string> replacementDependees = new ();
        graph.ReplaceDependees("a", replacementDependees);
        Assert.IsTrue(graph.Size == 0);
    }

    /// <summary>
    /// Tests that size updates properly after removing a dependency that does not exist.
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterRemovingNodeThatDoesNotExist_NoChange()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("q", "a");
        graph.AddDependency("d", "a");
        graph.RemoveDependency("sad", "w");
        Assert.IsTrue(graph.Size == 3);
    }

    /// <summary>
    /// Tests that size updates after replacing something with nothing. For a ReplaceDependents call.
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterReplacingSomethingWithNothingDependents_SizeDecreases()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "q");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "b");
        List<string> replacementDependents = new ();
        graph.ReplaceDependents("a", replacementDependents);
        Assert.IsTrue(graph.Size == 0);
    }

    /// <summary>
    /// Tests that size updates after replacing something with nothing. For a ReplaceDependees call.
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterReplacingSomethingWithNothingDependees_SizeDecreases()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("q", "a");
        graph.AddDependency("d", "a");
        List<string> replacementDependees = new ();
        graph.ReplaceDependees("a", replacementDependees);
        Assert.IsTrue(graph.Size == 0);
    }

    /// <summary>
    /// Tests that size updates after replacing nothing with something. For a ReplaceDependents call.
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterReplacingNothingWithSomethingDependents_SizeIncreases()
    {
        DependencyGraph graph = new DependencyGraph();
        List<string> replacementDependents = new ();
        replacementDependents.Add("b");
        graph.ReplaceDependees("a", replacementDependents);
        Assert.IsTrue(graph.Size == 1);
    }

    /// <summary>
    /// Tests that size updates after replacing nothing with something. For a ReplaceDependees call.
    /// </summary>
    [TestMethod]
    public void DependencyGraphSize_TestSizeAfterReplacingNothingWithSomethingDependees_SizeIncreases()
    {
        DependencyGraph graph = new DependencyGraph();
        List<string> replacementDependees = new ();
        replacementDependees.Add("b");
        graph.ReplaceDependees("a", replacementDependees);
        Assert.IsTrue(graph.Size == 1);
    }

    // AddDependency Tests (Most tests are included in other tests)

    /// <summary>
    /// Tests a simple case where adding a node does add the node to our DependencyGraph.
    /// </summary>
    [TestMethod]
    public void DependencyGraphAddDependency_TestAdding_Adds()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        HashSet<string> actualDependents = graph.GetDependents("a").ToHashSet();
        HashSet<string> actualDependees = graph.GetDependees("b").ToHashSet();
        HashSet<string> expectedDependents = new HashSet<string>();
        HashSet<string> expectedDependees = new HashSet<string>();
        expectedDependents.Add("b");
        expectedDependees.Add("a");
        bool sameContents1 = actualDependents.SetEquals(expectedDependents);
        bool sameContents2 = actualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents1);
        Assert.IsTrue(sameContents2);
    }

    /// <summary>
    /// Tests the case where we attempt to add the same node twice. We expected only one pair
    /// to be added.
    /// </summary>
    [TestMethod]
    public void DependencyGraphAddDependency_TestAddingDuplicates_DoesNotAdd()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "b");
        HashSet<string> actualDependents = graph.GetDependents("a").ToHashSet();
        HashSet<string> actualDependees = graph.GetDependees("b").ToHashSet();
        HashSet<string> expectedDependents = new HashSet<string>();
        HashSet<string> expectedDependees = new HashSet<string>();
        expectedDependents.Add("b");
        expectedDependees.Add("a");
        bool sameContents1 = actualDependents.SetEquals(expectedDependents);
        bool sameContents2 = actualDependees.SetEquals(expectedDependees);
        Assert.IsTrue(sameContents1);
        Assert.IsTrue(sameContents2);
    }

    // Stress Tests -------------------------

    /// <summary>
    ///  The following test is a stress test which tests how fast our DependencyGraph is able to add and remove
    ///  then add some back and remove some dependency pairs. The test must complete in less than two seconds
    ///  and must also be correct.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]
    public void StressTest()
    {
        DependencyGraph dg = new ();

        // A bunch of strings to use for adding dependents and dependees
        const int SIZE = 200;
        string[] letters = new string[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            letters[i] = string.Empty + ((char)('a' + i));
        }

        // The correct answers. This block of code gets what the correct dependencies should be.
        // This will be used later to determine if our method is correct.
        HashSet<string>[] dependents = new HashSet<string>[SIZE];
        HashSet<string>[] dependees = new HashSet<string>[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            dependents[i] = [];
            dependees[i] = [];
        }

        // Add a bunch of dependencies.
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 1; j < SIZE; j++)
            {
                dg.AddDependency(letters[i], letters[j]); // Adds the dependencies and uses a nested for loop to ensure we
                                                          // get a lot of dependencies.
                dependents[i].Add(letters[j]);  // correct answers
                dependees[j].Add(letters[i]); // correct answers
            }
        }

        // Remove a bunch of dependencies
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 4; j < SIZE; j += 4)
            {
                dg.RemoveDependency(letters[i], letters[j]); // Removes dependencies appropriately (only removes dependencies that exist)
                dependents[i].Remove(letters[j]); // corrects correct answers
                dependees[j].Remove(letters[i]); // corrects correct answers
            }
        }

        // Add some back
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 1; j < SIZE; j += 2)
            {
                dg.AddDependency(letters[i], letters[j]); // Adds the dependencies and uses a nested for loop to ensure we
                                                         // get a lot of dependencies.
                dependents[i].Add(letters[j]);
                dependees[j].Add(letters[i]);
            }
        }

        // Remove some more
        for (int i = 0; i < SIZE; i += 2)
        {
            for (int j = i + 3; j < SIZE; j += 3)
            {
                dg.RemoveDependency(letters[i], letters[j]); // Same as above removes some more.
                dependents[i].Remove(letters[j]);
                dependees[j].Remove(letters[i]);
            }
        }

        // Make sure everything is right. Now we are using the dependents and dependees HashSets we created to
        // to ensure that everything is correct.
        for (int i = 0; i < SIZE; i++)
        {
            Assert.IsTrue(dependents[i].SetEquals(new HashSet<string>(dg.GetDependents(letters[i]))));
            Assert.IsTrue(dependees[i].SetEquals(new HashSet<string>(dg.GetDependees(letters[i]))));
        }
    }

    /// <summary>
    ///  The following test is a stress test which tests how fast our DependencyGraph is able to add and remove
    ///  then add some back and remove some dependency pairs. The test must complete in less than three seconds
    ///  and must also be correct. This is a very intense stress test.
    /// </summary>
    [TestMethod]
    [Timeout(3000)]
    public void IntenseStressTest()
    {
        DependencyGraph dg = new ();

        // A bunch of strings to use for adding dependents and dependees
        const int SIZE = 1000;
        string[] letters = new string[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            letters[i] = string.Empty + ((char)('a' + i));
        }

        // The correct answers that will be used to compare our actual results too.
        // This code block basically adds the numbers 0 - size to a HashSet.
        HashSet<string>[] dependents = new HashSet<string>[SIZE];
        HashSet<string>[] dependees = new HashSet<string>[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            dependents[i] = [];
            dependees[i] = [];
        }

        // Add a bunch of dependencies to our graph more specifically about 1 million dependencies.
        // This is because it is doing 1000 loops 999 times
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 1; j < SIZE; j++)
            {
                dg.AddDependency(letters[i], letters[j]);
                dependents[i].Add(letters[j]);
                dependees[j].Add(letters[i]);
            }
        }

        // Remove a bunch of dependencies. More specifically a little under 1 million dependencies.
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 4; j < SIZE; j += 4)
            {
                dg.RemoveDependency(letters[i], letters[j]);
                dependents[i].Remove(letters[j]);
                dependees[j].Remove(letters[i]);
            }
        }

        // Add some back. About half of the original amount (500,000).
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 1; j < SIZE; j += 2)
            {
                dg.AddDependency(letters[i], letters[j]);
                dependents[i].Add(letters[j]);
                dependees[j].Add(letters[i]);
            }
        }

        // Remove some more.About one third of the original amount.
        for (int i = 0; i < SIZE; i += 2)
        {
            for (int j = i + 3; j < SIZE; j += 3)
            {
                dg.RemoveDependency(letters[i], letters[j]);
                dependents[i].Remove(letters[j]);
                dependees[j].Remove(letters[i]);
            }
        }

        // Make sure everything is right by cross checking with what was made earlier.
        for (int i = 0; i < SIZE; i++)
        {
            Assert.IsTrue(dependents[i].SetEquals(new HashSet<string>(dg.GetDependents(letters[i]))));
            Assert.IsTrue(dependees[i].SetEquals(new HashSet<string>(dg.GetDependees(letters[i]))));
        }
    }
}
