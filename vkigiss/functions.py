import database.db_functions

import threading
import base64

def get_route_name(user_id):
    number = database.db_functions.take_number(user_id)
    route_type = database.db_functions.take_type(user_id)
    type_for_message = ""
    if route_type == "bus":
        type_for_message = "Автобус"
    elif route_type == "trolleybus":
        type_for_message = "Троллейбус"
    elif route_type == "tram":
        type_for_message = "Трамвай"   
    message = "{} №{}".format(type_for_message, number) 
    return message

def get_route_name_by_num_type(number, route_type):
    type_for_message = ""
    if route_type == "bus":
        type_for_message = "Автобус"
    elif route_type == "trolleybus":
        type_for_message = "Троллейбус"
    elif route_type == "tram":
        type_for_message = "Трамвай"   
    message = "{} №{}".format(type_for_message, number) 
    return message    

def decode_image(*args):
    image_64 = ''.join(args)
    decoded_image_data = base64.b64decode(image_64)       
    with open("schedule.jpg", "wb") as file:
        file.write(decoded_image_data)

def start_decode_thread(image_64):
    t1 = threading.Thread(target=decode_image, args=(image_64))
    t1.start()
    