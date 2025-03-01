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
