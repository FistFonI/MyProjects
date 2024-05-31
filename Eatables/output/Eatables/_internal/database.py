import sqlite3, random

def get_connection():
    connection = sqlite3.connect("eatablesdb.db")
    exist = exist_tables(connection)
    if exist == 0:
        create_tables(connection)
    return connection

def exist_tables(connection):
    cursor = connection.cursor() 
    cursor.execute("""SELECT count(name) FROM sqlite_master WHERE type='table' AND name='answers'""") 
    if cursor.fetchone()[0] == 1 : 
        return 1
    else:
        return 0

def create_tables(connection):
    create_table_questions(connection)
    create_table_answer_question(connection)
    create_table_users(connection)
    create_table_user_questions(connection)
    create_table_answers(connection)
    add_new_quest("съедобное растение")
    add_new_answer("морковка")
    add_new_answ_quest(1, 1, True)

def create_table_answers(connection):
    cursor = connection.cursor()
    cursor.execute("""CREATE TABLE IF NOT EXISTS answers(
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        answer TEXT);
    """)
    connection.commit()

def create_table_questions(connection):
    cursor = connection.cursor()
    cursor.execute("""CREATE TABLE IF NOT EXISTS questions(
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        quest TEXT);
    """)
    connection.commit()

def create_table_answer_question(connection):
    cursor = connection.cursor()
    cursor.execute("""CREATE TABLE IF NOT EXISTS answer_questions(
        answ_id INTEGER NOT NULL,
        quest_id INTEGER NOT NULL,
        value BOOLEAN,
        tag INTEGER PRIMARY KEY AUTOINCREMENT,
        FOREIGN KEY (answ_id) REFERENCES answers(id)
        FOREIGN KEY (quest_id) REFERENCES questions(id));
    """)
    connection.commit()

def create_table_users(connection):
    cursor = connection.cursor()
    cursor.execute("""CREATE TABLE IF NOT EXISTS users(
        id INTEGER PRIMARY KEY,
        condition INTEGER,
        quest_id INTEGER,
        new_answ_id INTEGER,
        new_quest_id INTEGER);
    """)
    connection.commit()

def create_table_user_questions(connection):
    cursor = connection.cursor()
    cursor.execute("""CREATE TABLE IF NOT EXISTS user_questions(
        user_id INTEGER NOT NULL,
        quest_id INTEGER NOT NULL,
        value BOOLEAN,
        tag INTEGER PRIMARY KEY AUTOINCREMENT,
        FOREIGN KEY (user_id) REFERENCES answers(id)
        FOREIGN KEY (quest_id) REFERENCES questions(id));
    """)
    connection.commit()

##################################################################

def add_new_answer(answ):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "INSERT INTO answers (id, answer) VALUES (NULL, ?)"
        cursor.execute(sql, (answ,))
        connection.commit()
    finally:
        connection.close()
    return

def add_new_quest(qst):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "INSERT INTO questions (id, quest) VALUES (NULL, ?)"
        cursor.execute(sql, (qst,))
        connection.commit()
    finally:
        connection.close()
    return

def add_new_answ_quest(answ_id, quest_id, value):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "INSERT INTO answer_questions (answ_id, quest_id, value, tag) VALUES (?, ?, ?, NULL)"
        cursor.execute(sql, (answ_id, quest_id, value,))
        connection.commit()
    finally:
        connection.close()
    return

def add_new_user(userid):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "INSERT INTO users (id, condition) SELECT ?, ? WHERE NOT EXISTS (SELECT 1 FROM users WHERE id = ?)"
        cursor.execute(sql, (userid, 0, userid))
        connection.commit()
    finally:
        connection.close()
    return

def add_new_user_quest(user_id, quest_id, value):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "INSERT INTO user_questions (user_id, quest_id, value, tag) VALUES (?, ?, ?, NULL)"
        cursor.execute(sql, (user_id, quest_id, value,))
        connection.commit()
    finally:
        connection.close()
    return

