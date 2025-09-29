import mysql.connector
from mysql.connector import errorcode
import json
from datetime import date, time

# Database connection configuration.
# !!! IMPORTANT: Fill in your database credentials here.
db_config = {
    'user': 'your_username',
    'password': 'your_password',
    'host': 'localhost',
    'database': 'food_db'
}

def get_db_connection():
    """Establishes and returns a database connection."""
    try:
        conn = mysql.connector.connect(**db_config)
        return conn
    except mysql.connector.Error as err:
        if err.errno == errorcode.ER_ACCESS_DENIED_ERROR:
            print("Error: Invalid username or password.")
        elif err.errno == errorcode.ER_BAD_DB_ERROR:
            print("Error: Database does not exist.")
        else:
            print(f"Error: {err}")
        return None

def view_all_items():
    """Fetches and displays all items from the database."""
    conn = get_db_connection()
    if conn is None:
        return

    cursor = conn.cursor(dictionary=True)
    query = "SELECT * FROM Items"
    try:
        cursor.execute(query)
        items = cursor.fetchall()
        
        if items:
            print("\n--- Current Inventory ---")
            for item in items:
                print(f"ID: {item['ItemId']}, Name: {item['Name']}, "
                      f"Category: {item['Category']}, Quantity: {item['Quantity']}, "
                      f"Exp Date: {item['Exp_date']}, Barcode: {item.get('Barcode', 'N/A')}")
        else:
            print("No items found in the inventory.")

    except mysql.connector.Error as err:
        print(f"Error viewing items: {err}")
    finally:
        cursor.close()
        conn.close()

def add_item():
    """Prompts for item details and adds a new item to the database."""
    conn = get_db_connection()
    if conn is None:
        return

    name = input("Enter item name: ")
    category = input("Enter category: ")
    try:
        quantity = int(input("Enter initial quantity: "))
    except ValueError:
        print("Invalid quantity. Please enter a valid number.")
        return
    exp_date = input("Enter expiration date (YYYY-MM-DD): ")
    barcode = input("Enter barcode (optional, press Enter to skip): ")

    cursor = conn.cursor()
    query = "INSERT INTO Items (Name, Category, Quantity, Exp_date, Barcode) VALUES (%s, %s, %s, %s, %s)"
    
    try:
        cursor.execute(query, (name, category, quantity, exp_date, barcode if barcode else None))
        conn.commit()
        print(f"Item '{name}' added successfully.")
    except mysql.connector.Error as err:
        print(f"Error adding item: {err}")
        conn.rollback()
    finally:
        cursor.close()
        conn.close()

def record_student_visit():
    """
    Records a new visit in the PantryUsage table with the total number of items taken.
    """
    try:
        student_id = int(input("Enter the student_ID for the visit: "))
    except ValueError:
        print("Invalid student ID. Please enter a valid number.")
        return
    
    total_items = 0
    print("\nEnter items taken (enter 'done' when finished):")
    while True:
        item_name = input("Item name (or 'done'): ")
        if item_name.lower() == 'done':
            break
        try:
            quantity_taken = int(input(f"Quantity of {item_name}: "))
            total_items += quantity_taken
        except ValueError:
            print("Invalid quantity. Please enter a valid number.")
            continue

    conn = get_db_connection()
    if conn is None:
        return
        
    cursor = conn.cursor()
    query = "INSERT INTO PantryUsage (StudentId, Total_items) VALUES (%s, %s)"

    try:
        cursor.execute(query, (student_id, total_items))
        conn.commit()
        print(f"Visit recorded for student_ID {student_id} with {total_items} items.")
    except mysql.connector.Error as err:
        print(f"Error recording visit: {err}")
        conn.rollback()
    finally:
        cursor.close()
        conn.close()

