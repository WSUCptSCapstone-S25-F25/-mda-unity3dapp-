USE food_db;

DELETE FROM ItemsTaken;
DELETE FROM PantryUsage;
DELETE FROM VolunteerHours;
DELETE FROM Shifts;
DELETE FROM Volunteers;
DELETE FROM Students;
DELETE FROM Admins;
DELETE FROM Items;

-- Resetting auto-increment counters (optional, for clean re-seeding)
ALTER TABLE Admins AUTO_INCREMENT = 1;
ALTER TABLE Students AUTO_INCREMENT = 1;
ALTER TABLE Volunteers AUTO_INCREMENT = 1;
ALTER TABLE Items AUTO_INCREMENT = 1;
ALTER TABLE Shifts AUTO_INCREMENT = 1;
ALTER TABLE VolunteerHours AUTO_INCREMENT = 1;
ALTER TABLE PantryUsage AUTO_INCREMENT = 1;
ALTER TABLE ItemsTaken AUTO_INCREMENT = 1;

-- -----------------------------------------
-- 1. Admins Table
-- -----------------------------------------
INSERT INTO Admins (Name, Email, Phone, Username, PasswordHash) VALUES
    ('Maynard Siev', 'maynard.siev@wsu.edu', '509-555-0000', 'maynard', 'password123');

-- -----------------------------------------
-- 2. Students Table
-- (Includes students who are non-volunteers, and those who will be volunteers)
-- -----------------------------------------
INSERT INTO Students (CougarId, Name, Email, Major, QRCode, Username, PasswordHash) VALUES
    (11111111, 'Alice Smith', 'alice.smith@wsu.edu', 'Computer Science', 'qr1111', 'asmith', 'pass1'),
    (22222222, 'Bob Johnson', 'bob.johnson@wsu.edu', 'Electrical Engineering', 'qr2222', 'bjohnson', 'pass2'),
    (33333333, 'Charlie Brown', 'charlie.brown@wsu.edu', 'History', 'qr3333', 'cbrown', 'pass3'),
    (44444444, 'Diana Prince', 'diana.prince@wsu.edu', 'Communications', 'qr4444', 'dprince', 'pass4'),
    (55555555, 'Eve Adams', 'eve.adams@wsu.edu', 'Hospitality Business Management', 'qr5555', 'eadams', 'pass5');

-- -----------------------------------------
-- 3. Volunteers Table
-- (References StudentId 1 and 2 from above)
-- -----------------------------------------
INSERT INTO Volunteers (StudentId, Phone) VALUES
    (1, '509-555-1111'), -- Alice Smith (StudentId 1) is VolunteerId 1
    (2, '509-555-2222'); -- Bob Johnson (StudentId 2) is VolunteerId 2

-- -----------------------------------------
-- 4. Items Table (Inventory)
-- (Includes one item nearing expiration for reporting functionality)
-- -----------------------------------------
INSERT INTO Items (Name, Category, Quantity, ExpDate, Barcode) VALUES
    ('Canned Beans', 'Canned Goods', 50, '2026-06-30', '001122334455'),
    ('Pasta Noodles', 'Dry Goods', 35, '2027-01-15', '002233445566'),
    ('Toilet Paper (4pk)', 'Hygiene', 20, '2028-12-31', '003344556677'),
    ('Box of Cereal', 'Breakfast', 40, '2025-05-10', '004455667788'),
    ('Peanut Butter', 'Spreads', 25, '2026-03-01', '005566778899'),
    ('Toothpaste', 'Hygiene', 15, '2025-01-01', '006677889900'); -- Near Expiry

-- -----------------------------------------
-- 5. Shifts Table
-- -----------------------------------------
INSERT INTO Shifts (ShiftDate, StartTime, EndTime) VALUES
    ('2025-10-17', '09:00:00', '12:00:00'), -- ShiftId 1
    ('2025-10-17', '12:00:00', '15:00:00'), -- ShiftId 2
    ('2025-10-18', '10:00:00', '14:00:00'); -- ShiftId 3

-- -----------------------------------------
-- 6. VolunteerHours Table
-- (Links VolunteerId and ShiftId)
-- -----------------------------------------
INSERT INTO VolunteerHours (VolunteerID, ShiftId, WorkedHours) VALUES
    (1, 1, 3),  -- Alice (VolunteerId 1) worked Shift 1
    (2, 3, 4);  -- Bob (VolunteerId 2) worked Shift 3

-- -----------------------------------------
-- 7. PantryUsage Table
-- (Records student check-ins. TotalItems should be manually set here for seeding)
-- -----------------------------------------
INSERT INTO PantryUsage (StudentId, VisitDate, TotalItems) VALUES
    (3, '2025-10-16', 3), -- Charlie's visit (UsageId 1)
    (4, '2025-10-16', 4), -- Diana's visit (UsageId 2)
    (5, '2025-10-17', 2); -- Eve's visit (UsageId 3)

-- -----------------------------------------
-- 8. ItemsTaken Table
-- (Links UsageId to ItemId and QuantityTaken, updating inventory logic)
-- -----------------------------------------
INSERT INTO ItemsTaken (UsageId, ItemId, QuantityTaken) VALUES
    (1, 1, 2), -- Charlie: 2 Canned Beans (Item 1)
    (1, 2, 1), -- Charlie: 1 Pasta Noodles (Item 2)
    (2, 3, 1), -- Diana: 1 Toilet Paper (Item 3)
    (2, 4, 1), -- Diana: 1 Box of Cereal (Item 4)
    (2, 5, 1), -- Diana: 1 Peanut Butter (Item 5)
    (2, 6, 1), -- Diana: 1 Toothpaste (Item 6)
    (3, 4, 2); -- Eve: 2 Box of Cereal (Item 4)