def add_new_answ_quests(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT * FROM user_questions WHERE user_id = (?)"
        cursor.execute(sql, (user_id,))
        lines = cursor.fetchall()

        sql1 = "SELECT new_answ_id FROM users WHERE id = ?"
        cursor.execute(sql1, (user_id,))
        line1 = cursor.fetchone()
        answ_id = line1[0]

        for line in lines:
            sql2 = "INSERT INTO answer_questions (answ_id, quest_id, value, tag) VALUES (?, ?, ?, NULL)"
            cursor.execute(sql2, (answ_id, line[1], line[2],))
        connection.commit()
    finally:
        connection.close()
    return

##################################################################

def delete_user_quests(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "DELETE FROM user_questions WHERE user_id = ?"
        cursor.execute(sql, (user_id,))
        connection.commit()
    finally:
        connection.close()
    return

def update_user_condition(user_id, condition):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql2 = "UPDATE users SET condition = ? WHERE id = ?"
        cursor.execute(sql2, (condition, user_id,))
        connection.commit()
    finally:
        connection.close()
    return

def update_user_quest(user_id, quest):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT id FROM questions WHERE quest = ?"
        cursor.execute(sql, (quest,))
        line = cursor.fetchone()
        quest_id = line[0]
        sql2 = "UPDATE users SET quest_id = ? WHERE id = ?"
        cursor.execute(sql2, (quest_id, user_id,))
        connection.commit()
    finally:
        connection.close()
    return

def add_user_new_answer(user_id, answer):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT id FROM answers WHERE answer = ?"
        cursor.execute(sql, (answer,))
        line = cursor.fetchone()
        answ_id = line[0]
        sql2 = "UPDATE users SET new_answ_id = ? WHERE id = ?"
        cursor.execute(sql2, (answ_id, user_id,))
        connection.commit()
    finally:
        connection.close()
    return

def add_user_new_quest(user_id, quest):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT id FROM questions WHERE quest = ?"
        cursor.execute(sql, (quest,))
        line = cursor.fetchone()
        quest_id = line[0]
        sql2 = "UPDATE users SET new_quest_id = ? WHERE id = ?"
        cursor.execute(sql2, (quest_id, user_id,))
        connection.commit()
    finally:
        connection.close()
    return

##################################################################

def get_first_quest():
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        sql = "SELECT quest FROM questions WHERE id = ?"
        cursor.execute(sql, (1,))
        line = cursor.fetchone()
        return_quest = line[0]
    finally:
        connection.close()
    return return_quest 

def get_user_condition(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        sql = "SELECT condition FROM users WHERE id = ?"
        cursor.execute(sql, (user_id,))
        line = cursor.fetchone()
        condition = line[0]
    finally:
        connection.close()
    return condition 

def get_quest_id_from_user_id(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT quest_id FROM users WHERE id = ?"
        cursor.execute(sql, (user_id,))
        line = cursor.fetchone()
        quest_id = line[0]
    finally:
        connection.close()
    return quest_id 

def get_answ_from_user_id(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT quest_id FROM users WHERE id = ?"
        cursor.execute(sql, (user_id,))
        line = cursor.fetchone()
        answ = line[0]
    finally:
        connection.close()
    return answ

def get_next_quest(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT quest_id FROM users WHERE id = ?"
        cursor.execute(sql, (user_id,))
        line = cursor.fetchone()
        quest_id = line[0]
        sql2 = "SELECT quest FROM questions WHERE id = ?"
        cursor.execute(sql2, (quest_id + 1,))
        line2 = cursor.fetchone()
        return_quest = line2[0]
    finally:
        connection.close()
    return return_quest 

def get_new_answ_id(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT new_answ_id FROM users WHERE id = ?"
        cursor.execute(sql, (user_id,))
        line = cursor.fetchone()
        new_answ_id = line[0]

        sql1 = "SELECT id FROM answers WHERE id = ?"
        cursor.execute(sql1, (new_answ_id,))
        line1 = cursor.fetchone()
        answ_id = line1[0]
    finally:
        connection.close()
    return answ_id 

def get_new_quest_id(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT new_quest_id FROM users WHERE id = ?"
        cursor.execute(sql, (user_id,))
        line = cursor.fetchone()
        new_quest_id = line[0]

        sql1 = "SELECT id FROM questions WHERE id = ?"
        cursor.execute(sql1, (new_quest_id,))
        line1 = cursor.fetchone()
        quest_id = line1[0]
    finally:
        connection.close()
    return quest_id 

def get_quest_from_quest_id(quest_id):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT quest FROM questions WHERE id = ?"
        cursor.execute(sql, (quest_id,))
        line = cursor.fetchone()
        quest = line[0]
    finally:
        connection.close()
    return quest

def get_quest_id_from_quest(quest):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT id FROM questions WHERE quest = ?"
        cursor.execute(sql, (quest,))
        line = cursor.fetchone()
        id = line[0]
    finally:
        connection.close()
    return id

##################################################################

def check_answ(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql1 = "SELECT COUNT(*) FROM user_questions WHERE user_id = (?)"
        cursor.execute(sql1, (user_id,))
        line1 = cursor.fetchone()
        count = line1[0]

        print(line1)
        sql2 = "SELECT answ_id FROM answer_questions GROUP BY answ_id HAVING COUNT(answ_id) = (?)"
        cursor.execute(sql2, (count,))
        line2 = cursor.fetchone()
        answ_id = line2[0]

        sql3 = "SELECT answer FROM answers WHERE id = (?)"
        cursor.execute(sql3, (answ_id,))
        line3 = cursor.fetchone()
        answ = line3[0]

        
        print(line2)
        print(line3)
    finally:
        connection.close()
    return answ

def is_end(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        cursor.execute("""SELECT count(quest) FROM questions""")
        line = cursor.fetchone()
        count = line[0]
        sql = """SELECT quest_id FROM users WHERE id = (?)"""
        cursor.execute(sql, (user_id,))
        line1 = cursor.fetchone()
        quest_id = line1[0]
        end = quest_id >= count
    finally:
        connection.close()
    return end

def send_why_such_answer(user_id):
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        sql = ("""SELECT * FROM user_questions where user_id = ?""")
        cursor.execute(sql, (user_id,))
        lines = cursor.fetchall()
        txt = ""
        for line in lines:
            sql1 = ("""SELECT quest FROM questions where id = ?""")
            cursor.execute(sql1, (line[1],))
            line1 = cursor.fetchone()
            value = line[2]
            if (value):
                txt += "{}. ".format(line1[0])
            else:
                txt += "Не {}. ".format(line1[0])
    finally:
        connection.close()
    return txt

def all_database():
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        cursor.execute("""SELECT answ_id FROM answer_questions GROUP BY answ_id HAVING COUNT(*) = (SELECT MAX(cnt) FROM (SELECT COUNT(*) AS cnt FROM answer_questions GROUP BY answ_id) AS counts)""")
        line_id = cursor.fetchone()
        sql = "SELECT * FROM answer_questions WHERE answ_id = ?"
        cursor.execute(sql, (line_id[0],))
        lines = cursor.fetchall()
        print(lines)
        txt = ""
        for line in lines:
            quest = get_quest_from_quest_id(line[1])

            sql1 = "SELECT MIN(tag) FROM answer_questions WHERE quest_id = ?"
            cursor.execute(sql1, (line[1],))
            line1 = cursor.fetchone()
            sql2 = "SELECT answ_id FROM answer_questions WHERE quest_id = ? AND tag = ?"
            cursor.execute(sql2, (line[1], line1[0], ))
            line2 = cursor.fetchone()
            answer = get_answ_from_answ_id(line2[0])

            txt += """{} -> Да -> {}\n""".format(quest, answer)
            lenght = len(quest) + 7
            
            for y in range(1, lenght):
                txt += " "
            txt += "|\n"

            for y in range(1, lenght):
                txt += " "
            txt += "v\n"

            for y in range(1, lenght):
                txt += " "
            txt += "Нет\n"

            for y in range(1, lenght):
                txt += " "
            txt += "|\n"

            for y in range(1, lenght):
                txt += " "
            txt += "v\n"
    finally:
        connection.close()
    return txt

##################################################################

def get_answ_id_from_answ(answ):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT id FROM answers WHERE answer = ?"
        cursor.execute(sql, (answ,))
        line = cursor.fetchone()
        id = line[0]
    finally:
        connection.close()
    return id  

def get_answ_from_answ_id(answ_id):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT answer FROM answers WHERE id = ?"
        cursor.execute(sql, (answ_id,))
        line = cursor.fetchone()
        answer = line[0]
        
    finally:
        connection.close()
    return answer  

def get_answ_id_from_quest(quest):
    connection = get_connection()
    try:
        cursor = connection.cursor()
        sql = "SELECT answer_id FROM questions WHERE quest = ?"
        cursor.execute(sql, (quest,))
        line = cursor.fetchone()
        answ_id = line[0]
    finally:
        connection.close()
    return answ_id  

def all_quest():
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        cursor.execute("""SELECT * FROM questions""")
        lines = cursor.fetchall()
        print("\nВопросы:")
        for line in lines:
            print(line)
    finally:
        connection.close()

def all_answer():
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        cursor.execute("""SELECT * FROM answers""")
        lines = cursor.fetchall()
        print("\nОтветы:")
        for line in lines:
            print(line)
    finally:
        connection.close()


def all_users():
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        cursor.execute("""SELECT * FROM users""")
        lines = cursor.fetchall()
        print("\nПользователи:")
        for line in lines:
            print(line)
    finally:
        connection.close()

def all_user_questions():
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        cursor.execute("""SELECT * FROM user_questions""")
        lines = cursor.fetchall()
        print("\nПользователь - Вопрос:")
        for line in lines:
            print(line)
    finally:
        connection.close()

def all_answer_questions():
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        cursor.execute("""SELECT * FROM answer_questions""")
        lines = cursor.fetchall()
        print("\nОтвет - Вопрос:")
        for line in lines:
            print(line)
    finally:
        connection.close()



def check_paste(answ):
    connection = get_connection()
    try:
        cursor = connection.cursor() 
        cursor.execute("""SELECT answer FROM answers""")
        lines = cursor.fetchall()
        for line in lines:
            if (line[0] == answ):
                return False
    finally:
        connection.close()
    return True
