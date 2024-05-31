<!DOCTYPE html>
<html lang='ru'>

<head>
    <meta charset='utf-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>
    <title>Добро пожаловать!</title>
</head> 

<body>
<!--    ?php проверить cookie с именем login    
if (кука есть), то не выводить этот блок
else выводить
?> -->
    <?php
        $login = $_COOKIE['login'];
        $isLogin = isset($_COOKIE['login']);
        if (!$isLogin) { ?>
        <div id="registration-section">
        <form>
            Введите логин
            <input type="text" name="login" id="login" required>
            Введите пароль
            <input type="password" name="password" id="password" required>                
            <input type="submit" class="submit" name="enter" id="enter" value="Войти" >
            <input type="submit" class="submit" name="registration" id="registration" value="Зарегистрироваться" >
        </form>
        </div>
        <br />        
    <?php } else {?>
        <span id="fetch-result"><h3>Вы вошли под именем <b><?=$login?></b>!<h3></span>
    <?php } ?>    
<script src="registration.js"></script>
</body>
</html>