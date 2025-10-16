from flask import Flask, render_template, request, redirect, url_for
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
        exp_date = request.form['exp_date']
        barcode = request.form['barcode'] or None

        db_connection = get_db_connection()

        if db_connection:
            cursor = db_connection.cursor()
            cursor.execute ("INSERT INTO Items (Name, Category, Quantity, ExpDate, Barcode)" \
            " VALUES (%s, %s, %s, %s, %s)", (name, category, quantity, exp_date, barcode))

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
        exp_date = request.form['exp_date']

        cursor.execute ("UPDATE Items SET Name=%s, Category=%s, Quantity=%s, ExpDate=%s WHERE ItemId=%s",
                        (name, category, quantity, exp_date, item_id))
        

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
        firstname = request.form['firstname']
        lastname = request.form['lastname']
        email = request.form['email']
        studentID = request.form['studentID']
        major = request.form['major']
        username = request.form['username']
        password = request.form['password']

        db_connection = get_db_connection()

        if db_connection:
            cursor = db_connection.cursor()
            cursor.execute ("INSERT INTO Students (CougarId, Name, Email, Major, Username, PasswordHash)" \
            " VALUES (%s, %s, %s, %s, %s, %s)", (studentID, firstname + ' ' + lastname, email, major, username, password))

            db_connection.commit()
            cursor.close()
            db_connection.close()

        return redirect(url_for('home'))
   
    return render_template('register.html')

