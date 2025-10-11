# Food Pantry Database – Flask Front-End

## Project Overview
This project is part of the Voiland Food Pantry Capstone at Washington State University.  
It provides a local web-based front end to manage the Food Pantry’s database before integration with the main WordPress website.

The system allows pantry staff and volunteers to:
- View, add, edit, and delete inventory items
- Validate quantity and expiration date fields
- Interact with a local MySQL database through a Flask web interface


###Technologies Used
- Python Flask – web framework for the front-end interface  
- MySQL – relational database for persistent storage  
- mysql-connector-python – library for connecting Flask to MySQL  

This front end serves as a testing and validation tool for the local database, ensuring it functions correctly before integration with the WordPress website managed by the partner team.

---

## Prerequisites

### 1. Python 3
If not installed: 
``` bash
> winget install Python.Python.3 
```

### 2. MySQL 8.0 server 
If not installed:

Download from: https://dev.mysql.com/downloads/mysql/ 

### 3.Flask
If not installed: 
``` bash
> pip install flask
```

### 4.  Mysql.connector 
If not installed: 
``` bash
> pip install flask mysql-connector-python
```

## Running  App
- Go to App directory and open terminal.
- In terminal run: App> python -m flask run
- Open in browser: http://127.0.0.1:5000/


## File Structure and Explanations

``` bash
App/
│
├── pycache/ # Compiled Python files
│
├── DB Files/ # Database schema and seed data
│ ├── schema.sql
│ └── seed_data.sql
│
├── static/ # Static files (CSS, JS, images)
│
├── templates/ # HTML templates
│ ├── add_item.html
│ ├── edit_item.html
│ ├── home.html
│ └── inventory.html
│
├── app.py # Main Flask application
│
└── README.md # Project documentation
```

## Future Improvements
Add support for student visits and volunteer management 
- Implement user authentication (volunteer/admin login)  
- Integrate barcode / QR scanner support  
- Generate inventory reports (e.g., low-stock or expired items)  
- Enable automatic export to WordPress database

## Credits
Developed by the Database Team for the  
Voiland Food Pantry Project – WSU Capstone (CPTS 423)  




 



