import unittest
from unittest.mock import patch, MagicMock
from datetime import date
import json

# Import the functions to be tested from your script.
from food_pantry_CLI import (
    get_db_connection,
    view_all_items,
    add_item,
    record_student_visit,
    add_student,
    add_volunteer,
    add_shift,
    add_volunteer_hours,
)

class TestFoodPantryCLI(unittest.TestCase):
    """
    Unit tests for the Food Pantry CLI application.
    These tests mock the database connection to avoid actual database interactions.
    """

    def setUp(self):
        """Set up a mock database connection for each test."""
        # Patch the mysql.connector.connect function where it's called
        self.patcher = patch('food_pantry_CLI.mysql.connector.connect')
        self.mock_connect = self.patcher.start()

        # Create a mock connection and cursor
        self.mock_conn = MagicMock()
        self.mock_cursor = MagicMock()
        self.mock_conn.cursor.return_value = self.mock_cursor
        self.mock_connect.return_value = self.mock_conn
    
    def tearDown(self):
        """Stop the patcher after each test."""
        self.patcher.stop()

    def test_get_db_connection_success(self):
        """Test successful database connection."""
        conn = get_db_connection()
        self.assertEqual(conn, self.mock_conn)

    @patch('builtins.print')
    def test_view_all_items_with_data(self, mock_print):
        """Test viewing items when there is data in the database."""
        items = [{'ItemId': 1, 'Name': 'Rice', 'Category': 'Grain', 'Quantity': 5, 'Exp_date': date(2025, 12, 31), 'Barcode': '12345'}]
        self.mock_cursor.fetchall.return_value = items

        view_all_items()
        self.mock_cursor.execute.assert_called_with("SELECT * FROM Items")
        mock_print.assert_called()
        self.assertIn("Name: Rice", str(mock_print.call_args_list))

    @patch('builtins.print')
    def test_view_all_items_no_data(self, mock_print):
        """Test viewing items when the database table is empty."""
        self.mock_cursor.fetchall.return_value = []
        view_all_items()
        self.mock_cursor.execute.assert_called_with("SELECT * FROM Items")
        mock_print.assert_any_call("No items found in the inventory.")

    @patch('builtins.input', side_effect=['Test Item', 'Test Category', '10', '2025-12-31', '12345'])
    def test_add_item(self, mock_input):
        """Test adding a new item."""
        add_item()
        self.mock_cursor.execute.assert_called_with(
            "INSERT INTO Items (Name, Category, Quantity, Exp_date, Barcode) VALUES (%s, %s, %s, %s, %s)",
            ('Test Item', 'Test Category', 10, '2025-12-31', '12345')
        )
        self.mock_conn.commit.assert_called_once()
    
    @patch('builtins.input', side_effect=['101', 'Canned Soup', '2', 'done'])
    def test_record_student_visit(self, mock_input):
        """Test recording a new student visit with items taken."""
        record_student_visit()
        
        self.mock_cursor.execute.assert_called_with(
            "INSERT INTO PantryUsage (StudentId, Total_items) VALUES (%s, %s)",
            (101, 2)
        )
        self.mock_conn.commit.assert_called_once()

    @patch('builtins.input', side_effect=['123456789'])
    def test_add_student(self, mock_input):
        """Test adding a new student."""
        add_student()
        self.mock_cursor.execute.assert_called_with(
            "INSERT INTO Students (QR_Code) VALUES (%s)",
            ('123456789',)
        )
        self.mock_conn.commit.assert_called_once()

    @patch('builtins.input', side_effect=['John Smith', 'Coordinator', 'mysecretpassword'])
    def test_add_volunteer(self, mock_input):
        """Test adding a new volunteer."""
        add_volunteer()
        self.mock_cursor.execute.assert_called_with(
            "INSERT INTO Volunteers (Name, Role, Password) VALUES (%s, %s, %s)",
            ('John Smith', 'Coordinator', 'mysecretpassword')
        )
        self.mock_conn.commit.assert_called_once()

    @patch('builtins.input', side_effect=['2025-09-23', '09:00:00', '17:00:00'])
    def test_add_shift(self, mock_input):
        """Test adding a new shift."""
        add_shift()
        self.mock_cursor.execute.assert_called_with(
            "INSERT INTO Shifts (Shift_date, Shift_start, Shift_end) VALUES (%s, %s, %s)",
            ('2025-09-23', '09:00:00', '17:00:00')
        )
        self.mock_conn.commit.assert_called_once()

    @patch('builtins.input', side_effect=['1', '1', '8'])
    def test_add_volunteer_hours(self, mock_input):
        """Test recording volunteer hours."""
        add_volunteer_hours()
        self.mock_cursor.execute.assert_called_with(
            "INSERT INTO VolunteerHours (VolunteerID, ShiftId, Worked_hours) VALUES (%s, %s, %s)",
            (1, 1, 8)
        )
        self.mock_conn.commit.assert_called_once()

if __name__ == '__main__':
    unittest.main()
