import requests
import vk_api
from vk_api.upload import VkUpload
import csv
import database.db_functions
import functions
from fuzzywuzzy import fuzz
from fuzzywuzzy import process
import base64
import threading
from vk_api.longpoll import VkLongPoll, VkEventType

vk_session = vk_api.VkApi(token='')
longpoll = VkLongPoll(vk_session, 25)
vk = vk_session.get_api()
upload = VkUpload(vk)

stopname_list = []
shortstopname_list = []
with open('data/stop.csv') as File:
        stop_reader = csv.reader(File, delimiter = ";")
        for row in stop_reader:
                stopname_list.append(row[1]) 
                shortstopname_list.append(row[2]) 

busname_list = []
with open('data/route.csv') as File:
        bus_reader = csv.reader(File, delimiter = ";")
        for row in bus_reader:
                busname_list.append(row[2])

option_list = ["Меню","Время","Маршрут","Остановка","Все маршруты","Список","Расписание","Назад","Транспорт"]

def check_match (text, list):
        correctText = process.extract(text,list)
        correctWord = correctText[0]
        for word in correctText:
                if word[1] > correctWord[1]: correctWord = word
        if correctWord[1] >= 80:
                return correctWord[0]
        else:
                return "Error"

def send_messageWithKeyboard(user_id, id_keyboard, id_random, sendingMessage):
        vk.messages.send( 
                user_id=user_id,
                random_id=id_random,
                keyboard=open(id_keyboard, 'r', encoding='UTF-8').read(),
                message=sendingMessage
        )

def send_message(user_id, id_random, sendingMessage):
        vk.messages.send( 
                user_id=user_id,
                random_id=id_random,
                message=sendingMessage
        )

def upload_photo(photo):
        response = upload.photo_messages(photo)[0]
        owner_id = response['owner_id']
        photo_id = response['id']
        access_key = response['access_key']
        return owner_id, photo_id, access_key

def send_photo(peer_id, id_random, owner_id, photo_id, access_key):
        attachment = f'photo{owner_id}_{photo_id}_{access_key}'
        vk.messages.send(
                random_id=id_random,
                peer_id=peer_id,
                attachment=attachment
        )

def get_schedule_from_routeid(routeid): #добавить сохранение расписание для каждого маршрута отдельно, так как фиг знает как будет работать при большой нагрузке один файл
        response = requests.get(''.format(route=routeid))
        if response.status_code == 200:
                json_data = response.json()
                data = json_data.get('data')
                image_64 = data.get('image')
                decoded_image_data = base64.b64decode(image_64)       
                with open("schedule.jpg", "wb") as file:
                        file.write(decoded_image_data)
                #functions.start_decode_thread(image_64)
                return ""
        elif response.status_code == 422:
                return "На сегодня не запланирован больше ни один рейс или ТС не найдено."
        else:
                return ""  
         
