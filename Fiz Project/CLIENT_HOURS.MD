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
A very early Entity-Relationship diagram was shared with the client through Teams. The client also further emphasized that the default state of the application is the clientele-facing page. Volunteers should see a different sign-in option. Our database structure must implement separate data models for both clients and volunteers.
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
 <li>Still need detailed specifications from the client on exact reporting requirements.</li> 
<li>Need to establish a workflow for syncing progress with the website team.</li>
 </ul> 
<h3>Changes to implement ASAP:</h3>
 <ul> <li>Finalize and refine ER diagram to capture usage, inventory, volunteer hours, and shift scheduling.</li> 
<li>Collaborate with the website team to define database endpoints and data exchange format.</li>
 </ul>
<h2>Agenda (<em>10/2/2025</em>)</h2> <ul> <li>Review and feedback on current database schema.</li> <li>Discussion on user roles and relationships between entities.</li> <li>Determining additional tables and reporting requirements.</li> </ul> <h2>Minutes</h2> <p>Cristobal met with our client, <strong>Maynard Siev</strong>, to discuss continued progress on the Voiland Food Pantry &amp; Wellness Center Data Tracker project. The discussion focused on the database structure and refining the Entity-Relationship (ER) model.</p> <p>The client approved the overall schema direction and suggested improvements for better data organization. Maynard emphasized adding a <strong>Donors</strong> table to record contributions, donation sources, and frequencies. He also confirmed that <strong>volunteers will only consist of students</strong>, meaning the <em>Student</em> and <em>Volunteer</em> tables should be related in the schema.</p> <p>Three main user types were finalized: <strong>Administrators</strong>, <strong>Students (main users)</strong>, and <strong>Volunteers</strong>. Each will have distinct access privileges and user interfaces suited to their role. The client also requested that the database support <strong>reporting on items nearing expiration</strong> to help manage inventory rotation and reduce waste.</p> <h2>Retrospective Summary</h2> <h3>This went well:</h3> <ul> <li>Received clear feedback to refine database schema and entity relationships.</li> <li>Confirmed user role hierarchy: Admin, Student, and Volunteer.</li> <li>Identified new feature requirements for donor tracking and near-expiry item reports.</li> </ul> <h3>This needs improvement:</h3> <ul> <li>Define precise access permissions and restrictions for each user type.</li> <li>Clarify how donor data connects to inventory and reporting functionalities.</li> <li>Plan for implementing automated expiry alerts or reporting scripts.</li> </ul> <h3>Changes to implement ASAP:</h3> <ul> <li>Add a <strong>Donors</strong> table and link it with the <em>Inventory</em> table.</li> <li>Update ER diagram to show the relationship between <em>Students</em> and <em>Volunteers</em>.</li> <li>Implement logic for generating <strong>reports on soon-to-expire items</strong>.</li> <li>Document access levels and privileges for each user type.</li> </ul>

<h2>Agenda (<em>10/8/2025</em>)</h2>
<ul>
  <li>Coordinate progress between the database and website teams.</li>
  <li>Confirm MVP goals for the next sprint.</li>
  <li>Discuss scanner functionality and inventory tracking priorities.</li>
</ul>

<h2>Minutes</h2>
<p>
A joint meeting was held with the database team, website team, and clients Maynard Siev and Alena Hume. The discussion focused on coordinating progress between both teams and ensuring that each group’s deliverables align for the next development phase. The client reaffirmed that the WordPress platform will serve as the main website interface for students and volunteers.

The website team emphasized the need to define a clear data exchange structure between the two teams to simplify integration later in the semester. The group discussed the upcoming sprint’s objectives, which include focusing on inventory management and barcode scanner testing as the next major milestone. The client also mentioned a potential on-site visit to the Voiland Food Pantry to better understand the current check-in process and hardware setup.
</p>

<h2>Retrospective Summary</h2>

<h3>This went well:</h3>
<ul>
  <li>Confirmed WordPress as the official front-end platform for the project.</li>
  <li>Established clear sprint goals focusing on inventory and scanner integration.</li>
</ul>

<h3>This needs improvement:</h3>
<ul>
  <li>Continue defining the data exchange format for compatibility between the WordPress and MySQL databases.</li>
  <li>Improve communication rhythm between both teams for status updates and shared milestones.</li>
</ul>

<h3>Changes to implement ASAP:</h3>
<ul>
  <li>Finalize export and import scripts for WordPress integration testing.</li>
  <li>Schedule the planned on-site pantry visit during mid-October for system observation.</li>
</ul>

