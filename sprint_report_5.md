# Sprint 5 Report (5/13/2025 - 6/27/2025)

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

Video: 
