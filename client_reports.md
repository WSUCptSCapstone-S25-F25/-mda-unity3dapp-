# Client Meetings Report

<h2>Agenda (<em>1/28/2025</em>)</h2>
<ul>
  <li>Initial client greeting.</li>
  <li>Brief explanation of tin whiskers.</li>
  <li>Brief explanation of preceding team's work.</li>
</ul>

<h2>Minutes</h2>
<p>NASA subject matter expert Mr. Jay Brusse, MDA STEM advisor Dr. Donna Havrisik, government sponsor at MDA Mr. Stephen Wells made initial contact with members of our team, Ram Logic. Additionally, Mr. Jay Brusse gave our team a brief overview of metal whiskers.
Mr. Jay Brusse asked to set up a second meeting to further inform our team of tin whiskers.</p>

<h2>Retrospective Summary</h2>
<h3>This went well:</h3>
<ul>
  <li>Initial greetings were thorough, and our team was able to extract important information of client roles.</li>
  <li>Subject matter expert, Mr. Jay Brusse, gave a concise explanation on tin whiskers.</li>
</ul>

<h3>This needs improvement:</h3>
<ul>
  <li>Understanding of project goals.</li>
  <li>Understanding of predecessor work.</li>
</ul>

<h3>Changes to implement ASAP:</h3>
<ul>
  <li>Immediate changes are unneccesary; time should be spent researching predecessor work.</li>
</ul>

<h2>Agenda (<em>1/30/2025</em>)</h2>
<ul>
  <li>Historical overview of metal whiskers.</li>
  <li>Cause of metal whisker growth.</li>
  <li>Discussion of important mathematical concepts.</li>
</ul>

<h2>Minutes</h2>
<p>NASA subject matter expert Mr. Jay Brusse explains the impact of metal whiskers historically and attempts at preventing metal whisker growth. Mr. Jay Brusse discussed the growth of metal whiskers, emphasizing it is not conceptually similar to 
"squeezing a tube of toothpaste." Finally Mr. Brusse touches on important statistical concepts in this projects context such as the lognormal distribution.</p>

<h2>Retrospective Summary</h2>
<h3>This went well:</h3>
<ul>
  <li>The importance of the project is emphasized as Mr. Brusse discussed the historical tin whisker failures.</li>
  <li>Lognormal distribution and the Monte Carlo Method were discussed and set as one of the team's learning goals.</li>
</ul>

<h3>This needs improvement:</h3>
<ul>
  <li>Mr. Brusse emphasizes the importance of learning the preceding team's code base as well as challenges they faced.</li>
  <li>Understanding of project goals.</li>
  <li>Understanding of client requirements.</li>
</ul>

<h3>Changes to implement ASAP:</h3>
<ul>
  <li>Brush up on statistical concepts.</li>
  <li>Dive into preceding team's work.</li>
</ul>

<h2>Agenda (<em>2/4/2025</em>)</h2>
<ul>
  <li>Improvements of the application</li>
  <li> Client requirements</li>
  <li>Ranking the project requirements based on priority level</li>
</ul>
 
<h2>Minutes</h2>
<p> In this meeting we discuss with our mentor Jay about how we could improve GPU acceleration in the app, he gave us some insights based on what he knew and what the previous team suggested, he also encouraged us to reach out to them for further details. Dr. Havrisik started giving some insights about the client requirements but her connection wasn’t the best, then she lost connection and we didn’t get much unfortunately. Mr. Wells mentioned he wasn’t involved as much with the previous team and that we had to review our requirements rankings with Dr. Havrisik.
 </p>

<h2>Retrospective Summary</h2>
<h3>This went well:</h3>
<ul>
  <li>Got some information to start working in requirements section. </li>
  <li>Getting to know more about our long-term goals </li>
</ul>
 
<h3>This needs improvement:</h3>
<ul>
  <li>Understanding how we can improve GPU acceleration in Unity. </li>
    <li>Understanding the requirements ranking. </li>
  <li>Understanding the solution approach. </li>
</ul>
 
<h3>Changes to implement ASAP:</h3>
<ul>
  <li>Reach to previous team for clarification regarding GPU acceleration. </li>
<li> Email and ask Dr. Havrisik for more information about the requirements and the priority rankings.
</ul>

<h2>Agenda (<em>2/11/2025</em>)</h2>  
<ul>  
  <li>Discuss project priorities and scope.</li>  
  <li>Address challenges with Unity physics (CPU vs. GPU).</li>  
  <li>Consider alternatives to GPU-based physics, focusing on optimizing existing algorithms and UI.</li>  
  <li>Explore methods for identifying conductive parts on a PCB.</li>  