def get_time_from_stops(start, end):
                response = requests.get('https://testapi.igis-transport.ru/vk-2FYffVHod55q89Br/prediction-stop/{stop_start}-{stop_finish}'.format(stop_start=start, stop_finish=end))
                json_data = response.json()
                data = json_data.get('data')
                routeid = list(data.keys())[0]
                routedata = data.get(routeid)
                if routedata.get('online') == True:
                        if routedata.get('is_work_today') == True:
                                if routedata.get('status') == 'ok':
                                        seconds = routedata.get('seconds')
                                        min = (seconds // 60) + 1
                                        route = database.db_functions.get_route_from_routeid(routeid)
                                        text = "Ближайший транспорт - маршрут №{route}.\n Прибывает через - {min} мин.".format(route=route,min=min)
                                else:
                                        text = "По выбранному набору остановок на текущий момент нету ближайших транспортов."
                        else:
                                text = "По выбранному набору остановок транспорт в выходные не ходит."
                else:
                        text = "По выбранному набору остановок информации о прибытии транспорта нету."                                        
                return text

def handle_message(stopname_list, shortstopname_list, busname_list, option_list, event):
    number_condition = database.db_functions.take_condition(event.user_id)
        #database.db_functions.insert() #для тестирования
    if number_condition == 0:
            message = """Привет, я ИГИС-бот. Бот, который поможет тебе добраться до нужного места.
                Для дальнейшего использования введи одну из следующих команд: \"Маршрут\" или \"Остановка\"."""
            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json", event.random_id, message)
            database.db_functions.add_new_line(event.user_id)

    elif number_condition == 1:
            correctOption = check_match(event.text, option_list)
            if correctOption == "Маршрут":
                    database.db_functions.update_condition(event.user_id, "20")
                    message = """Напиши номер маршрута, который тебе нужен, или команду \"Все маршруты\", чтобы узнать существующие маршруты."""
                    send_messageWithKeyboard(event.user_id,"keyboards/keyboard_allRoutes.json",event.random_id, message)
            elif correctOption == "Остановка":
                    database.db_functions.update_condition(event.user_id, "30")
                    message = """Напиши название стартовой остановки пути."""
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_menu.json", event.random_id, message)
            else:
                    message = """Я не понял, что ты написал. Попробуй написать снова, одну из следующих команд:
                        \"Маршрут\" или \"Остановка\"."""
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json", event.random_id, message)

    elif number_condition == 20:
            correctOption = check_match(event.text, option_list)
            if correctOption == "Все маршруты":
                    routes_list = database.db_functions.get_routes()
                    message = """На данный момент, я поддерживаю следующие маршруты:"""
                    for route in routes_list:
                        route_name = functions.get_route_name_by_num_type(route[0], route[1])
                        message += """\n- {}""".format(route_name)
                    message += """\nТеперь ты знаешь все маршруты, и можешь выбрать, который тебе нужен."""
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_menu.json", event.random_id, message)
            else:
                    correctWord = check_match(event.text, busname_list)
                    if correctWord != "Error":
                            database.db_functions.update_number(event.user_id, correctWord)
                            routeids = database.db_functions.get_routeid_from_route(correctWord)
                            if type(routeids) == list:
                                    database.db_functions.update_condition(event.user_id, "22")
                                    message = """Уточните вид транспорта выбранного маршрута №{route}. \nВам нужен: """.format(route=correctWord)
                                    cond = 0
                                    for routeid in routeids:
                                            route_type = database.db_functions.get_type_from_routeid(routeid)
                                            if route_type == "bus":
                                                    cond += 100
                                                    message += "\nАвтобус"
                                            elif route_type == "trolleybus":
                                                    cond += 10     
                                                    message += "\nТроллейбус"
                                            elif route_type == "tram":
                                                    cond += 1
                                                    message += "\nТрамвай"      
                                    match cond:
                                        case 11:
                                                send_messageWithKeyboard(event.user_id, "keyboards/keyboard_TramAndTrol.json", event.random_id, message)   
                                        case 101:
                                                send_messageWithKeyboard(event.user_id, "keyboards/keyboard_busAndTram.json", event.random_id, message)
                                        case 110:
                                                send_messageWithKeyboard(event.user_id, "keyboardskeyboard_busAndTrol.json", event.random_id, message)
                                        case 111:
                                                send_messageWithKeyboard(event.user_id, "keyboards/keyboard_busAndTrolAndTram.json", event.random_id, message)                
                                    
                            else: 
                                    database.db_functions.update_condition(event.user_id, "21")
                                    rtype = database.db_functions.get_type_from_routeid(routeids)
                                    database.db_functions.update_type(event.user_id, rtype)
                                    database.db_functions.update_routeid(event.user_id, routeids)
                                    route_name = functions.get_route_name(event.user_id)
                                    message = """Выбран маршрут {}.\nНапиши \"Список\", чтобы узнать все остановки 
                                        выбранного маршрута или \"Расписание\".""".format(route_name)
                                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_listOrSchedule.json", event.random_id, message)
                    elif correctOption == "Меню":
                            database.db_functions.update_condition(event.user_id, "1")
                            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json", event.random_id, "Напиши \"Маршрут\" или \"Остановка\" для дальнейшей работы.")
                    else:
                            message = """Данный маршрут не поддерживается или не существует. Напишите маршрут заново. Также вы можете написать \"Меню\" для выхода """
                            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_menu.json", event.random_id, message)

    elif number_condition == 21:
            correctOption = check_match(event.text, option_list)
            if correctOption == "Список":
                    database.db_functions.update_condition(event.user_id, "1")
                    routeid = database.db_functions.take_routeid(event.user_id)
                    stops_list = database.db_functions.get_stops_from_routeid(routeid)
                    unique_stops = []
                    for stops in stops_list: 
                        name_list = database.db_functions.get_list_from_stops(stops)
                        for name in name_list:
                                if name not in unique_stops:
                                        unique_stops.append(name)     

                    route_name = functions.get_route_name(event.user_id)
                    message = "Список остановок через которые проходит маршрут - {}".format(route_name)
                    for name in unique_stops:
                        message += "\n- {name}".format(name=name)         
                    send_messageWithKeyboard(event.user_id,"keyboards/keyboard_routeOrStop.json", event.random_id, message)

            elif correctOption == "Расписание":
                    database.db_functions.update_condition(event.user_id, "1")
                    route_name = functions.get_route_name(event.user_id)
                    message = "Расписание маршрута {} загружается, пожалуйста подождите.".format(route_name)
                    send_message(event.user_id, event.random_id, message) 
                    routeid = database.db_functions.take_routeid(event.user_id)
                    message = get_schedule_from_routeid(routeid)
                    if message == "":
                            send_photo(event.peer_id, event.random_id, *upload_photo('schedule.jpg')) 
                            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json", event.random_id, "Напиши \"Маршрут\" или \"Остановка\" для дальнейшей работы.")
                    else:
                            message += "\nНапиши \"Маршрут\" или \"Остановка\" для дальнейшей работы."
                            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json", event.random_id, message)

            elif correctOption == "Меню":
                    database.db_functions.update_condition(event.user_id, "1")
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json", event.random_id, "Напиши \"Маршрут\" или \"Остановка\" для дальнейшей работы.")

            else:
                    message = """Данная функция не поддерживаются. Напишите функцию заново. Доступные функции: \"Список\", \"Расписание\" и \"Меню\"."""
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_listOrSchedule.json", event.random_id, message)                 

    elif number_condition == 22:
            correctOption = check_match(event.text, option_list)
            correctWord = check_match(event.text, ["Автобус", "Троллейбус", "Трамвай"])
            if correctWord != "Error" and correctOption == "Error": 
                    if correctWord == "Автобус":
                            route_type = "bus"
                    elif correctWord == "Троллейбус":
                            route_type = "trolleybus"
                    elif correctWord == "Трамвай":
                            route_type = "tram"       
                    number = database.db_functions.take_number(event.user_id)                                                 
                    routeid = database.db_functions.get_routeid_from_number_type(number, route_type)
                    database.db_functions.update_condition(event.user_id, "21")
                    database.db_functions.update_routeid(event.user_id, routeid)
                    database.db_functions.update_type(event.user_id, route_type)
                    message = """Выбран маршрут {rtype} №{number}.\nНапиши \"Список\", чтобы узнать все остановки 
                                выбранного маршрута или \"Расписание\".""".format(rtype=correctWord,number=number)
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_listOrSchedule.json", event.random_id, message)
            elif correctOption == "Меню":
                    database.db_functions.update_condition(event.user_id, "1")
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json", event.random_id, "Напиши \"Маршрут\" или \"Остановка\" для дальнейшей работы.")
            else:
                    message = """Данный маршрут не поддерживается или не существует. Напишите маршрут заново. Также вы можете написать \"Меню\" для выхода """
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_menu.json", event.random_id, message)


    elif number_condition == 30:
            correctWord = check_match(event.text, shortstopname_list)
            correctOption = check_match(event.text, option_list)
            if correctWord != "Error":
                    database.db_functions.update_start(event.user_id, correctWord)
                    database.db_functions.update_condition(event.user_id, 31)
                    message = """Выбрана начальная остановка - {}. Теперь напиши конечную остановку пути. \nЕсли начальная остановка выбрана неверно, напиши \"Назад\" и попробуй ввести название остановки заново""".format(correctWord)
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_wrongFirstStop.json", event.random_id, message)
                
            else:
                    correctWord = check_match(event.text, stopname_list)
                    if correctWord != "Error":
                            database.db_functions.update_start(event.user_id, correctWord)
                            database.db_functions.update_condition(event.user_id, 31)
                            message = """Выбрана начальная остановка - {}. Теперь напиши конечную остановку пути. \nЕсли начальная остановка выбрана неверно, напиши \"Назад\".""".format(correctWord)
                            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_wrongFirstStop.json", event.random_id, message)

                    elif correctOption == "Меню":
                            database.db_functions.update_condition(event.user_id, "1")
                            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json", event.random_id, "Напиши \"Маршрут\" или \"Остановка\" для дальнейшей работы.")

                    else:
                            message = """Данная остановка не поддерживаются. Напишите остановку заново.
                                Если хотите вернуться к выбору начальной остановки или к выбору маршрута напишите \"Меню\"."""
                            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_menu.json", event.random_id, message)   
                
    elif number_condition == 31:
            correctWord = check_match(event.text, shortstopname_list)
            correctOption = check_match(event.text, option_list)
            if correctWord != "Error":
                    database.db_functions.update_end(event.user_id, correctWord)
                    start = database.db_functions.take_start(event.user_id)
                    end = database.db_functions.take_end(event.user_id)
                    if database.db_functions.check_start_end(start, end) == None:
                            message = """Выбрана конечная остановка - {}.
                                Через выбранные остановки не проходит ни один поддерживаемый маршрут. Напиши начальную остановку заново.""".format(correctWord)
                            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_menu.json", event.random_id, message)
                            database.db_functions.update_condition(event.user_id, "30") 
                    else:
                            database.db_functions.update_condition(event.user_id, 32)
                            message = """Выбрана конечная остановка - {}. Теперь ты можешь написать следующие команды:
                                \n\"Время\", для того чтобы узнать время прибытия ближайшего транспорта для выбранного набора остановок. 
                                \n\"Транспорт\", для того чтобы узнать все маршруты, проходящие для выбранного набора остановок.""".format(correctWord)
                            send_messageWithKeyboard(event.user_id,"keyboards/keyboard_time.json", event.random_id, message)

            else:
                    correctWord = check_match(event.text, stopname_list)
                    if correctWord != "Error":
                            database.db_functions.update_end(event.user_id, correctWord)
                            start = database.db_functions.take_start(event.user_id)
                            end = database.db_functions.take_end(event.user_id)
                            if database.db_functions.check_start_end(start, end) == None:
                                    message = """Выбрана конечная остановка - {}.
                                        Через выбранные остановки не проходит ни один поддерживаемый маршрут. Напиши начальную остановку заново.""".format(correctWord)
                                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_menu.json", event.random_id, message)
                                    database.db_functions.update_condition(event.user_id, "30") 
                            else:
                                    database.db_functions.update_condition(event.user_id, 32)
                                    message = """Выбрана конечная остановка - {}. Теперь ты можешь написать следующие команды:
                                        \n\"Время\", для того чтобы узнать время прибытия ближайшего транспорта для выбранного набора остановок. 
                                        \n\"Транспорт\", для того чтобы узнать все маршруты, проходящие для выбранного набора остановок.""".format(correctWord)
                                    send_messageWithKeyboard(event.user_id,"keyboards/keyboard_time.json", event.random_id, message)

                    elif correctOption == "Меню":
                            database.db_functions.update_condition(event.user_id, "1")
                            send_messageWithKeyboard(event.user_id,"keyboards/keyboard_routeOrStop.json", event.random_id, "Напиши \"Маршрут\" или \"Остановка\" для дальнейшей работы.")
                        
                    elif correctOption == "Назад":
                            database.db_functions.update_condition(event.user_id, "30")
                            send_messageWithKeyboard(event.user_id,"keyboards/keyboard_menu.json", event.random_id, "Напиши название стартовой остановки пути.")

                    else:
                            message = """Данная функция не поддерживаются. Напишите функцию заново. Доступные функции: \"Назад\" и \"Меню\"."""
                            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_wrongFirstStop.json", event.random_id, message)
                
    elif number_condition == 32:
            correctOption = check_match(event.text, option_list)
            if correctOption == "Время":
                    database.db_functions.update_condition(event.user_id, 1)
                    start = database.db_functions.take_start(event.user_id)
                    end = database.db_functions.take_end(event.user_id)
                    stops_list = database.db_functions.check_start_end(start, end)
                    message = get_time_from_stops(stops_list[0][0],stops_list[0][1])
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json",  event.random_id, message)
                
            elif correctOption == "Транспорт":
                    database.db_functions.update_condition(event.user_id, 1)
                    start = database.db_functions.take_start(event.user_id)
                    end = database.db_functions.take_end(event.user_id)
                    stops_list = database.db_functions.check_start_end_for_routes(start, end)
                    message = """Через выбранные остановки проходят следующие маршруты:\n"""
                    routes = []
                    for stops in stops_list:
                                routeid = database.db_functions.get_routeid_from_stops(stops) 
                                route = database.db_functions.get_route_from_routeid(routeid)
                                rtype = database.db_functions.get_type_from_routeid(routeid)   
                                if route not in routes:
                                        routes.append(route)
                                        type_for_message = ""
                                        if rtype == "bus":
                                                type_for_message = "Автобус"
                                        elif rtype == "trolleybus":
                                                type_for_message = "Троллейбус"
                                        elif rtype == "tram":
                                                type_for_message = "Трамвай"
                                        message += """\n{} №{}""".format(type_for_message, route)
                    message += """\n\nНапиши \"Маршрут\" или \"Остановка\" для дальнейшей работы."""
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json",  event.random_id, message)

            elif correctOption == "Меню":
                    database.db_functions.update_condition(event.user_id, "1")
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json", event.random_id, "Напиши \"Маршрут\" или \"Остановка\" для дальнейшей работы.")

            else:
                    message = """Данная функция не поддерживаются. Напишите функцию заново. Доступные функции: \"Время\", \"Транспорт\" и \"Меню\"."""
                    send_messageWithKeyboard(event.user_id, "keyboards/keyboard_time.json", event.random_id, message)   

    else:
            database.db_functions.update_condition(event.user_id, "1")
            send_messageWithKeyboard(event.user_id, "keyboards/keyboard_routeOrStop.json",  event.random_id, "Напишите \"Маршрут\" или \"Остановка\" для дальнейшей работы.")

for event in longpoll.listen():
    if event.type == VkEventType.MESSAGE_NEW and event.to_me and event.text:
        t1 = threading.Thread(target=handle_message, args=(stopname_list, shortstopname_list, busname_list, option_list, event))
        t1.start()
