<?php
    
    require_once "DBClass.php";
    
    $db = new DBClass();

    $login = $_POST["login"];
    $password = $_POST["password"];
    $isEnter = $_POST["isEnter"] === "true";    
    if (!$db->checkLink()) {
        echo json_encode(["error" => "Ошибка подключения к БД!"]);
        return ;        
    }
    
    if ($isEnter) {
    // здесь авторизация    
        if ($db->checkUser($login, $password)) {
            setcookie("login",$login);
            echo json_encode(["login" => $login]);
            //echo "<h1>Вы вошли под именем $login!</h1>";
            return; 
        } 
        echo json_encode(["error" => "Ошибка авторизации!"]);        
        return;        
    }

    // здесь регистрация
    if ($db->getUser($login)) {
        echo json_encode(["error" => "Пользователь $login уже существует в базе!"]);                
        return;
    }

    if (!$db->setUser($login, $password)) {
        echo json_encode(["error" => "Ошибка регистрации!"]);                        
        return;
    }
    echo json_encode(["login" => $login]);
    