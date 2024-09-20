// <copyright file="DependencyGraph.cs" company="UofU-CS3500">
// Copyright (c) 2024 UofU-CS3500 All rights reserved.
// </copyright>
// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta
// Version 1.3 - H. James de St. Germain Fall 2024
// (Clarified meaning of dependent and dependee.)
// (Clarified names in solution/project structure.)
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
//  This project is made for the purpose of finding which cells are dependent on other cells in a spreadsheet.
//  We achieve this through creating a dependency graph which maps out what cells are dependees to their dependents.
//
// </summary>

// Ignore Spelling: Dependees dependee
namespace CS3500.DependencyGraph;

/// <summary>
///   <para>
///     (s1,t1) is an ordered pair of strings, meaning t1 depends on s1.
///     (in other words: s1 must be evaluated before t1.)
///   </para>
///   <para>
///     A DependencyGraph can be modeled as a set of ordered pairs of strings.
///     Two ordered pairs (s1,t1) and (s2,t2) are considered equal if and only
///     if s1 equals s2 and t1 equals t2.
///   </para>
///   <remarks>
///     Recall that sets never contain duplicates.
///     If an attempt is made to add an element to a set, and the element is already
///     in the set, the set remains unchanged.
///   </remarks>
///   <para>
///     Given a DependencyGraph DG:
///   </para>
///   <list type="number">
///     <item>
///       If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
///       (The set of things that depend on s.)
///     </item>
///     <item>
///       If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
///       (The set of things that s depends on.)
///     </item>
///   </list>
///   <para>
///      For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}.
///   </para>
///   <code>
///     dependents("a") = {"b", "c"}
///     dependents("b") = {"d"}
///     dependents("c") = {}
///     dependents("d") = {"d"}
///     dependees("a")  = {}
///     dependees("b")  = {"a"}
///     dependees("c")  = {"a"}
///     dependees("d")  = {"b", "d"}
///   </code>
/// </summary>
public class DependencyGraph
{
    // The node and all of the nodes that depend on it.
    private Dictionary<string, HashSet<string>> dependents;

    // The node and all nodes it depends on.
    private Dictionary<string, HashSet<string>> dependees;

    // The size of the graph. The number of Dependency pairs in the graph.
    private int sizeOfGraph;

    /// <summary>
    ///   Initializes a new instance of the <see cref="DependencyGraph"/> class.
    ///   The initial DependencyGraph is empty.
    /// </summary>
    public DependencyGraph()
    {
        this.dependees = new Dictionary<string, HashSet<string>>();
        this.dependents = new Dictionary<string, HashSet<string>>();
        this.sizeOfGraph = 0;
    }

    /// <summary>
    /// Gets the number of ordered pairs in the DependencyGraph.
    /// </summary>
    public int Size
    {
        get { return this.sizeOfGraph; }
    }

    /// <summary>
    ///   Reports whether the given node has dependents (i.e., other nodes depend on it).
    /// </summary>
    /// <param name="nodeName"> The name of the node.</param>
    /// <returns> true if the node has dependents. </returns>
    public bool HasDependents(string nodeName)
    {
        bool hasDependents = this.dependents.ContainsKey(nodeName);
        return hasDependents;
    }

    /// <summary>
    ///   Reports whether the given node has dependees (i.e., depends on one or more other nodes).
    /// </summary>
    /// <returns> true if the node has dependees.</returns>
    /// <param name="nodeName">The name of the node.</param>
    public bool HasDependees(string nodeName)
    {
        bool hasDependees = this.dependees.ContainsKey(nodeName);
        return hasDependees;
    }

    /// <summary>
    ///   <para>
    ///     Returns the dependents of the node with the given name.
    ///   </para>
    /// </summary>
    /// <param name="nodeName"> The node we are looking at.</param>
    /// <returns> The dependents of nodeName. </returns>
    public IEnumerable<string> GetDependents(string nodeName)
    {
        if (this.dependents.ContainsKey(nodeName))
        {
            HashSet<string> nodesDependents = this.dependents[nodeName];
            return nodesDependents;
        }
        else
        {
            return new HashSet<string>();
        }
    }

    /// <summary>
    ///   <para>
    ///     Returns the dependees of the node with the given name.
    ///   </para>
    /// </summary>
    /// <param name="nodeName"> The node we are looking at.</param>
    /// <returns> The dependees of nodeName. </returns>
    public IEnumerable<string> GetDependees(string nodeName)
    {
        if (this.dependees.ContainsKey(nodeName))
        {
            HashSet<string> nodesDependees = this.dependees[nodeName];
            return nodesDependees;
        }
        else
        {
            return new HashSet<string>();
        }
    }

