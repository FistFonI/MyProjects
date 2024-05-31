<?php
class DBClass {
    private $server = "localhost";
    private $userName = "root";
    private $password = "root";
    private $dbName = "boyardb";
    private $port = 3306;

    private $link;

    function __construct(){
        $this->link = mysqli_connect(
            $this->server, 
            $this->userName, 
            $this->password,
            $this->dbName,
            $this->port
        );
    }

    function __destruct(){
        mysqli_close($this->link);
    }

    function checkLink(){
        if (!$this->link) {
            echo "Ошибка: соединение с БД не установлено!".PHP_EOL;
            echo "Текст ошибки:".mysqli_connect_error().PHP_EOL;
            return false;
        }
        return true;
    }

    function checkUser($login, $password) {
        $login = mysqli_real_escape_string($this->link, $login);
        $sqlText = "SELECT * FROM user WHERE login = '$login'";
        $result = mysqli_query($this->link, $sqlText);
        if (!$result) {
            echo "Запрос выполнен с ошибками!"
                .PHP_EOL
                .mysqli_error($this->link);
            return false;
        }
        $rowCount = mysqli_num_rows($result);
        if($rowCount == 0) {
            return false;
        }
        $row = mysqli_fetch_assoc($result);
        $hash = $row['password'];
        if (!password_verify($password, $hash)) 
            return false;
        return true;
    }

    function setUser($login, $password){
        $login = mysqli_real_escape_string($this->link, $login);
        $passwordHash = password_hash($password, PASSWORD_DEFAULT);
        $sqlText = "INSERT INTO user (login, password) 
            VALUES ('$login', '$passwordHash')";
        //$sqlText1 = "INSERT INTO user SET login = '$login', password = '$password'";
        $result = mysqli_query($this->link, $sqlText);
        if (!$result) {
            echo "Запрос выполнен с ошибками!"
                .PHP_EOL
                .mysqli_error($this->link);
            return false;
        } 
        return true;
    }

    function getUser($login) {
        $login = mysqli_real_escape_string($this->link, $login);
        $sqlText = "SELECT * FROM user WHERE login = ?";

        $stmt = mysqli_stmt_init($this->link);

        if (mysqli_stmt_prepare($stmt, $sqlText)) {
            mysqli_stmt_bind_param($stmt, 's', $login);            
            if (!mysqli_stmt_execute($stmt)) {
                echo "Запрос выполнен с ошибками!"
                    .PHP_EOL
                    .mysqli_error($this->link);
                return false;
            }
            mysqli_stmt_store_result($stmt);

            $rowCount = mysqli_stmt_num_rows($stmt);
            if($rowCount == 0) {
                return false;
            }        
            return true;            
        }
        return false;            
    }
}