</ul>  

<h2>Minutes</h2>  
<p>NASA subject matter expert Mr. Jay Brusse and government sponsor at MDA Mr. Stephen Wells met with our team, Ram Logic, to discuss project priorities and scope. Donna Havrisik was unable to attend.</p>  

<p>The team revisited the challenges of running physics simulations in Unity. By default, Unity uses PhysX, which runs on the CPU, and transitioning to GPU-based physics would require a custom physics engine, making it an impractical solution. Research confirmed that while options such as Compute Shaders, NVIDIA Flex, and external libraries exist, they all demand extensive custom development. Given these constraints, the team is now considering refining existing algorithms and UI to enhance application performance rather than building a custom physics engine.</p>  

<p>Additionally, the previous team was contacted, and they expressed similar concerns regarding the complexity of GPU-based physics. Their struggles reinforced our decision to focus on optimizing the built-in Unity physics engine instead of developing a new one.</p>  

<p>Another key discussion point was automating the identification of conductive parts on a PCB. One potential method involves having a team member manually tag conductive areas and save them in a metadata file for future reference. An alternative approach would involve searching for metallic components using automated techniques, but this requires further exploration.</p>  

<h2>Retrospective Summary</h2>  

<h3>This went well:</h3>  
<ul>  
  <li>Confirmed project priorities and refined scope, ruling out the need for a custom physics engine.</li>  
  <li>Gained insights from the previous team, validating our decision to improve existing algorithms and UI.</li>  
  <li>Identified potential approaches for conductive PCB part detection.</li>  
</ul>  

<h3>This needs improvement:</h3>  
<ul>  
  <li>Further exploration of UI and algorithm optimizations to enhance performance.</li>  
  <li>More detailed investigation into automated conductive part detection techniques.</li>  
</ul>  

<h3>Changes to implement ASAP:</h3>  
<ul>  
  <li>Focus efforts on refining the application’s existing physics algorithms and UI rather than GPU-based physics.</li>  
  <li>Begin prototyping a metadata tagging approach for conductive PCB parts.</li>  
  <li>Continue researching alternative automated methods for identifying metallic components.</li>  
</ul>  

<h2>Agenda (<em>2/18
/2025</em>)</h2>
<ul>
  <li>Discuss progress on optimizing Unity’s built-in physics and UI performance.</li>
  <li>Review initial prototype for PCB conductive part identification</li>
  <li>Finalize project priorities and next steps.</li>
</ul>
 
<h2>Minutes</h2>
<p> For this short meeting Dr. Havrisk was absent, Mr. Jay and Mr. Stephen attended our meeting to discuss possible solutions to optimize Unity’s physics engine and improve the app performance. Mr. Stephen suggested that if we believe that improving GPU acceleration might be too challenging or that it will take more time then we have, we should consider discarding it since it was suggested by the previous team and not necessarily required from them. We also discussed about different types of CAD files and how we could use some of these types of files to detect conductive materials, Jay suggested that we should try to talk to someone that is expert building/designing CAD files and obtain more information about what files could be of use for automation of conductive materials. Also, Mr. Steven offered to ask an expert in CAD and try to set a meeting with this person for us, so we can ask questions and gain some insights on how to proceed with automation of conductive materials. </p>
<h2>Retrospective Summary</h2>
<h3>This went well:</h3>
<ul>
  <li>Gained insights about how to proceed if GPU acceleration is out of range.  </li>
  <li>Discuss with client about possible CAD files that we could use to detect conductive materials.</li>
 <li> Got suggestions about who we should ask to inform ourselves more about CAD files.</li>
</ul>
 
<h3>This needs improvement:</h3>
<ul>
  <li>Research more about different CAD files that the app could support. </li>
  <li>Understanding how we can do automation of conductive materials . </li>
  
</ul>
 
<h3>Changes to implement ASAP:</h3>
<ul>
  <li>Reach out to Dr. Havrisik to clarify if we need to include a mockup design</li>
<li> Lower priority on GPU acceleration and focus on other requirements
</ul>

<h2>Agenda (<em>2/28/2025</em>)</h2>

<ul>
  <li>Jay demonstrates an Excel program for generating reports on the app.</li>
  <li>Discussion on benchmarking our program.</li>
  <li>Update from Stephen regarding a colleague who can assist with PCB file education.</li>
  <li>Progress update on the app development.</li>
</ul>

<h2>Minutes</h2>

