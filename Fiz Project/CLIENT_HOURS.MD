# Client Meetings Report

<h2>Agenda (<em>9/3/2025</em>)</h2>
 <ul> <li>Introduction to project background and overview.</li>
 <li>Expectations and intended users of the system.</li> 
<li>Clarification of division of work between website and database teams.</li>
 </ul> 

<h2>Minutes</h2> 

<p>Maynard Siev, our client, introduced our team to the Voiland Food Pantry & Wellness Center Data Tracker project. The project aimed to design a user-friendly system that would motivate students to sign in and track their pantry usage. Students would check in with their Cougar Card and a digital form as a backup.
We asked for clarification about the website team working on the frontend, while our team would be working on the database. The database must be flexible to store inventory, volunteer hours, and collect pantry usage data. It should also be easy to expand to multiple departments if desired. The client emphasized that the budget would be an issue and the simplicity of implementation should be a top concern.</p>

<h2>Retrospective Summary</h2> 
<h3>This went well:</h3>
 <ul>
 <li>Established the division of responsibilities between the website team and our team.</li>
 <li>Understood core client needs: Cougar Card integration, backup digital sign-in, and simple data entry.</li> 
</ul> 
<h3>This needs improvement:</h3> 
<ul> 
<li>Further clarification is required on the exact data fields to collect for usage, inventory, and volunteer tracking.</li>
 <li>Need to coordinate closely with the website team to ensure smooth database integration.</li>
 </ul> 
<h3>Changes to implement ASAP:</h3>
 <ul>
 <li>Draft initial Entity-Relationship (ER) diagram to outline tables and relationships.</li> <li>Begin compiling a list of required data fields based on client priorities.</li> </ul>


<h2>Agenda (<em>9/10/2025</em>)</h2>
 <ul>
 <li>Specification clarification </li>
 <li>Database team introduction and updates.</li> 
<li>Hardware availability and integration requirements.</li> 
</ul> 
<h2>Minutes</h2> 
<p>The client confirmed that they had the hardware (a Cougar Card reader and barcode scanner) that we can access to integrate into our database. They have both tested and confirmed working conditions, and had recently installed the latest drivers. The client also expects it to be completely IT approved shortly.
We had a new addition to our database team, Alan Sun. After we went over our project requirements, a very early Entity-Relationship diagram was shared with the client through Teams. The client also further emphasized that the default state of the application is the clientele-facing page. Volunteers should see a different sign-in option. Our database structure must implement separate data models for both clients and volunteers.
The client also reconfirmed that the website would be on a WordPress domain, so we will need our database to be available to that platform in some manner. Our short-term deliverable will be a collaborative specification report, where we outline the website and database details.</p>
<h2>Retrospective Summary</h2> 
<h3>This went well:</h3> 
<ul> 
<li>Confirmed available hardware for integration with the database (Cougar Card reader and scanner).</li>
 <li>Began ER diagram drafting.</li> 
<li>Clearer understanding of how volunteer vs. client sign-ins will need to be modeled in the database.</li> 
</ul>
 <h3>This needs improvement:</h3> 
<ul>
 <li>Still need detailed specifications from the client on exact reporting requirements (e.g., usage reports, volunteer hour logs).</li> 
<li>Need to establish a workflow for syncing progress with the website team.</li>
 </ul> 
<h3>Changes to implement ASAP:</h3>
 <ul> <li>Finalize and refine ER diagram to capture usage, inventory, volunteer hours, and shift scheduling.</li> 
<li>Collaborate with the website team to define database endpoints and data exchange format.</li>
 </ul>