    /// <summary>
    /// <para>
    ///   Adds the ordered pair (dependee, dependent), if it doesn't already exist (otherwise nothing happens).
    /// </para>
    /// <para>
    ///   This can be thought of as: dependee must be evaluated before dependent.
    /// </para>
    /// </summary>
    /// <param name="dependee"> The name of the node that must be evaluated first. </param>
    /// <param name="dependent"> The name of the node that cannot be evaluated until after the other node has been. </param>
    public void AddDependency(string dependee, string dependent)
    {
        int dependentCount = this.dependents.Count;
        int dependeeCount = this.dependees.Count;
        bool increaseSize = false;

        if (!this.dependents.ContainsKey(dependee))
        {
            this.dependents.Add(dependee, new HashSet<string>());
            int ogCount = this.dependents[dependee].Count;
            this.dependents[dependee].Add(dependent);
            if (this.dependents[dependee].Count != ogCount)
            {
                increaseSize = true;
            }
        }
        else
        {
            int ogCount = this.dependents[dependee].Count;
            this.dependents[dependee].Add(dependent);
            if (this.dependents[dependee].Count != ogCount)
            {
                increaseSize = true;
            }
        }

        if (!this.dependees.ContainsKey(dependent))
        {
            this.dependees.Add(dependent, new HashSet<string>());
            int ogCount = this.dependees[dependent].Count;
            this.dependees[dependent].Add(dependee);
            if (this.dependees[dependent].Count != ogCount)
            {
                increaseSize = true;
            }
        }
        else
        {
            int ogCount = this.dependees[dependent].Count;
            this.dependees[dependent].Add(dependee);
            if (this.dependees[dependent].Count != ogCount)
            {
                increaseSize = true;
            }
        }

        if (increaseSize)
        {
            this.sizeOfGraph++;
        }
    }

    /// <summary>
    ///   <para>
    ///     Removes the ordered pair (dependee, dependent), if it exists (otherwise nothing happens).
    ///   </para>
    /// </summary>
    /// <param name="dependee"> The name of the node that must be evaluated first. </param>
    /// <param name="dependent"> The name of the node that cannot be evaluated until the other node has been. </param>
    public void RemoveDependency(string dependee, string dependent)
    {
        bool containedDependee = false;
        bool containedDependent = false;
        if (this.dependents.ContainsKey(dependee))
        {
            this.dependents[dependee].Remove(dependent);
            if (this.dependents[dependee].Count == 0)
            {
                this.dependents.Remove(dependee);
            }

            containedDependee = true;
        }

        if (this.dependees.ContainsKey(dependent))
        {
            this.dependees[dependent].Remove(dependee);
            if (this.dependees[dependent].Count == 0)
            {
                this.dependees.Remove(dependent);
            }

            containedDependent = true;
        }

        if (containedDependent && containedDependee)
        {
            this.sizeOfGraph--;
        }
    }

    /// <summary>
    ///   Removes all existing ordered pairs of the form (nodeName, *).  Then, for each
    ///   t in newDependents, adds the ordered pair (nodeName, t).
    /// </summary>
    /// <param name="nodeName"> The name of the node who's dependents are being replaced. </param>
    /// <param name="newDependents"> The new dependents for nodeName. </param>
    public void ReplaceDependents(string nodeName, IEnumerable<string> newDependents)
    {
        if (this.dependents.ContainsKey(nodeName))
        {
            // Removes all references to of nodeName from the dependees to make sure we are updating
            // both data structures.
            HashSet<string> nodesOriginalDependents = (HashSet<string>)this.GetDependents(nodeName);
            foreach (string dependent in nodesOriginalDependents)
            {
                this.dependees[dependent].Remove(nodeName);
                if (this.dependees[dependent].Count == 0)
                {
                    this.dependees.Remove(dependent);
                }
            }

            int sizeOfNodeSet = this.dependents[nodeName].Count;
            this.sizeOfGraph -= sizeOfNodeSet;
            this.dependents[nodeName].Clear();
            foreach (string newDependent in newDependents)
            {
                this.AddDependency(nodeName, newDependent);
            }
        }
        else
        {
            this.dependents.Add(nodeName, new HashSet<string>());
            foreach (string newDependent in newDependents)
            {
                this.AddDependency(nodeName, newDependent);
            }
        }

        if (this.dependents[nodeName].Count == 0)
        {
            this.dependents.Remove(nodeName);
        }
    }

    /// <summary>
    ///   <para>
    ///     Removes all existing ordered pairs of the form (*, nodeName).  Then, for each
    ///     t in newDependees, adds the ordered pair (t, nodeName).
    ///   </para>
    /// </summary>
    /// <param name="nodeName"> The name of the node who's dependees are being replaced. </param>
    /// <param name="newDependees"> The new dependees for nodeName. Could be empty.</param>
    public void ReplaceDependees(string nodeName, IEnumerable<string> newDependees)
    {
        if (this.dependees.ContainsKey(nodeName))
        {
            // Removes all references to of nodeName from the dependents to make sure we are updating
            // both data structures.
            HashSet<string> nodesOriginalDependees = (HashSet<string>)this.GetDependees(nodeName);
            foreach (string dependee in nodesOriginalDependees)
            {
                this.dependents[dependee].Remove(nodeName);
                if (this.dependents[dependee].Count == 0)
                {
                    this.dependents.Remove(dependee);
                }
            }

            int sizeOfNodeSet = this.dependees[nodeName].Count;
            this.sizeOfGraph -= sizeOfNodeSet;
            this.dependees[nodeName].Clear();
            foreach (string newDependee in newDependees)
            {
                this.AddDependency(newDependee, nodeName);
            }
        }
        else
        {
            this.dependees.Add(nodeName, new HashSet<string>());
            foreach (string newDependee in newDependees)
            {
                this.AddDependency(newDependee, nodeName);
            }
        }

        if (this.dependees[nodeName].Count == 0)
        {
            this.dependees.Remove(nodeName);
        }
    }
}
