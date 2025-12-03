from flask import Flask, render_template, request, redirect, url_for, flash, session
import mysql.connector
import secrets

app = Flask(__name__)
app.secret_key = secrets.token_hex(16)


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
    user = session.get('user', {'type': 'guest'})

    if user.get('type') == 'student':
        db_connection = get_db_connection()
        cursor = db_connection.cursor(dictionary = True)

        cursor.execute("SELECT * FROM Volunteers WHERE StudentId=%s", (user['id'], ))
        volunteer = cursor.fetchone()
        cursor.close()
        db_connection.close()

        if volunteer:
            user['volunteer_status'] = volunteer['Approved']
        else:
            None

    return render_template('home.html', user=user)

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
        user = session.get('user', {'type': 'guest'})

    return render_template('inventory.html', items = items, user = user)

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

            cursor.execute ("SELECT * FROM Admins WHERE Username=%s",
                            (username, ))
            
            admin = cursor.fetchone()

            if admin and admin['PasswordHash'] == password:
                session['user'] = {'id': admin['AdminId'], 'username': admin['Username'], 'name': admin['Name'], 'type': 'admin'}

                return redirect(url_for('home'))

            cursor.execute ("SELECT * FROM Students WHERE Username=%s",
                            (username, ))
            
            student = cursor.fetchone()
            if student and student['PasswordHash'] == password:

                cursor.execute ("SELECT * FROM Volunteers WHERE StudentId=%s",
                            (student['StudentId'], ))
                volunteer = cursor.fetchone()
                
                if volunteer:
                    session['user'] = {'id': student['StudentId'],  'name': student['Name'], 'username': student['Username'], 'type': 'volunteer'}

                else:
                    session['user'] = {'id': student['StudentId'], 'name': student['Name'], 'username': student['Username'], 'type': 'student'}

                return redirect(url_for('home'))

            cursor.close()
            db_connection.close()
           
    return render_template('login.html')

# route to logout
@app.route('/logout')
def logout():
    session.clear()
    return redirect(url_for('home'))

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

# route to view shifts
@app.route('/shifts')
def shifts():
    db_connection = get_db_connection()
    shifts = []

    if db_connection:
        cursor = db_connection.cursor(dictionary = True)
        cursor.execute ("SELECT * FROM Shifts")
        shifts = cursor.fetchall()
        
        # Format date and time objects for display in the template
        for shift in shifts:
            if shift['ShiftDate']:
                shift['ShiftDate'] = shift['ShiftDate'].strftime('%Y-%m-%d')

            if shift['StartTime']:
                s = str(shift['StartTime'])  
                s = s.split('.')[0]          
                if 'day' in s:               
                    s = s.split(',')[-1].strip()
                shift['StartTime'] = s[:5]   

            if shift['EndTime']:
                e = str(shift['EndTime'])
                e = e.split('.')[0]
                if 'day' in e:
                    e = e.split(',')[-1].strip()
                shift['EndTime'] = e[:5]

        cursor.close()
        db_connection.close()

    return render_template('shifts.html', shifts = shifts)

# route to add a shift
@app.route('/add_shift', methods = ['GET', 'POST'])
def add_shift():
    if request.method == 'POST':
        shift_date = request.form['shift_date']
        start_time = request.form['start_time']
        end_time = request.form['end_time']

        db_connection = get_db_connection()

        if db_connection:
            cursor = db_connection.cursor()
            cursor.execute ("INSERT INTO Shifts (ShiftDate, StartTime, EndTime)" \
            " VALUES (%s, %s, %s)", (shift_date, start_time, end_time))

            db_connection.commit()
            cursor.close()
            db_connection.close()

        return redirect(url_for('shifts'))

    return render_template('add_shift.html')

