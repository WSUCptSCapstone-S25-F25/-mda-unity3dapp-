CREATE DATABASE food_db
USE food_db

CREATE TABLE Items (
    ItemId INT AUTOINCREMENT PRIMARY KEY, 
    Name VARCHAR(100) NOT NULL,
    Category VARCHAR(100) NOT NULL,
    Quantity INT NOT NULL DEFAULT 0,
    Exp_date DATE,
    % Barcode = UNIQUE
    
);

CREATE TABLE Students (
    StudentId INT AUTOINCREMENT PRIMARY KEY, 
    -- QR Code?
);


CREATE TABLE Volunteers (
    VolunteerID INT AUTOINCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    -- Role ? 
    -- Password ?
);


 CREATE TABLE PantryUsage (
    UsageID INT AUTOINCREMENT PRIMARY KEY,
    StudentId INT NOT NULL,
    Visit_date DATE DEFAULT (CURRENT_DATE), 

    FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
     ON DELETE CASCADE

 )

