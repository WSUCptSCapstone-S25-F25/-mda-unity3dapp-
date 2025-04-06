using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using SimInfo;


public class SimStateTests
{
    /// <summary>
    /// Normal case testing for SaveSimToJSON()
    /// </summary>
    [Test]
    public void TestSaveSimToJSONNormalCase()
    {
        // Use the Assert class to test conditions
        SimState simState = new SimState();
        // creates path
        string newPath = Application.persistentDataPath + "/path.json";

        // deletes path if exists
        if (File.Exists(newPath))
        {
            File.Delete(newPath);
        }

        // checks if sim info was saved into newPath file
        simState.SaveSimToJSON(newPath);
        Assert.IsTrue(File.Exists(newPath));

    }

    /// <summary>
    /// Edge case testing for SaveSimToJSON() with empty path
    /// </summary>
    [Test]
    public void TestSaveSimToJSONEdgeCase()
    {
        // Use the Assert class to test conditions
        SimState simState = new SimState();
        // creates a path as empty string
        string newPath = "";

        // deletes path if exists
        if (File.Exists(newPath))
        {
            File.Delete(newPath);
        }

        // catch exception error
        try
        {
            simState.SaveSimToJSON(newPath);
        }
        catch(System.ArgumentException newException)
        {
            Assert.Pass("Argument Exception: " + newException.Message);
        }

    }

    /// <summary>
    /// Normal case testing for SaveSimToJSON() with invalid path
    /// </summary>
    [Test]
    public void TestSaveSimToJSONExceptionalCase()
    {
        // Use the Assert class to test conditions
        SimState simState = new SimState();
        // creates a path as empty string
        string newPath = "/nf_path.json";

        // deletes path if exists
        if (File.Exists(newPath))
        {
            File.Delete(newPath);
        }

        // catch exception error
        try
        {
            simState.SaveSimToJSON(newPath);
        }
        catch(System.Exception newException)
        {
            Assert.Pass("Exception: "+ newException.Message);
        }

    }

    /// <summary>
    /// Normal case testing for SaveToCSV()
    /// </summary>
    [Test]
    public void TestSaveToCSVNormalCase()
    {
        // Use the Assert class to test conditions
        SimState simState = new SimState();
        // creates path
        string newPath = Application.persistentDataPath + "/path.csv";

        // deletes path if exists
        if (File.Exists(newPath))
        {
            File.Delete(newPath);
        }

        // checks if sim info was saved into newPath file
        simState.SaveToCSV(newPath);
        Assert.IsTrue(File.Exists(newPath));

    }

    /// <summary>
    /// Edge case testing SaveToCSV() with empty path
    /// </summary>
    [Test]
    public void TestSaveToCSVEdgeCase()
    {
        // Use the Assert class to test conditions
        SimState simState = new SimState();
        // creates a path as empty string
        string newPath = "";

        // deletes path if exists
        if (File.Exists(newPath))
        {
            File.Delete(newPath);
        }

        // catch exception error
        try
        {
            simState.SaveToCSV(newPath);
        }
        catch(System.ArgumentException newException)
        {
            Assert.Pass("Argument Exception: " + newException.Message);
        }

    }

    /// <summary>
    /// Normal case testing SaveToJSON() with invalid path
    /// </summary>
    [Test]
    public void TestSaveToCSVExceptionalCase()
    {
        // Use the Assert class to test conditions
        SimState simState = new SimState();
        // creates a path as empty string
        string newPath = "/nf_path.csv";

        // deletes path if exists
        if (File.Exists(newPath))
        {
            File.Delete(newPath);
        }

        // catch exception error
        try
        {
            simState.SaveToCSV(newPath);
        }
        catch(System.Exception newException)
        {
            Assert.Pass("Exception: "+ newException.Message);
        }

    }


    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator NewTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}

