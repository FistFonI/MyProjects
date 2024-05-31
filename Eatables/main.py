from aiogram import Bot, Dispatcher, types
from aiogram.types import ReplyKeyboardRemove, ReplyKeyboardMarkup, KeyboardButton
from aiogram.types import InlineKeyboardMarkup, InlineKeyboardButton
from aiogram.utils.keyboard import InlineKeyboardBuilder
from aiogram.filters.command import Command
import database
import asyncio
import logging
from aiogram import F

API_TOKEN =''

logging.basicConfig(level=logging.INFO)
bot = Bot(token=API_TOKEN)
dp = Dispatcher()

#
#database.add_new_quest("растет на дереве")
#database.add_new_answer("яблоко")
#database.add_new_answ_quest(2, 1, True)
#database.add_new_answ_quest(2, 2, True)

database.all_quest()
database.all_answer()
database.all_users()
database.all_answer_questions()
database.all_user_questions()
#

kb = [
    [
        types.KeyboardButton(text="Начать игру"),
        types.KeyboardButton(text="Вывести всю базу знаний"),
    ],
]
keyboard = types.ReplyKeyboardMarkup(
keyboard=kb, 
resize_keyboard=True,
)

builder_quest = InlineKeyboardBuilder()
builder_quest.add(types.InlineKeyboardButton(
    text="Да",
    callback_data="quest_yes_button")
)
builder_quest.add(types.InlineKeyboardButton(
    text="Нет",
    callback_data="quest_no_button")
)

builder_answ = InlineKeyboardBuilder()
builder_answ.add(types.InlineKeyboardButton(
    text="Да",
    callback_data="answ_yes_button")
)
builder_answ.add(types.InlineKeyboardButton(
    text="Нет",
    callback_data="answ_no_button")
)

builder_new = InlineKeyboardBuilder()
builder_new.add(types.InlineKeyboardButton(
    text="Начать новую игру",
    callback_data="new_game_button")
)
builder_new.add(types.InlineKeyboardButton(
    text="Почему такой ответ",
    callback_data="why_answer_button")
)

builder_new_answ = InlineKeyboardBuilder()
builder_new_answ.add(types.InlineKeyboardButton(
    text="Да",
    callback_data="new_yes_button")
)
builder_new_answ.add(types.InlineKeyboardButton(
    text="Нет",
    callback_data="new_no_button")
)

@dp.message(Command("start"))
async def send_welcome(message: types.Message):
    user_id = message.from_user.id
    database.add_new_user(user_id)     
    await message.reply("Привет!\nЯ бот-угадайка. Угадываю съедобные растения. Для старта нажми на кнопку \"Начать игру\".", reply_markup=keyboard)

@dp.message()
async def echo(message: types.Message):
    user_id = message.from_user.id
    database.add_new_user(user_id)
    condition = database.get_user_condition(user_id)
    if (message.text == "Начать игру"):
        database.delete_user_quests(user_id)
        quest = database.get_first_quest()
        database.update_user_condition(user_id, 10)
        database.update_user_quest(user_id, quest)
        await message.answer("""Это {}?""".format(quest), reply_markup=builder_quest.as_markup())

    elif (message.text == "Вывести всю базу знаний"):
        txt = database.all_database()
        await message.answer(txt, reply_markup=keyboard)

    elif (condition == 20):
        answer = message.text
        database.add_new_answer(answer)
        database.add_user_new_answer(user_id, answer)
        database.update_user_condition(user_id, 21)
        await message.answer("""Сформулируйте вопрос (с положительным ответом), ответ на который поможет отличить \"{}\" от других съедобных растений.""".format(answer))
    elif (condition == 21):
        quest = message.text
        database.add_new_quest(quest)
        database.add_user_new_quest(user_id, quest)
        database.update_user_condition(user_id, 22)
        add_new_answ(user_id, True)
        await message.answer("""Спасибо! Если хотите продолжить играть нажмите на кнопку.""", reply_markup=builder_new.as_markup()) 
        #await message.answer("""Подскажите вариант правильного ответа: да или нет.""", reply_markup=builder_new_answ.as_markup())   #Тут работает пока что правильно только если ответ да  

