# Sprint 4 Report (4/2/2025 - 5/2/2025)

## What's New (User Facing)
 * Added unit test cases
 * Materials selected as conductive are save and loaded back when loading PCB again

## Work Summary (Developer Facing)
For this sprint, we added some test scripts to verify the correctness of the objects SimState and WhiskerCollider. We implement saving the current state of PCB conductive components in a file and load it once the PCB is loaded again. We also work on documentation, we added alpha prototype description, alpha prototype demonstration and future work section into our project report.

## Unfinished Work

## Completed Issues/User Stories
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/28
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/29
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/31
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/37
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/38
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/39 

 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 * Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/22
 * Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/32
 * Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/36 

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
Test scripts: 
* TestSimState https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/unit-testing/Tin%20Whisker%20POC/Assets/Tests/TestSimState.cs
* TestWhiskerCollider https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/unit-testing/Tin%20Whisker%20POC/Assets/Tests/TestWhiskerCollider.cs

 
## Retrospective Summary
What Went Well:
* Successfully saved and loaded the PCB state.
* Expanded unit testing

What Needs Improvement:
* Be more active in github

Changes to Implement in the Next Sprint:
* Research deeper into other options about how to automate conductive material detection
* Finish unit and system testing.

Video: https://www.youtube.com/watch?v=KxmNcI2kibQ 


# Kyle's Sprint 4 Report (5/13/2025 - 6/27/2025)

## What's New (User Facing)
 * Added Logger System
 * More than 100 simulations can be run in the monte-carlo model.
 * Optimized monte-carlo simulations
 * Added test PCBs with non-equidistant pads.

## Work Summary (Developer Facing)
For this sprint, a modular, extensible logger system was added to the program. The logger works with the WhiskerSim.cs class, the MonteCarlo.cs class, and the MainController.cs class. More than 100 simulations can be run with the monte-carlo model as a result of refactoring UI code. Monte-carlo simulations were monolithic so the MonteCarlo.cs class was broken down -- this resulted in optimized monte-carlo simulations. Additionally, simple test PCBs were added as requested by the client.

## Unfinished Work
Issues 42 and 47 (These both addressed redundant log generation) were unable to be completed because issue 53 (Addressing codebase refactoring) was not completed. Codebase refactoring to reduce monolithic structuring in code is not a trivial issue. Issue 43 (Determine optimal number of whiskers to spawn) will be abandoned -- it should be up to the user to determine the amount of whiskers to spawn, regardless of time expense.

## Completed Issues/User Stories
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/40 
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/41
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/44 
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/45 
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/46
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/48 
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/51 

 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 * Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/42 
 * Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/43 
 * Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/47
 * Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/53 

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
Test scripts: 
* Tin Whisker POC/Assets/Scripts/LoggingComponents/*.cs https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/tree/main/Tin%20Whisker%20POC/Assets/Scripts/LoggingComponents 
* MainController.cs https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/unit-testing/Tin%20Whisker%20POC/Assets/Tests/TestWhiskerCollider.cs
* MonteCarloSim.cs https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/main/Tin%20Whisker%20POC/Assets/Scripts/Simulation/MonteCarloSim.cs 
* WhiskerSim.cs https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/main/Tin%20Whisker%20POC/Assets/Scripts/Simulation/WhiskerSim.cs  

 
## Retrospective Summary
What Went Well:
* Created logger system
* Optimized codebase
* Refactored [some of] the codebase

What Needs Improvement:
* Communication with the client

Changes to Implement in the Next Sprint:
* Continue refactoring codebase to allow for unit and system testing in the future.

Video: [Sprint 5 report video](https://youtu.be/HslXHINKQSw)



