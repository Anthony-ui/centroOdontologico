const validaciones = async () => {
    var cont = 0;

    await document.querySelector("#formulario").querySelectorAll("input.validador,textarea.validador,select.validador").forEach(res => {
        if (res.value.trim() == "") {
            res.classList.add("is-invalid");
            cont = cont + 1;
        } else {
            res.classList.remove("is-invalid");
        }
    });

    if (cont > 0) {
        return "ok";
    }
}

const vacio = async () => {
    await document.querySelector("#formulario").querySelectorAll("input.validador,textarea.validador,select.validador").forEach(res => {

        


        res.addEventListener("keyup", () => {

          

            if (res.classList.contains("email")) {

                return;
            }


            if (res.classList.contains("telefono")) {

                return;
            }


            if (res.classList.contains("decimal")) {

                return;
            }


      

            if (res.value.trim() == "") {
                res.classList.add("is-invalid");
            } else {
                res.classList.remove("is-invalid");
            }


        });

        res.addEventListener("change", () => {


            if (res.classList.contains("email")) {

                return;
            }


            if (res.value.trim() == "") {
                res.classList.add("is-invalid");
            } else {
                res.classList.remove("is-invalid");
            }
        });
    });
}

const vaciar = async () => {
    const form = document.querySelector("#formulario").querySelectorAll("input.validador,textarea.validador,select.validador");
    form.forEach(res => {
        res.value = "";
        res.classList.remove("is-invalid");
    });
}


const email = async (valor) => {

    const correo = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    const form = document.querySelector("#formulario").querySelectorAll("input.email");



    form.forEach(res => {
    



        if (!correo.test(valor)) {

            res.classList.add("is-invalid");
            

        } else {
            res.classList.remove("is-invalid");
        }


    });

}



const telefono = async (valor) => {

    const telefonoRegx = /^[0-9]+$/;
    const form = document.querySelector("#formulario").querySelectorAll("input.telefono");







    form.forEach(res => {



        if (!telefonoRegx.test(valor)) {

            res.classList.add("is-invalid");



        } else {
            res.classList.remove("is-invalid");
      
        }


    });

}







const decimal = async (valor) => {

    const decimalRegx = /^(\d+(?:[\.\,]\d{2})?)$/;
    const form = document.querySelector("#formulario").querySelectorAll("input.decimal");







    form.forEach(res => {



        if (!decimalRegx.test(valor)) {

            res.classList.add("is-invalid");



        } else {
            res.classList.remove("is-invalid");
      
        }


    });

}



vacio();
