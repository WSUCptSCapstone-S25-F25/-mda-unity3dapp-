from flask import Flask, render_template, request, redirect, url_for, flash
import mysql.connector

app = Flask(__name__)


db_config = {
    'user': 'root',
    'password': '11',
    'host': 'localhost',
    'database': 'food_db'

}

def get_db_connection():
    try:
        return mysql.connector.connect(**db_config)
    except mysql.connector.Error as err:
        print(f"DB error: {err}")
        return None
    
# route for home page 
@app.route('/')
@app.route('/home')
def home():
    return render_template('home.html')

# route for inventory
@app.route('/inventory')
def inventory():
    db_connection = get_db_connection()
    items = []

    if db_connection:
        cursor = db_connection.cursor(dictionary = True)
        cursor.execute ("SELECT * FROM Items")
        items = cursor.fetchall()
        cursor.close()
        db_connection.close()

    return render_template('inventory.html', items = items)

# route to add an item
@app.route('/add_item', methods = ['GET', 'POST'])
def add_item():
    if request.method == 'POST':
        name = request.form['name']
        category = request.form['category']
        quantity = request.form['quantity']
        weight = request.form['weight']
        exp_date = request.form['exp_date']
        barcode = request.form['barcode'] or None

        db_connection = get_db_connection()

        if db_connection:
            cursor = db_connection.cursor()
            cursor.execute ("INSERT INTO Items (Name, Category, Quantity, Weight, ExpDate, UPC, Comment)" \
            " VALUES (%s, %s, %s, %s, %s, %s, %s)", (name, category, quantity, weight, exp_date, barcode, " "))

            db_connection.commit()
            cursor.close()
            db_connection.close()

        return redirect(url_for('inventory'))

    return render_template('add_item.html')

# route to edit item
@app.route('/edit_item/<int:item_id>', methods = ['GET', 'POST'])
def edit_item(item_id):
    db_connection = get_db_connection()
    cursor = db_connection.cursor(dictionary=True)

    if request.method == 'POST':
        name = request.form['name']
        category = request.form['category']
        quantity = request.form['quantity']
        weight = request.form['weight']
        exp_date = request.form['exp_date']
        comment = request.form['comment']

        cursor.execute ("UPDATE Items SET Name=%s, Category=%s, Quantity=%s, Weight=%s, ExpDate=%s, Comment=%s WHERE ItemId=%s",
                        (name, category, quantity, weight, exp_date, comment, item_id))
        

        db_connection.commit()
        cursor.close()
        db_connection.close()
        return redirect(url_for('inventory'))
    
    cursor.execute("SELECT * FROM Items WHERE ItemId=%s", (item_id,))
    item = cursor.fetchone()
    cursor.close()
    db_connection.close()

    if item and item['ExpDate']:
        item["ExpDate"] = item["ExpDate"].strftime('%Y-%m-%d')

    return render_template('edit_item.html', item=item)



# route to delete an item
@app.route('/delete_item/<int:item_id>', methods = ['GET', 'POST'])
def delete_item(item_id):

    db_connection = get_db_connection()
    cursor = db_connection.cursor()

    cursor.execute ("DELETE FROM Items WHERE ItemId = %s", (item_id,))
    db_connection.commit()
    cursor.close()
    db_connection.close()

    return redirect(url_for('inventory'))

# route to register
@app.route('/register', methods = ['GET', 'POST'])
def register():
    if request.method == 'POST':
        name = request.form['name']
        email = request.form['email']
        studentID = request.form['studentID']
        major = request.form['major']
        username = request.form['username']
        password = request.form['password']

        db_connection = get_db_connection()

        if db_connection:
            cursor = db_connection.cursor()
            cursor.execute ("INSERT INTO Students (CougarId, Name, Email, Major, Username, PasswordHash)" \
            " VALUES (%s, %s, %s, %s, %s, %s)", (studentID, name, email, major, username, password))

            db_connection.commit()
            cursor.close()
            db_connection.close()
            
            return redirect(url_for('students'))
   
    return render_template('register.html')


# route to login
# @app.route('/', methods = ['GET', 'POST'])
@app.route('/login', methods = ['GET', 'POST'])
def login():

    if request.method == 'POST':
        username = request.form['username']
        password = request.form['password']
  
        db_connection = get_db_connection()

        if db_connection:
            cursor = db_connection.cursor(dictionary=True)
            cursor.execute ("SELECT * FROM Students WHERE Username=%s AND PasswordHash=%s",
                            (username, password))
            
            student = cursor.fetchone()

            if student:
                return redirect(url_for('home'))
            

            cursor.close()
            db_connection.close()
           
    return render_template('login.html')

