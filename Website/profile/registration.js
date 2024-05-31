window.onload = () => {
    document.querySelectorAll(".submit")
        .forEach(el => el.onclick =
            (async (event) => {
                event.preventDefault(); 
                event.stopPropagation(); 
                let target = event.target;
                let isEnter = target.value=="Войти"?true:false;                
                let formData = new FormData();
                formData.append("login", document.getElementById("login").value);
                formData.append("password", document.getElementById("password").value);
                formData.append("isEnter", isEnter);
                let response = await fetch("registration.php",
                {
                    method: "POST",
                    body: formData
                });
                let result = await response.json();
                if (result.login) {
                    document.getElementById("registration-section").innerHTML = "";
                    document.getElementById("fetch-result").innerHTML = 
                        "<h3>Вы вошли под именем <b>" + result.login + "</b>!<h3>";     
                }
                if (result.error) {
                    document.getElementById("fetch-result").innerHTML = 
                        "<h3>" + result.error + "<h3>";
                }            
            })  
        )      
}

    