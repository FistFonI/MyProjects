import sqlite3
import csv

def take_condition(id_user):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT condition FROM users WHERE userid = ?"
        cursor.execute(sql, (id_user,))
        line = cursor.fetchone()
        if line is None:
            return_count = 0
        else:
            return_count = line[0]
    finally:
        connection.close()
    return return_count

def add_new_line(id_user):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "INSERT INTO users (userid, condition) VALUES (?, ?)"
        cursor.execute(sql, (id_user, "1",))
        connection.commit()
    finally:
        connection.close()
    return

def update_condition(id_user, new_condition):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "UPDATE users SET condition = ? WHERE userid = ?"
        cursor.execute(sql, (new_condition, id_user,))
        connection.commit()
    finally:
        connection.close()
    return

def get_connection():
    connection = sqlite3.connect("database/vkbotdb.db")
    exist = exist_tables(connection)
    if exist == 0:
        create_tables(connection)
    return connection

def exist_tables(connection):
    cursor = connection.cursor() 
    cursor.execute("""SELECT count(name) FROM sqlite_master WHERE type='table' AND name='users'""") 
    if cursor.fetchone()[0] == 1 : 
        return 1
    else:
        return 0

def create_tables(connection):
    create_table(connection)
    create_route_table(connection)
    create_subroute_table(connection)
    create_stop_table(connection)

    fill_route_table(connection)
    fill_subroute_table(connection)
    fill_stop_table(connection)

def create_table(connection):
    cursor = connection.cursor()
    cursor.execute("""CREATE TABLE IF NOT EXISTS users(
        userid INT PRIMARY KEY,
        condition INT,
        number TEXT,
        type TEXT,
        routeid INT,
        start TEXT,
        end TEXT);
    """)
    connection.commit()

def create_route_table(connection):
    cursor = connection.cursor()
    cursor.execute("""CREATE TABLE IF NOT EXISTS route(
        routeid INT PRIMARY KEY,
        type TEXT,
        number TEXT);
    """)
    connection.commit()

def create_subroute_table(connection):
    cursor = connection.cursor()
    cursor.execute("""CREATE TABLE IF NOT EXISTS subroute(
        routeid INT,
        direction INT,
        stops TEXT PRIMARY KEY);
    """)
    connection.commit()

def create_stop_table(connection):
    cursor = connection.cursor()
    cursor.execute("""CREATE TABLE IF NOT EXISTS stop(
        stopid INT PRIMARY KEY,
        name TEXT,
        shortname TEXT,
        direction TEXT); 
    """) #direction не нужен.
    connection.commit()

def take_number(id_user):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT number FROM users WHERE userid = ?"
        cursor.execute(sql, (id_user,))
        line = cursor.fetchone()
        number = line[0]
    finally:
        connection.close()
    return number

def update_number(id_user, new_number):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "UPDATE users SET number = ? WHERE userid = ?"
        cursor.execute(sql, (new_number, id_user,))
        connection.commit()
    finally:
        connection.close()
    return

def take_type(id_user):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT type FROM users WHERE userid = ?"
        cursor.execute(sql, (id_user,))
        line = cursor.fetchone()
        rtype = line[0]
    finally:
        connection.close()
    return rtype

def update_type(id_user, new_type):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "UPDATE users SET type = ? WHERE userid = ?"
        cursor.execute(sql, (new_type, id_user,))
        connection.commit()
    finally:
        connection.close()
    return

def take_routeid(id_user):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT routeid FROM users WHERE userid = ?"
        cursor.execute(sql, (id_user,))
        line = cursor.fetchone()
        routeid = line[0]
    finally:
        connection.close()
    return routeid

def update_routeid(id_user, new_routeid):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "UPDATE users SET routeid = ? WHERE userid = ?"
        cursor.execute(sql, (new_routeid, id_user,))
        connection.commit()
    finally:
        connection.close()
    return

def take_start(id_user):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT start FROM users WHERE userid = ?"
        cursor.execute(sql, (id_user,))
        line = cursor.fetchone()
        start = line[0]
    finally:
        connection.close()
    return start

def update_start(id_user, new_start):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "UPDATE users SET start = ? WHERE userid = ?"
        cursor.execute(sql, (new_start, id_user,))
        connection.commit()
    finally:
        connection.close()
    return    

def take_end(id_user):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT end FROM users WHERE userid = ?"
        cursor.execute(sql, (id_user,))
        line = cursor.fetchone()
        end = line[0]
    finally:
        connection.close()
    return end

def update_end(id_user, new_end):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "UPDATE users SET end = ? WHERE userid = ?"
        cursor.execute(sql, (new_end, id_user,))
        connection.commit()
    finally:
        connection.close()
    return

def fill_route_table(connection):
    route_list = []
    with open('data/route.csv') as File:
        bus_reader = csv.reader(File, delimiter = ";")
        for row in bus_reader:
            route_list.append([row[0], row[1], row[2]])

    try:
        cursor = connection.cursor()
        sql = "INSERT INTO route (routeid, type, number) VALUES (?, ?, ?)"
        for route in route_list:
            cursor.executemany(sql, (route,))
        connection.commit()
    except:
        return
    return

