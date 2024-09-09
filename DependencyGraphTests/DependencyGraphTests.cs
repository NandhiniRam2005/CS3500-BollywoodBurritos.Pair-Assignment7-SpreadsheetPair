// <summary>
// Author:    Joel Rodriguez
// Partner:   None
// Date:      September 8, 2024
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

// Ignore Spelling: Dependees

namespace DependencyGraphTests;

using System.Collections;
using CS3500.DependencyGraph;

/// <summary>
///   This is a test class for DependencyGraphTest and is intended
///   to contain all DependencyGraphTest Unit Tests
/// </summary>
[TestClass]
public class DependencyGraphTests
{
    // GraphDependency Constructor  Tests -----------------

    /// <summary>
    /// Ensures that no exception is thrown upon creation of a DependencyGraph
    /// </summary>
    [TestMethod]
    public void DependencyGraphConstructor_DependencyGraphIsCreated_True()
    {
       _ = new DependencyGraph();
       
    }

    // Size Tests -----------------

    // HasDependents  Tests -----------------

    /// <summary>
    /// Checks to make sure a node with dependents does appear to have dependents.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphHasDependents_GraphWithOnePair()
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
    public void DependencyGraphHasDependents_GraphWithMultipleDependents()
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
    public void DependencyGraphHasDependents_GraphWithMultipleDependeesAndDependents()
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
    public void DependencyGraphHasDependees_GraphWithOnePair()
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
    public void DependencyGraphHasDependees_GraphWithMultipleDependees()
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
    public void DependencyGraphHasDependees_GraphWithMultipleDependentsAndDependees()
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
    public void DependencyGraphHasDependees_GraphWithOnePair_NoDependees()
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
    public void DependencyGraphGetDependents_GraphWithOnePair()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        List<string> expectedDependents = new ();
        expectedDependents.Add ("b");
        Assert.AreEqual(actualDependents, expectedDependents);


    }
    /// <summary>
    /// Checks to make sure a node which depends on multiple nodes does appear to have those specific dependents.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependents_GraphWithMultipleDependents()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "q");
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        List<string> expectedDependents = new ();
        expectedDependents.Add("b");
        expectedDependents.Add("c");
        expectedDependents.Add("d");
        expectedDependents.Add("q");
        Assert.AreEqual(actualDependents, expectedDependents);


    }
    /// <summary>
    /// Checks to make sure two nodes which depend on multiple nodes do appear to both have those specific dependents.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependents_GraphWithMultipleDependeesAndDependents()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "z");
        graph.AddDependency("a", "f");
        graph.AddDependency("a", "v");
        graph.AddDependency("q", "t");
        graph.AddDependency("q", "e");
        IEnumerable<string> aActualDependents = graph.GetDependents("a");
        List<string> aExpectedDependents = new ();
        aExpectedDependents.Add("b");
        aExpectedDependents.Add("z");
        aExpectedDependents.Add("f");
        aExpectedDependents.Add("v");
        IEnumerable<string> qActualDependents = graph.GetDependents("q");
        List<string> qExpectedDependents = new ();
        qExpectedDependents.Add("t");
        qExpectedDependents.Add("e");
        Assert.AreEqual(aActualDependents, aExpectedDependents);
        Assert.AreEqual(qActualDependents, qExpectedDependents);

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
        List<string> expectedDependents = new();
        Assert.AreEqual(actualDependents, expectedDependents);

    }

    // GetDependees  Tests -----------------

    /// <summary>
    /// Checks to make sure a node with a dependee does appear to have that dependee.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependees_GraphWithOnePair()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        IEnumerable<string> actualDependees = graph.GetDependees("b");
        List<string> expectedDependees = new();
        expectedDependees.Add("a");
        Assert.AreEqual(actualDependees, expectedDependees);


    }
    /// <summary>
    /// Checks to make sure a node which depends on multiple nodes does appear to have those specific dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependees_GraphWithMultipleDependees()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "a");
        graph.AddDependency("d", "a");
        graph.AddDependency("q", "a");
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        List<string> expectedDependees = new();
        expectedDependees.Add("b");
        expectedDependees.Add("c");
        expectedDependees.Add("d");
        expectedDependees.Add("q");
        Assert.AreEqual(actualDependees, expectedDependees);


    }
    /// <summary>
    /// Checks to make sure two nodes which depend on multiple nodes do appear to both have those specific dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphGetDependees_GraphWithMultipleDependeesAndDependees()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("z", "a");
        graph.AddDependency("f", "a");
        graph.AddDependency("v", "a");
        graph.AddDependency("t", "q");
        graph.AddDependency("e","q");
        IEnumerable<string> aActualDependees = graph.GetDependees("a");
        List<string> aExpectedDependees = new();
        aExpectedDependees.Add("b");
        aExpectedDependees.Add("z");
        aExpectedDependees.Add("f");
        aExpectedDependees.Add("v");
        IEnumerable<string> qActualDependees = graph.GetDependees("q");
        List<string> qExpectedDependees = new();
        qExpectedDependees.Add("t");
        qExpectedDependees.Add("e");
        Assert.AreEqual(aActualDependees, aExpectedDependees);
        Assert.AreEqual(qActualDependees, qExpectedDependees);

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
        List<string> expectedDependees = new();
        Assert.AreEqual(actualDependees, expectedDependees);

    }
    // Remove Dependency Tests -------------------


    /// <summary>
    /// Checks to make sure a node with a dependee can have all of its dependency's removed and return the proper
    /// dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphRemoveDependees_GraphWithOnePair()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        graph.RemoveDependency("a", "b");
        IEnumerable<string> actualDependees = graph.GetDependees("b");
        List<string> expectedDependees = new();
        Assert.AreEqual(actualDependees, expectedDependees);


    }
    /// <summary>
    /// Checks to make sure a node which depends on multiple nodes can have some of its nodes removed and return
    /// the correct nodes for its dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphRemoveDependees_GraphWithMultipleDependees()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("b", "a");
        graph.AddDependency("c", "a");
        graph.AddDependency("d", "a");
        graph.AddDependency("q", "a");
        graph.RemoveDependency("q", "a");
        graph.RemoveDependency("d", "a");
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        List<string> expectedDependees = new();
        expectedDependees.Add("b");
        expectedDependees.Add("c");
        Assert.AreEqual(actualDependees, expectedDependees);


    }
    /// <summary>
    /// Checks to make sure two nodes which depend on multiple nodes can have their nodes removed and return the correct
    /// nodes for its dependees.
    /// This test doubles as an AddDependency test.
    /// </summary>
    [TestMethod]
    public void DependencyGraphRemoveDependees_GraphWithMultipleDependeesAndDependees()
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
        List<string> aExpectedDependees = new();
        aExpectedDependees.Add("b");
        aExpectedDependees.Add("z");
        IEnumerable<string> qActualDependees = graph.GetDependees("q");
        List<string> qExpectedDependees = new();
        qExpectedDependees.Add("t");
        Assert.AreEqual(aActualDependees, aExpectedDependees);
        Assert.AreEqual(qActualDependees, qExpectedDependees);

    }

    /// <summary>
    /// Checks to make sure a node with no dependees does appear to not have any dependees and when attempting to
    /// remove a nonexistent dependee that no exception will be thrown.
    /// </summary>
    [TestMethod]
    public void DependencyGraphRemoveDependees_RemovingFromNodeWithNoDependees_Valid()
    {
        DependencyGraph graph = new DependencyGraph();
        graph.AddDependency("a", "b");
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        List<string> expectedDependees = new();
        graph.RemoveDependency("b", "a");
        Assert.AreEqual(actualDependees, expectedDependees);

    }

    // AddDependency  Tests -----------------

    // ReplaceDependents  Tests -----------------

    /// <summary>
    /// Tests replacing one nodes only dependent with a new dependent.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceOneDependentWithOneDependent()
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
        List<string> expectedDependents = new();
        expectedDependents.Add("f");
        expectedDependents.Add("g");
        expectedDependents.Add("h");
        expectedDependents.Add("p");
        Assert.AreEqual(actualDependents, expectedDependents);

    }
    /// <summary>
    /// Tests replacing one nodes only dependent with many new dependents.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceOneDependentWithMultipleDependent()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        List<string> replacementDependents = new();
        replacementDependents.Add("f");
        replacementDependents.Add("g");
        replacementDependents.Add("h");
        replacementDependents.Add("p");
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        List<string> expectedDependents = new();
        expectedDependents.Add("f");
        expectedDependents.Add("g");
        expectedDependents.Add("h");
        expectedDependents.Add("p");
        Assert.AreEqual(actualDependents, expectedDependents);
    }
    /// <summary>
    /// Tests replacing one nodes many dependents with one dependent.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceManyDependentWithOneDependent()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependents = new();
        replacementDependents.Add("f");
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        List<string> expectedDependents = new();
        expectedDependents.Add("f");
        Assert.AreEqual(actualDependents, expectedDependents);
    }
    /// <summary>
    /// Tests replacing one nodes many dependents with many new dependents.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceManyDependentWithManyDependents()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependents = new();
        replacementDependents.Add("f");
        replacementDependents.Add("g");
        replacementDependents.Add("h");
        replacementDependents.Add("p");
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        List<string> expectedDependents = new();
        expectedDependents.Add("f");
        expectedDependents.Add("g");
        expectedDependents.Add("h");
        expectedDependents.Add("p");
        Assert.AreEqual(actualDependents, expectedDependents);
    }
    /// <summary>
    /// Tests replacing one nodes many dependents with no dependents.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceManyDependentWithNoDependents()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependents = new();
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        List<string> expectedDependents = new();
        Assert.AreEqual(actualDependents, expectedDependents);
    }
    /// <summary>
    /// Tests replacing one nodes only dependent with no dependents.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceOneDependentWithNoDependents()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        List<string> replacementDependents = new();
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        List<string> expectedDependents = new();
        Assert.AreEqual(actualDependents, expectedDependents);
    }
    /// <summary>
    /// Tests "replacing" a nodes dependents (none) with many dependents. Effectively making the method an add all nodes method
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependents_ReplaceNoDependentsWithManyDependents()
    {
        DependencyGraph graph = new();
        List<string> replacementDependents = new();
        replacementDependents.Add("f");
        replacementDependents.Add("g");
        replacementDependents.Add("h");
        replacementDependents.Add("p");
        graph.ReplaceDependents("a", replacementDependents);
        IEnumerable<string> actualDependents = graph.GetDependents("a");
        List<string> expectedDependents = new();
        expectedDependents.Add("f");
        expectedDependents.Add("g");
        expectedDependents.Add("h");
        expectedDependents.Add("p");
        Assert.AreEqual(actualDependents, expectedDependents);
    }

    // ReplaceDependees  Tests -----------------

    /// <summary>
    /// Tests replacing one nodes only dependee with a new dependee.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceOneDependentWithOneDependent()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependees = new();
        replacementDependees.Add("f");
        replacementDependees.Add("g");
        replacementDependees.Add("h");
        replacementDependees.Add("p");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        List<string> expectedDependees = new();
        expectedDependees.Add("f");
        expectedDependees.Add("g");
        expectedDependees.Add("h");
        expectedDependees.Add("p");
        Assert.AreEqual(actualDependees, expectedDependees);

    }
    /// <summary>
    /// Tests replacing one nodes only dependee with many new dependees.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceOneDependentWithMultipleDependent()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        List<string> replacementDependees = new();
        replacementDependees.Add("f");
        replacementDependees.Add("g");
        replacementDependees.Add("h");
        replacementDependees.Add("p");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        List<string> expectedDependees = new();
        expectedDependees.Add("f");
        expectedDependees.Add("g");
        expectedDependees.Add("h");
        expectedDependees.Add("p");
        Assert.AreEqual(actualDependees, expectedDependees);
    }
    /// <summary>
    /// Tests replacing one nodes many dependees with one dependee.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceManyDependentWithOneDependent()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependees = new();
        replacementDependees.Add("f");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        List<string> expectedDependees = new();
        expectedDependees.Add("f");
        Assert.AreEqual(actualDependees, expectedDependees);
    }
    /// <summary>
    /// Tests replacing one nodes many dependees with many new dependees.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceManyDependentWithManyDependees()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependees = new();
        replacementDependees.Add("f");
        replacementDependees.Add("g");
        replacementDependees.Add("h");
        replacementDependees.Add("p");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        List<string> expectedDependees = new();
        expectedDependees.Add("f");
        expectedDependees.Add("g");
        expectedDependees.Add("h");
        expectedDependees.Add("p");
        Assert.AreEqual(actualDependees, expectedDependees);
    }
    /// <summary>
    /// Tests replacing one nodes many dependees with no dependees.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceManyDependentWithNoDependees()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        graph.AddDependency("a", "c");
        graph.AddDependency("a", "d");
        graph.AddDependency("a", "e");
        List<string> replacementDependees = new();
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        List<string> expectedDependees = new();
        Assert.AreEqual(actualDependees, expectedDependees);
    }
    /// <summary>
    /// Tests replacing one nodes only dependee with no dependees.
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceOneDependentWithNoDependees()
    {
        DependencyGraph graph = new();
        graph.AddDependency("a", "b");
        List<string> replacementDependees = new();
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        List<string> expectedDependees = new();
        Assert.AreEqual(actualDependees, expectedDependees);
    }
    /// <summary>
    /// Tests "replacing" a nodes dependees (none) with many dependees. Effectively making the method an add all nodes method
    /// </summary>
    [TestMethod]
    public void DependencyGraphReplaceDependees_ReplaceNoDependeesWithManyDependees()
    {
        DependencyGraph graph = new();
        List<string> replacementDependees = new();
        replacementDependees.Add("f");
        replacementDependees.Add("g");
        replacementDependees.Add("h");
        replacementDependees.Add("p");
        graph.ReplaceDependees("a", replacementDependees);
        IEnumerable<string> actualDependees = graph.GetDependees("a");
        List<string> expectedDependees = new();
        expectedDependees.Add("f");
        expectedDependees.Add("g");
        expectedDependees.Add("h");
        expectedDependees.Add("p");
        Assert.AreEqual(actualDependees, expectedDependees);
    }
    // Stress Tests -------------------------
    /// <summary>
    ///   FIXME: Explain carefully what this code tests.
    ///          Also, update in-line comments as appropriate.
    /// </summary>
    [TestMethod]
    [Timeout(2000)]  // 2 second run time limit <-- remove this comment
    public void StressTest()
    {
        DependencyGraph dg = new();

        // A bunch of strings to use
        const int SIZE = 200;
        string[] letters = new string[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            letters[i] = string.Empty + ((char)('a' + i));
        }

        // The correct answers
        HashSet<string>[] dependents = new HashSet<string>[SIZE];
        HashSet<string>[] dependees = new HashSet<string>[SIZE];
        for (int i = 0; i < SIZE; i++)
        {
            dependents[i] = [];
            dependees[i] = [];
        }

        // Add a bunch of dependencies
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 1; j < SIZE; j++)
            {
                dg.AddDependency(letters[i], letters[j]);
                dependents[i].Add(letters[j]);
                dependees[j].Add(letters[i]);
            }
        }

        // Remove a bunch of dependencies
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 4; j < SIZE; j += 4)
            {
                dg.RemoveDependency(letters[i], letters[j]);
                dependents[i].Remove(letters[j]);
                dependees[j].Remove(letters[i]);
            }
        }

        // Add some back
        for (int i = 0; i < SIZE; i++)
        {
            for (int j = i + 1; j < SIZE; j += 2)
            {
                dg.AddDependency(letters[i], letters[j]);
                dependents[i].Add(letters[j]);
                dependees[j].Add(letters[i]);
            }
        }

        // Remove some more
        for (int i = 0; i < SIZE; i += 2)
        {
            for (int j = i + 3; j < SIZE; j += 3)
            {
                dg.RemoveDependency(letters[i], letters[j]);
                dependents[i].Remove(letters[j]);
                dependees[j].Remove(letters[i]);
            }
        }

        // Make sure everything is right
        for (int i = 0; i < SIZE; i++)
        {
            Assert.IsTrue(dependents[i].SetEquals(new HashSet<string>(dg.GetDependents(letters[i]))));
            Assert.IsTrue(dependees[i].SetEquals(new HashSet<string>(dg.GetDependees(letters[i]))));
        }
    }


}
