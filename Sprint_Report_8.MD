# Sprint 8 Report (11/09/2025 – 12/06/2025)

## What’s New (User Facing)
* Students can now submit a volunteer application including availability and a personal statement. 
* The system now distinguishes between Students, Volunteers, and Admins, granting different levels of access.
* Admins can view all shifts; formatting for times and dates is implemented.
* Home page dynamically adjusts available actions based on logged-in user role.
* Admins can now see volunteer applications and full details of each applicant.
* Updated project documentation.  

## Work Summary (Developer Facing)
This sprint focused heavily on completing the final functional components of the prototype and preparing for the end-of-semester demo. The most significant development work was implementing the complete volunteer application workflow, including schema changes, new routes, validation logic, and new HTML templates.

A major effort was refining the authentication system) to correctly detect student volunteer transitions and adjust permissions accordingly. The home page logic and item CRUD permissions were updated to properly reflect user type including admin / volunteer / student.

The team additionally worked on documentation deliverables including the final project report, team member bios, ERD updates, and the capstone poster, which were completed and submitted this sprint.

Finally, multiple bugs were resolved involving shift time formatting, SQL joins, volunteer field mismatches, and Flask session behavior. These fixes stabilized the system for the final prototype demonstration.

## Unfinished Work
* Implementing the admin workflow for approving/denying volunteer applications.
* Generating automatic weekly/monthly reports as requested by the client.
* Ensuring full role-based route protection across all endpoints.
* Allow the user to edit its information.

## Completed Issues / User Stories
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/75:  ** Update frontend README file.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/77: ** Create volunteer register page.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/78: ** Implement view, create, edit and delete shifts.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/86: ** Update Entity-Relationship Diagram.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/87:  ** Complete and upload Testing and Acceptance Plan section.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/88: ** Add contents page to report.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/89: ** Complete and upload Alpha Prototype Description section.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/90: ** Implement role-based access control.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/91:  ** Extended login functionality: support volunteer logins.
* **Issuehttps://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/93:  ** Modify admin view for volunteer and shifts management.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/94: ** Complete and Upload Sprint 8.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/95: ** Upload Final Report.
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/96:  ** Complete Client prototype demo.


## Incomplete Issues / User Stories
* **Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/92:  ** Implement admin approval/denial for volunteer applications.

## Code Files for Review
* volunteers.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/volunteers.html 
* volunteer_app.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/volunteer_app.html   
* view_volunteers_app.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/view_volunteers_app.html   
* inventory.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/inventory.html
* students.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/students.html 
* register.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/register.html 
* login.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/login.html 
* home.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/home.html 
* base.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/base.html 
* app.py: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/app.py 


## Retrospective Summary
**What Went Well:**  
* Successfully completed all major backend features for the final demo. 
* Added volunteer application workflow and fully integrated it with login system.
* Documentation and project poster were finished and submitted on time.
* Improved communication with the website team for integration planning. 

**What Needs Improvement:**  
* Volunteer approval and reporting features are still pending.
* Role-based access still needs tightening for production use. 

# Sprint Report Video:
* https://www.youtube.com/watch?v=dUc8ffunUfw 


