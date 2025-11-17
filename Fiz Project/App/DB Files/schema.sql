CREATE DATABASE IF NOT EXISTS food_db;
USE food_db;

-- Table for pantry items
CREATE TABLE Items (
    ItemId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Category VARCHAR(100) NOT NULL,
    Quantity INT NOT NULL DEFAULT 0,
    Weight Decimal (5,2),
    ExpDate DATE,
    UPC VARCHAR(255) UNIQUE,
    Comment TEXT
);

-- Table for students
CREATE TABLE Students (
    StudentId INT AUTO_INCREMENT PRIMARY KEY,
    CougarId INT NOT NULL,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Major VARCHAR(100) NOT NULL,
    QRCode VARCHAR(255) UNIQUE,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255)
);

-- Table for volunteers
CREATE TABLE Volunteers (
    VolunteerId INT AUTO_INCREMENT PRIMARY KEY,
    StudentId INT NOT NULL,
    Approved ENUM('Pending', 'Approved', 'Denied') DEFAULT 'Pending',
    Statement TEXT,
    PreferredDays TEXT,
    AppliedTime TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
        ON DELETE CASCADE
);

-- Table for Donors
-- CREATE TABLE Donors (
--     DonorId INT AUTO_INCREMENT PRIMARY KEY,
--     Name VARCHAR(100) NOT NULL,
--     Email VARCHAR(100) NOT NULL UNIQUE,
--     Phone VARCHAR(100) NOT NULL,
-- );

-- Table for Admins
CREATE TABLE Admins (
    AdminId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Phone VARCHAR(100) NOT NULL,
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255)
);

-- Table for recording pantry usage by students
CREATE TABLE PantryUsage (
    UsageId INT AUTO_INCREMENT PRIMARY KEY,
    StudentId INT NOT NULL,
    VisitDate DATE DEFAULT (CURRENT_DATE),
    TotalItems INT DEFAULT 0,
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
        ON DELETE CASCADE
);

-- Table for Items Taken 
CREATE TABLE ItemsTaken (
    ItemTakenId INT AUTO_INCREMENT PRIMARY KEY,
    UsageId INT NOT NULL,
    ItemId INT NOT NULL,
    QuantityTaken INT NOT NULL,
    FOREIGN KEY (UsageId) REFERENCES PantryUsage(UsageId)
        ON DELETE CASCADE,
    FOREIGN KEY (ItemId) REFERENCES Items(ItemId)
        ON DELETE CASCADE
);

-- Table for volunteer shifts
CREATE TABLE Shifts (
    ShiftId INT AUTO_INCREMENT PRIMARY KEY,
    ShiftDate DATE NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL
);

-- Table for volunteer hours
CREATE TABLE VolunteerHours (
    HoursId INT AUTO_INCREMENT PRIMARY KEY,
    VolunteerID INT NOT NULL,
    ShiftId INT NOT NULL,
    WorkedHours INT,
    FOREIGN KEY (VolunteerID) REFERENCES Volunteers(VolunteerID)
        ON DELETE CASCADE,
    FOREIGN KEY (ShiftId) REFERENCES Shifts(ShiftId)
        ON DELETE CASCADE
);