# route to view students
@app.route('/students')
def students():
    db_connection = get_db_connection()
    students = []

    if db_connection:
        cursor = db_connection.cursor(dictionary = True)
        cursor.execute ("SELECT * FROM Students")
        students = cursor.fetchall()
        cursor.close()
        db_connection.close()

    return render_template('students.html', students = students)

# route to edit student info
@app.route('/edit_student/<int:student_id>', methods = ['GET', 'POST'])
def edit_student(student_id):
    db_connection = get_db_connection()
    cursor = db_connection.cursor(dictionary=True)

    if request.method == 'POST':
        name = request.form['name']
        email = request.form['email']
        studentID = request.form['studentID']
        major = request.form['major']
        username = request.form['username']
        password = request.form['password']

        cursor.execute ("UPDATE Students SET CougarId=%s, Name=%s, Email=%s, Major=%s, Username=%s, PasswordHash=%s WHERE StudentId=%s",
                        (studentID, name, email, major, username, password, student_id))
        

        db_connection.commit()
        cursor.close()
        db_connection.close()
        return redirect(url_for('students'))
    
    cursor.execute("SELECT * FROM Students WHERE StudentId=%s", (student_id,))
    student = cursor.fetchone()
    cursor.close()
    db_connection.close()

    return render_template('edit_student.html', student=student)

# route to delete an student
@app.route('/delete_student/<int:student_id>', methods = ['GET'])
def delete_student(student_id):

    db_connection = get_db_connection()
    cursor = db_connection.cursor()

    cursor.execute ("DELETE FROM Students WHERE StudentId = %s", (student_id,))
    db_connection.commit()
    cursor.close()
    db_connection.close()

    return redirect(url_for('students'))


# route to view admins
@app.route('/admins')
def admins():
    db_connection = get_db_connection()
    admins = []

    if db_connection:
        cursor = db_connection.cursor(dictionary = True)
        cursor.execute ("SELECT * FROM Admins")
        admins = cursor.fetchall()
        cursor.close()
        db_connection.close()

    return render_template('admins.html', admins = admins)

# route to register
@app.route('/register_admin', methods = ['GET', 'POST'])
def register_admin():
    if request.method == 'POST':
        name = request.form['name']
        email = request.form['email']
        phone = request.form['phone']
        username = request.form['username']
        password = request.form['password']

        db_connection = get_db_connection()

        if db_connection:
            cursor = db_connection.cursor()
            cursor.execute ("INSERT INTO Admins (Name, Email, Phone, Username, PasswordHash)" \
            " VALUES (%s, %s, %s, %s, %s)", (name, email, phone, username, password))

            db_connection.commit()
            cursor.close()
            db_connection.close()
            
            return redirect(url_for('admins'))
   
    return render_template('register_admin.html')

# route to edit admin info
@app.route('/edit_admin/<int:admin_id>', methods = ['GET', 'POST'])
def edit_admin(admin_id):
    db_connection = get_db_connection()
    cursor = db_connection.cursor(dictionary=True)

    if request.method == 'POST':
        name = request.form['name']
        email = request.form['email']
        phone = request.form['phone']
        username = request.form['username']
        password = request.form['password']

        cursor.execute ("UPDATE Admins SET Name=%s, Email=%s, Phone=%s, Username=%s, PasswordHash=%s WHERE AdminId=%s",
                        (name, email, phone, username, password, admin_id))
        

        db_connection.commit()
        cursor.close()
        db_connection.close()
        return redirect(url_for('admins'))
    
    cursor.execute("SELECT * FROM Admins WHERE AdminId=%s", (admin_id,))
    admin = cursor.fetchone()
    cursor.close()
    db_connection.close()

    return render_template('edit_admin.html', admin=admin)

# route to delete an admin
@app.route('/delete_admin/<int:admin_id>', methods = ['GET'])
def delete_admin(admin_id):

    db_connection = get_db_connection()
    cursor = db_connection.cursor()

    cursor.execute ("DELETE FROM admins WHERE AdminId = %s", (admin_id,))
    db_connection.commit()
    cursor.close()
    db_connection.close()

    return redirect(url_for('admins'))
