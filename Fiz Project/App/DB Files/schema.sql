CREATE DATABASE IF NOT EXISTS food_db;
USE food_db;

-- Table for pantry items
CREATE TABLE Items (
    ItemId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Category VARCHAR(100) NOT NULL,
    Quantity INT NOT NULL DEFAULT 0,
    Exp_date DATE,
    Barcode VARCHAR(255) UNIQUE
);

-- Table for students
CREATE TABLE Students (
    StudentId INT AUTO_INCREMENT PRIMARY KEY,
    QR_Code VARCHAR(255) UNIQUE
);

-- Table for volunteers
CREATE TABLE Volunteers (
    VolunteerID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Role VARCHAR(50),
    Password VARCHAR(255)
);

-- Table for recording pantry usage by students
CREATE TABLE PantryUsage (
    UsageID INT AUTO_INCREMENT PRIMARY KEY,
    StudentId INT NOT NULL,
    Visit_date DATE DEFAULT (CURRENT_DATE),
    Total_items INT DEFAULT 0,
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
        ON DELETE CASCADE
);

-- Table for volunteer shifts
CREATE TABLE Shifts (
    ShiftId INT AUTO_INCREMENT PRIMARY KEY,
    Shift_date DATE NOT NULL,
    Shift_start TIME NOT NULL,
    Shift_end TIME NOT NULL
);

-- Table for volunteer hours
CREATE TABLE VolunteerHours (
    HoursId INT AUTO_INCREMENT PRIMARY KEY,
    VolunteerID INT NOT NULL,
    ShiftId INT NOT NULL,
    Worked_hours INT,
    FOREIGN KEY (VolunteerID) REFERENCES Volunteers(VolunteerID)
        ON DELETE CASCADE,
    FOREIGN KEY (ShiftId) REFERENCES Shifts(ShiftId)
        ON DELETE CASCADE
);
