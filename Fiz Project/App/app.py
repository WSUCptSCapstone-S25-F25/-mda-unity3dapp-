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
        barcode = request.form['barcode']

        db_connection = get_db_connection()

        if db_connection:
            cursor = db_connection.cursor()
            cursor.execute ("INSERT INTO Items (Name, Category, Quantity, Exp_date, Barcode)" \
            " VALUES (%s, %s, %s, %s, %s)", (name, category, quantity, exp_date, barcode))

            db_connection.commit()
            cursor.close()
            db_connection.close()

        return redirect(url_for('inventory'))

    return render_template('add_item.html')