<p>NASA subject matter expert Mr. Jay Brusse and government sponsor at MDA Mr. Stephen Wells met with our team, Ram Logic. Dr. Donna Havrisik was absent. Mr. Jay Brusse demonstrated an Excel program he developed to generate reports on the app's performance and data analysis. He emphasized the importance of benchmarking our program to ensure it meets performance standards and aligns with project goals. Mr. Stephen Wells provided an update on a colleague who could assist in educating the team on PCB files, but he encouraged the team to continue making progress on the app even without a full understanding of PCB files at this stage.</p>

<h2>Retrospective Summary</h2>

<h3>This went well:</h3>

<ul>
  <li>Jay's Excel program demonstration was clear and provided a useful tool for generating reports on the app.</li>
  <li>Stephen's update on potential educational support for PCB files was helpful and forward-looking.</li>
</ul>

<h3>This needs improvement:</h3>

<ul>
  <li>Further clarification on the benchmarking process and specific metrics to focus on.</li>
  <li>Continued progress on the app development despite the lack of full understanding of PCB files.</li>
</ul>

<h3>Changes to implement ASAP:</h3>

<ul>
  <li>Begin benchmarking our program using the metrics and standards discussed during the meeting.</li>
  <li>Continue app development efforts, focusing on areas that do not require in-depth knowledge of PCB files.</li>
</ul>

<h2>Agenda (<em>Next Meeting</em>)</h2>

<ul>
  <li>Review benchmarking results and discuss any necessary adjustments.</li>
  <li>Update on app development progress.</li>
  <li>Further discussion on PCB file education and potential collaboration with Stephen's colleague.</li>
</ul>
<h2>Agenda (<em>3/4/2025</em>)</h2>
<ul>
  <li>Update on Stephen’s colleague for PCB file education.</li>
  <li>Review of Sprint 2 progress.</li>
</ul>

<h2>Minutes</h2>
<p>Government sponsor at MDA Mr. Stephen Wells met with our team, Ram Logic. Dr. Donna Havrisik and Mr. Jay Brusse were absent. Mr. Wells provided an update on a colleague who could assist with PCB file education but mentioned a delay in availability. In the meantime, the team decided to research PCB basics independently to ensure progress. Sprint 2 progress was reviewed, focusing on UI improvements and core functionality, both of which were reported to be on track.</p>

<h2>Retrospective Summary</h2>

<h3>This went well:</h3>
<ul>
  <li>Received clear feedback from Stephen regarding Sprint 2.</li>
</ul>

<h3>This needs improvement:</h3>
<ul>
  <li>Expedite plans for PCB training to avoid delays in development.</li>
</ul>

<h3>Changes to implement ASAP:</h3>
<ul>
  <li>Document Sprint 2 outcomes for future reference.</li>
  <li>Follow up on the timeline for PCB training.</li>
</ul>

<h2>Agenda (<em>3/11/2025</em>)</h2>
<ul>
  <li>Confirm Sprint 3 progress during check-in.</li>
</ul>

<h2>Minutes</h2>
<p>This was a brief check-in meeting, due to Spring Break. The team confirmed that there were no blockers and that Sprint 3 progress was proceeding as planned.</p>

<h2>Agenda (<em>3/18/2025</em>)</h2>
<ul>
  <li>Review of Altium processor parsing and rendering in Unity.</li>
</ul>

<h2>Minutes</h2>
<p>NASA subject matter expert Mr. Jay Brusse, government sponsor at MDA Mr. Stephen Wells, and the Ram Logic Team attended the meeting. Dr. Donna Havrisik was absent. Mr. Brusse provided a detailed technical review of the requirements for integrating Altium data into Unity. The team asked clarifying questions to better understand the scope of the task. Mr. Wells emphasized the importance of ensuring that all developments align with NASA/MDA standards.</p>

<h2>Retrospective Summary</h2>

<h3>This went well:</h3>
<ul>
  <li>Received thorough technical guidance from Mr. Brusse regarding Altium-Unity integration.</li>
</ul>

<h3>This needs improvement:</h3>
<ul>
  <li>Establish clearer milestones and a structured timeline for Unity implementation.</li>
</ul>

<h3>Changes to implement ASAP:</h3>
<ul>
  <li>Draft a workflow for Altium-Unity integration.</li>
  <li>Schedule a follow-up session with Mr. Brusse to address any outstanding questions.</li>
</ul>

<h2>Agenda (<em>Next Meeting – 3/25/2025</em>)</h2>
<ul>
  <li>Review the drafted Altium-Unity integration workflow.</li>
  <li>Prepare for Sprint 3 demo.</li>
</ul>

<h2>Agenda (3/25/2025)</h2>  
<ul>  
  <li>Discuss upcoming sprint plans, including paper, testing, and Altium processor feature implementation.</li>  
  <li>Kyle will be progressing report by meeting with a statistician.</li>  
