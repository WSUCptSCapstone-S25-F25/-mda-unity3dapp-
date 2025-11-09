# Sprint 7 Report (10/11/2025 – 11/08/2025)

## What’s New (User Facing)

* **User Authentication:** A full user authentication system has been implemented. Users can now log in, log out, and have their session managed (Issue #72).
* **Login & Registration:** New pages for user login and student registration have been designed and implemented (Issue #74).
* **Admin Registration:** A separate, dedicated page for registering new Admin accounts has been created (Issue #76).
* **User Management Views:** Admin users, once logged in, can now view lists of all registered Students, Volunteers, and Admins in the system (Issue #80).

## Work Summary (Developer Facing)

This sprint was heavily focused on building the core user management and authentication backbone of the Flask application.

The primary accomplishment was the full implementation of a user authentication system using Flask sessions (Issue #72). This included creating the necessary login (Issue #74) and admin-specific registration pages (Issue #76).

To support this, the backend `app.py` file was significantly expanded with new routes for logging in/out, registering, and handling the new user-view pages. All relevant HTML templates were updated or created (Issue #82). We also implemented routes and HTML pages to allow admins to view the `Students`, `Volunteers`, and `Admins` tables (Issue #80).

The database schema (`schema.sql`) and seed data (`seed_data.sql`) were updated multiple times to support these new features, modifying tables and adding test users (Issues #65, #73, #81).

Finally, key project documentation was completed, including submitting the project draft (Issue #69), creating the frontend `README.md` (Issue #70), updating client hours logs (Issues #67, #84), and submitting the capstone poster assignment (Issue #79).

## Unfinished Work

* The primary functional piece remaining is the "Shifts" management system, which includes the routes and pages to view, create, edit, and delete shifts (Issue #78).
* A dedicated registration page for students to apply as volunteers is still pending (Issue #77).
* Documentation is still being finalized, including the "Solution Approach" section of the main project report (Issue #71) and an update to the frontend README (Issue #75).
* The Entity-Relationship Diagram needs to be updated to reflect the latest database schema changes (Issue #86).

## Completed Issues / User Stories

* **Issue #65:** Update schema tables and seed_data files.
* **Issue #67:** Update client hours.
* **Issue #69:** Submit Project Draft.
* **Issue #70:** Create a README files for frontend.
* **Issue #72:** Expand frontend: Add user authentication system.
* **Issue #73:** Modify seed_data file.
* **Issue #74:** Design Login and Registration Pages.
* **Issue #76:** Create admin register page.
* **Issue #79:** Complete and submit poster assignment.
* **Issue #80:** Implement routes and pages to view students, volunteers and admins.
* **Issue #81:** Update schema and seed data file.
* **Issue #82:** Update html files and routes in app.py.
* **Issue #84:** Update client hours.

## Incomplete Issues / User Stories

* **Issue #71:** Complete and upload solution approach section.
* **Issue #75:** Update frontend README file.
* **Issue #77:** Create volunteer register page.
* **Issue #78:** Implement view, create, edit and delete shifts.
* **Issue #83:** Complete and upload Sprint 7 report.
* **Issue #85:** Complete and upload sprint demo video.
* **Issue #86:** Update Entity-Relationship Diagram.

## Code Files for Review

* `Fiz Project/App/app.py`
* `Fiz Project/App/DB Files/schema.sql`
* `Fiz Project/App/DB Files/seed_data.sql`
* `Fiz Project/App/templates/login.html`
* `Fiz Project/App/templates/register.html`
* `Fiz Project/App/templates/register_admin.html`
* `Fiz Project/App/templates/students.html`
* `Fiz Project/App/templates/volunteers.html`
* `Fiz Project/App/templates/admins.html`
* `README.md`

## Retrospective Summary

**What Went Well:**
* This was a very productive sprint for feature development. We successfully built and implemented the entire user authentication and management backbone of the application.
* The database schema is stabilizing, and the seed data file is proving very useful for testing these new user-related features.
* The team also successfully met external deadlines for the poster and project draft.

**What Needs Improvement:**
* Documentation is lagging slightly behind development. The ER diagram is now out of sync with the schema (Issue #86), and the Solution Approach section needs to be written (Issue #71).
* With the auth system in place, we now need to lock down routes to be role-specific (e.g., only admins can access admin pages).

**Changes to Implement in the Next Sprint:**
* Prioritize development of the **Shifts** management functionality (Issue #78) as it's the next major piece of the application.
* Update all key documentation (ER Diagram, README, and the main report) to reflect the current state of the application.
* Begin implementing role-based authorization for all Flask routes.