def fill_subroute_table(connection):
    subroute_list = []
    with open('data/subroute.csv') as File:
        subroute_reader = csv.reader(File, delimiter = ";")
        for row in subroute_reader:
            subroute_list.append([row[0], row[1], row[2]])
     
    try:
        cursor = connection.cursor()
        sql = "INSERT INTO subroute (routeid, direction, stops) VALUES (?, ?, ?)"
        for subroute in subroute_list: 
            cursor.executemany(sql, (subroute,))
        connection.commit()
    except:
        return
    return

def fill_stop_table(connection):
    stop_list = []
    with open('data/stop.csv') as File:
        stop_reader = csv.reader(File, delimiter = ";")
        for row in stop_reader:
            stop_list.append([row[0], row[1], row[2], row[3]])

    try:
        cursor = connection.cursor()
        sql = "INSERT INTO stop (stopid, name, shortname, direction) VALUES (?, ?, ?, ?)"
        for stop in stop_list: 
            cursor.executemany(sql, (stop,))
        connection.commit()
    except:
        return
    return

def get_routeid_from_route(route_number):
    routeid_list = []
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT routeid FROM route WHERE number = ?"
        cursor.execute(sql, (route_number,))
        line = cursor.fetchall()
        for id in line:
            routeid_list.append(id[0])
    finally:
        connection.close()
    if len(routeid_list) > 1:
        return routeid_list
    else:
        return routeid_list[0]

def get_route_from_routeid(routeid):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT number FROM route WHERE routeid = ?"
        cursor.execute(sql, (routeid,))
        line = cursor.fetchone()
        return_number = line[0]
    finally:
        connection.close()
    return return_number    

def get_stops_from_routeid(route_id):
    stops_list = []
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT stops FROM subroute WHERE routeid = ?"
        cursor.execute(sql, (route_id,))
        line = cursor.fetchall()
        for stops in line:
            stops_list.append(stops[0])
    finally:
        connection.close()
    return stops_list

def get_list_from_stops(stops):
    name_list = []
    id_list = stops.split(',')
    connection = get_connection()
    try:
        cursor = connection.cursor()
        for id in id_list:
            sql = "SELECT name FROM stop WHERE stopid = ?"
            cursor.execute(sql, (id,))
            line = cursor.fetchone()
            name_list.append(line[0])
    finally:
        connection.close()
    return name_list
        
def get_stopsid_from_stop(stop):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT stopid FROM stop WHERE name = ? OR shortname = ?"
        cursor.execute(sql, (stop, stop,))
        lines = cursor.fetchall()
    finally:
        connection.close()
    return lines

def get_stops_from_subroute_table():
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT stops FROM subroute"
        cursor.execute(sql)
        return_stops_list = cursor.fetchall()
    finally:
        connection.close()
    return return_stops_list

def check_start_end(start, end):
    startid_list = get_stopsid_from_stop(start)
    endid_list = get_stopsid_from_stop(end)
    stops_list = get_stops_from_subroute_table()
    subid_list = []
    for stops in stops_list: 
        id_list = stops[0].split(',')
        for startid in startid_list:
            for endid in endid_list:
                if str(startid[0]) in id_list and str(endid[0]) in id_list and id_list.index(str(startid[0])) < id_list.index(str(endid[0])):
                    subid_list.append([startid[0], endid[0]]) 
    if len(subid_list) > 0:
        return subid_list
    else:
        return None                

def check_start_end_for_routes(start, end):
    startid_list = get_stopsid_from_stop(start)
    endid_list = get_stopsid_from_stop(end)
    stops_list = get_stops_from_subroute_table()
    routes_list = []
    for stops in stops_list: 
        id_list = stops[0].split(',')
        for startid in startid_list:
            for endid in endid_list:
                if str(startid[0]) in id_list and str(endid[0]) in id_list and id_list.index(str(startid[0])) < id_list.index(str(endid[0])):
                    routes_list.append(stops[0]) 
    if len(routes_list) > 0:
        return routes_list
    else:
        return None 

def get_routeid_from_stops(stops):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT routeid FROM subroute WHERE stops = ?"
        cursor.execute(sql, (stops,))
        line = cursor.fetchone()
        return_routeid = line[0]
    finally:
        connection.close()
    return return_routeid

def insert(): #для тестирования
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "Select * From route "
        cursor.execute(sql)
        a = cursor.fetchall()
        print(a)
        sql = "INSERT or IGNORE INTO route (routeid, type, number) VALUES (?, ?, ?)"
        cursor.execute(sql, (500, "tram", "7",))
    finally:
        connection.commit()    

def get_type_from_routeid(routeid):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT type FROM route WHERE routeid = ?"
        cursor.execute(sql, (routeid,))
        line = cursor.fetchone()
        return_type = line[0]
    finally:
        connection.close()
    return return_type

def get_routeid_from_number_type(number, route_type):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT routeid FROM route WHERE number = ? AND type = ?"
        cursor.execute(sql, (number, route_type,))
        line = cursor.fetchone()
        return_routeid = line[0]
    finally:
        connection.close()
    return return_routeid

def get_routes():
    routes_list = []
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT number, type FROM route"
        cursor.execute(sql)
        line = cursor.fetchall()
        for route in line:
            routes_list.append([route[0], route[1]])
    finally:
        connection.close()
    return routes_list