# route to edit shift
@app.route('/edit_shift/<int:shift_id>', methods = ['GET', 'POST'])
def edit_shift(shift_id):
    db_connection = get_db_connection()
    cursor = db_connection.cursor(dictionary=True)

    if request.method == 'POST':
        shift_date = request.form['shift_date']
        start_time = request.form['start_time']
        end_time = request.form['end_time']

        cursor.execute ("UPDATE Shifts SET ShiftDate=%s, StartTime=%s, EndTime=%s WHERE ShiftId=%s",
                        (shift_date, start_time, end_time, shift_id))
        
        db_connection.commit()
        cursor.close()
        db_connection.close()
        return redirect(url_for('shifts'))
    
    # GET request: fetch the shift data
    cursor.execute("SELECT * FROM Shifts WHERE ShiftId=%s", (shift_id,))
    shift = cursor.fetchone()
    
    # Format date and time objects for the form fields
    if shift:
        if shift['ShiftDate']:
            shift['ShiftDate'] = shift['ShiftDate'].strftime('%Y-%m-%d')
        if shift['StartTime']:
            # Convert timedelta or time object to HH:MM string
            shift['StartTime'] = str(shift['StartTime']).split('.')[0]
            if len(shift['StartTime']) > 5: # Handle full timedelta string like '1 day, 9:00:00'
                 shift['StartTime'] = shift['StartTime'][-8:-3] # Get '09:00'
            else: # Handle 'HH:MM:SS'
                 shift['StartTime'] = shift['StartTime'][:5]
                 
        if shift['EndTime']:
            shift['EndTime'] = str(shift['EndTime']).split('.')[0]
            if len(shift['EndTime']) > 5:
                 shift['EndTime'] = shift['EndTime'][-8:-3]
            else:
                 shift['EndTime'] = shift['EndTime'][:5]

    cursor.close()
    db_connection.close()

    return render_template('edit_shift.html', shift=shift)

# route to delete a shift
@app.route('/delete_shift/<int:shift_id>', methods = ['GET'])
def delete_shift(shift_id):

    db_connection = get_db_connection()
    cursor = db_connection.cursor()

    cursor.execute ("DELETE FROM Shifts WHERE ShiftId = %s", (shift_id,))
    db_connection.commit()
    cursor.close()
    db_connection.close()

    return redirect(url_for('shifts'))

# route to view volunteers
@app.route('/volunteers')
def volunteers():
    db_connection = get_db_connection()
    volunteers = []

    if db_connection:
        cursor = db_connection.cursor(dictionary = True)
        cursor.execute ("SELECT volunteer.VolunteerId, volunteer.Approved, volunteer.Statement, volunteer.PreferredDays," \
        "volunteer.AppliedTime, student.CougarId, student.StudentId, student.Name, student.Major, student.Email, student.Username," \
        "student.PasswordHash FROM Volunteers volunteer JOIN Students student ON volunteer.StudentId = student.StudentId")
        volunteers = cursor.fetchall()
        cursor.close()
        db_connection.close()

    return render_template('volunteers.html', volunteers = volunteers)

# route to view volunteers applications pending
@app.route('/view_volunteers_app')
def view_volunteers_app():
    db_connection = get_db_connection()
    volunteers = []

    if db_connection:
        cursor = db_connection.cursor(dictionary = True)
        cursor.execute ("SELECT volunteer.VolunteerId, volunteer.Approved, volunteer.Statement, volunteer.PreferredDays," \
        "volunteer.AppliedTime, student.CougarId, student.StudentId, student.Name, student.Major, student.Email, student.Username," \
        "student.PasswordHash FROM Volunteers volunteer JOIN Students student ON volunteer.StudentId = student.StudentId WHERE volunteer.Approved = 'Pending'")
        volunteers = cursor.fetchall()
        cursor.close()
        db_connection.close()

    return render_template('view_volunteers_app.html', volunteers = volunteers)

# route to apply as volunteer
@app.route('/volunteer_app', methods=['GET', 'POST'])
def volunteer_app():
    user = session.get('user')

    if user['type'] != 'student':
        return redirect(url_for('home'))
    
    db_connection = get_db_connection()
    cursor = db_connection.cursor(dictionary=True)

    cursor.execute("SELECT * FROM Volunteers WHERE StudentId=%s", (user['id'], ))
    volunteer = cursor.fetchone()

    if volunteer:
        cursor.close()
        db_connection.close()
        return redirect(url_for('home'))

    if request.method == 'POST':
        statement = request.form['statement']
        days = request.form.getlist('preferred_days')
        preferred_days = ",".join(days)

        cursor.execute("INSERT INTO Volunteers (StudentId, Statement, PreferredDays) VALUES (%s, %s, %s)", (user['id'], statement, preferred_days))

        db_connection.commit()
        cursor.close()
        db_connection.close()

        return redirect(url_for('home'))

    cursor.close()
    db_connection.close()
    return render_template("volunteer_app.html", user = user)