@dp.callback_query(F.data == "quest_yes_button")
async def start_quest_yes_value(callback: types.CallbackQuery):
    user_id = callback.from_user.id
    quest_id = database.get_quest_id_from_user_id(user_id)
    database.add_new_user_quest(user_id, quest_id, True)
    answer = database.check_answ(user_id)
    await callback.message.answer("""Это {}!""".format(answer), reply_markup=builder_answ.as_markup())

@dp.callback_query(F.data == "quest_no_button")
async def start_quest_no_value(callback: types.CallbackQuery):
    user_id = callback.from_user.id
    quest_id = database.get_quest_id_from_user_id(user_id)
    if (quest_id == 1):
        await callback.message.answer("""Это несъедобное растение! Ничем помочь не могу.""", reply_markup=builder_new.as_markup())
    else:
        is_end = database.is_end(user_id)
        if (is_end):
            database.update_user_condition(user_id, 20)
            await callback.message.answer("""Сдаюсь. Подскажите правильный ответ.""")
        else:
            quest = database.get_next_quest(user_id)
            quest_id = database.get_quest_id_from_quest(quest)
            database.add_new_user_quest(user_id, quest_id, False)
            database.update_user_quest(user_id, quest)
            await callback.message.answer("""Это {}?""".format(quest), reply_markup=builder_quest.as_markup())

@dp.callback_query(F.data == "answ_yes_button")
async def start_answ_yes_value(callback: types.CallbackQuery):
    await callback.message.answer("""Спасибо за игру! Если хотите начать заново, нажмите на кнопку.""", reply_markup=builder_new.as_markup())

@dp.callback_query(F.data == "answ_no_button")
async def start_answ_no_value(callback: types.CallbackQuery):
    user_id = callback.from_user.id
    is_end = database.is_end(user_id)
    if (is_end):
        database.update_user_condition(user_id, 20)
        await callback.message.answer("""Сдаюсь. Подскажите правильный ответ.""")
    else:
        quest = database.get_next_quest(user_id)
        database.update_user_quest(user_id, quest)
        await callback.message.answer("""Это {}?""".format(quest), reply_markup=builder_quest.as_markup())

@dp.callback_query(F.data == "new_game_button")
async def start_new_game(callback: types.CallbackQuery):
    user_id = callback.from_user.id
    database.delete_user_quests(user_id)
    quest = database.get_first_quest()
    database.update_user_quest(user_id, quest)
    await callback.message.answer("""Это {}?""".format(quest), reply_markup=builder_quest.as_markup())

@dp.callback_query(F.data == "why_answer_button")
async def start_why_answer(callback: types.CallbackQuery):
    user_id = callback.from_user.id
    txt = database.send_why_such_answer(user_id)
    await callback.message.answer(txt, reply_markup=builder_new.as_markup())

@dp.callback_query(F.data == "new_yes_button")
async def start_new_answ_yes_value(callback: types.CallbackQuery):
    user_id = callback.from_user.id
    add_new_answ(user_id, True)
    await callback.message.answer("""Спасибо! Если хотите продолжить играть нажмите на кнопку.""", reply_markup=builder_new.as_markup()) 

@dp.callback_query(F.data == "new_no_button")
async def start_new_answ_no_value(callback: types.CallbackQuery):
    user_id = callback.from_user.id
    add_new_answ(user_id, False)
    await callback.message.answer("""Спасибо! Если хотите продолжить играть нажмите на кнопку.""", reply_markup=builder_new.as_markup()) 

def add_new_answ(user_id, value):
    database.add_new_answ_quests(user_id)
    quest_id = database.get_new_quest_id(user_id)
    answ_id = database.get_new_answ_id(user_id)
    database.add_new_answ_quest(answ_id, quest_id, value)       
    database.update_user_condition(user_id, 0)

async def main():
    await dp.start_polling(bot)

if __name__ == '__main__':
   asyncio.run(main())

