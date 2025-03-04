# Sprint 2 Report (2/2/2025 - 3/2/2025)

## What's New (User Facing)
 * Fixed Monte Carlo simulation bugs
.csv file not being generated due to directory complications
Monte Carlo crashing program due to divide by zero
 * Implemented prototype NVIDIA GPU hardware acceleration
 * Fixed gitignore for .idea files in warp_implementation directory.

## Work Summary (Developer Facing)
For this sprint, more documentation was completed, including the addition of the requirements and specifications section and the solution approach section to the project report. Revisions and edits were made to the introduction and project description sections, and the client hours report was updated. Research was conducted to determine whether GPU acceleration was a feasible solution.

To improve the stability of the application, we debugged the program by carefully reviewing the code and taking the time to analyze relevant logic. This process involved learning to use Unity's debugging tools and systematically examining the application for bugs. We ranked the identified issues based on relevance and severity to prioritize fixes effectively.

Throughout the sprint, the team faced challenges related to debugging complex Monte Carlo simulation errors and ensuring compatibility with NVIDIA GPUs for acceleration. The process required an in-depth review of the simulation logic to resolve critical calculation errors. Additionally, we gained valuable experience using Unity’s profiling tools to diagnose performance bottlenecks. These efforts not only improved the application's reliability but also helped refine our approach to debugging and performance optimization.

## Unfinished Work
Generate report on benefits of GPU physics was not completed during this sprint. A statistician must be interviewed first.

To ensure proper tracking of this unfinished work:
(a) Any progress made has been documented in the issue tracker, including partial research findings and performance observations.
(b) A comment has been added to the issue explaining that additional time is required for data collection and analysis.
(c) The issue has been moved to the next sprint backlog so that we can complete the report with a more thorough evaluation of GPU physics benefits.

## Completed Issues/User Stories
Here are links to the issues that we completed in this sprint:
 
 * Issue #8: Compiling the newest version of Unity leads to errors.
 * Issue #12: Revise project description section of the report.
 * Issue #13: Complete and upload requirements and specifications.
 * Issue #14: Update client hours report.
 * Issue #15: Revise requirements and specification sections.
 * Issue #16: Complete and upload solution approach section.
 * Issue #19: Monte Carlo simulation pauses the whole program upon completion.
 * Issue #20: Experimental GPU physics simulator.
 
 ## Incomplete Issues/User Stories
 Here are links to issues we worked on but did not complete in this sprint:
 
 * Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/22: Generate report on benefits of GPU physics.  <<We need to contact experts and consult on the issues of whether or not certain features are needed or how much we need to scale the application>>

 * Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/23: Hover in End simulation button won't leave mouse and won't let run monte carlo.  <<We are able to disable the hover but we haven’t figure out how to make it visible only before clicking the button. >>

## Code Files for Review
Please review the following code files, which were actively developed during this sprint, for quality:
 * warp_implementation https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/tree/main/warp_implementation
 * ResultsProcessor.cs https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/main/Tin%20Whisker%20POC/Assets/Scripts/ResultsProcessor.cs


 
## Retrospective Summary

What Went Well:

* Debugging efforts significantly improved application stability, especially for Monte Carlo simulations.
* Implementing prototype GPU acceleration was a major step toward performance improvements.
* The team's ability to systematically track and rank bugs allowed for effective prioritization.
* Research into GPU physics provided insights into potential future optimizations.

What Needs Improvement:

* Time estimation for research-heavy tasks (e.g., GPU physics report) needs better scoping.
* More structured collaboration on documentation to ensure consistency across sections.
* Further refinement of benchmarking strategies to better measure GPU acceleration benefits.

Changes to Implement in the Next Sprint:

* Complete and finalize the report on GPU physics benefits, incorporating expert feedback.
* Expand performance benchmarking to gather more detailed insights.
* Improve documentation workflows to streamline writing and revisions.
* Continue refining the Monte Carlo simulation to ensure numerical stability.

# Sprint Report Video:
* https://youtu.be/eR3K5Z8yFhA 
