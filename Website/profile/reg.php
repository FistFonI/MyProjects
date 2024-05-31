<?php
echo "<br /><br /><h1><a href='\'>На главную!</a><h1><br>";
    require_once "DBClass.php";
    
    $db = new DBClass();

    $login = $_GET["login"];
    $password = $_GET["password"];
    $isEnter = $_GET["enter"] == "Войти";

    if (!$db->checkLink()) {
        return false;        
    }
    
    if ($isEnter) {
    // здесь авторизация    
        if ($db->checkUser($login, $password)) {
            echo "<h1>Вы вошли под именем $login!</h1>";
            return; 
        } 
        echo "<h1 class=''>Ошибка авторизации!</h1>";
        return;        
    }

    // здесь регистрация
    if ($db->getUser($login)) {
        echo "<h1>Пользователь $login уже существует в базе!</h1>";
        return;
    }

    if (!$db->setUser($login, $password)) {
        echo "<h1 class=''>Ошибка регистрации!</h1>";
        return;
    }
    echo "<h1>Пользователь <b>$login</b> успешно зарегистрирован!</h1>";
    
