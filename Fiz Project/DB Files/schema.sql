CREATE DATABASE food_db
USE food_db

CREATE TABLE Items (
    ItemId INT AUTOINCREMENT PRIMARY KEY, 
    Name VARCHAR(100) NOT NULL,
    Category VARCHAR(100) NOT NULL,
    Quantity INT NOT NULL DEFAULT 0,
    Exp_date DATE,
    -- Barcode = UNIQUE
    
);

CREATE TABLE Students (
    StudentId INT AUTOINCREMENT PRIMARY KEY, 
    -- QR Code?
);


CREATE TABLE Volunteers (
    VolunteerID INT AUTOINCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    -- Role = Admin, user, student ? 
    -- Password ?
);


 CREATE TABLE PantryUsage (
    UsageID INT AUTOINCREMENT PRIMARY KEY,
    StudentId INT NOT NULL,
    Visit_date DATE DEFAULT (CURRENT_DATE), 
    Total_items INT DEFAULT 0,

    FOREIGN KEY (StudentId) REFERENCES Students(StudentId) ON DELETE CASCADE,
    FOREIGN KEY (ItemId) REFERENCES Items(ItemId) ON DELETE CASCADE

 );


CREATE TABLE Shifts (
    ShiftId INT AUTOINCREMENT PRIMARY KEY,
    Shift_date DATE NOT NULL,
    Shift_start DATE NOT NULL,
    Shift_end DATE NOT NULL;
)


CREATE TABLE VolunteerHours (
    HoursId INT AUTOINCREMENT PRIMARY KEY,
    VolunteerID INT NOT NULL,
    ShiftId INT NOT NULL,
    -- Worked_hours INT?

    FOREIGN KEY (VolunteerID) REFERENCES Volunteers(VolunteerID) ON DELETE CASCADE,
    FOREIGN KEY (ShiftId) REFERENCES Shifts(ShiftId) ON DELETE CASCADE
)