def add_student():
    """Adds a new student to the Students table."""
    conn = get_db_connection()
    if conn is None:
        return
    
    qr_code = input("Enter student's QR Code (optional, press Enter to skip): ")

    cursor = conn.cursor()
    query = "INSERT INTO Students (QR_Code) VALUES (%s)"

    try:
        cursor.execute(query, (qr_code if qr_code else None,))
        conn.commit()
        print(f"Student added successfully.")
    except mysql.connector.Error as err:
        print(f"Error adding student: {err}")
        conn.rollback()
    finally:
        cursor.close()
        conn.close()

def add_volunteer():
    """Adds a new volunteer to the Volunteers table."""
    conn = get_db_connection()
    if conn is None:
        return

    name = input("Enter volunteer's full name: ")
    role = input("Enter volunteer's role (optional, press Enter to skip): ")
    password = input("Enter volunteer's password (optional, press Enter to skip): ")
    
    cursor = conn.cursor()
    query = "INSERT INTO Volunteers (Name, Role, Password) VALUES (%s, %s, %s)"
    
    try:
        cursor.execute(query, (name, role if role else None, password if password else None))
        conn.commit()
        print(f"Volunteer '{name}' added successfully.")
    except mysql.connector.Error as err:
        print(f"Error adding volunteer: {err}")
        conn.rollback()
    finally:
        cursor.close()
        conn.close()

def add_shift():
    """Adds a new shift to the Shifts table."""
    conn = get_db_connection()
    if conn is None:
        return

    shift_date = input("Enter shift date (YYYY-MM-DD): ")
    shift_start = input("Enter shift start time (HH:MM:SS): ")
    shift_end = input("Enter shift end time (HH:MM:SS): ")

    cursor = conn.cursor()
    query = "INSERT INTO Shifts (Shift_date, Shift_start, Shift_end) VALUES (%s, %s, %s)"
    
    try:
        cursor.execute(query, (shift_date, shift_start, shift_end))
        conn.commit()
        print(f"Shift on {shift_date} from {shift_start} to {shift_end} added successfully.")
    except mysql.connector.Error as err:
        print(f"Error adding shift: {err}")
        conn.rollback()
    finally:
        cursor.close()
        conn.close()

def add_volunteer_hours():
    """Records volunteer hours for a specific shift."""
    conn = get_db_connection()
    if conn is None:
        return
    
    try:
        volunteer_id = int(input("Enter Volunteer ID: "))
        shift_id = int(input("Enter Shift ID: "))
        worked_hours = int(input("Enter worked hours: "))
    except ValueError:
        print("Invalid input. Please enter valid numbers.")
        return

    cursor = conn.cursor()
    query = "INSERT INTO VolunteerHours (VolunteerID, ShiftId, Worked_hours) VALUES (%s, %s, %s)"

    try:
        cursor.execute(query, (volunteer_id, shift_id, worked_hours))
        conn.commit()
        print(f"Volunteer {volunteer_id} logged {worked_hours} hours for shift {shift_id}.")
    except mysql.connector.Error as err:
        print(f"Error adding volunteer hours: {err}")
        conn.rollback()
    finally:
        cursor.close()
        conn.close()

def main_menu():
    """Main menu loop for the CLI application."""
    while True:
        print("\n--- Food Pantry Management System ---")
        print("1. View all items")
        print("2. Add a new item")
        print("3. Add a new student")
        print("4. Add a new volunteer")
        print("5. Record a student visit")
        print("6. Add a new shift")
        print("7. Record volunteer hours")
        print("8. Exit")
        
        choice = input("Enter your choice: ")
        
        if choice == '1':
            view_all_items()
        elif choice == '2':
            add_item()
        elif choice == '3':
            add_student()
        elif choice == '4':
            add_volunteer()
        elif choice == '5':
            record_student_visit()
        elif choice == '6':
            add_shift()
        elif choice == '7':
            add_volunteer_hours()
        elif choice == '8':
            print("Exiting. Goodbye!")
            break
        else:
            print("Invalid choice. Please try again.")

if __name__ == "__main__":
    main_menu()
