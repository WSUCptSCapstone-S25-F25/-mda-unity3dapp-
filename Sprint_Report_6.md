# Sprint 6 Report (09/13/2025 – 10/11/2025)

## What’s New (User Facing)
* Created the initial MySQL database schema.  
* Started implementation of a local Flask-based front end for testing purposes.   
* Updated project documentation. 


## Work Summary (Developer Facing)
In this sprint, we created a MySQL database and started testing it locally.  The schema includes tables such as Items, Students, Volunteers, PantryUsage, Shifts, and VolunteerHours.  

A Flask web interface was created to evaluate database connections and functionalities forming a testing platform for interface design prior to delivery to the WordPress development team.  
We developed routes and functions for viewing, adding, editing and deleting inventory items while also implementing basic form validation.

Also, we updated our project report by adding the requirements and specifications section.

## Unfinished Work
* Implement user authentication and role-based login system.  
* Develop database export functionality for WordPress integration.  
* Define data synchronization workflow between Flask and WordPress systems.  
* Continue implementing form-level error handling for user input validation.

## Completed Issues / User Stories
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/58: Create database, add schema file.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/59: Create and add seed data file.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/60: Run local tests on schema.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/61: Create frontend to test database ( flask setup).  
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/62: Decide on format of database with team.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/63: Complete Requirements section.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/64: Add Entity-Relationship diagram draft.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/65: Update schema tables and seed_data files.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/66: Add edit and delete item buttons for the view inventory page.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/67: Update client hours.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/68: Complete and upload Sprint 6.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/69: Submit Project Draft.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/70: Create a README files for frontend.


## Incomplete Issues / User Stories
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/71: Complete and upload solution approach section.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/72: Expand frontend: Add user authentication system.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/73: Modify seed_data file.
* Issue https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/issues/74: Design Login and Registration Pages.

## Code Files for Review
* schema.sql: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/DB%20Files/schema.sql 
* seed_data.sql: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/DB%20Files/seed_data.sql   
* home.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/home.html   
* inventory.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/inventory.html
* add_item.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/add_item.html 
* edit_item.html: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/templates/edit_item.html 
* app.py: https://github.com/WSUCptSCapstone-S25-F25/-mda-unity3dapp-/blob/fiz-project/Fiz%20Project/App/app.py 


## Retrospective Summary
**What Went Well:**  
* Successfully created SQL schema and seed files for database setup.  
* Integrated Flask with MySQL and confirmed working CRUD operations.  
* Improved communication with the website team for integration planning. 

**What Needs Improvement:**  
* Synchronize front-end testing with database updates more consistently.  
* Increase communication frequency with the website team to ensure shared milestones. 

**Changes to Implement in the Next Sprint:**  
* Add authentication and login system for multiple user roles.  
* Develop export functionality for WordPress integration.  
* Prepare for scanner integration and role-based front-end updates.

# Sprint Report Video:
* https://www.youtube.com/watch?v=wI5cyJW-GlQ 