</ul>  

<h2>Minutes</h2>  
<p>Mr. Stephen Wells and the Ram Logic Team attended the meeting. Mr. Jay Brusse was unable to attend. The team discussed upcoming sprint plans, including progress on the paper, testing strategies, and the implementation of a new feature for Altium processors.</p>  

<h2>Retrospective Summary</h2>  

<h3>This went well:</h3>  
<ul>  
  <li>Clear discussion of sprint priorities.</li>  
  <li>Team aligned on key goals for paper, testing, and feature implementation.</li>  
</ul>  

<h3>This needs improvement:</h3>  
<ul>  
  <li>More structured updates for tracking progress.</li>  
</ul>  

<h3>Changes to implement ASAP:</h3>  
<ul>  
  <li>Ensure consistent documentation of progress on Altium processor feature.</li>  
  <li>Continue development on paper and testing strategies.</li>  
</ul>  

---

<h2>Agenda (4/01/2025)</h2>  
<ul>  
  <li>Review testing and acceptance portion of the project.</li>  
  <li>Discuss completed documentation and create a timeline for systems testing.</li>  
  <li>Plan to complete systems testing within this sprint.</li>  
  <li>Kyle will be progressing report by meeting with a statistician.</li>  
</ul>  

<h2>Minutes</h2>  
<p>NASA subject matter expert Mr. Jay Brusse, government sponsor at MDA Mr. Stephen Wells, and the Ram Logic Team attended the meeting. Dr. Donna Havrisik was absent. The discussion focused on the testing and acceptance portion of the project. The team reviewed completed documentation and worked on creating a structured timeline for systems testing, aiming to complete it within the current sprint. Mr. Brusse inquired about the expected results from the testing process.</p>  

<h2>Retrospective Summary</h2>  

<h3>This went well:</h3>  
<ul>  
  <li>Progress on documentation and testing timeline creation.</li>  
  <li>Clear goal to complete systems testing this sprint.</li>  
</ul>  

<h3>This needs improvement:</h3>  
<ul>  
  <li>Further refinement of expected testing outcomes and validation metrics.</li>  
</ul>  

<h3>Changes to implement ASAP:</h3>  
<ul>  
  <li>Finalize the testing timeline and begin execution.</li>  
  <li>Provide clear documentation of test results for review.</li>  
</ul> 

<h2>Agenda (<em>4/17/2025</em>)</h2>

<ul>
<li> Discussed about our findings regarding automotive conductive material </li>
  <li> Consulting with client about saving conductive material state </li>
  <li> Discussed type of video demonstration</li>
  <li>Schedule a video demonstration.</li>
  
</ul>

<h2>Minutes</h2>

<p>NASA subject matter expert Mr. Jay Brusse and government sponsor at MDA Mr. Stephen Wells met with our team, Ram Logic. Dr. Donna Havrisik was absent. In this short meeting we discussed with our client what kind of video demonstration they prefer, they decided a short video demonstration was enough since we’ve been updating our progress frequently. Also a time was scheduled to do the client demonstration. We updated our clients based on our findings about conductive materials and how complex it looks like. </p>
<h2>Retrospective Summary</h2>

<h3>This went well:</h3>

<ul>
  <li>Figured out what format video demo our clients prefer.</li>
  <li>Schedule a time to do the client demo. </li>
</ul>

<h3>This needs improvement:</h3>

<ul>
  <li> Research other options to see if automation of conductive material is possible </li>
  <li>Continued adding unit tests</li>
</ul>

<h3>Changes to implement ASAP:</h3>

<ul>
  <li>Add issues as smaller tasks </li>
  <li> Continue systems testing.</li>
</ul>

<h2>Agenda (<em>4/24/2025</em>)</h2>

<ul>
<li> Demonstration of the state of the application</li>
  <li> Discussion about future work and plans</li>
  <li> Feedback about current progress </li>
  
</ul>

<h2>Minutes</h2>

<p>NASA subject matter expert Mr. Jay Brusse met with our team, Ram Logic. Dr. Donna Havrisik and Mr. Stephen couldn’t attend this meeting. This meeting was to demonstrate what we’ve been working on and how the program looks currently. </p>
<h2>Retrospective Summary</h2>

<h3>This went well:</h3>

<ul>
  <li> Successfully showed current application</li>
  <li> Received comments and suggestions </li>

</ul>

<h3>This needs improvement:</h3>

<ul>
  <li> Continue working on research paper </li>
  <li>Complete project report</li>
</ul>

<h3>Changes to implement ASAP:</h3>

<ul>
  <li>Add more issues </li>
  <li> Continue expanding unit tests.</li>
</ul>

